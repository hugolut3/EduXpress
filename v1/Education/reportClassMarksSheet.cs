using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Resources;
using static EduXpress.Functions.PublicVariables;
using EduXpress.Functions;
using System.Globalization;
using System.IO;
using System.Data;


namespace EduXpress.Education
{
    public partial class reportClassMarksSheet : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportClassMarksSheet).Assembly);
        public reportClassMarksSheet()
        {
            InitializeComponent();
        }

        private void reportClassMarksSheet_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
            if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str1eP").ToUpper())
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeLa").ToUpper() + " " + LocRM.GetString("strAssessment1stPeriod").ToUpper();
            }
            else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str2eP").ToUpper())
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeLa").ToUpper() + " " + LocRM.GetString("strAssessment2ndPeriod").ToUpper();
            }
            else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str3eP").ToUpper())
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeLa").ToUpper() + " " + LocRM.GetString("strAssessment3rdPeriod").ToUpper();
            }
            else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str4eP").ToUpper())
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeLa").ToUpper() + " " + LocRM.GetString("strAssessment4thPeriod").ToUpper();
            }

            else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str5eP").ToUpper())
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeLa").ToUpper() + " " + LocRM.GetString("strAssessment5thPeriod").ToUpper();
            }
            else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str6eP").ToUpper())
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeLa").ToUpper() + " " + LocRM.GetString("strAssessment6thPeriod").ToUpper();
            }
            else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam1").ToUpper()) && isSemesterGlobalVariable == true)
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeL").ToUpper() + " " + LocRM.GetString("strFirstSemesterExam").ToUpper();
            }
            else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam1").ToUpper()) && isSemesterGlobalVariable == false)
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeL").ToUpper() + " " + LocRM.GetString("strFirstTrimesterExam").ToUpper();
            }
            else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam2").ToUpper()) && isSemesterGlobalVariable == true)
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeL").ToUpper() + " " + LocRM.GetString("strSecondSemesterExam").ToUpper();
            }
            else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam2").ToUpper()) && isSemesterGlobalVariable == false)
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeL").ToUpper() + " " + LocRM.GetString("strSecondTrimesterExam").ToUpper();
            }
            else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam3").ToUpper()) && isSemesterGlobalVariable == false)
            {
                xrLblAssessmentPeriod.Text = LocRM.GetString("strResultsDeL").ToUpper() + " " + LocRM.GetString("strThirdTrimesterExam").ToUpper();
            }

         //   xrLblTeacher.Text = LocRM.GetString("strTeacher").ToUpper() + ": " + UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper();
         //   xrLblSubject.Text = LocRM.GetString("strSubjectOne").ToUpper() + ": " + subjectGlobalVariable.ToUpper();
            xrLblAssessmentPeriodTitle.Text = LocRM.GetString("strListSchoolMarks").ToUpper();
            xrLblClass.Text = LocRM.GetString("strClass").ToUpper() + ": " + className.ToUpper();
            //xrLblAssessmentPeriod.Visible = true;
            //    xrLblTeacher.Visible = true;
            xrLblNumberStudents.Text = subjectTotAvarageGlobalVariable;
            //double avarage = (double)totalPointsGlobalVariable / numberStudentsGlobalVariable;  //subjectTotAvarageGlobalVariable
            //avarage = Math.Round(avarage, 1);
            //xrLblAvarage.Text = LocRM.GetString("strClassAverage") + ": " + avarage.ToString();
            //double passedPercentage = (double)(passedGlobalVariable * 100) / numberStudentsGlobalVariable;

            //passedPercentage = Math.Round(passedPercentage, 1);
            //xrLblPercentageSuccess.Text = LocRM.GetString("strPercentageSuccesses") + ": " + passedPercentage + "%";

            //}

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
                        xrLblFooterText.Text = LocRM.GetString("strDoneAt") + " " + townCommune + ", " + LocRM.GetString("strThe") + " " + myTI.ToTitleCase(currentdateTime) + ". " + LocRM.GetString("strGeneratedBy") + " :" + UserLoggedSurname.ToUpper() + " " + UserLoggedName;
                    }
                    else
                    {
                        xrLblFooterText.Text = LocRM.GetString("strDate") + " " + myTI.ToTitleCase(currentdateTime) + ". " + LocRM.GetString("strGeneratedBy") + " :" + UserLoggedSurname.ToUpper() + " " + UserLoggedName;
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
