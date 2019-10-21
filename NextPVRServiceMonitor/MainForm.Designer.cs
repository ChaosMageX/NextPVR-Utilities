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
            resources.ApplyResources(this.logTXT, "logTXT");
            this.logTXT.Name = "logTXT";
            this.logTXT.ReadOnly = true;
            // 
            // npvrLogLocLBL
            // 
            resources.ApplyResources(this.npvrLogLocLBL, "npvrLogLocLBL");
            this.npvrLogLocLBL.Name = "npvrLogLocLBL";
            // 
            // npvrLogLocTXT
            // 
            resources.ApplyResources(this.npvrLogLocTXT, "npvrLogLocTXT");
            this.npvrLogLocTXT.Name = "npvrLogLocTXT";
            this.npvrLogLocTXT.ReadOnly = true;
            // 
            // npvrLogLocBTN
            // 
            resources.ApplyResources(this.npvrLogLocBTN, "npvrLogLocBTN");
            this.npvrLogLocBTN.Name = "npvrLogLocBTN";
            this.npvrLogLocBTN.UseVisualStyleBackColor = true;
            this.npvrLogLocBTN.Click += new System.EventHandler(this.npvrLogLocBTN_Click);
            // 
            // logLBL
            // 
            resources.ApplyResources(this.logLBL, "logLBL");
            this.logLBL.Name = "logLBL";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // backupNpvrLogsBTN
            // 
            resources.ApplyResources(this.backupNpvrLogsBTN, "backupNpvrLogsBTN");
            this.backupNpvrLogsBTN.Name = "backupNpvrLogsBTN";
            this.backupNpvrLogsBTN.UseVisualStyleBackColor = true;
            this.backupNpvrLogsBTN.Click += new System.EventHandler(this.backupNpvrLogsBTN_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backupNpvrLogsBTN);
            this.Controls.Add(this.logLBL);
            this.Controls.Add(this.npvrLogLocBTN);
            this.Controls.Add(this.npvrLogLocTXT);
            this.Controls.Add(this.npvrLogLocLBL);
            this.Controls.Add(this.logTXT);
            this.Name = "MainForm";
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

