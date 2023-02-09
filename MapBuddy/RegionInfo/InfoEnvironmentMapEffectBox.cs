using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.RegionInfo
{
    internal class InfoEnvironmentMapEffectBox
    {
        Logger logger = new Logger();

        string output_dir = "";
        string header = "";
        string propertyName = "EnvironmentMapEffectBox";
        string combined_file_name = "";
        string file_format = "csv";

        public InfoEnvironmentMapEffectBox(string path, Dictionary<string, string> mapDict, bool splitByMap)
        {
            combined_file_name = $"Global_{propertyName}";

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
                    $"EnableDist;" +
                    $"TransitionDist;" +
                    $"UnkT08;" +
                    $"UnkT09;" +
                    $"UnkT0A;" +
                    $"UnkT24;" +
                    $"UnkT28;" +
                    $"UnkT2C;" +
                    $"UnkT2E;" +
                    $"UnkT2F;" +
                    $"UnkT30;" +
                    $"UnkT33;" +
                    $"UnkT34;" +
                    $"UnkT36;" +

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

            foreach (MSBE.Region.EnvironmentMapEffectBox entry in msb.Regions.EnvironmentMapEffectBoxes)
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
                    $"{entry.EnableDist};" +
                    $"{entry.TransitionDist};" +
                    $"{entry.UnkT08};" +
                    $"{entry.UnkT09};" +
                    $"{entry.UnkT0A};" +
                    $"{entry.UnkT24};" +
                    $"{entry.UnkT28};" +
                    $"{entry.UnkT2C};" +
                    $"{entry.UnkT2E};" +
                    $"{entry.UnkT2F};" +
                    $"{entry.UnkT30};" +
                    $"{entry.UnkT33};" +
                    $"{entry.UnkT34};" +
                    $"{entry.UnkT36};" +

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