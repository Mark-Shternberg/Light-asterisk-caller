namespace Light_Asterisk_Caller
{
    partial class Call
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Call));
            Search_LDAP = new MaterialSkin.Controls.MaterialTextBox2();
            Search_LDAP_combobox = new MaterialSkin.Controls.MaterialComboBox();
            Search_LDAP_phone = new MaterialSkin.Controls.MaterialComboBox();
            Phone = new MaterialSkin.Controls.MaterialTextBox2();
            Call_button = new MaterialSkin.Controls.MaterialButton();
            Cancel_button = new MaterialSkin.Controls.MaterialButton();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            sip_exist_label = new Label();
            menuStrip1 = new MenuStrip();
            settings_button = new ToolStripMenuItem();
            materialCard1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Search_LDAP
            // 
            Search_LDAP.AnimateReadOnly = false;
            Search_LDAP.BackgroundImageLayout = ImageLayout.None;
            Search_LDAP.CharacterCasing = CharacterCasing.Normal;
            Search_LDAP.Depth = 0;
            Search_LDAP.Enabled = false;
            Search_LDAP.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            Search_LDAP.HideSelection = true;
            Search_LDAP.Hint = "Search LDAP (Loading ... )";
            Search_LDAP.LeadingIcon = null;
            Search_LDAP.Location = new Point(11, 17);
            Search_LDAP.MaxLength = 32767;
            Search_LDAP.MouseState = MaterialSkin.MouseState.OUT;
            Search_LDAP.Name = "Search_LDAP";
            Search_LDAP.PasswordChar = '\0';
            Search_LDAP.PrefixSuffixText = null;
            Search_LDAP.ReadOnly = false;
            Search_LDAP.RightToLeft = RightToLeft.No;
            Search_LDAP.SelectedText = "";
            Search_LDAP.SelectionLength = 0;
            Search_LDAP.SelectionStart = 0;
            Search_LDAP.ShortcutsEnabled = true;
            Search_LDAP.Size = new Size(286, 48);
            Search_LDAP.TabIndex = 2;
            Search_LDAP.TabStop = false;
            Search_LDAP.TextAlign = HorizontalAlignment.Left;
            Search_LDAP.TrailingIcon = null;
            Search_LDAP.UseSystemPasswordChar = false;
            Search_LDAP.TextChanged += Search_LDAP_TextChanged;
            // 
            // Search_LDAP_combobox
            // 
            Search_LDAP_combobox.AutoResize = false;
            Search_LDAP_combobox.BackColor = Color.FromArgb(255, 255, 255);
            Search_LDAP_combobox.Depth = 0;
            Search_LDAP_combobox.DrawMode = DrawMode.OwnerDrawVariable;
            Search_LDAP_combobox.DropDownHeight = 174;
            Search_LDAP_combobox.DropDownStyle = ComboBoxStyle.DropDownList;
            Search_LDAP_combobox.DropDownWidth = 121;
            Search_LDAP_combobox.Enabled = false;
            Search_LDAP_combobox.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            Search_LDAP_combobox.ForeColor = Color.FromArgb(222, 0, 0, 0);
            Search_LDAP_combobox.FormattingEnabled = true;
            Search_LDAP_combobox.Hint = "Name";
            Search_LDAP_combobox.IntegralHeight = false;
            Search_LDAP_combobox.ItemHeight = 43;
            Search_LDAP_combobox.Location = new Point(11, 83);
            Search_LDAP_combobox.MaxDropDownItems = 4;
            Search_LDAP_combobox.MouseState = MaterialSkin.MouseState.OUT;
            Search_LDAP_combobox.Name = "Search_LDAP_combobox";
            Search_LDAP_combobox.Size = new Size(286, 49);
            Search_LDAP_combobox.StartIndex = 0;
            Search_LDAP_combobox.TabIndex = 3;
            Search_LDAP_combobox.SelectedIndexChanged += Search_LDAP_combobox_SelectedIndexChanged;
            Search_LDAP_combobox.TextUpdate += Search_LDAP_combobox_TextUpdate;
            // 
            // Search_LDAP_phone
            // 
            Search_LDAP_phone.AutoResize = false;
            Search_LDAP_phone.BackColor = Color.FromArgb(255, 255, 255);
            Search_LDAP_phone.Depth = 0;
            Search_LDAP_phone.DrawMode = DrawMode.OwnerDrawVariable;
            Search_LDAP_phone.DropDownHeight = 174;
            Search_LDAP_phone.DropDownStyle = ComboBoxStyle.DropDownList;
            Search_LDAP_phone.DropDownWidth = 121;
            Search_LDAP_phone.Enabled = false;
            Search_LDAP_phone.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            Search_LDAP_phone.ForeColor = Color.FromArgb(222, 0, 0, 0);
            Search_LDAP_phone.FormattingEnabled = true;
            Search_LDAP_phone.Hint = "Phone";
            Search_LDAP_phone.IntegralHeight = false;
            Search_LDAP_phone.ItemHeight = 43;
            Search_LDAP_phone.Location = new Point(11, 147);
            Search_LDAP_phone.MaxDropDownItems = 4;
            Search_LDAP_phone.MouseState = MaterialSkin.MouseState.OUT;
            Search_LDAP_phone.Name = "Search_LDAP_phone";
            Search_LDAP_phone.Size = new Size(286, 49);
            Search_LDAP_phone.StartIndex = 0;
            Search_LDAP_phone.TabIndex = 4;
            Search_LDAP_phone.SelectedIndexChanged += Search_LDAP_phone_SelectedIndexChanged;
            // 
            // Phone
            // 
            Phone.AccessibleRole = AccessibleRole.Cell;
            Phone.AnimateReadOnly = false;
            Phone.BackgroundImageLayout = ImageLayout.None;
            Phone.CharacterCasing = CharacterCasing.Normal;
            Phone.Depth = 0;
            Phone.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            Phone.HideSelection = true;
            Phone.Hint = "Phone";
            Phone.LeadingIcon = null;
            Phone.Location = new Point(29, 95);
            Phone.MaxLength = 100;
            Phone.MouseState = MaterialSkin.MouseState.OUT;
            Phone.Name = "Phone";
            Phone.PasswordChar = '\0';
            Phone.PrefixSuffixText = null;
            Phone.ReadOnly = false;
            Phone.RightToLeft = RightToLeft.No;
            Phone.SelectedText = "";
            Phone.SelectionLength = 0;
            Phone.SelectionStart = 0;
            Phone.ShortcutsEnabled = true;
            Phone.Size = new Size(423, 48);
            Phone.TabIndex = 1;
            Phone.TabStop = false;
            Phone.TextAlign = HorizontalAlignment.Left;
            Phone.TrailingIcon = null;
            Phone.UseSystemPasswordChar = false;
            Phone.KeyPress += Phone_KeyPress;
            Phone.TextChanged += Phone_TextChanged;
            // 
            // Call_button
            // 
            Call_button.AutoSize = false;
            Call_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Call_button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            Call_button.Depth = 0;
            Call_button.HighEmphasis = true;
            Call_button.Icon = null;
            Call_button.Location = new Point(52, 174);
            Call_button.Margin = new Padding(4, 6, 4, 6);
            Call_button.MouseState = MaterialSkin.MouseState.HOVER;
            Call_button.Name = "Call_button";
            Call_button.NoAccentTextColor = Color.Empty;
            Call_button.Size = new Size(399, 36);
            Call_button.TabIndex = 5;
            Call_button.Text = "Call";
            Call_button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            Call_button.UseAccentColor = false;
            Call_button.UseVisualStyleBackColor = true;
            Call_button.Click += Call_button_Click;
            // 
            // Cancel_button
            // 
            Cancel_button.AutoSize = false;
            Cancel_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Cancel_button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            Cancel_button.Depth = 0;
            Cancel_button.HighEmphasis = true;
            Cancel_button.Icon = null;
            Cancel_button.Location = new Point(168, 225);
            Cancel_button.Margin = new Padding(4, 6, 4, 6);
            Cancel_button.MouseState = MaterialSkin.MouseState.HOVER;
            Cancel_button.Name = "Cancel_button";
            Cancel_button.NoAccentTextColor = Color.Empty;
            Cancel_button.Size = new Size(170, 36);
            Cancel_button.TabIndex = 6;
            Cancel_button.Text = "Cancel";
            Cancel_button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            Cancel_button.UseAccentColor = false;
            Cancel_button.UseVisualStyleBackColor = true;
            Cancel_button.Click += Cancel_button_Click;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(Search_LDAP);
            materialCard1.Controls.Add(Search_LDAP_combobox);
            materialCard1.Controls.Add(Search_LDAP_phone);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(469, 78);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(314, 216);
            materialCard1.TabIndex = 7;
            // 
            // sip_exist_label
            // 
            sip_exist_label.AutoSize = true;
            sip_exist_label.BackColor = Color.Transparent;
            sip_exist_label.Font = new Font("Sans Serif Collection", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sip_exist_label.ForeColor = Color.IndianRed;
            sip_exist_label.Location = new Point(148, 146);
            sip_exist_label.Name = "sip_exist_label";
            sip_exist_label.Size = new Size(204, 27);
            sip_exist_label.TabIndex = 9;
            sip_exist_label.Text = "SIP settings doesn't exist";
            sip_exist_label.Visible = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { settings_button });
            menuStrip1.Location = new Point(3, 64);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(794, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // settings_button
            // 
            settings_button.Name = "settings_button";
            settings_button.Size = new Size(61, 20);
            settings_button.Text = "Settings";
            settings_button.Click += settings_button_Click;
            // 
            // Call
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 309);
            Controls.Add(sip_exist_label);
            Controls.Add(Cancel_button);
            Controls.Add(Call_button);
            Controls.Add(Phone);
            Controls.Add(materialCard1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(800, 309);
            MinimumSize = new Size(800, 309);
            Name = "Call";
            Sizable = false;
            Text = "Call";
            FormClosed += Call_FormClosed;
            materialCard1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox2 Search_LDAP;
        private MaterialSkin.Controls.MaterialComboBox Search_LDAP_combobox;
        private MaterialSkin.Controls.MaterialComboBox Search_LDAP_phone;
        private MaterialSkin.Controls.MaterialTextBox2 Phone;
        private MaterialSkin.Controls.MaterialButton Call_button;
        private MaterialSkin.Controls.MaterialButton Cancel_button;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private Label sip_exist_label;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem settings_button;
    }
}