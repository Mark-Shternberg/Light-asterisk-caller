using AsterNET.Manager.Action;
using AsterNET.Manager.Response;
using AsterNET.Manager;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Light_Asterisk_Caller
{
    internal static class Program
    {
        private static Mutex mutex = null;
        private static NotifyIcon trayIcon;
        private static IConfiguration configuration;

        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            try
            {
                // CONFIGURATION LOAD
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                configuration = builder.Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке конфигурации: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            string allArguments = "";
            if (args.Length > 0)
            {
                // Объединяем все аргументы в одну строку
                allArguments = string.Join("", args);

                if (allArguments == "callform")
                {
                    ShowCallForm(configuration);
                }
                else
                {
                    // Передаем объединенную строку в функцию
                    QuickCall quickCall = new QuickCall(allArguments, configuration);
                    Environment.Exit(0);
                }
            }

            // Проверка на наличие второго экземпляра программы в трее
            const string appName = "LightAsteriskCallerApp";
            mutex = new Mutex(true, appName, out bool createdNew);

            if (!createdNew)
            {
                if (allArguments == "callform") Environment.Exit(0);
                // Если программа уже запущена, выводим сообщение с кнопками OK и Cancel
                DialogResult result = MessageBox.Show("Program is already running! Do you want to restart?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                if (result == DialogResult.OK)
                {
                    // Завершаем все процессы программы "Light Asterisk Caller.exe"
                    KillExistingProcesses();
                }
                else if (result == DialogResult.Cancel)
                {
                    // Логика для выхода из программы
                    Environment.Exit(0);    // Просто выходим
                }
            }

            // Создаем иконку для трея
            InitializeTrayIcon(configuration);

            // Запускаем приложение, но не показываем никакую форму
            Application.Run();
        }

        // CREATE TRAY ICON
        private static void InitializeTrayIcon(IConfiguration configuration)
        {
            trayIcon = new NotifyIcon
            {
                Icon = new Icon("Resources/favicon.ico"),
                Visible = true,
                Text = "Light Asterisk Caller",
                ContextMenuStrip = CreateTrayMenu(configuration)
            };

            // Подписываемся на событие двойного клика
            trayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
        }

        // CREATE TRAY MENU
        private static ContextMenuStrip CreateTrayMenu(IConfiguration configuration)
        {

            var menu = new ContextMenuStrip();
            menu.Items.Add("Call", null, (sender, e) => ShowCallForm(configuration));
            menu.Items.Add("Options", null, (sender, e) => ShowOptionsForm(configuration));
            menu.Items.Add("Exit", null, (sender, e) => ExitApplication());
            return menu;
        }

        // Метод для показа формы Call
        private static void ShowCallForm(IConfiguration configuration)
        {
            // Проверяем, открыта ли форма типа Call
            Form callForm = Application.OpenForms.OfType<Call>().FirstOrDefault();

            if (callForm != null)
            {
                // Если форма уже существует, активируем её
                callForm.WindowState = FormWindowState.Normal; // Восстановить, если свернута
                callForm.Focus();
            }
            else
            {
                // Если формы нет, создаём новую
                Call newCallForm = new Call(configuration);
                newCallForm.Show();  // Используем Show для немодального окна
            }
        }

        // Метод для показа формы Options
        private static void ShowOptionsForm(IConfiguration configuration)
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
                using (Options Options = new Options(configuration))
                {
                    //Options.FormClosed += Options_FormClosed;
                    Options.ShowDialog();
                }
            }
        }

        private static void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Убедитесь, что иконка в трее корректно обновлена
            InitializeTrayIcon(configuration);
        }

        // KILL PROGRAM IF IT's ALREADY RUNNING
        private static void KillExistingProcesses()
        {
            var currentProcessName = "Light Asterisk Caller";
            foreach (var process in System.Diagnostics.Process.GetProcessesByName(currentProcessName))
            {
                if (process.Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                {
                    process.Kill();
                }
            }
        }

        // Обработчик события двойного клика по иконке в трее
        private static void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowCallForm(configuration);
            }
        }

        // Метод для выхода из приложения
        private static void ExitApplication()
        {
            trayIcon.Visible = false; // Скрыть иконку перед выходом
            Application.Exit();
        }
    }
}