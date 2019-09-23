using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NextPvrTools
{
    public class RecordingList : IEnumerable<Recording>
    {
        private List<Recording> mList;
        private string mShowName;

        private RecordingList()
        {
            this.mList = new List<Recording>();
        }

        public RecordingList(string fileName)
        {
            this.mList = new List<Recording>();
            this.mShowName = "";
            if (File.Exists(fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                if (!string.Equals(xmlDoc.DocumentElement.Name, "recordings"))
                    throw new Exception(string.Concat("Error: ", fileName, " is not a NextPVR Recording List"));
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    this.mList.Add(new Recording(node));
                }
            }
        }

        public RecordingList(XmlDocument xmlDoc)
        {
            this.mList = new List<Recording>();
            this.mShowName = "";
            if (!string.Equals(xmlDoc.DocumentElement.Name, "recordings"))
                throw new Exception("Error: Xml Document is not a NextPVR Recording List");
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                this.mList.Add(new Recording(node));
            }
        }

        public void Serialize(string fileName)
        {
            StreamWriter writer;
            writer = new StreamWriter(fileName, false, Encoding.UTF8);
            this.Serialize(writer);
            writer.Flush();
            writer.Close();
        }

        public void Serialize(System.IO.TextWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            writer.WriteLine("<recordings>");
            for (int i = 0; i < this.mList.Count; i++)
            {
                this.mList[i].Serialize(writer);
            }
            writer.WriteLine("</recordings>");
        }

        public static string GetFolder(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directory))
            {
                return "";
            }
            char ch;
            for (int i = directory.Length - 1; i >= 0; i--)
            {
                ch = directory[i];
                if (ch == Path.DirectorySeparatorChar ||
                    ch == Path.AltDirectorySeparatorChar ||
                    ch == Path.VolumeSeparatorChar)
                {
                    return directory.Substring(i + 1, directory.Length - i - 1);
                }
            }
            return "";
        }

        public class Channel
        {
            private int mChannelOID;
            private string mChannelName;
            private RecordingList[] mShows;

            public Channel(int channelOID, string channelName, RecordingList[] shows)
            {
                this.mChannelOID = channelOID;
                this.mChannelName = channelName;
                this.mShows = shows;
            }

            public int ChannelOID { get { return this.mChannelOID; } }
            public string ChannelName { get { return this.mChannelName; } }
            public RecordingList[] Shows { get { return this.mShows; } }
        }

        public Channel[] DivideByChannelAndShow()
        {
            string channelName, showName;
            Recording rec;
            RecordingList recList;
            Dictionary<string, RecordingList> recTable;
            Dictionary<string, Dictionary<string, RecordingList>> table = new Dictionary<string, Dictionary<string, RecordingList>>();
            for (int i = 0; i < this.mList.Count; i++)
            {
                rec = this.mList[i];
                channelName = rec.Channel;
                if (!table.TryGetValue(channelName, out recTable))
                {
                    recTable = new Dictionary<string, RecordingList>();
                    table.Add(channelName, recTable);
                }
                if (string.IsNullOrEmpty(rec.FileName))
                    showName = "NULL";
                else
                    showName = GetFolder(rec.FileName);
                if (!recTable.TryGetValue(showName, out recList))
                {
                    recList = new RecordingList();
                    recList.mShowName = showName;
                    recTable.Add(showName, recList);
                }
                recList.mList.Add(rec);
            }
            int j = 0;
            RecordingList[] shows;
            Channel[] result = new Channel[table.Count];
            foreach (KeyValuePair<string, Dictionary<string, RecordingList>> entry in table)
            {
                int k = 0;
                recTable = entry.Value;
                shows = new RecordingList[recTable.Count];
                foreach (KeyValuePair<string, RecordingList> r in recTable)
                {
                    shows[k++] = r.Value;
                }
                // In case of Manual Recordings, which don't have an Event
                rec = shows[--k][0];
                while (k > 0 && rec.Event == null)
                {
                    rec = shows[--k][0];
                }
                result[j++] = new Channel(rec.Event == null ? 0 : rec.Event.ChannelOID, rec.Channel, shows);
            }
            return result;
        }

        #region Properties
        public IEnumerator<Recording> GetEnumerator()
        {
            return this.mList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.mList.GetEnumerator();
        }

        public string ShowName { get { return this.mShowName; } }

        public int Count { get { return this.mList.Count; } }
        public Recording this[int index] { get { return this.mList[index]; } }
        #endregion
    }
}
