using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;
using SoulsFormats.KF4;
using static SoulsFormats.MSB.Shape.Composite;

namespace MapBuddy.Action
{
    internal class EntityID
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        DCX.Type compressionType = DCX.Type.DCX_DFLT_10000_44_9;

        public EntityID(string path)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }
        }

        public void Execute()
        {
            foreach (KeyValuePair<string, string> entry in mapDict)
            {
                string map_name = entry.Key;
                string map_path = entry.Value;

                logger.AddToLog($"Editing {map_name}.");

                string[] map_indexes = map_name.Replace("m", "").Split("_");
                string entity_prefix = map_indexes[0] + map_indexes[1];

                MSBE msb = MSBE.Read(map_path);

                msb = AddUniqueEntityID(msb, entity_prefix);

                msb.Write(map_path, compressionType);

                logger.AddToLog($"Finished editing {map_name}.");
                logger.WriteLog();
            }

            MessageBox.Show("Unique EntityID given to all enemies.", "Information", MessageBoxButtons.OK);
        }

        public MSBE AddUniqueEntityID(MSBE msb, string entity_id_prefix)
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

                        logger.AddToLog($"Added {enemy.EntityID} to {enemy.Name}.");
                    }
                }
                else
                {
                    logger.AddToLog($"No valid Entity ID available to assign to {enemy.Name}.");
                }
            }

            return msb;
        }
    }
}
