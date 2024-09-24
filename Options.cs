using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Asterisk_Caller
{
    public partial class Options : MaterialForm
    {
        private IConfiguration Configuration;

        public Options(IConfiguration configuration)
        {
            Configuration = configuration;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            bool isDarkMode = IsDarkTheme();

            if (isDarkMode == true) { materialSkinManager.Theme = MaterialSkinManager.Themes.DARK; }
            else { materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; }

            materialSkinManager.EnforceBackcolorOnAllComponents = true;

            InitializeComponent();
            LoadCurrentSetting();
        }

        // CHECK SYSTEM THEME
        static bool IsDarkTheme()
        {
            const string registryKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string valueName = "AppsUseLightTheme";
            object registryValue = Registry.GetValue(registryKey, valueName, null);

            if (registryValue is int themeValue)
            {
                return themeValue == 0; // 0 означает тёмную тему
            }

            // Если параметр не найден, считаем, что используется светлая тема
            return false;
        }

        // LOAD CURRENT SETTINGS
        private void LoadCurrentSetting()
        {
            // Настройки подключения к Asterisk
            if (Configuration["Asterisk-Connection:server"] == "") AsteriskServer.Text = "";
            else AsteriskServer.Text = Configuration["Asterisk-Connection:server"]; // IP адрес Asterisk сервера
            if (Configuration["Asterisk-Connection:port"] == "") AsteriskPort.Text = "";
            else AsteriskPort.Text = Configuration["Asterisk-Connection:port"];               // Порт AMI (по умолчанию 5038)
            if (Configuration["Asterisk-Connection:user"] == "") AsteriskUser.Text = "";
            else AsteriskUser.Text = Configuration["Asterisk-Connection:user"];     // Логин AMI пользователя
            if (Configuration["Asterisk-Connection:password"] == "") AsteriskPassword.Text = "";
            else AsteriskPassword.Text = Configuration["Asterisk-Connection:password"];  // Пароль AMI пользователя

            // Данные для звонка
            if (Configuration["SIP-settings:channel"] == "") SIPChannel.Text = "";
            else SIPChannel.Text = Configuration["SIP-settings:channel"];         // Внутренний номер вызывающего абонента
            if (Configuration["SIP-settings:context"] == "") SIPContext.Text = "";
            else SIPContext.Text = Configuration["SIP-settings:context"];          // Контекст, определённый в Asterisk
            if (Configuration["SIP-settings:caller-id"] == "") SIPCallerID.Text = "";
            else SIPCallerID.Text = Configuration["SIP-settings:caller-id"];      // Caller ID

            // Данные LDAP
            var LDAP_active = Configuration["LDAP-settings:ACTIVE"]; // Включен ли LDAP
            if (Configuration["LDAP-settings:server"] == "") LDAPServer.Text = "";
            else LDAPServer.Text = Configuration["LDAP-settings:server"]; // IP адрес AD сервера
            if (Configuration["LDAP-settings:user"] == "") LDAPUser.Text = "";
            else LDAPUser.Text = Configuration["LDAP-settings:user"];     // Логин AD пользователя
            if (Configuration["LDAP-settings:password"] == "") LDAPPassword.Text = "";
            else LDAPPassword.Text = Configuration["LDAP-settings:password"];  // Пароль AD пользователя
            if (Configuration["LDAP-settings:filter"] == "") LDAPSearchFilter.Text = "";
            else LDAPSearchFilter.Text = Configuration["LDAP-settings:filter"];  // Фильтр поиска

            if (LDAP_active == "True")
            {
                LDAPSwitch.Checked = true;
                LDAPPassword.Enabled = true;
                LDAPServer.Enabled = true;
                LDAPUser.Enabled = true;
                LDAPSearchFilter.Enabled = true;
            }
            else
            {
                LDAPSwitch.Checked = false;
                LDAPPassword.Enabled = false;
                LDAPServer.Enabled = false;
                LDAPUser.Enabled = false;
                LDAPSearchFilter.Enabled = false;
            }
        }

        // LDAP SWITCH CHECK CHANGED
        private void LDAPSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (LDAPSwitch.Checked == true)
            {
                LDAPPassword.Enabled = true;
                LDAPServer.Enabled = true;
                LDAPUser.Enabled = true;
                LDAPSearchFilter.Enabled = true;
            }
            else
            {
                LDAPPassword.Enabled = false;
                LDAPServer.Enabled = false;
                LDAPUser.Enabled = false;
                LDAPSearchFilter.Enabled = false;
            }
        }

        // SAVE BUTTON
        private async void Save_button_Click(object sender, EventArgs e)
        {
            // Путь к файлу appsettings.json
            string filePath = "appsettings.json";

            // Чтение файла appsettings.json
            string jsonString = File.ReadAllText(filePath);

            // Парсим содержимое в объект JsonNode
            JsonNode settings = JsonNode.Parse(jsonString);

            int errors = 0;

            // Меняем значения в конфигурации
            if (AsteriskServer.Text == null || AsteriskServer.Text == "")
            {
                errors++;
                MessageBox.Show("Введите адрес сервера", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else settings["Asterisk-Connection"]["server"] = AsteriskServer.Text;
            if (AsteriskPort.Text == "" || AsteriskPort.Text == null) settings["Asterisk-Connection"]["port"] = "5038";
            else settings["Asterisk-Connection"]["port"] = AsteriskPort.Text;

            if (AsteriskUser.Text == null || AsteriskUser.Text == "")
            {
                errors++;
                MessageBox.Show("Введите пользователя Asterisk AMI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else settings["Asterisk-Connection"]["user"] = AsteriskUser.Text;

            if (AsteriskPassword.Text == null || AsteriskPassword.Text == "")
            {
                errors++;
                MessageBox.Show("Введите пароль пользователя Asterisk AMI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else settings["Asterisk-Connection"]["password"] = AsteriskPassword.Text;

            if (SIPChannel.Text == null || SIPChannel.Text == "")
            {
                errors++;
                MessageBox.Show("Введите SIP channel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else settings["SIP-settings"]["channel"] = SIPChannel.Text;

            if (SIPContext.Text == null || SIPContext.Text == "")
            {
                errors++;
                MessageBox.Show("Введите SIP context", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else settings["SIP-settings"]["context"] = SIPContext.Text;

            if (SIPCallerID.Text == null || SIPCallerID.Text == "")
            {
                errors++;
                MessageBox.Show("Введите Caller ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else settings["SIP-settings"]["caller-id"] = SIPCallerID.Text;

            if (LDAPSwitch.Checked == true)
            {
                if (LDAPServer.Text == null || LDAPServer.Text == "")
                {
                    errors++;
                    MessageBox.Show("Введите LDAP сервер", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else settings["LDAP-settings"]["server"] = LDAPServer.Text;

                if (LDAPUser.Text == null || LDAPUser.Text == "")
                {
                    errors++;
                    MessageBox.Show("Введите LDAP пользователя", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else settings["LDAP-settings"]["user"] = LDAPUser.Text;

                if (LDAPPassword.Text == null || LDAPPassword.Text == "")
                {
                    errors++;
                    MessageBox.Show("Введите LDAP пароль", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else settings["LDAP-settings"]["password"] = LDAPPassword.Text;

                if (LDAPSearchFilter.Text == null || LDAPSearchFilter.Text == "")
                {
                    errors++;
                    MessageBox.Show("Введите LDAP фильтр", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else settings["LDAP-settings"]["filter"] = LDAPSearchFilter.Text;

                if (errors == 0) settings["LDAP-settings"]["ACTIVE"] = "True";
            }
            else settings["LDAP-settings"]["ACTIVE"] = "False";

            // Сериализация обратно в строку
            string updatedJsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });

            // Записываем обновленные данные обратно в файл
            if (errors == 0)
            {
                try
                {
                    await File.WriteAllTextAsync(filePath, updatedJsonString);
                    //ReloadConfiguration();
                    AsteriskPassword.Text = "";
                    LDAPPassword.Text = "";
                    MessageBox.Show("Изменения сохранены", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    functions.FlushMem();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при записи настроек: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        // RELOAD CONFIGURATION
        public void ReloadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Базовая директория
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            Configuration = builder.Build();
        }

        // CANCEL BUTTON
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            functions.FlushMem();
            this.Close();
        }

        // FORM CLOSED
        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            functions.FlushMem();
        }

        // PORT ONLY NUMBERS
        private void AsteriskPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
