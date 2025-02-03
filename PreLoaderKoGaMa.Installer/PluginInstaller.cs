

using PreLoaderKoGaMa.Installer.Shared.Helpers;
using System.IO.Compression;
using System.IO;
using System.Text.Json;

namespace PreLoaderKoGaMa.Installer
{
    public partial class PluginInstaller : Form
    {
        string PluginName = "";
        string[] PathsInstall = Array.Empty<string>();
        Loading? loading = null;
        public PluginInstaller(string[] paths)
        {
            PathsInstall = paths;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        
        void button1_Click(object sender, EventArgs e)
        {
            PluginName = textBox1.Text;
            var index = comboBox1.SelectedIndex;
            switch (index)
            {
                case 0:
                    {
                        loading = new()
                        {
                            RunCallback = () => Task.Run(() => PluginHelper.OnInstallOficial(PathsInstall, PluginName))
                        };
                        loading.ShowDialog(this);
                        break;
                    }
                case 1:
                    {

                        loading = new()
                        {
                            RunCallback = () => Task.Run(() => PluginHelper.OnInstallurl(PathsInstall, PluginName))
                        };
                        loading.ShowDialog(this);
                        break;
                    }
                case 2:
                    {

                        loading = new()
                        {
                            RunCallback = () => Task.Run(() => PluginHelper.OnInstallFile(PathsInstall, PluginName))
                        };
                        loading.ShowDialog(this);
                        break;
                    }

            }
            
            
        }
       
        
        void button2_Click(object sender, EventArgs e)
        {
            PluginName = textBox1.Text;
            loading = new()
            {
                RunCallback = () => Task.Run(() => UninstallPlugin.Uninstall(PathsInstall, PluginName))
            };
            loading.ShowDialog(this);
        }
    }
}
