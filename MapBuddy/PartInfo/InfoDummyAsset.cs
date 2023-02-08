using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.Info
{
    internal class InfoDummyAsset
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        string output_dir = "";
        string header = "";
        string combined_file_name = "Global_DummyAsset";
        string file_format = "csv";

        public InfoDummyAsset(string path)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }

            output_dir = logger.GetLogDir() + "\\Part\\DummyAsset\\";

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

            logger.AddToLog($"Written DummyAsset data to {output_dir}");
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

            foreach (MSBE.Part.DummyAsset asset in msb.Parts.DummyAssets)
            {
                string line = "";

                // Combined: add MapID column
                if (!outputByMap)
                {
                    line = line +
                        $"{map_name};";
                }

                line = line +
                    $"{asset.Name};" +
                    $"{asset.ModelName};" +
                    $"{asset.Unk08};" +
                    $"{asset.SibPath};" +
                    $"{asset.Position};" +
                    $"{asset.Rotation};" +
                    $"{asset.Scale};" +
                    $"{asset.Unk44};" +
                    $"{asset.MapStudioLayer};" +
                    $"{asset.EntityID};" +
                    $"{asset.UnkE04};" +
                    $"{asset.LodParamID};" +
                    $"{asset.UnkE09};" +
                    $"{asset.IsPointLightShadowSrc};" +
                    $"{asset.UnkE0B};" +
                    $"{asset.IsShadowSrc};" +
                    $"{asset.IsStaticShadowSrc};" +
                    $"{asset.IsCascade3ShadowSrc};" +
                    $"{asset.UnkE0F};" +
                    $"{asset.UnkE10};" +
                    $"{asset.IsShadowDest};" +
                    $"{asset.IsShadowOnly};" +
                    $"{asset.DrawByReflectCam};" +
                    $"{asset.DrawOnlyReflectCam};" +
                    $"{asset.EnableOnAboveShadow};" +
                    $"{asset.DisablePointLightEffect};" +
                    $"{asset.UnkE17};" +
                    $"{asset.UnkE18};" +
                    $"{asset.EntityGroupIDs[0]};" +
                    $"{asset.EntityGroupIDs[1]};" +
                    $"{asset.EntityGroupIDs[2]};" +
                    $"{asset.EntityGroupIDs[3]};" +
                    $"{asset.EntityGroupIDs[4]};" +
                    $"{asset.EntityGroupIDs[5]};" +
                    $"{asset.EntityGroupIDs[6]};" +
                    $"{asset.EntityGroupIDs[7]};" +
                    $"{asset.UnkE3C};" +
                    $"{asset.UnkE3E};" +

                    $"{asset.Unk1.DisplayGroups[0]};" +
                    $"{asset.Unk1.DisplayGroups[1]};" +
                    $"{asset.Unk1.DisplayGroups[2]};" +
                    $"{asset.Unk1.DisplayGroups[3]};" +
                    $"{asset.Unk1.DisplayGroups[4]};" +
                    $"{asset.Unk1.DisplayGroups[5]};" +
                    $"{asset.Unk1.DisplayGroups[6]};" +
                    $"{asset.Unk1.DisplayGroups[7]};" +
                    $"{asset.Unk1.DrawGroups[0]};" +
                    $"{asset.Unk1.DrawGroups[1]};" +
                    $"{asset.Unk1.DrawGroups[2]};" +
                    $"{asset.Unk1.DrawGroups[3]};" +
                    $"{asset.Unk1.DrawGroups[4]};" +
                    $"{asset.Unk1.DrawGroups[5]};" +
                    $"{asset.Unk1.DrawGroups[6]};" +
                    $"{asset.Unk1.DrawGroups[7]};" +
                    $"{asset.Unk1.CollisionMask[0]};" +
                    $"{asset.Unk1.CollisionMask[1]};" +
                    $"{asset.Unk1.CollisionMask[2]};" +
                    $"{asset.Unk1.CollisionMask[3]};" +
                    $"{asset.Unk1.CollisionMask[4]};" +
                    $"{asset.Unk1.CollisionMask[5]};" +
                    $"{asset.Unk1.CollisionMask[6]};" +
                    $"{asset.Unk1.CollisionMask[7]};" +
                    $"{asset.Unk1.CollisionMask[8]};" +
                    $"{asset.Unk1.CollisionMask[9]};" +
                    $"{asset.Unk1.CollisionMask[10]};" +
                    $"{asset.Unk1.CollisionMask[11]};" +
                    $"{asset.Unk1.CollisionMask[12]};" +
                    $"{asset.Unk1.CollisionMask[13]};" +
                    $"{asset.Unk1.CollisionMask[14]};" +
                    $"{asset.Unk1.CollisionMask[15]};" +
                    $"{asset.Unk1.CollisionMask[16]};" +
                    $"{asset.Unk1.CollisionMask[17]};" +
                    $"{asset.Unk1.CollisionMask[18]};" +
                    $"{asset.Unk1.CollisionMask[19]};" +
                    $"{asset.Unk1.CollisionMask[20]};" +
                    $"{asset.Unk1.CollisionMask[21]};" +
                    $"{asset.Unk1.CollisionMask[22]};" +
                    $"{asset.Unk1.CollisionMask[23]};" +
                    $"{asset.Unk1.CollisionMask[24]};" +
                    $"{asset.Unk1.CollisionMask[25]};" +
                    $"{asset.Unk1.CollisionMask[26]};" +
                    $"{asset.Unk1.CollisionMask[27]};" +
                    $"{asset.Unk1.CollisionMask[28]};" +
                    $"{asset.Unk1.CollisionMask[29]};" +
                    $"{asset.Unk1.CollisionMask[30]};" +
                    $"{asset.Unk1.CollisionMask[31]};" +
                    $"{asset.Unk1.Condition1};" +
                    $"{asset.Unk1.Condition2};" +
                    $"{asset.Unk1.UnkC2};" +
                    $"{asset.Unk1.UnkC3};" +
                    $"{asset.Unk1.UnkC4};" +
                    $"{asset.Unk1.UnkC6};" +

                    $"{asset.Gparam.LightSetID};" +
                    $"{asset.Gparam.FogParamID};" +
                    $"{asset.Gparam.LightScatteringID};" +
                    $"{asset.Gparam.EnvMapID};" +
                    $"{asset.Unk8.Unk00};" +

                    $"{asset.Unk10.MapID};" +
                    $"{asset.Unk10.Unk04};" +
                    $"{asset.Unk10.Unk0C};" +
                    $"{asset.Unk10.Unk10};" +
                    $"{asset.Unk10.Unk14};" +

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
