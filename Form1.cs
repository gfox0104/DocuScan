using Atalasoft.Imaging;
using Atalasoft.Imaging.Codec;
using Atalasoft.Imaging.Codec.Pdf;
using Atalasoft.Imaging.Codec.Tiff;
using Atalasoft.Imaging.ColorManagement;
using Atalasoft.Imaging.ImageProcessing.Transforms;
using Atalasoft.Imaging.WinControls;
using Atalasoft.PdfDoc;
using Atalasoft.Twain;
using DocuScan.Api;
using DocuScan.Logging;
using DocuScan.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DocuScan
{
    public partial class Form1 : Form
    {
        private string tempFolderPath;
        private string currentBatchFolder;
        private int batchCount;
        private int scanCount;
        private int pageCount;
        private int scanLimit;
        private bool appending;
        private bool reScan;
        private bool insertBeforeScan;
        private bool insertAfterScan;
        private int insertAfterOrder;
        private Log log;
        private int numBGW;
        private string defaultDestinationPath;
        private bool appendingScanEnabled;
        private ScanProcess scanner;
        private StatusStrip statusStrip;


        public Form1()
        {
            
            ConfigurationManager.RefreshSection("appSettings");
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool logEnabled = false;
            if (configuration.AppSettings.Settings["WriteLog"] == null)
            {
                configuration.AppSettings.Settings.Add("WriteLog", "false");
                configuration.Save(ConfigurationSaveMode.Modified);
            }
            else
                logEnabled = Convert.ToBoolean(configuration.AppSettings.Settings["WriteLog"].Value);
            StartLog(logEnabled);
            try
            {
                log.Info("Initializing UI");
               InitializeComponent();
                numBGW = 0;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            if (configuration.AppSettings.Settings["DefaultDestinationPath"] != null)
            {
                configuration.AppSettings.Settings.Remove("DefaultDestinationPath");
                configuration.Save(ConfigurationSaveMode.Modified);
            }
            InitScanProcess();
            InitThumbnailViewer();
            InitScanSettings();
            InitParams();
            ConfigurationManager.RefreshSection("appSettings");
            log.Info("== Initialize Bulk Scan Complete ==");
        }

        private void InitScanProcess()
        {
            log.Info("Initializing Scan Process");
            try
            {
                scanner = new ScanProcess();
                scanner.AddAcquireCanceledEventHandler(new Action<object, EventArgs>(ScanCanceled));
                scanner.AddAcquireFinishedEventhandler(new Action<object, EventArgs>(ScanFinished));
                scanner.AddAsynchronousExceptionEventHandler(new Action<object, AsynchronousExceptionEventArgs>(AsynchronousExceptionHandler));
                scanner.AddImageAcquiredEventHandler(new Action<object, AcquireEventArgs>(ScanAcquired));
                scanner.AddFileTransferEventhandler(new Action<object, FileTransferEventArgs>(ScanFileTransfered));
                scanner.BeforeDeviceClose += new EventHandler<EventArgs>(Scanner_BeforeDeviceClose);
                scanner.AfterDeviceClose += new EventHandler<EventArgs>(Scanner_AfterDeviceClose);
            }
            catch (Exception ex)
            {
                log.Error("Exception trying to initialize a new ScanProcess! Source: " + ex.Source + " - Message: " + ex.Message);
            }
        }

        private void InitThumbnailViewer()
        {
            try
            {
                log.Info("Initializing Thumbnail Viewer");
                tempFolderPath = Path.GetTempPath() + "\\DotTwain Temp";
                SetupTempFolder();
                scanCount = 0;
                batchCount = 0;
                int num = int.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
                thumbnailViewer.ThumbnailSize = new Size(num, num);
                thumbnailViewer.Refresh();
            }
            catch (Exception ex)
            {
                log.Error("Error | Thumbnail Viewer | Message: " + ex.Message);
            }
        }

        private void InitScanSettings()
        {
            try
            {
                log.Info("Initializing Scan Settings");
                appending = false;
                reScan = false;
                insertBeforeScan = false;
                insertAfterScan = false;
                insertAfterOrder = 1;
                scanLimit = int.Parse(ConfigurationManager.AppSettings["ScanLimit"]);
                string appSetting = ConfigurationManager.AppSettings["ScannerLastUsed"];
                if (scanner.DeviceList != null && scanner.DeviceList.Length > 0)
                {
                    if (appSetting != null && ((IEnumerable<string>)scanner.DeviceList).Contains<string>(appSetting))
                    {
                        scannerComboBox.Text = appSetting;
                        scanner.SetDevice(appSetting);
                    }
                    else
                    {
                        scannerComboBox.Text = scanner.DeviceList[0];
                        scanner.SetDevice(scanner.DeviceList[0]);
                    }
                    scannerComboBox.Items.Clear();
                    scannerComboBox.Items.AddRange((object[])scanner.DeviceList);
                    scannerComboBox.SelectedIndex = 0;
                    if (scannerComboBox.Items.Count != 0)
                        return;
                    scannerComboBox.Text = "No Scanner Detected";

                }
                else
                {
                    scannerComboBox.Text = "No Scanner Detected";
                    scanButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error | Scan Settings | Message: " + ex.Message);
            }
        }

        private void InitParams()
        {
            try
            {
                log.Info("Initializing Destination Settings");
                GetAliases();
                if (destinationsComboBox.Items.Cast<string>().ToList<string>().Contains(ConfigurationManager.AppSettings["DestinationLastUsed"]))
                {
                    destinationsComboBox.Text = ConfigurationManager.AppSettings["DestinationLastUsed"];
                    destinationsComboBox.SelectedItem = (object)ConfigurationManager.AppSettings["DestinationLastUsed"];
                }
                else
                {
                    destinationsComboBox.Text = "Scan to Desktop";
                    destinationsComboBox.SelectedItem = (object)"Scan to Desktop";
                }
            }
            catch (Exception ex)
            {
                log.Error("Error | InitParams | Message: " + ex.Message);
            }
        }

        private void AttemptSettingsMigration()
        {
            log.Info("Settings Migration Status is pending.");
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Royal Imaging\\Bulk Scan\\Settings.ini";
            if (File.Exists(path))
            {
                log.Info("Old settings file found at " + path);
                StreamReader streamReader = new StreamReader(path);
                string str1 = "";
                string source = streamReader.ReadLine();
                while (source != "")
                {
                    string str2 = source;
                    source = streamReader.ReadLine();
                    switch (str2)
                    {
                        case "[SCANNER]":
                            if (settings["ScannerLastUsed"] == null)
                                settings.Add("ScannerLastUsed", source);
                            else
                                settings["ScannerLastUsed"].Value = source;
                            log.Info("Setting for ScannerLastused is set to " + source);
                            continue;
                        case "[SCANLIMIT]":
                            if (settings["ScanLimit"] == null)
                                settings.Add("ScanLimit", source);
                            else
                                settings["ScanLimit"].Value = source;
                            log.Info("Setting for ScanLimit is set to " + source);
                            continue;
                        case "[THUMBNAILSIZE]":
                            if (settings["ThumbnailSize"] == null)
                                settings.Add("ThumbnailSize", source);
                            else
                                settings["ThumbnailSize"].Value = source;
                            log.Info("Setting for ThumbnailSize is set to " + source);
                            continue;
                        case "[DESTINATIONS]":
                            int num = 0;
                            string str3 = "";
                            for (; source.Contains<char>('|'); source = streamReader.ReadLine())
                            {
                                ++num;
                                if (num > 1)
                                    str3 += ";";
                                str3 += source;
                                str1 = source;
                            }
                            if (settings["Destinations"] == null)
                                settings.Add("Destinations", str3);
                            else
                                settings["Destinations"].Value = str3;
                            log.Info("Setting for Destinations is set to " + source);
                            continue;
                        default:
                            continue;
                    }
                }
                streamReader.Close();
                if (settings["SettingsMigrationStatus"] == null)
                    settings.Add("SettingsMigrationStatus", "Done");
                else
                    settings["SettingsMigrationStatus"].Value = "Done";
                configuration.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                log.Info("Old settings file NOT found at " + path);
                if (settings["SettingsMigrationStatus"] == null)
                    settings.Add("SettingsMigrationStatus", "Old Settings Not Found");
                else
                    settings["SettingsMigrationStatus"].Value = "Old Settings Not Found";
            }
            ConfigurationManager.RefreshSection("appSettings");
            configuration.Save(ConfigurationSaveMode.Modified);
        }

        private void StartLog(bool logEnabled)
        {
            string path1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).ToString() + "\\Office Gemini";
            string path2 = path1 + "\\Bulk Scan";
            if (!Directory.Exists(path1))
                Directory.CreateDirectory(path1);
            if (!Directory.Exists(path2))
                Directory.CreateDirectory(path2);
            log = new Log(logEnabled);
            log.Info("*** Bulk Scan Session Log Started ***");
            log.Info("User: " + Environment.UserName);
            log.Info("  PC: " + Environment.MachineName);
            log.Info("  OS: " + Environment.OSVersion.ToString());
        }

        private void GetAliases()
        {
            string str1 = destinationsComboBox.SelectedItem == null ? "Scan to Desktop" : destinationsComboBox.SelectedItem.ToString();
            destinationsComboBox.Items.Clear();
            string[] strArray = ConfigurationManager.AppSettings["Destinations"].Split(';');
            defaultDestinationPath = GetDefaultDestination();
            if (!Directory.Exists(defaultDestinationPath))
                Directory.CreateDirectory(defaultDestinationPath);
            log.Info("Adding default destination to the dropdown");
            destinationsComboBox.Items.Add((object)"Scan to Desktop");
            foreach (string str2 in strArray)
            {
                if (str2.Length > 0)
                {
                    string str3 = str2.Split('|')[0];
                    destinationsComboBox.Items.Add((object)str3);
                    log.Info("Found and Added destination '" + str3 + "' to the dropdown.");
                }
            }
            if (destinationsComboBox.Items.IndexOf((object)str1) >= 0)
                destinationsComboBox.SelectedIndex = destinationsComboBox.Items.IndexOf((object)str1);
            else
                destinationsComboBox.SelectedIndex = 0;
        }

        private void SetupTempFolder()
        {
            if (Directory.Exists(tempFolderPath))
            {
                foreach (string file in Directory.GetFiles(tempFolderPath))
                    File.Delete(file);
            }
            else
                Directory.CreateDirectory(tempFolderPath);
            log.Info("Temporary folder created at " + tempFolderPath);
        }

        private void DisableControls()
        {
            log.Info(nameof(DisableControls));
            mergeAndTransferButton.Enabled = false;
            scanButton.Enabled = false;
            insertBeforeButton.Enabled = false;
            insertBehindButton.Enabled = false;
            rescanButton.Enabled = false;
        }

        private void ReverseEnableControls(Control control)
        {
            if (control == null)
                return;
            control.Enabled = true;
            ReverseEnableControls(control.Parent);
        }

        private void ToggleScanAndExportButtons()
        {
            try
            {
                if (scanner.IsDeviceOpen)
                    return;
                scanButton.Enabled = true;
                if (thumbnailViewer.SelectedItems.Count == 0)
                {
                    insertBeforeButton.Enabled = false;
                    insertBehindButton.Enabled = false;
                    rescanButton.Enabled = false;
                }
                else if (thumbnailViewer.SelectedItems.Count == 1)
                {
                    insertBeforeButton.Enabled = true;
                    insertBehindButton.Enabled = true;
                    rescanButton.Enabled = true;
                }
                else
                {
                    insertBeforeButton.Enabled = false;
                    insertBehindButton.Enabled = false;
                    rescanButton.Enabled = false;
                }
                if (thumbnailViewer.Items.Count > 0)
                    mergeAndTransferButton.Enabled = true;
                else
                    mergeAndTransferButton.Enabled = false;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void UpdateEditButtons()
        {
            if (thumbnailViewer.SelectedItems.Count == 0)
            {
                rotateClockwiseButton.Enabled = false;
                rotateCounterClockButton.Enabled = false;
                deletePageButton.Enabled = false;
            }
            else if (thumbnailViewer.SelectedItems.Count == 1)
            {
                rotateClockwiseButton.Enabled = true;
                rotateCounterClockButton.Enabled = true;
                deletePageButton.Enabled = true;
            }
            else
            {
                rotateClockwiseButton.Enabled = true;
                rotateCounterClockButton.Enabled = true;
                deletePageButton.Enabled = true;
            }
            deleteBatchButton.Enabled = thumbnailViewer.Items.Count > 0;
            pageCountStatusStripLabel.Text = "Page Count: " + (object)thumbnailViewer.Items.Count;
        }

        private bool UpdateUIBasedOnDeviceList()
        {
            try
            {
                if (scanner.DeviceList.Length != 0)
                    return true;
                scanButton.Enabled = false;
                insertBeforeButton.Enabled = false;
                insertBehindButton.Enabled = false;
                rescanButton.Enabled = false;
                scannerComboBox.Text = "No Scanner Detected";
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Update");
                throw;
            }
                                  
        }

        private string GetDefaultDestination() => Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DocuWare";

               private string GetDestinationPathFromAlias(string alias)
        {
            string str = GetDefaultDestination();
            if (alias != "Scan to Desktop")
                str = ((IEnumerable<string>)ConfigurationManager.AppSettings["Destinations"].Split(';')).ToList<string>().First<string>((Func<string, bool>)(d => d.Split('|')[0] == alias)).Replace(alias + "|", "");
            return str;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            splitContainer.Width = Width - 14;
            splitContainer.Height = Height - 104;
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            thumbnailViewer.Width = splitContainer.SplitterDistance - 6;
            thumbnailViewer.Height = splitContainer.Height - 7;
            workspaceViewer.Width = splitContainer.Width - splitContainer.SplitterDistance - 11;
            workspaceViewer.Height = splitContainer.Height - 7;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            thumbnailViewer.Width = splitContainer.SplitterDistance - 6;
            workspaceViewer.Width = splitContainer.Width - splitContainer.SplitterDistance - 11;
        }

       private void scanButton_MouseEnter(object sender, EventArgs e)

        {
            if (!appendingScanEnabled)
                commandDetailStatusStripLabel.Text = "Start a new scanning batch.";
            else
                commandDetailStatusStripLabel.Text = "Append new pages to the current batch.";
        }

        private void hideShowInterfaceButton_MouseEnter(object sender, EventArgs e)
        {
            if (hideShowInterfaceButton.Checked)
                commandDetailStatusStripLabel.Text = "Scanner interface is currently hidden; click to turn this option off.";
            else
                commandDetailStatusStripLabel.Text = "Hide the scanner interface for unobscured scanning.";
        }

        private void insertBeforeButton_MouseEnter(object sender, EventArgs e) 
        { 
            commandDetailStatusStripLabel.Text = "Scan one or more pages to be inserted before (above) the selected page."; 
        }

        private void insertBehindButton_MouseEnter(object sender, EventArgs e) 
        { 
            commandDetailStatusStripLabel.Text = "Scan one or more pages to be inserted behind (under) the selected page."; 
        }

        private void rescanButton_MouseEnter(object sender, EventArgs e) 
        { 
            commandDetailStatusStripLabel.Text = "Rescan the selected page - or, replace the selected page with a new scan."; 
        }

        private void rotateClockwiseButton_MouseEnter(object sender, EventArgs e)
        {
            if (thumbnailViewer.SelectedItems.Count > 1)
                commandDetailStatusStripLabel.Text = "Rotate the selected pages 90º clockwise.";
            else
                commandDetailStatusStripLabel.Text = "Rotate the selected page 90º clockwise.";
        }

        private void rotateCounterClockButton_MouseEnter(object sender, EventArgs e)
        {
            if (thumbnailViewer.SelectedItems.Count > 1)
                commandDetailStatusStripLabel.Text = "Rotate the selected pages 90º counter-clockwise.";
            else
                commandDetailStatusStripLabel.Text = "Rotate the selected page 90º counter-clockwise.";
        }

        private void deletePageButton_MouseEnter(object sender, EventArgs e)
        {
            if (thumbnailViewer.SelectedItems.Count > 1)
                commandDetailStatusStripLabel.Text = "Delete the selected pages from this batch.";
            else
                commandDetailStatusStripLabel.Text = "Delete the selected page from this batch.";
        }

        private void scanButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void hideShowInterfaceButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void insertBeforeButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void insertBehindButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void rescanButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void rotateClockwiseButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void rotateCounterClockButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void deletePageButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void deleteBatchButton_MouseEnter(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "Delete the entire batch permanently.";

        private void mergeAndTransferButton_MouseEnter(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "Merge the pages in this batch to one multi-page file and copy to the selected destination.";

        private void optionsButton_MouseEnter(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "View and edit application settings.";

        private void aboutButton_MouseEnter(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "About this application...";

        private void deleteBatchButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void mergeAndTransferButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void optionsButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";

        private void aboutButton_MouseLeave(object sender, EventArgs e) => commandDetailStatusStripLabel.Text = "";
        
        private void scannerComboBox_ScannerSelected(object sender, EventArgs e)
        {
            scanner.SetDevice((sender as ToolStripComboBox).SelectedItem as string);
            scanButton.Enabled = true;
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            log.Info("Scan button clicked.");
            if (scanner.DeviceList == null || scanner.DeviceList.Length == 0)
                scanner.UpdateDeviceList();
            if (!UpdateUIBasedOnDeviceList())
                return;
            commandDetailStatusStripLabel.Text = "";
            reScan = false;
            insertBeforeScan = false;
            insertAfterScan = false;
            thumbnailViewer.SelectedItems.Clear();
            if (!appending)
            {
                ++batchCount;
                currentBatchFolder = tempFolderPath + "\\Batch" + batchCount.ToString();
                if (Directory.Exists(currentBatchFolder))
                    Directory.Delete(currentBatchFolder, true);
                Directory.CreateDirectory(currentBatchFolder);
            }
            pageCount = thumbnailViewer.Items.Count;
            DisableControls();
            if (scanner.ScanImages())
                return;
            ToggleScanAndExportButtons();
            Select();
        }

        private void hideShowInterfaceButton_Click(object sender, EventArgs e)
        {
            log.Info("Hide/Show Interface button clicked.");
            commandDetailStatusStripLabel.Text = "";
            scanner.SetShowScanOptions(hideShowInterfaceButton.Checked);
            if (hideShowInterfaceButton.Checked)
            {
                hideShowInterfaceButton.Image = Resources.NoInterface_On_32x32;
                hideShowInterfaceButton.ToolTipText = "Show Scanner Interface <H>";
            }
            else
            {
                hideShowInterfaceButton.Image = Resources.NoInterface_Off_32x32;
                
                hideShowInterfaceButton.ToolTipText = "Hide Scanner Interface <H>";
            }
        }

        private void insertBeforeButton_Click(object sender, EventArgs e)
        {
            if (!UpdateUIBasedOnDeviceList())
                return;
            log.Info("Insert Before button clicked.");
            commandDetailStatusStripLabel.Text = "";
            reScan = false;
            insertBeforeScan = true;
            insertAfterScan = false;
            pageCount = thumbnailViewer.Items.Count;
            DisableControls();
            if (scanner.ScanImages())
                return;
            ToggleScanAndExportButtons();
            Select();
        }

        private void insertBehindButton_Click(object sender, EventArgs e)
        {
            if (!UpdateUIBasedOnDeviceList())
                return;
            log.Info("Insert Behind button clicked.");
            commandDetailStatusStripLabel.Text = "";
            reScan = false;
            insertBeforeScan = false;
            insertAfterScan = true;
            insertAfterOrder = 1;
            pageCount = thumbnailViewer.Items.Count;
            DisableControls();
            if (scanner.ScanImages())
                return;
            ToggleScanAndExportButtons();
            Select();
        }

        private void rescanButton_Click(object sender, EventArgs e)
        {
            if (!UpdateUIBasedOnDeviceList())
                return;
            log.Info("Rescan button clicked.");
            commandDetailStatusStripLabel.Text = "";
            reScan = true;
            insertBeforeScan = false;
            insertAfterScan = false;
            pageCount = thumbnailViewer.Items.Count;
            DisableControls();
            if (scanner.ScanImage())
                return;
            ToggleScanAndExportButtons();
            Select();
        }

        private void rotateClockwiseButton_Click(object sender, EventArgs e)
        {
            commandDetailStatusStripLabel.Text = "";
            if (thumbnailViewer.SelectedItems.Count == 0)
                return;
            log.Info("Rotate Clockwise button clicked.");
            RotateCommand rotateCommand = new RotateCommand(90.0);
            AtalaImage image1 = workspaceViewer.Images.Current;
            foreach (Thumbnail selectedItem in (ReadOnlyCollectionBase)thumbnailViewer.SelectedItems)
            {
                AtalaImage image2 = rotateCommand.ApplyToImage(new AtalaImage(selectedItem.FilePath));
                if (selectedItem == thumbnailViewer.FocusedItem)
                    image1 = image2;
                image2.Save(selectedItem.FilePath, (ImageEncoder)new TiffEncoder(TiffCompression.Default, false), (ProgressEventHandler)null);
                thumbnailViewer.Items.Update(selectedItem, image2);
            }
            if (image1 != null)
                workspaceViewer.Images.Replace(image1);
            workspaceViewer.Update();
        }

        private void rotateCounterClockButton_Click(object sender, EventArgs e)
        {
            commandDetailStatusStripLabel.Text = "";
            if (thumbnailViewer.SelectedItems.Count == 0)
                return;
            log.Info("Rotate Counter-Clockwise button clicked.");
            RotateCommand rotateCommand = new RotateCommand(270.0);
            AtalaImage image1 = workspaceViewer.Images.Current;
            foreach (Thumbnail selectedItem in (ReadOnlyCollectionBase)thumbnailViewer.SelectedItems)
            {
                AtalaImage image2 = rotateCommand.ApplyToImage(new AtalaImage(selectedItem.FilePath));
                if (selectedItem == thumbnailViewer.FocusedItem)
                    image1 = image2;
                image2.Save(selectedItem.FilePath, (ImageEncoder)new TiffEncoder(TiffCompression.Default, false), (ProgressEventHandler)null);
                thumbnailViewer.Items.Update(selectedItem, image2);
            }
            if (image1 != null)
                workspaceViewer.Images.Replace(image1);
            workspaceViewer.Update();
        }

        private void deletePageButton_Click(object sender, EventArgs e)
        {
            log.Info("Delete Page button clicked.");
            commandDetailStatusStripLabel.Text = "";
            if (MessageBox.Show("Do you want to delete selected batch page?", "Confirm Page Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No || thumbnailViewer.SelectedItems.Count == 0)
                return;
            foreach (Thumbnail selectedItem in (ReadOnlyCollectionBase)thumbnailViewer.SelectedItems)
                File.Delete(selectedItem.FilePath);
            Thumbnail[] thumbnailArray = new Thumbnail[thumbnailViewer.SelectedItems.Count];
            thumbnailViewer.SelectedItems.CopyTo(thumbnailArray);
            thumbnailViewer.Items.Remove(thumbnailArray);
            if (thumbnailViewer.FocusedItem == null || !File.Exists(thumbnailViewer.FocusedItem.FilePath))
                workspaceViewer.Images.Clear();
            UpdateEditButtons();
            ToggleScanAndExportButtons();
        }

        private void deleteBatchButton_Click(object sender, EventArgs e)
        {
            log.Info("Delete Batch button clicked.");
            commandDetailStatusStripLabel.Text = "";
            if (MessageBox.Show("Do you want to delete the whole batch?", "Confirm Batch Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;
            thumbnailViewer.Items.Clear();
            workspaceViewer.Images.Clear();
            scanButton.Image = (Image)Resources.Scanner1_32x32;
            pageCount = 0;
            appending = false;
            Directory.Delete(currentBatchFolder, true);
            UpdateEditButtons();
            ToggleScanAndExportButtons();
        }

        private void mergeAndTransferButton_Click(object sender, EventArgs e)
        {
            log.Info("Merge and Transfer Button clicked.");
            string destinationPathFromAlias = GetDestinationPathFromAlias(destinationsComboBox.SelectedItem.ToString());
            if (Directory.Exists(destinationPathFromAlias))
            {
                commandDetailStatusStripLabel.Text = "";
                List<string> stringList = new List<string>();
                foreach (Thumbnail thumbnail in (CollectionBase)thumbnailViewer.Items)
                    stringList.Add(thumbnail.FilePath);
                string[] array = new string[stringList.Count];
                stringList.CopyTo(array);
                workspaceViewer.Images.Clear();
                thumbnailViewer.Items.Clear();
                UpdateEditButtons();
                ToggleScanAndExportButtons();
                scanButton.Image = (Image)Resources.Scanner1_32x32;
                appending = false;
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += new DoWorkEventHandler(mergeAndTransferWorker_DoWork);
                backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(mergeAndTransferWorker_ProgressChanged);
                backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mergeAndTransferWorker_RunWorkerCompleted);
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.WorkerSupportsCancellation = false;
                Tuple<string[], string, string, string, string, int> tuple = new Tuple<string[], string, string, string, string, int>(array, ConfigurationManager.AppSettings["ExportFiletype"].ToString(), currentBatchFolder, destinationPathFromAlias, GetExportDocumentName(), batchCount);
                backgroundWorker.RunWorkerAsync((object)tuple);
            }
            else
            {
                int num = (int)MessageBox.Show("The path '" + destinationPathFromAlias + "' does not exist.\nEnsure target path exists.", "Target Path Does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

       
        private void thumbnailViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (thumbnailViewer.FocusedItem == null)
                workspaceViewer.Images.Clear();
            else if (File.Exists(thumbnailViewer.FocusedItem.FilePath))
                workspaceViewer.Open(thumbnailViewer.FocusedItem.FilePath, 0);
            UpdateEditButtons();
            ToggleScanAndExportButtons();
            UpdateUIBasedOnDeviceList();
        }

        private void Scanner_BeforeDeviceClose(object sender, EventArgs e)
        {
            UpdateEditButtons();
            if (thumbnailViewer.Items.Count > 0)
                mergeAndTransferButton.Enabled = true;
            else
                mergeAndTransferButton.Enabled = false;
        }

        private void Scanner_AfterDeviceClose(object sender, EventArgs e)
        {
            ToggleScanAndExportButtons();
            Select();
        }

        private void ScanCanceled(object sender, EventArgs e)
        {
        }

        private void ScanFileTransfered(object sender, FileTransferEventArgs e)
        {
            e.FileFormat = SourceImageFormat.Tiff;
            e.FileName = GetFileName();
        }

        private string GetFileName()
        {
            string empty = string.Empty;
            ++scanCount;
            if (!reScan)
                ++pageCount;
            return currentBatchFolder + "\\scan" + scanCount.ToString().PadLeft(3, '0') + ".tif";
        }

        private void ScanAcquired(object sender, AcquireEventArgs e)
        {
            if (pageCount <= scanLimit)
            {
                AtalaImage image = (AtalaImage)null;
                string fileName = string.Empty;
                if (e.Image != null)
                {
                    fileName = GetFileName();
                    image = AtalaImage.FromBitmap(e.Image);
                    image.Save(fileName, (ImageEncoder)new TiffEncoder(TiffCompression.Default, false), (ProgressEventHandler)null);
                }
                else if (e.FileName != null && File.Exists(e.FileName))
                {
                    fileName = e.FileName;
                    using (AtalaImage atalaImage = new AtalaImage(e.FileName))
                        atalaImage.Save(fileName, (ImageEncoder)new TiffEncoder(TiffCompression.Default, false), (ProgressEventHandler)null);
                    image = new AtalaImage(fileName);
                }
                if (reScan && thumbnailViewer.SelectedItems.Count == 1)
                {
                    image.Save(thumbnailViewer.SelectedItems[0].FilePath, (ImageEncoder)new TiffEncoder(TiffCompression.Default, false), (ProgressEventHandler)null);
                    thumbnailViewer.Items.Update(thumbnailViewer.SelectedItems[0], image);
                    workspaceViewer.Images.Replace(image);
                }
                else if (insertBeforeScan && thumbnailViewer.SelectedItems.Count == 1)
                    thumbnailViewer.Items.Insert(thumbnailViewer.Items.IndexOf(thumbnailViewer.SelectedItems[0]), fileName, 0, "");
                else if (insertAfterScan && thumbnailViewer.SelectedItems.Count == 1)
                {
                    thumbnailViewer.Items.Insert(thumbnailViewer.Items.IndexOf(thumbnailViewer.SelectedItems[0]) + insertAfterOrder++, fileName, 0, "");
                }
                else
                {
                    thumbnailViewer.Items.Add(fileName, 0, "", "");
                    thumbnailViewer.Items[thumbnailViewer.Items.Count - 1].Selected = true;
                    workspaceViewer.Images.Replace(image);
                    workspaceViewer.Image = (AtalaImage)null;
                }
            }
            else if (pageCount == scanLimit + 1)
            {
                int num = (int)MessageBox.Show("This document has reached the page limit." + Environment.NewLine + "Please remove remaining pages from the document feeder." + Environment.NewLine + "Any images pending in the queue will be discarded.", "Page Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            workspaceViewer.Update();
        }

        private void AsynchronousExceptionHandler(object sender, AsynchronousExceptionEventArgs e)
        {
        }

        private void ScanFinished(object sender, EventArgs e)
        {
            if (reScan)
                return;
            if (appending)
                log.Info(pageCount.ToString() + " image(s) scanned to Batch " + (object)batchCount + ".");
            else if (thumbnailViewer.Items.Count > 0)
            {
                log.Info(pageCount.ToString() + " image(s) scanned to Batch " + (object)batchCount + ". Switching to append mode...");
                appending = true;
                scanButton.Image = (Image)Resources.ScanAppend_32x32;
            }
            else
            {
                log.Info("One page has been rescanned in Batch " + (object)batchCount + ".");
                thumbnailViewer.Refresh();
            }
        }

        private string GetExportDocumentName()
        {
            StringBuilder stringBuilder = new StringBuilder();
            DateTime now = DateTime.Now;
            stringBuilder.Append(now.Month.ToString().PadLeft(2, '0') + (object)'-');
            stringBuilder.Append(now.Day.ToString().PadLeft(2, '0') + (object)'-');
            stringBuilder.Append(now.Year.ToString().PadLeft(2, '0') + (object)'-');
            stringBuilder.Append(now.Hour.ToString().PadLeft(2, '0') + (object)'-');
            stringBuilder.Append(now.Minute.ToString().PadLeft(2, '0') + (object)'-');
            stringBuilder.Append(now.Second.ToString().PadLeft(2, '0') + (object)'.');
            stringBuilder.Append(Environment.UserName + (object)'.');
            stringBuilder.Append(Environment.MachineName);
            return stringBuilder.ToString();
        }

        private void mergeAndTransferWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ++numBGW;
            mergeTransferStatusStatusStripLabel.Visible = true;
            mergeTransferStatusStatusStripLabel.Text = "Number of Batches Merging/Transferring: " + (object)numBGW;
            Tuple<string[], string, string, string, string, int> tuple = (Tuple<string[], string, string, string, string, int>)e.Argument;
            Transfer(Merge(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item5, tuple.Item6), tuple.Item4, tuple.Item6);
            log.Info("Exported " + tuple.Item5 + (tuple.Item2 == "TIF" ? (object)".tif" : (object)".pdf") + " to " + tuple.Item4 + "\\, " + (object)tuple.Item1.Length + " pages.");
        }

        private string Merge(
          string[] filesToMerge,
          string mergeType,
          string batchFolder,
          string mergeFileName,
          int batchNum)
        {
            string mergedFileDestination = batchFolder + "\\" + mergeFileName;
            if (mergeType == "PDF")
            {
                mergedFileDestination += ".pdf";
                MergeToPDF(filesToMerge, mergedFileDestination, batchNum);
            }
            else if (mergeType == "TIF")
            {
                mergedFileDestination += ".tif";
                MergeToTIF(filesToMerge, mergedFileDestination, batchNum);
            }
            log.Info("Successfully merged Batch " + (object)batchNum + " to File: " + mergeFileName);
            return mergedFileDestination;
        }

        private void MergeToPDF(string[] filesToMerge, string mergedFileDestination, int batchNum)
        {
            log.Info("Merging images in Batch " + (object)batchNum + " into a PDF file...");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfEncoder pdfEncoder = new PdfEncoder();
                PdfImageCollection images = new PdfImageCollection();
                foreach (string fileName in filesToMerge)
                images.Add(new PdfImage(fileName, 0, PdfCompressionType.CcittGroup4));
                pdfEncoder.Save((Stream)memoryStream, images, (ProgressEventHandler)null);
                memoryStream.Position = 0L;
                //PdfDocument pdfDocument = new PdfDocument((string)null, (string)null, (Stream)memoryStream, (PdfDocumentLoadedProgress)null, new RepairOptions());
                PdfDocument pdfDocument = new PdfDocument((Stream)memoryStream);
                pdfDocument.Save(mergedFileDestination);
                pdfDocument.Close();
            }
        }

        private void MergeToTIF(string[] filesToMerge, string mergedFileDestination, int batchNum)
        {
            if (filesToMerge.Length == 1)
            {
                log.Info("Copying single image in Batch " + (object)batchNum + " to export file...");
                File.Copy(filesToMerge[0], mergedFileDestination);
            }
            else
            {
                log.Info("Merging images in Batch " + (object)batchNum + " into a multipage TIF file...");
                TiffDocument.Combine(mergedFileDestination, filesToMerge);
            }
        }

        private bool Transfer(string mergedFilePath, string targetDestination, int batchNum)
        {
            log.Info("Begin transferring Batch " + (object)batchNum + " to Destination: " + targetDestination + "...");
            try
            {
                int num1 = 0;
                FileStream fileStream1 = new FileStream(mergedFilePath, FileMode.Open, FileAccess.Read);
                FileStream fileStream2 = new FileStream(targetDestination + mergedFilePath.Substring(mergedFilePath.LastIndexOf("\\")), FileMode.CreateNew);
                BinaryReader binaryReader = new BinaryReader((Stream)fileStream1);
                BinaryWriter binaryWriter = new BinaryWriter((Stream)fileStream2);
                long num2 = fileStream1.Length / 100L;
                byte[] numArray = new byte[1024];
                binaryReader.BaseStream.Position = 0L;
                do
                {
                    byte[] buffer = binaryReader.ReadBytes(numArray.Length);
                    binaryWriter.Write(buffer);
                    num1 += buffer.Length;
                    long num3 = (long)num1 / binaryReader.BaseStream.Length;
                }
                while ((long)num1 < binaryReader.BaseStream.Length);
                binaryReader.Close();
                binaryWriter.Close();
                fileStream1.Close();
                fileStream2.Close();
            }
            catch (Exception ex)
            {
                log.Info("Failed to transfer Batch " + (object)batchCount);
                return false;
            }
            log.Info("Successfully transferred Batch " + (object)batchNum + " to Destination: " + targetDestination);
            return true;
        }

        private void mergeAndTransferWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void mergeAndTransferWorker_RunWorkerCompleted(
          object sender,
          RunWorkerCompletedEventArgs e)
        {
            --numBGW;
            if (numBGW == 0)
                mergeTransferStatusStatusStripLabel.Text = "Merge and Transfer Complete.";
            else
                mergeTransferStatusStatusStripLabel.Text = "Number of Merges and Transfers: " + (object)numBGW;
        }

        private void ExportLog(string entry)
        {
            DateTime now = DateTime.Now;
            StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Bulk Scan Export Log " + now.Month.ToString().PadLeft(2, '0') + "-" + now.Day.ToString().PadLeft(2, '0') + "-" + now.Year.ToString() + ".log", true);
            streamWriter.WriteLine(entry);
            streamWriter.Close();
            streamWriter.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (numBGW > 0)
            {
                int num = (int)MessageBox.Show("A batch is being merged and transferred. Application exit cancelled.", "Exit Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                e.Cancel = true;
            }
            else if (thumbnailViewer.Items.Count > 0 && MessageBox.Show("The batch still has scans that haven't been merged and transferred yet. Would you like to exit application?", "Batch Contains Unmerged Scans", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                log.Info("Shutting down application...");
                System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
                if (settings["ScannerLastUsed"] == null)
                    settings.Add("ScannerLastUsed", "No Scanner Detected");
                if (scannerComboBox.Text != "No Scanner Detected")
                    settings["ScannerLastUsed"].Value = scannerComboBox.Text;
                if (settings["DestinationLastUsed"] == null)
                    settings.Add("DestinationLastUsed", "Scan to Desktop");
                if (destinationsComboBox.SelectedItem != null)
                    settings["DestinationLastUsed"].Value = destinationsComboBox.SelectedItem.ToString();
                configuration.Save(ConfigurationSaveMode.Modified);
                scanner.KillTwain();
                scanner = (ScanProcess)null;
                if (Directory.Exists(tempFolderPath))
                    Directory.Delete(tempFolderPath, true);
                log.Info("Session log end.");
            }
        }

        private void thumbnailViewer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        if (!deleteBatchButton.Enabled)
                            break;
                        deleteBatchButton.PerformClick();
                        break;
                    }
                    if (!deletePageButton.Enabled)
                        break;
                    deletePageButton.PerformClick();
                    break;
                case Keys.A:
                    if (Control.ModifierKeys != Keys.Control || thumbnailViewer.Items.Count <= 0)
                        break;
                    thumbnailViewer.SelectAll();
                    break;
                case Keys.H:
                    hideShowInterfaceButton.PerformClick();
                    break;
                case Keys.R:
                    if (Control.ModifierKeys != Keys.Alt || !rescanButton.Enabled)
                        break;
                    rescanButton.PerformClick();
                    break;
                case Keys.S:
                    if (Control.ModifierKeys != Keys.Alt)
                        break;
                    scanButton.PerformClick();
                    break;
                case Keys.T:
                    if (Control.ModifierKeys != Keys.Alt || !mergeAndTransferButton.Enabled)
                        break;
                    mergeAndTransferButton.PerformClick();
                    break;
                case Keys.OemOpenBrackets:
                    if (!rotateCounterClockButton.Enabled)
                        break;
                    rotateCounterClockButton.PerformClick();
                    break;
                case Keys.OemCloseBrackets:
                    if (!rotateClockwiseButton.Enabled)
                        break;
                    rotateClockwiseButton.PerformClick();
                    break;
            }
        }

        private void scannerComboBox_KeyDown(object sender, KeyEventArgs e) => e.SuppressKeyPress = true;

        private void destinationsComboBox_KeyDown(object sender, KeyEventArgs e) => e.SuppressKeyPress = true;

        private void Form1_Load(object sender, EventArgs e)
                    {
            string dateInString = "02.10.2022";

            DateTime startDate = DateTime.Parse(dateInString);
            DateTime expiryDate = startDate.AddDays(30);
            if (DateTime.Now > expiryDate)
            {
                MessageBox.Show("Trail Period Expired.");
                Close();
            }

            try
            {
                Icon = Resources.DocuScanIcon;

                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                Text = string.Format("DocuScan Version {0} - Trail Expiration: {1}", version,expiryDate.ToString("MM.dd.yy"));
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            if (new optionsDialog().ShowDialog() != DialogResult.OK)
                return;
            ConfigurationManager.RefreshSection("appSettings");
            int num = int.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            thumbnailViewer.ThumbnailSize = new Size(num, num);
            thumbnailViewer.Refresh();
            GetAliases();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            int num = (int)new AboutDocuScan().ShowDialog();
        }

        private void scannerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            scanner.SetDevice((sender as ToolStripComboBox).SelectedItem as string);
            scanButton.Enabled = true;
        }

        private void scannerComboBox_Click(object sender, EventArgs e)
        {
        //     scannerComboBox.Items.Clear();
        //     scannerComboBox.Items.AddRange((object[])scanner.DeviceList);
        //     if (scannerComboBox.Items.Count != 0)
        //         return;
        //     scannerComboBox.Text = "No Scanner Detected";
        }
    }
}
