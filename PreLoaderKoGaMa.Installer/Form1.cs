using PreLoaderKoGaMa.Installer.Helpers;
using System.IO.Compression;

namespace PreLoaderKoGaMa.Installer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_custom.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var zip = new ZipArchive(File.OpenRead("PreLoaderKoGaMa.zip"));
            if (checkBox_br.Checked)
            {
                var pathlauncher = PathHelper.GetLauncher(PathHelper.BRPath);
                InstallHelper.Install(pathlauncher, zip);
            }
            if (checkBox_www.Checked)
            {
                var pathlauncher = PathHelper.GetLauncher(PathHelper.WWWPath);
                InstallHelper.Install(pathlauncher, zip);
            }
            if (checkBox_www.Checked)
            {
                var pathlauncher = PathHelper.GetLauncher(PathHelper.FriendsPath);
                InstallHelper.Install(pathlauncher, zip);
            }
            if (checkBox4_custom.Checked)
            {
                var pathlauncher = PathHelper.GetLauncher(textBox_custom.Text);
                InstallHelper.Install(pathlauncher, zip);
            }
        }
    }
}
