using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SoulsFormats;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

namespace MapBuddy.Action
{
    internal class EntityGroupID
    {
        Logger logger = new Logger();
        Util util = new Util();

        Dictionary<string, string> map_dict = new Dictionary<string, string>();

        DCX.Type compressionType = DCX.Type.DCX_DFLT_10000_44_9;

        public EntityGroupID(string map_selection, string path, bool isAssetChange, bool isEnemyChange, bool replaceExisting, string EntityGroupID, string EntityGroupIndex, bool limitByModelName_Asset, string AssetLimitString_ModelName, bool limitByModelName_Enemy, string EnemyLimitString_ModelName, bool limitByNPCParam_Enemy, string EnemyLimitString_NPCParam)
        {
            map_dict = util.GetMapSelection(map_selection, path, logger);

            foreach (KeyValuePair<string, string> entry in map_dict)
            {
                string map_name = entry.Key;
                string map_path = entry.Value;

                bool edit = false;
                if (map_selection == "All")
                {
                    edit = true;
                }
                else if (map_selection.Contains(map_name))
                {
                    edit = true;
                }

                if (edit)
                {
                    logger.AddToLog($"Editing {map_name}.");

                    string[] map_indexes = map_name.Replace("m", "").Split("_");

                    MSBE msb = MSBE.Read(map_path);

                    msb = AddEntityGroupID(msb, map_name,
                        isAssetChange, isEnemyChange, replaceExisting,
                        EntityGroupID, EntityGroupIndex,
                        limitByModelName_Asset, AssetLimitString_ModelName,
                        limitByModelName_Enemy, EnemyLimitString_ModelName,
                        limitByNPCParam_Enemy, EnemyLimitString_NPCParam
                    );

                    msb.Write(map_path, compressionType);

                    logger.AddToLog($"Finished editing {map_name}.");
                    logger.WriteLog();
                }
            }

            MessageBox.Show("Applied Entity Group ID according to specification.", "Information", MessageBoxButtons.OK);
        }

        public MSBE AddEntityGroupID(MSBE msb, string map_name, bool isAssetChange, bool isEnemyChange, bool replaceExisting, string EntityGroupID, string EntityGroupIndex, bool limitByModelName_Asset, string AssetLimitString_ModelName, bool limitByModelName_Enemy, string EnemyLimitString_ModelName, bool limitByNPCParam_Enemy, string EnemyLimitString_NPCParam)
        {

            uint entity_group_id = Convert.ToUInt32(EntityGroupID);
            int base_entity_group_index = Convert.ToInt32(EntityGroupIndex);

            if (isAssetChange)
            {
                string[] limitList_ModelName = AssetLimitString_ModelName.Split(";");

                foreach (MSBE.Part.Asset entity in msb.Parts.Assets)
                {
                    int current_index = base_entity_group_index;

                    // Skip this entity if it not one of the specified ModelNames if that limit is enabled
                    if (limitByModelName_Asset && !limitList_ModelName.Contains(entity.ModelName))
                    {
                        continue;
                    }

                    if (entity.EntityGroupIDs[current_index] == 0)
                    {
                        entity.EntityGroupIDs[current_index] = entity_group_id;
                        logger.AddToLog($"Added {entity_group_id} to EntityGroupID[{current_index}] for {entity.Name}.");
                    }
                    else
                    {
                        bool wasAssigned = false;

                        for (int i = 0; i < entity.EntityGroupIDs.Length; i++)
                        {
                            if (entity.EntityGroupIDs[i] == 0)
                            {
                                entity.EntityGroupIDs[i] = entity_group_id;
                                logger.AddToLog($"Added {entity_group_id} to EntityGroupID[{i}] for {entity.Name}.");
                                wasAssigned = true;
                                break;
                            }
                        }

                        if(!wasAssigned)
                        {
                            logger.AddToLog($"No empty slots to add {entity_group_id} to EntityGroupID[{current_index}] for {entity.Name}.");
                        }
                    }
                }
            }

            if (isEnemyChange)
            {
                string[] limitList_ModelName = EnemyLimitString_ModelName.Split(";");
                string[] limitList_NPCParam = EnemyLimitString_NPCParam.Split(";");

                foreach (MSBE.Part.Enemy entity in msb.Parts.Enemies)
                {
                    int current_index = base_entity_group_index;

                    // Skip this entity if it not one of the specified ModelNames if that limit is enabled
                    if (limitByModelName_Enemy && !limitList_ModelName.Contains(entity.ModelName))
                    {
                        continue;
                    }

                    // Skip this entity if it not one of the specified NPCParams if that limit is enabled
                    if (limitByNPCParam_Enemy && !limitList_NPCParam.Contains(entity.NPCParamID.ToString()))
                    {
                        continue;
                    }

                    // Override existing if enabled
                    if (replaceExisting)
                    {
                        entity.EntityGroupIDs[current_index] = entity_group_id;
                        logger.AddToLog($"Added {entity_group_id} to EntityGroupID[{current_index}] for {entity.Name}.");
                    }
                    // Otherwise try specified index, and if it is not empty, find next empty index if possible.
                    else
                    {
                        if (entity.EntityGroupIDs[current_index] == 0)
                        {
                            entity.EntityGroupIDs[current_index] = entity_group_id;
                            logger.AddToLog($"Added {entity_group_id} to EntityGroupID[{current_index}] for {entity.Name}.");
                        }
                        else
                        {
                            bool wasAssigned = false;

                            for (int i = 0; i < entity.EntityGroupIDs.Length; i++)
                            {
                                if (entity.EntityGroupIDs[i] == 0)
                                {
                                    entity.EntityGroupIDs[i] = entity_group_id;
                                    logger.AddToLog($"Added {entity_group_id} to EntityGroupID[{i}] for {entity.Name}.");
                                    wasAssigned = true;
                                    break;
                                }
                            }

                            if (!wasAssigned)
                            {
                                logger.AddToLog($"No empty slots to add {entity_group_id} to EntityGroupID[{current_index}] for {entity.Name}.");
                            }
                        }
                    }
                }
            }

            return msb;
        }
    }
}
