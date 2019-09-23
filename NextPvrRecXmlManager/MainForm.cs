using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using NextPvrTools;

namespace NextPvrRecXmlManager
{
    public partial class MainForm : Form
    {
        private class RecurringChannel
        {
            public int OID;
            public string Name;
            public RecurringRecordingList List;
            public List<RecurringRecording> Shows;

            public RecurringChannel(int oid, string name, RecurringRecordingList list)
            {
                this.OID = oid;
                this.Name = name;
                this.List = list;
                this.Shows = new List<RecurringRecording>();
            }
        }

        private string mFileName = null;
        private RecurringRecordingList[] mRecurringChannels = null;
        private RecurringChannel[] mRecurringTrash = null;
        private int mRecurringChannelCount = 0;
        private int mRecurringTrashCount = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void onChannelUpClick(object sender, EventArgs e)
        {
            int index = this.mChannelsListBox.SelectedIndex;
            if (index == 0)
                return;

            object display = this.mChannelsListBox.Items[index];
            this.mChannelsListBox.Items[index] = this.mChannelsListBox.Items[index - 1];
            this.mChannelsListBox.Items[index - 1] = display;

            RecurringRecordingList list = this.mRecurringChannels[index];
            this.mRecurringChannels[index] = this.mRecurringChannels[index - 1];
            this.mRecurringChannels[index - 1] = list;

            this.mChannelsListBox.SelectedIndex = index - 1;
        }

        private void onChannelDownClick(object sender, EventArgs e)
        {
            int index = this.mChannelsListBox.SelectedIndex;
            if (index == this.mRecurringChannelCount - 1)
                return;

            object display = this.mChannelsListBox.Items[index];
            this.mChannelsListBox.Items[index] = this.mChannelsListBox.Items[index + 1];
            this.mChannelsListBox.Items[index + 1] = display;

            RecurringRecordingList list = this.mRecurringChannels[index];
            this.mRecurringChannels[index] = this.mRecurringChannels[index + 1];
            this.mRecurringChannels[index + 1] = list;

            this.mChannelsListBox.SelectedIndex = index + 1;
        }

        private void onShowUpClick(object sender, EventArgs e)
        {
            int index = this.mChannelsListBox.SelectedIndex;
            RecurringRecordingList list = this.mRecurringChannels[index];
            index = this.mShowsListBox.SelectedIndex;
            if (index == 0)
                return;

            object display = this.mShowsListBox.Items[index];
            this.mShowsListBox.Items[index] = this.mShowsListBox.Items[index - 1];
            this.mShowsListBox.Items[index - 1] = display;

            RecurringRecording rec = list[index];
            list[index] = list[index - 1];
            list[index - 1] = rec;

            this.mShowsListBox.SelectedIndex = index - 1;
        }

        private void onShowDownClick(object sender, EventArgs e)
        {
            int index = this.mChannelsListBox.SelectedIndex;
            RecurringRecordingList list = this.mRecurringChannels[index];
            index = this.mShowsListBox.SelectedIndex;
            if (index == this.mRecurringChannelCount - 1)
                return;

            object display = this.mShowsListBox.Items[index];
            this.mShowsListBox.Items[index] = this.mShowsListBox.Items[index + 1];
            this.mShowsListBox.Items[index + 1] = display;

            RecurringRecording rec = list[index];
            list[index] = list[index + 1];
            list[index + 1] = rec;

            this.mShowsListBox.SelectedIndex = index + 1;
        }

        private void onChannelSortClick(object sender, EventArgs e)
        {
            if (this.mRecurringChannels == null || this.mRecurringChannelCount < 2)
                return;

            RecNameSort sortByName;
            if (this.mChannelSortNameNoneRAD.Checked)
                sortByName = RecNameSort.None;
            else if (this.mChannelSortNameAscendingRAD.Checked)
                sortByName = RecNameSort.NameAscending;
            else if (this.mChannelSortNameDescendingRAD.Checked)
                sortByName = RecNameSort.NameDescending;
            else if (this.mChannelSortOidAscendingRAD.Checked)
                sortByName = RecNameSort.OidAscending;
            else if (this.mChannelSortOidDescendingRAD.Checked)
                sortByName = RecNameSort.OidDescending;
            else
                sortByName = RecNameSort.None;

            RecSizeSort sortBySize;
            if (this.mChannelSortSizeNoneRAD.Checked)
                sortBySize = RecSizeSort.None;
            else if (this.mChannelSortSizeAscendingRAD.Checked)
                sortBySize = RecSizeSort.Ascending;
            else if (this.mChannelSortSizeDescendingRAD.Checked)
                sortBySize = RecSizeSort.Descending;
            else
                sortBySize = RecSizeSort.None;

            RecurringRecordingList.SortLists(this.mRecurringChannels, this.mRecurringChannelCount, sortByName, sortBySize);
        }

        private void onChannelsSelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.mChannelsListBox.SelectedIndex;
            this.mShowsListBox.Items.Clear();
            foreach (RecurringRecording rec in this.mRecurringChannels[index])
            {
                this.mShowsListBox.Items.Add(string.Concat(rec.Name, " | ", rec.CreateRuleString()));
            }
            index = this.mRecurringChannels[index].Count;
            this.mShowUpButton.Enabled = index > 1;
            this.mShowDownButton.Enabled = index > 1;
        }

        private void onTrashUpClick(object sender, EventArgs e)
        {
            int index = this.mTrashChannelsListBox.SelectedIndex;
            RecurringChannel channel = this.mRecurringTrash[index];
            index = this.mTrashShowsListBox.SelectedIndex;
            RecurringRecording show = channel.Shows[index];
            RecurringRecordingList recList;
            if (this.mRecurringChannelCount == 0)
            {
                recList = channel.List;
                recList.Add(show);
                this.mRecurringChannels[this.mRecurringChannelCount++] = recList;
                this.mChannelsListBox.Items.Add(string.Concat(channel.Name, " | 1"));
            }
            else
            {
                int i = 0;
                recList = this.mRecurringChannels[i];
                while (i < this.mRecurringChannelCount && recList.ChannelOID != channel.OID)
                {
                    recList = this.mRecurringChannels[++i];
                }
                if (i == this.mRecurringChannelCount)
                {
                    recList = channel.List;
                    recList.Add(show);
                    this.mRecurringChannels[this.mRecurringChannelCount++] = recList;
                    this.mChannelsListBox.Items.Add(string.Concat(channel.Name, " | 1"));
                }
                else
                {
                    recList.Add(show);
                    this.mChannelsListBox.Items[i] = string.Concat(channel.Name, " | ", recList.Count.ToString());
                }
            }
            if (channel.Shows.Count > 1)
            {
                this.mTrashShowsListBox.SelectedIndex = index == 0 ? 1 : index - 1;
            }
            channel.Shows.RemoveAt(index);
            this.mTrashShowsListBox.Items.RemoveAt(index);
            if (channel.Shows.Count == 0)
            {
                index = this.mTrashChannelsListBox.SelectedIndex;
                if (this.mRecurringTrashCount > 0)
                {
                    this.mTrashChannelsListBox.SelectedIndex = index == 0 ? 1 : index - 1;
                }
                this.mRecurringTrashCount--;
                for (int j = index; j < this.mRecurringTrashCount; j++)
                {
                    this.mRecurringTrash[j] = this.mRecurringTrash[j + 1];
                }
                this.mTrashChannelsListBox.Items.RemoveAt(index);
            }
            this.mChannelUpButton.Enabled = this.mRecurringChannelCount > 1;
            this.mChannelDownButton.Enabled = this.mRecurringChannelCount > 1;
            this.mTrashUpButton.Enabled = this.mRecurringTrashCount == 0;
            this.mTrashDownButton.Enabled = true;
        }

        private void onTrashDownClick(object sender, EventArgs e)
        {
            int index = this.mChannelsListBox.SelectedIndex;
            RecurringRecordingList recList = this.mRecurringChannels[index];
            index = this.mShowsListBox.SelectedIndex;
            RecurringRecording rec = recList[index];
            RecurringChannel channel;
            if (this.mRecurringTrashCount == 0)
            {
                channel = new RecurringChannel(rec.ChannelOID, rec.ChannelName, recList);
                channel.Shows.Add(rec);
                this.mRecurringTrash[this.mRecurringTrashCount++] = channel;
                this.mTrashChannelsListBox.Items.Add(string.Concat(channel.Name, " | 1"));
            }
            else
            {
                int i = 0;
                channel = this.mRecurringTrash[i];
                while (i < this.mRecurringTrashCount && channel.OID != rec.ChannelOID)
                {
                    channel = this.mRecurringTrash[++i];
                }
                if (i == this.mRecurringTrashCount)
                {
                    channel = new RecurringChannel(rec.ChannelOID, rec.ChannelName, recList);
                    channel.Shows.Add(rec);
                    this.mRecurringTrash[this.mRecurringTrashCount++] = channel;
                    this.mTrashChannelsListBox.Items.Add(string.Concat(channel.Name, " | 1"));
                }
                else
                {
                    channel.Shows.Add(rec);
                    this.mTrashChannelsListBox.Items[i] = string.Concat(channel.Name, " | ", channel.Shows.Count.ToString());
                }
            }
            if (recList.Count > 1)
            {
                this.mShowsListBox.SelectedIndex = index == 0 ? 1 : index - 1;
            }
            recList.RemoveAt(index);
            this.mShowsListBox.Items.RemoveAt(index);
            if (recList.Count == 0)
            {
                index = this.mChannelsListBox.SelectedIndex;
                if (this.mRecurringChannelCount > 1)
                {
                    this.mChannelsListBox.SelectedIndex = index == 0 ? 1 : index - 1;
                }
                this.mRecurringChannelCount--;
                for (int j = index; j < this.mRecurringChannelCount; j++)
                {
                    this.mRecurringChannels[j] = this.mRecurringChannels[j + 1];
                }
                this.mChannelsListBox.Items.RemoveAt(index);
            }
            this.mChannelUpButton.Enabled = this.mRecurringChannelCount > 1;
            this.mChannelDownButton.Enabled = this.mRecurringChannelCount > 1;
            this.mTrashUpButton.Enabled = true;
            this.mTrashDownButton.Enabled = this.mRecurringChannelCount == 0;
        }

        private void onTrashChannelsSelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.mTrashChannelsListBox.SelectedIndex;
            RecurringChannel channel = this.mRecurringTrash[index];
            this.mTrashShowsListBox.Items.Clear();
            foreach (RecurringRecording rec in channel.Shows)
            {
                this.mTrashShowsListBox.Items.Add(string.Concat(rec.Name, " | ", rec.CreateRuleString()));
            }
        }

        private void onFileOpenClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML Files (*.xml)|*.xml";
            dialog.RestoreDirectory = true;
            dialog.ShowReadOnly = true;
            dialog.ReadOnlyChecked = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.mFileName = dialog.FileName;
                XmlDocument document = new XmlDocument();
                document.Load(this.mFileName);
                string rootName = document.DocumentElement.Name;
                if (rootName.Equals("recurrings"))
                {
                    RecurringRecordingList channel = new RecurringRecordingList(document);
                    this.mRecurringChannels = channel.DivideByChannel(RecNameSort.None, RecSizeSort.None);
                    this.mRecurringChannelCount = this.mRecurringChannels.Length;
                    this.mRecurringTrash = new RecurringChannel[this.mRecurringChannelCount];
                    this.mRecurringTrashCount = 0;
                    for (int i = 0; i < this.mRecurringChannelCount; i++)
                    {
                        channel = this.mRecurringChannels[i];
                        this.mChannelsListBox.Items.Add(string.Concat(channel.ChannelName, " | ", channel.Count.ToString()));
                    }
                    this.saveToolStripMenuItem.Enabled = dialog.ReadOnlyChecked;
                    this.saveAsToolStripMenuItem.Enabled = true;
                    this.closeToolStripMenuItem.Enabled = true;

                    this.mChannelSortButton.Enabled = true;
                    this.mChannelUpButton.Enabled = this.mRecurringChannelCount > 1;
                    this.mChannelDownButton.Enabled = this.mRecurringChannelCount > 1;
                    this.mTrashDownButton.Enabled = true;
                }
            }
        }

        private void onFileSaveClick(object sender, EventArgs e)
        {
            RecurringRecordingList list = RecurringRecordingList.MergeLists(this.mRecurringChannels, this.mRecurringChannelCount);
            list.Serialize(this.mFileName);
        }

        private void onFileSaveAsClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML Files (*.xml)|*.xml";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.mFileName = dialog.FileName;
                RecurringRecordingList list = RecurringRecordingList.MergeLists(this.mRecurringChannels, this.mRecurringChannelCount);
                list.Serialize(this.mFileName);
            }
        }

        private void onFileCloseClick(object sender, EventArgs e)
        {
            this.mFileName = null;
            this.mRecurringChannels = null;
            this.mRecurringTrash = null;
            this.mRecurringChannelCount = 0;
            this.mRecurringTrashCount = 0;

            this.mChannelsListBox.Items.Clear();
            this.mShowsListBox.Items.Clear();
            this.mTrashChannelsListBox.Items.Clear();
            this.mTrashShowsListBox.Items.Clear();

            this.mChannelSortButton.Enabled = false;
            this.mChannelUpButton.Enabled = false;
            this.mChannelDownButton.Enabled = false;
            this.mShowUpButton.Enabled = false;
            this.mShowDownButton.Enabled = false;
            this.mTrashUpButton.Enabled = false;
            this.mTrashDownButton.Enabled = false;
        }

        private void onFileExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
