using System;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        reboot r = new reboot();
        private void button1_Click(object sender, EventArgs e)
        {
            r.halt(false, true);//Класс основанный на API винды
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 128;
            new Gamma().Value = 128;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            new Gamma().Value = trackBar1.Value;//Класс основанный на API драйвера системы. Меняет контрастность и 
        }
        public struct stock
        {
            public string name { get; set; }
            public string driveFormat { get; set; }
            public string driveType { get; set; }
            public string availableFreeSpace { get; set; }
            public string isReady { get; set; }
            public string rootDirectory { get; set; }
            public string totalFreeSpace { get; set; }
            public string totalSize { get; set; }
            public string volumeLabel { get; set; }

        };
        List<stock> dRive = new List<stock>();
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(CPU);
            thread1.IsBackground = true;
            thread1.Start();
        
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    dRive.Add(new stock()
                    {
                        name = ("Имя диска: " + drive.Name),
                        driveFormat = ("Файловая система: " + drive.DriveFormat),
                        driveType = ("Тип диска: " + drive.DriveType),
                        availableFreeSpace = ("Объем доступного свободного места (в байтах): " + drive.AvailableFreeSpace),
                        isReady = ("Готов ли диск: " + drive.IsReady),
                        rootDirectory = ("Корневой каталог диска: " + drive.RootDirectory),
                        totalFreeSpace = ("Общий объем свободного места, доступного на диске (в байтах): " + drive.TotalFreeSpace),
                        totalSize = ("Размер диска (в байтах): " + drive.TotalSize),
                        volumeLabel = ("Метка тома диска: " + drive.VolumeLabel)
                    });
                    
                }
                catch { }

                Console.WriteLine();
            }
            upDrive();
        }
        int ind = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            upDrive();
        }
        void upDrive()
        {
            if (ind >= dRive.Count)
                ind = 0;
            label3.Text = ("Имя диска: " + dRive[ind].name);
            label4.Text = ("Файловая система: " + dRive[ind].driveFormat);
            label5.Text = ("Тип диска: " + dRive[ind].driveType);
            label6.Text = ("Объем доступного свободного места (в байтах): " + dRive[ind].availableFreeSpace);
            label7.Text = ("Готов ли диск: " + dRive[ind].isReady);
            label8.Text = ("Корневой каталог диска: " + dRive[ind].rootDirectory);
            label9.Text = ("Общий объем свободного места, доступного на диске (в байтах): " + dRive[ind].totalFreeSpace);
            label10.Text = ("Размер диска (в байтах): " + dRive[ind].totalSize);
            label11.Text = ("Метка тома диска: " + dRive[ind].volumeLabel);
            ind++;
        }
        protected PerformanceCounter ramCounter;
        ManagementObjectSearcher ramMonitor =    //запрос к WMI для получения памяти ПК
           new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");
        double totalRam;
        double busyRam;
        public void CPU()//ЦП метод парсит системные таблицы виндовс для извлечения данных состояния процессора и озу
        {
            while (true)
            {
                try
                {
                    ManagementObjectSearcher man = new ManagementObjectSearcher("SELECT LoadPercentage FROM Win32_Processor");
                    foreach (ManagementObject obj in man.Get())
                        label1.Invoke(new MethodInvoker(delegate { label1.Text = ("ЦП: " + obj["LoadPercentage"] + "%"); }));
                    foreach (ManagementObject objram in ramMonitor.Get())
                    {
                        totalRam = Convert.ToUInt64(objram["TotalVisibleMemorySize"]);    //общая память ОЗУ
                        busyRam = totalRam - Convert.ToUInt64(objram["FreePhysicalMemory"]);//занятная память = (total-free)
                        busyRam /= 1048576;
                        totalRam /= 1048576;
                        label2.Invoke(new MethodInvoker(delegate { label2.Text = "ОЗУ: " + Math.Round(busyRam, 1) + "/" + Math.Round(totalRam, 1); }));//вычисляем проценты занятой памяти
                    }
                    Thread.Sleep(500);
                }
                catch (Exception)
                {
                    Application.Restart();
                }
            }
        }
    }
}
