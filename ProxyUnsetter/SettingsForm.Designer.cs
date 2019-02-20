namespace ProxyUnsetter
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxUnsetProxyAutomatically = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifyWhenProxySet = new System.Windows.Forms.CheckBox();
            this.checkBoxLaunchAtWindowsStartup = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckForNewReleaseWeekly = new System.Windows.Forms.CheckBox();
            this.buttonCheckForNewRelease = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxManualProxy = new System.Windows.Forms.TextBox();
            this.buttonSetManualProxy = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.listBoxIpWhitelist = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDetectedIp = new System.Windows.Forms.Label();
            this.radioButtonUnsetPac = new System.Windows.Forms.RadioButton();
            this.radioButtonFakePac = new System.Windows.Forms.RadioButton();
            this.groupBoxPacBehaviour = new System.Windows.Forms.GroupBox();
            this.groupBoxPacBehaviour.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 212);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "IP whitelist";
            this.toolTip1.SetToolTip(this.label5, "If your current IP is in this list, ProxyUnsetter won\'t touch your proxy settings" +
        ".\r\n");
            // 
            // checkBoxUnsetProxyAutomatically
            // 
            this.checkBoxUnsetProxyAutomatically.AutoSize = true;
            this.checkBoxUnsetProxyAutomatically.Location = new System.Drawing.Point(9, 9);
            this.checkBoxUnsetProxyAutomatically.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxUnsetProxyAutomatically.Name = "checkBoxUnsetProxyAutomatically";
            this.checkBoxUnsetProxyAutomatically.Size = new System.Drawing.Size(146, 17);
            this.checkBoxUnsetProxyAutomatically.TabIndex = 5;
            this.checkBoxUnsetProxyAutomatically.Text = "Unset proxy automatically";
            this.toolTip1.SetToolTip(this.checkBoxUnsetProxyAutomatically, "When the proxy is set by the system or a group policy, unset it immediately. The " +
        "proxy is checked every 5 seconds.");
            this.checkBoxUnsetProxyAutomatically.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyWhenProxySet
            // 
            this.checkBoxNotifyWhenProxySet.AutoSize = true;
            this.checkBoxNotifyWhenProxySet.Location = new System.Drawing.Point(9, 29);
            this.checkBoxNotifyWhenProxySet.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxNotifyWhenProxySet.Name = "checkBoxNotifyWhenProxySet";
            this.checkBoxNotifyWhenProxySet.Size = new System.Drawing.Size(127, 17);
            this.checkBoxNotifyWhenProxySet.TabIndex = 6;
            this.checkBoxNotifyWhenProxySet.Text = "Notify when proxy set";
            this.checkBoxNotifyWhenProxySet.UseVisualStyleBackColor = true;
            // 
            // checkBoxLaunchAtWindowsStartup
            // 
            this.checkBoxLaunchAtWindowsStartup.AutoSize = true;
            this.checkBoxLaunchAtWindowsStartup.Location = new System.Drawing.Point(9, 97);
            this.checkBoxLaunchAtWindowsStartup.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxLaunchAtWindowsStartup.Name = "checkBoxLaunchAtWindowsStartup";
            this.checkBoxLaunchAtWindowsStartup.Size = new System.Drawing.Size(156, 17);
            this.checkBoxLaunchAtWindowsStartup.TabIndex = 7;
            this.checkBoxLaunchAtWindowsStartup.Text = "Launch at Windows startup";
            this.checkBoxLaunchAtWindowsStartup.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckForNewReleaseWeekly
            // 
            this.checkBoxCheckForNewReleaseWeekly.AutoSize = true;
            this.checkBoxCheckForNewReleaseWeekly.Location = new System.Drawing.Point(9, 117);
            this.checkBoxCheckForNewReleaseWeekly.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxCheckForNewReleaseWeekly.Name = "checkBoxCheckForNewReleaseWeekly";
            this.checkBoxCheckForNewReleaseWeekly.Size = new System.Drawing.Size(168, 17);
            this.checkBoxCheckForNewReleaseWeekly.TabIndex = 8;
            this.checkBoxCheckForNewReleaseWeekly.Text = "Check for new release weekly";
            this.checkBoxCheckForNewReleaseWeekly.UseVisualStyleBackColor = true;
            // 
            // buttonCheckForNewRelease
            // 
            this.buttonCheckForNewRelease.Location = new System.Drawing.Point(181, 113);
            this.buttonCheckForNewRelease.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCheckForNewRelease.Name = "buttonCheckForNewRelease";
            this.buttonCheckForNewRelease.Size = new System.Drawing.Size(69, 22);
            this.buttonCheckForNewRelease.TabIndex = 9;
            this.buttonCheckForNewRelease.Text = "Check now";
            this.buttonCheckForNewRelease.UseVisualStyleBackColor = true;
            this.buttonCheckForNewRelease.Click += new System.EventHandler(this.ButtonCheckForNewRelease_Click);
            // 
            // textBoxManualProxy
            // 
            this.textBoxManualProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxManualProxy.Location = new System.Drawing.Point(83, 157);
            this.textBoxManualProxy.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxManualProxy.Name = "textBoxManualProxy";
            this.textBoxManualProxy.Size = new System.Drawing.Size(179, 20);
            this.textBoxManualProxy.TabIndex = 17;
            this.toolTip1.SetToolTip(this.textBoxManualProxy, "Proxy server with port, e.g. 127.0.0.1:8080");
            // 
            // buttonSetManualProxy
            // 
            this.buttonSetManualProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetManualProxy.Location = new System.Drawing.Point(267, 155);
            this.buttonSetManualProxy.Name = "buttonSetManualProxy";
            this.buttonSetManualProxy.Size = new System.Drawing.Size(74, 23);
            this.buttonSetManualProxy.TabIndex = 20;
            this.buttonSetManualProxy.Text = "Apply";
            this.toolTip1.SetToolTip(this.buttonSetManualProxy, "Apply manual proxy.");
            this.buttonSetManualProxy.UseVisualStyleBackColor = true;
            this.buttonSetManualProxy.Click += new System.EventHandler(this.ButtonSetManualProxy_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(372, 312);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(74, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // listBoxIpWhitelist
            // 
            this.listBoxIpWhitelist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxIpWhitelist.FormattingEnabled = true;
            this.listBoxIpWhitelist.Location = new System.Drawing.Point(9, 227);
            this.listBoxIpWhitelist.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxIpWhitelist.Name = "listBoxIpWhitelist";
            this.listBoxIpWhitelist.Size = new System.Drawing.Size(172, 108);
            this.listBoxIpWhitelist.TabIndex = 12;
            this.listBoxIpWhitelist.SelectedIndexChanged += new System.EventHandler(this.ListBoxIpWhitelist_SelectedIndexChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(183, 227);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 23);
            this.buttonAdd.TabIndex = 13;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(183, 255);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(50, 23);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 160);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Manual proxy";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Detected IP:";
            // 
            // labelDetectedIp
            // 
            this.labelDetectedIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDetectedIp.AutoSize = true;
            this.labelDetectedIp.Location = new System.Drawing.Point(80, 193);
            this.labelDetectedIp.Name = "labelDetectedIp";
            this.labelDetectedIp.Size = new System.Drawing.Size(10, 13);
            this.labelDetectedIp.TabIndex = 19;
            this.labelDetectedIp.Text = "-";
            // 
            // radioButtonUnsetPac
            // 
            this.radioButtonUnsetPac.AutoSize = true;
            this.radioButtonUnsetPac.Location = new System.Drawing.Point(6, 19);
            this.radioButtonUnsetPac.Name = "radioButtonUnsetPac";
            this.radioButtonUnsetPac.Size = new System.Drawing.Size(190, 17);
            this.radioButtonUnsetPac.TabIndex = 22;
            this.radioButtonUnsetPac.TabStop = true;
            this.radioButtonUnsetPac.Text = "Unset Proxy autoconfig (PAC) URL";
            this.radioButtonUnsetPac.UseVisualStyleBackColor = true;
            // 
            // radioButtonFakePac
            // 
            this.radioButtonFakePac.AutoSize = true;
            this.radioButtonFakePac.Location = new System.Drawing.Point(6, 42);
            this.radioButtonFakePac.Name = "radioButtonFakePac";
            this.radioButtonFakePac.Size = new System.Drawing.Size(157, 17);
            this.radioButtonFakePac.TabIndex = 23;
            this.radioButtonFakePac.TabStop = true;
            this.radioButtonFakePac.Text = "Hijack PAC URL and fake it";
            this.radioButtonFakePac.UseVisualStyleBackColor = true;
            // 
            // groupBoxPacBehaviour
            // 
            this.groupBoxPacBehaviour.Controls.Add(this.radioButtonUnsetPac);
            this.groupBoxPacBehaviour.Controls.Add(this.radioButtonFakePac);
            this.groupBoxPacBehaviour.Location = new System.Drawing.Point(209, 9);
            this.groupBoxPacBehaviour.Name = "groupBoxPacBehaviour";
            this.groupBoxPacBehaviour.Size = new System.Drawing.Size(235, 72);
            this.groupBoxPacBehaviour.TabIndex = 24;
            this.groupBoxPacBehaviour.TabStop = false;
            this.groupBoxPacBehaviour.Text = "PAC behaviour";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 342);
            this.Controls.Add(this.groupBoxPacBehaviour);
            this.Controls.Add(this.buttonSetManualProxy);
            this.Controls.Add(this.labelDetectedIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxManualProxy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listBoxIpWhitelist);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonCheckForNewRelease);
            this.Controls.Add(this.checkBoxCheckForNewReleaseWeekly);
            this.Controls.Add(this.checkBoxLaunchAtWindowsStartup);
            this.Controls.Add(this.checkBoxNotifyWhenProxySet);
            this.Controls.Add(this.checkBoxUnsetProxyAutomatically);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.groupBoxPacBehaviour.ResumeLayout(false);
            this.groupBoxPacBehaviour.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxUnsetProxyAutomatically;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxNotifyWhenProxySet;
        private System.Windows.Forms.CheckBox checkBoxLaunchAtWindowsStartup;
        private System.Windows.Forms.CheckBox checkBoxCheckForNewReleaseWeekly;
        private System.Windows.Forms.Button buttonCheckForNewRelease;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListBox listBoxIpWhitelist;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxManualProxy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDetectedIp;
        private System.Windows.Forms.Button buttonSetManualProxy;
        private System.Windows.Forms.RadioButton radioButtonUnsetPac;
        private System.Windows.Forms.RadioButton radioButtonFakePac;
        private System.Windows.Forms.GroupBox groupBoxPacBehaviour;
    }
}