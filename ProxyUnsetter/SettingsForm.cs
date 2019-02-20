using System;
using System.Net;
using System.Windows.Forms;
using ProxyUnsetter.Helpers;
using ProxyUnsetter.Properties;

namespace ProxyUnsetter
{
    internal partial class SettingsForm : Form
    {
        private readonly ReleaseChecker _releaseChecker;
        private readonly ProxyManager _proxyManager;
        private readonly SettingsManager _settingsManager;

        public SettingsForm(ReleaseChecker releaseChecker, ProxyManager proxyManager, SettingsManager settingsManager)
        {
            InitializeComponent();
            _releaseChecker = releaseChecker;
            _proxyManager = proxyManager;
            _settingsManager = settingsManager;

            checkBoxUnsetProxyAutomatically.Checked = Settings.Default.UnsetProxyAutomatically;
            radioButtonFakePac.Checked = !Settings.Default.UnsetOrFakePac;
            radioButtonUnsetPac.Checked = Settings.Default.UnsetOrFakePac;
            checkBoxNotifyWhenProxySet.Checked = Settings.Default.NotifyOfProxySet;
            checkBoxCheckForNewReleaseWeekly.Checked = Settings.Default.CheckForNewReleaseWeekly;
            checkBoxLaunchAtWindowsStartup.Checked = _settingsManager.GetLaunchAtWindowsStartupState();

            checkBoxUnsetProxyAutomatically.CheckedChanged += CheckBoxUnsetProxyAutomatically_CheckedChanged;
            radioButtonUnsetPac.CheckedChanged += RadioButtonUnsetPacOnCheckedChanged;
            checkBoxNotifyWhenProxySet.CheckedChanged += CheckBoxNotifyWhenProxySet_CheckedChanged;
            checkBoxCheckForNewReleaseWeekly.CheckedChanged += CheckBoxCheckForNewReleaseWeekly_CheckedChanged;
            checkBoxLaunchAtWindowsStartup.CheckedChanged += CheckBoxLaunchAtWindowsStartup_CheckedChanged;

            buttonDelete.Enabled = false;

            InitializeIpWhitelist();
            InitializeManualProxy();
        }

        private void RadioButtonUnsetPacOnCheckedChanged(object sender, EventArgs e)
        {
            var radioButton = (RadioButton) sender;
            SettingsManager.ToggleUnsetOrFakePac(radioButton.Checked);
        }

        private void InitializeManualProxy()
        {
            textBoxManualProxy.Text = Settings.Default.ManuallySetProxy;
            textBoxManualProxy.LostFocus +=
                (sender, args) => _settingsManager.SetManuallySetProxy(textBoxManualProxy.Text);
        }

        private void InitializeIpWhitelist()
        {
            labelDetectedIp.Text = _proxyManager.LocalIpAddress().ToString();
            foreach (var ip in Settings.Default.IpWhitelist)
            {
                listBoxIpWhitelist.Items.Add(ip);
            }

            listBoxIpWhitelist.MouseDoubleClick += ListBoxWhitelist_MouseDoubleClick;
        }

        private void ListBoxWhitelist_MouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var index = listBoxIpWhitelist.IndexFromPoint(mouseEventArgs.Location);
            if (index == ListBox.NoMatches)
                return;
            var item = (string) listBoxIpWhitelist.Items[index];
            var strings = SettingsManager.GetIpAddressAndNetmaskFromSetting(item);
            var ipAddress = IPAddress.Parse(strings[0]);
            var netmask = IPAddress.Parse(strings[1]);

            using (var ipAddressEntryForm = new IpAddressEntryForm(ipAddress, netmask))
            {
                var dialogResult = ipAddressEntryForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    item = $"{ipAddressEntryForm.IpAddressString}/{ipAddressEntryForm.NetmaskString}";
                    listBoxIpWhitelist.Items[index] = item;
                    Settings.Default.IpWhitelist[index] = item;
                    Settings.Default.Save();
                }
            }
        }

        private static void CheckBoxUnsetProxyAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsManager.ToggleAutomaticProxyUnset(checkbox.Checked);
        }

        private static void CheckBoxNotifyWhenProxySet_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsManager.ToggleNotifyOfProxySet(checkbox.Checked);
        }

        private static void CheckBoxLaunchAtWindowsStartup_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsManager.ToggleWindowsStartup(checkbox.Checked);
        }

        private void CheckBoxCheckForNewReleaseWeekly_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsManager.ToggleCheckForNewReleaseWeekly(checkbox.Checked, _releaseChecker);
        }

        private void ButtonCheckForNewRelease_Click(object sender, EventArgs e)
        {
            _releaseChecker.CheckNow();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var ipAddressEntryForm =
                new IpAddressEntryForm(IPAddress.Parse("0.0.0.0"), IPAddress.Parse("0.0.0.0")))
            {
                var dialogResult = ipAddressEntryForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    var item = $"{ipAddressEntryForm.IpAddressString}/{ipAddressEntryForm.NetmaskString}";
                    listBoxIpWhitelist.Items.Add(item);
                    Settings.Default.IpWhitelist.Add(item);
                    Settings.Default.Save();
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var index = listBoxIpWhitelist.SelectedIndex;
            listBoxIpWhitelist.Items.RemoveAt(index);
            Settings.Default.IpWhitelist.RemoveAt(index);
            Settings.Default.Save();
        }

        private void ListBoxIpWhitelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxIpWhitelist.SelectedIndex >= 0)
            {
                buttonDelete.Enabled = true;
            }
            else
            {
                buttonDelete.Enabled = false;
            }
        }

        private void ButtonSetManualProxy_Click(object sender, EventArgs e)
        {
            _proxyManager.SetManuallySetProxy();
            _proxyManager.LastProxyState = _proxyManager.GetCurrentProxyState();
            Program.SimpleLogLines.Add(
                $"{DateTime.Now:g} Proxy was manually set to {_proxyManager.ManuallySetProxyServer}.");
        }
    }
}