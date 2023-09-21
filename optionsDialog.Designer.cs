
namespace DocuScan
{
    partial class optionsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.destinationFiletypeTab = new System.Windows.Forms.TabPage();
            this.destinationFiletypeLabel = new System.Windows.Forms.Label();
            this.tifRadioButton = new System.Windows.Forms.RadioButton();
            this.pdfRadioButton = new System.Windows.Forms.RadioButton();
            this.thumbnailSizeTab = new System.Windows.Forms.TabPage();
            this.thumbnailView1 = new Atalasoft.Imaging.WinControls.ThumbnailView();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.destinationsTab = new System.Windows.Forms.TabPage();
            this.defaultDestinationTextBox = new System.Windows.Forms.TextBox();
            this.deleteDestinationButton = new System.Windows.Forms.Button();
            this.addNewDestinationButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AliasColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.browseColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.destinationFiletypeTab.SuspendLayout();
            this.thumbnailSizeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.destinationsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.destinationFiletypeTab);
            this.tabControl1.Controls.Add(this.thumbnailSizeTab);
            this.tabControl1.Controls.Add(this.destinationsTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(383, 344);
            this.tabControl1.TabIndex = 0;
            // 
            // destinationFiletypeTab
            // 
            this.destinationFiletypeTab.Controls.Add(this.destinationFiletypeLabel);
            this.destinationFiletypeTab.Controls.Add(this.tifRadioButton);
            this.destinationFiletypeTab.Controls.Add(this.pdfRadioButton);
            this.destinationFiletypeTab.Location = new System.Drawing.Point(4, 22);
            this.destinationFiletypeTab.Name = "destinationFiletypeTab";
            this.destinationFiletypeTab.Padding = new System.Windows.Forms.Padding(3);
            this.destinationFiletypeTab.Size = new System.Drawing.Size(375, 318);
            this.destinationFiletypeTab.TabIndex = 0;
            this.destinationFiletypeTab.Text = "Destination Filetype";
            this.destinationFiletypeTab.UseVisualStyleBackColor = true;
            // 
            // destinationFiletypeLabel
            // 
            this.destinationFiletypeLabel.AutoSize = true;
            this.destinationFiletypeLabel.Location = new System.Drawing.Point(22, 26);
            this.destinationFiletypeLabel.MaximumSize = new System.Drawing.Size(333, 0);
            this.destinationFiletypeLabel.Name = "destinationFiletypeLabel";
            this.destinationFiletypeLabel.Size = new System.Drawing.Size(325, 26);
            this.destinationFiletypeLabel.TabIndex = 2;
            this.destinationFiletypeLabel.Text = "Please select the filetype of the finished, exported document as it is stored in " +
    "the desired destination:";
            // 
            // tifRadioButton
            // 
            this.tifRadioButton.AutoSize = true;
            this.tifRadioButton.Location = new System.Drawing.Point(213, 128);
            this.tifRadioButton.Name = "tifRadioButton";
            this.tifRadioButton.Size = new System.Drawing.Size(41, 17);
            this.tifRadioButton.TabIndex = 1;
            this.tifRadioButton.TabStop = true;
            this.tifRadioButton.Text = "TIF";
            this.tifRadioButton.UseVisualStyleBackColor = true;
            // 
            // pdfRadioButton
            // 
            this.pdfRadioButton.AutoSize = true;
            this.pdfRadioButton.Location = new System.Drawing.Point(76, 128);
            this.pdfRadioButton.Name = "pdfRadioButton";
            this.pdfRadioButton.Size = new System.Drawing.Size(46, 17);
            this.pdfRadioButton.TabIndex = 0;
            this.pdfRadioButton.TabStop = true;
            this.pdfRadioButton.Text = "PDF";
            this.pdfRadioButton.UseVisualStyleBackColor = true;
            // 
            // thumbnailSizeTab
            // 
            this.thumbnailSizeTab.Controls.Add(this.thumbnailView1);
            this.thumbnailSizeTab.Controls.Add(this.trackBar1);
            this.thumbnailSizeTab.Location = new System.Drawing.Point(4, 22);
            this.thumbnailSizeTab.Name = "thumbnailSizeTab";
            this.thumbnailSizeTab.Padding = new System.Windows.Forms.Padding(3);
            this.thumbnailSizeTab.Size = new System.Drawing.Size(375, 318);
            this.thumbnailSizeTab.TabIndex = 1;
            this.thumbnailSizeTab.Text = "Thumbnail Size";
            this.thumbnailSizeTab.UseVisualStyleBackColor = true;
            // 
            // thumbnailView1
            // 
            this.thumbnailView1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.thumbnailView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.thumbnailView1.DragSelectionColor = System.Drawing.Color.Red;
            this.thumbnailView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.thumbnailView1.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.thumbnailView1.HighlightTextColor = System.Drawing.SystemColors.HighlightText;
            this.thumbnailView1.LoadErrorMessage = "";
            this.thumbnailView1.Location = new System.Drawing.Point(6, 6);
            this.thumbnailView1.Margins = new Atalasoft.Imaging.WinControls.Margin(4, 4, 4, 4);
            this.thumbnailView1.MultiSelect = false;
            this.thumbnailView1.Name = "thumbnailView1";
            this.thumbnailView1.SelectionRectangleBackColor = System.Drawing.Color.Transparent;
            this.thumbnailView1.SelectionRectangleDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.thumbnailView1.SelectionRectangleLineColor = System.Drawing.Color.Black;
            this.thumbnailView1.Size = new System.Drawing.Size(363, 255);
            this.thumbnailView1.TabIndex = 1;
            this.thumbnailView1.ThumbnailBackground = null;
            this.thumbnailView1.ThumbnailOffset = new System.Drawing.Point(0, 0);
            this.thumbnailView1.ThumbnailSize = new System.Drawing.Size(110, 110);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 267);
            this.trackBar1.Maximum = 220;
            this.trackBar1.Minimum = 50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(363, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickFrequency = 5;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // destinationsTab
            // 
            this.destinationsTab.Controls.Add(this.defaultDestinationTextBox);
            this.destinationsTab.Controls.Add(this.deleteDestinationButton);
            this.destinationsTab.Controls.Add(this.addNewDestinationButton);
            this.destinationsTab.Controls.Add(this.dataGridView1);
            this.destinationsTab.Location = new System.Drawing.Point(4, 22);
            this.destinationsTab.Name = "destinationsTab";
            this.destinationsTab.Size = new System.Drawing.Size(375, 318);
            this.destinationsTab.TabIndex = 2;
            this.destinationsTab.Text = "Destinations";
            this.destinationsTab.UseVisualStyleBackColor = true;
            // 
            // defaultDestinationTextBox
            // 
            this.defaultDestinationTextBox.Location = new System.Drawing.Point(63, 290);
            this.defaultDestinationTextBox.Name = "defaultDestinationTextBox";
            this.defaultDestinationTextBox.Size = new System.Drawing.Size(129, 20);
            this.defaultDestinationTextBox.TabIndex = 3;
            // 
            // deleteDestinationButton
            // 
            this.deleteDestinationButton.Location = new System.Drawing.Point(34, 290);
            this.deleteDestinationButton.Name = "deleteDestinationButton";
            this.deleteDestinationButton.Size = new System.Drawing.Size(23, 23);
            this.deleteDestinationButton.TabIndex = 2;
            this.deleteDestinationButton.Text = "-";
            this.deleteDestinationButton.UseVisualStyleBackColor = true;
            this.deleteDestinationButton.Click += new System.EventHandler(this.deleteDestinationButton_Click);
            // 
            // addNewDestinationButton
            // 
            this.addNewDestinationButton.Location = new System.Drawing.Point(5, 290);
            this.addNewDestinationButton.Name = "addNewDestinationButton";
            this.addNewDestinationButton.Size = new System.Drawing.Size(23, 23);
            this.addNewDestinationButton.TabIndex = 1;
            this.addNewDestinationButton.Text = "+";
            this.addNewDestinationButton.UseVisualStyleBackColor = true;
            this.addNewDestinationButton.Click += new System.EventHandler(this.addNewDestinationButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AliasColumn,
            this.TargetPathColumn,
            this.browseColumn});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(369, 281);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // AliasColumn
            // 
            this.AliasColumn.HeaderText = "Alias";
            this.AliasColumn.Name = "AliasColumn";
            // 
            // TargetPathColumn
            // 
            this.TargetPathColumn.HeaderText = "Target Path";
            this.TargetPathColumn.Name = "TargetPathColumn";
            this.TargetPathColumn.Width = 200;
            // 
            // browseColumn
            // 
            this.browseColumn.HeaderText = "";
            this.browseColumn.Name = "browseColumn";
            this.browseColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.browseColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.browseColumn.Text = "...";
            this.browseColumn.Width = 25;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(320, 362);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(239, 362);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // optionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 398);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "optionsDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.optionsDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.destinationFiletypeTab.ResumeLayout(false);
            this.destinationFiletypeTab.PerformLayout();
            this.thumbnailSizeTab.ResumeLayout(false);
            this.thumbnailSizeTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.destinationsTab.ResumeLayout(false);
            this.destinationsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage destinationFiletypeTab;
        private System.Windows.Forms.Label destinationFiletypeLabel;
        private System.Windows.Forms.RadioButton tifRadioButton;
        private System.Windows.Forms.RadioButton pdfRadioButton;
        private System.Windows.Forms.TabPage thumbnailSizeTab;
        private Atalasoft.Imaging.WinControls.ThumbnailView thumbnailView1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TabPage destinationsTab;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button deleteDestinationButton;
        private System.Windows.Forms.Button addNewDestinationButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AliasColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetPathColumn;
        private System.Windows.Forms.DataGridViewButtonColumn browseColumn;
        private System.Windows.Forms.TextBox defaultDestinationTextBox;
    }
}