using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NextPvrTools
{
    public class Recording
    {
        private int mOID = 0;
        private string mName = "";
        private string mChannel = "";
        private string mFileName = "";
        private string mStatus = "";
        private DateTime mStartTime = DateTime.MinValue;
        private DateTime mEndTime = DateTime.MaxValue;
        private RecordingEvent mEvent;

        private List<string> mExtraFields = new List<string>(1);

        public Recording(XmlNode node)
        {
            int intValue;
            DateTime dtValue;
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "oid":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mOID = intValue;
                        break;
                    case "name":
                        this.mName = child.InnerText;
                        break;
                    case "channel":
                        this.mChannel = child.InnerText;
                        break;
                    case "filename":
                        this.mFileName = child.InnerText;
                        break;
                    case "status":
                        this.mStatus = child.InnerText;
                        break;
                    case "startTime":
                        if (NextPvrDateTime.TryParse(child.InnerText, out dtValue))
                            this.mStartTime = dtValue;
                        break;
                    case "endTime":
                        if (NextPvrDateTime.TryParse(child.InnerText, out dtValue))
                            this.mEndTime = dtValue;
                        break;
                    case "Event":
                        this.mEvent = new RecordingEvent(child);
                        break;
                    default:
                        this.mExtraFields.Add(child.OuterXml);
                        break;
                }
            }
        }

        public void Serialize(System.IO.TextWriter writer)
        {
            writer.WriteLine("  <recording>");
            writer.WriteLine(string.Concat("    <oid>", this.mOID.ToString(), "</oid>"));
            writer.WriteLine(string.Concat("    <name>", this.mName, "</name>"));
            writer.WriteLine(string.Concat("    <channel>", this.mChannel, "</channel>"));
            writer.WriteLine(string.Concat("    <filename>", this.mFileName, "</filename>"));
            writer.WriteLine(string.Concat("    <status>", this.mStatus, "</status>"));
            writer.WriteLine(string.Concat("    <startTime>", NextPvrDateTime.Serialize(this.mStartTime), "</startTime>"));
            writer.WriteLine(string.Concat("    <endTime>", NextPvrDateTime.Serialize(this.mEndTime), "</endTime>"));
            if (this.mExtraFields.Count > 0)
            {
                for (int i = 0; i < this.mExtraFields.Count; i++)
                    writer.WriteLine(string.Concat("  ", this.mExtraFields[i]));
            }
            if (this.mEvent != null)
                this.mEvent.Serialize(writer);
            writer.WriteLine("  </recording>");
        }

        #region Properties
        public int OID { get { return this.mOID; } }
        public string Name { get { return this.mName; } }
        public string Channel { get { return this.mChannel; } }
        public string FileName { get { return this.mFileName; } }
        public string Status { get { return this.mStatus; } }
        public DateTime StartTime { get { return this.mStartTime; } }
        public DateTime EndTime { get { return this.mEndTime; } }
        public RecordingEvent Event { get { return this.mEvent; } }
        #endregion
    }
}
