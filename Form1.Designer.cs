
namespace DocuScan
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.thumbnailViewer = new Atalasoft.Imaging.WinControls.ThumbnailView();
            this.workspaceViewer = new Atalasoft.Imaging.WinControls.WorkspaceViewer();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.scannerLabel = new System.Windows.Forms.ToolStripLabel();
            this.scannerComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.scanButton = new System.Windows.Forms.ToolStripButton();
            this.hideShowInterfaceButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.insertBeforeButton = new System.Windows.Forms.ToolStripButton();
            this.insertBehindButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rescanButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rotateClockwiseButton = new System.Windows.Forms.ToolStripButton();
            this.rotateCounterClockButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.deletePageButton = new System.Windows.Forms.ToolStripButton();
            this.deleteBatchButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToLabel = new System.Windows.Forms.ToolStripLabel();
            this.destinationsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.mergeAndTransferButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsButton = new System.Windows.Forms.ToolStripButton();
            this.aboutButton = new System.Windows.Forms.ToolStripButton();
            this.pageCountStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.commandDetailStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mergeTransferStatusStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 40);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.thumbnailViewer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.workspaceViewer);
            this.splitContainer.Size = new System.Drawing.Size(974, 388);
            this.splitContainer.SplitterDistance = 471;
            this.splitContainer.TabIndex = 7;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // thumbnailViewer
            // 
            this.thumbnailViewer.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.thumbnailViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thumbnailViewer.DragSelectionColor = System.Drawing.Color.Red;
            this.thumbnailViewer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.thumbnailViewer.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.thumbnailViewer.HighlightTextColor = System.Drawing.SystemColors.HighlightText;
            this.thumbnailViewer.LoadErrorMessage = "";
            this.thumbnailViewer.Location = new System.Drawing.Point(0, 0);
            this.thumbnailViewer.Margins = new Atalasoft.Imaging.WinControls.Margin(4, 4, 4, 4);
            this.thumbnailViewer.Name = "thumbnailViewer";
            this.thumbnailViewer.SelectionMode = Atalasoft.Imaging.WinControls.ThumbnailSelectionMode.MultiSelect;
            this.thumbnailViewer.SelectionRectangleBackColor = System.Drawing.Color.Transparent;
            this.thumbnailViewer.SelectionRectangleDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.thumbnailViewer.SelectionRectangleLineColor = System.Drawing.Color.Black;
            this.thumbnailViewer.Size = new System.Drawing.Size(471, 388);
            this.thumbnailViewer.TabIndex = 0;
            this.thumbnailViewer.Text = "thumbnailView1";
            this.thumbnailViewer.ThumbnailBackground = null;
            this.thumbnailViewer.ThumbnailOffset = new System.Drawing.Point(0, 0);
            this.thumbnailViewer.ThumbnailSize = new System.Drawing.Size(100, 100);
            this.thumbnailViewer.SelectedIndexChanged += new System.EventHandler(this.thumbnailViewer_SelectedIndexChanged);
            this.thumbnailViewer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.thumbnailViewer_KeyDown);
            // 
            // workspaceViewer
            // 
            this.workspaceViewer.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ScaleToGray;
            this.workspaceViewer.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.FitToHeight;
            this.workspaceViewer.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.workspaceViewer.DisplayProfile = null;
            this.workspaceViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspaceViewer.Location = new System.Drawing.Point(0, 0);
            this.workspaceViewer.Magnifier.BackColor = System.Drawing.Color.White;
            this.workspaceViewer.Magnifier.BorderColor = System.Drawing.Color.Black;
            this.workspaceViewer.Magnifier.Size = new System.Drawing.Size(100, 100);
            this.workspaceViewer.Name = "workspaceViewer";
            this.workspaceViewer.OutputProfile = null;
            this.workspaceViewer.Selection = null;
            this.workspaceViewer.Size = new System.Drawing.Size(499, 388);
            this.workspaceViewer.TabIndex = 0;
            // 
            // ToolStrip
            // 
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scannerLabel,
            this.scannerComboBox,
            this.scanButton,
            this.hideShowInterfaceButton,
            this.ToolStripSeparator1,
            this.insertBeforeButton,
            this.insertBehindButton,
            this.ToolStripSeparator2,
            this.rescanButton,
            this.ToolStripSeparator3,
            this.rotateClockwiseButton,
            this.rotateCounterClockButton,
            this.ToolStripSeparator4,
            this.deletePageButton,
            this.deleteBatchButton,
            this.ToolStripSeparator5,
            this.saveToLabel,
            this.destinationsComboBox,
            this.mergeAndTransferButton,
            this.ToolStripSeparator6,
            this.optionsButton,
            this.aboutButton});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(974, 40);
            this.ToolStrip.TabIndex = 5;
            this.ToolStrip.Text = "ToolStrip";
            // 
            // scannerLabel
            // 
            this.scannerLabel.Name = "scannerLabel";
            this.scannerLabel.Size = new System.Drawing.Size(52, 37);
            this.scannerLabel.Text = "Scanner:";
            // 
            // scannerComboBox
            // 
            this.scannerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scannerComboBox.DropDownWidth = 200;
            this.scannerComboBox.Name = "scannerComboBox";
            this.scannerComboBox.Size = new System.Drawing.Size(200, 40);
            this.scannerComboBox.Click += new System.EventHandler(this.scannerComboBox_Click);
            this.scannerComboBox.SelectedIndexChanged += new System.EventHandler(scannerComboBox_SelectedIndexChanged);
            // 
            // scanButton
            // 
            this.scanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.scanButton.Image = global::DocuScan.Properties.Resources.Scanner1_32x32;
            this.scanButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.scanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(37, 37);
            this.scanButton.Text = "ToolStripButton1";
            this.scanButton.ToolTipText = "Scan (Alt + S)";
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            this.scanButton.MouseEnter += new System.EventHandler(this.scanButton_MouseEnter);
            this.scanButton.MouseLeave += new System.EventHandler(this.scanButton_MouseLeave);
            // 
            // hideShowInterfaceButton
            // 
            this.hideShowInterfaceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideShowInterfaceButton.Image = global::DocuScan.Properties.Resources.NoInterface_Off_32x32;
            this.hideShowInterfaceButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.hideShowInterfaceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideShowInterfaceButton.Name = "hideShowInterfaceButton";
            this.hideShowInterfaceButton.Size = new System.Drawing.Size(37, 37);
            this.hideShowInterfaceButton.Text = "hideShowInterfaceButton";
            this.hideShowInterfaceButton.ToolTipText = "Hide Scanner interface ( H )";
            this.hideShowInterfaceButton.Click += new System.EventHandler(this.hideShowInterfaceButton_Click);
            this.hideShowInterfaceButton.MouseEnter += new System.EventHandler(this.hideShowInterfaceButton_MouseEnter);
            this.hideShowInterfaceButton.MouseLeave += new System.EventHandler(this.hideShowInterfaceButton_MouseLeave);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // insertBeforeButton
            // 
            this.insertBeforeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.insertBeforeButton.Image = global::DocuScan.Properties.Resources.NewDocumentInFront_32x32;
            this.insertBeforeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.insertBeforeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertBeforeButton.Name = "insertBeforeButton";
            this.insertBeforeButton.Size = new System.Drawing.Size(36, 37);
            this.insertBeforeButton.Text = "Insert Before";
            this.insertBeforeButton.Click += new System.EventHandler(this.insertBeforeButton_Click);
            this.insertBeforeButton.MouseEnter += new System.EventHandler(this.insertBeforeButton_MouseEnter);
            this.insertBeforeButton.MouseLeave += new System.EventHandler(this.insertBeforeButton_MouseLeave);
            // 
            // insertBehindButton
            // 
            this.insertBehindButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.insertBehindButton.Image = global::DocuScan.Properties.Resources.NewDocumentBehind_32x32;
            this.insertBehindButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.insertBehindButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertBehindButton.Name = "insertBehindButton";
            this.insertBehindButton.Size = new System.Drawing.Size(36, 37);
            this.insertBehindButton.Text = "Insert Behind";
            this.insertBehindButton.Click += new System.EventHandler(this.insertBehindButton_Click);
            this.insertBehindButton.MouseEnter += new System.EventHandler(this.insertBehindButton_MouseEnter);
            this.insertBehindButton.MouseLeave += new System.EventHandler(this.insertBehindButton_MouseLeave);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // rescanButton
            // 
            this.rescanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rescanButton.Image = global::DocuScan.Properties.Resources.rescan_32x32;
            this.rescanButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rescanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rescanButton.Name = "rescanButton";
            this.rescanButton.Size = new System.Drawing.Size(36, 37);
            this.rescanButton.Text = "Rescan";
            this.rescanButton.ToolTipText = "Rescan ( Alt + R )";
            this.rescanButton.Click += new System.EventHandler(this.rescanButton_Click);
            this.rescanButton.MouseEnter += new System.EventHandler(this.rescanButton_MouseEnter);
            this.rescanButton.MouseLeave += new System.EventHandler(this.rescanButton_MouseLeave);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // rotateClockwiseButton
            // 
            this.rotateClockwiseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateClockwiseButton.Image = global::DocuScan.Properties.Resources.RotateClockwise_32x32;
            this.rotateClockwiseButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotateClockwiseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateClockwiseButton.Name = "rotateClockwiseButton";
            this.rotateClockwiseButton.Size = new System.Drawing.Size(36, 37);
            this.rotateClockwiseButton.Text = "Rotate Clockwise";
            this.rotateClockwiseButton.ToolTipText = "Rotate Clockwise ( → )";
            this.rotateClockwiseButton.Click += new System.EventHandler(this.rotateClockwiseButton_Click);
            this.rotateClockwiseButton.MouseEnter += new System.EventHandler(this.rotateClockwiseButton_MouseEnter);
            this.rotateClockwiseButton.MouseLeave += new System.EventHandler(this.rotateClockwiseButton_MouseLeave);
            // 
            // rotateCounterClockButton
            // 
            this.rotateCounterClockButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateCounterClockButton.Image = global::DocuScan.Properties.Resources.RotateCounter_32x32;
            this.rotateCounterClockButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotateCounterClockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateCounterClockButton.Name = "rotateCounterClockButton";
            this.rotateCounterClockButton.Size = new System.Drawing.Size(36, 37);
            this.rotateCounterClockButton.Text = "Rotate Counter Clock";
            this.rotateCounterClockButton.ToolTipText = "Rotate Counter Clockwise ( ← )";
            this.rotateCounterClockButton.Click += new System.EventHandler(this.rotateCounterClockButton_Click);
            this.rotateCounterClockButton.MouseEnter += new System.EventHandler(this.rotateCounterClockButton_MouseEnter);
            this.rotateCounterClockButton.MouseLeave += new System.EventHandler(this.rotateCounterClockButton_MouseLeave);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // deletePageButton
            // 
            this.deletePageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deletePageButton.Image = global::DocuScan.Properties.Resources.DocumentDelete_32x32;
            this.deletePageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deletePageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deletePageButton.Name = "deletePageButton";
            this.deletePageButton.Size = new System.Drawing.Size(36, 37);
            this.deletePageButton.Text = "Delete Page";
            this.deletePageButton.ToolTipText = "Delete Page ( Del )";
            this.deletePageButton.Click += new System.EventHandler(this.deletePageButton_Click);
            this.deletePageButton.MouseEnter += new System.EventHandler(this.deletePageButton_MouseEnter);
            this.deletePageButton.MouseLeave += new System.EventHandler(this.deletePageButton_MouseLeave);
            // 
            // deleteBatchButton
            // 
            this.deleteBatchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteBatchButton.Image = global::DocuScan.Properties.Resources.BatchDelete_32x32;
            this.deleteBatchButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteBatchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteBatchButton.Name = "deleteBatchButton";
            this.deleteBatchButton.Size = new System.Drawing.Size(36, 37);
            this.deleteBatchButton.Text = "Delete Batch";
            this.deleteBatchButton.ToolTipText = "Delete Batch ( Alt + Del )";
            this.deleteBatchButton.Click += new System.EventHandler(this.deleteBatchButton_Click);
            this.deleteBatchButton.MouseEnter += new System.EventHandler(this.deleteBatchButton_MouseEnter);
            this.deleteBatchButton.MouseLeave += new System.EventHandler(this.deleteBatchButton_MouseLeave);
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 40);
            // 
            // saveToLabel
            // 
            this.saveToLabel.Name = "saveToLabel";
            this.saveToLabel.Size = new System.Drawing.Size(49, 37);
            this.saveToLabel.Text = "Save To:";
            // 
            // destinationsComboBox
            // 
            this.destinationsComboBox.Name = "destinationsComboBox";
            this.destinationsComboBox.Size = new System.Drawing.Size(121, 40);
            // 
            // mergeAndTransferButton
            // 
            this.mergeAndTransferButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mergeAndTransferButton.Image = global::DocuScan.Properties.Resources.MergeAndXfer_32x32;
            this.mergeAndTransferButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mergeAndTransferButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mergeAndTransferButton.Name = "mergeAndTransferButton";
            this.mergeAndTransferButton.Size = new System.Drawing.Size(36, 37);
            this.mergeAndTransferButton.Text = "Merge & Transfer";
            this.mergeAndTransferButton.ToolTipText = "Merge &  Transfer";
            this.mergeAndTransferButton.Click += new System.EventHandler(this.mergeAndTransferButton_Click);
            this.mergeAndTransferButton.MouseEnter += new System.EventHandler(this.mergeAndTransferButton_MouseEnter);
            this.mergeAndTransferButton.MouseLeave += new System.EventHandler(this.mergeAndTransferButton_MouseLeave);
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 40);
            // 
            // optionsButton
            // 
            this.optionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.optionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(53, 37);
            this.optionsButton.Text = "Options";
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            this.optionsButton.MouseEnter += new System.EventHandler(this.optionsButton_MouseEnter);
            this.optionsButton.MouseLeave += new System.EventHandler(this.optionsButton_MouseLeave);
            // 
            // aboutButton
            // 
            this.aboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutButton.Image")));
            this.aboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(44, 37);
            this.aboutButton.Text = "About";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            this.aboutButton.MouseEnter += new System.EventHandler(this.aboutButton_MouseEnter);
            this.aboutButton.MouseLeave += new System.EventHandler(this.aboutButton_MouseLeave);
            // 
            // pageCountStatusStripLabel
            // 
            this.pageCountStatusStripLabel.Name = "pageCountStatusStripLabel";
            this.pageCountStatusStripLabel.Size = new System.Drawing.Size(72, 17);
            this.pageCountStatusStripLabel.Text = "Page Count:";
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageCountStatusStripLabel,
            this.commandDetailStatusStripLabel,
            this.mergeTransferStatusStatusStripLabel});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 428);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(974, 22);
            this.StatusStrip1.TabIndex = 6;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // commandDetailStatusStripLabel
            // 
            this.commandDetailStatusStripLabel.Name = "commandDetailStatusStripLabel";
            this.commandDetailStatusStripLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // mergeTransferStatusStatusStripLabel
            // 
            this.mergeTransferStatusStatusStripLabel.Name = "mergeTransferStatusStatusStripLabel";
            this.mergeTransferStatusStatusStripLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 450);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.StatusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.SplitContainer splitContainer;
        internal System.Windows.Forms.ToolStrip ToolStrip;
        internal System.Windows.Forms.ToolStripLabel scannerLabel;
        internal System.Windows.Forms.ToolStripComboBox scannerComboBox;
        internal System.Windows.Forms.ToolStripButton scanButton;
        internal System.Windows.Forms.ToolStripButton hideShowInterfaceButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton insertBeforeButton;
        internal System.Windows.Forms.ToolStripButton insertBehindButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton rescanButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton rotateClockwiseButton;
        internal System.Windows.Forms.ToolStripButton rotateCounterClockButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton deletePageButton;
        internal System.Windows.Forms.ToolStripButton deleteBatchButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripLabel saveToLabel;
        internal System.Windows.Forms.ToolStripComboBox destinationsComboBox;
        internal System.Windows.Forms.ToolStripButton mergeAndTransferButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        internal System.Windows.Forms.ToolStripButton optionsButton;
        internal System.Windows.Forms.ToolStripButton aboutButton;
        internal System.Windows.Forms.ToolStripStatusLabel pageCountStatusStripLabel;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        private Atalasoft.Imaging.WinControls.ThumbnailView thumbnailViewer;
        private Atalasoft.Imaging.WinControls.WorkspaceViewer workspaceViewer;
        private System.Windows.Forms.ToolStripStatusLabel commandDetailStatusStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel mergeTransferStatusStatusStripLabel;
    }
}

