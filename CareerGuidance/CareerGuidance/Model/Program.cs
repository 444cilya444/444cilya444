using CareerGuidance.Forms;
using System;
using System.Windows.Forms;

namespace CareerGuidance
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
            Application.Run(new Aut());
        }
    }
}
