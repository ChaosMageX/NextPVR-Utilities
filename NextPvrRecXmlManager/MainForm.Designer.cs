namespace NextPvrRecXmlManager
{
    partial class MainForm
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
            this.mChannelsLabel = new System.Windows.Forms.Label();
            this.mChannelsListBox = new System.Windows.Forms.ListBox();
            this.mChannelUpButton = new System.Windows.Forms.Button();
            this.mChannelDownButton = new System.Windows.Forms.Button();
            this.mShowsLabel = new System.Windows.Forms.Label();
            this.mShowsListBox = new System.Windows.Forms.ListBox();
            this.mShowUpButton = new System.Windows.Forms.Button();
            this.mShowDownButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mChannelNameSortGroup = new System.Windows.Forms.GroupBox();
            this.mChannelSortNameNoneRAD = new System.Windows.Forms.RadioButton();
            this.mChannelSortOidDescendingRAD = new System.Windows.Forms.RadioButton();
            this.mChannelSortOidAscendingRAD = new System.Windows.Forms.RadioButton();
            this.mChannelSortNameDescendingRAD = new System.Windows.Forms.RadioButton();
            this.mChannelSortNameAscendingRAD = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mChannelSortSizeNoneRAD = new System.Windows.Forms.RadioButton();
            this.mChannelSortSizeDescendingRAD = new System.Windows.Forms.RadioButton();
            this.mChannelSortSizeAscendingRAD = new System.Windows.Forms.RadioButton();
            this.mChannelSortButton = new System.Windows.Forms.Button();
            this.mTrashChannelsLabel = new System.Windows.Forms.Label();
            this.mTrashChannelsListBox = new System.Windows.Forms.ListBox();
            this.mTrashShowsLabel = new System.Windows.Forms.Label();
            this.mTrashShowsListBox = new System.Windows.Forms.ListBox();
            this.mTrashUpButton = new System.Windows.Forms.Button();
            this.mTrashDownButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.mChannelNameSortGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mChannelsLabel
            // 
            this.mChannelsLabel.AutoSize = true;
            this.mChannelsLabel.Location = new System.Drawing.Point(12, 134);
            this.mChannelsLabel.Name = "mChannelsLabel";
            this.mChannelsLabel.Size = new System.Drawing.Size(54, 13);
            this.mChannelsLabel.TabIndex = 0;
            this.mChannelsLabel.Text = "Channels:";
            // 
            // mChannelsListBox
            // 
            this.mChannelsListBox.FormattingEnabled = true;
            this.mChannelsListBox.Items.AddRange(new object[] {
            "MMMMM-MM | 00000"});
            this.mChannelsListBox.Location = new System.Drawing.Point(12, 150);
            this.mChannelsListBox.Name = "mChannelsListBox";
            this.mChannelsListBox.Size = new System.Drawing.Size(150, 251);
            this.mChannelsListBox.TabIndex = 1;
            this.mChannelsListBox.SelectedIndexChanged += new System.EventHandler(this.onChannelsSelectedIndexChanged);
            // 
            // mChannelUpButton
            // 
            this.mChannelUpButton.Enabled = false;
            this.mChannelUpButton.Location = new System.Drawing.Point(169, 200);
            this.mChannelUpButton.Name = "mChannelUpButton";
            this.mChannelUpButton.Size = new System.Drawing.Size(25, 25);
            this.mChannelUpButton.TabIndex = 2;
            this.mChannelUpButton.Text = "⬆️";
            this.mChannelUpButton.UseVisualStyleBackColor = true;
            this.mChannelUpButton.Click += new System.EventHandler(this.onChannelUpClick);
            // 
            // mChannelDownButton
            // 
            this.mChannelDownButton.Enabled = false;
            this.mChannelDownButton.Location = new System.Drawing.Point(169, 325);
            this.mChannelDownButton.Name = "mChannelDownButton";
            this.mChannelDownButton.Size = new System.Drawing.Size(25, 25);
            this.mChannelDownButton.TabIndex = 3;
            this.mChannelDownButton.Text = "⬇️";
            this.mChannelDownButton.UseVisualStyleBackColor = true;
            this.mChannelDownButton.Click += new System.EventHandler(this.onChannelDownClick);
            // 
            // mShowsLabel
            // 
            this.mShowsLabel.AutoSize = true;
            this.mShowsLabel.Location = new System.Drawing.Point(200, 134);
            this.mShowsLabel.Name = "mShowsLabel";
            this.mShowsLabel.Size = new System.Drawing.Size(42, 13);
            this.mShowsLabel.TabIndex = 4;
            this.mShowsLabel.Text = "Shows:";
            // 
            // mShowsListBox
            // 
            this.mShowsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mShowsListBox.FormattingEnabled = true;
            this.mShowsListBox.Location = new System.Drawing.Point(200, 150);
            this.mShowsListBox.Name = "mShowsListBox";
            this.mShowsListBox.Size = new System.Drawing.Size(351, 251);
            this.mShowsListBox.TabIndex = 5;
            // 
            // mShowUpButton
            // 
            this.mShowUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mShowUpButton.Enabled = false;
            this.mShowUpButton.Location = new System.Drawing.Point(557, 200);
            this.mShowUpButton.Name = "mShowUpButton";
            this.mShowUpButton.Size = new System.Drawing.Size(25, 25);
            this.mShowUpButton.TabIndex = 6;
            this.mShowUpButton.Text = "⬆️";
            this.mShowUpButton.UseVisualStyleBackColor = true;
            this.mShowUpButton.Click += new System.EventHandler(this.onShowUpClick);
            // 
            // mShowDownButton
            // 
            this.mShowDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mShowDownButton.Enabled = false;
            this.mShowDownButton.Location = new System.Drawing.Point(557, 325);
            this.mShowDownButton.Name = "mShowDownButton";
            this.mShowDownButton.Size = new System.Drawing.Size(25, 25);
            this.mShowDownButton.TabIndex = 7;
            this.mShowDownButton.Text = "⬇️";
            this.mShowDownButton.UseVisualStyleBackColor = true;
            this.mShowDownButton.Click += new System.EventHandler(this.onShowDownClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(594, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.onFileOpenClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.onFileSaveClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.onFileSaveAsClick);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.onFileCloseClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.onFileExitClick);
            // 
            // mChannelNameSortGroup
            // 
            this.mChannelNameSortGroup.Controls.Add(this.mChannelSortNameNoneRAD);
            this.mChannelNameSortGroup.Controls.Add(this.mChannelSortOidDescendingRAD);
            this.mChannelNameSortGroup.Controls.Add(this.mChannelSortOidAscendingRAD);
            this.mChannelNameSortGroup.Controls.Add(this.mChannelSortNameDescendingRAD);
            this.mChannelNameSortGroup.Controls.Add(this.mChannelSortNameAscendingRAD);
            this.mChannelNameSortGroup.Location = new System.Drawing.Point(15, 31);
            this.mChannelNameSortGroup.Name = "mChannelNameSortGroup";
            this.mChannelNameSortGroup.Size = new System.Drawing.Size(513, 45);
            this.mChannelNameSortGroup.TabIndex = 9;
            this.mChannelNameSortGroup.TabStop = false;
            this.mChannelNameSortGroup.Text = "Channel Name Sort:";
            // 
            // mChannelSortNameNoneRAD
            // 
            this.mChannelSortNameNoneRAD.AutoSize = true;
            this.mChannelSortNameNoneRAD.Checked = true;
            this.mChannelSortNameNoneRAD.Location = new System.Drawing.Point(7, 20);
            this.mChannelSortNameNoneRAD.Name = "mChannelSortNameNoneRAD";
            this.mChannelSortNameNoneRAD.Size = new System.Drawing.Size(51, 17);
            this.mChannelSortNameNoneRAD.TabIndex = 4;
            this.mChannelSortNameNoneRAD.TabStop = true;
            this.mChannelSortNameNoneRAD.Text = "None";
            this.mChannelSortNameNoneRAD.UseVisualStyleBackColor = true;
            // 
            // mChannelSortOidDescendingRAD
            // 
            this.mChannelSortOidDescendingRAD.AutoSize = true;
            this.mChannelSortOidDescendingRAD.Location = new System.Drawing.Point(398, 20);
            this.mChannelSortOidDescendingRAD.Name = "mChannelSortOidDescendingRAD";
            this.mChannelSortOidDescendingRAD.Size = new System.Drawing.Size(104, 17);
            this.mChannelSortOidDescendingRAD.TabIndex = 3;
            this.mChannelSortOidDescendingRAD.Text = "OID Descending";
            this.mChannelSortOidDescendingRAD.UseVisualStyleBackColor = true;
            // 
            // mChannelSortOidAscendingRAD
            // 
            this.mChannelSortOidAscendingRAD.AutoSize = true;
            this.mChannelSortOidAscendingRAD.Location = new System.Drawing.Point(295, 20);
            this.mChannelSortOidAscendingRAD.Name = "mChannelSortOidAscendingRAD";
            this.mChannelSortOidAscendingRAD.Size = new System.Drawing.Size(97, 17);
            this.mChannelSortOidAscendingRAD.TabIndex = 2;
            this.mChannelSortOidAscendingRAD.Text = "OID Ascending";
            this.mChannelSortOidAscendingRAD.UseVisualStyleBackColor = true;
            // 
            // mChannelSortNameDescendingRAD
            // 
            this.mChannelSortNameDescendingRAD.AutoSize = true;
            this.mChannelSortNameDescendingRAD.Location = new System.Drawing.Point(176, 20);
            this.mChannelSortNameDescendingRAD.Name = "mChannelSortNameDescendingRAD";
            this.mChannelSortNameDescendingRAD.Size = new System.Drawing.Size(113, 17);
            this.mChannelSortNameDescendingRAD.TabIndex = 1;
            this.mChannelSortNameDescendingRAD.Text = "Name Descending";
            this.mChannelSortNameDescendingRAD.UseVisualStyleBackColor = true;
            // 
            // mChannelSortNameAscendingRAD
            // 
            this.mChannelSortNameAscendingRAD.AutoSize = true;
            this.mChannelSortNameAscendingRAD.Location = new System.Drawing.Point(64, 20);
            this.mChannelSortNameAscendingRAD.Name = "mChannelSortNameAscendingRAD";
            this.mChannelSortNameAscendingRAD.Size = new System.Drawing.Size(106, 17);
            this.mChannelSortNameAscendingRAD.TabIndex = 0;
            this.mChannelSortNameAscendingRAD.Text = "Name Ascending";
            this.mChannelSortNameAscendingRAD.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mChannelSortSizeNoneRAD);
            this.groupBox1.Controls.Add(this.mChannelSortSizeDescendingRAD);
            this.groupBox1.Controls.Add(this.mChannelSortSizeAscendingRAD);
            this.groupBox1.Location = new System.Drawing.Point(15, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 45);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel Size Sort:";
            // 
            // mChannelSortSizeNoneRAD
            // 
            this.mChannelSortSizeNoneRAD.AutoSize = true;
            this.mChannelSortSizeNoneRAD.Checked = true;
            this.mChannelSortSizeNoneRAD.Location = new System.Drawing.Point(7, 20);
            this.mChannelSortSizeNoneRAD.Name = "mChannelSortSizeNoneRAD";
            this.mChannelSortSizeNoneRAD.Size = new System.Drawing.Size(51, 17);
            this.mChannelSortSizeNoneRAD.TabIndex = 2;
            this.mChannelSortSizeNoneRAD.TabStop = true;
            this.mChannelSortSizeNoneRAD.Text = "None";
            this.mChannelSortSizeNoneRAD.UseVisualStyleBackColor = true;
            // 
            // mChannelSortSizeDescendingRAD
            // 
            this.mChannelSortSizeDescendingRAD.AutoSize = true;
            this.mChannelSortSizeDescendingRAD.Location = new System.Drawing.Point(142, 20);
            this.mChannelSortSizeDescendingRAD.Name = "mChannelSortSizeDescendingRAD";
            this.mChannelSortSizeDescendingRAD.Size = new System.Drawing.Size(82, 17);
            this.mChannelSortSizeDescendingRAD.TabIndex = 1;
            this.mChannelSortSizeDescendingRAD.Text = "Descending";
            this.mChannelSortSizeDescendingRAD.UseVisualStyleBackColor = true;
            // 
            // mChannelSortSizeAscendingRAD
            // 
            this.mChannelSortSizeAscendingRAD.AutoSize = true;
            this.mChannelSortSizeAscendingRAD.Location = new System.Drawing.Point(64, 20);
            this.mChannelSortSizeAscendingRAD.Name = "mChannelSortSizeAscendingRAD";
            this.mChannelSortSizeAscendingRAD.Size = new System.Drawing.Size(75, 17);
            this.mChannelSortSizeAscendingRAD.TabIndex = 0;
            this.mChannelSortSizeAscendingRAD.Text = "Ascending";
            this.mChannelSortSizeAscendingRAD.UseVisualStyleBackColor = true;
            // 
            // mChannelSortButton
            // 
            this.mChannelSortButton.Enabled = false;
            this.mChannelSortButton.Location = new System.Drawing.Point(252, 97);
            this.mChannelSortButton.Name = "mChannelSortButton";
            this.mChannelSortButton.Size = new System.Drawing.Size(104, 23);
            this.mChannelSortButton.TabIndex = 11;
            this.mChannelSortButton.Text = "Sort Channels";
            this.mChannelSortButton.UseVisualStyleBackColor = true;
            this.mChannelSortButton.Click += new System.EventHandler(this.onChannelSortClick);
            // 
            // mTrashChannelsLabel
            // 
            this.mTrashChannelsLabel.AutoSize = true;
            this.mTrashChannelsLabel.Location = new System.Drawing.Point(12, 438);
            this.mTrashChannelsLabel.Name = "mTrashChannelsLabel";
            this.mTrashChannelsLabel.Size = new System.Drawing.Size(79, 13);
            this.mTrashChannelsLabel.TabIndex = 12;
            this.mTrashChannelsLabel.Text = "Channel Trash:";
            // 
            // mTrashChannelsListBox
            // 
            this.mTrashChannelsListBox.FormattingEnabled = true;
            this.mTrashChannelsListBox.Location = new System.Drawing.Point(12, 454);
            this.mTrashChannelsListBox.Name = "mTrashChannelsListBox";
            this.mTrashChannelsListBox.Size = new System.Drawing.Size(150, 95);
            this.mTrashChannelsListBox.TabIndex = 13;
            this.mTrashChannelsListBox.SelectedIndexChanged += new System.EventHandler(this.onTrashChannelsSelectedIndexChanged);
            // 
            // mTrashShowsLabel
            // 
            this.mTrashShowsLabel.AutoSize = true;
            this.mTrashShowsLabel.Location = new System.Drawing.Point(200, 438);
            this.mTrashShowsLabel.Name = "mTrashShowsLabel";
            this.mTrashShowsLabel.Size = new System.Drawing.Size(67, 13);
            this.mTrashShowsLabel.TabIndex = 14;
            this.mTrashShowsLabel.Text = "Show Trash:";
            // 
            // mTrashShowsListBox
            // 
            this.mTrashShowsListBox.FormattingEnabled = true;
            this.mTrashShowsListBox.Location = new System.Drawing.Point(200, 454);
            this.mTrashShowsListBox.Name = "mTrashShowsListBox";
            this.mTrashShowsListBox.Size = new System.Drawing.Size(351, 95);
            this.mTrashShowsListBox.TabIndex = 15;
            // 
            // mTrashUpButton
            // 
            this.mTrashUpButton.Enabled = false;
            this.mTrashUpButton.Location = new System.Drawing.Point(300, 407);
            this.mTrashUpButton.Name = "mTrashUpButton";
            this.mTrashUpButton.Size = new System.Drawing.Size(25, 25);
            this.mTrashUpButton.TabIndex = 16;
            this.mTrashUpButton.Text = "⬆️";
            this.mTrashUpButton.UseVisualStyleBackColor = true;
            this.mTrashUpButton.Click += new System.EventHandler(this.onTrashUpClick);
            // 
            // mTrashDownButton
            // 
            this.mTrashDownButton.Enabled = false;
            this.mTrashDownButton.Location = new System.Drawing.Point(425, 407);
            this.mTrashDownButton.Name = "mTrashDownButton";
            this.mTrashDownButton.Size = new System.Drawing.Size(25, 25);
            this.mTrashDownButton.TabIndex = 17;
            this.mTrashDownButton.Text = "⬇️";
            this.mTrashDownButton.UseVisualStyleBackColor = true;
            this.mTrashDownButton.Click += new System.EventHandler(this.onTrashDownClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 561);
            this.Controls.Add(this.mTrashDownButton);
            this.Controls.Add(this.mTrashUpButton);
            this.Controls.Add(this.mTrashShowsListBox);
            this.Controls.Add(this.mTrashShowsLabel);
            this.Controls.Add(this.mTrashChannelsListBox);
            this.Controls.Add(this.mTrashChannelsLabel);
            this.Controls.Add(this.mChannelSortButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mChannelNameSortGroup);
            this.Controls.Add(this.mShowDownButton);
            this.Controls.Add(this.mShowUpButton);
            this.Controls.Add(this.mShowsListBox);
            this.Controls.Add(this.mShowsLabel);
            this.Controls.Add(this.mChannelDownButton);
            this.Controls.Add(this.mChannelUpButton);
            this.Controls.Add(this.mChannelsListBox);
            this.Controls.Add(this.mChannelsLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "NextPVR Recording XML Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mChannelNameSortGroup.ResumeLayout(false);
            this.mChannelNameSortGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mChannelsLabel;
        private System.Windows.Forms.ListBox mChannelsListBox;
        private System.Windows.Forms.Button mChannelUpButton;
        private System.Windows.Forms.Button mChannelDownButton;
        private System.Windows.Forms.Label mShowsLabel;
        private System.Windows.Forms.ListBox mShowsListBox;
        private System.Windows.Forms.Button mShowUpButton;
        private System.Windows.Forms.Button mShowDownButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox mChannelNameSortGroup;
        private System.Windows.Forms.RadioButton mChannelSortNameNoneRAD;
        private System.Windows.Forms.RadioButton mChannelSortOidDescendingRAD;
        private System.Windows.Forms.RadioButton mChannelSortOidAscendingRAD;
        private System.Windows.Forms.RadioButton mChannelSortNameDescendingRAD;
        private System.Windows.Forms.RadioButton mChannelSortNameAscendingRAD;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mChannelSortSizeNoneRAD;
        private System.Windows.Forms.RadioButton mChannelSortSizeDescendingRAD;
        private System.Windows.Forms.RadioButton mChannelSortSizeAscendingRAD;
        private System.Windows.Forms.Button mChannelSortButton;
        private System.Windows.Forms.Label mTrashChannelsLabel;
        private System.Windows.Forms.ListBox mTrashChannelsListBox;
        private System.Windows.Forms.Label mTrashShowsLabel;
        private System.Windows.Forms.ListBox mTrashShowsListBox;
        private System.Windows.Forms.Button mTrashUpButton;
        private System.Windows.Forms.Button mTrashDownButton;
    }
}

