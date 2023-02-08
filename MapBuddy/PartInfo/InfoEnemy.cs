using SoulsFormats;
using SoulsFormats.KF4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.Info
{
    internal class InfoEnemy
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        string output_dir = "";
        string header = "";
        string combined_file_name = "Global_Enemy";
        string file_format = "csv";

        public InfoEnemy(string path)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }

            output_dir = logger.GetLogDir() + "\\Part\\Enemy\\";

            header = $"Name;" +
                    $"ModelName;" +
                    $"Unk08;" +
                    $"SibPath;" +
                    $"Position;" +
                    $"Rotation;" +
                    $"Scale;" +
                    $"Unk44;" +
                    $"MapStudioLayer;" +
                    $"EntityID;" +
                    $"UnkE04;" +
                    $"LodParamID;" +
                    $"UnkE09;" +
                    $"IsPointLightShadowSrc;" +
                    $"UnkE0B;" +
                    $"IsShadowSrc;" +
                    $"IsStaticShadowSrc;" +
                    $"IsCascade3ShadowSrc;" +
                    $"UnkE0F;" +
                    $"UnkE10;" +
                    $"IsShadowDest;" +
                    $"IsShadowOnly;" +
                    $"DrawByReflectCam;" +
                    $"DrawOnlyReflectCam;" +
                    $"EnableOnAboveShadow;" +
                    $"DisablePointLightEffect;" +
                    $"UnkE17;" +
                    $"UnkE18;" +
                    $"EntityGroupIDs[0];" +
                    $"EntityGroupIDs[1];" +
                    $"EntityGroupIDs[2];" +
                    $"EntityGroupIDs[3];" +
                    $"EntityGroupIDs[4];" +
                    $"EntityGroupIDs[5];" +
                    $"EntityGroupIDs[6];" +
                    $"EntityGroupIDs[7];" +
                    $"UnkE3C;" +
                    $"UnkE3E;" +
                    $"Unk1.DisplayGroups[0];" +
                    $"Unk1.DisplayGroups[1];" +
                    $"Unk1.DisplayGroups[2];" +
                    $"Unk1.DisplayGroups[3];" +
                    $"Unk1.DisplayGroups[4];" +
                    $"Unk1.DisplayGroups[5];" +
                    $"Unk1.DisplayGroups[6];" +
                    $"Unk1.DisplayGroups[7];" +
                    $"Unk1.DrawGroups[0];" +
                    $"Unk1.DrawGroups[1];" +
                    $"Unk1.DrawGroups[2];" +
                    $"Unk1.DrawGroups[3];" +
                    $"Unk1.DrawGroups[4];" +
                    $"Unk1.DrawGroups[5];" +
                    $"Unk1.DrawGroups[6];" +
                    $"Unk1.DrawGroups[7];" +
                    $"Unk1.CollisionMask[0];" +
                    $"Unk1.CollisionMask[1];" +
                    $"Unk1.CollisionMask[2];" +
                    $"Unk1.CollisionMask[3];" +
                    $"Unk1.CollisionMask[4];" +
                    $"Unk1.CollisionMask[5];" +
                    $"Unk1.CollisionMask[6];" +
                    $"Unk1.CollisionMask[7];" +
                    $"Unk1.CollisionMask[8];" +
                    $"Unk1.CollisionMask[9];" +
                    $"Unk1.CollisionMask[10];" +
                    $"Unk1.CollisionMask[11];" +
                    $"Unk1.CollisionMask[12];" +
                    $"Unk1.CollisionMask[13];" +
                    $"Unk1.CollisionMask[14];" +
                    $"Unk1.CollisionMask[15];" +
                    $"Unk1.CollisionMask[16];" +
                    $"Unk1.CollisionMask[17];" +
                    $"Unk1.CollisionMask[18];" +
                    $"Unk1.CollisionMask[19];" +
                    $"Unk1.CollisionMask[20];" +
                    $"Unk1.CollisionMask[21];" +
                    $"Unk1.CollisionMask[22];" +
                    $"Unk1.CollisionMask[23];" +
                    $"Unk1.CollisionMask[24];" +
                    $"Unk1.CollisionMask[25];" +
                    $"Unk1.CollisionMask[26];" +
                    $"Unk1.CollisionMask[27];" +
                    $"Unk1.CollisionMask[28];" +
                    $"Unk1.CollisionMask[29];" +
                    $"Unk1.CollisionMask[30];" +
                    $"Unk1.CollisionMask[31];" +
                    $"Unk1.Condition1;" +
                    $"Unk1.Condition2;" +
                    $"Unk1.UnkC2;" +
                    $"Unk1.UnkC3;" +
                    $"Unk1.UnkC4;" +
                    $"Unk1.UnkC6;" +
                    $"Gparam.LightSetID;" +
                    $"Gparam.FogParamID;" +
                    $"Gparam.LightScatteringID;" +
                    $"Gparam.EnvMapID;" +
                    $"Unk8.Unk00;" +
                    $"Unk10.MapID;" +
                    $"Unk10.Unk04;" +
                    $"Unk10.Unk0C;" +
                    $"Unk10.Unk10;" +
                    $"Unk10.Unk14;" +
                    $"ThinkParamID;" +
                    $"NPCParamID;" +
                    $"TalkID;" +
                    $"UnkT15;" +
                    $"PlatoonID;" +
                    $"CharaInitID;" +
                    $"CollisionPartName;" +
                    $"WalkRouteName;" +
                    $"UnkT24;" +
                    $"UnkT28;" +
                    $"ChrActivateCondParamID;" +
                    $"UnkT34;" +
                    $"BackupEventAnimID;" +
                    $"UnkT3C;" +
                    $"SpEffectSetParamID[0];" +
                    $"SpEffectSetParamID[1];" +
                    $"SpEffectSetParamID[2];" +
                    $"SpEffectSetParamID[3];" +
                    $"UnkT84;" +
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

        string file_format = "csv";
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

            logger.AddToLog($"Written Enemy data to {output_dir}");
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

            foreach (MSBE.Part.Enemy enemy in msb.Parts.Enemies)
            {
                string line = "";

                // Combined: add MapID column
                if (!outputByMap)
                {
                    line = line +
                        $"{map_name};";
                }

                line = line +
                    $"{enemy.Name};" +
                    $"{enemy.ModelName};" +
                    $"{enemy.Unk08};" +
                    $"{enemy.SibPath};" +
                    $"{enemy.Position};" +
                    $"{enemy.Rotation};" +
                    $"{enemy.Scale};" +
                    $"{enemy.Unk44};" +
                    $"{enemy.MapStudioLayer};" +
                    $"{enemy.EntityID};" +
                    $"{enemy.UnkE04};" +
                    $"{enemy.LodParamID};" +
                    $"{enemy.UnkE09};" +
                    $"{enemy.IsPointLightShadowSrc};" +
                    $"{enemy.UnkE0B};" +
                    $"{enemy.IsShadowSrc};" +
                    $"{enemy.IsStaticShadowSrc};" +
                    $"{enemy.IsCascade3ShadowSrc};" +
                    $"{enemy.UnkE0F};" +
                    $"{enemy.UnkE10};" +
                    $"{enemy.IsShadowDest};" +
                    $"{enemy.IsShadowOnly};" +
                    $"{enemy.DrawByReflectCam};" +
                    $"{enemy.DrawOnlyReflectCam};" +
                    $"{enemy.EnableOnAboveShadow};" +
                    $"{enemy.DisablePointLightEffect};" +
                    $"{enemy.UnkE17};" +
                    $"{enemy.UnkE18};" +
                    $"{enemy.EntityGroupIDs[0]};" +
                    $"{enemy.EntityGroupIDs[1]};" +
                    $"{enemy.EntityGroupIDs[2]};" +
                    $"{enemy.EntityGroupIDs[3]};" +
                    $"{enemy.EntityGroupIDs[4]};" +
                    $"{enemy.EntityGroupIDs[5]};" +
                    $"{enemy.EntityGroupIDs[6]};" +
                    $"{enemy.EntityGroupIDs[7]};" +
                    $"{enemy.UnkE3C};" +
                    $"{enemy.UnkE3E};" +
                    $"{enemy.Unk1.DisplayGroups[0]};" +
                    $"{enemy.Unk1.DisplayGroups[1]};" +
                    $"{enemy.Unk1.DisplayGroups[2]};" +
                    $"{enemy.Unk1.DisplayGroups[3]};" +
                    $"{enemy.Unk1.DisplayGroups[4]};" +
                    $"{enemy.Unk1.DisplayGroups[5]};" +
                    $"{enemy.Unk1.DisplayGroups[6]};" +
                    $"{enemy.Unk1.DisplayGroups[7]};" +
                    $"{enemy.Unk1.DrawGroups[0]};" +
                    $"{enemy.Unk1.DrawGroups[1]};" +
                    $"{enemy.Unk1.DrawGroups[2]};" +
                    $"{enemy.Unk1.DrawGroups[3]};" +
                    $"{enemy.Unk1.DrawGroups[4]};" +
                    $"{enemy.Unk1.DrawGroups[5]};" +
                    $"{enemy.Unk1.DrawGroups[6]};" +
                    $"{enemy.Unk1.DrawGroups[7]};" +
                    $"{enemy.Unk1.CollisionMask[0]};" +
                    $"{enemy.Unk1.CollisionMask[1]};" +
                    $"{enemy.Unk1.CollisionMask[2]};" +
                    $"{enemy.Unk1.CollisionMask[3]};" +
                    $"{enemy.Unk1.CollisionMask[4]};" +
                    $"{enemy.Unk1.CollisionMask[5]};" +
                    $"{enemy.Unk1.CollisionMask[6]};" +
                    $"{enemy.Unk1.CollisionMask[7]};" +
                    $"{enemy.Unk1.CollisionMask[8]};" +
                    $"{enemy.Unk1.CollisionMask[9]};" +
                    $"{enemy.Unk1.CollisionMask[10]};" +
                    $"{enemy.Unk1.CollisionMask[11]};" +
                    $"{enemy.Unk1.CollisionMask[12]};" +
                    $"{enemy.Unk1.CollisionMask[13]};" +
                    $"{enemy.Unk1.CollisionMask[14]};" +
                    $"{enemy.Unk1.CollisionMask[15]};" +
                    $"{enemy.Unk1.CollisionMask[16]};" +
                    $"{enemy.Unk1.CollisionMask[17]};" +
                    $"{enemy.Unk1.CollisionMask[18]};" +
                    $"{enemy.Unk1.CollisionMask[19]};" +
                    $"{enemy.Unk1.CollisionMask[20]};" +
                    $"{enemy.Unk1.CollisionMask[21]};" +
                    $"{enemy.Unk1.CollisionMask[22]};" +
                    $"{enemy.Unk1.CollisionMask[23]};" +
                    $"{enemy.Unk1.CollisionMask[24]};" +
                    $"{enemy.Unk1.CollisionMask[25]};" +
                    $"{enemy.Unk1.CollisionMask[26]};" +
                    $"{enemy.Unk1.CollisionMask[27]};" +
                    $"{enemy.Unk1.CollisionMask[28]};" +
                    $"{enemy.Unk1.CollisionMask[29]};" +
                    $"{enemy.Unk1.CollisionMask[30]};" +
                    $"{enemy.Unk1.CollisionMask[31]};" +
                    $"{enemy.Unk1.Condition1};" +
                    $"{enemy.Unk1.Condition2};" +
                    $"{enemy.Unk1.UnkC2};" +
                    $"{enemy.Unk1.UnkC3};" +
                    $"{enemy.Unk1.UnkC4};" +
                    $"{enemy.Unk1.UnkC6};" +
                    $"{enemy.Gparam.LightSetID};" +
                    $"{enemy.Gparam.FogParamID};" +
                    $"{enemy.Gparam.LightScatteringID};" +
                    $"{enemy.Gparam.EnvMapID};" +
                    $"{enemy.Unk8.Unk00};" +
                    $"{enemy.Unk10.MapID};" +
                    $"{enemy.Unk10.Unk04};" +
                    $"{enemy.Unk10.Unk0C};" +
                    $"{enemy.Unk10.Unk10};" +
                    $"{enemy.Unk10.Unk14};" +
                    $"{enemy.ThinkParamID};" +
                    $"{enemy.NPCParamID};" +
                    $"{enemy.TalkID};" +
                    $"{enemy.UnkT15};" +
                    $"{enemy.PlatoonID};" +
                    $"{enemy.CharaInitID};" +
                    $"{enemy.CollisionPartName};" +
                    $"{enemy.WalkRouteName};" +
                    $"{enemy.UnkT24};" +
                    $"{enemy.UnkT28};" +
                    $"{enemy.ChrActivateCondParamID};" +
                    $"{enemy.UnkT34};" +
                    $"{enemy.BackupEventAnimID};" +
                    $"{enemy.UnkT3C};" +
                    $"{enemy.SpEffectSetParamID[0]};" +
                    $"{enemy.SpEffectSetParamID[1]};" +
                    $"{enemy.SpEffectSetParamID[2]};" +
                    $"{enemy.SpEffectSetParamID[3]};" +
                    $"{enemy.UnkT84};" +
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
