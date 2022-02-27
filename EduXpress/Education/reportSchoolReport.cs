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
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace EduXpress.Education
{
    public partial class reportSchoolReport : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportSchoolReport).Assembly);
        public reportSchoolReport()
        {
            InitializeComponent();
        }

        private void reportSchoolReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();
                if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str1eP").ToUpper())
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strAssessment1stPeriod").ToUpper();
                }
                else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str2eP").ToUpper())
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strAssessment2ndPeriod").ToUpper();
                }
                else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str3eP").ToUpper())
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strAssessment3rdPeriod").ToUpper();
                }
                else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str4eP").ToUpper())
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strAssessment4thPeriod").ToUpper();
                }

                else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str5eP").ToUpper())
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strAssessment5thPeriod").ToUpper();
                }
                else if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str6eP").ToUpper())
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strAssessment6thPeriod").ToUpper();
                }
                else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam1").ToUpper()) && isSemesterGlobalVariable == true)
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strFirstSemesterExam").ToUpper();
                }
                else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam1").ToUpper()) && isSemesterGlobalVariable == false)
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strFirstTrimesterExam").ToUpper();
                }
                else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam2").ToUpper()) && isSemesterGlobalVariable == true)
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strSecondSemesterExam").ToUpper();
                }
                else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam2").ToUpper()) && isSemesterGlobalVariable == false)
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strSecondTrimesterExam").ToUpper();
                }
                else if ((assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("strExam3").ToUpper()) && isSemesterGlobalVariable == false)
                {
                    xrLblAssessmentPeriod.Text = LocRM.GetString("strAssessmentPeriod").ToUpper() + ": " + LocRM.GetString("strThirdTrimesterExam").ToUpper();
                }


                xrLblTeacher.Text = LocRM.GetString("strResponsibleTeacher").ToUpper() + ": " + UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper();
                xrLblAssessmentPeriodTitle.Text = LocRM.GetString("strStudentAssessmentReport").ToUpper();
                xrLblClass.Text = LocRM.GetString("strClass").ToUpper() + ": " + className.ToUpper();

                //**************
                xrLblNumberStudents.Text = LocRM.GetString("strTotalNumberStudents") + ": " + numberStudentsGlobalVariable.ToString();
                //double avarage = (double)totalPointsGlobalVariable / numberStudentsGlobalVariable;
                //avarage = Math.Round(avarage, 1);
                xrLblAvarage.Text = LocRM.GetString("strClassAverage") + ": " + totalAvaragePercClassGlobalVariable;
                double passedPercentage = (double)(passedGlobalVariable * 100) / numberStudentsGlobalVariable;

                passedPercentage = Math.Round(passedPercentage, 1);
                xrLblPercentageSuccess.Text = LocRM.GetString("strPercentageSuccesses") + ": " + passedPercentage + "%";
                //**************

                //Set Footer  
                TextInfo myTI = CultureInfo.CurrentCulture.TextInfo; //Set Sentence case
                string currentdateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);

                //Display School info            
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "select SchoolName,SchoolMotto,AddressTown,AddressCommune, SchoolLogo,SchoolStamp from CompanyProfile ";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        xrLblSchoolName.Text = pf.Decrypt(rdr.GetString(0));
                        xrLblSlogan.Text = rdr.GetString(1);

                        string townCommune = "";
                        if (!Convert.IsDBNull(rdr["AddressTown"]))  //check if not null, columns added later
                        {
                            townCommune = pf.Decrypt(rdr.GetString(2));
                            if (townCommune == "")
                            {
                                if (!Convert.IsDBNull(rdr["AddressCommune"]))
                                {
                                    townCommune = rdr.GetString(3);
                                }
                            }
                        }
                        else if (!Convert.IsDBNull(rdr["AddressCommune"]))
                        {
                            townCommune = rdr.GetString(3);
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
                            // Image image = xrPictureSchoolLogo.ImageSource.Image;
                        }
                        else
                        {
                            xrPictureSchoolLogo.Image = null;
                        }

                        if (useSchoolStampGlobalVariable == true)
                        {
                            xrPictureStamp.Visible = true;
                            if (!Convert.IsDBNull(rdr["SchoolStamp"]))
                            {
                                byte[] x = (byte[])rdr["SchoolStamp"];
                                MemoryStream ms = new MemoryStream(x);

                                xrPictureStamp.Image = Image.FromStream(ms);
                                // Image image = xrPictureStamp.ImageSource.Image;
                            }
                        }
                        else
                        {
                            xrPictureStamp.Visible = false;
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

                //Display student details  
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select StudentNumber,StudentSurname,StudentFirstNames,Gender,DateBirth, StudentPicture from Students where StudentNumber = @d1 ";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NVarChar, 60, "StudentNumber").Value = studentNumber;
                    rdr = cmd.ExecuteReader();

  
                    if (rdr.Read())
                    {
                        xrLblStudent.Text = LocRM.GetString("strStudent").ToUpper() + ": " + rdr.GetString(1) + " " + rdr.GetString(2);
                        xrLblSexe.Text = LocRM.GetString("strSex").ToUpper() + ": " + rdr.GetString(3);
                        DateTime dt = (DateTime)rdr.GetValue(4);    
                        xrLblDateBirth.Text = LocRM.GetString("strDateBirth").ToUpper() + ": " + dt.ToString("dd/MM/yyyy");                        
                        

                        if (studentPhotoGlobalVariable == true)
                        {
                            xrPictureStudentPhoto.Visible = true;
                            if (!Convert.IsDBNull(rdr["StudentPicture"]))
                            {
                                byte[] x = (byte[])rdr["StudentPicture"];
                                MemoryStream ms = new MemoryStream(x);

                                xrPictureStudentPhoto.Image = Image.FromStream(ms);
                                // Image image = xrPictureStamp.ImageSource.Image;
                            }
                        }
                        else
                        {
                            xrPictureStudentPhoto.Visible = false;
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
            catch (Exception ex)
            {              

                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
