namespace Light_Asterisk_Caller
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            Save_button = new MaterialSkin.Controls.MaterialButton();
            Cancel_button = new MaterialSkin.Controls.MaterialButton();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            AsteriskPassword = new MaterialSkin.Controls.MaterialTextBox();
            AsteriskUser = new MaterialSkin.Controls.MaterialTextBox();
            AsteriskPort = new MaterialSkin.Controls.MaterialTextBox();
            AsteriskServer = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            materialCard2 = new MaterialSkin.Controls.MaterialCard();
            SIPCallerID = new MaterialSkin.Controls.MaterialTextBox();
            SIPContext = new MaterialSkin.Controls.MaterialTextBox();
            SIPChannel = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            LDAPSearchFilter = new MaterialSkin.Controls.MaterialTextBox();
            LDAPPassword = new MaterialSkin.Controls.MaterialTextBox();
            LDAPUser = new MaterialSkin.Controls.MaterialTextBox();
            LDAPServer = new MaterialSkin.Controls.MaterialTextBox();
            LDAPSwitch = new MaterialSkin.Controls.MaterialSwitch();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            materialCard1.SuspendLayout();
            materialCard2.SuspendLayout();
            materialCard3.SuspendLayout();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.AutoSize = false;
            Save_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Save_button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            Save_button.Depth = 0;
            Save_button.HighEmphasis = true;
            Save_button.Icon = null;
            Save_button.Location = new Point(176, 613);
            Save_button.Margin = new Padding(4, 6, 4, 6);
            Save_button.MouseState = MaterialSkin.MouseState.HOVER;
            Save_button.Name = "Save_button";
            Save_button.NoAccentTextColor = Color.Empty;
            Save_button.Size = new Size(227, 36);
            Save_button.TabIndex = 13;
            Save_button.Text = "Save";
            Save_button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            Save_button.UseAccentColor = false;
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Cancel_button
            // 
            Cancel_button.AutoSize = false;
            Cancel_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Cancel_button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            Cancel_button.Depth = 0;
            Cancel_button.HighEmphasis = true;
            Cancel_button.Icon = null;
            Cancel_button.Location = new Point(411, 613);
            Cancel_button.Margin = new Padding(4, 6, 4, 6);
            Cancel_button.MouseState = MaterialSkin.MouseState.HOVER;
            Cancel_button.Name = "Cancel_button";
            Cancel_button.NoAccentTextColor = Color.Empty;
            Cancel_button.Size = new Size(125, 36);
            Cancel_button.TabIndex = 14;
            Cancel_button.Text = "Cancel";
            Cancel_button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            Cancel_button.UseAccentColor = false;
            Cancel_button.UseVisualStyleBackColor = true;
            Cancel_button.Click += Cancel_button_Click;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(AsteriskPassword);
            materialCard1.Controls.Add(AsteriskUser);
            materialCard1.Controls.Add(AsteriskPort);
            materialCard1.Controls.Add(AsteriskServer);
            materialCard1.Controls.Add(materialLabel1);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(17, 78);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(412, 262);
            materialCard1.TabIndex = 2;
            // 
            // AsteriskPassword
            // 
            AsteriskPassword.AnimateReadOnly = false;
            AsteriskPassword.BorderStyle = BorderStyle.None;
            AsteriskPassword.Depth = 0;
            AsteriskPassword.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            AsteriskPassword.Hint = "Password";
            AsteriskPassword.LeadingIcon = null;
            AsteriskPassword.Location = new Point(17, 164);
            AsteriskPassword.MaxLength = 50;
            AsteriskPassword.MouseState = MaterialSkin.MouseState.OUT;
            AsteriskPassword.Multiline = false;
            AsteriskPassword.Name = "AsteriskPassword";
            AsteriskPassword.Password = true;
            AsteriskPassword.Size = new Size(378, 50);
            AsteriskPassword.TabIndex = 4;
            AsteriskPassword.Text = "";
            AsteriskPassword.TrailingIcon = null;
            // 
            // AsteriskUser
            // 
            AsteriskUser.AnimateReadOnly = false;
            AsteriskUser.BorderStyle = BorderStyle.None;
            AsteriskUser.Depth = 0;
            AsteriskUser.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            AsteriskUser.Hint = "AMI user";
            AsteriskUser.LeadingIcon = null;
            AsteriskUser.Location = new Point(17, 108);
            AsteriskUser.MaxLength = 50;
            AsteriskUser.MouseState = MaterialSkin.MouseState.OUT;
            AsteriskUser.Multiline = false;
            AsteriskUser.Name = "AsteriskUser";
            AsteriskUser.Size = new Size(378, 50);
            AsteriskUser.TabIndex = 3;
            AsteriskUser.Text = "";
            AsteriskUser.TrailingIcon = null;
            // 
            // AsteriskPort
            // 
            AsteriskPort.AnimateReadOnly = false;
            AsteriskPort.BorderStyle = BorderStyle.None;
            AsteriskPort.Depth = 0;
            AsteriskPort.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            AsteriskPort.Hint = "Port (5038)";
            AsteriskPort.LeadingIcon = null;
            AsteriskPort.Location = new Point(285, 52);
            AsteriskPort.MaxLength = 50;
            AsteriskPort.MouseState = MaterialSkin.MouseState.OUT;
            AsteriskPort.Multiline = false;
            AsteriskPort.Name = "AsteriskPort";
            AsteriskPort.Size = new Size(110, 50);
            AsteriskPort.TabIndex = 2;
            AsteriskPort.Text = "";
            AsteriskPort.TrailingIcon = null;
            AsteriskPort.KeyPress += AsteriskPort_KeyPress;
            // 
            // AsteriskServer
            // 
            AsteriskServer.AnimateReadOnly = false;
            AsteriskServer.BorderStyle = BorderStyle.None;
            AsteriskServer.Depth = 0;
            AsteriskServer.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            AsteriskServer.Hint = "Server IP";
            AsteriskServer.LeadingIcon = null;
            AsteriskServer.Location = new Point(17, 52);
            AsteriskServer.MaxLength = 50;
            AsteriskServer.MouseState = MaterialSkin.MouseState.OUT;
            AsteriskServer.Multiline = false;
            AsteriskServer.Name = "AsteriskServer";
            AsteriskServer.Size = new Size(262, 50);
            AsteriskServer.TabIndex = 1;
            AsteriskServer.Text = "";
            AsteriskServer.TrailingIcon = null;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(17, 14);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(117, 19);
            materialLabel1.TabIndex = 3;
            materialLabel1.Text = "Asterisk settings";
            // 
            // materialCard2
            // 
            materialCard2.BackColor = Color.FromArgb(255, 255, 255);
            materialCard2.Controls.Add(SIPCallerID);
            materialCard2.Controls.Add(SIPContext);
            materialCard2.Controls.Add(SIPChannel);
            materialCard2.Controls.Add(materialLabel2);
            materialCard2.Depth = 0;
            materialCard2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard2.Location = new Point(443, 78);
            materialCard2.Margin = new Padding(14);
            materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard2.Name = "materialCard2";
            materialCard2.Padding = new Padding(14);
            materialCard2.Size = new Size(306, 262);
            materialCard2.TabIndex = 3;
            // 
            // SIPCallerID
            // 
            SIPCallerID.AnimateReadOnly = false;
            SIPCallerID.BorderStyle = BorderStyle.None;
            SIPCallerID.Depth = 0;
            SIPCallerID.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            SIPCallerID.Hint = "Caller ID";
            SIPCallerID.LeadingIcon = null;
            SIPCallerID.Location = new Point(17, 164);
            SIPCallerID.MaxLength = 50;
            SIPCallerID.MouseState = MaterialSkin.MouseState.OUT;
            SIPCallerID.Multiline = false;
            SIPCallerID.Name = "SIPCallerID";
            SIPCallerID.Size = new Size(272, 50);
            SIPCallerID.TabIndex = 7;
            SIPCallerID.Text = "";
            SIPCallerID.TrailingIcon = null;
            // 
            // SIPContext
            // 
            SIPContext.AnimateReadOnly = false;
            SIPContext.BorderStyle = BorderStyle.None;
            SIPContext.Depth = 0;
            SIPContext.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            SIPContext.Hint = "Context (from-internal e.g.)";
            SIPContext.LeadingIcon = null;
            SIPContext.Location = new Point(17, 108);
            SIPContext.MaxLength = 50;
            SIPContext.MouseState = MaterialSkin.MouseState.OUT;
            SIPContext.Multiline = false;
            SIPContext.Name = "SIPContext";
            SIPContext.Size = new Size(272, 50);
            SIPContext.TabIndex = 6;
            SIPContext.Text = "";
            SIPContext.TrailingIcon = null;
            // 
            // SIPChannel
            // 
            SIPChannel.AnimateReadOnly = false;
            SIPChannel.BorderStyle = BorderStyle.None;
            SIPChannel.Depth = 0;
            SIPChannel.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            SIPChannel.Hint = "Channel (SIP/230 e.g.)";
            SIPChannel.LeadingIcon = null;
            SIPChannel.Location = new Point(17, 53);
            SIPChannel.MaxLength = 50;
            SIPChannel.MouseState = MaterialSkin.MouseState.OUT;
            SIPChannel.Multiline = false;
            SIPChannel.Name = "SIPChannel";
            SIPChannel.Size = new Size(272, 50);
            SIPChannel.TabIndex = 5;
            SIPChannel.Text = "";
            SIPChannel.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(17, 14);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(85, 19);
            materialLabel2.TabIndex = 3;
            materialLabel2.Text = "SIP settings";
            // 
            // materialCard3
            // 
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.Controls.Add(LDAPSearchFilter);
            materialCard3.Controls.Add(LDAPPassword);
            materialCard3.Controls.Add(LDAPUser);
            materialCard3.Controls.Add(LDAPServer);
            materialCard3.Controls.Add(LDAPSwitch);
            materialCard3.Controls.Add(materialLabel3);
            materialCard3.Depth = 0;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(17, 355);
            materialCard3.Margin = new Padding(14);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(14);
            materialCard3.Size = new Size(732, 238);
            materialCard3.TabIndex = 4;
            // 
            // LDAPSearchFilter
            // 
            LDAPSearchFilter.AnimateReadOnly = false;
            LDAPSearchFilter.BorderStyle = BorderStyle.None;
            LDAPSearchFilter.Depth = 0;
            LDAPSearchFilter.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LDAPSearchFilter.Hint = "Search filter";
            LDAPSearchFilter.LeadingIcon = null;
            LDAPSearchFilter.Location = new Point(216, 171);
            LDAPSearchFilter.MaxLength = 150;
            LDAPSearchFilter.MouseState = MaterialSkin.MouseState.OUT;
            LDAPSearchFilter.Multiline = false;
            LDAPSearchFilter.Name = "LDAPSearchFilter";
            LDAPSearchFilter.Size = new Size(499, 50);
            LDAPSearchFilter.TabIndex = 12;
            LDAPSearchFilter.Text = "";
            LDAPSearchFilter.TrailingIcon = null;
            // 
            // LDAPPassword
            // 
            LDAPPassword.AnimateReadOnly = false;
            LDAPPassword.BorderStyle = BorderStyle.None;
            LDAPPassword.Depth = 0;
            LDAPPassword.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LDAPPassword.Hint = "Password";
            LDAPPassword.LeadingIcon = null;
            LDAPPassword.Location = new Point(460, 110);
            LDAPPassword.MaxLength = 50;
            LDAPPassword.MouseState = MaterialSkin.MouseState.OUT;
            LDAPPassword.Multiline = false;
            LDAPPassword.Name = "LDAPPassword";
            LDAPPassword.Password = true;
            LDAPPassword.Size = new Size(255, 50);
            LDAPPassword.TabIndex = 11;
            LDAPPassword.Text = "";
            LDAPPassword.TrailingIcon = null;
            // 
            // LDAPUser
            // 
            LDAPUser.AnimateReadOnly = false;
            LDAPUser.BorderStyle = BorderStyle.None;
            LDAPUser.Depth = 0;
            LDAPUser.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LDAPUser.Hint = "User (urer@example.com e.g.)";
            LDAPUser.LeadingIcon = null;
            LDAPUser.Location = new Point(216, 110);
            LDAPUser.MaxLength = 50;
            LDAPUser.MouseState = MaterialSkin.MouseState.OUT;
            LDAPUser.Multiline = false;
            LDAPUser.Name = "LDAPUser";
            LDAPUser.Size = new Size(238, 50);
            LDAPUser.TabIndex = 10;
            LDAPUser.Text = "";
            LDAPUser.TrailingIcon = null;
            // 
            // LDAPServer
            // 
            LDAPServer.AnimateReadOnly = false;
            LDAPServer.BorderStyle = BorderStyle.None;
            LDAPServer.Depth = 0;
            LDAPServer.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LDAPServer.Hint = "AD address (LDAP://example.com/DC=example,DC=com e.g.)";
            LDAPServer.LeadingIcon = null;
            LDAPServer.Location = new Point(216, 54);
            LDAPServer.MaxLength = 50;
            LDAPServer.MouseState = MaterialSkin.MouseState.OUT;
            LDAPServer.Multiline = false;
            LDAPServer.Name = "LDAPServer";
            LDAPServer.Size = new Size(499, 50);
            LDAPServer.TabIndex = 9;
            LDAPServer.Text = "";
            LDAPServer.TrailingIcon = null;
            // 
            // LDAPSwitch
            // 
            LDAPSwitch.AutoSize = true;
            LDAPSwitch.Depth = 0;
            LDAPSwitch.Location = new Point(17, 54);
            LDAPSwitch.Margin = new Padding(0);
            LDAPSwitch.MouseLocation = new Point(-1, -1);
            LDAPSwitch.MouseState = MaterialSkin.MouseState.HOVER;
            LDAPSwitch.Name = "LDAPSwitch";
            LDAPSwitch.Ripple = true;
            LDAPSwitch.Size = new Size(106, 37);
            LDAPSwitch.TabIndex = 8;
            LDAPSwitch.Text = "Enable";
            LDAPSwitch.UseVisualStyleBackColor = true;
            LDAPSwitch.CheckedChanged += LDAPSwitch_CheckedChanged;
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(17, 14);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(101, 19);
            materialLabel3.TabIndex = 5;
            materialLabel3.Text = "LDAP settings";
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(773, 677);
            Controls.Add(materialCard3);
            Controls.Add(materialCard2);
            Controls.Add(materialCard1);
            Controls.Add(Cancel_button);
            Controls.Add(Save_button);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(773, 677);
            MinimumSize = new Size(773, 677);
            Name = "Options";
            Sizable = false;
            Text = "Options";
            FormClosed += Options_FormClosed;
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            materialCard2.ResumeLayout(false);
            materialCard2.PerformLayout();
            materialCard3.ResumeLayout(false);
            materialCard3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialButton Save_button;
        private MaterialSkin.Controls.MaterialButton Cancel_button;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox AsteriskPort;
        private MaterialSkin.Controls.MaterialTextBox AsteriskServer;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialTextBox AsteriskPassword;
        private MaterialSkin.Controls.MaterialTextBox AsteriskUser;
        private MaterialSkin.Controls.MaterialTextBox SIPContext;
        private MaterialSkin.Controls.MaterialTextBox SIPChannel;
        private MaterialSkin.Controls.MaterialTextBox SIPCallerID;
        private MaterialSkin.Controls.MaterialTextBox LDAPSearchFilter;
        private MaterialSkin.Controls.MaterialTextBox LDAPPassword;
        private MaterialSkin.Controls.MaterialTextBox LDAPUser;
        private MaterialSkin.Controls.MaterialTextBox LDAPServer;
        private MaterialSkin.Controls.MaterialSwitch LDAPSwitch;
    }
}