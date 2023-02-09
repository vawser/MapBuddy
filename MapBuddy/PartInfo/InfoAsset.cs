using SoulsFormats;
using SoulsFormats.KF4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy.Info
{
    internal class InfoAsset
    {
        Logger logger = new Logger();

        string output_dir = "";
        string header = "";
        string combined_file_name = "Global_Asset";
        string file_format = "csv";

        public InfoAsset(string path, Dictionary<string, string> mapDict, bool splitByMap)
        {
            output_dir = logger.GetLogDir() + "\\Part\\Asset\\";

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

                    $"Unk2.Condition;" +
                    $"Unk2.DispGroups[0];" +
                    $"Unk2.DispGroups[1];" +
                    $"Unk2.DispGroups[2];" +
                    $"Unk2.DispGroups[3];" +
                    $"Unk2.DispGroups[4];" +
                    $"Unk2.DispGroups[5];" +
                    $"Unk2.DispGroups[6];" +
                    $"Unk2.DispGroups[7];" +

                    $"Unk2.Unk24;" +
                    $"Unk2.Unk26;" +

                    $"Gparam.LightSetID;" +
                    $"Gparam.FogParamID;" +
                    $"Gparam.LightScatteringID;" +
                    $"Gparam.EnvMapID;" +

                    $"Unk7.Unk00;" +
                    $"Unk7.Unk04;" +
                    $"Unk7.Unk08;" +
                    $"Unk7.Unk0C;" +
                    $"Unk7.Unk10;" +
                    $"Unk7.Unk14;" +
                    $"Unk7.Unk18;" +
                    $"Unk8.Unk00;" +
                    $"Unk9.Unk00;" +

                    $"Unk10.MapID;" +
                    $"Unk10.Unk04;" +
                    $"Unk10.Unk0C;" +
                    $"Unk10.Unk10;" +
                    $"Unk10.Unk14;" +

                    $"UnkT02;" +
                    $"UnkT10;" +
                    $"UnkT11;" +
                    $"UnkT12;" +
                    $"AssetSfxParamRelativeID;" +
                    $"UnkT1E;" +
                    $"UnkT24;" +
                    $"UnkT28;" +
                    $"UnkT30;" +
                    $"UnkT34;" +
                    $"UnkPartNames[0];" +
                    $"UnkPartNames[1];" +
                    $"UnkPartNames[2];" +
                    $"UnkPartNames[3];" +
                    $"UnkPartNames[4];" +
                    $"UnkPartNames[5];" +
                    $"UnkT50;" +
                    $"UnkT51;" +
                    $"UnkT53;" +
                    $"UnkT54;" +
                    $"UnkT58;" +
                    $"UnkT5C;" +
                    $"UnkT60;" +
                    $"UnkT64;" +

                    $"AssetUnk1.Unk00;" +
                    $"AssetUnk1.Unk04;" +
                    $"AssetUnk1.DisableTorrentAssetOnly;" +
                    $"AssetUnk1.Unk1C;" +
                    $"AssetUnk1.Unk24;" +
                    $"AssetUnk1.Unk26;" +
                    $"AssetUnk1.Unk28;" +
                    $"AssetUnk1.Unk2C;" +

                    $"AssetUnk2.Unk00;" +
                    $"AssetUnk2.Unk04;" +
                    $"AssetUnk2.Unk14;" +
                    $"AssetUnk2.Unk1C;" +
                    $"AssetUnk2.Unk1D;" +
                    $"AssetUnk2.Unk1E;" +
                    $"AssetUnk2.Unk1F;" +

                    $"AssetUnk3.Unk00;" +
                    $"AssetUnk3.Unk04;" +
                    $"AssetUnk3.Unk09;" +
                    $"AssetUnk3.Unk0A;" +
                    $"AssetUnk3.Unk0B;" +
                    $"AssetUnk3.Unk0C;" +
                    $"AssetUnk3.Unk0E;" +
                    $"AssetUnk3.Unk10;" +
                    $"AssetUnk3.Unk14;" +
                    $"AssetUnk3.Unk18;" +
                    $"AssetUnk3.Unk1C;" +
                    $"AssetUnk3.Unk20;" +
                    $"AssetUnk3.Unk24;" +
                    $"AssetUnk3.Unk25;" +

                    $"AssetUnk4.Unk00;" +
                    $"AssetUnk4.Unk01;" +
                    $"AssetUnk4.Unk02;" +
                    $"AssetUnk4.Unk03;" +

                    $"\n";

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

            logger.AddToLog($"Written Asset data to {output_dir}");
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

            foreach (MSBE.Part.Asset asset in msb.Parts.Assets)
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

                    $"{asset.Unk2.Condition};" +
                    $"{asset.Unk2.DispGroups[0]};" +
                    $"{asset.Unk2.DispGroups[1]};" +
                    $"{asset.Unk2.DispGroups[2]};" +
                    $"{asset.Unk2.DispGroups[3]};" +
                    $"{asset.Unk2.DispGroups[4]};" +
                    $"{asset.Unk2.DispGroups[5]};" +
                    $"{asset.Unk2.DispGroups[6]};" +
                    $"{asset.Unk2.DispGroups[7]};" +

                    $"{asset.Unk2.Unk24};" +
                    $"{asset.Unk2.Unk26};" +

                    $"{asset.Gparam.LightSetID};" +
                    $"{asset.Gparam.FogParamID};" +
                    $"{asset.Gparam.LightScatteringID};" +
                    $"{asset.Gparam.EnvMapID};" +

                    $"{asset.Unk7.Unk00};" +
                    $"{asset.Unk7.Unk04};" +
                    $"{asset.Unk7.Unk08};" +
                    $"{asset.Unk7.Unk0C};" +
                    $"{asset.Unk7.Unk10};" +
                    $"{asset.Unk7.Unk14};" +
                    $"{asset.Unk7.Unk18};" +
                    $"{asset.Unk8.Unk00};" +
                    $"{asset.Unk9.Unk00};" +

                    $"{asset.Unk10.MapID};" +
                    $"{asset.Unk10.Unk04};" +
                    $"{asset.Unk10.Unk0C};" +
                    $"{asset.Unk10.Unk10};" +
                    $"{asset.Unk10.Unk14};" +

                    $"{asset.UnkT02};" +
                    $"{asset.UnkT10};" +
                    $"{asset.UnkT11};" +
                    $"{asset.UnkT12};" +
                    $"{asset.AssetSfxParamRelativeID};" +
                    $"{asset.UnkT1E};" +
                    $"{asset.UnkT24};" +
                    $"{asset.UnkT28};" +
                    $"{asset.UnkT30};" +
                    $"{asset.UnkT34};" +
                    $"{asset.UnkPartNames[0]};" +
                    $"{asset.UnkPartNames[1]};" +
                    $"{asset.UnkPartNames[2]};" +
                    $"{asset.UnkPartNames[3]};" +
                    $"{asset.UnkPartNames[4]};" +
                    $"{asset.UnkPartNames[5]};" +
                    $"{asset.UnkT50};" +
                    $"{asset.UnkT51};" +
                    $"{asset.UnkT53};" +
                    $"{asset.UnkT54};" +
                    $"{asset.UnkT58};" +
                    $"{asset.UnkT5C};" +
                    $"{asset.UnkT60};" +
                    $"{asset.UnkT64};" +

                    $"{asset.AssetUnk1.Unk00};" +
                    $"{asset.AssetUnk1.Unk04};" +
                    $"{asset.AssetUnk1.DisableTorrentAssetOnly};" +
                    $"{asset.AssetUnk1.Unk1C};" +
                    $"{asset.AssetUnk1.Unk24};" +
                    $"{asset.AssetUnk1.Unk26};" +
                    $"{asset.AssetUnk1.Unk28};" +
                    $"{asset.AssetUnk1.Unk2C};" +

                    $"{asset.AssetUnk2.Unk00};" +
                    $"{asset.AssetUnk2.Unk04};" +
                    $"{asset.AssetUnk2.Unk14};" +
                    $"{asset.AssetUnk2.Unk1C};" +
                    $"{asset.AssetUnk2.Unk1D};" +
                    $"{asset.AssetUnk2.Unk1E};" +
                    $"{asset.AssetUnk2.Unk1F};" +

                    $"{asset.AssetUnk3.Unk00};" +
                    $"{asset.AssetUnk3.Unk04};" +
                    $"{asset.AssetUnk3.Unk09};" +
                    $"{asset.AssetUnk3.Unk0A};" +
                    $"{asset.AssetUnk3.Unk0B};" +
                    $"{asset.AssetUnk3.Unk0C};" +
                    $"{asset.AssetUnk3.Unk0E};" +
                    $"{asset.AssetUnk3.Unk10};" +
                    $"{asset.AssetUnk3.Unk14};" +
                    $"{asset.AssetUnk3.Unk18};" +
                    $"{asset.AssetUnk3.Unk1C};" +
                    $"{asset.AssetUnk3.Unk20};" +
                    $"{asset.AssetUnk3.Unk24};" +
                    $"{asset.AssetUnk3.Unk25};" +

                    $"{asset.AssetUnk4.Unk00};" +
                    $"{asset.AssetUnk4.Unk01};" +
                    $"{asset.AssetUnk4.Unk02};" +
                    $"{asset.AssetUnk4.Unk03};" +

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
