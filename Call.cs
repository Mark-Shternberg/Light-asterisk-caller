using AsterNET.Manager.Action;
using AsterNET.Manager.Response;
using AsterNET.Manager;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace Light_Asterisk_Caller
{
    public partial class Call : MaterialForm
    {
        private IConfiguration Configuration;

        public DataTable ldap_results_table { get; set; } = new DataTable();

        public Call(IConfiguration configuration)
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

            set_up_settings();
        }

        // SET UP SETTINGS
        private void set_up_settings()
        {
            // CHECK SETTINGS EXIST
            bool sip_exist = check_sip_exist();
            if (sip_exist == false)
            {
                Phone.Enabled = false;
                sip_exist_label.Visible = true;
                sip_exist_label.ForeColor = Color.IndianRed;
                sip_exist_label.Font = new Font("Sans Serif Collection", 8, FontStyle.Bold);
                Call_button.Enabled = false;
            }
            else
            {
                Phone.Enabled = true;
                sip_exist_label.Visible = false;
                Call_button.Enabled = true;
            }

            // LDAP SEARCH
            var ldap_active = Configuration["LDAP-settings:ACTIVE"];
            if (ldap_active == "True")
            {
                Search_LDAP.Enabled = true;
                Search_LDAP_combobox.Enabled = true;
                Search_LDAP_phone.Enabled = true;
                Search_LDAP.Hint = "Search LDAP (Loading ... )";

                // Запускаем LDAP_search асинхронно
                try
                {
                    Task.Run(() => LDAP_search())
                        .ContinueWith(task =>
                        {
                            // Код выполняется по завершении LDAP_search
                            if (task.Exception == null)
                            {
                                // Проверяем, что форма и её элементы ещё существуют
                                if (this.IsHandleCreated && !this.IsDisposed)
                                {
                                    try
                                    {
                                        // Обновляем элементы интерфейса в UI потоке
                                        this.Invoke(new Action(() =>
                                        {
                                            Search_LDAP.Enabled = true;
                                            Search_LDAP.Hint = "Search LDAP";
                                            Search_LDAP_combobox.Enabled = true;
                                            Search_LDAP_phone.Enabled = true;
                                        }));
                                    }
                                    catch (ObjectDisposedException)
                                    {
                                        // Игнорируем исключение, если объект был уничтожен во время операции
                                    }
                                }
                            }
                            else
                            {
                                // Обработка ошибок
                                MessageBox.Show($"Ошибка LDAP: {task.Exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Search_LDAP.Hint = "Error";
                                Search_LDAP.Enabled = false;
                                Search_LDAP_combobox.Enabled = false;
                                Search_LDAP_phone.Enabled = false;
                            }
                        }, TaskScheduler.FromCurrentSynchronizationContext()); // Возвращаем выполнение в UI поток
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при инициации звонка: {ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else Search_LDAP.Hint = "LDAP disabled";
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

        // CHECK SIP SETTINGS EXIST
        private bool check_sip_exist()
        {
            int error = 0;

            if (Configuration["Asterisk-Connection:server"] == "") error++; // IP адрес Asterisk сервера
            if (Configuration["Asterisk-Connection:port"] == "") error++;               // Порт AMI (по умолчанию 5038)
            if (Configuration["Asterisk-Connection:user"] == "") error++;     // Логин AMI пользователя
            if (Configuration["Asterisk-Connection:password"] == "") error++;  // Пароль AMI пользователя
            if (Configuration["SIP-settings:channel"] == "") error++;         // Внутренний номер вызывающего абонента
            if (Configuration["SIP-settings:context"] == "") error++;          // Контекст, определённый в Asterisk
            if (Configuration["SIP-settings:caller-id"] == "") error++;      // Caller ID

            if (error == 0) return true;
            else return false;
        }

        // CALL FUNCTION
        private async void call()
        {
            // Настройки подключения к Asterisk
            var asteriskHost = Configuration["Asterisk-Connection:server"]; // IP адрес Asterisk сервера
            int asteriskPort = Convert.ToInt32(Configuration["Asterisk-Connection:port"]); // Порт AMI (по умолчанию 5038)
            var asteriskUsername = Configuration["Asterisk-Connection:user"]; // Логин AMI пользователя
            var asteriskPassword = Configuration["Asterisk-Connection:password"]; // Пароль AMI пользователя

            // Данные для звонка
            var fromExtension = Configuration["SIP-settings:channel"]; // Внутренний номер вызывающего абонента
            string toNumber = RemoveUnwantedCharacters(Phone.Text); // Номер, на который звоним
            var context = Configuration["SIP-settings:context"]; // Контекст, определённый в Asterisk

            // Инициализируем подключение к Asterisk
            ManagerConnection manager = new ManagerConnection(asteriskHost, asteriskPort, asteriskUsername, asteriskPassword);

            try
            {
                // Подключаемся к Asterisk
                manager.Login();

                // Создаем запрос на инициирование звонка
                OriginateAction originate = new OriginateAction();
                originate.Channel = fromExtension; // Канал вызова (например, SIP/1001)
                originate.Context = context; // Контекст для вызова
                originate.Exten = toNumber; // Номер, на который будет звонок
                originate.Priority = "1"; // Приоритет вызова
                originate.CallerId = fromExtension; // ID вызывающего абонента
                originate.Timeout = 30000; // Тайм-аут на вызов (30 секунд)

                // Отключаем кнопку вызова, чтобы пользователь не мог нажимать её повторно
                Call_button.Enabled = false;
                Call_button.Text = "Call in progress...";
                Phone.Enabled = false;

                // Запускаем вызов асинхронно, чтобы не блокировать UI
                ManagerResponse originateResponse = await Task.Run(() => manager.SendAction(originate, originate.Timeout));

                // Проверяем ответ от Asterisk
                if (originateResponse.Response != "Success")
                {
                    MessageBox.Show($"Ошибка при инициации звонка: {originateResponse.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Отключаемся от Asterisk
                manager.Logoff();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициации звонка: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Возвращаем кнопку вызова в активное состояние
                Call_button.Enabled = true;
                Call_button.Text = "Call";
                Phone.Enabled = true;
            }
        }

        // CALL BUTTON
        private void Call_button_Click(object sender, EventArgs e)
        {
            call();
        }

        // REMOVE UNWANTED CHARACTERS FROM PHONE NUMBER
        static string RemoveUnwantedCharacters(string input)
        {
            // Удаляем пробелы, скобки и дефисы
            return input.Replace(" ", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("+7", "8")
                        .Replace("-", "");
        }

        //-----------------------------------------------------------------------------//

        // LDAP load
        private void LDAP_search()
        {
            // Параметры подключения к LDAP серверу
            var ldapPath = Configuration["LDAP-settings:server"]; // URL вашего LDAP сервера
            var username = Configuration["LDAP-settings:user"]; // Имя пользователя
            var password = Configuration["LDAP-settings:password"]; // Пароль

            // Создаем объект DirectoryEntry для подключения к LDAP серверу
            using (DirectoryEntry entry = new DirectoryEntry(ldapPath, username, password))
            {
                // Создаем объект DirectorySearcher для выполнения поиска
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    // Задаем фильтр для поиска пользователей
                    searcher.Filter = Configuration["LDAP-settings:filter"]; // Фильтр поиска
                    searcher.PropertiesToLoad.Add("givenName");
                    searcher.PropertiesToLoad.Add("sn");
                    searcher.PropertiesToLoad.Add("mobile");
                    searcher.PropertiesToLoad.Add("telephoneNumber");

                    // Создаем таблицу для хранения результатов
                    ldap_results_table.Columns.Clear();
                    ldap_results_table.Rows.Clear();
                    ldap_results_table.Columns.Add("Name", typeof(string));
                    ldap_results_table.Columns.Add("Mobile", typeof(string));
                    ldap_results_table.Columns.Add("Telephone Number", typeof(string));

                    // Выполняем поиск
                    SearchResultCollection results = searcher.FindAll();

                    // Обрабатываем результаты поиска
                    foreach (SearchResult result in results)
                    {
                        DirectoryEntry userEntry = result.GetDirectoryEntry();

                        // Получаем информацию о пользователе
                        string givenName = userEntry.Properties["givenName"].Value?.ToString() ?? "N/A";
                        string sn = userEntry.Properties["sn"].Value?.ToString() ?? "N/A";
                        string mobile = userEntry.Properties["mobile"].Value?.ToString() ?? "N/A";
                        string telephoneNumber = userEntry.Properties["telephoneNumber"].Value?.ToString() ?? "N/A";

                        // Добавляем строку в таблицу
                        ldap_results_table.Rows.Add(givenName + " " + sn, mobile, telephoneNumber);

                        // Проверяем, что форма и её элементы ещё существуют
                        if (this.IsHandleCreated && !this.IsDisposed)
                        {
                            try
                            {
                                // Используем Invoke для добавления элемента в ComboBox в основном потоке UI
                                this.Invoke(new Action(() =>
                                {
                                    if (Search_LDAP_combobox != null && !Search_LDAP_combobox.IsDisposed)
                                    {
                                        Search_LDAP_combobox.Items.Add(givenName + ' ' + sn);
                                    }
                                }));
                            }
                            catch (ObjectDisposedException)
                            {
                                // Игнорируем исключение, если объект был уничтожен во время операции
                            }
                        }
                    }
                }
            }
        }

        // LDAP SEARCH TEXT CHANGED
        private void Search_LDAP_TextChanged(object sender, EventArgs e)
        {
            // Очищаем ComboBox1 перед обновлением
            Search_LDAP_combobox.Items.Clear();

            // Получаем введенный текст
            string searchText = Search_LDAP.Text.ToLower();

            // Фильтруем строки по первому столбцу "Name"
            var matchingRows = ldap_results_table.AsEnumerable()
                .Where(row => row.Field<string>("Name").ToLower().Contains(searchText));

            // Добавляем совпадения в ComboBox1
            foreach (var row in matchingRows)
            {
                Search_LDAP_combobox.Items.Add(row.Field<string>("Name"));
            }

            // Если совпадение одно, сразу выбираем его
            if (Search_LDAP_combobox.Items.Count == 1)
            {
                Search_LDAP_combobox.SelectedIndex = 0;
                this.Refresh();
            }
        }

        // LDAP SEARCH COMBOBOX INDEX CHANGED
        private void Search_LDAP_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очищаем Search_LDAP_phone перед добавлением новых значений
            Search_LDAP_phone.Items.Clear();

            // Получаем выбранный элемент из Search_LDAP_combobox
            string selectedName = Search_LDAP_combobox.SelectedItem?.ToString();

            if (selectedName != null)
            {
                // Ищем строку с соответствующим значением в столбце "Name"
                var matchingRow = ldap_results_table.AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("Name") == selectedName);

                if (matchingRow != null)
                {
                    // Добавляем номера в ComboBox2
                    Search_LDAP_phone.Items.Add(matchingRow.Field<string>("Telephone Number"));
                    Search_LDAP_phone.Items.Add(matchingRow.Field<string>("Mobile"));

                    // Автоматически выбираем первый элемент в ComboBox2
                    Search_LDAP_phone.SelectedIndex = 0;
                    this.Refresh();
                }
            }
        }

        // LDAP SEARCH COMBOBOX TEXT CHANGED
        private void Search_LDAP_combobox_TextUpdate(object sender, EventArgs e)
        {
            if (Search_LDAP_combobox.SelectedIndex.ToString() == "") Search_LDAP_phone.Items.Clear();
        }

        // LDAP PHONE COMBOBOX INDEX CHANGED
        private void Search_LDAP_phone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Phone.Text = Search_LDAP_phone.SelectedItem.ToString();
        }

        //-----------------------------------------------------------------------------//

        // FORM CLOSING
        private void Call_FormClosed(object sender, FormClosedEventArgs e)
        {
            functions.FlushMem();
        }

        // FORM CLOSING (cancel button)
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            functions.FlushMem();
            this.Close();
        }

        // ENTER KEY PRESS
        private void Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли символ цифрой, или одним из допустимых символов: '+', '(', ')', '-'
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '-' && e.KeyChar != '\b')
            {
                // Разрешаем сочетание CTRL + V (вставка)
                if (!(e.KeyChar == 22 && Control.ModifierKeys == Keys.Control))  // 22 - это ASCII-код символа V
                {
                    // Если символ не соответствует, отменяем его ввод
                    e.Handled = true;
                }
            }

            // Если нажата клавиша Enter (код 13), выполняем вызов функции call()
            if (e.KeyChar == (char)13)
            {
                call();
            }
        }

        // PHONE VALIDATION
        private void Phone_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9+\-() ]*$");

            MatchCollection matches = regex.Matches(Phone.Text);
            if (matches.Count == 0)
            {
                Phone.Text = "";
                MessageBox.Show("Введён неверный номер телефона", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void settings_button_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            int check = 0;

            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Text == "Options")
                {
                    check++;
                    frm.Focus();
                    break;
                }
            }

            if (check == 0)
            {
                using (Options Options = new Options(Configuration))
                {
                    Options.ShowDialog();
                    set_up_settings();
                }
            }
        }
    }
}
