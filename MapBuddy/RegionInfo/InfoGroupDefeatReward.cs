using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.RegionInfo
{
    internal class InfoGroupDefeatReward
    {
        Logger logger = new Logger();

        string output_dir = "";
        string header = "";
        string propertyName = "GroupDefeatReward";
        string combined_file_name = "";
        string file_format = "csv";

        public InfoGroupDefeatReward(string path, Dictionary<string, string> mapDict, bool splitByMap)
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
                    $"UnkT00;" +
                    $"UnkT04;" +
                    $"UnkT08;" +
                    $"PartNames[0];" +
                    $"PartNames[1];" +
                    $"PartNames[2];" +
                    $"PartNames[3];" +
                    $"PartNames[4];" +
                    $"PartNames[5];" +
                    $"PartNames[6];" +
                    $"PartNames[7];" +
                    $"UnkT34;" +
                    $"UnkT38;" +
                    $"UnkT54;" +

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

            foreach (MSBE.Region.GroupDefeatReward entry in msb.Regions.GroupDefeatRewards)
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
                    $"{entry.UnkT00};" +
                    $"{entry.UnkT04};" +
                    $"{entry.UnkT08};" +
                    $"{entry.PartNames[0]};" +
                    $"{entry.PartNames[1]};" +
                    $"{entry.PartNames[2]};" +
                    $"{entry.PartNames[3]};" +
                    $"{entry.PartNames[4]};" +
                    $"{entry.PartNames[5]};" +
                    $"{entry.PartNames[6]};" +
                    $"{entry.PartNames[7]};" +
                    $"{entry.UnkT34};" +
                    $"{entry.UnkT38};" +
                    $"{entry.UnkT54};" +

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