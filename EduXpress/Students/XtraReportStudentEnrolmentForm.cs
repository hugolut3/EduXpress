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

namespace EduXpress.Students
{
    public partial class XtraReportStudentEnrolmentForm : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(XtraReportStudentEnrolmentForm).Assembly);
        public XtraReportStudentEnrolmentForm()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Set Header details
            string currentDate = DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern, CultureInfo.CurrentCulture);
            TextInfo myTI = CultureInfo.CurrentCulture.TextInfo; 
            xrLblDate.Text = $"{LocRM.GetString("strDate")}: {myTI.ToTitleCase( currentDate)}"; // Display cureent date with Title case
            xrLblEnrolmentForm.Text = LocRM.GetString("strStudentEnrolmentForm");
            //xrLabelRequiredField.Text = LocRM.GetString("strRequiredFields"); 

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
                    xrLblSchoolMoto.Text = rdr.GetString(2);
                    xrLblTelephone.Text =  $"{LocRM.GetString("strTelephone")}: {rdr.GetString(3)}";
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
