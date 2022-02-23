using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Threading;
using System.Globalization;
using static EduXpress.Functions.PublicFunctions;
using DevExpress.XtraEditors;

namespace EduXpress
{
    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            //reset user settings
            //Properties.Settings.Default.Reset();
            //log error message
            if (Properties.Settings.Default.LogErrors)
            {
                AppDomain.CurrentDomain.FirstChanceException += (sender, e) => {
                    System.Text.StringBuilder msg = new System.Text.StringBuilder();
                    msg.AppendLine(DateTime.Now + ": " + e.Exception.GetType().FullName);
                    msg.AppendLine(e.Exception.Message);
                    System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                    msg.AppendLine(st.ToString());
                    msg.AppendLine();
                    String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    // save error log file in  LocalApplicationData: C:\Users\[User]\AppData\Roaming
                    // string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string logFilePath = String.Format("{0}\\{1}", desktopPath, "EduXpress_log.txt");
                    System.IO.File.AppendAllText(logFilePath, msg.ToString());
                };
            }
                

            //An application restart was requested, wait for 3 seconds to make sure the application has completely closed 
            //before starting a new instance of the application
           if (Properties.Settings.Default.IsRestarting)
            {
                Properties.Settings.Default.IsRestarting = false;
                Properties.Settings.Default.Save();
                Thread.Sleep(3000);
            }
            //Restrict the Application to run Just One Instance
            Mutex eduXpressSync;
            // ----- Perhaps another copy is already running. Use a mutex shared among
            //       all copies of the app to see if another copy exists.
            eduXpressSync = new Mutex(true, "EduXpress.Management.CoreApp.Token"); //Token for running applicatiion: "EduXpress.Management.CoreApp.Token"
            if ((eduXpressSync == null) || (eduXpressSync.WaitOne(TimeSpan.Zero, true) == false))
            {
                // ----- Send a message to the other copy.
               Functions.NativeMethods.PostMessage((IntPtr)Functions.NativeMethods.HWND_BROADCAST,
                    Functions.NativeMethods.WM_EduXpressNotify, IntPtr.Zero, IntPtr.Zero);
                Application.Exit();   // Someone else has the token.
                return;
            }

            //check if preferences exist then load from database to application settings. Used string connection lenght of >10 randomly as will be greater than 10
            if((Properties.Settings.Default.PreferencesExist==false) &&((Properties.Settings.Default.DatabaseConnection +"").Trim().Length>10 ))
            {
                Preferences();
            }

                // LanguageAuto
                CultureInfo culture = null;
           if ( Properties.Settings.Default.LanguageAuto == true)
           {
               // Determines the specific Parent culture fr
               if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "fr")
               {
                    culture = CultureInfo.CreateSpecificCulture("fr-FR");
               }                    
                else
               {
                   culture = CultureInfo.CreateSpecificCulture("en-US");
               }
                   
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
       
               if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
               {
                    Thread.CurrentThread.CurrentCulture.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "," };
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };
                   // Thread.CurrentThread.CurrentCulture.DateTimeFormat = new DateTimeFormatInfo();
                }
            }
            //change language
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Properties.Settings.Default.Language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
                if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
                {
                    Thread.CurrentThread.CurrentCulture.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "," };
                   // Thread.CurrentThread.CurrentCulture.nu
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };
                }
            }


            // ----- Perform Windows-related initialization.
            WindowsFormsSettings.ForceDirectXPaint(); //Enable DirectX rendering
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //enable and select skins 
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.ApplicationSkin);

            // ----- Add the global event handler for uncaught errors.
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalErrorHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // ----- Perform general initialization.
            InitializeSystem();

            //Start application
            
            
            Application.Run(new Form1());

            // ----- Let another copy run later.
            eduXpressSync.ReleaseMutex();

        }
        private static void GlobalErrorHandler(object sender, ThreadExceptionEventArgs e)
        {
            // ----- Some unhandled error occurred. Just report the error and keep
            //       the program running.
            GeneralError("EduXpress.UnhandledError", e.Exception);
        }
    }
}
