using MapBuddy.EventInfo;
using MapBuddy.Info;
using MapBuddy.RegionInfo;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SoulsFormats.MSBE.Region;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MapBuddy.Action
{
    internal class MapInfo
    {
        Util logger = new Util();

        private List<string> maps = new List<string>();
        Dictionary<string, string> mapDict = new Dictionary<string, string>();

        public MapInfo(string path, Dictionary<string, bool> part_elements, Dictionary<string, bool> event_elements, Dictionary<string, bool> region_elements, bool isSplitbyMap)
        {
            maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();
            foreach (string map in maps)
            {
                string map_path = map;
                string map_name = Path.GetFileNameWithoutExtension(map);
                mapDict.Add(map_name, map_path);
            }

            // Parts
            foreach (KeyValuePair<string, bool> entry in part_elements)
            {
                string propertyName = entry.Key;
                bool write = entry.Value;

                if(write)
                {
                    if(propertyName == "Asset")
                    {
                        InfoAsset action = new InfoAsset(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Collision")
                    {
                        InfoCollision action = new InfoCollision(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "ConnectCollision")
                    {
                        InfoConnectCollision action = new InfoConnectCollision(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "DummyAsset")
                    {
                        InfoDummyAsset action = new InfoDummyAsset(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "DummyEnemy")
                    {
                        InfoDummyEnemy action = new InfoDummyEnemy(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Enemy")
                    {
                        InfoEnemy action = new InfoEnemy(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MapPiece")
                    {
                        InfoMapPiece action = new InfoMapPiece(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Player")
                    {
                        InfoPlayer action = new InfoPlayer(path);
                        action.Execute(isSplitbyMap);
                    }
                }
            }

            // Events
            foreach (KeyValuePair<string, bool> entry in event_elements)
            {
                string propertyName = entry.Key;
                bool write = entry.Value;

                if (write)
                {
                    if (propertyName == "Generator")
                    {
                        InfoGenerator action = new InfoGenerator(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Mount")
                    {
                        InfoMount action = new InfoMount(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Navmesh")
                    {
                        InfoNavmesh action = new InfoNavmesh(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "ObjAct")
                    {
                        InfoObjAct action = new InfoObjAct(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Other")
                    {
                        InfoOther_event action = new InfoOther_event(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "PatrolInfo")
                    {
                        InfoPatrolInfo action = new InfoPatrolInfo(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "PlatoonInfo")
                    {
                        InfoPlatoonInfo action = new InfoPlatoonInfo(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "PseudoMultiplayer")
                    {
                        InfoPseudoMultiplayer action = new InfoPseudoMultiplayer(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "RetryPoint")
                    {
                        InfoRetryPoint action = new InfoRetryPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "SignPool")
                    {
                        InfoSignPool action = new InfoSignPool(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Treasure")
                    {
                        InfoTreasure action = new InfoTreasure(path);
                        action.Execute(isSplitbyMap);
                    }
                }
            }

            // Region
            foreach (KeyValuePair<string, bool> entry in region_elements)
            {
                string propertyName = entry.Key;
                bool write = entry.Value;

                if (write)
                {
                    if (propertyName == "AutoDrawGroupPoint")
                    {
                        InfoAutoDrawGroupPoint action = new InfoAutoDrawGroupPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "BuddySummonPoint")
                    {
                        InfoBuddySummonPoint action = new InfoBuddySummonPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Connection")
                    {
                        InfoConnection action = new InfoConnection(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Dummy")
                    {
                        InfoDummy action = new InfoDummy(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "EnvironmentMapEffectBox")
                    {
                        InfoEnvironmentMapEffectBox action = new InfoEnvironmentMapEffectBox(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "EnvironmentMapOutput")
                    {
                        InfoEnvironmentMapOutput action = new InfoEnvironmentMapOutput(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "EnvironmentMapPoint")
                    {
                        InfoEnvironmentMapPoint action = new InfoEnvironmentMapPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "FallPreventionRemoval")
                    {
                        InfoFallPreventionRemoval action = new InfoFallPreventionRemoval(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "FastTravelRestriction")
                    {
                        InfoFastTravelRestriction action = new InfoFastTravelRestriction(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "GroupDefeatReward")
                    {
                        InfoGroupDefeatReward action = new InfoGroupDefeatReward(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Hitset")
                    {
                        InfoHitset action = new InfoHitset(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "HorseProhibition")
                    {
                        InfoHorseProhibition action = new InfoHorseProhibition(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "InvasionPoint")
                    {
                        InfoInvasionPoint action = new InfoInvasionPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MapNameOverride")
                    {
                        InfoMapNameOverride action = new InfoMapNameOverride(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MapPoint")
                    {
                        InfoMapPoint action = new InfoMapPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MapPointDiscoveryOverride")
                    {
                        InfoMapPointDiscoveryOverride action = new InfoMapPointDiscoveryOverride(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MapPointParticipationOverride")
                    {
                        InfoMapPointParticipationOverride action = new InfoMapPointParticipationOverride(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Message")
                    {
                        InfoMessage action = new InfoMessage(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MountJump")
                    {
                        InfoMountJump action = new InfoMountJump(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MountJumpFall")
                    {
                        InfoMountJumpFall action = new InfoMountJumpFall(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MufflingBox")
                    {
                        InfoMufflingBox action = new InfoMufflingBox(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MufflingPlane")
                    {
                        InfoMufflingPlane action = new InfoMufflingPlane(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "MufflingPortal")
                    {
                        InfoMufflingPortal action = new InfoMufflingPortal(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "NavmeshCutting")
                    {
                        InfoNavmeshCutting action = new InfoNavmeshCutting(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Other")
                    {
                        InfoOther action = new InfoOther(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "PatrolRoute")
                    {
                        InfoPatrolRoute action = new InfoPatrolRoute(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "PatrolRoute22")
                    {
                        InfoPatrolRoute22 action = new InfoPatrolRoute22(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "PlayArea")
                    {
                        InfoPlayArea action = new InfoPlayArea(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "SFX")
                    {
                        InfoSFX action = new InfoSFX(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "Sound")
                    {
                        InfoSound action = new InfoSound(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "SoundRegion")
                    {
                        InfoSoundRegion action = new InfoSoundRegion(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "SpawnPoint")
                    {
                        InfoSpawnPoint action = new InfoSpawnPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "WeatherCreateAssetPoint")
                    {
                        InfoWeatherCreateAssetPoint action = new InfoWeatherCreateAssetPoint(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "WeatherOverride")
                    {
                        InfoWeatherOverride action = new InfoWeatherOverride(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "WindArea")
                    {
                        InfoWindArea action = new InfoWindArea(path);
                        action.Execute(isSplitbyMap);
                    }
                    if (propertyName == "WindSFX")
                    {
                        InfoWindSFX action = new InfoWindSFX(path);
                        action.Execute(isSplitbyMap);
                    }
                }
            }

            MessageBox.Show($"Finished writing CSV output for selected types.", "Information", MessageBoxButtons.OK);
        }

    }
}
