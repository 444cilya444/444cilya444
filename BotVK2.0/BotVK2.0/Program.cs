using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace BotVK2._0
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
      
        static void Main()
        {          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(new Form1());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Thread.Sleep(3000);
            GPUup gpu = new GPUup();
            gpu.ErrFaile(e.Exception.ToString());
            Process.Start(Assembly.GetEntryAssembly().Location);
            Process.GetCurrentProcess().Kill();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Thread.Sleep(3000);
            GPUup gpu = new GPUup();
            gpu.ErrFaile(e.ExceptionObject.ToString());
            Process.Start(Assembly.GetEntryAssembly().Location);
            Process.GetCurrentProcess().Kill();
        }       
    }
}
