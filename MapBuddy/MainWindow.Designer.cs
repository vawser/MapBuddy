namespace MapBuddy
{

    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_mod_folder = new System.Windows.Forms.TextBox();
            this.buttom_select_folder = new System.Windows.Forms.Button();
            this.dialog_mod_folder = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_log_entity_ids = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textbox_chr_id_limit = new System.Windows.Forms.TextBox();
            this.textbox_entity_group_index = new System.Windows.Forms.TextBox();
            this.textbox_entity_group_id = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_entity_groups = new System.Windows.Forms.Button();
            this.button_execute = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mod Folder";
            // 
            // textbox_mod_folder
            // 
            this.textbox_mod_folder.Location = new System.Drawing.Point(8, 37);
            this.textbox_mod_folder.Name = "textbox_mod_folder";
            this.textbox_mod_folder.ReadOnly = true;
            this.textbox_mod_folder.Size = new System.Drawing.Size(404, 23);
            this.textbox_mod_folder.TabIndex = 1;
            // 
            // buttom_select_folder
            // 
            this.buttom_select_folder.Location = new System.Drawing.Point(8, 66);
            this.buttom_select_folder.Name = "buttom_select_folder";
            this.buttom_select_folder.Size = new System.Drawing.Size(404, 23);
            this.buttom_select_folder.TabIndex = 3;
            this.buttom_select_folder.Text = "Select Folder";
            this.buttom_select_folder.UseVisualStyleBackColor = true;
            this.buttom_select_folder.Click += new System.EventHandler(this.buttom_select_folder_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textbox_mod_folder);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.buttom_select_folder);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(448, 102);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Configuration";
            // 
            // button_log_entity_ids
            // 
            this.button_log_entity_ids.Location = new System.Drawing.Point(6, 6);
            this.button_log_entity_ids.Name = "button_log_entity_ids";
            this.button_log_entity_ids.Size = new System.Drawing.Size(182, 23);
            this.button_log_entity_ids.TabIndex = 4;
            this.button_log_entity_ids.Text = "Output Entity Information";
            this.button_log_entity_ids.UseVisualStyleBackColor = true;
            this.button_log_entity_ids.Click += new System.EventHandler(this.button_log_entity_ids_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button_log_entity_ids);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textbox_chr_id_limit);
            this.tabPage1.Controls.Add(this.textbox_entity_group_index);
            this.tabPage1.Controls.Add(this.textbox_entity_group_id);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button_entity_groups);
            this.tabPage1.Controls.Add(this.button_execute);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(408, 303);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Enemy";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(358, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Writes all enemy entity information to the Log folder, split by map.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 15);
            this.label6.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Applies an unique Entity ID to all enemies that lack them.";
            // 
            // textbox_chr_id_limit
            // 
            this.textbox_chr_id_limit.Location = new System.Drawing.Point(6, 265);
            this.textbox_chr_id_limit.Name = "textbox_chr_id_limit";
            this.textbox_chr_id_limit.Size = new System.Drawing.Size(182, 23);
            this.textbox_chr_id_limit.TabIndex = 9;
            // 
            // textbox_entity_group_index
            // 
            this.textbox_entity_group_index.Location = new System.Drawing.Point(194, 218);
            this.textbox_entity_group_index.Name = "textbox_entity_group_index";
            this.textbox_entity_group_index.Size = new System.Drawing.Size(182, 23);
            this.textbox_entity_group_index.TabIndex = 7;
            // 
            // textbox_entity_group_id
            // 
            this.textbox_entity_group_id.Location = new System.Drawing.Point(6, 218);
            this.textbox_entity_group_id.Name = "textbox_entity_group_id";
            this.textbox_entity_group_id.Size = new System.Drawing.Size(182, 23);
            this.textbox_entity_group_id.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Limit by ChrID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Entity Group Index";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Entity Group ID";
            // 
            // button_entity_groups
            // 
            this.button_entity_groups.Location = new System.Drawing.Point(6, 139);
            this.button_entity_groups.Name = "button_entity_groups";
            this.button_entity_groups.Size = new System.Drawing.Size(182, 23);
            this.button_entity_groups.TabIndex = 3;
            this.button_entity_groups.Text = "Add Entity Group ID";
            this.button_entity_groups.UseVisualStyleBackColor = true;
            this.button_entity_groups.Click += new System.EventHandler(this.button_entity_groups_Click);
            // 
            // button_execute
            // 
            this.button_execute.Location = new System.Drawing.Point(6, 73);
            this.button_execute.Name = "button_execute";
            this.button_execute.Size = new System.Drawing.Size(182, 23);
            this.button_execute.TabIndex = 2;
            this.button_execute.Text = "Add Unique Entity ID to All";
            this.button_execute.UseVisualStyleBackColor = true;
            this.button_execute.Click += new System.EventHandler(this.button_execute_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 129);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(416, 331);
            this.tabControl1.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(374, 30);
            this.label8.TabIndex = 13;
            this.label8.Text = "Add the specified Entity Group ID at the specified index.\r\nYou may limit the assi" +
    "gnment to specific ChrID by listing them below.";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 473);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Map Buddy";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private TextBox textbox_mod_folder;
        private Button buttom_select_folder;
        private FolderBrowserDialog dialog_mod_folder;
        private GroupBox groupBox3;
        private Button button_log_entity_ids;
        private TabPage tabPage1;
        private Label label6;
        private Label label5;
        private TextBox textbox_chr_id_limit;
        private TextBox textbox_entity_group_index;
        private TextBox textbox_entity_group_id;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button button_entity_groups;
        private Button button_execute;
        private TabControl tabControl1;
        private Label label7;
        private Label label8;
    }
}