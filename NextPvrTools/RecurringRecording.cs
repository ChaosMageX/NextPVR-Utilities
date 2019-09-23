using System;
using System.Collections.Generic;
using System.Xml;

namespace NextPvrTools
{
    public class RecurringRecording
    {
        private int mOID = 0;
        private string mName = "";

        private string mEPGTitle = "";
        private int mChannelOID = 0;
        private string mChannelName = "";
        private DateTime mStartTime = DateTime.MinValue;
        private DateTime mEndTime = DateTime.MaxValue;
        private int mPrePadding = 0;
        private int mPostPadding = 0;
        private int mQuality = 0;
        private int mKeep = 0;

        private bool mOnlyNewEpisodes = false;
        private DayOfWeek[] mDays = null;

        private List<string> mExtraRules = new List<string>(1);

        public RecurringRecording(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "oid":
                        this.mOID = int.Parse(child.InnerText);
                        break;
                    case "name":
                        this.mName = child.InnerText;
                        break;
                    case "matchrules":
                        if (child.FirstChild.Name == "Rules")
                            this.parseMatchRules(child.FirstChild);
                        break;
                }
            }
        }

        private void parseMatchRules(XmlNode rules)
        {
            int intValue;
            DateTime dtValue;
            foreach (XmlNode child in rules.ChildNodes)
            {
                switch (child.Name)
                {
                    case "EPGTitle":
                        this.mEPGTitle = child.InnerText;
                        break;
                    case "ChannelOID":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mChannelOID = intValue;
                        break;
                    case "ChannelName":
                        this.mChannelName = child.InnerText;
                        break;
                    case "StartTime":
                        if (NextPvrDateTime.TryParse(child.InnerText, out dtValue))
                            this.mStartTime = dtValue;
                        break;
                    case "EndTime":
                        if (NextPvrDateTime.TryParse(child.InnerText, out dtValue))
                            this.mEndTime = dtValue;
                        break;
                    case "PrePadding":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mPrePadding = intValue;
                        break;
                    case "PostPadding":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mPostPadding = intValue;
                        break;
                    case "Quality":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mQuality = intValue;
                        break;
                    case "Keep":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mKeep = intValue;
                        break;
                    case "OnlyNewEpisodes":
                        bool onlyNewEpisodes;
                        if (bool.TryParse(child.InnerText, out onlyNewEpisodes))
                            this.mOnlyNewEpisodes = onlyNewEpisodes;
                        break;
                    case "Days":
                        DayOfWeek day;
                        string[] dayStrs = child.InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        List<DayOfWeek> days = new List<DayOfWeek>(dayStrs.Length);
                        for (int i = 0; i < dayStrs.Length; i++)
                            if (Enum.TryParse<DayOfWeek>(dayStrs[i], true, out day))
                                days.Add(day);
                        if (days.Count > 0)
                            this.mDays = days.ToArray();
                        break;
                    default:
                        this.mExtraRules.Add(child.OuterXml);
                        break;
                }
            }
        }

        public void Serialize(System.IO.TextWriter writer)
        {
            writer.WriteLine("  <recurring>");
            writer.WriteLine(string.Concat("    <oid>", this.mOID.ToString(), "</oid>"));
            writer.WriteLine(string.Concat("    <name>", this.mName, "</name>"));
            writer.WriteLine("    <matchrules>");
            writer.WriteLine("<Rules>");
            writer.WriteLine(string.Concat("  <EPGTitle>", this.mEPGTitle, "</EPGTitle>"));
            writer.WriteLine(string.Concat("  <ChannelOID>", this.mChannelOID.ToString(), "</ChannelOID>"));
            writer.WriteLine(string.Concat("  <ChannelName>", this.mChannelName, "</ChannelName>"));
            writer.WriteLine(string.Concat("  <StartTime>", NextPvrDateTime.Serialize(this.mStartTime), "</StartTime>"));
            writer.WriteLine(string.Concat("  <EndTime>", NextPvrDateTime.Serialize(this.mEndTime), "</EndTime>"));
            writer.WriteLine(string.Concat("  <PrePadding>", this.mPrePadding.ToString(), "</PrePadding>"));
            writer.WriteLine(string.Concat("  <PostPadding>", this.mPostPadding.ToString(), "</PostPadding>"));
            writer.WriteLine(string.Concat("  <Quality>", this.mQuality.ToString(), "</Quality>"));
            writer.WriteLine(string.Concat("  <Keep>", this.mKeep.ToString(), "</Keep>"));
            if (this.mOnlyNewEpisodes)
                writer.WriteLine("  <OnlyNewEpisodes>true</OnlyNewEpisodes");
            if (this.mDays != null)
            {
                string days = this.mDays[0].ToString().ToUpper();
                for (int j = 1; j < this.mDays.Length; j++)
                    days = string.Concat(days, ",", this.mDays[j].ToString().ToUpper());
                writer.WriteLine(string.Concat("  <Days>", days, "</Days>"));
            }
            for (int i = 0; i < this.mExtraRules.Count; i++)
            {
                writer.WriteLine(string.Concat("  ", this.mExtraRules[i]));
            }
            writer.WriteLine("</Rules>");
            writer.WriteLine();
            writer.WriteLine("    </matchrules>");
            writer.WriteLine("  </recurring>");
        }

        public string CreateRuleString()
        {
            if (this.mDays != null)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append(this.mStartTime.ToLocalTime().ToString("h:mm tt"));
                builder.Append(" - ");
                builder.Append(this.mEndTime.ToLocalTime().ToString("h:mm tt"));
                switch (this.mDays.Length)
                {
                    case 1:
                        builder.Append(" (Weekly - ");
                        builder.Append(this.mDays[0].ToString());
                        builder.Append(")");
                        break;
                    case 2:
                        builder.Append(" (Weekends)");
                        break;
                    case 5:
                        builder.Append(" (Monday-Friday)");
                        break;
                    case 7:
                        builder.Append(" (Daily)");
                        break;
                    default:
                        builder.Append(" (");
                        builder.Append(this.mDays[0].ToString());
                        for (int i = 1; i < this.mDays.Length; i++)
                        {
                            builder.Append(", ");
                            builder.Append(this.mDays[i].ToString());
                        }
                        builder.Append(")");
                        break;
                }
                return builder.ToString();
            }
            return this.mOnlyNewEpisodes ? "All New Episodes" : "All Episodes";
        }

        #region Properties
        public int OID { get { return this.mOID; } }
        public string Name { get { return this.mName; } }
        
        public string EPGTitle { get { return this.mEPGTitle; } }
        public int ChannelOID { get { return this.mChannelOID; } }
        public string ChannelName { get { return this.mChannelName; } }
        public DateTime StartTime { get { return this.mStartTime; } }
        public DateTime EndTime { get { return this.mEndTime; } }
        public int PrePadding { get { return this.mPrePadding; } }
        public int PostPadding { get { return this.mPostPadding; } }
        public int Quality { get { return this.mQuality; } }
        public int Keep { get { return this.mKeep; } }
        public bool OnlyNewEpisodes { get { return this.mOnlyNewEpisodes; } }
        public DayOfWeek[] Days {  get { return this.mDays; } }
        #endregion
    }
}
