using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;
using SoulsFormats.KF4;
using static SoulsFormats.MSB.Shape.Composite;

namespace MapBuddy
{
    internal class MapEdit
    {
        DCX.Type compressionType = DCX.Type.DCX_DFLT_10000_44_9;

        private string mod_folder;
        private List<string> maps = new List<string>();

        string base_dir = System.AppDomain.CurrentDomain.BaseDirectory;
        string log_dir = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";

        Dictionary<string, string> map_dict = new Dictionary<string, string>();

        private string log_string;

        public MapEdit(string path)
        {
            mod_folder = path;
        }

        public void BuildMapDict()
        {
            maps = Directory.GetFileSystemEntries(mod_folder + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (String map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                map_dict.Add(map_name, map_path);
            }
        }

        public void AddUniqueEntityIDToAll()
        {
            foreach (KeyValuePair<string, string> entry in map_dict)
            {
                string map_name = entry.Key;
                string map_path = entry.Value;

                AddToLog($"Editing {map_name}.");

                string[] map_indexes = map_name.Replace("m", "").Split("_");
                string entity_prefix = map_indexes[0] + map_indexes[1];

                MSBE msb = MSBE.Read(map_path);

                msb = AddUniqueEntityID_byMap(msb, entity_prefix);

                msb.Write(map_path, compressionType);

                AddToLog($"Finished editing {map_name}.");
                WriteLog();
            }

            MessageBox.Show("Unique EntityID given to all enemies.", "Information", MessageBoxButtons.OK);
        }

        public MSBE AddUniqueEntityID_byMap(MSBE msb, string entity_id_prefix)
        {
            List<int> valid_entity_ids = new List<int>();
            List<int> used_entity_ids = new List<int>();

            // Build list of used
            foreach (MSBE.Part.Enemy enemy in msb.Parts.Enemies)
            {
                used_entity_ids.Add(Convert.ToInt32(enemy.EntityID));
            }

            // Build list of possible
            int minID = Convert.ToInt32(entity_id_prefix + "0100");

            List<int> possible_entity_ids = Enumerable.Range(minID, 999).ToList();

            // Remove collisions
            var used_set = new HashSet<int>(used_entity_ids);
            var possible_set = new HashSet<int>(possible_entity_ids);

            possible_set.SymmetricExceptWith(used_set);

            valid_entity_ids = possible_set.ToList();
            valid_entity_ids.Remove(0);

            // Add new entity IDs to the enemies without them
            foreach (MSBE.Part.Enemy enemy in msb.Parts.Enemies)
            {
                if (valid_entity_ids.Count >= 1)
                {
                    if (enemy.EntityID == 0)
                    {
                        int id = valid_entity_ids[0];
                        enemy.EntityID = Convert.ToUInt32(id);
                        valid_entity_ids.Remove(id);

                        AddToLog($"Added {enemy.EntityID} to {enemy.Name}.");
                    }
                }
                else
                {
                    AddToLog($"No valid Entity ID available to assign to {enemy.Name}.");
                }
            }

            return msb;
        }

        public void WriteEnemyInfo()
        {
            foreach (KeyValuePair<string, string> entry in map_dict)
            {
                string map_name = entry.Key;
                string map_path = entry.Value;

                string[] map_indexes = map_name.Replace("m", "").Split("_");
                string entity_prefix = map_indexes[0] + map_indexes[1];

                MSBE msb = MSBE.Read(map_path);

                WriteEnemyInfo_byMap(msb, map_name);
            }

            AddToLog($"Written entity information to {log_dir}\\Enemy\\");
            MessageBox.Show($"Written entity information to {log_dir}\\Enemy\\", "Information", MessageBoxButtons.OK);
        }

        public void WriteEnemyInfo_byMap(MSBE msb, string map_name)
        {
            string enemy_info_dir = log_dir + "\\Enemy\\";

            bool exists = System.IO.Directory.Exists(enemy_info_dir);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(enemy_info_dir);
            }

            string text = "";

            foreach (MSBE.Part.Enemy enemy in msb.Parts.Enemies)
            {
                string line = $"{enemy.EntityID} - {enemy.Name} - {enemy.NPCParamID} - {enemy.ModelName}\n";

                text = text + line;
            }

            File.WriteAllText(enemy_info_dir + $"{map_name}.txt", text);
        }

        public void AddEntityGroupID(int entity_id, int index, string chrID)
        {
            foreach (KeyValuePair<string, string> entry in map_dict)
            {
                string map_name = entry.Key;
                string map_path = entry.Value;

                AddToLog($"Editing {map_name}.");

                string[] map_indexes = map_name.Replace("m", "").Split("_");
                string entity_prefix = map_indexes[0] + map_indexes[1];

                MSBE msb = MSBE.Read(map_path);

                msb = AddEntityGroupID_byMap(msb, map_name, entity_id, index, chrID);

                msb.Write(map_path, compressionType);

                AddToLog($"Finished editing {map_name}.");
                WriteLog();
            }

            MessageBox.Show($"Added specified {entity_id} at {index} for all valid enemies.", "Information", MessageBoxButtons.OK);
        }

        public MSBE AddEntityGroupID_byMap(MSBE msb, string map_name, int entity_id, int index, string chrID)
        {
            // Build list of used
            foreach (MSBE.Part.Enemy enemy in msb.Parts.Enemies)
            {
                bool editEnemy = false;

                if (chrID != "")
                {
                    string[] valid_chrID = chrID.Split(";");

                    foreach (string s in valid_chrID)
                    {
                        if (enemy.ModelName.ToString() == s)
                        {
                            editEnemy = true;
                        }
                    }
                }
                else
                {
                    editEnemy = true;
                }

                if (editEnemy)
                {
                    int current_index = index;

                    // Try assigned index first
                    if (enemy.EntityGroupIDs[index] == 0)
                    {
                        enemy.EntityGroupIDs[index] = Convert.ToUInt32(entity_id);
                        AddToLog($"Added {entity_id} to EntityGroupID[{index}] for {enemy.Name}.");
                    }
                    // Otherwise find an empty one
                    else
                    {
                        for (int i = 0; i < enemy.EntityGroupIDs.Length; i++)
                        {
                            if (enemy.EntityGroupIDs[i] == 0)
                            {
                                enemy.EntityGroupIDs[i] = Convert.ToUInt32(entity_id);
                                AddToLog($"Added {entity_id} to EntityGroupID[{i}] for {enemy.Name}.");
                                break;
                            }
                        }
                    }
                }
            }

            return msb;
        }
        public void WriteLog()
        {
            bool exists = System.IO.Directory.Exists(log_dir);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(log_dir);
            }

            File.AppendAllText(log_dir + $"log.txt", log_string);

            // Reset log string after writing
            log_string = "";
        }

        public void AddToLog(string text)
        {
            log_string = log_string + text + "\n";
        }
    }
}
