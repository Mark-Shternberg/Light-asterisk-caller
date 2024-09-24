using AsterNET.Manager.Action;
using AsterNET.Manager.Response;
using AsterNET.Manager;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialSkin;

namespace Light_Asterisk_Caller
{
    internal class QuickCall
    {
        private readonly IConfiguration Configuration;

        public QuickCall(string phone, IConfiguration configuration)
        {
            Configuration = configuration;

            if(check_sip_exist() == false)
            {
                MessageBox.Show($"Настройки SIP отсутствуют", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else call(phone);
        }

        // CALL FUNCTION
        private async void call(string phone)
        {
            // Настройки подключения к Asterisk
            var asteriskHost = Configuration["Asterisk-Connection:server"]; // IP адрес Asterisk сервера
            int asteriskPort = Convert.ToInt32(Configuration["Asterisk-Connection:port"]); // Порт AMI (по умолчанию 5038)
            var asteriskUsername = Configuration["Asterisk-Connection:user"]; // Логин AMI пользователя
            var asteriskPassword = Configuration["Asterisk-Connection:password"]; // Пароль AMI пользователя

            // Данные для звонка
            var fromExtension = Configuration["SIP-settings:channel"]; // Внутренний номер вызывающего абонента
            string toNumber = RemoveUnwantedCharacters(phone); // Номер, на который звоним
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

                // Запускаем вызов асинхронно, чтобы не блокировать UI
                ManagerResponse originateResponse = await Task.Run(() => manager.SendAction(originate, originate.Timeout));

                // Проверяем ответ от Asterisk
                if (originateResponse.Response != "Success")
                {
                    MessageBox.Show($"Ошибка при инициации звонка: {originateResponse.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Отключаемся от Asterisk
                manager.Logoff();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициации звонка: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // REMOVE UNWANTED CHARACTERS FROM PHONE NUMBER
        static string RemoveUnwantedCharacters(string input)
        {
            // Удаляем пробелы, скобки и дефисы
            return input.Replace(" ", "")
                        .Replace("tel:", "")
                        .Replace("%20", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("+7", "8")
                        .Replace("-", "");
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
    }
}
