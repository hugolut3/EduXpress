using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Data.SqlClient;
using EduXpress.Functions;
using System.IO;
using System.Data;
using static EduXpress.Functions.PublicVariables;

namespace EduXpress.Reports
{
    public partial class reportFeeDue : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportFeeDue).Assembly);
        
        public reportFeeDue()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Set Header details
            xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear")+": "+  PublicVariables.SchoolYear;
            xrLblSchoolClass.Text = LocRM.GetString("strClass") + ": " + PublicVariables.SchoolClass;
            xrLblFeePeriod.Text = LocRM.GetString("strPeriod") + ": " + PublicVariables.FeePeriod;
            if(PublicVariables.FeesDueFull == true)
            {
                xrLblFeesDue.Text = LocRM.GetString("strListStudentsFeesDueFull");
            }
            else
            {
                xrLblFeesDue.Text = LocRM.GetString("strListStudentsFeesDuePartially");
            }

            //Set Footer  
            TextInfo myTI = CultureInfo.CurrentCulture.TextInfo; //Set Sentence case
            string currentdateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
            xrLblFooterText.Text = $"{LocRM.GetString("strReportGeneratedDateTime")}: {myTI.ToTitleCase(currentdateTime)}. {LocRM.GetString("strGeneratedBy")}: {UserLoggedSurname.ToUpper()} {UserLoggedName}.";

            //Display School info            
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select * from CompanyProfile ";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    xrLblSchoolName.Text = pf.Decrypt(rdr.GetString(1));
                    if (!Convert.IsDBNull(rdr["SchoolLogo"]))
                    {
                        byte[] x = (byte[])rdr["SchoolLogo"];
                        MemoryStream ms = new MemoryStream(x);

                        xrPictureSchoolLogo.Image = Image.FromStream(ms);
                        Image image = xrPictureSchoolLogo.ImageSource.Image;                        
                    }
                    else
                    {
                        xrPictureSchoolLogo.Image = null;
                    }
                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            
        }
    }
}
