using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NextPVRServiceMonitor
{
    public partial class MainForm : Form
    {
        private const string cDefaultNpvrLogPath = "C:\\Users\\Public\\NPVR\\Logs";

        private ServiceController mNPVRRecSC;

        private StringBuilder mLogBuilder;
        private StreamWriter mLogFileWriter;

        // Should this be volatile?
        private string mNpvrLogPath;

        private bool bKeepRunning = true;
        private Thread mMainThread;

        private void initializeStuff()
        {
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            for (int i = scServices.Length - 1; i >= 0; i--)
            {
                mNPVRRecSC = scServices[i];
                if (mNPVRRecSC.ServiceName == "NPVR Recording Service")
                    break;
            }

            mLogBuilder = new StringBuilder();

            string logFile = Path.Combine(Directory.GetCurrentDirectory(), 
                "NextPVRServiceLog.txt");
            mLogFileWriter = new StreamWriter(logFile, true);

            mNpvrLogPath = Properties.Settings.Default.sLogFilePath;
            if (!Directory.Exists(mNpvrLogPath))
            {
                mNpvrLogPath = cDefaultNpvrLogPath;
                Directory.CreateDirectory(mNpvrLogPath);
            }
            npvrLogLocTXT.Text = mNpvrLogPath;
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

        private void mainFunction()
        {
            DateTime nowDT;
            string backupPath;
            string logFolderName;
            string logFolderParent;

            while (bKeepRunning)
            {
                if (mNPVRRecSC.Status == ServiceControllerStatus.Stopped)
                {
                    nowDT = DateTime.Now;
                    
                    // Log and report the time the service stopped

                    mLogBuilder.Append("Stopped ");
                    mLogBuilder.AppendLine(nowDT.ToString("F"));

                    mLogFileWriter.Write("Stopped ");
                    mLogFileWriter.WriteLine(nowDT.ToString("F"));
                    mLogFileWriter.Flush();

                    // Backup the NextPVR logs

                    logFolderName = Path.GetFileName(mNpvrLogPath);
                    logFolderParent = Path.GetDirectoryName(mNpvrLogPath);

                    backupPath = nowDT.ToString("_yyyy-dd-MM_HH\\hmm_ss");
                    backupPath = string.Concat(logFolderName, backupPath);
                    backupPath = Path.Combine(logFolderParent, backupPath);
                    copyContents(mNpvrLogPath, backupPath);

                    // Attempt to restart the service until successful

                    mNPVRRecSC.Start();
                    while (mNPVRRecSC.Status != ServiceControllerStatus.Running 
                        && bKeepRunning)
                    {
                        Thread.Sleep(500);
                        mNPVRRecSC.Refresh();
                    }

                    nowDT = DateTime.Now;

                    // Log and report the time the service started again

                    mLogBuilder.Append("Started ");
                    mLogBuilder.AppendLine(nowDT.ToString("F"));

                    mLogFileWriter.Write("Started ");
                    mLogFileWriter.WriteLine(nowDT.ToString("F"));
                    mLogFileWriter.Flush();
                }
                Thread.Sleep(1000);
                mNPVRRecSC.Refresh();
            }
        }

        public MainForm()
        {
            InitializeComponent();
            initializeStuff();

            mMainThread = new Thread(mainFunction);
            mMainThread.Start();
        }

        private void npvrLogLocBTN_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = mNpvrLogPath;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                mNpvrLogPath = folderDialog.SelectedPath;
                npvrLogLocTXT.Text = mNpvrLogPath;
                Properties.Settings.Default.sLogFilePath = mNpvrLogPath;
                Properties.Settings.Default.Save();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            bKeepRunning = false;
            Thread.Sleep(3000);
            mLogFileWriter.Close();
            Properties.Settings.Default.Save();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            logTXT.Text = mLogBuilder.ToString();
        }
    }
}
