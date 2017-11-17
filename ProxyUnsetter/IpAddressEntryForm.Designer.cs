namespace ProxyUnsetter
{
    partial class IpAddressEntryForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.ipAddressControlNetmask = new ProxyUnsetter.IpAddressControl.IPAddressControl();
            this.ipAddressControlIp = new ProxyUnsetter.IpAddressControl.IPAddressControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Netmask";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(258, 110);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 30);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(161, 110);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(91, 30);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ipAddressControlNetmask
            // 
            this.ipAddressControlNetmask.AllowInternalTab = false;
            this.ipAddressControlNetmask.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ipAddressControlNetmask.AutoHeight = true;
            this.ipAddressControlNetmask.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlNetmask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlNetmask.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlNetmask.Location = new System.Drawing.Point(115, 47);
            this.ipAddressControlNetmask.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControlNetmask.MinimumSize = new System.Drawing.Size(126, 26);
            this.ipAddressControlNetmask.Name = "ipAddressControlNetmask";
            this.ipAddressControlNetmask.ReadOnly = false;
            this.ipAddressControlNetmask.Size = new System.Drawing.Size(137, 26);
            this.ipAddressControlNetmask.TabIndex = 3;
            this.ipAddressControlNetmask.Text = "...";
            // 
            // ipAddressControlIp
            // 
            this.ipAddressControlIp.AllowInternalTab = false;
            this.ipAddressControlIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ipAddressControlIp.AutoHeight = true;
            this.ipAddressControlIp.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlIp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlIp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlIp.Location = new System.Drawing.Point(115, 13);
            this.ipAddressControlIp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControlIp.MinimumSize = new System.Drawing.Size(126, 26);
            this.ipAddressControlIp.Name = "ipAddressControlIp";
            this.ipAddressControlIp.ReadOnly = false;
            this.ipAddressControlIp.Size = new System.Drawing.Size(137, 26);
            this.ipAddressControlIp.TabIndex = 0;
            this.ipAddressControlIp.Text = "...";
            // 
            // IpAddressEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 152);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.ipAddressControlNetmask);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipAddressControlIp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IpAddressEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IP address";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IpAddressControl.IPAddressControl ipAddressControlIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private IpAddressControl.IPAddressControl ipAddressControlNetmask;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}