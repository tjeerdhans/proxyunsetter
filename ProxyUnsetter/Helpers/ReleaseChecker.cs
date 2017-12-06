using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using ProxyUnsetter.Properties;
using Timer = System.Windows.Forms.Timer;

namespace ProxyUnsetter.Helpers
{
    internal class ReleaseChecker
    {
        private Timer _checkForNewReleaseTimer;

        public void Start()
        {
            _checkForNewReleaseTimer = new Timer { Interval = 60 * 60 * 1000 }; // check every hour if we need to check for new releases
            _checkForNewReleaseTimer.Tick += CheckForNewReleaseTimer_Tick;
            _checkForNewReleaseTimer.Start();
        }

        public void Stop()
        {
            _checkForNewReleaseTimer.Stop();
            _checkForNewReleaseTimer.Dispose();
        }

        public void CheckNow()
        {
            CheckForNewReleaseTimer_Tick(this, new ReleaseCheckEventArgs(true));
        }

        public void CheckNowSilent()
        {
            CheckForNewReleaseTimer_Tick(this, EventArgs.Empty);
        }

        private async void CheckForNewReleaseTimer_Tick(object sender, EventArgs e)
        {
            var deliberateCheck = e is ReleaseCheckEventArgs args && args.DeliberateCheck;
            if (Settings.Default.LastReleaseCheck > DateTime.Now.AddDays(-7) && !deliberateCheck)
            {
                return; // it hasn't been a week since the last check.
            }
            string latestReleaseJson;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("proxyunsetter", Application.ProductVersion));
                try
                {
                    latestReleaseJson = await
                        httpClient.GetStringAsync(
                            "https://api.github.com/repos/tjeerdhans/proxyunsetter/releases/latest");
                }
                catch (HttpRequestException exception)
                {
                    Program.SimpleLogLines.Add(
                        $"{DateTime.Now:g} Couldn't reach github in order to check for new releases. Exception message: {exception.Message}");
                    if (deliberateCheck)
                    {
                        MessageBox.Show($"Couldn't reach github in order to check for new releases.\r\nException message: {exception.Message}\r\n{exception.InnerException?.Message}", @"Proxy Unsetter", MessageBoxButtons.OK);
                    }
                    return;
                }
            }
            var latestRelease = latestReleaseJson.FromJson<GitHubRelease>();
            if (latestRelease == null)
            {
                Program.SimpleLogLines.Add($"{DateTime.Now:g} Couldn't parse json response while checking for a new release at GitHub.");
                return;
            }

            var latestReleaseTagSplitted = latestRelease.tag_name.Split('.');
            var currentReleaseSplitted = Application.ProductVersion.Split('.');
            var latestMajor = int.Parse(latestReleaseTagSplitted[0]);
            var latestMinor = int.Parse(latestReleaseTagSplitted[1]);
            var currentMajor = int.Parse(currentReleaseSplitted[0]);
            var currentMinor = int.Parse(currentReleaseSplitted[1]);
            var newerAvailable = false;
            if (latestMajor > currentMajor)
            {
                newerAvailable = true;
            }
            else if (latestMajor == currentMajor && latestMinor > currentMinor)
            {
                newerAvailable = true;
            }

            if (newerAvailable)
            {
                Program.SimpleLogLines.Add($"{DateTime.Now:g} Checked for new release: New release available, ({latestRelease.tag_name}, '{latestRelease.name}').");
                if (MessageBox.Show(
                        $@"There is a new version available ({latestRelease.tag_name}, '{latestRelease.name}'). Click 'OK' to browse to the release at https://github.com/tjeerdhans/proxyunsetter/releases/latest. Download the latest executable and replace your current one with it.",
                        @"New version", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start("https://github.com/tjeerdhans/proxyunsetter/releases/latest");
                }
            }
            else
            {
                Program.SimpleLogLines.Add($"{DateTime.Now:g} Checked for new release: no new release available.");
                if (deliberateCheck)
                {
                    MessageBox.Show(@"No new release available.", @"Proxy Unsetter", MessageBoxButtons.OK);
                }
            }
            Settings.Default.LastReleaseCheck = DateTime.Now;
            Settings.Default.Save();
        }
    }
}
