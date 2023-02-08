using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.EventInfo
{
    internal class InfoPlatoonInfo
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        string output_dir = "";
        string header = "";
        string combined_file_name = "Global_PlatoonInfo";
        string file_format = "csv";

        public InfoPlatoonInfo(string path)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }

            output_dir = logger.GetLogDir() + "\\Event\\PlatoonInfo\\";

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
                    $"PlatoonIDScriptActivate;" +
                    $"State;" +
                    $"GroupPartsNames[0];" +
                    $"GroupPartsNames[1];" +
                    $"GroupPartsNames[2];" +
                    $"GroupPartsNames[3];" +
                    $"GroupPartsNames[4];" +
                    $"GroupPartsNames[5];" +
                    $"GroupPartsNames[6];" +
                    $"GroupPartsNames[7];" +
                    $"GroupPartsNames[8];" +
                    $"GroupPartsNames[9];" +
                    $"GroupPartsNames[10];" +
                    $"GroupPartsNames[11];" +
                    $"GroupPartsNames[12];" +
                    $"GroupPartsNames[13];" +
                    $"GroupPartsNames[14];" +
                    $"GroupPartsNames[15];" +
                    $"GroupPartsNames[16];" +
                    $"GroupPartsNames[17];" +
                    $"GroupPartsNames[18];" +
                    $"GroupPartsNames[19];" +
                    $"GroupPartsNames[20];" +
                    $"GroupPartsNames[21];" +
                    $"GroupPartsNames[22];" +
                    $"GroupPartsNames[23];" +
                    $"GroupPartsNames[24];" +
                    $"GroupPartsNames[25];" +
                    $"GroupPartsNames[26];" +
                    $"GroupPartsNames[27];" +
                    $"GroupPartsNames[28];" +
                    $"GroupPartsNames[29];" +
                    $"GroupPartsNames[30];" +
                    $"GroupPartsNames[31];" +

                    $"\n";
        }

        public void Execute(bool splitByMap)
        {
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

            logger.AddToLog($"Written PlatoonInfo data to {output_dir}");
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

            foreach (MSBE.Event.PlatoonInfo entry in msb.Events.PlatoonInfo)
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
                    $"{entry.PlatoonIDScriptActivate};" +
                    $"{entry.State};" +
                    $"{entry.GroupPartsNames[0]};" +
                    $"{entry.GroupPartsNames[1]};" +
                    $"{entry.GroupPartsNames[2]};" +
                    $"{entry.GroupPartsNames[3]};" +
                    $"{entry.GroupPartsNames[4]};" +
                    $"{entry.GroupPartsNames[5]};" +
                    $"{entry.GroupPartsNames[6]};" +
                    $"{entry.GroupPartsNames[7]};" +
                    $"{entry.GroupPartsNames[8]};" +
                    $"{entry.GroupPartsNames[9]};" +
                    $"{entry.GroupPartsNames[10]};" +
                    $"{entry.GroupPartsNames[11]};" +
                    $"{entry.GroupPartsNames[12]};" +
                    $"{entry.GroupPartsNames[13]};" +
                    $"{entry.GroupPartsNames[14]};" +
                    $"{entry.GroupPartsNames[15]};" +
                    $"{entry.GroupPartsNames[16]};" +
                    $"{entry.GroupPartsNames[17]};" +
                    $"{entry.GroupPartsNames[18]};" +
                    $"{entry.GroupPartsNames[19]};" +
                    $"{entry.GroupPartsNames[20]};" +
                    $"{entry.GroupPartsNames[21]};" +
                    $"{entry.GroupPartsNames[22]};" +
                    $"{entry.GroupPartsNames[23]};" +
                    $"{entry.GroupPartsNames[24]};" +
                    $"{entry.GroupPartsNames[25]};" +
                    $"{entry.GroupPartsNames[26]};" +
                    $"{entry.GroupPartsNames[27]};" +
                    $"{entry.GroupPartsNames[28]};" +
                    $"{entry.GroupPartsNames[29]};" +
                    $"{entry.GroupPartsNames[30]};" +
                    $"{entry.GroupPartsNames[31]};" +

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

