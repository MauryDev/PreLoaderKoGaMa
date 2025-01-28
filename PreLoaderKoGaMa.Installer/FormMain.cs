using PreLoaderKoGaMa.Installer.Helpers;
using System.Collections;
using System.IO.Compression;

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

            using HttpClient httpClient = new();
            loading.NextState("Downloading PreLoaderKoGaMa archive");
            var urlstream = GithubRawHelper.GetUrlCurrent("PreLoaderKoGaMa.Installer/src/PreLoaderKoGaMa.zip");
            using var stream = await httpClient.GetStreamAsync(urlstream);
            loading.NextState("Loading PreLoaderKoGaMa archive");

            using var zip = new ZipArchive(stream);
            if (BrEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to KoGaMa BR");

                var pathlauncher = PathHelper.GetLauncher(PathHelper.BRPath);
                InstallHelper.Install(pathlauncher, zip);
            }
            if (WwwEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to KoGaMa WWW");

                var pathlauncher = PathHelper.GetLauncher(PathHelper.WWWPath);
                InstallHelper.Install(pathlauncher, zip);
            }
            if (FriendsEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to KoGaMa Friends");

                var pathlauncher = PathHelper.GetLauncher(PathHelper.FriendsPath);
                InstallHelper.Install(pathlauncher, zip);
            }
            if (CustomEnable)
            {
                loading.NextState("Extracting PreLoaderKoGaMa to custom path");

                var pathlauncher = PathHelper.GetLauncher(customPath);
                InstallHelper.Install(pathlauncher, zip);
            }
            loading.NextState("PreLoaderKoGaMa installation successful");
            Thread.Sleep(5000);
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

            using HttpClient httpClient = new();
            loading.NextState("Downloading PreLoaderKoGaMa archive");
            using var stream = await httpClient.GetStreamAsync("https://raw.githubusercontent.com/MauryDev/PreLoaderKoGaMa/refs/heads/master/PreLoaderKoGaMa.Installer/src/PreLoaderKoGaMa.zip");
            loading.NextState("Loading PreLoaderKoGaMa archive");

            using var zip = new ZipArchive(stream);
            if (BrEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from KoGaMa BR");

                var pathlauncher = PathHelper.GetLauncher(PathHelper.BRPath);
                UninstallHelper.Uninstall(pathlauncher, zip);
            }
            if (WwwEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from KoGaMa WWW");

                var pathlauncher = PathHelper.GetLauncher(PathHelper.WWWPath);
                UninstallHelper.Uninstall(pathlauncher, zip);
            }
            if (FriendsEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from KoGaMa Friends");

                var pathlauncher = PathHelper.GetLauncher(PathHelper.FriendsPath);
                UninstallHelper.Uninstall(pathlauncher, zip);
            }
            if (CustomEnable)
            {
                loading.NextState("Uninstalling PreLoaderKoGaMa from custom path");

                var pathlauncher = PathHelper.GetLauncher(customPath);
                UninstallHelper.Uninstall(pathlauncher, zip);
            }
            loading.NextState("PreLoaderKoGaMa uninstallation successful");
            Thread.Sleep(5000);
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
    }
}
