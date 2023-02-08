using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.RegionInfo
{
    internal class InfoMessage
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        string output_dir = "";
        string header = "";
        string propertyName = "Message";
        string combined_file_name = "";
        string file_format = "csv";

        public InfoMessage(string path)
        {
            combined_file_name = $"Global_{propertyName}";

            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }

            output_dir = logger.GetLogDir() + $"\\Region\\{propertyName}\\";

            header = $"Name;" +
                    $"Shape;" +
                    $"Position;" +
                    $"Rotation;" +
                    $"Unk2C;" +
                    $"Unk40;" +
                    $"MapStudioLayer;" +
                    $"UnkE08;" +
                    $"MapID;" +
                    $"UnkS04;" +
                    $"UnkS0C;" +
                    $"ActivationPartName;" +
                    $"EntityID;" +
                    $"MessageID;" +
                    $"UnkT02;" +
                    $"Hidden;" +
                    $"UnkT08;" +
                    $"UnkT0C;" +
                    $"EnableEventFlagID;" +
                    $"CharacterModelName;" +
                    $"NPCParamID;" +
                    $"AnimationID;" +
                    $"CharaInitParamID;" +

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

            logger.AddToLog($"Written {propertyName} data to {output_dir}");
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

            foreach (MSBE.Region.Message entry in msb.Regions.Messages)
            {
                string line = "";

                // Combined: add MapID column
                if (!outputByMap)
                {
                    line = line +
                        $"{map_name};";
                }

                line = line +
                    $"{entry.Name};" +
                    $"{entry.Shape};" +
                    $"{entry.Position};" +
                    $"{entry.Rotation};" +
                    $"{entry.Unk2C};" +
                    $"{entry.Unk40};" +
                    $"{entry.MapStudioLayer};" +
                    $"{entry.UnkE08};" +
                    $"{entry.MapID};" +
                    $"{entry.UnkS04};" +
                    $"{entry.UnkS0C};" +
                    $"{entry.ActivationPartName};" +
                    $"{entry.EntityID};" +
                    $"{entry.MessageID};" +
                    $"{entry.UnkT02};" +
                    $"{entry.Hidden};" +
                    $"{entry.UnkT08};" +
                    $"{entry.UnkT0C};" +
                    $"{entry.EnableEventFlagID};" +
                    $"{entry.CharacterModelName};" +
                    $"{entry.NPCParamID};" +
                    $"{entry.AnimationID};" +
                    $"{entry.CharaInitParamID};" +

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