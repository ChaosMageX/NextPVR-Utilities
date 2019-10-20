namespace NextPVRServiceMonitor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.logTXT = new System.Windows.Forms.TextBox();
            this.npvrLogLocLBL = new System.Windows.Forms.Label();
            this.npvrLogLocTXT = new System.Windows.Forms.TextBox();
            this.npvrLogLocBTN = new System.Windows.Forms.Button();
            this.logLBL = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.backupNpvrLogsBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logTXT
            // 
            this.logTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTXT.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTXT.Location = new System.Drawing.Point(13, 65);
            this.logTXT.Multiline = true;
            this.logTXT.Name = "logTXT";
            this.logTXT.ReadOnly = true;
            this.logTXT.Size = new System.Drawing.Size(559, 284);
            this.logTXT.TabIndex = 0;
            this.logTXT.WordWrap = false;
            // 
            // npvrLogLocLBL
            // 
            this.npvrLogLocLBL.AutoSize = true;
            this.npvrLogLocLBL.Location = new System.Drawing.Point(13, 13);
            this.npvrLogLocLBL.Name = "npvrLogLocLBL";
            this.npvrLogLocLBL.Size = new System.Drawing.Size(112, 13);
            this.npvrLogLocLBL.TabIndex = 1;
            this.npvrLogLocLBL.Text = "NextPVR Logs Folder:";
            // 
            // npvrLogLocTXT
            // 
            this.npvrLogLocTXT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.npvrLogLocTXT.Location = new System.Drawing.Point(131, 10);
            this.npvrLogLocTXT.Name = "npvrLogLocTXT";
            this.npvrLogLocTXT.ReadOnly = true;
            this.npvrLogLocTXT.Size = new System.Drawing.Size(360, 20);
            this.npvrLogLocTXT.TabIndex = 2;
            this.npvrLogLocTXT.Text = "C:\\Users\\Public\\NPVR\\Logs";
            // 
            // npvrLogLocBTN
            // 
            this.npvrLogLocBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.npvrLogLocBTN.Location = new System.Drawing.Point(497, 8);
            this.npvrLogLocBTN.Name = "npvrLogLocBTN";
            this.npvrLogLocBTN.Size = new System.Drawing.Size(75, 23);
            this.npvrLogLocBTN.TabIndex = 3;
            this.npvrLogLocBTN.Text = "Browse";
            this.npvrLogLocBTN.UseVisualStyleBackColor = true;
            this.npvrLogLocBTN.Click += new System.EventHandler(this.npvrLogLocBTN_Click);
            // 
            // logLBL
            // 
            this.logLBL.AutoSize = true;
            this.logLBL.Location = new System.Drawing.Point(12, 49);
            this.logLBL.Name = "logLBL";
            this.logLBL.Size = new System.Drawing.Size(203, 13);
            this.logLBL.TabIndex = 4;
            this.logLBL.Text = "NextPVR Recording Service Activity Log:";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // backupNpvrLogsBTN
            // 
            this.backupNpvrLogsBTN.Location = new System.Drawing.Point(402, 36);
            this.backupNpvrLogsBTN.Name = "backupNpvrLogsBTN";
            this.backupNpvrLogsBTN.Size = new System.Drawing.Size(170, 23);
            this.backupNpvrLogsBTN.TabIndex = 5;
            this.backupNpvrLogsBTN.Text = "Backup NextPVR Logs Now";
            this.backupNpvrLogsBTN.UseVisualStyleBackColor = true;
            this.backupNpvrLogsBTN.Click += new System.EventHandler(this.backupNpvrLogsBTN_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.backupNpvrLogsBTN);
            this.Controls.Add(this.logLBL);
            this.Controls.Add(this.npvrLogLocBTN);
            this.Controls.Add(this.npvrLogLocTXT);
            this.Controls.Add(this.npvrLogLocLBL);
            this.Controls.Add(this.logTXT);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "NextPVR Service Monitor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logTXT;
        private System.Windows.Forms.Label npvrLogLocLBL;
        private System.Windows.Forms.TextBox npvrLogLocTXT;
        private System.Windows.Forms.Button npvrLogLocBTN;
        private System.Windows.Forms.Label logLBL;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Button backupNpvrLogsBTN;
    }
}

