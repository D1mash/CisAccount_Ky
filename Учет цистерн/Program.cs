using System;
using System.Globalization;
using System.Threading;
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

            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");

            // The following line provides localization for the application's user interface.  
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats.  
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application.  
            // Note: The following properties are supported in the .NET Framework 4.5+ 
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            #region Splash
            Application.Run(new SplashForm());
            Application.Run(new LoginForm());
            #endregion
        }
    }
}
