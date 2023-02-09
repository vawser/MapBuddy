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
        Logger logger = new Logger();
        Util util = new Util();

        Dictionary<string, string> map_dict = new Dictionary<string, string>();

        DCX.Type compressionType = DCX.Type.DCX_DFLT_10000_44_9;

        bool incompleteChange;

        public EntityID(string map_selection, string path, bool isAssetChange, bool isEnemyChange, bool isPlayerChange, bool overrideExisting, int range_start_id, int range_end_id)
        {
            map_dict = util.GetMapSelection(map_selection, path, logger);

            foreach (KeyValuePair<string, string> entry in map_dict)
            {
                string map_name = entry.Key;
                string map_path = entry.Value;

                logger.AddToLog($"Editing {map_name}.");

                string[] map_indexes = map_name.Replace("m", "").Split("_");
                string entity_prefix = map_indexes[0] + map_indexes[1];

                MSBE msb = MSBE.Read(map_path);

                incompleteChange = false;
                msb = AddUniqueEntityID(msb, entity_prefix, isAssetChange, isEnemyChange, isPlayerChange, overrideExisting, range_start_id, range_end_id);

                msb.Write(map_path, compressionType);

                if(incompleteChange)
                {
                    MessageBox.Show($"Applied changes for {map_name} were incomplete, as entity ID range was insufficient to cover all entities.", "Information", MessageBoxButtons.OK);
                }

                logger.AddToLog($"Finished editing {map_name}.");
                logger.WriteLog();
            }

            MessageBox.Show("Applied unique Entity ID to specified type.", "Information", MessageBoxButtons.OK);
        }

        public MSBE AddUniqueEntityID(MSBE msb, string entity_id_prefix, bool isAssetChange, bool isEnemyChange, bool isPlayerChange, bool overrideExisting, int range_start_id, int range_end_id)
        {
            List<int> existing_ID_list = GetExistingList(msb);

            // Get count for Enumerable
            int range_diff = range_end_id - range_start_id;

            // Middle strings should be empty by default
            string start_middle_str = "";
            string end_middle_str = "";

            // Adjust start middle string if input is below 1000
            if (range_start_id >= 999 && range_start_id <= 100)
            {
                start_middle_str = "0";
            }
            else if (range_start_id >= 99 && range_start_id <= 10)
            {
                start_middle_str = "00";
            }
            else if (range_start_id >= 9 && range_start_id <= 1)
            {
                start_middle_str = "000";
            }

            // Adjust end middle string if input is below 1000
            if (range_end_id >= 999 && range_end_id <= 100)
            {
                end_middle_str = "0";
            }
            else if (range_end_id >= 99 && range_end_id <= 10)
            {
                end_middle_str = "00";
            }
            else if (range_end_id >= 9 && range_end_id <= 1)
            {
                end_middle_str = "000";
            }

            string start_id_str = $"{entity_id_prefix}{start_middle_str}{range_start_id}";
            string end_id_str = $"{entity_id_prefix}{end_middle_str}{range_end_id}";
            int start_id = Convert.ToInt32(start_id_str);
            int end_id = Convert.ToInt32(end_id_str);

            // Build the possible ID list
            List<int> possible_ID_list = Enumerable.Range(start_id, range_diff).ToList();

            // Get differences between existing and possible
            var used_set = new HashSet<int>(existing_ID_list);
            var possible_set = new HashSet<int>(possible_ID_list);
            possible_set.SymmetricExceptWith(used_set);

            // Build the valid ID list from the differences
            List<int> temp_id_list = possible_set.ToList();
            List<int> valid_ID_list = new List<int>();

            // Build valid ID list from possible list, contrained by the bounds set
            foreach (int entry in temp_id_list)
            {
                if(entry >= start_id && entry <= end_id)
                {
                    valid_ID_list.Add(entry);
                }
            }

            // Apply entity ID change
            if (isEnemyChange)
            {
                foreach (MSBE.Part.Enemy entity in msb.Parts.Enemies)
                {
                    if (valid_ID_list.Count >= 1)
                    {
                        // Replace if the current is 0 or overrideExisting is chosen
                        if (entity.EntityID == 0 || overrideExisting == true)
                        {
                            int id = valid_ID_list[0]; // Get first ID from valid ID list

                            entity.EntityID = Convert.ToUInt32(id); // Convert to UInt to fit MSB type

                            valid_ID_list.Remove(id); // Remove the used id from the list

                            logger.AddToLog($"Added {entity.EntityID} to {entity.Name}.");
                        }
                    }
                    else
                    {
                        incompleteChange = true;
                        logger.AddToLog($"No valid Entity ID available to assign to {entity.Name} with set range.");
                    }
                }
            }
            if (isAssetChange)
            {
                foreach (MSBE.Part.Asset entity in msb.Parts.Assets)
                {
                    if (valid_ID_list.Count >= 1)
                    {
                        // Replace if the current is 0 or overrideExisting is chosen
                        if (entity.EntityID == 0 || overrideExisting == true)
                        {
                            int id = valid_ID_list[0]; // Get first ID from valid ID list

                            entity.EntityID = Convert.ToUInt32(id); // Convert to UInt to fit MSB type

                            valid_ID_list.Remove(id); // Remove the used id from the list

                            logger.AddToLog($"Added {entity.EntityID} to {entity.Name}.");
                        }
                    }
                    else
                    {
                        incompleteChange = true;
                        logger.AddToLog($"No valid Entity ID available to assign to {entity.Name} with set range.");
                    }
                }
            }
            if (isPlayerChange)
            {
                foreach (MSBE.Part.Player entity in msb.Parts.Players)
                {
                    if (valid_ID_list.Count >= 1)
                    {
                        // Replace if the current is 0 or overrideExisting is chosen
                        if (entity.EntityID == 0 || overrideExisting == true)
                        {
                            int id = valid_ID_list[0]; // Get first ID from valid ID list

                            entity.EntityID = Convert.ToUInt32(id); // Convert to UInt to fit MSB type

                            valid_ID_list.Remove(id); // Remove the used id from the list

                            logger.AddToLog($"Added {entity.EntityID} to {entity.Name}.");
                        }
                    }
                    else
                    {
                        incompleteChange = true;
                        logger.AddToLog($"No valid Entity ID available to assign to {entity.Name} with set range.");
                    }
                }
            }

            return msb;
        }

        public List<int> GetExistingList(MSBE msb)
        {
            List<int> id_list = new List<int>();

            // Get existing IDs from all parts
            foreach (MSBE.Part.Enemy entity in msb.Parts.Enemies)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Part.Asset entity in msb.Parts.Assets)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Part.Player entity in msb.Parts.Players)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Part.Collision entity in msb.Parts.Collisions)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Part.ConnectCollision entity in msb.Parts.ConnectCollisions)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Part.MapPiece entity in msb.Parts.MapPieces)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Part.DummyAsset entity in msb.Parts.DummyAssets)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Part.DummyEnemy entity in msb.Parts.DummyEnemies)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }

            // Get existing IDs from all events
            foreach (MSBE.Event.Generator entity in msb.Events.Generators)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.Mount entity in msb.Events.Mounts)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.Navmesh entity in msb.Events.Navmeshes)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.ObjAct entity in msb.Events.ObjActs)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.Other entity in msb.Events.Others)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.PatrolInfo entity in msb.Events.PatrolInfo)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.PlatoonInfo entity in msb.Events.PlatoonInfo)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.PseudoMultiplayer entity in msb.Events.PseudoMultiplayers)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.RetryPoint entity in msb.Events.RetryPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.SignPool entity in msb.Events.SignPools)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Event.Treasure entity in msb.Events.Treasures)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }

            // Get existing IDs from all regions
            foreach (MSBE.Region.AutoDrawGroupPoint entity in msb.Regions.AutoDrawGroupPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.BuddySummonPoint entity in msb.Regions.BuddySummonPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.Connection entity in msb.Regions.Connections)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.Dummy entity in msb.Regions.Dummies)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.EnvironmentMapEffectBox entity in msb.Regions.EnvironmentMapEffectBoxes)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.EnvironmentMapOutput entity in msb.Regions.EnvironmentMapOutputs)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.EnvironmentMapPoint entity in msb.Regions.EnvironmentMapPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.FallPreventionRemoval entity in msb.Regions.FallPreventionRemovals)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.FastTravelRestriction entity in msb.Regions.FastTravelRestriction)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.GroupDefeatReward entity in msb.Regions.GroupDefeatRewards)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.Hitset entity in msb.Regions.Hitsets)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.HorseProhibition entity in msb.Regions.HorseProhibitions)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.InvasionPoint entity in msb.Regions.InvasionPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MapNameOverride entity in msb.Regions.MapNameOverrides)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MapPoint entity in msb.Regions.MapPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MapPointDiscoveryOverride entity in msb.Regions.MapPointDiscoveryOverrides)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MapPointParticipationOverride entity in msb.Regions.MapPointParticipationOverrides)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.Message entity in msb.Regions.Messages)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MountJump entity in msb.Regions.MountJumps)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MountJumpFall entity in msb.Regions.MountJumpFalls)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MufflingBox entity in msb.Regions.MufflingBoxes)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MufflingPlane entity in msb.Regions.MufflingPlanes)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.MufflingPortal entity in msb.Regions.MufflingPortals)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.NavmeshCutting entity in msb.Regions.NavmeshCuttings)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.Other entity in msb.Regions.Others)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.PatrolRoute entity in msb.Regions.PatrolRoutes)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.PatrolRoute22 entity in msb.Regions.PatrolRoute22s)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.PlayArea entity in msb.Regions.PlayAreas)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.SFX entity in msb.Regions.SFX)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.Sound entity in msb.Regions.Sounds)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.SoundRegion entity in msb.Regions.SoundRegions)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.SpawnPoint entity in msb.Regions.SpawnPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.WeatherCreateAssetPoint entity in msb.Regions.WeatherCreateAssetPoints)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.WeatherOverride entity in msb.Regions.WeatherOverrides)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.WindArea entity in msb.Regions.WindAreas)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }
            foreach (MSBE.Region.WindSFX entity in msb.Regions.WindSFX)
            {
                id_list.Add(Convert.ToInt32(entity.EntityID));
            }

            return id_list;
        }

    }
}
