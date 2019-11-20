using System;
using System.Windows.Forms;
using Учет_цистерн.Forms;

namespace Учет_цистерн
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
            #region Splash
            Application.Run(new SplashForm());
            Application.Run(new LoginForm());
            #endregion
        }
    }
}
