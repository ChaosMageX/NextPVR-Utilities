using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NextPvrTools;

namespace NextPvrRecordingXmlSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "";
            if (args.Length == 1 && File.Exists(args[0]))
            {
                filePath = args[0];
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (File.Exists(args[i]))
                        filePath = args[i];
                }
            }
            if (filePath != "")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                string outputDir, outputFile;
                StreamWriter channelStats, showStats;
                string rootName = xmlDoc.DocumentElement.Name;
                if (string.Equals(rootName, "recordings"))
                {
                    Console.WriteLine("NextPVR Recording List Xml File Recognized.");
                    string outputPath;
                    RecordingList show;
                    RecordingList[] shows;
                    RecordingList.Channel channel;
                    outputDir = CreateOutputDirectory(filePath);
                    channelStats = new StreamWriter(Path.Combine(outputDir, "ChannelStats.txt"));
                    RecordingList list = new RecordingList(xmlDoc);
                    Console.WriteLine(string.Concat(list.Count.ToString(), " recordings found."));
                    RecordingList.Channel[] channels = list.DivideByChannelAndShow();
                    Console.WriteLine(string.Concat(channels.Length.ToString(), " unique channels found."));
                    for (int i = 0; i < channels.Length; i++)
                    {
                        channel = channels[i];
                        shows = channel.Shows;
                        channelStats.Write(channel.ChannelName);
                        channelStats.Write(" | ");
                        channelStats.WriteLine(shows.Length.ToString());
                        outputPath = Path.Combine(outputDir, channel.ChannelName);
                        if (!Directory.Exists(outputPath))
                            Directory.CreateDirectory(outputPath);
                        outputFile = Path.Combine(outputDir, string.Concat(channel.ChannelName, "_ShowStats.txt"));
                        showStats = new StreamWriter(outputFile);
                        for (int j = 0; j < shows.Length; j++)
                        {
                            show = shows[j];
                            outputFile = Path.Combine(outputPath, string.Concat(show.ShowName, ".xml"));
                            show.Serialize(outputFile);
                            showStats.Write(show.ShowName);
                            showStats.Write(" | ");
                            showStats.WriteLine(show.Count.ToString());
                        }
                        showStats.Flush();
                        showStats.Close();
                    }
                    channelStats.Flush();
                    channelStats.Close();
                }
                else if (string.Equals(rootName, "recurrings"))
                {
                    Console.WriteLine("NextPVR Recurring Time List Xml File Recognized.");
                    outputDir = CreateOutputDirectory(filePath);
                    channelStats = new StreamWriter(Path.Combine(outputDir, "ChannelStats.txt"));
                    RecurringRecordingList list = new RecurringRecordingList(xmlDoc);
                    Console.WriteLine(string.Concat(list.Count.ToString(), " recurring timers found."));
                    RecurringRecordingList[] channels = list.DivideByChannel(RecNameSort.NameAscending, RecSizeSort.Descending);
                    Console.WriteLine(string.Concat(channels.Length.ToString(), " unique channels found."));
                    for (int i = 0; i < channels.Length; i++)
                    {
                        list = channels[i];
                        outputFile = string.Concat("recurring_", list.ChannelName);
                        outputFile = Path.Combine(outputDir, outputFile);
                        list.Serialize(string.Concat(outputFile, ".xml"));
                        channelStats.Write(list.ChannelName);
                        channelStats.Write(" | ");
                        channelStats.WriteLine(list.Count.ToString());
                        showStats = new StreamWriter(string.Concat(outputFile, "_ShowStats.txt"));
                        foreach (RecurringRecording rec in list)
                        {
                            showStats.Write(rec.Name);
                            showStats.Write(" | ");
                            //showStats.Write(rec.EPGTitle);
                            //showStats.Write(" | ");
                            showStats.WriteLine(rec.CreateRuleString());
                        }
                        showStats.Flush();
                        showStats.Close();
                    }
                    outputFile = Path.Combine(outputDir, DateTime.Now.ToString("'recurring_'yyyy-MM-dd_HH'h'mm_ss"));
                    list = RecurringRecordingList.MergeLists(channels, channels.Length);
                    list.Serialize(string.Concat(outputFile, ".xml"));
                    channelStats.Flush();
                    channelStats.Close();
                }
            }
        }

        private static string CreateOutputDirectory(string path)
        {
            path = Path.GetFullPath(path);
            path = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
