using PreLoaderKoGaMa.Installer.Shared.Helpers;
using System.Collections;
using System.IO.Compression;
using System.Runtime.Loader;

namespace PreLoaderKoGaMa.Installer
{
    public partial class FormMain : Form
    {
        public BitArray bitArray = new(4);
        public string customPath = string.Empty;
        public Loading? loading;
        public FormMain()
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
            bitArray[0] = checkBox_br.Checked;
            bitArray[1] = checkBox_www.Checked;
            bitArray[2] = checkBox_friends.Checked;
            bitArray[3] = checkBox4_custom.Checked;
            customPath = textBox_custom.Text;
            loading = new()
            {
                RunCallback = () => backgroundWorker_install.RunWorkerAsync(1)
            };
            loading.ShowDialog(this);
        }
        async Task Install()
        {
            var BrEnable = bitArray[0];
            var WwwEnable = bitArray[1];
            var FriendsEnable = bitArray[2];
            var CustomEnable = bitArray[3];
            var installlen = 3;

            for (int i = 0; i < 4; i++)
            {
                if (bitArray[i])
                    installlen++;
            }
            loading.TotalStep = installlen;

            loading.NextState("Downloading PreLoaderKoGaMa archive");
            using var stream = await GithubRawHelper.GetStreamCurrent("PreLoaderKoGaMa.Installer/src/PreLoaderKoGaMa.zip");
            loading.NextState("Loading PreLoaderKoGaMa archive");

            using var zip = new ZipArchive(stream);
            if (BrEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to KoGaMa BR");

                await InstallHelper.Install(KoGaMaServer.BR, zip);
            }
            if (WwwEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to KoGaMa WWW");
                await InstallHelper.Install(KoGaMaServer.WWW, zip);
            }
            if (FriendsEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to KoGaMa Friends");

                await InstallHelper.Install(KoGaMaServer.Friends, zip);
            }
            if (CustomEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to custom path");


                var pathlauncher = PathHelper.GetLauncher(customPath);
                await InstallHelper.Install(pathlauncher, zip);
            }
            loading.NextState("PreLoaderKoGaMa installation successful");
            await Task.Delay(5000);
            loading.Invoke(() => loading.Close());
            loading = null;
        }

        async Task Uninstall()
        {
            var BrEnable = bitArray[0];
            var WwwEnable = bitArray[1];
            var FriendsEnable = bitArray[2];
            var CustomEnable = bitArray[3];
            var installlen = 3;

            for (int i = 0; i < 4; i++)
            {
                if (bitArray[i])
                    installlen++;
            }
            loading.TotalStep = installlen;

            loading.NextState("Downloading PreLoaderKoGaMa archive");

            using var stream = await GithubRawHelper.GetStreamCurrent("PreLoaderKoGaMa.Installer/src/PreLoaderKoGaMa.zip");

            loading.NextState("Loading PreLoaderKoGaMa archive");

            using var zip = new ZipArchive(stream);
            if (BrEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from KoGaMa BR");

                await UninstallHelper.Uninstall(KoGaMaServer.BR, zip);
            }
            if (WwwEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from KoGaMa WWW");

                await UninstallHelper.Uninstall(KoGaMaServer.WWW, zip);
            }
            if (FriendsEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from KoGaMa Friends");

                await UninstallHelper.Uninstall(KoGaMaServer.Friends, zip);
            }
            if (CustomEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from custom path");
                if (Directory.Exists(customPath))
                {
                    var pathlauncher = PathHelper.GetLauncher(customPath);
                    await UninstallHelper.Uninstall(pathlauncher, zip);
                }

            }
            loading.NextState("PreLoaderKoGaMa uninstallation successful");
            await Task.Delay(5000);
            loading.Invoke(() => loading.Close());
            loading = null;
        }
        private async void backgroundWorker_install_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            if (e.Argument is int i)
            {
                if (i == 1)
                    await Install();
                else if (i == 2)
                    await Uninstall();
            }
        }

        private void button_uninstall_Click(object sender, EventArgs e)
        {
            bitArray[0] = checkBox_br.Checked;
            bitArray[1] = checkBox_www.Checked;
            bitArray[2] = checkBox_friends.Checked;
            bitArray[3] = checkBox4_custom.Checked;
            customPath = textBox_custom.Text;
            loading = new()
            {
                RunCallback = () => backgroundWorker_install.RunWorkerAsync(2)
            };

            loading.ShowDialog(this);
        }
        public IEnumerable<string> GetAllKoGaMaPath()
        {
            if (checkBox_br.Checked)
                yield return PathHelper.BRPath;
            if (checkBox_www.Checked)
                yield return PathHelper.WWWPath;
            if (checkBox_friends.Checked)
                yield return PathHelper.FriendsPath;
            if (checkBox4_custom.Checked)
                yield return textBox_custom.Text;

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            var paths = (from item in GetAllKoGaMaPath()
                        select  PathHelper.GetLauncher(item))
                        .ToArray();
            var plugininstaller = new PluginInstaller(paths);
            
            plugininstaller.ShowDialog(this);
        }
    }
}
