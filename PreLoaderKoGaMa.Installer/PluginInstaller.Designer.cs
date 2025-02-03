namespace PreLoaderKoGaMa.Installer
{
    partial class PluginInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            comboBox1 = new ComboBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(63, 9);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(229, 26);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(45, 19);
            label1.TabIndex = 1;
            label1.Text = "Nome";
            // 
            // button1
            // 
            button1.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(12, 81);
            button1.Name = "button1";
            button1.Size = new Size(75, 29);
            button1.TabIndex = 2;
            button1.Text = "Instalar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(196, 81);
            button2.Name = "button2";
            button2.Size = new Size(96, 29);
            button2.TabIndex = 3;
            button2.Text = "Desinstalar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Official", "URL", "File" });
            comboBox1.Location = new Point(63, 43);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(229, 28);
            comboBox1.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 47);
            label2.Name = "label2";
            label2.Size = new Size(45, 19);
            label2.TabIndex = 5;
            label2.Text = "Tipo";
            // 
            // PluginInstaller
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(307, 119);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "PluginInstaller";
            Text = "Plugin Installer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private Button button2;
        private ComboBox comboBox1;
        private Label label2;
    }
}