using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NextPVRServiceMonitor
{
    public partial class MainForm : Form
    {
        private const string cDefaultNpvrLogPath = "C:\\Users\\Public\\NPVR\\Logs";

        private volatile bool bResetTitle = false;
        private string mOriginalTitle;

        private ServiceController mNPVRRecSC;

        private volatile bool bUpdateLogTXT = false;
        private StringBuilder mLogBuilder = new StringBuilder();
        private StreamWriter mLogFileWriter;

        // Should this be volatile?
        private string mNpvrLogPath;
        private string mLogBackupPath;

        private volatile bool bKeepRunning = true;
        private Thread mMainThread;

        private bool initializeStuff()
        {
            if (!bKeepRunning) return false;

            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            for (int i = scServices.Length - 1; i >= 0; i--)
            {
                mNPVRRecSC = scServices[i];
                if (mNPVRRecSC.ServiceName == "NPVR Recording Service")
                    break;
            }

            string logFile = Path.Combine(Directory.GetCurrentDirectory(), 
                "NextPVRServiceLog.txt");
            mLogFileWriter = new StreamWriter(logFile, true);

            mNpvrLogPath = Properties.Settings.Default.NpvrLogPath;
            if (!Directory.Exists(mNpvrLogPath))
            {
                mNpvrLogPath = cDefaultNpvrLogPath;
                Directory.CreateDirectory(mNpvrLogPath);
            }
            createBackupPath();
            npvrLogLocTXT.Text = mNpvrLogPath;

            return true;
        }

        private void createBackupPath()
        {
            string path = Path.GetFileName(mNpvrLogPath);
            path = string.Concat(path, "_Backup");
            path = Path.Combine(Path.GetDirectoryName(mNpvrLogPath), path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            mLogBackupPath = path;
        }

        private void copyContents(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            string file;
            string[] files = Directory.GetFiles(sourceDir, 
                "*", SearchOption.TopDirectoryOnly);
            for (int i = files.Length - 1; i >= 0; i--)
            {
                file = Path.Combine(targetDir, Path.GetFileName(files[i]));
                File.Copy(files[i], file, true);
            }

            string dir;
            string[] dirs = Directory.GetDirectories(sourceDir, 
                "*", SearchOption.TopDirectoryOnly);
            for (int j = dirs.Length - 1; j >= 0; j--)
            {
                dir = Path.Combine(targetDir, Path.GetFileName(dirs[j]));
                copyContents(dirs[j], dir);
            }
        }

        private void backupNpvrLogs(DateTime dt)
        {
            string path = dt.ToString("_yyyy-dd-MM_HH\\hmm_ss");
            path = string.Concat(Path.GetFileName(mNpvrLogPath), path);
            path = Path.Combine(mLogBackupPath, path);
            copyContents(mNpvrLogPath, path);
        }

        private void mainFunction()
        {
            DateTime nowDT;
            // Wait 30 seconds for NextPVR to start the service itself.
            // This is done just in case this app is run from Startup.
            int wait = Properties.Settings.Default.SecsToStart;
            if (wait <= 0) wait = 30;
            if (wait > 600) wait = 600;
            Thread.Sleep(wait * 1000);
            bResetTitle = true;

            while (bKeepRunning)
            {
                if (mNPVRRecSC.Status == ServiceControllerStatus.Stopped)
                {
                    nowDT = DateTime.Now;
                    
                    // Log and report the time the service stopped

                    mLogBuilder.Append("Stopped ");
                    mLogBuilder.AppendLine(nowDT.ToString("F"));
                    bUpdateLogTXT = true;

                    mLogFileWriter.Write("Stopped ");
                    mLogFileWriter.WriteLine(nowDT.ToString("F"));
                    mLogFileWriter.Flush();

                    // Backup the NextPVR logs

                    backupNpvrLogs(nowDT);

                    // Attempt to restart the service until successful

                    try
                    {
                        mNPVRRecSC.Start();
                        while (mNPVRRecSC.Status != ServiceControllerStatus.Running)
                        {
                            Thread.Sleep(500);
                            mNPVRRecSC.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        bKeepRunning = false;
                        nowDT = DateTime.Now;

                        mLogBuilder.Append("  Error ");
                        mLogBuilder.AppendLine(nowDT.ToString("F"));
                        bUpdateLogTXT = true;

                        mLogFileWriter.Write("  Error ");
                        mLogFileWriter.WriteLine(nowDT.ToString("F"));
                        mLogFileWriter.WriteLine(
                            "Failed to restart NextPVR Recording Service.");
                        mLogFileWriter.WriteLine("  Error details below:");
                        mLogFileWriter.WriteLine("== START ==");
                        for (Exception inex = ex; inex != null; inex = inex.InnerException)
                        {
                            mLogFileWriter.WriteLine(inex.Message);
                            mLogFileWriter.Write("Help: ");
                            mLogFileWriter.WriteLine(inex.HelpLink);
                            mLogFileWriter.WriteLine(inex.StackTrace);
                            mLogFileWriter.WriteLine("== ----- ==");
                        }
                        mLogFileWriter.Flush();
                    }

                    if (!bKeepRunning) break;

                    nowDT = DateTime.Now;

                    // Log and report the time the service started again

                    mLogBuilder.Append("Started ");
                    mLogBuilder.AppendLine(nowDT.ToString("F"));
                    bUpdateLogTXT = true;

                    mLogFileWriter.Write("Started ");
                    mLogFileWriter.WriteLine(nowDT.ToString("F"));
                    mLogFileWriter.Flush();
                }
                Thread.Sleep(1000);
                mNPVRRecSC.Refresh();
            }
        }

        private bool performSecurityChecks()
        {
            // Make sure there isn't already an instance of this app

            int count = 0;
            Process process = Process.GetCurrentProcess();
            string name = process.ProcessName;
            Process[] processes = Process.GetProcesses();
            for (int i = processes.Length - 1; i >= 0; i--)
            {
                if (string.Equals(name, processes[i].ProcessName, 
                    StringComparison.OrdinalIgnoreCase))
                    count++;
            }
            if (count > 1)
            {
                MessageBox.Show(
                    "There is already an instance of this app running.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bKeepRunning = false;
                return false;
            }

            // Make sure this app has administrator privileges

            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    MessageBox.Show(
                        "This app needs to run with admin privileges.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bKeepRunning = false;
                    return false;
                }
            }

            return true;
        }

        public MainForm()
        {
            InitializeComponent();
            mOriginalTitle = this.Text;
            this.Text = mOriginalTitle + " (Waiting...)";
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            performSecurityChecks();
            initializeStuff();

            if (bKeepRunning)
            {
                mMainThread = new Thread(mainFunction);
                mMainThread.Start();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bKeepRunning)
            {
                bKeepRunning = false;

                // Wait for the main thread to finish

                while (mNPVRRecSC.Status != ServiceControllerStatus.Running)
                {
                    Thread.Sleep(500);
                    mNPVRRecSC.Refresh();
                }
                Thread.Sleep(2000);

                // Free up the allocated resources

                mNPVRRecSC.Close();
                mLogFileWriter.Close();
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (bUpdateLogTXT)
            {
                logTXT.Text = mLogBuilder.ToString();
                bUpdateLogTXT = false;
            }
            if (bResetTitle)
            {
                this.Text = mOriginalTitle;
                bResetTitle = false;
            }
            if (!bKeepRunning)
            {
                this.Text = mOriginalTitle + " (Closing...)";
                this.Close();
            }
        }

        private void npvrLogLocBTN_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = mNpvrLogPath;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    mNpvrLogPath = folderDialog.SelectedPath;
                    createBackupPath();
                    npvrLogLocTXT.Text = mNpvrLogPath;
                    Properties.Settings.Default.NpvrLogPath = mNpvrLogPath;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void backupNpvrLogsBTN_Click(object sender, EventArgs e)
        {
            backupNpvrLogs(DateTime.Now);
        }
    }
}
