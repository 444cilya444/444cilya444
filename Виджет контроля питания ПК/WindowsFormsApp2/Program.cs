using System;
using System.Threading;
using System.Windows.Forms;

namespace PowerOff
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            new Mutex(false, " ", out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Программа запущена!");
                return; 
            }                       
               
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PowerOff());
        }
    }
}
