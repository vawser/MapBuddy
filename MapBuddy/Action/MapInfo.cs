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
        Logger logger = new Logger();
        Util util = new Util();

        Dictionary<string, string> map_dict = new Dictionary<string, string>();

        public MapInfo(string map_selection, string path, Dictionary<string, bool> part_elements, Dictionary<string, bool> event_elements, Dictionary<string, bool> region_elements, bool isSplitbyMap)
        {
            map_dict = util.GetMapSelection(map_selection, path, logger);

            // Parts
            foreach (KeyValuePair<string, bool> entry in part_elements)
            {
                string propertyName = entry.Key;
                bool write = entry.Value;

                if(write)
                {
                    if(propertyName == "Asset")
                    {
                        InfoAsset action = new InfoAsset(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Collision")
                    {
                        InfoCollision action = new InfoCollision(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "ConnectCollision")
                    {
                        InfoConnectCollision action = new InfoConnectCollision(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "DummyAsset")
                    {
                        InfoDummyAsset action = new InfoDummyAsset(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "DummyEnemy")
                    {
                        InfoDummyEnemy action = new InfoDummyEnemy(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Enemy")
                    {
                        InfoEnemy action = new InfoEnemy(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MapPiece")
                    {
                        InfoMapPiece action = new InfoMapPiece(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Player")
                    {
                        InfoPlayer action = new InfoPlayer(path, map_dict, isSplitbyMap);
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
                        InfoGenerator action = new InfoGenerator(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Mount")
                    {
                        InfoMount action = new InfoMount(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Navmesh")
                    {
                        InfoNavmesh action = new InfoNavmesh(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "ObjAct")
                    {
                        InfoObjAct action = new InfoObjAct(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Other")
                    {
                        InfoOther_event action = new InfoOther_event(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "PatrolInfo")
                    {
                        InfoPatrolInfo action = new InfoPatrolInfo(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "PlatoonInfo")
                    {
                        InfoPlatoonInfo action = new InfoPlatoonInfo(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "PseudoMultiplayer")
                    {
                        InfoPseudoMultiplayer action = new InfoPseudoMultiplayer(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "RetryPoint")
                    {
                        InfoRetryPoint action = new InfoRetryPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "SignPool")
                    {
                        InfoSignPool action = new InfoSignPool(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Treasure")
                    {
                        InfoTreasure action = new InfoTreasure(path, map_dict, isSplitbyMap);
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
                        InfoAutoDrawGroupPoint action = new InfoAutoDrawGroupPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "BuddySummonPoint")
                    {
                        InfoBuddySummonPoint action = new InfoBuddySummonPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Connection")
                    {
                        InfoConnection action = new InfoConnection(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Dummy")
                    {
                        InfoDummy action = new InfoDummy(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "EnvironmentMapEffectBox")
                    {
                        InfoEnvironmentMapEffectBox action = new InfoEnvironmentMapEffectBox(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "EnvironmentMapOutput")
                    {
                        InfoEnvironmentMapOutput action = new InfoEnvironmentMapOutput(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "EnvironmentMapPoint")
                    {
                        InfoEnvironmentMapPoint action = new InfoEnvironmentMapPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "FallPreventionRemoval")
                    {
                        InfoFallPreventionRemoval action = new InfoFallPreventionRemoval(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "FastTravelRestriction")
                    {
                        InfoFastTravelRestriction action = new InfoFastTravelRestriction(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "GroupDefeatReward")
                    {
                        InfoGroupDefeatReward action = new InfoGroupDefeatReward(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Hitset")
                    {
                        InfoHitset action = new InfoHitset(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "HorseProhibition")
                    {
                        InfoHorseProhibition action = new InfoHorseProhibition(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "InvasionPoint")
                    {
                        InfoInvasionPoint action = new InfoInvasionPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MapNameOverride")
                    {
                        InfoMapNameOverride action = new InfoMapNameOverride(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MapPoint")
                    {
                        InfoMapPoint action = new InfoMapPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MapPointDiscoveryOverride")
                    {
                        InfoMapPointDiscoveryOverride action = new InfoMapPointDiscoveryOverride(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MapPointParticipationOverride")
                    {
                        InfoMapPointParticipationOverride action = new InfoMapPointParticipationOverride(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Message")
                    {
                        InfoMessage action = new InfoMessage(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MountJump")
                    {
                        InfoMountJump action = new InfoMountJump(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MountJumpFall")
                    {
                        InfoMountJumpFall action = new InfoMountJumpFall(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MufflingBox")
                    {
                        InfoMufflingBox action = new InfoMufflingBox(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MufflingPlane")
                    {
                        InfoMufflingPlane action = new InfoMufflingPlane(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "MufflingPortal")
                    {
                        InfoMufflingPortal action = new InfoMufflingPortal(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "NavmeshCutting")
                    {
                        InfoNavmeshCutting action = new InfoNavmeshCutting(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Other")
                    {
                        InfoOther action = new InfoOther(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "PatrolRoute")
                    {
                        InfoPatrolRoute action = new InfoPatrolRoute(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "PatrolRoute22")
                    {
                        InfoPatrolRoute22 action = new InfoPatrolRoute22(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "PlayArea")
                    {
                        InfoPlayArea action = new InfoPlayArea(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "SFX")
                    {
                        InfoSFX action = new InfoSFX(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "Sound")
                    {
                        InfoSound action = new InfoSound(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "SoundRegion")
                    {
                        InfoSoundRegion action = new InfoSoundRegion(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "SpawnPoint")
                    {
                        InfoSpawnPoint action = new InfoSpawnPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "WeatherCreateAssetPoint")
                    {
                        InfoWeatherCreateAssetPoint action = new InfoWeatherCreateAssetPoint(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "WeatherOverride")
                    {
                        InfoWeatherOverride action = new InfoWeatherOverride(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "WindArea")
                    {
                        InfoWindArea action = new InfoWindArea(path, map_dict, isSplitbyMap);
                    }
                    if (propertyName == "WindSFX")
                    {
                        InfoWindSFX action = new InfoWindSFX(path, map_dict, isSplitbyMap);
                    }
                }
            }

            MessageBox.Show($"Finished writing CSV output for selected types.", "Information", MessageBoxButtons.OK);
        }

    }
}
