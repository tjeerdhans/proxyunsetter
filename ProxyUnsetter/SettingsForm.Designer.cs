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
            this.buttonOK = new System.Windows.Forms.Button();
            this.listBoxIpWhitelist = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "IP whitelist";
            // 
            // checkBoxUnsetProxyAutomatically
            // 
            this.checkBoxUnsetProxyAutomatically.AutoSize = true;
            this.checkBoxUnsetProxyAutomatically.Location = new System.Drawing.Point(13, 13);
            this.checkBoxUnsetProxyAutomatically.Name = "checkBoxUnsetProxyAutomatically";
            this.checkBoxUnsetProxyAutomatically.Size = new System.Drawing.Size(215, 24);
            this.checkBoxUnsetProxyAutomatically.TabIndex = 5;
            this.checkBoxUnsetProxyAutomatically.Text = "Unset proxy automatically";
            this.toolTip1.SetToolTip(this.checkBoxUnsetProxyAutomatically, "When the proxy is set by the system or a group policy, unset it immediately. The " +
        "proxy is checked every 5 seconds.");
            this.checkBoxUnsetProxyAutomatically.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyWhenProxySet
            // 
            this.checkBoxNotifyWhenProxySet.AutoSize = true;
            this.checkBoxNotifyWhenProxySet.Location = new System.Drawing.Point(13, 44);
            this.checkBoxNotifyWhenProxySet.Name = "checkBoxNotifyWhenProxySet";
            this.checkBoxNotifyWhenProxySet.Size = new System.Drawing.Size(184, 24);
            this.checkBoxNotifyWhenProxySet.TabIndex = 6;
            this.checkBoxNotifyWhenProxySet.Text = "Notify when proxy set";
            this.checkBoxNotifyWhenProxySet.UseVisualStyleBackColor = true;
            // 
            // checkBoxLaunchAtWindowsStartup
            // 
            this.checkBoxLaunchAtWindowsStartup.AutoSize = true;
            this.checkBoxLaunchAtWindowsStartup.Location = new System.Drawing.Point(13, 75);
            this.checkBoxLaunchAtWindowsStartup.Name = "checkBoxLaunchAtWindowsStartup";
            this.checkBoxLaunchAtWindowsStartup.Size = new System.Drawing.Size(228, 24);
            this.checkBoxLaunchAtWindowsStartup.TabIndex = 7;
            this.checkBoxLaunchAtWindowsStartup.Text = "Launch at Windows startup";
            this.checkBoxLaunchAtWindowsStartup.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckForNewReleaseWeekly
            // 
            this.checkBoxCheckForNewReleaseWeekly.AutoSize = true;
            this.checkBoxCheckForNewReleaseWeekly.Location = new System.Drawing.Point(13, 106);
            this.checkBoxCheckForNewReleaseWeekly.Name = "checkBoxCheckForNewReleaseWeekly";
            this.checkBoxCheckForNewReleaseWeekly.Size = new System.Drawing.Size(243, 24);
            this.checkBoxCheckForNewReleaseWeekly.TabIndex = 8;
            this.checkBoxCheckForNewReleaseWeekly.Text = "Check for new release weekly";
            this.checkBoxCheckForNewReleaseWeekly.UseVisualStyleBackColor = true;
            // 
            // buttonCheckForNewRelease
            // 
            this.buttonCheckForNewRelease.Location = new System.Drawing.Point(262, 101);
            this.buttonCheckForNewRelease.Name = "buttonCheckForNewRelease";
            this.buttonCheckForNewRelease.Size = new System.Drawing.Size(104, 33);
            this.buttonCheckForNewRelease.TabIndex = 9;
            this.buttonCheckForNewRelease.Text = "Check now";
            this.buttonCheckForNewRelease.UseVisualStyleBackColor = true;
            this.buttonCheckForNewRelease.Click += new System.EventHandler(this.buttonCheckForNewRelease_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(276, 319);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(111, 35);
            this.buttonOK.TabIndex = 11;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // listBoxIpWhitelist
            // 
            this.listBoxIpWhitelist.FormattingEnabled = true;
            this.listBoxIpWhitelist.ItemHeight = 20;
            this.listBoxIpWhitelist.Location = new System.Drawing.Point(13, 189);
            this.listBoxIpWhitelist.Name = "listBoxIpWhitelist";
            this.listBoxIpWhitelist.Size = new System.Drawing.Size(256, 164);
            this.listBoxIpWhitelist.TabIndex = 12;
            this.listBoxIpWhitelist.SelectedIndexChanged += new System.EventHandler(this.listBoxIpWhitelist_SelectedIndexChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonAdd.Location = new System.Drawing.Point(275, 189);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 35);
            this.buttonAdd.TabIndex = 13;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonDelete.Location = new System.Drawing.Point(276, 230);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 35);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 366);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listBoxIpWhitelist);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCheckForNewRelease);
            this.Controls.Add(this.checkBoxCheckForNewReleaseWeekly);
            this.Controls.Add(this.checkBoxLaunchAtWindowsStartup);
            this.Controls.Add(this.checkBoxNotifyWhenProxySet);
            this.Controls.Add(this.checkBoxUnsetProxyAutomatically);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
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
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListBox listBoxIpWhitelist;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
    }
}