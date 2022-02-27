using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using static EduXpress.Functions.PublicVariables;
using System.Globalization;
using System.Resources;
using System.Data.SqlClient;
using EduXpress.Functions;
using System.IO;

namespace EduXpress.Reports
{
    public partial class reportFeePayment : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportFeePayment).Assembly);
        public reportFeePayment()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Set Header details
            string currentDate = DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern, CultureInfo.CurrentCulture);
           // TextInfo myTI = CultureInfo.CurrentCulture.TextInfo;
           // xrLblDate.Text = $"{LocRM.GetString("strDate")}: {myTI.ToTitleCase(currentDate)}"; // Display cureent date with Title case

            if (studentSearchBy == 1)  //Search All cashiers by Today
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strToday").ToUpper(); 
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper(); 
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
            }
            else if (studentSearchBy == 2)  //Search All cashiers by Yesterday
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strYesterday").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 3)  //Search All cashiers by this week
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strThisWeek").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 4)  //Search All cashiers by last week
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strLastWeek").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 5)  //Search All cashiers by this month
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strThisMonth").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 6)   //Search by a cashier Today
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strToday").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 7)   //Search by a cashier Yesterday
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strYesterday").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 8)   //Search by a cashier this week
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strThisWeek").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 9)   //Search by a cashier last week
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strLastWeek").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 10)   //Search by a cashier this month
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strThisMonth").ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 11)   //Search by All custom date
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strFrom").ToUpper()  + " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " +  searchbyDateTo;
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": "+ LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 12)   //Search by Cashier custom date
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strFrom").ToUpper() + " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " + searchbyDateTo;
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 13)   //Search by All class
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strClass").ToUpper() + ": " + className.ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 14)   //Search by Cashier class
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strClass").ToUpper() + ": " + className.ToUpper();
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 15)   //Search by All Cycle
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strCycle").ToUpper() + ": " + educationCycle.ToUpper() + ". " + LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strFrom").ToUpper() + " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " + searchbyDateTo;
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 16)   //Search by Cashier Cycle
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strCycle").ToUpper() + ": " + educationCycle.ToUpper() + ". " + LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strFrom").ToUpper() + " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " + searchbyDateTo;
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }

            else if (studentSearchBy == 17)   //Search by All Section
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strSection").ToUpper() + ": " + section.ToUpper() + ". " + LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strFrom").ToUpper() + " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " + searchbyDateTo;
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + LocRM.GetString("strAll").ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
            }
            else if (studentSearchBy == 18)   //Search by Cashier Section
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                xrLblPeriod.Text = LocRM.GetString("strSection").ToUpper() + ": " + section.ToUpper() + ". " + LocRM.GetString("strPeriod").ToUpper() + ": " + LocRM.GetString("strFrom").ToUpper() + " " + searchbyDateFrom + " " + LocRM.GetString("strTo").ToUpper() + " " + searchbyDateTo;
                xrLblCashier.Text = LocRM.GetString("strCashier").ToUpper() + ": " + searchbyCashier.ToUpper();
                xrLblPeriod.Visible = true;
                xrLblCashier.Visible = true;
                xrLblFeesPaid.Text = LocRM.GetString("strReportFeesPaid").ToUpper();
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
