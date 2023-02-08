using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SoulsFormats;

namespace MapBuddy.Action
{
    internal class EntityGroupID
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        DCX.Type compressionType = DCX.Type.DCX_DFLT_10000_44_9;

        public EntityGroupID(string path)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }
        }
        public void Execute(int entity_id, int index, string chrID)
        {
            foreach (KeyValuePair<string, string> entry in mapDict)
            {
                string map_name = entry.Key;
                string map_path = entry.Value;

                logger.AddToLog($"Editing {map_name}.");

                string[] map_indexes = map_name.Replace("m", "").Split("_");

                MSBE msb = MSBE.Read(map_path);

                msb = AddEntityGroupID(msb, map_name, entity_id, index, chrID);

                msb.Write(map_path, compressionType);

                logger.AddToLog($"Finished editing {map_name}.");
                logger.WriteLog();
            }

            MessageBox.Show($"Added specified {entity_id} at {index} for all valid enemies.", "Information", MessageBoxButtons.OK);
        }

        public MSBE AddEntityGroupID(MSBE msb, string map_name, int entity_id, int index, string chrID)
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
                        logger.AddToLog($"Added {entity_id} to EntityGroupID[{index}] for {enemy.Name}.");
                    }
                    // Otherwise find an empty one
                    else
                    {
                        for (int i = 0; i < enemy.EntityGroupIDs.Length; i++)
                        {
                            if (enemy.EntityGroupIDs[i] == 0)
                            {
                                enemy.EntityGroupIDs[i] = Convert.ToUInt32(entity_id);
                                logger.AddToLog($"Added {entity_id} to EntityGroupID[{i}] for {enemy.Name}.");
                                break;
                            }
                        }
                    }
                }
            }

            return msb;
        }
    }
}
