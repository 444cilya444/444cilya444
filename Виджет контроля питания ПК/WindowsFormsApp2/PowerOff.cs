using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using PowerOff.Properties;


namespace PowerOff
{
    public partial class PowerOff : Form
    {
        double totalRam;
        double busyRam;
        double USD;

        const string name = "PowreOff";
        reboot r = new reboot();
        const int DISTANCE = 100;
        Point lastPoint;
        bool Reset = true; //востановление формы по последнему параметру()
        List<CurrencyRate> tmp = CurrencyRates.GetExchangeRates();
        public PowerOff()
        {
            InitializeComponent();
            Thread thread1 = new Thread(CPU);
            thread1.IsBackground = true;
            thread1.Start();
            var rect = new Rectangle(1, 1, trackBar1.Width - 2, trackBar1.Height - 2);
            trackBar1.Region = new Region(rect);
            trackBar1.Value = 128;
            new Gamma().Value = 128;
            trackBar1.Value = 128;
            label1.Text = "";
            timer1.Enabled = true;
            timer1.Interval = 1000;
            this.MinimizeBox = false;
            notifyIcon1.Visible = true;
            this.SendToBack();

            USD = tmp.FindLast(s => s.CurrencyStringCode == "USD").ExchangeRate;
            label4.Text = "USD " + Math.Round(USD, 1) + "₽";

            Point pt = Screen.PrimaryScreen.WorkingArea.Location;  //Перенос в нижний правый угол экрана без панели задач         
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            if (Reset)
                this.RestoreWindowPosition(); //востановление формы по последнему параметру
            HideFromAltTab(this.Handle);//Скрытие из таксбара
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0046 /* WM_WINDOWPOSCHANGING */)
            {
                Rectangle workArea = SystemInformation.WorkingArea;
                Rectangle rect = (Rectangle)Marshal.PtrToStructure((IntPtr)(IntPtr.Size * 2 + m.LParam.ToInt64()), typeof(Rectangle));

                if (rect.X <= workArea.Left + DISTANCE)
                    Marshal.WriteInt32(m.LParam, IntPtr.Size * 2, workArea.Left);

                if (rect.X + rect.Width >= workArea.Width - DISTANCE)
                    Marshal.WriteInt32(m.LParam, IntPtr.Size * 2, workArea.Right - rect.Width);

                if (rect.Y <= workArea.Top + DISTANCE)
                    Marshal.WriteInt32(m.LParam, IntPtr.Size * 2 + 4, workArea.Top);

                if (rect.Y + rect.Height >= workArea.Height - DISTANCE)
                    Marshal.WriteInt32(m.LParam, IntPtr.Size * 2 + 4, workArea.Bottom - rect.Height);
            }
            base.WndProc(ref m);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveWindowPosition();
            notifyIcon1.Visible = false;
        }
        private void RestoreWindowPosition()
        {
            if (Settings.Default.HasSetDefaults)
            {
                this.WindowState = Settings.Default.WindowState;
                this.Location = Settings.Default.Location;
                this.Size = Settings.Default.Size;
            }
        }

        private void SaveWindowPosition()
        {
            Settings.Default.WindowState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.Location = this.Location;
                Settings.Default.Size = this.Size;
            }
            else
            {
                Settings.Default.Location = this.RestoreBounds.Location;
                Settings.Default.Size = this.RestoreBounds.Size;
            }
            Settings.Default.HasSetDefaults = true;
            Settings.Default.Save();
        }

        private void pictureBox2_Click(object sender, EventArgs e) => r.halt(false, true);

        private void pictureBox1_Click(object sender, EventArgs e) => r.halt(true, true);

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            bool isHibernate = Application.SetSuspendState(PowerState.Hibernate, false, false);
            if (isHibernate == false)
                MessageBox.Show("Не удалось перевести систему в спящий режим");
        }

        private void pictureBox4_Click(object sender, EventArgs e) => Application.Exit();
        //==========================================================================================================
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled();

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (DwmIsCompositionEnabled())
            {

                var margins = new MARGINS();
                margins.Top = 10000;
                margins.Left = 10000;
                DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }
        //==============================================================================

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public static void HideFromAltTab(IntPtr Handle)
        {
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle,
                GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToShortTimeString();
        }
        //Помещает форму в самые пучины ада
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();
        private void перезагрузитьПриложениеToolStripMenuItem_Click(object sender, EventArgs e) => Application.Restart();
        private void Form1_Shown(object sender, EventArgs e) => this.SendToBack();
        private void Form1_Activated(object sender, EventArgs e) => this.SendToBack();
        private void Form1_Enter(object sender, EventArgs e) => this.SendToBack();
        private void Form1_Validating(object sender, System.ComponentModel.CancelEventArgs e) => this.SendToBack();
        private void Form1_MouseDown(object sender, MouseEventArgs e) => lastPoint = new Point(e.X, e.Y);


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        //Автозагрузка
        private void поставитьНаАвтозагрузкуToolStripMenuItem_Click(object sender, EventArgs e) => SetAutorunValue(true);
        public bool SetAutorunValue(bool autorun)
        {
            string ExePath = Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void PowerOff_MouseUp(object sender, MouseEventArgs e)
        {
            this.SaveWindowPosition();
        }
        //Изменение яркости


        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            trackBar1.Value = 128;
            new Gamma().Value = 128;
        }
        protected PerformanceCounter ramCounter;
        ManagementObjectSearcher ramMonitor =    //запрос к WMI для получения памяти ПК
           new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");
        public void CPU()//ЦП метод
        {
            while (true)
            {
                try
                {
                    ManagementObjectSearcher man = new ManagementObjectSearcher("SELECT LoadPercentage FROM Win32_Processor");
                    foreach (ManagementObject obj in man.Get())
                        label2.Invoke(new MethodInvoker(delegate { label2.Text = ("ЦП: " + obj["LoadPercentage"] + "%"); }));
                    foreach (ManagementObject objram in ramMonitor.Get())
                    {
                        totalRam = Convert.ToUInt64(objram["TotalVisibleMemorySize"]);    //общая память ОЗУ
                        busyRam = totalRam - Convert.ToUInt64(objram["FreePhysicalMemory"]);//занятная память = (total-free)
                        busyRam /= 1048576;
                        totalRam /= 1048576;
                        label3.Invoke(new MethodInvoker(delegate { label3.Text = "ОЗУ: " + Math.Round(busyRam, 1) + "/" + Math.Round(totalRam, 1); }));//вычисляем проценты занятой памяти
                    }
                    Thread.Sleep(500);
                }
                catch (Exception)
                {
                    Application.Restart();
                }             
            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            new Gamma().Value = trackBar1.Value;
        }

    }
}

