using Atalasoft.Imaging;
using Atalasoft.Imaging.WinControls;
using DocuScan.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DocuScan
{
     public partial class optionsDialog : Form
    {

        private List<Destination> destinationList;
       

        public optionsDialog() => this.InitializeComponent();

        private void optionsDialog_Load(object sender, EventArgs e)
        {
            ConfigurationManager.RefreshSection("appSettings");
            switch (ConfigurationManager.AppSettings["ExportFiletype"])
            {
                case "TIF":
                    this.pdfRadioButton.Checked = false;
                    this.tifRadioButton.Checked = true;
                    break;
                default:
                    this.pdfRadioButton.Checked = true;
                    this.tifRadioButton.Checked = false;
                    break;
            }
            this.AddImage(AtalaImage.FromBitmap(Resources.SampleDocument));
            if (this.trackBar1.Value == Convert.ToInt32(ConfigurationManager.AppSettings["ThumbnailSize"]))
                this.SetThumbnailSize(this.trackBar1.Value);
            else
                this.trackBar1.Value = Convert.ToInt32(ConfigurationManager.AppSettings["ThumbnailSize"]);
            this.destinationList = this.GetDestinationList();
            this.dataGridView1.Rows.Clear();
            foreach (Destination destination in this.destinationList)
                this.dataGridView1.Rows.Add((object)destination.Alias, (object)destination.TargetPath);
            this.defaultDestinationTextBox.Text = ConfigurationManager.AppSettings["DefaultDestinationPath"];
        
    }

        private void AddImage(AtalaImage image)
        {
            if (this.thumbnailView1.InvokeRequired)
                this.Invoke((Delegate)new optionsDialog.AddImageCallback(this.AddImage), (object)image);
            else
                this.thumbnailView1.Items.Add(image, "Size: ");
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        { 
            this.SetThumbnailSize(this.trackBar1.Value); 
        } 
          

        private void SetThumbnailSize(int size)
        {
            this.thumbnailView1.ThumbnailSize = new Size(size, size);
            this.thumbnailView1.Margins = new Margin((this.thumbnailView1.Size.Width - size) / 2, (this.thumbnailView1.Size.Height - size) / 2, 0, 0);
            this.thumbnailView1.Items[0].Text = "Size: " + size.ToString();
        }

        private List<Destination> GetDestinationList()
        {
            List<Destination> destinationList = new List<Destination>();
            string appSetting = ConfigurationManager.AppSettings["Destinations"];
            char[] chArray = new char[1] { ';' };
            foreach (string str in appSetting.Split(chArray))
            {
                if (str.Length > 0)
                {
                    string[] strArray = str.Split('|');
                    Destination destination = new Destination(strArray[0], strArray[1]);
                    destinationList.Add(destination);
                }
            }
            return destinationList;
        }

        private void addNewDestinationButton_Click(object sender, EventArgs e) => this.dataGridView1.Rows.Add();

        private void deleteDestinationButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0 || this.dataGridView1.SelectedRows[0].Index == this.dataGridView1.Rows.Count - 1)
                return;
            this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewButtonColumn) || e.RowIndex < 0)
                return;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            int num = (int)folderBrowserDialog.ShowDialog();
            if (string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                return;
            this.dataGridView1[1, e.RowIndex].Value = (object)folderBrowserDialog.SelectedPath;
        }

        private void defaultBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            int num = (int)folderBrowserDialog.ShowDialog();
            if (string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                return;
            this.defaultDestinationTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
            this.DialogResult = DialogResult.OK;
            settings["ExportFiletype"].Value = this.pdfRadioButton.Checked ? "PDF" : "TIF";
            settings["ThumbnailSize"].Value = this.trackBar1.Value.ToString();
            string str = "";
            for (int index = 0; index < this.dataGridView1.Rows.Count - 1; ++index)
            {
                if (this.dataGridView1.Rows[index].Cells[0].Value != null && this.dataGridView1.Rows[index].Cells[1].Value != null)
                {
                    if (index > 0)
                        str += ";";
                    str = str + this.dataGridView1.Rows[index].Cells[0].Value.ToString() + "|" + this.dataGridView1.Rows[index].Cells[1].Value.ToString();
                }
            }
            settings["Destinations"].Value = str;
            configuration.Save(ConfigurationSaveMode.Modified);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private delegate void AddImageCallback(AtalaImage image);


    }
}
