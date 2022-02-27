using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Globalization;
using static EduXpress.Functions.PublicFunctions;

namespace EduXpress.UI
{
    public partial class frmAbout : DevExpress.XtraEditors.XtraForm
    {
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmAbout).Assembly);
        public frmAbout()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // ----- Fade the form out.
            for (int counter = 90; counter >= 10; counter -= 10)
            {
                this.Opacity = counter / 50.0;
                this.Refresh();
                System.Threading.Thread.Sleep(100);
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            // ----- Show the company web page.
            Process.Start("http://www.bindu.co.za/");
        }

        private void hyperlinkLabelControl2_Click(object sender, EventArgs e)
        {
            // ----- Send email to the company.
            Process.Start("mailto:info@bindu.co.za");
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            //load logo image
            if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
            {
                pictureEditLogo.Image = Properties.Resources.EduXpress_About_fr;
            }
            else
            {
                pictureEditLogo.Image = Properties.Resources.EduXpress_About_en;
            }

                // ----- Prepare the form.
                LicenseFileDetail licenseDetails;
            Assembly currentAssembly;
            Version versionInfo;

            // ----- Display the licensee.
            licenseDetails = ExamineLicense();
            if (licenseDetails.Status == LicenseStatus.ValidLicense)
            {
                lblSerialNo.Visible = true;
                lblSerialNoValue.Visible = true;
                lblVersionType.Visible = true;
                lblExpiryDate.Visible = true;
                lblLicenseName.Text = licenseDetails.Licensee;
                lblSerialNoValue.Text= licenseDetails.SerialNumber;  
                lblExpiryDate.Text = LocRM.GetString("strExpiryDate") + ": " + (licenseDetails.ExpireDate).ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern,CultureInfo.CurrentCulture);
                string LicenseFeature = "";
                LicenseFeature = licenseDetails.Feature;
                if (LicenseFeature=="0".Trim())
                {
                    lblVersionType.Text=LocRM.GetString("strTrialVersion"); //3 months Trial version with all current modules
                }
                if (LicenseFeature == "1".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strExpressVersion");
                }
                if (LicenseFeature == "2".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strStandardVersion"); //Module de base only
                }
                if (LicenseFeature == "3".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strProfessionalVersion");//module de base plus suplimentaire
                }
                if (LicenseFeature == "4".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strLiteVersion");//only enrolment, office and communication (email and sms)
                }
                if (LicenseFeature == "5".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strUltimateVersion");//all available modules
                }
            }
            else if (licenseDetails.Status == LicenseStatus.LicenseExpired)
            {
                lblSerialNo.Visible = true;
                lblSerialNoValue.Visible = true;
                lblVersionType.Visible = true;
                lblExpiryDate.Visible = true;
                lblLicenseName.Text = licenseDetails.Licensee;
                lblSerialNoValue.Text = licenseDetails.SerialNumber;
                lblExpiryDate.Text = LocRM.GetString("strExpiryDate")+ ": "+(licenseDetails.ExpireDate).ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
                string LicenseFeature = "";
                LicenseFeature = licenseDetails.Feature;
                if (LicenseFeature == "0".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strExpired") + ": "+ LocRM.GetString("strTrialVersion"); //3 months Trial version with all current modules
                }
                if (LicenseFeature == "1".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strExpired") + ": " + LocRM.GetString("strExpressVersion");
                }
                if (LicenseFeature == "2".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strExpired") + ": " + LocRM.GetString("strStandardVersion"); //Module de base only
                }
                if (LicenseFeature == "3".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strExpired") + ": " + LocRM.GetString("strProfessionalVersion");//module de base plus suplimentaire
                }
                if (LicenseFeature == "4".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strExpired") + ": " + LocRM.GetString("strLiteVersion");//only enrolment, office and communication (email and sms)
                }
                if (LicenseFeature == "5".Trim())
                {
                    lblVersionType.Text = LocRM.GetString("strExpired") + ": " + LocRM.GetString("strUltimateVersion");//all available modules
                }
            }
            else
            {
                lblSerialNo.Visible = false;
                lblSerialNoValue.Visible = false;
                lblVersionType.Visible = false;
                lblExpiryDate.Visible = false;
                lblLicenseName.Text = LocRM.GetString("strUnlicensed");
            }

            // ----- Update the version number.
            currentAssembly = Assembly.GetEntryAssembly();
            if (currentAssembly == null)
                currentAssembly = Assembly.GetCallingAssembly();
            versionInfo = currentAssembly.GetName().Version;
            // lblVersionNo.Text = string.Format("Version {0}.{1} " +LocRM.GetString("strRevision")+ " {2}",
            lblVersionNo.Text = string.Format("Eduxpress v{0}.{1}.{2}.{3}",
                 versionInfo.Major, versionInfo.Minor, versionInfo.Build, versionInfo.Revision);
            // ----- Prepare the form for later fade-out.
            this.Opacity = 0.85;            
        }
    }
}