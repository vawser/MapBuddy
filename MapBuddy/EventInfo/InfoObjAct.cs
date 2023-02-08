using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.EventInfo
{
    internal class InfoObjAct
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        string output_dir = "";
        string header = "";
        string combined_file_name = "Global_ObjAct";
        string file_format = "csv";

        public InfoObjAct(string path)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }

            output_dir = logger.GetLogDir() + "\\Event\\ObjAct\\";

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
                    $"ObjActEntityID;" +
                    $"ObjActPartName;" +
                    $"ObjActID;" +
                    $"StateType;" +
                    $"EventFlagID;" +

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

            logger.AddToLog($"Written ObjAct data to {output_dir}");
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

            foreach (MSBE.Event.ObjAct entry in msb.Events.ObjActs)
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
                    $"{entry.ObjActEntityID};" +
                    $"{entry.ObjActPartName};" +
                    $"{entry.ObjActID};" +
                    $"{entry.StateType};" +
                    $"{entry.EventFlagID};" +

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

