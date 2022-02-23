using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using static EduXpress.Functions.PublicVariables;
using EduXpress.Functions;
using System.Globalization;
using System.Data.SqlClient;
using System.Resources;
using System.IO;
using System.Data;

namespace EduXpress.Education
{
    public partial class reportSupplementaryExam : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportSupplementaryExam).Assembly);
        public reportSupplementaryExam()
        {
            InitializeComponent();
        }

        private void reportSupplementaryExam_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();           

            xrLblTeacher.Text = LocRM.GetString("strTeacher").ToUpper() + ": " + UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper();
            xrLblSubject.Text = LocRM.GetString("strSubjectOne").ToUpper() + ": " + subjectGlobalVariable.ToUpper();
            xrLblAssessmentPeriodTitle.Text = LocRM.GetString("strReportSupplementaryExam").ToUpper();
            xrLblClass.Text = LocRM.GetString("strClass").ToUpper() + ": " + className.ToUpper();            

            //Set Footer  
            TextInfo myTI = CultureInfo.CurrentCulture.TextInfo; //Set Sentence case
            string currentdateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
            // xrLblFooterText.Text = $"{LocRM.GetString("strReportGeneratedDateTime")}: {myTI.ToTitleCase(currentdateTime)}. {LocRM.GetString("strGeneratedBy")}: {UserLoggedSurname.ToUpper()} {UserLoggedName}.";

            //Display School info            
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select SchoolName,AddressTown,AddressCommune, SchoolLogo from CompanyProfile ";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    xrLblSchoolName.Text = pf.Decrypt(rdr.GetString(0));
                    string townCommune = "";
                    if (!Convert.IsDBNull(rdr["AddressTown"]))  //check if not null, columns added later
                    {
                        townCommune = pf.Decrypt(rdr.GetString(1));
                        if (townCommune == "")
                        {
                            if (!Convert.IsDBNull(rdr["AddressCommune"]))
                            {
                                townCommune = rdr.GetString(2);
                            }
                        }
                    }
                    else if (!Convert.IsDBNull(rdr["AddressCommune"]))
                    {
                        townCommune = rdr.GetString(2);
                    }
                    if (townCommune != "")
                    {
                        xrLblFooterText.Text = LocRM.GetString("strDoneAt") + " " + townCommune + ", " + LocRM.GetString("strThe") + " " + myTI.ToTitleCase(currentdateTime);
                    }
                    else
                    {
                        xrLblFooterText.Text = LocRM.GetString("strDate") + " " + myTI.ToTitleCase(currentdateTime);
                    }
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
