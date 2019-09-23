using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NextPvrTools
{
    public class RecurringRecordingList : IEnumerable<RecurringRecording>
    {
        private List<RecurringRecording> mList;
        private int mChannelOID;
        private string mChannelName;

        private RecurringRecordingList() { }

        private RecurringRecordingList(int capacity)
        {
            this.mList = new List<RecurringRecording>(capacity);
            this.mChannelOID = 0;
            this.mChannelName = "";
        }
        
        public RecurringRecordingList(string fileName)
        {
            this.mList = new List<RecurringRecording>();
            this.mChannelOID = 0;
            this.mChannelName = "";
            if (System.IO.File.Exists(fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                if (!string.Equals(xmlDoc.DocumentElement.Name, "recurrings"))
                    throw new Exception(string.Concat("Error: ", fileName, " is not a NextPVR Recurring Timer List"));
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    this.mList.Add(new RecurringRecording(node));
                }
                this.checkChannelOID();
            }
        }

        public RecurringRecordingList(XmlDocument xmlDoc)
        {
            this.mList = new List<RecurringRecording>();
            this.mChannelOID = 0;
            this.mChannelName = "";
            if (!string.Equals(xmlDoc.DocumentElement.Name, "recurrings"))
                throw new Exception("Error: Xml Document is not a NextPVR Recurring Timer List");
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                this.mList.Add(new RecurringRecording(node));
            }
            this.checkChannelOID();
        }

        private void checkChannelOID()
        {
            if (this.mList.Count > 0)
            {
                this.mChannelOID = this.mList[0].ChannelOID;
                this.mChannelName = this.mList[0].ChannelName;
                if (this.mList.Count > 1)
                {
                    for (int i = 1; i < this.mList.Count; i++)
                    {
                        if (this.mList[i].ChannelOID != this.mChannelOID)
                        {
                            this.mChannelOID = 0;
                            this.mChannelName = "";
                            i = this.mList.Count;
                        }
                    }
                }
            }
        }

        public void Serialize(string fileName)
        {
            System.IO.StreamWriter writer;
            writer = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
            this.Serialize(writer);
            writer.Flush();
            writer.Close();
        }

        public void Serialize(System.IO.TextWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            writer.WriteLine("<recurrings>");
            for (int i = 0; i < this.mList.Count; i++)
            {
                this.mList[i].Serialize(writer);
            }
            writer.WriteLine("</recurrings>");
        }

        private class RecListComparer : IComparer<KeyValuePair<int, RecurringRecordingList>>
        {
            public RecNameSort SortByName;
            public RecSizeSort SortBySize;

            public RecListComparer() { }

            public RecListComparer(RecNameSort sortByName, RecSizeSort sortBySize)
            {
                this.SortByName = sortByName;
                this.SortBySize = sortBySize;
            }

            public int Compare(KeyValuePair<int, RecurringRecordingList> x, KeyValuePair<int, RecurringRecordingList> y)
            {
                int diff;
                switch (SortBySize)
                {
                    case RecSizeSort.None:
                        diff = 0;
                        break;
                    case RecSizeSort.Ascending:
                        diff = x.Value.mList.Count - y.Value.mList.Count;
                        break;
                    case RecSizeSort.Descending:
                        diff = y.Value.mList.Count - x.Value.mList.Count;
                        break;
                    default:
                        diff = 0;
                        break;
                }
                if (diff == 0)
                {
                    switch (SortByName)
                    {
                        case RecNameSort.None:
                            diff = 0;
                            break;
                        case RecNameSort.NameAscending:
                            diff = string.Compare(x.Value.mChannelName, y.Value.mChannelName);
                            break;
                        case RecNameSort.NameDescending:
                            diff = string.Compare(y.Value.mChannelName, x.Value.mChannelName);
                            break;
                        case RecNameSort.OidAscending:
                            diff = x.Key - y.Key;
                            break;
                        case RecNameSort.OidDescending:
                            diff = y.Key - x.Key;
                            break;
                        default:
                            diff = 0;
                            break;
                    }
                }
                return diff;
            }
        }

        private static RecListComparer sRecListComparer = new RecListComparer();

        public RecurringRecordingList[] DivideByChannel(RecNameSort sortByName, RecSizeSort sortBySize)
        {
            RecurringRecordingList[] channels;
            if (this.mChannelOID != 0)
            {
                channels = new RecurringRecordingList[1];
                channels[0] = this;
                return channels;
            }
            int i, channelOID;
            RecurringRecordingList list;
            Dictionary<int, RecurringRecordingList> channelTable = new Dictionary<int, RecurringRecordingList>();
            for (i = 0; i < this.mList.Count; i++)
            {
                channelOID = this.mList[i].ChannelOID;
                if (!channelTable.TryGetValue(channelOID, out list))
                {
                    list = new RecurringRecordingList();
                    list.mChannelOID = channelOID;
                    list.mChannelName = this.mList[i].ChannelName;
                    list.mList = new List<RecurringRecording>();
                    channelTable.Add(channelOID, list);
                }
                list.mList.Add(this.mList[i]);
            }
            channelOID = 0;
            channels = new RecurringRecordingList[channelTable.Count];
            if (sortByName != RecNameSort.None || sortBySize != RecSizeSort.None)
            {
                i = 0;
                KeyValuePair<int, RecurringRecordingList>[] sortedTable = new KeyValuePair<int, RecurringRecordingList>[channelTable.Count];
                foreach (KeyValuePair<int, RecurringRecordingList> channel in channelTable)
                {
                    sortedTable[i++] = channel;
                }
                sRecListComparer.SortByName = sortByName;
                sRecListComparer.SortBySize = sortBySize;
                Array.Sort(sortedTable, sRecListComparer);
                for (i = 0; i < sortedTable.Length; i++)
                {
                    channels[i] = sortedTable[i].Value;
                }
            }
            else
            {
                i = 0;
                foreach (KeyValuePair<int, RecurringRecordingList> channel in channelTable)
                {
                    channels[i++] = channel.Value;
                }
            }
            return channels;
        }

        public void GroupByChannel(RecNameSort sortByName, RecSizeSort sortBySize)
        {
            if (this.mChannelOID == 0)
            {
                int i, channelOID;
                RecurringRecordingList list;
                Dictionary<int, RecurringRecordingList> channelTable = new Dictionary<int, RecurringRecordingList>();
                for (i = 0; i < this.mList.Count; i++)
                {
                    channelOID = this.mList[i].ChannelOID;
                    if (!channelTable.TryGetValue(channelOID, out list))
                    {
                        list = new RecurringRecordingList();
                        list.mChannelOID = channelOID;
                        list.mChannelName = this.mList[i].ChannelName;
                        list.mList = new List<RecurringRecording>();
                        channelTable.Add(channelOID, list);
                    }
                    list.mList.Add(this.mList[i]);
                }
                if (sortByName != RecNameSort.None || sortBySize != RecSizeSort.None)
                {
                    KeyValuePair<int, RecurringRecordingList>[] sortedTable = new KeyValuePair<int, RecurringRecordingList>[channelTable.Count];
                    i = 0;
                    foreach (KeyValuePair<int, RecurringRecordingList> channel in channelTable)
                    {
                        sortedTable[i++] = channel;
                    }
                    sRecListComparer.SortByName = sortByName;
                    sRecListComparer.SortBySize = sortBySize;
                    Array.Sort(sortedTable, sRecListComparer);
                    this.mList.Clear();
                    for (int j = 0; j < sortedTable.Length; j++)
                    {
                        list = sortedTable[j].Value;
                        for (i = 0; i < list.mList.Count; i++)
                            this.mList.Add(list.mList[i]);
                    }
                }
                else
                {
                    this.mList.Clear();
                    foreach (KeyValuePair<int, RecurringRecordingList> channel in channelTable)
                    {
                        list = channel.Value;
                        for (i = 0; i < list.mList.Count; i++)
                            this.mList.Add(list.mList[i]);
                    }
                }
            }
        }

        public static bool SortLists(RecurringRecordingList[] lists, int length, RecNameSort sortByName, RecSizeSort sortBySize)
        {
            if (lists == null || lists.Length == 0 || length < 1)
                return false;
            if (length > lists.Length)
                length = lists.Length;
            if (length == 1)
                return true;
            if (sortByName == RecNameSort.None && sortBySize == RecSizeSort.None)
                return true;
            KeyValuePair<int, RecurringRecordingList>[] sortedTable = new KeyValuePair<int, RecurringRecordingList>[length];
            for (int i = 0; i < length; i++)
            {
                sortedTable[i] = new KeyValuePair<int, RecurringRecordingList>(lists[i].ChannelOID, lists[i]);
            }
            sRecListComparer.SortByName = sortByName;
            sRecListComparer.SortBySize = sortBySize;
            Array.Sort(sortedTable, sRecListComparer);
            for (int i = 0; i < length; i++)
            {
                lists[i] = sortedTable[i].Value;
            }
            return true;
        }

        public static RecurringRecordingList MergeLists(RecurringRecordingList[] lists, int length)
        {
            if (lists == null || lists.Length == 0 || length < 1)
                return null;
            if (length > lists.Length)
                length = lists.Length;
            if (length == 1)
                return lists[0];
            int count = 0;
            for (int i = 0; i < length; i++)
                count += lists[i].mList.Count;
            RecurringRecordingList list = new RecurringRecordingList(count);
            for (int j = 0; j < length; j++)
            {
                count = lists[j].mList.Count;
                for (int k = 0; k < count; k++)
                    list.mList.Add(lists[j].mList[k]);
            }
            list.checkChannelOID();
            return list;
        }

        #region Properties
        public IEnumerator<RecurringRecording> GetEnumerator()
        {
            return this.mList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.mList.GetEnumerator();
        }

        public int ChannelOID { get { return this.mChannelOID; } }
        public string ChannelName { get { return this.mChannelName; } }
        
        public int Count { get { return this.mList.Count; } }
        public RecurringRecording this[int index]
        {
            get
            {
                return this.mList[index];
            }
            set
            {
                this.mList[index] = value ?? throw new ArgumentNullException("value");
                this.checkChannelOID();
            }
        }
        #endregion

        public void Add(RecurringRecording item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            this.mList.Add(item);
            if (this.mList.Count == 1)
            {
                this.mChannelOID = item.ChannelOID;
                this.mChannelName = item.ChannelName;
            }
            else if (this.mChannelOID != item.ChannelOID)
            {
                this.mChannelOID = 0;
                this.mChannelName = "";
            }
        }

        public void Insert(int index, RecurringRecording item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            this.mList.Insert(index, item);
            if (this.mList.Count == 1)
            {
                this.mChannelOID = item.ChannelOID;
                this.mChannelName = item.ChannelName;
            }
            else if (this.mChannelOID != item.ChannelOID)
            {
                this.mChannelOID = 0;
                this.mChannelName = "";
            }
        }

        public void Remove(RecurringRecording item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            this.mList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.mList.RemoveAt(index);
        }
    }
}
