using SoulsFormats.KF4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy
{
    internal class Util
    {
        public Dictionary<string, string> GetMapSelection(string map_selection, string path, Logger logger)
        {
             List<string> map_list = new List<string>();
             Dictionary<string, string> map_dict = new Dictionary<string, string>();

            if (map_selection == "All")
            {
                map_list = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
                logger.AddToLog($"Editing all maps.");
            }
            else
            {
                List<string> temp = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
                foreach (string s in temp)
                {
                    if (s.Contains(map_selection))
                    {
                        map_list.Add(s);
                    }
                }

                logger.AddToLog($"Editing map {map_selection}.");
            }

            foreach (string map in map_list)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                map_dict.Add(map_name, map_path);
            }

            return map_dict;
        }
    }
}
