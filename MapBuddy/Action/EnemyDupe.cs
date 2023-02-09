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
    internal class EnemyDupe
    {
        Logger logger = new Logger();
        Util util = new Util();

        Dictionary<string, string> map_dict = new Dictionary<string, string>();

        DCX.Type compressionType = DCX.Type.DCX_DFLT_10000_44_9;

        public EnemyDupe(string map_selection, string path, string dupe_count, bool isUsingExcludeList, string excludeList, bool isUsingIncludeList, string includeList, bool ignoreBossEnemy, bool ignorePlayerEnemy, bool ignorePassiveEnemy, bool ignoreScriptEnemy)
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

                msb = DupeEnemies(
                    msb,
                    dupe_count,
                    isUsingExcludeList, 
                    excludeList, 
                    isUsingIncludeList, 
                    includeList, 
                    ignoreBossEnemy, 
                    ignorePlayerEnemy, 
                    ignorePassiveEnemy,
                    ignoreScriptEnemy
                );

                msb.Write(map_path, compressionType);

                logger.AddToLog($"Finished editing {map_name}.");
                logger.WriteLog();
            }

            MessageBox.Show("Duplicated enemies.", "Information", MessageBoxButtons.OK);
        }

        public MSBE DupeEnemies(MSBE msb, string dupe_count, bool isUsingExcludeList, string excludeList, bool isUsingIncludeList, string includeList, bool ignoreBossEnemy, bool ignorePlayerEnemy, bool ignorePassiveEnemy, bool ignoreScriptEnemy)
        {
            List<MSBE.Part.Enemy> new_enemies = new List<MSBE.Part.Enemy>();

            string ResourceFolder = System.AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\";

            string boss_list = File.ReadAllText(ResourceFolder + "enemy_boss.txt");
            string passive_list = File.ReadAllText(ResourceFolder + "enemy_passive.txt");
            string player_list = File.ReadAllText(ResourceFolder + "enemy_player.txt");
            string script_list = File.ReadAllText(ResourceFolder + "enemy_script.txt");

            string[] boss_exclusion = boss_list.Split(";");
            string[] passive_exclusion = passive_list.Split(";");
            string[] player_exclusion = player_list.Split(";");
            string[] script_exclusion = script_list.Split(";");

            foreach (MSBE.Part.Enemy entity in msb.Parts.Enemies)
            {
                string ModelName = entity.ModelName;
                string EntityID = Convert.ToString(entity.EntityID);

                // Skip if ModelName is in Boss exclusion list
                if (ignoreBossEnemy && boss_exclusion.Contains(EntityID))
                {
                    logger.AddToLog($"Ignored boss enemy: {entity.Name}.");
                    continue;
                }

                // Skip if ModelName is in Passive exclusion list
                if (ignorePassiveEnemy && passive_exclusion.Contains(ModelName))
                {
                    logger.AddToLog($"Ignored passive enemy: {entity.Name}.");
                    continue;
                }

                // Skip if ModelName is in Player exclusion list
                if (ignorePlayerEnemy && player_exclusion.Contains(ModelName))
                {
                    logger.AddToLog($"Ignored player enemy: {entity.Name}.");
                    continue;
                }

                // Skip if ModelName is in Script exclusion list
                if (ignoreScriptEnemy && script_exclusion.Contains(ModelName))
                {
                    logger.AddToLog($"Ignored script enemy: {entity.Name}.");
                    continue;
                }

                if (isUsingExcludeList)
                {
                    string[] exclude_list_ModelName = excludeList.Split(";");

                    // Skip if enemy is in exclude list
                    if (exclude_list_ModelName.Contains(entity.ModelName))
                    {
                        logger.AddToLog($"Ignored exclusion list enemy: {entity.Name}.");
                        continue;
                    }
                }
                if (isUsingIncludeList)
                {
                    string[] include_list_ModelName = includeList.Split(";");

                    // Skip if enemy isn't in include list
                    if (!include_list_ModelName.Contains(entity.ModelName))
                    {
                        logger.AddToLog($"Ignored enemy outside of inclusion list: {entity.Name}.");
                        continue;
                    }
                }

                int dupe_num = Convert.ToInt32(dupe_count);

                for (int i = 1; i <= dupe_num; i++)
                {
                    MSBE.Part.Enemy new_enemy = new MSBE.Part.Enemy();
                    new_enemy = DupeEnemy(new_enemy, entity, i);
                    new_enemies.Add(new_enemy);
                }
            }

            foreach(MSBE.Part.Enemy entity in new_enemies)
            {
                msb.Parts.Enemies.Add(entity);
            }
            
            return msb;
        }

        public MSBE.Part.Enemy DupeEnemy(MSBE.Part.Enemy enemy, MSBE.Part.Enemy sourceEnemy, int dupeLevel)
        {
            int offset = sourceEnemy.Unk08 + (10000 * dupeLevel); // This is used to ensure Unk08 is a unique ID
            uint entityId = 0; // Dupes should never inherit the source's entity ID
            int talkId = 0; // Dupes should never inherit the source's talk ID
            short platoonID = 0; // Dupes should never inherit the source's platoon ID

            // Part
            enemy.Name = sourceEnemy.Name + $"_DUPE_{dupeLevel}";
            enemy.ModelName = sourceEnemy.ModelName;
            enemy.Unk08 = offset;
            enemy.SibPath = sourceEnemy.SibPath;
            enemy.Position = sourceEnemy.Position;
            enemy.Rotation = sourceEnemy.Rotation;
            enemy.Scale = sourceEnemy.Scale;
            enemy.Unk44 = sourceEnemy.Unk44;
            enemy.EntityID = entityId; 
            enemy.UnkE04 = sourceEnemy.UnkE04;
            enemy.LodParamID = sourceEnemy.LodParamID;
            enemy.UnkE09 = sourceEnemy.UnkE09;
            enemy.IsPointLightShadowSrc = sourceEnemy.IsPointLightShadowSrc;
            enemy.UnkE0B = sourceEnemy.UnkE0B;
            enemy.IsShadowSrc = sourceEnemy.IsShadowSrc;
            enemy.IsStaticShadowSrc = sourceEnemy.IsStaticShadowSrc;
            enemy.IsCascade3ShadowSrc = sourceEnemy.IsCascade3ShadowSrc;
            enemy.UnkE0F = sourceEnemy.UnkE0F;
            enemy.UnkE10 = sourceEnemy.UnkE10;
            enemy.IsShadowDest = sourceEnemy.IsShadowDest;
            enemy.IsShadowOnly = sourceEnemy.IsShadowOnly;
            enemy.DrawByReflectCam = sourceEnemy.DrawByReflectCam;
            enemy.DrawOnlyReflectCam = sourceEnemy.DrawOnlyReflectCam;
            enemy.EnableOnAboveShadow = sourceEnemy.EnableOnAboveShadow;
            enemy.DisablePointLightEffect = sourceEnemy.DisablePointLightEffect;
            enemy.UnkE17 = sourceEnemy.UnkE17;
            enemy.UnkE18 = sourceEnemy.UnkE18;
            enemy.EntityGroupIDs[0] = sourceEnemy.EntityGroupIDs[0];
            enemy.EntityGroupIDs[1] = sourceEnemy.EntityGroupIDs[1];
            enemy.EntityGroupIDs[2] = sourceEnemy.EntityGroupIDs[2];
            enemy.EntityGroupIDs[3] = sourceEnemy.EntityGroupIDs[3];
            enemy.EntityGroupIDs[4] = sourceEnemy.EntityGroupIDs[4];
            enemy.EntityGroupIDs[5] = sourceEnemy.EntityGroupIDs[5];
            enemy.EntityGroupIDs[6] = sourceEnemy.EntityGroupIDs[6];
            enemy.EntityGroupIDs[7] = sourceEnemy.EntityGroupIDs[7];
            enemy.UnkE3C = sourceEnemy.UnkE3C;
            enemy.UnkE3E = sourceEnemy.UnkE3E;

            // Property groups - Copy from source
            enemy.Unk1 = sourceEnemy.Unk1;
            enemy.Gparam = sourceEnemy.Gparam;
            enemy.Unk8 = sourceEnemy.Unk8;
            enemy.Unk10 = sourceEnemy.Unk10;

            // Enemy-specific
            enemy.ThinkParamID = sourceEnemy.ThinkParamID;
            enemy.NPCParamID = sourceEnemy.NPCParamID;
            enemy.TalkID = talkId;
            enemy.UnkT15 = sourceEnemy.UnkT15;
            enemy.PlatoonID = platoonID;
            enemy.CharaInitID = sourceEnemy.CharaInitID;
            enemy.CollisionPartName = sourceEnemy.CollisionPartName;
            enemy.WalkRouteName = sourceEnemy.WalkRouteName;
            enemy.UnkT24 = sourceEnemy.UnkT24;
            enemy.UnkT28 = sourceEnemy.UnkT28;
            enemy.ChrActivateCondParamID = sourceEnemy.ChrActivateCondParamID;
            enemy.UnkT34 = sourceEnemy.UnkT34;
            enemy.BackupEventAnimID = sourceEnemy.BackupEventAnimID;
            enemy.UnkT3C = sourceEnemy.UnkT3C;
            enemy.SpEffectSetParamID[0] = sourceEnemy.SpEffectSetParamID[0];
            enemy.SpEffectSetParamID[1] = sourceEnemy.SpEffectSetParamID[1];
            enemy.SpEffectSetParamID[2] = sourceEnemy.SpEffectSetParamID[2];
            enemy.SpEffectSetParamID[3] = sourceEnemy.SpEffectSetParamID[3];
            enemy.UnkT84 = sourceEnemy.UnkT84;

            return enemy;
        }
    }
}
