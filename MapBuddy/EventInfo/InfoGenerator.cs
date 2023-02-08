using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.EventInfo
{
    internal class InfoGenerator
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        string output_dir = "";
        string header = "";
        string combined_file_name = "Global_Generator";
        string file_format = "csv";

        public InfoGenerator(string path)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }

            output_dir = logger.GetLogDir() + "\\Event\\Generator\\";

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
                    $"MaxNum;" +
                    $"GenType;" +
                    $"LimitNum;" +
                    $"MinGenNum;" +
                    $"MaxGenNum;" +
                    $"MinInterval;" +
                    $"MaxInterval;" +
                    $"InitialSpawnCount;" +
                    $"UnkT14;" +
                    $"UnkT18;" +
                    $"SpawnRegionNames[0];" +
                    $"SpawnRegionNames[1];" +
                    $"SpawnRegionNames[2];" +
                    $"SpawnRegionNames[3];" +
                    $"SpawnRegionNames[4];" +
                    $"SpawnRegionNames[5];" +
                    $"SpawnRegionNames[6];" +
                    $"SpawnRegionNames[7];" +
                    $"SpawnPartNames[0];" +
                    $"SpawnPartNames[1];" +
                    $"SpawnPartNames[2];" +
                    $"SpawnPartNames[3];" +
                    $"SpawnPartNames[4];" +
                    $"SpawnPartNames[5];" +
                    $"SpawnPartNames[6];" +
                    $"SpawnPartNames[7];" +
                    $"SpawnPartNames[8];" +
                    $"SpawnPartNames[9];" +
                    $"SpawnPartNames[10];" +
                    $"SpawnPartNames[11];" +
                    $"SpawnPartNames[12];" +
                    $"SpawnPartNames[13];" +
                    $"SpawnPartNames[14];" +
                    $"SpawnPartNames[15];" +
                    $"SpawnPartNames[16];" +
                    $"SpawnPartNames[17];" +
                    $"SpawnPartNames[18];" +
                    $"SpawnPartNames[19];" +
                    $"SpawnPartNames[20];" +
                    $"SpawnPartNames[21];" +
                    $"SpawnPartNames[22];" +
                    $"SpawnPartNames[23];" +
                    $"SpawnPartNames[24];" +
                    $"SpawnPartNames[25];" +
                    $"SpawnPartNames[26];" +
                    $"SpawnPartNames[27];" +
                    $"SpawnPartNames[28];" +
                    $"SpawnPartNames[29];" +
                    $"SpawnPartNames[30];" +
                    $"SpawnPartNames[31];" +

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

            logger.AddToLog($"Written Generator data to {output_dir}");
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

            foreach (MSBE.Event.Generator entry in msb.Events.Generators)
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
                    $"{entry.MaxNum};" +
                    $"{entry.GenType};" +
                    $"{entry.LimitNum};" +
                    $"{entry.MinGenNum};" +
                    $"{entry.MaxGenNum};" +
                    $"{entry.MinInterval};" +
                    $"{entry.MaxInterval};" +
                    $"{entry.InitialSpawnCount};" +
                    $"{entry.UnkT14};" +
                    $"{entry.UnkT18};" +
                    $"{entry.SpawnRegionNames[0]};" +
                    $"{entry.SpawnRegionNames[1]};" +
                    $"{entry.SpawnRegionNames[2]};" +
                    $"{entry.SpawnRegionNames[3]};" +
                    $"{entry.SpawnRegionNames[4]};" +
                    $"{entry.SpawnRegionNames[5]};" +
                    $"{entry.SpawnRegionNames[6]};" +
                    $"{entry.SpawnRegionNames[7]};" +
                    $"{entry.SpawnPartNames[0]};" +
                    $"{entry.SpawnPartNames[1]};" +
                    $"{entry.SpawnPartNames[2]};" +
                    $"{entry.SpawnPartNames[3]};" +
                    $"{entry.SpawnPartNames[4]};" +
                    $"{entry.SpawnPartNames[5]};" +
                    $"{entry.SpawnPartNames[6]};" +
                    $"{entry.SpawnPartNames[7]};" +
                    $"{entry.SpawnPartNames[8]};" +
                    $"{entry.SpawnPartNames[9]};" +
                    $"{entry.SpawnPartNames[10]};" +
                    $"{entry.SpawnPartNames[11]};" +
                    $"{entry.SpawnPartNames[12]};" +
                    $"{entry.SpawnPartNames[13]};" +
                    $"{entry.SpawnPartNames[14]};" +
                    $"{entry.SpawnPartNames[15]};" +
                    $"{entry.SpawnPartNames[16]};" +
                    $"{entry.SpawnPartNames[17]};" +
                    $"{entry.SpawnPartNames[18]};" +
                    $"{entry.SpawnPartNames[19]};" +
                    $"{entry.SpawnPartNames[20]};" +
                    $"{entry.SpawnPartNames[21]};" +
                    $"{entry.SpawnPartNames[22]};" +
                    $"{entry.SpawnPartNames[23]};" +
                    $"{entry.SpawnPartNames[24]};" +
                    $"{entry.SpawnPartNames[25]};" +
                    $"{entry.SpawnPartNames[26]};" +
                    $"{entry.SpawnPartNames[27]};" +
                    $"{entry.SpawnPartNames[28]};" +
                    $"{entry.SpawnPartNames[29]};" +
                    $"{entry.SpawnPartNames[30]};" +
                    $"{entry.SpawnPartNames[31]};" +

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
