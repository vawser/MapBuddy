using SoulsFormats;
using System;
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

            // Tooltips
            ToolTip tooltip_1 = CreateTooltip(label2, "Example: \n40005000");
            ToolTip tooltip_2 = CreateTooltip(label3, "Example: \n1");
            ToolTip tooltip_3 = CreateTooltip(label4, "Example: \nc2000\nc2000;c2010");


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

        private void buttom_select_folder_Click(object sender, EventArgs e)
        {
            if (dialog_mod_folder.ShowDialog() == DialogResult.OK)
            {
                mod_folder = dialog_mod_folder.SelectedPath;
                textbox_mod_folder.Text = mod_folder;
            }
        }

        private void button_execute_Click(object sender, EventArgs e)
        {
            MapEdit edit = new MapEdit(mod_folder);
            edit.BuildMapDict();
            edit.AddUniqueEntityIDToAll();
            edit.WriteLog();
        }

        private void button_log_entity_ids_Click(object sender, EventArgs e)
        {
            MapEdit edit = new MapEdit(mod_folder);
            edit.BuildMapDict();
            edit.WriteEnemyInfo();
            edit.WriteLog();
        }

        private void button_emevd_Click(object sender, EventArgs e)
        {

        }

        private void button_entity_groups_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(textbox_entity_group_index.Text);
                int entity_id = Convert.ToInt32(textbox_entity_group_id.Text);
                string chrID = textbox_chr_id_limit.Text;

                MapEdit edit = new MapEdit(mod_folder);
                edit.BuildMapDict();
                edit.AddEntityGroupID(entity_id, index, chrID);
                edit.WriteLog();
            }
            catch
            {
                MessageBox.Show($"Paramters not set correctly.", "Error", MessageBoxButtons.OK);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            string version = "0.0.1";
            Text = "Map Buddy - " + version;
        }
    }
}