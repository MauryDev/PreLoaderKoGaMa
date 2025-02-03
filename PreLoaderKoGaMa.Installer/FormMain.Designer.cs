namespace PreLoaderKoGaMa.Installer
{
    partial class FormMain
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
            checkBox_br = new CheckBox();
            checkBox_www = new CheckBox();
            checkBox_friends = new CheckBox();
            label1 = new Label();
            button_install = new Button();
            checkBox4_custom = new CheckBox();
            textBox_custom = new TextBox();
            button_selectcustomfolder = new Button();
            button_uninstall = new Button();
            label2 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            backgroundWorker_install = new System.ComponentModel.BackgroundWorker();
            button2 = new Button();
            SuspendLayout();
            // 
            // checkBox_br
            // 
            checkBox_br.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox_br.ForeColor = SystemColors.Control;
            checkBox_br.Image = Properties.Resources.brasil;
            checkBox_br.ImageAlign = ContentAlignment.MiddleRight;
            checkBox_br.Location = new Point(9, 37);
            checkBox_br.Name = "checkBox_br";
            checkBox_br.Size = new Size(88, 40);
            checkBox_br.TabIndex = 0;
            checkBox_br.Text = "BR";
            checkBox_br.TextImageRelation = TextImageRelation.ImageBeforeText;
            checkBox_br.UseVisualStyleBackColor = true;
            // 
            // checkBox_www
            // 
            checkBox_www.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox_www.ForeColor = SystemColors.Control;
            checkBox_www.Image = Properties.Resources.www;
            checkBox_www.ImageAlign = ContentAlignment.MiddleRight;
            checkBox_www.Location = new Point(95, 37);
            checkBox_www.Name = "checkBox_www";
            checkBox_www.Size = new Size(92, 40);
            checkBox_www.TabIndex = 1;
            checkBox_www.Text = "WWW";
            checkBox_www.TextImageRelation = TextImageRelation.ImageBeforeText;
            checkBox_www.UseVisualStyleBackColor = true;
            // 
            // checkBox_friends
            // 
            checkBox_friends.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox_friends.ForeColor = SystemColors.Control;
            checkBox_friends.Image = Properties.Resources.friends;
            checkBox_friends.ImageAlign = ContentAlignment.MiddleRight;
            checkBox_friends.Location = new Point(193, 37);
            checkBox_friends.Name = "checkBox_friends";
            checkBox_friends.Size = new Size(124, 40);
            checkBox_friends.TabIndex = 2;
            checkBox_friends.Text = "Friends";
            checkBox_friends.TextImageRelation = TextImageRelation.ImageBeforeText;
            checkBox_friends.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(9, 9);
            label1.Name = "label1";
            label1.Size = new Size(118, 21);
            label1.TabIndex = 3;
            label1.Text = "Game Servers:";
            // 
            // button_install
            // 
            button_install.BackColor = Color.FromArgb(64, 64, 64);
            button_install.FlatStyle = FlatStyle.Flat;
            button_install.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button_install.ForeColor = SystemColors.ButtonHighlight;
            button_install.Image = Properties.Resources.download;
            button_install.ImageAlign = ContentAlignment.MiddleRight;
            button_install.Location = new Point(10, 229);
            button_install.Name = "button_install";
            button_install.Size = new Size(337, 44);
            button_install.TabIndex = 4;
            button_install.Text = "Install";
            button_install.TextAlign = ContentAlignment.MiddleLeft;
            button_install.TextImageRelation = TextImageRelation.ImageBeforeText;
            button_install.UseVisualStyleBackColor = false;
            button_install.Click += button1_Click;
            // 
            // checkBox4_custom
            // 
            checkBox4_custom.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox4_custom.ForeColor = SystemColors.Control;
            checkBox4_custom.Image = Properties.Resources.customize;
            checkBox4_custom.ImageAlign = ContentAlignment.MiddleRight;
            checkBox4_custom.Location = new Point(9, 87);
            checkBox4_custom.Name = "checkBox4_custom";
            checkBox4_custom.Size = new Size(118, 40);
            checkBox4_custom.TabIndex = 5;
            checkBox4_custom.Text = "Custom";
            checkBox4_custom.TextImageRelation = TextImageRelation.ImageBeforeText;
            checkBox4_custom.UseVisualStyleBackColor = true;
            // 
            // textBox_custom
            // 
            textBox_custom.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_custom.Location = new Point(12, 152);
            textBox_custom.Multiline = true;
            textBox_custom.Name = "textBox_custom";
            textBox_custom.ReadOnly = true;
            textBox_custom.Size = new Size(283, 71);
            textBox_custom.TabIndex = 6;
            // 
            // button_selectcustomfolder
            // 
            button_selectcustomfolder.BackColor = Color.DimGray;
            button_selectcustomfolder.FlatStyle = FlatStyle.Flat;
            button_selectcustomfolder.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button_selectcustomfolder.ForeColor = SystemColors.Control;
            button_selectcustomfolder.Image = Properties.Resources.open_folder;
            button_selectcustomfolder.Location = new Point(301, 152);
            button_selectcustomfolder.Name = "button_selectcustomfolder";
            button_selectcustomfolder.Size = new Size(48, 71);
            button_selectcustomfolder.TabIndex = 7;
            button_selectcustomfolder.UseVisualStyleBackColor = false;
            button_selectcustomfolder.Click += button2_Click;
            // 
            // button_uninstall
            // 
            button_uninstall.BackColor = Color.FromArgb(64, 64, 64);
            button_uninstall.FlatStyle = FlatStyle.Flat;
            button_uninstall.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button_uninstall.ForeColor = SystemColors.ButtonHighlight;
            button_uninstall.Image = Properties.Resources.download;
            button_uninstall.ImageAlign = ContentAlignment.MiddleRight;
            button_uninstall.Location = new Point(10, 279);
            button_uninstall.Name = "button_uninstall";
            button_uninstall.Size = new Size(337, 44);
            button_uninstall.TabIndex = 8;
            button_uninstall.Text = "Uninstall";
            button_uninstall.TextAlign = ContentAlignment.MiddleLeft;
            button_uninstall.TextImageRelation = TextImageRelation.ImageBeforeText;
            button_uninstall.UseVisualStyleBackColor = false;
            button_uninstall.Click += button_uninstall_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(12, 128);
            label2.Name = "label2";
            label2.Size = new Size(111, 21);
            label2.TabIndex = 9;
            label2.Text = "Custom Path:";
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.LocalApplicationData;
            // 
            // backgroundWorker_install
            // 
            backgroundWorker_install.DoWork += backgroundWorker_install_DoWork;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(64, 64, 64);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Image = Properties.Resources.package;
            button2.ImageAlign = ContentAlignment.MiddleRight;
            button2.Location = new Point(10, 329);
            button2.Name = "button2";
            button2.Size = new Size(337, 44);
            button2.TabIndex = 11;
            button2.Text = "Packages";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click_1;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(361, 376);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(button_uninstall);
            Controls.Add(button_selectcustomfolder);
            Controls.Add(textBox_custom);
            Controls.Add(checkBox4_custom);
            Controls.Add(button_install);
            Controls.Add(label1);
            Controls.Add(checkBox_friends);
            Controls.Add(checkBox_www);
            Controls.Add(checkBox_br);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "FormMain";
            ShowIcon = false;
            Text = "PreLoader KoGaMa Installer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox_br;
        private CheckBox checkBox_www;
        private CheckBox checkBox_friends;
        private Label label1;
        private Button button_install;
        private CheckBox checkBox4_custom;
        private TextBox textBox_custom;
        private Button button_selectcustomfolder;
        private Button button_uninstall;
        private Label label2;
        private FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker_install;
        private Button button2;
    }
}
