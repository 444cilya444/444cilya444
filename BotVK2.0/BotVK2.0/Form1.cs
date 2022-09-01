using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace BotVK2._0
{
    public partial class Form1 : Form
    {


        reboot r = new reboot();
        VkApi vk = new VkApi();
        string grupID = "204138183", token = "9b644d3603a5d70fa18c5115c14de68d4fef75a4c16650b53b5d049cd8e22796f2daac193f3d234eafa87"; //Валера
        dynamic resphonseLongPool;
        string json = string.Empty;
        string url;
        const string name = "BotVK2.1";
        long elapsedTime;
        string ConnectTaime;
        Stopwatch stopWatch = new Stopwatch();
        bool MessageEvent = true, flag = true, botConnect = true;
        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Visible = false;
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
            Resize += new EventHandler(this.Form1_Resize);
            SetAutorunValue(true);
            stopWatch.Start();
            Auth();
        }

        void Potok(bool flagClos = true)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(LongPool_Tick));
            if (flagClos)
            {
                thread.Start();
            }
            else
            {
                ConnectTaime = DateTime.Now.ToString("dd.MM hh:mm");
                thread.Abort();
                Auth();
            }
        }

        void DebugMesseg(bool flag)
        {
            if (flag)
            {
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = 370784587,
                    RandomId = new Random().Next(),
                    Message = "Начало работы"
                });
            }
            else
            {
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = 370784587,
                    RandomId = new Random().Next(),
                    Message = $"Подключение востоновлено\nОтсутсвие сети {ConnectTaime} до {DateTime.Now:dd.MM hh:mm}"
                });
            }
        }
        public void Auth()
        {
            try
            {
                vk.Authorize(new ApiAuthParams()
                {
                    ApplicationId = 7832996,
                    AccessToken = token,
                    Settings = Settings.All
                });
                var param = new VkParameters
                 {
                     { "group_id", grupID },
                     { "access_token", token }
                 };
                try
                { resphonseLongPool = JObject.Parse(vk.Call("groups.getLongPollServer", param).RawJson); }
                catch (Exception)
                {
                    if (botConnect)
                    {
                        MessageEvent = false;
                        AuthReset();
                    }
                    else
                    {
                        MessageEvent = false;
                        AuthReset(false);
                    }
                }
                if (vk.IsAuthorized)
                {
                    Potok();
                    if (MessageEvent)
                    {
                        if (flag)
                            DebugMesseg(true);
                        else
                            DebugMesseg(false);
                        MessageEvent = false;
                    }
                }
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                Auth();
            }
        }
        void AuthReset(bool flag = true)
        {
            MessageEvent = true;
            if (flag)
            {
                //Федя
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = 370784587,
                    RandomId = new Random().Next(),
                    Message = "Переключаюсь на Федю"
                });
                grupID = "205415406";
                token = "2f43538e37fe84e249228720e5cbc7a1dd87a1b00c613e28489557b72219a7407cccf2d546b8b1a06288d";
            }
            else
            {
                //Валера
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = 370784587,
                    RandomId = new Random().Next(),
                    Message = "Переключаюсь на Валеру"
                });
                grupID = "204138183";
                token = "9b644d3603a5d70fa18c5115c14de68d4fef75a4c16650b53b5d049cd8e22796f2daac193f3d234eafa87";
            }
            Thread.Sleep(1000);
            Auth();
        }

        private void LongPool_Tick(object theard)
        {
            flag = true;
            while (flag)
            {
                var param = new VkParameters
                {
                    { "group_id", grupID },
                    { "access_token", token }
                };
                resphonseLongPool = JObject.Parse(vk.Call("groups.getLongPollServer", param).RawJson);

                var webClient = new WebClient() { Encoding = Encoding.UTF8 };
                var server = resphonseLongPool?.response?.server?.ToString();
                var key = resphonseLongPool?.response?.key?.ToString();
                var ts = json != string.Empty ? JObject.Parse(json)["ts"]?.ToString() : resphonseLongPool?.response?.ts?.ToString();
                if (server == null || key == null || ts == null)
                {
                    MessageEvent = true;
                    flag = false;
                    Potok(false);
                }
                url = string.Format("{0}?act=a_check&key={1}&ts={2}&wait=25", server, key, ts);
                try
                {
                    json = webClient.DownloadString(url);
                    var col = JObject.Parse(json)["updates"].ToList();
                    foreach (var item in col)
                    {
                        if (item["type"].ToString() == "message_new")
                        {
                            string msg = item["object"]["message"]["text"].ToString();
                            string ThisUserID = item["object"]["message"]["from_id"].ToString();
                            NewMessage(msg.ToLower(), ThisUserID);
                        }
                    }
                }
                catch (Exception)
                {                  
                    MessageEvent = true;
                    flag = false;
                    Potok(false);
                }
                Thread.Sleep(2000);
            }
        }
        public void NewMessage(string msg, string ThisUserID)
        {
            elapsedTime = stopWatch.ElapsedMilliseconds;
            TimeSpan interval = TimeSpan.FromMilliseconds(elapsedTime);
            string time = interval.Days.ToString() + " дней " + interval.Hours.ToString() + " часов " + interval.Minutes.ToString() + " минут ";

            if (msg == "статус" || msg == "привет")
            {
                GPUup up = new GPUup();
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = long.Parse(ThisUserID),
                    RandomId = new Random().Next(),
                    Message = up.gpu() + "\nЯ проработал: \n" + time.Trim()
                });
            }
            else if (msg == "пер" || msg == "перезагрузка")
            {
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = long.Parse(ThisUserID),
                    RandomId = new Random().Next(),
                    Message = "Перезагружаюсь! Я проработал: \n" + time.Trim()
                });
                r.halt(true, false);
            }
            else if (msg == "выход" || msg == "завершение работы")
            {
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = long.Parse(ThisUserID),
                    RandomId = new Random().Next(),
                    Message = "Завершение работы! Я проработал: \n" + time.Trim()
                });
                r.halt(false, true);
            }
            else if (msg == "федя" || msg == "валера")
            {
                if (msg == "федя")
                {                 
                    AuthReset();                                  
                }
                if (msg == "валера")
                {
                    AuthReset(false);                                    
                }
            }
            else if (msg == "зп" || msg == "бот")
            {
                Process.Start(Assembly.GetEntryAssembly().Location);
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                vk.Messages.SendAsync(new MessagesSendParams
                {
                    UserId = long.Parse(ThisUserID),
                    RandomId = new Random().Next(),
                    Message = "Неизвестная команда!\n" +
                    "Список доступных команд:\n" +
                    "(Статус,Привет) - Состояние ПК,\n" +
                    "(Пер,Перезагрузка) - Перезагрузка ПК,\n" +
                    "(Выход,Завершение работы) - Завершение работы ПК.\n"+
                    "(Федя,Валера) - Переключение ботов.\n"+
                    "(Бот,зп) - Перезагрузка бота.\n"
                });
            }
        }
        /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/
        /*Логика бота*/
        /*=================================================================================================================*/
        /*Визуализация формы*/
        /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
        public bool SetAutorunValue(bool autorun)
        {
            string ExePath = System.Windows.Forms.Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                {
                    reg.SetValue(name, ExePath);
                }
                else
                {
                    reg.DeleteValue(name);
                }

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

