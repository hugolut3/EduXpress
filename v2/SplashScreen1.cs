using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace EduXpress
{
    public partial class SplashScreen1 : SplashScreen
    {
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(SplashScreen1).Assembly);
        int dotCount = 0;
        public SplashScreen1()
        {
            InitializeComponent();
            DevExpress.Utils.ImageCollection imageCol = this.imageCollection1;
            if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
            {
                SplashImageOptions.Image = imageCol.Images[1];// ("EduXpress_Splash_fr.png");
            }
            else
            {
                SplashImageOptions.Image = imageCol.Images[0];// ("EduXpress_Splash.png");
            }                
           
            Timer tmr = new Timer();
            tmr.Interval = 400;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Start();
        }
        void tmr_Tick(object sender, EventArgs e)
        {
            if (++dotCount > 3) dotCount = 0;
            labelControl2.Text = string.Format("{1}{0}", GetDots(dotCount), LocRM.GetString("strStarting"));  
        }
        string GetDots(int count)
        {
            string ret = string.Empty;
            for (int i = 0; i < count; i++) ret += ".";
            return ret;
        }
        

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void SplashScreen1_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(524, 320);
            // ----- Update the version number.
            Assembly currentAssembly;
            Version versionInfo;

            // ----- Show the version details.
            currentAssembly = Assembly.GetEntryAssembly();
            if (currentAssembly == null)
                currentAssembly = Assembly.GetCallingAssembly();
            versionInfo = currentAssembly.GetName().Version;
            lblProgramVersion.Text = string.Format("Version {0}.{1}.{2}",
                versionInfo.Major, versionInfo.Minor, versionInfo.Revision);          


            //1.x.y.z
            //1-> Major, x-> Minor, y -> Build (incremented for each build), z -> Revision. In setup use major,minor and revision.

            lblCopyright.Text = "© 2019 - " + DateTime.Now.Year.ToString() + " " + lblCopyright.Text;
            
            // ----- Show the copyright information.
            //object[] attrSet = currentAssembly.GetCustomAttributes(
            //    typeof(AssemblyCopyrightAttribute), inherit: true);
            //if (attrSet.Length != 0)
            //    lblCopyright.Text =
            //        ((AssemblyCopyrightAttribute)attrSet[0]).Copyright;
        }
        
    }
    }
