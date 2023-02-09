using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.EventInfo
{
    internal class InfoPatrolInfo
    {
        Logger logger = new Logger();

        string output_dir = "";
        string header = "";
        string combined_file_name = "Global_PatrolInfo";
        string file_format = "csv";

        public InfoPatrolInfo(string path, Dictionary<string, string> mapDict, bool splitByMap)
        {
            output_dir = logger.GetLogDir() + "\\Event\\PatrolInfo\\";

            header = $"EventID;" +
                    $"PartName;" +
                    $"RegionName;" +
                    $"EntityID;" +
                    $"Unk14;" +
                    $"MapID;" +
                    $"UnkE0C;" +
                    $"UnkS04;" +
                    $"UnkS08;" +
                    $"UnkS0C;" +
                    $"Name;" +
                    $"PatrolType;" +
                    $"WalkRegionNames[0];" +
                    $"WalkRegionNames[1];" +
                    $"WalkRegionNames[2];" +
                    $"WalkRegionNames[3];" +
                    $"WalkRegionNames[4];" +
                    $"WalkRegionNames[5];" +
                    $"WalkRegionNames[6];" +
                    $"WalkRegionNames[7];" +
                    $"WalkRegionNames[8];" +
                    $"WalkRegionNames[9];" +
                    $"WalkRegionNames[10];" +
                    $"WalkRegionNames[11];" +
                    $"WalkRegionNames[12];" +
                    $"WalkRegionNames[13];" +
                    $"WalkRegionNames[14];" +
                    $"WalkRegionNames[15];" +
                    $"WalkRegionNames[16];" +
                    $"WalkRegionNames[17];" +
                    $"WalkRegionNames[18];" +
                    $"WalkRegionNames[19];" +
                    $"WalkRegionNames[20];" +
                    $"WalkRegionNames[21];" +
                    $"WalkRegionNames[22];" +
                    $"WalkRegionNames[23];" +
                    $"WalkRegionNames[24];" +
                    $"WalkRegionNames[25];" +
                    $"WalkRegionNames[26];" +
                    $"WalkRegionNames[27];" +
                    $"WalkRegionNames[28];" +
                    $"WalkRegionNames[29];" +
                    $"WalkRegionNames[30];" +
                    $"WalkRegionNames[31];" +
                    $"WalkRegionNames[32];" +
                    $"WalkRegionNames[33];" +
                    $"WalkRegionNames[34];" +
                    $"WalkRegionNames[35];" +
                    $"WalkRegionNames[36];" +
                    $"WalkRegionNames[37];" +
                    $"WalkRegionNames[38];" +
                    $"WalkRegionNames[39];" +
                    $"WalkRegionNames[40];" +
                    $"WalkRegionNames[41];" +
                    $"WalkRegionNames[42];" +
                    $"WalkRegionNames[43];" +
                    $"WalkRegionNames[44];" +
                    $"WalkRegionNames[45];" +
                    $"WalkRegionNames[46];" +
                    $"WalkRegionNames[47];" +
                    $"WalkRegionNames[48];" +
                    $"WalkRegionNames[49];" +
                    $"WalkRegionNames[50];" +
                    $"WalkRegionNames[51];" +
                    $"WalkRegionNames[52];" +
                    $"WalkRegionNames[53];" +
                    $"WalkRegionNames[54];" +
                    $"WalkRegionNames[55];" +
                    $"WalkRegionNames[56];" +
                    $"WalkRegionNames[57];" +
                    $"WalkRegionNames[58];" +
                    $"WalkRegionNames[59];" +
                    $"WalkRegionNames[60];" +
                    $"WalkRegionNames[61];" +
                    $"WalkRegionNames[62];" +
                    $"WalkRegionNames[63];" +

                    $"\n";
 
            bool exists = Directory.Exists(output_dir);

            if (!exists)
            {
                Directory.CreateDirectory(output_dir);
            }

            // Combined: write header here
            if (!splitByMap)
            {
                header = $"MapID;" + header; // Add MapID column for combined file

                File.WriteAllText(output_dir + $"{combined_file_name}.{file_format}", header);
            }

            foreach (KeyValuePair<string, string> entry in mapDict)
            {
                string map_name = entry.Key.Replace(".msb", "");
                string map_path = entry.Value;

                string[] map_indexes = map_name.Replace("m", "").Split("_");

                MSBE msb = MSBE.Read(map_path);

                WriteCSV(msb, map_name, splitByMap);
            }

            logger.AddToLog($"Written PatrolInfo data to {output_dir}");
            logger.WriteLog();
        }

        public void WriteCSV(MSBE msb, string map_name, bool outputByMap)
        {

            string text = "";

            // By Map: write header here
            if (outputByMap)
            {
                File.WriteAllText(output_dir + $"{map_name}.{file_format}", header);
            }

            foreach (MSBE.Event.PatrolInfo entry in msb.Events.PatrolInfo)
            {
                string line = "";

                // Combined: add MapID column
                if (!outputByMap)
                {
                    line = line +
                        $"{map_name};";
                }

                line = line +
                    $"{entry.EventID};" +
                    $"{entry.PartName};" +
                    $"{entry.RegionName};" +
                    $"{entry.EntityID};" +
                    $"{entry.Unk14};" +
                    $"{entry.MapID};" +
                    $"{entry.UnkE0C};" +
                    $"{entry.UnkS04};" +
                    $"{entry.UnkS08};" +
                    $"{entry.UnkS0C};" +
                    $"{entry.Name};" +
                    $"{entry.PatrolType};" +
                    $"{entry.WalkRegionNames[0]};" +
                    $"{entry.WalkRegionNames[1]};" +
                    $"{entry.WalkRegionNames[2]};" +
                    $"{entry.WalkRegionNames[3]};" +
                    $"{entry.WalkRegionNames[4]};" +
                    $"{entry.WalkRegionNames[5]};" +
                    $"{entry.WalkRegionNames[6]};" +
                    $"{entry.WalkRegionNames[7]};" +
                    $"{entry.WalkRegionNames[8]};" +
                    $"{entry.WalkRegionNames[9]};" +
                    $"{entry.WalkRegionNames[10]};" +
                    $"{entry.WalkRegionNames[11]};" +
                    $"{entry.WalkRegionNames[12]};" +
                    $"{entry.WalkRegionNames[13]};" +
                    $"{entry.WalkRegionNames[14]};" +
                    $"{entry.WalkRegionNames[15]};" +
                    $"{entry.WalkRegionNames[16]};" +
                    $"{entry.WalkRegionNames[17]};" +
                    $"{entry.WalkRegionNames[18]};" +
                    $"{entry.WalkRegionNames[19]};" +
                    $"{entry.WalkRegionNames[20]};" +
                    $"{entry.WalkRegionNames[21]};" +
                    $"{entry.WalkRegionNames[22]};" +
                    $"{entry.WalkRegionNames[23]};" +
                    $"{entry.WalkRegionNames[24]};" +
                    $"{entry.WalkRegionNames[25]};" +
                    $"{entry.WalkRegionNames[26]};" +
                    $"{entry.WalkRegionNames[27]};" +
                    $"{entry.WalkRegionNames[28]};" +
                    $"{entry.WalkRegionNames[29]};" +
                    $"{entry.WalkRegionNames[30]};" +
                    $"{entry.WalkRegionNames[31]};" +
                    $"{entry.WalkRegionNames[32]};" +
                    $"{entry.WalkRegionNames[33]};" +
                    $"{entry.WalkRegionNames[34]};" +
                    $"{entry.WalkRegionNames[35]};" +
                    $"{entry.WalkRegionNames[36]};" +
                    $"{entry.WalkRegionNames[37]};" +
                    $"{entry.WalkRegionNames[38]};" +
                    $"{entry.WalkRegionNames[39]};" +
                    $"{entry.WalkRegionNames[40]};" +
                    $"{entry.WalkRegionNames[41]};" +
                    $"{entry.WalkRegionNames[42]};" +
                    $"{entry.WalkRegionNames[43]};" +
                    $"{entry.WalkRegionNames[44]};" +
                    $"{entry.WalkRegionNames[45]};" +
                    $"{entry.WalkRegionNames[46]};" +
                    $"{entry.WalkRegionNames[47]};" +
                    $"{entry.WalkRegionNames[48]};" +
                    $"{entry.WalkRegionNames[49]};" +
                    $"{entry.WalkRegionNames[50]};" +
                    $"{entry.WalkRegionNames[51]};" +
                    $"{entry.WalkRegionNames[52]};" +
                    $"{entry.WalkRegionNames[53]};" +
                    $"{entry.WalkRegionNames[54]};" +
                    $"{entry.WalkRegionNames[55]};" +
                    $"{entry.WalkRegionNames[56]};" +
                    $"{entry.WalkRegionNames[57]};" +
                    $"{entry.WalkRegionNames[58]};" +
                    $"{entry.WalkRegionNames[59]};" +
                    $"{entry.WalkRegionNames[60]};" +
                    $"{entry.WalkRegionNames[61]};" +
                    $"{entry.WalkRegionNames[62]};" +
                    $"{entry.WalkRegionNames[63]};" +

                    $"\n";

                text = text + line;
            }

            // By Map
            if (outputByMap)
            {
                File.AppendAllText(output_dir + $"{map_name}.{file_format}", text);
            }
            // Combined
            else
            {
                File.AppendAllText(output_dir + $"{combined_file_name}.{file_format}", text);
            }
        }
    }
}

