using MapBuddy.Action;
using MapBuddy.EventInfo;
using MapBuddy.Info;
using MapBuddy.RegionInfo;
using SoulsFormats;
using SoulsFormats.KF4;
using System;
using System.IO;
using System.Reflection;

namespace MapBuddy
{
    public partial class MainWindow : Form
    {
        string mod_folder = null;
        string log_dir = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";

        public MainWindow()
        {
            InitializeComponent();

            //mod_folder = "";
            mod_folder = "C:\\Users\\Xylozi\\Documents\\C# Projects\\MapBuddy\\MapBuddy\\bin\\Debug\\net7.0-windows\\example_mod";

            textbox_mod_folder.Text = mod_folder;

            bool exists = System.IO.Directory.Exists(log_dir);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(log_dir);
            }

            File.WriteAllText(log_dir + $"log.txt", "");

            // Defaults
            c_entityid_enemy.Checked = true;
            t_entityid_start.Text = "100";
            t_entityid_end.Text = "800";

            c_entitygroup_enemy.Checked = true;
            t_entitygroup_id.Text = "40005000";
            t_entitygroup_index.Text = "0";

            c_ignore_boss.Checked = true;
            c_ignore_player.Checked = true;
            c_ignore_passive.Checked = true;
            c_ignore_script.Checked = true;

            t_dupe_count.Text = "1";

            UpdateMapSelection(mod_folder);
        }
        private void UpdateMapSelection(string path)
        {
            List<string> maps = Directory.GetFileSystemEntries(path + "\\map\\mapstudio", @"*.msb.dcx").ToList();

            cb_map_select.Items.Clear();

            if (maps.Count > 0)
            {
                cb_map_select.Items.Add("All");
                cb_map_select.SelectedIndex = cb_map_select.FindStringExact("All");

                foreach (string map in maps)
                {
                    string map_path = map;
                    string map_name = Path.GetFileNameWithoutExtension(map);

                    cb_map_select.Items.Add(map_name);
                }
            }
            else
            {
                cb_map_select.Items.Add("None");
                cb_map_select.SelectedIndex = cb_map_select.FindStringExact("None");
            }
        }

        private ToolTip CreateTooltip(Label label, string text)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            tooltip.ShowAlways = true;

            tooltip.SetToolTip(label, text);

            return tooltip;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            string version = "0.0.3";
            Text = "Map Buddy - " + version;
        }

        private void buttom_select_folder_Click(object sender, EventArgs e)
        {
            if (dialog_mod_folder.ShowDialog() == DialogResult.OK)
            {
                mod_folder = dialog_mod_folder.SelectedPath;
                textbox_mod_folder.Text = mod_folder;

                UpdateMapSelection(mod_folder);
            }
        }

        private void b_enemy_dupe_Click(object sender, EventArgs e)
        {
            string entity_group_id = t_dupe_entity_group_id.Text;
            string dupe_count = t_dupe_count.Text;

             if (!IsDigitsOnly(entity_group_id))
            {
                MessageBox.Show($"Entity Group ID is not numeric.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (!IsDigitsOnly(dupe_count))
            {
                MessageBox.Show($"Dupe Count is not numeric.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (dupe_count == "")
            {
                MessageBox.Show($"Dupe Count is empty.", "Error", MessageBoxButtons.OK);
                return;
            }

            EnemyDupe action = new EnemyDupe(
                cb_map_select.Text,
                mod_folder,
                t_dupe_count.Text,
                c_dupe_ignore_list.Checked,
                t_dupe_ignore_list.Text,
                c_dupe_include_list.Checked,
                t_dupe_include_list.Text,
                c_ignore_boss.Checked,
                c_ignore_player.Checked,
                c_ignore_passive.Checked,
                c_ignore_script.Checked,
                t_dupe_entity_group_id.Text
            );
        }

        private void b_uniqueEntity_action_Click(object sender, EventArgs e)
        {
            string start_id = t_entityid_start.Text;
            string end_id = t_entityid_end.Text;

            if (!c_entityid_asset.Checked && !c_entityid_enemy.Checked && !c_entityid_player.Checked)
            {
                MessageBox.Show($"No property type selected.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if(!IsDigitsOnly(start_id))
            {
                MessageBox.Show($"Start ID is not numeric.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (!IsDigitsOnly(end_id))
            {
                MessageBox.Show($"End ID is not numeric.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (start_id == "" || end_id == "")
            {
                MessageBox.Show($"Start/End ID is empty.", "Error", MessageBoxButtons.OK);
                return;
            }

            int start_id_num = Convert.ToInt32(start_id);
            int end_id_num = Convert.ToInt32(end_id);

            if(end_id_num < start_id_num)
            {
                MessageBox.Show($"End ID is smaller than Start ID.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (end_id_num == start_id_num)
            {
                MessageBox.Show($"End ID is equal to the Start ID.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (start_id_num >= 10000)
            {
                MessageBox.Show($"Start ID cannot be bigger than 10,000.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (end_id_num >= 10000)
            {
                MessageBox.Show($"Start ID cannot be bigger than 10,000.", "Error", MessageBoxButtons.OK);
                return;
            }

            EntityID action = new EntityID(
                cb_map_select.Text,
                mod_folder, 
                c_entityid_asset.Checked, 
                c_entityid_enemy.Checked, 
                c_entityid_player.Checked,
                c_override_existing.Checked, 
                Convert.ToInt32(t_entityid_start.Text),
                Convert.ToInt32(t_entityid_end.Text)
            );
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void b_entityGroup_action_Click(object sender, EventArgs e)
        {
            if (!c_entitygroup_asset.Checked && !c_entitygroup_enemy.Checked)
            {
                MessageBox.Show($"No property type selected.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (!IsDigitsOnly(t_entitygroup_id.Text))
            {
                MessageBox.Show($"Entity Group ID is not numeric.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (t_entitygroup_id.Text == "")
            {
                MessageBox.Show($"Entity Group ID is empty.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (!IsDigitsOnly(t_entitygroup_index.Text))
            {
                MessageBox.Show($"Entity Group Index is not numeric.", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (t_entitygroup_index.Text == "")
            {
                MessageBox.Show($"Entity Group Index is empty.", "Error", MessageBoxButtons.OK);
                return;
            }

            try
            {
                int entitygroup_id = Convert.ToInt32(t_entitygroup_id.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Entity Group ID is too high.", "Error", MessageBoxButtons.OK);
                return;
            }

            try
            {
                int entitygroup_index = Convert.ToInt32(t_entitygroup_index.Text);

                if (entitygroup_index > 7 || entitygroup_index < 0)
                {
                    MessageBox.Show($"Entity Group Index must be between 0 and 7.", "Error", MessageBoxButtons.OK);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Entity Group ID is too high.", "Error", MessageBoxButtons.OK);
                return;
            }

            EntityGroupID action = new EntityGroupID(
                cb_map_select.Text,
                mod_folder,
                c_entitygroup_asset.Checked,
                c_entitygroup_enemy.Checked,
                c_entitygroup_replace_existing.Checked,
                t_entitygroup_id.Text,
                t_entitygroup_index.Text,
                c_limit_asset_modelname.Checked,
                t_limit_asset_modelname.Text,
                c_limit_enemy_modelname.Checked,
                t_limit_enemy_modelname.Text,
                c_limit_enemy_npcparam.Checked,
                t_limit_enemy_npcparam.Text
            );
        }

        private void b_output_csv_Click(object sender, EventArgs e)
        {
            // Build dictionaries to hold the object types to export
            Dictionary<string, bool> part_elements = new Dictionary<string, bool>();
            Dictionary<string, bool> event_elements = new Dictionary<string, bool>();
            Dictionary<string, bool> region_elements = new Dictionary<string, bool>();

            // Parts
            part_elements.Add("Asset", c_part_asset.Checked);
            part_elements.Add("Collision", c_part_collision.Checked);
            part_elements.Add("ConnectCollision", c_part_connectcollision.Checked);
            part_elements.Add("DummyAsset", c_part_dummyasset.Checked);
            part_elements.Add("DummyEnemy", c_part_dummyenemy.Checked);
            part_elements.Add("Enemy", c_part_enemy.Checked);
            part_elements.Add("MapPiece", c_part_mappiece.Checked);
            part_elements.Add("Player", c_part_player.Checked);

            // Events
            event_elements.Add("Generator", c_event_generator.Checked);
            event_elements.Add("Mount", c_event_mount.Checked);
            event_elements.Add("Navmesh", c_event_navmesh.Checked);
            event_elements.Add("ObjAct", c_event_objact.Checked);
            event_elements.Add("Other", c_event_other.Checked);
            event_elements.Add("PatrolInfo", c_event_patrolinfo.Checked);
            event_elements.Add("PlatoonInfo", c_event_platooninfo.Checked);
            event_elements.Add("PseudoMultiplayer", c_event_pseudomultiplayer.Checked);
            event_elements.Add("RetryPoint", c_event_retrypoint.Checked);
            event_elements.Add("SignPool", c_event_signpool.Checked);
            event_elements.Add("Treasure", c_event_treasure.Checked);

            // Regions
            region_elements.Add("AutoDrawGroupPoint", c_region_autodrawgrouppoint.Checked);
            region_elements.Add("BuddySummonPoint", c_region_buddysummonpoint.Checked);
            region_elements.Add("Connection", c_region_connection.Checked);
            region_elements.Add("Dummy", c_region_dummy.Checked);
            region_elements.Add("EnvironmentMapEffectBox", c_region_environmentmapeffectbox.Checked);
            region_elements.Add("EnvironmentMapOutput", c_region_environmentmapoutput.Checked);
            region_elements.Add("EnvironmentMapPoint", c_region_environmentmappoint.Checked);
            region_elements.Add("FallPreventionRemoval", c_region_fallpreventionremoval.Checked);
            region_elements.Add("FastTravelRestriction", c_region_fasttravelrestriction.Checked);
            region_elements.Add("GroupDefeatReward", c_region_groupdefeatreward.Checked);
            region_elements.Add("Hitset", c_region_hitset.Checked);
            region_elements.Add("HorseProhibition", c_region_horseprohibition.Checked);
            region_elements.Add("InvasionPoint", c_region_invasionpoint.Checked);
            region_elements.Add("MapNameOverride", c_region_mapnameoverride.Checked);
            region_elements.Add("MapPoint", c_region_mappoint.Checked);
            region_elements.Add("MapPointDiscoveryOverride", c_region_mappointdiscoveryoverride.Checked);
            region_elements.Add("MapPointParticipationOverride", c_region_mappointparticipationoverride.Checked);
            region_elements.Add("Message", c_region_message.Checked);
            region_elements.Add("MountJump", c_region_mountjump.Checked);
            region_elements.Add("MountJumpFall", c_region_mountjumpfall.Checked);
            region_elements.Add("MufflingBox", c_region_mufflingbox.Checked);
            region_elements.Add("MufflingPlane", c_region_mufflingplane.Checked);
            region_elements.Add("MufflingPortal", c_region_mufflingportal.Checked);
            region_elements.Add("NavmeshCutting", c_region_navmeshcutting.Checked);
            region_elements.Add("Other", c_region_other.Checked);
            region_elements.Add("PatrolRoute", c_region_patrolroute.Checked);
            region_elements.Add("PatrolRoute22", c_region_patrolroute22.Checked);
            region_elements.Add("PlayArea", c_region_playarea.Checked);
            region_elements.Add("SFX", c_region_sfx.Checked);
            region_elements.Add("Sound", c_region_sound.Checked);
            region_elements.Add("SoundRegion", c_region_soundregion.Checked);
            region_elements.Add("SpawnPoint", c_region_spawnpoint.Checked);
            region_elements.Add("WeatherCreateAssetPoint", c_region_weathercreateassetpoint.Checked);
            region_elements.Add("WeatherOverride", c_region_weatheroverride.Checked);
            region_elements.Add("WindArea", c_region_windarea.Checked);
            region_elements.Add("WindSFX", c_region_windsfx.Checked);

            MapInfo action = new MapInfo(
                cb_map_select.Text, 
                mod_folder, 
                part_elements, 
                event_elements, 
                region_elements, 
                c_split_by_map.Checked
            );
        }

        private void b_tick_all_parts_Click(object sender, EventArgs e)
        {
            c_part_asset.Checked = !c_part_asset.Checked;
            c_part_collision.Checked = !c_part_collision.Checked;
            c_part_connectcollision.Checked = !c_part_connectcollision.Checked;
            c_part_dummyasset.Checked = !c_part_dummyasset.Checked;
            c_part_dummyenemy.Checked = !c_part_dummyenemy.Checked;
            c_part_enemy.Checked = !c_part_enemy.Checked;
            c_part_mappiece.Checked = !c_part_mappiece.Checked;
            c_part_player.Checked = !c_part_player.Checked;

        }

        private void b_tick_all_events_Click(object sender, EventArgs e)
        {
            c_event_generator.Checked = !c_event_generator.Checked;
            c_event_mount.Checked = !c_event_mount.Checked;
            c_event_navmesh.Checked = !c_event_navmesh.Checked;
            c_event_objact.Checked = !c_event_objact.Checked;
            c_event_other.Checked = !c_event_other.Checked;
            c_event_patrolinfo.Checked = !c_event_patrolinfo.Checked;
            c_event_platooninfo.Checked = !c_event_platooninfo.Checked;
            c_event_pseudomultiplayer.Checked = !c_event_pseudomultiplayer.Checked;
            c_event_retrypoint.Checked = !c_event_retrypoint.Checked;
            c_event_signpool.Checked = !c_event_signpool.Checked;
            c_event_treasure.Checked = !c_event_treasure.Checked;
        }

        private void b_tick_all_regions_Click(object sender, EventArgs e)
        {
            c_region_autodrawgrouppoint.Checked = !c_region_autodrawgrouppoint.Checked;
            c_region_buddysummonpoint.Checked = !c_region_buddysummonpoint.Checked;
            c_region_connection.Checked = !c_region_connection.Checked;
            c_region_dummy.Checked = !c_region_dummy.Checked;
            c_region_environmentmapeffectbox.Checked = !c_region_environmentmapeffectbox.Checked;
            c_region_environmentmapoutput.Checked = !c_region_environmentmapoutput.Checked;
            c_region_environmentmappoint.Checked = !c_region_environmentmappoint.Checked;
            c_region_fallpreventionremoval.Checked = !c_region_fallpreventionremoval.Checked;
            c_region_fasttravelrestriction.Checked = !c_region_fasttravelrestriction.Checked;
            c_region_groupdefeatreward.Checked = !c_region_groupdefeatreward.Checked;
            c_region_hitset.Checked = !c_region_hitset.Checked;
            c_region_horseprohibition.Checked = !c_region_horseprohibition.Checked;
            c_region_invasionpoint.Checked = !c_region_invasionpoint.Checked;
            c_region_mapnameoverride.Checked = !c_region_mapnameoverride.Checked;
            c_region_mappoint.Checked = !c_region_mappoint.Checked;
            c_region_mappointdiscoveryoverride.Checked = !c_region_mappointdiscoveryoverride.Checked;
            c_region_mappointparticipationoverride.Checked = !c_region_mappointparticipationoverride.Checked;
            c_region_message.Checked = !c_region_message.Checked;
            c_region_mountjump.Checked = !c_region_mountjump.Checked;
            c_region_mountjumpfall.Checked = !c_region_mountjumpfall.Checked;
            c_region_mufflingbox.Checked = !c_region_mufflingbox.Checked;
            c_region_mufflingplane.Checked = !c_region_mufflingplane.Checked;
            c_region_mufflingportal.Checked = !c_region_mufflingportal.Checked;
            c_region_navmeshcutting.Checked = !c_region_navmeshcutting.Checked;
            c_region_other.Checked = !c_region_other.Checked;
            c_region_patrolroute.Checked = !c_region_patrolroute.Checked;
            c_region_patrolroute22.Checked = !c_region_patrolroute22.Checked;
            c_region_playarea.Checked = !c_region_playarea.Checked;
            c_region_sfx.Checked = !c_region_sfx.Checked;
            c_region_sound.Checked = !c_region_sound.Checked;
            c_region_soundregion.Checked = !c_region_soundregion.Checked;
            c_region_spawnpoint.Checked = !c_region_spawnpoint.Checked;
            c_region_weathercreateassetpoint.Checked = !c_region_weathercreateassetpoint.Checked;
            c_region_weatheroverride.Checked = !c_region_weatheroverride.Checked;
            c_region_windarea.Checked = !c_region_windarea.Checked;
            c_region_windsfx.Checked = !c_region_windsfx.Checked;
        }

        private void c_entityid_asset_CheckedChanged(object sender, EventArgs e)
        {
            if (c_entityid_asset.Checked)
            {
                c_entityid_enemy.Checked = false;
                c_entityid_player.Checked = false;
            }
        }

        private void c_entityid_enemy_CheckedChanged(object sender, EventArgs e)
        {
            if (c_entityid_enemy.Checked)
            {
                c_entityid_asset.Checked = false;
                c_entityid_player.Checked = false;
            }
        }

        private void c_entityid_player_CheckedChanged(object sender, EventArgs e)
        {
            if (c_entityid_player.Checked)
            {
                c_entityid_enemy.Checked = false;
                c_entityid_asset.Checked = false;
            }
        }

        private void c_limit_enemy_npcparam_CheckedChanged(object sender, EventArgs e)
        {
            if (c_limit_enemy_npcparam.Checked)
            {
                c_limit_enemy_modelname.Checked = false;
            }
        }

        private void c_limit_enemy_modelname_CheckedChanged(object sender, EventArgs e)
        {
            if (c_limit_enemy_modelname.Checked)
            {
                c_limit_enemy_npcparam.Checked = false;
            }
        }

        private void c_entitygroup_asset_CheckedChanged(object sender, EventArgs e)
        {
            if (c_entitygroup_asset.Checked)
            {
                c_entitygroup_enemy.Checked = false;
            }
        }

        private void c_entitygroup_enemy_CheckedChanged(object sender, EventArgs e)
        {
            if (c_entitygroup_enemy.Checked)
            {
                c_entitygroup_asset.Checked = false;
            }
        }

        private void c_dupe_ignore_list_CheckedChanged(object sender, EventArgs e)
        {
            if (c_dupe_ignore_list.Checked)
            {
                c_dupe_include_list.Checked = false;
            }
        }

        private void c_dupe_include_list_CheckedChanged(object sender, EventArgs e)
        {
            if (c_dupe_include_list.Checked)
            {
                c_dupe_ignore_list.Checked = false;
            }
        }
    }
}
