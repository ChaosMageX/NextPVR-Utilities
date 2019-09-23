using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NextPvrTools
{
    public class RecordingEvent
    {
        private int mOID = 0;
        private string mTitle = "";
        private string mSubTitle = "";
        private string mDescription = "";
        private int mChannelOID = 0;
        private DateTime mStartTime = DateTime.MinValue;
        private DateTime mEndTime = DateTime.MinValue;
        private bool mFirstRun = false;

        private DateTime mOriginalAirDate = DateTime.MinValue;
        private string mRating = null;
        private string mAudio = null;
        private string mAspect = null;
        private string mQuality = null;

        private List<string> mExtraFields = new List<string>(1);

        private List<string> mGenres = null;
        private List<string> mCast = null;
        private List<string> mCrew = null;

        private string mUniqueID = "";

        public RecordingEvent(XmlNode node)
        {
            int intValue;
            DateTime dtValue;
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "OID":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mOID = intValue;
                        break;
                    case "Title":
                        this.mTitle = child.InnerText;
                        break;
                    case "SubTitle":
                        this.mSubTitle = child.InnerText;
                        break;
                    case "Description":
                        this.mDescription = child.InnerText;
                        break;
                    case "ChannelOID":
                        if (int.TryParse(child.InnerText, out intValue))
                            this.mChannelOID = intValue;
                        break;
                    case "StartTime":
                        if (NextPvrDateTime.TryParse(child.InnerText, out dtValue))
                            this.mStartTime = dtValue;
                        break;
                    case "EndTime":
                        if (NextPvrDateTime.TryParse(child.InnerText, out dtValue))
                            this.mEndTime = dtValue;
                        break;
                    case "FirstRun":
                        bool firstRun;
                        if (bool.TryParse(child.InnerText, out firstRun))
                            this.mFirstRun = firstRun;
                        break;
                    case "OriginalAirDate":
                        if (NextPvrDateTime.TryParse(child.InnerText, out dtValue))
                            this.mOriginalAirDate = dtValue;
                        break;
                    case "Rating":
                        this.mRating = child.InnerText;
                        break;
                    case "Audio":
                        this.mAudio = child.InnerText;
                        break;
                    case "Aspect":
                        this.mAspect = child.InnerText;
                        break;
                    case "Quality":
                        this.mQuality = child.InnerText;
                        break;
                    case "Genres":
                        this.mGenres = new List<string>(child.ChildNodes.Count);
                        foreach (XmlNode genre in child.ChildNodes)
                            this.mGenres.Add(genre.InnerText);
                        break;
                    case "Cast":
                        this.mCast = new List<string>(child.ChildNodes.Count);
                        foreach (XmlNode member in child.ChildNodes)
                            this.mCast.Add(member.InnerText);
                        break;
                    case "Crew":
                        this.mCrew = new List<string>(child.ChildNodes.Count);
                        foreach (XmlNode member in child.ChildNodes)
                            this.mCrew.Add(member.InnerText);
                        break;
                    case "UniqueID":
                        this.mUniqueID = child.InnerText;
                        break;
                    default:
                        this.mExtraFields.Add(child.OuterXml);
                        break;
                }
            }
        }
        
        public void Serialize(System.IO.TextWriter writer)
        {
            int i;
            writer.WriteLine("<Event>");
            writer.WriteLine(string.Concat("  <OID>", this.mOID.ToString(), "</OID>"));
            writer.WriteLine(string.Concat("  <Title>", this.mTitle, "</Title>"));
            writer.WriteLine(string.Concat("  <SubTitle>", this.mSubTitle, "</SubTitle>"));
            writer.WriteLine(string.Concat("  <Description>", this.mDescription, "</Description>"));
            writer.WriteLine(string.Concat("  <ChannelOID>", this.mChannelOID.ToString(), "</ChannelOID>"));
            writer.WriteLine(string.Concat("  <StartTime>", NextPvrDateTime.Serialize(this.mStartTime), "</StartTime>"));
            writer.WriteLine(string.Concat("  <EndTime>", NextPvrDateTime.Serialize(this.mEndTime), "</EndTime>"));
            writer.WriteLine(string.Concat("  <FirstRun>", this.mFirstRun ? "true" : "false", "</FirstRun>"));

            if (this.mOriginalAirDate != DateTime.MinValue)
                writer.WriteLine(string.Concat("  <OriginalAirDate>", NextPvrDateTime.Serialize(this.mOriginalAirDate, true), "</OriginalAirDate>"));
            if (this.mRating != null)
                writer.WriteLine(string.Concat("  <Rating>", this.mRating, "</Rating>"));
            if (this.mAudio != null)
                writer.WriteLine(string.Concat("  <Audio>", this.mAudio, "</Audio>"));
            if (this.mAspect != null)
                writer.WriteLine(string.Concat("  <Aspect>", this.mAspect, "</Aspect>"));
            if (this.mQuality != null)
                writer.WriteLine(string.Concat("  <Quality>", this.mQuality, "</Quality>"));

            if (this.mExtraFields.Count > 0)
            {
                for (i = 0; i < this.mExtraFields.Count; i++)
                    writer.WriteLine(string.Concat("  ", this.mExtraFields[i]));
            }
            if (this.mGenres != null)
            {
                writer.WriteLine("  <Genres>");
                for (i = 0; i < this.mGenres.Count; i++)
                    writer.WriteLine(string.Concat("    <Genre>", this.mGenres[i], "</Genre>"));
                writer.WriteLine("  </Genres>");
            }
            if (this.mCast != null)
            {
                writer.WriteLine("  <Cast>");
                for (i = 0; i < this.mCast.Count; i++)
                    writer.WriteLine(string.Concat("    <Member>", this.mCast[i], "</Member>"));
                writer.WriteLine("  </Cast>");
            }
            if (this.mCrew != null)
            {
                writer.WriteLine("  <Crew>");
                for (i = 0; i < this.mCrew.Count; i++)
                    writer.WriteLine(string.Concat("    <Member>", this.mCrew[i], "</Member>"));
                writer.WriteLine("  </Crew>");
            }
            writer.WriteLine(string.Concat("  <UniqueID>", this.mUniqueID, "</UniqueID>"));
            writer.WriteLine("</Event>");
        }

        #region Properties
        public int OID { get { return this.mOID; } }
        public string Title { get { return this.mTitle; } }
        public string SubTitle { get { return this.mSubTitle; } }
        public string Description { get { return this.mDescription; } }
        public int ChannelOID { get { return this.mChannelOID; } }
        public DateTime StartTime { get { return this.mStartTime; } }
        public DateTime EndTime { get { return this.mEndTime; } }
        public bool FirstRun { get { return this.mFirstRun; } }
        public DateTime OriginalAirDate { get { return this.mOriginalAirDate; } }
        public string Rating { get { return this.mRating; } }
        public string Audio { get { return this.mAudio; } }
        public string Aspect { get { return this.mAspect; } }
        public string Quality { get { return this.mQuality; } }
        public string[] Genres { get { return this.mGenres == null ? null : this.mGenres.ToArray(); } }
        public string[] Cast { get { return this.mCast == null ? null : this.mCast.ToArray(); } }
        public string[] Crew { get { return this.mCrew == null ? null : this.mCrew.ToArray(); } }
        public string UniqueID { get { return this.mUniqueID; } }
        #endregion
    }
}
