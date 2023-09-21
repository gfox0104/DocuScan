using Atalasoft.Twain;
using DocuScan.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace DocuScan.Api
{
   public class ScanProcess
    {
        private Acquisition _acquisition;
        private Device _device;
        private string _deviceName;
        private Log log;
        private string[] _deviceList;

        public bool IsDeviceOpen { get; private set; }

        public string[] DeviceList
        {
            get => this._deviceList;
            set => this._deviceList = value;
        }

        public ScanProcess()
        {
            this.log = new Log(Convert.ToBoolean(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings["WriteLog"].Value));
            this.InitializeTwain();
        }

        private void InitializeTwain()
        {
            if (this._acquisition == null)
            {
                this.log.Info("ScanProcess: Creating new acquisition object...");
                this._acquisition = new Acquisition();
            }
            if (!this._acquisition.SystemHasTwain)
                this.log.Warn("ScanProcess: System does not have twain!");
            else if (this._acquisition.Devices.Count == 0)
                this.log.Warn("ScanProcess: No acquisition devices found on this system!");
            if (!this._acquisition.SystemHasTwain || this._acquisition.Devices.Count == 0)
                return;
            this.log.Info("ScanProcess: Creating event handlers...");
            this._acquisition.AcquireCanceled += new EventHandler(this.AcquireCanceled);
            this._acquisition.AcquireFinished += new EventHandler(this.AcquireFinished);
            this._acquisition.AsynchronousException += new AsynchronousExceptionEventHandler(this.AsynchronousException);
            this._acquisition.ImageAcquired += new ImageAcquiredEventHandler(this.ImageAcquired);
            this._acquisition.FileTransfer += new FileTransferEventHandler(this._acquisition_FileTransfer);
            this.log.Info("ScanProcess: Selecting default device..");
            this._device = this._acquisition.Devices.Default;
            this.UpdateDeviceList();
        }

        private void _acquisition_FileTransfer(object sender, FileTransferEventArgs e)
        {
        }

        public void KillTwain()
        {
            if (this._device != null)
                this._device.Close();
            if (this._acquisition != null)
                this._acquisition.Dispose();
            this._device = (Device)null;
            this._acquisition = (Acquisition)null;
        }

        public void UpdateDeviceList()
        {
            this.log.Info("ScanProcess: Updating device list...");
            List<string> stringList = new List<string>();
            string empty = string.Empty;
            foreach (Device device in (ReadOnlyCollectionBase)this._acquisition.Devices)
            {
                try
                {
                    string productName = device.Identity.ProductName;
                    this.log.Info("ScanProcess: Aquisition device found: " + productName);
                    if (!productName.ToLower().Contains("webcam"))
                        stringList.Add(productName);
                    else
                        this.log.Info("ScanProcess: Aquisition device '" + productName + "' will not be added to the list.");
                }
                catch (Exception ex)
                {
                    this.log.Error("ScanProcess: Exception thrown by " + ex.Source + ": " + ex.Message);
                }
            }
            this.DeviceList = stringList.ToArray();
        }

        public void SetDevice(string deviceName)
        {
            this._deviceName = deviceName;
            bool flag = this._device != null && this._device.HideInterface;
            this._device = this._acquisition.Devices.GetDevice(deviceName);
            this._device.HideInterface = flag;
        }

        public void SetShowScanOptions(bool hide) => this._device.HideInterface = hide;

        public void AddAcquireCanceledEventHandler(Action<object, EventArgs> eventHandler) => this._acquisition.AcquireCanceled += (EventHandler)((o, ea) => eventHandler(o, ea));

        public void AddImageAcquiredEventHandler(Action<object, AcquireEventArgs> imageEventHandler) => this._acquisition.ImageAcquired += (ImageAcquiredEventHandler)((o, ea) => imageEventHandler(o, ea));

        public void AddAcquireFinishedEventhandler(Action<object, EventArgs> eventHandler) => this._acquisition.AcquireFinished += (EventHandler)((o, ea) => eventHandler(o, ea));

        public void AddFileTransferEventhandler(Action<object, FileTransferEventArgs> eventHandler) => this._acquisition.FileTransfer += (FileTransferEventHandler)((o, ea) => eventHandler(o, ea));

        public void AddAsynchronousExceptionEventHandler(
          Action<object, AsynchronousExceptionEventArgs> eventHandler)
        {
            this._acquisition.AsynchronousException += (AsynchronousExceptionEventHandler)((o, ea) => eventHandler(o, ea));
        }

        public event EventHandler<EventArgs> BeforeDeviceClose;

        public event EventHandler<EventArgs> AfterDeviceClose;

        public bool ScanImages() => this.ScanImages(-1);

        private bool ScanImages(int count)
        {
            try
            {
                if (this._device == null)
                {
                    this.logMessage("Current Device is null...");
                    return false;
                }
                this.logMessage("before _device.State.");
                this.logMessage(this._device.State.ToString());
                this.logMessage("before _device.State.");
                this.logMessage("before device.TryOpen()");
                if (this._device.TryOpen())
                {
                    this.logMessage("After device.TryOpen()");
                    this._device.TransferCount = count;
                    this.HandleVRS();
                    this.logMessage("before device.Acquire()");
                    this.IsDeviceOpen = true;
                    this._device.Acquire();
                    this.logMessage("After device.Acquire()");
                    return true;
                }
                this.logMessage("Device TryOpen Failed...");
            }
            catch (Exception ex)
            {
                this.logMessage(ex.ToString());
            }
            return false;
        }

        private void HandleVRS()
        {
            this.logMessage("Twain state = " + this._device.State.ToString());
            this.logMessage("  TWAIN Driver: " + this._device.Identity.VersionMajor.ToString() + "." + this._device.Identity.VersionMinor.ToString());
            this.logMessage("  Assessing device capabilities... ");
            foreach (DeviceCapability supportedCapability in this._device.GetSupportedCapabilities())
            {
                if (supportedCapability == DeviceCapability.CAP_DUPLEX)
                {
                    this.logMessage("Duplex Enabled.");
                    this._device.DuplexEnabled = true;
                }
                if (supportedCapability.ToString() == "53267" && this._device.Author.Contains("Kofax Software VRS"))
                {
                    this.logMessage("Long Document Option found.");
                    int num = (int)this._device.Controller.SetCapabilityValue(supportedCapability, true);
                    bool flag = true;
                    if (this._device.Controller.GetCapabilityValue(supportedCapability, out flag) == TwainReturnCode.TWRC_SUCCESS)
                        this.logMessage("Long Document Option enabled.");
                }
            }
            if (((IEnumerable<TwainTransferMethod>)this._device.GetSupportedTransferMethods()).Contains<TwainTransferMethod>(TwainTransferMethod.TWSX_FILE))
            {
                this.logMessage("File Transfer Mode Enabled.");
                this._device.TransferMethod = TwainTransferMethod.TWSX_FILE;
            }
            this._device.ModalAcquire = false;
            this._device.AutoScan = true;
            if (!this._device.Author.Contains("Kofax Software VRS"))
                this._device.ThreadingEnabled = true;
            this._device.DisplayProgressIndicator = false;
        }

        private void saveCapabilities(DeviceCapability[] capabilities)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "capabilities.txt");
            if (File.Exists(path))
                File.Delete(path);
            this.logMessage("Creating new 'capabilities.txt'");
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("This scanner (" + this._device.Identity.ProductName + ") supports the following capabilites:");
                streamWriter.WriteLine();
                for (int index = 0; index <= capabilities.Length - 1; ++index)
                    streamWriter.WriteLine(capabilities[index].ToString() + " (" + (object)capabilities[index] + ")");
            }
        }

        private void logMessage(string msg) => this.log.Info(msg);

        private void logError(string msg) => this.log.Error(msg);

        public bool ScanImage() => this.ScanImages(1);

        private void OnBeforeCloseDevice(EventArgs e)
        {
            if (this.BeforeDeviceClose == null)
                return;
            this.BeforeDeviceClose((object)this._acquisition, e);
        }

        private void OnAfterCloseDevice(EventArgs e)
        {
            if (this.AfterDeviceClose == null)
                return;
            this.AfterDeviceClose((object)this._acquisition, e);
        }

        private void AcquireCanceled(object sender, EventArgs e)
        {
            this.logMessage("before AcquiredCanceled ");
            this.closeDevice(e);
            this.logMessage("after AcquiredCanceled ");
        }

        private void AcquireFinished(object sender, EventArgs e)
        {
            this.logMessage("before AcquireFinished ");
            this.closeDevice(e);
            this.logMessage("after AcquireFinished");
        }

        private void AsynchronousException(object sender, AsynchronousExceptionEventArgs e) => this.closeDevice((EventArgs)e);

        private void closeDevice(EventArgs e)
        {
            try
            {
                this.OnBeforeCloseDevice(e);
                this._device.Close();
            }
            catch (Exception ex)
            {
                this.logError(ex.ToString());
            }
            finally
            {
                this.IsDeviceOpen = false;
                this.OnAfterCloseDevice(e);
            }
        }

        private void ImageAcquired(object sender, AcquireEventArgs e)
        {
        }
    }
}