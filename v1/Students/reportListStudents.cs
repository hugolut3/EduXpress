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

namespace EduXpress.Students
{
    public partial class reportListStudents : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportListStudents).Assembly);
        public reportListStudents()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Set Header details
            string currentDate = DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern, CultureInfo.CurrentCulture);
            //       TextInfo myTI = CultureInfo.CurrentCulture.TextInfo;
            //       xrLblDate.Text = $"{LocRM.GetString("strDate")}: {myTI.ToTitleCase(currentDate)}"; // Display cureent date with Title case
            
            if (studentSearchBy==1)  //Search list by class
            {
                xrlblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear;
                xrLblSection.Text = LocRM.GetString("strCycle").ToUpper() + ": " + section.ToUpper();
                xrlblClass.Text = LocRM.GetString("strEnrolmentList").ToUpper() + " " + LocRM.GetString("strClass").ToUpper() + ": " + className.ToUpper();
                xrLblSection.Visible = true;
                xrlblClass.Visible = true;
            }
            else if (studentSearchBy == 2) //Search list by option
            {
                xrlblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear;
                xrLblSection.Text = LocRM.GetString("strEnrolmentList").ToUpper() + " " + LocRM.GetString("strSection").ToUpper() + ": " + section.ToUpper();
                xrLblSection.Visible = true;
                xrlblClass.Visible=false;                
            }
            else if (studentSearchBy == 3)  //Search list by section
            {
                xrlblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear;
                xrLblSection.Text = LocRM.GetString("strEnrolmentList").ToUpper() + " " + LocRM.GetString("strCycle").ToUpper() + ": " + section.ToUpper();
                xrLblSection.Visible = true;
                xrlblClass.Visible = false;
            }
            else if (studentSearchBy == 4)  //Search list by surname
            {
                xrlblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear;
                xrLblSection.Text = LocRM.GetString("strEnrolmentListBySurname").ToUpper() + ": " + studentSurName.ToUpper();
                xrLblSection.Visible = true;
                xrlblClass.Visible = false;
            }
            else if (studentSearchBy == 5)  //Search list by name
            {
                xrlblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear;
                xrLblSection.Text = LocRM.GetString("strEnrolmentListByName").ToUpper() + ": " + studentName.ToUpper();
                xrLblSection.Visible = true;
                xrlblClass.Visible = false;
            }

            else if (studentSearchBy == 6)  //Search list by enrolment date
            {
                xrlblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear;
                xrLblSection.Text = LocRM.GetString("strEnrolmentList").ToUpper() + " " + LocRM.GetString("strFrom").ToUpper()+ " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " + searchbyDateTo; 
                xrLblSection.Visible = true;
                xrlblClass.Visible = false;
            }
            else if (studentSearchBy == 7)  //Search list by school year
            {
                xrlblSchoolYear.Text = LocRM.GetString("strEnrolmentListAllStudents").ToUpper() + ": " + SchoolYear;
               // xrlblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
               // xrLblSection.Text = LocRM.GetString("strEnrolmentList").ToUpper() + " " + LocRM.GetString("strFrom").ToUpper() + " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " + searchbyDateTo;
                xrLblSection.Visible = false;
                xrlblClass.Visible = false;
            }


            //Display School info            
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select * from CompanyProfile ";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    xrLblSchoolName.Text = pf.Decrypt(rdr.GetString(1)).ToUpper(); ;
                    xrLblSchoolMoto.Text = rdr.GetString(2).ToUpper(); ;
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
