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

namespace EduXpress.Education.BulletinsRDC
{
    public partial class reportBulletinLIT_1_2 : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportBulletinLIT_1_2).Assembly);
        public reportBulletinLIT_1_2()
        {
            InitializeComponent();
        }

        private void reportBulletinLIT_1_2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Show/hide stamp
            if (useSchoolStampGlobalVariable == true)
            {
                xrPictureStamp.Visible = true;
            }
            else
            {
                xrPictureStamp.Visible = false;
            }

            if(isViewTemplateGlobalVariable == false)
            {
                try
                {
                    //Display School info            
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "select SchoolName,AddressTown,AddressCommune,Province, Code, SchoolStamp from CompanyProfile ";
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            xrLblEcole.Text = xrLblEcole.Text + pf.Decrypt(rdr.GetString(0));

                            if (!Convert.IsDBNull(rdr["AddressTown"]))
                            {
                                xrLblVille.Text = xrLblVille.Text + pf.Decrypt(rdr.GetString(1));
                            }
                            if (!Convert.IsDBNull(rdr["AddressCommune"]))
                            {
                                xrLblCommune.Text = xrLblCommune.Text + rdr.GetString(2);
                            }
                            if (!Convert.IsDBNull(rdr["Province"]))
                            {
                                xrLlbProvince.Text = xrLlbProvince.Text + rdr.GetString(3);
                            }
                            if (!Convert.IsDBNull(rdr["Code"]))
                            {
                                xrCharacterComb1.Text = rdr.GetString(4);
                            }


                            if (!Convert.IsDBNull(rdr["SchoolStamp"]))
                            {
                                byte[] x = (byte[])rdr["SchoolStamp"];
                                MemoryStream ms = new MemoryStream(x);

                                xrPictureStamp.Image = Image.FromStream(ms);
                                Image image = xrPictureStamp.ImageSource.Image;
                            }
                            else
                            {
                                xrPictureStamp.Image = null;
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
                        string ct = "select StudentNumber,StudentSurname,StudentFirstNames,Gender,DateBirth,NoID,NoPerm,BornAt, StudentPicture from Students where StudentNumber = @d1 ";
                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.Parameters.Add("@d1", SqlDbType.NVarChar, 60, "StudentNumber").Value = studentNumber;
                        rdr = cmd.ExecuteReader();


                        if (rdr.Read())
                        {
                            xrLblEleve.Text = xrLblEleve.Text + " " + rdr.GetString(1).ToUpper() + " " + rdr.GetString(2).ToUpper();
                            string sexe = rdr.GetString(3);
                            if (sexe == "Garçon")
                            {
                                xrLblSexe.Text = xrLblSexe.Text + " M";
                            }
                            else if (sexe == "Fille")
                            {
                                xrLblSexe.Text = xrLblSexe.Text + " F";
                            }

                            DateTime dt = (DateTime)rdr.GetValue(4);
                            xrLblBirthDate.Text = "LE " + dt.ToString("dd/MM/yyyy");
                            if (!Convert.IsDBNull(rdr["NoID"]))
                            {
                                xrCharacterCombNoID.Text = rdr.GetString(5);
                            }
                            if (!Convert.IsDBNull(rdr["NoPerm"]))
                            {
                                xrCharacterCombNoPerm.Text = rdr.GetString(6);
                            }
                            if (!Convert.IsDBNull(rdr["BornAt"]))
                            {
                                xrLblBirthPlace.Text = xrLblBirthPlace.Text + " " + rdr.GetString(7).ToUpper();
                            }

                            xrLblClass.Text = xrLblClass.Text + " " + className.ToUpper();
                            xrLblSchoolYear.Text = LocRM.GetString("strSchoolYear").ToUpper() + ": " + SchoolYear.ToUpper();


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

                    //update from maxima to conduite.
                    //if (assessmentPeriodGlobalVariable.ToUpper() == LocRM.GetString("str1eP").ToUpper())
                    //{
                    xrTableMaximaGenerauxPeriode1.Text = pointArrayGlobalVariable[34, 0]; //MaximaGeneraux Periode;
                    xrTableTotauxPeriode1.Text = pointArrayGlobalVariable[35, 0];
                    xrTablePourcentagePeriode1.Text = pointArrayGlobalVariable[36, 0];
                    xrTablePlacePeriode1.Text = pointArrayGlobalVariable[37, 0];
                    xrTableApplicationPeriode1.Text = pointArrayGlobalVariable[38, 0];
                    xrTableConduitePeriode1.Text = pointArrayGlobalVariable[39, 0];
                    //string elementValue1 = pointArrays[2, 0];  //rel
                    //string elementValue2 = pointArrays[3, 0];  //ed civ
                    //string elementValue3 = pointArrays[4, 0];  // vie
                    //Populate points for 1e Period
                    int maxima1 = (Convert.ToInt16(xrTableMaxima1Periode1.Text));
                    xrTableReligionPeriode1.Text = pointArrayGlobalVariable[2, 0]; //Religion
                    xrTableEdCivMoralePeriode1.Text = pointArrayGlobalVariable[3, 0]; //Ed. Civ. & Morale
                    xrTableEducationLaViePeriode1.Text = pointArrayGlobalVariable[4, 0]; //Education à la Vie
                    xrTableInformatiquePeriode1.Text = pointArrayGlobalVariable[5, 0]; //Informatique (1)

                    xrTableMicrobiologiePeriode1.Text = pointArrayGlobalVariable[7, 0]; //Microbiologie (1)
                    xrTableChimiePeriode1.Text = pointArrayGlobalVariable[8, 0]; //Chimie
                    xrTableEduPhysiquePeriode1.Text = pointArrayGlobalVariable[9, 0]; //Education Physique
                    xrTableGéographiePeriode1.Text = pointArrayGlobalVariable[10, 0]; //Géographie

                    xrTableHistoirePeriode1.Text = pointArrayGlobalVariable[11, 0]; //Histoire
                    xrTableMathématiquesPeriode1.Text = pointArrayGlobalVariable[12, 0]; //Mathématiques (1)
                    xrTablePhysiquePeriode1.Text = pointArrayGlobalVariable[13, 0]; //Physique
                    xrTableEcopolPeriode1.Text = pointArrayGlobalVariable[14, 0]; //Sociologie / Ecopol (1)

                    xrTableAnglaisPeriode1.Text = pointArrayGlobalVariable[16, 0]; //Anglais
                    xrTableGrecPeriode1.Text = pointArrayGlobalVariable[17, 0]; //Grec
                    xrTableMathématiques2Periode1.Text = pointArrayGlobalVariable[18, 0]; //Mathématiques (1)
                    xrTableFrançaisPeriode1.Text = pointArrayGlobalVariable[20, 0]; //Français
                    xrTableLatinPeriode1.Text = pointArrayGlobalVariable[21, 0]; //Latin


                    //2e periode
                    xrTableMaximaGenerauxPeriode2.Text = pointArrayGlobalVariable[34, 1]; //MaximaGeneraux Periode;
                    xrTableTotauxPeriode2.Text = pointArrayGlobalVariable[35, 1];
                    xrTablePourcentagePeriode2.Text = pointArrayGlobalVariable[36, 1];
                    xrTablePlacePeriode2.Text = pointArrayGlobalVariable[37, 1];
                    xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionPeriode2.Text = pointArrayGlobalVariable[2, 1]; //Religion
                    xrTableEdCivMoralePeriode2.Text = pointArrayGlobalVariable[3, 1]; //Ed. Civ. & Morale
                    xrTableEducationLaViePeriode2.Text = pointArrayGlobalVariable[4, 1]; //Education à la Vie
                    xrTableInformatiquePeriode2.Text = pointArrayGlobalVariable[5, 1]; //Informatique (1)

                    xrTableMicrobiologiePeriode2.Text = pointArrayGlobalVariable[7, 1]; //Microbiologie (1)
                    xrTableChimiePeriode2.Text = pointArrayGlobalVariable[8, 1]; //Chimie
                    xrTableEduPhysiquePeriode2.Text = pointArrayGlobalVariable[9, 1]; //Education Physique
                    xrTableGéographiePeriode2.Text = pointArrayGlobalVariable[10, 1]; //Géographie

                    xrTableHistoirePeriode2.Text = pointArrayGlobalVariable[11, 1]; //Histoire
                    xrTableMathématiquesPeriode2.Text = pointArrayGlobalVariable[12, 1]; //Mathématiques (1)
                    xrTablePhysiquePeriode2.Text = pointArrayGlobalVariable[13, 1]; //Physique
                    xrTableEcopolPeriode2.Text = pointArrayGlobalVariable[14, 1]; //Sociologie / Ecopol (1)

                    xrTableAnglaisPeriode2.Text = pointArrayGlobalVariable[16, 1]; //Anglais
                    xrTableGrecPeriode2.Text = pointArrayGlobalVariable[17, 1]; //Grec
                    xrTableMathématiques2Periode2.Text = pointArrayGlobalVariable[18, 1]; //Mathématiques (1)
                    xrTableFrançaisPeriode2.Text = pointArrayGlobalVariable[20, 1]; //Français
                    xrTableLatinPeriode2.Text = pointArrayGlobalVariable[21, 1]; //Latin

                    //Exam1
                    xrTableMaximaGenerauxExamSemester1.Text = pointArrayGlobalVariable[34, 2]; //MaximaGeneraux Periode;
                    xrTableTotauxExamSemester1.Text = pointArrayGlobalVariable[35, 2];
                    xrTablePourcentageExamSemester1.Text = pointArrayGlobalVariable[36, 2];
                    xrTablePlaceExamSemester1.Text = pointArrayGlobalVariable[37, 2];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionSemester1Exam.Text = pointArrayGlobalVariable[2, 2]; //Religion
                    xrTableEdCivMoraleSemester1Exam.Text = pointArrayGlobalVariable[3, 2]; //Ed. Civ. & Morale
                    xrTableEducationLaVieSemester1Exam.Text = pointArrayGlobalVariable[4, 2]; //Education à la Vie
                    xrTableInformatiqueSemester1Exam.Text = pointArrayGlobalVariable[5, 2]; //Informatique (1)

                    xrTableMicrobiologieSemester1Exam.Text = pointArrayGlobalVariable[7, 2]; //Microbiologie (1)
                    xrTableChimieSemester1Exam.Text = pointArrayGlobalVariable[8, 2]; //Chimie
                    xrTableEduPhysiqueSemester1Exam.Text = pointArrayGlobalVariable[9, 2]; //Education Physique
                    xrTableGéographieExamSemester1.Text = pointArrayGlobalVariable[10, 2]; //Géographie

                    xrTableHistoireExamSemester1.Text = pointArrayGlobalVariable[11, 2]; //Histoire
                    xrTableMathématiquesExamSemester1.Text = pointArrayGlobalVariable[12, 2]; //Mathématiques (1)
                    xrTablePhysiqueExamSemester1.Text = pointArrayGlobalVariable[13, 2]; //Physique
                    xrTableEcopolExamSemester1.Text = pointArrayGlobalVariable[14, 2]; //Sociologie / Ecopol (1)

                    xrTableAnglaisExamSemester1.Text = pointArrayGlobalVariable[16, 2]; //Anglais
                    xrTableGrecExamSemester1.Text = pointArrayGlobalVariable[17, 2]; //Grec
                    xrTableMathématiques2ExamSemester1.Text = pointArrayGlobalVariable[18, 2]; //Mathématiques (1)
                    xrTableFrançaisExamSemester1.Text = pointArrayGlobalVariable[20, 2]; //Français
                    xrTableLatinExamSemester1.Text = pointArrayGlobalVariable[21, 2]; //Latin

                    //3e periode
                    xrTableMaximaGenerauxPeriode3.Text = pointArrayGlobalVariable[34, 3]; //MaximaGeneraux Periode;
                    xrTableTotauxPeriode3.Text = pointArrayGlobalVariable[35, 3];
                    xrTablePourcentagePeriode3.Text = pointArrayGlobalVariable[36, 3];
                    xrTablePlacePeriode3.Text = pointArrayGlobalVariable[37, 3];
                    xrTableApplicationPeriode3.Text = pointArrayGlobalVariable[38, 3];
                    xrTableConduitePeriode3.Text = pointArrayGlobalVariable[39, 3];

                    xrTableReligionPeriode3.Text = pointArrayGlobalVariable[2, 3]; //Religion
                    xrTableEdCivMoralePeriode3.Text = pointArrayGlobalVariable[3, 3]; //Ed. Civ. & Morale
                    xrTableEducationLaViePeriode3.Text = pointArrayGlobalVariable[4, 3]; //Education à la Vie
                    xrTableInformatiquePeriode3.Text = pointArrayGlobalVariable[5, 3]; //Informatique (1)

                    xrTableMicrobiologiePeriode3.Text = pointArrayGlobalVariable[7, 3]; //Microbiologie (1)
                    xrTableChimiePeriode3.Text = pointArrayGlobalVariable[8, 3]; //Chimie
                    xrTableEduPhysiquePeriode3.Text = pointArrayGlobalVariable[9, 3]; //Education Physique
                    xrTableGéographiePeriode3.Text = pointArrayGlobalVariable[10, 3]; //Géographie

                    xrTableHistoirePeriode3.Text = pointArrayGlobalVariable[11, 3]; //Histoire
                    xrTableMathématiquesPeriode3.Text = pointArrayGlobalVariable[12, 3]; //Mathématiques (1)
                    xrTablePhysiquePeriode3.Text = pointArrayGlobalVariable[13, 3]; //Physique
                    xrTableEcopolPeriode3.Text = pointArrayGlobalVariable[14, 3]; //Sociologie / Ecopol (1)

                    xrTableAnglaisPeriode3.Text = pointArrayGlobalVariable[16, 3]; //Anglais
                    xrTableGrecPeriode3.Text = pointArrayGlobalVariable[17, 3]; //Grec
                    xrTableMathématiques2Periode3.Text = pointArrayGlobalVariable[18, 3]; //Mathématiques (1)
                    xrTableFrançaisPeriode3.Text = pointArrayGlobalVariable[20, 3]; //Français
                    xrTableLatinPeriode3.Text = pointArrayGlobalVariable[21, 3]; //Latin

                    //4e periode
                    xrTableMaximaGenerauxPeriode4.Text = pointArrayGlobalVariable[34, 4]; //MaximaGeneraux Periode;
                    xrTableTotauxPeriode4.Text = pointArrayGlobalVariable[35, 4];
                    xrTablePourcentagePeriode4.Text = pointArrayGlobalVariable[36, 4];
                    xrTablePlacePeriode4.Text = pointArrayGlobalVariable[37, 4];
                    xrTableApplicationPeriode4.Text = pointArrayGlobalVariable[38, 4];
                    xrTableConduitePeriode4.Text = pointArrayGlobalVariable[39, 4];

                    xrTableReligionPeriode4.Text = pointArrayGlobalVariable[2, 4]; //Religion
                    xrTableEdCivMoralePeriode4.Text = pointArrayGlobalVariable[3, 4]; //Ed. Civ. & Morale
                    xrTableEducationLaViePeriode4.Text = pointArrayGlobalVariable[4, 4]; //Education à la Vie
                    xrTableInformatiquePeriode4.Text = pointArrayGlobalVariable[5, 4]; //Informatique (1)

                    xrTableMicrobiologiePeriode4.Text = pointArrayGlobalVariable[7, 4]; //Microbiologie (1)
                    xrTableChimiePeriode4.Text = pointArrayGlobalVariable[8, 4]; //Chimie
                    xrTableEduPhysiquePeriode4.Text = pointArrayGlobalVariable[9, 4]; //Education Physique
                    xrTableGéographiePeriode4.Text = pointArrayGlobalVariable[10, 4]; //Géographie

                    xrTableHistoirePeriode4.Text = pointArrayGlobalVariable[11, 4]; //Histoire
                    xrTableMathématiquesPeriode4.Text = pointArrayGlobalVariable[12, 4]; //Mathématiques (1)
                    xrTablePhysiquePeriode4.Text = pointArrayGlobalVariable[13, 4]; //Physique
                    xrTableEcopolPeriode4.Text = pointArrayGlobalVariable[14, 4]; //Sociologie / Ecopol (1)

                    xrTableAnglaisPeriode4.Text = pointArrayGlobalVariable[16, 4]; //Anglais
                    xrTableGrecPeriode4.Text = pointArrayGlobalVariable[17, 4]; //Grec
                    xrTableMathématiques2Periode4.Text = pointArrayGlobalVariable[18, 4]; //Mathématiques (1)
                    xrTableFrançaisPeriode4.Text = pointArrayGlobalVariable[20, 4]; //Français
                    xrTableLatinPeriode4.Text = pointArrayGlobalVariable[21, 4]; //Latin

                    //Exam2
                    xrTableMaximaGenerauxExamSemester2.Text = pointArrayGlobalVariable[34, 5]; //MaximaGeneraux Periode;
                    xrTableTotauxExamSemester2.Text = pointArrayGlobalVariable[35, 5];
                    xrTablePourcentageExamSemester2.Text = pointArrayGlobalVariable[36, 5];
                    xrTablePlaceExamSemester2.Text = pointArrayGlobalVariable[37, 5];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionSemester2Exam.Text = pointArrayGlobalVariable[2, 5]; //Religion
                    xrTableEdCivMoraleSemester2Exam.Text = pointArrayGlobalVariable[3, 5]; //Ed. Civ. & Morale
                    xrTableEducationLaVieSemester2Exam.Text = pointArrayGlobalVariable[4, 5]; //Education à la Vie
                    xrTableInformatiqueSemester1Exam.Text = pointArrayGlobalVariable[5, 5]; //Informatique (1)

                    xrTableMicrobiologieSemester2Exam.Text = pointArrayGlobalVariable[7, 5]; //Microbiologie (1)
                    xrTableChimieSemester2Exam.Text = pointArrayGlobalVariable[8, 5]; //Chimie
                    xrTableEduPhysiqueSemester2Exam.Text = pointArrayGlobalVariable[9, 5]; //Education Physique
                    xrTableGéographieExamSemester2.Text = pointArrayGlobalVariable[10, 5]; //Géographie

                    xrTableHistoireExamSemester2.Text = pointArrayGlobalVariable[11, 5]; //Histoire
                    xrTableMathématiquesExamSemester2.Text = pointArrayGlobalVariable[12, 5]; //Mathématiques (1)
                    xrTablePhysiqueExamSemester2.Text = pointArrayGlobalVariable[13, 5]; //Physique
                    xrTableEcopolExamSemester2.Text = pointArrayGlobalVariable[14, 5]; //Sociologie / Ecopol (1)

                    xrTableAnglaisExamSemester2.Text = pointArrayGlobalVariable[16, 5]; //Anglais
                    xrTableGrecExamSemester2.Text = pointArrayGlobalVariable[17, 5]; //Grec
                    xrTableMathématiques2ExamSemester2.Text = pointArrayGlobalVariable[18, 5]; //Mathématiques (1)
                    xrTableFrançaisExamSemester2.Text = pointArrayGlobalVariable[20, 5]; //Français
                    xrTableLatinExamSemester2.Text = pointArrayGlobalVariable[21, 5]; //Latin

                    //Tot semester 1
                    xrTableMaximaGenerauxTotSemester1.Text = pointArrayGlobalVariable[34, 9]; //MaximaGeneraux Periode;
                    xrTableTotauxTotSemester1.Text = pointArrayGlobalVariable[35, 9];
                    xrTablePourcentageTotSemester1.Text = pointArrayGlobalVariable[36, 9];
                    xrTablePlaceTotSemester1.Text = pointArrayGlobalVariable[37, 9];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionSemester1Tot.Text = pointArrayGlobalVariable[2, 9]; //Religion
                    xrTableEdCivMoraleSemester1Tot.Text = pointArrayGlobalVariable[3, 9]; //Ed. Civ. & Morale
                    xrTableEducationLaVieSemester1Tot.Text = pointArrayGlobalVariable[4, 9]; //Education à la Vie
                    xrTableInformatiqueSemester1Tot.Text = pointArrayGlobalVariable[5, 9]; //Informatique (1)

                    xrTableMicrobiologieSemester1Tot.Text = pointArrayGlobalVariable[7, 9]; //Microbiologie (1)
                    xrTableChimieSemester1Tot.Text = pointArrayGlobalVariable[8, 9]; //Chimie
                    xrTableEduPhysiqueSemester1Tot.Text = pointArrayGlobalVariable[9, 9]; //Education Physique
                    xrTableGéographieTotSemester1.Text = pointArrayGlobalVariable[10, 9]; //Géographie

                    xrTableHistoireTotSemester1.Text = pointArrayGlobalVariable[11, 9]; //Histoire
                    xrTableMathématiquesTotSemester1.Text = pointArrayGlobalVariable[12, 9]; //Mathématiques (1)
                    xrTablePhysiqueTotSemester1.Text = pointArrayGlobalVariable[13, 9]; //Physique
                    xrTableEcopolTotSemester1.Text = pointArrayGlobalVariable[14, 9]; //Sociologie / Ecopol (1)

                    xrTableAnglaisTotSemester1.Text = pointArrayGlobalVariable[16, 9]; //Anglais
                    xrTableGrecTotSemester1.Text = pointArrayGlobalVariable[17, 9]; //Grec
                    xrTableMathématiques2TotSemester1.Text = pointArrayGlobalVariable[18, 9]; //Mathématiques (1)
                    xrTableFrançaisTotSemester1.Text = pointArrayGlobalVariable[20, 9]; //Français
                    xrTableLatinTotSemester1.Text = pointArrayGlobalVariable[21, 9]; //Latin

                    //Tot semester 2
                    xrTableMaximaGenerauxTotSemester2.Text = pointArrayGlobalVariable[34, 10]; //MaximaGeneraux Periode;
                    xrTableTotauxTotSemester2.Text = pointArrayGlobalVariable[35, 10];
                    xrTablePourcentageTotSemester2.Text = pointArrayGlobalVariable[36, 10];
                    xrTablePlaceTotSemester2.Text = pointArrayGlobalVariable[37, 10];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionSemester2Tot.Text = pointArrayGlobalVariable[2, 10]; //Religion
                    xrTableEdCivMoraleSemester2Tot.Text = pointArrayGlobalVariable[3, 10]; //Ed. Civ. & Morale
                    xrTableEducationLaVieSemester2Tot.Text = pointArrayGlobalVariable[4, 10]; //Education à la Vie
                    xrTableInformatiqueSemester2Tot.Text = pointArrayGlobalVariable[5, 10]; //Informatique (1)

                    xrTableMicrobiologieSemester2Tot.Text = pointArrayGlobalVariable[7, 10]; //Microbiologie (1)
                    xrTableChimieSemester2Tot.Text = pointArrayGlobalVariable[8, 10]; //Chimie
                    xrTableEduPhysiqueSemester2Tot.Text = pointArrayGlobalVariable[9, 10]; //Education Physique
                    xrTableGéographieTotSemester2.Text = pointArrayGlobalVariable[10, 10]; //Géographie

                    xrTableHistoireTotSemester2.Text = pointArrayGlobalVariable[11, 10]; //Histoire
                    xrTableMathématiquesTotSemester2.Text = pointArrayGlobalVariable[12, 10]; //Mathématiques (1)
                    xrTablePhysiqueTotSemester2.Text = pointArrayGlobalVariable[13, 10]; //Physique
                    xrTableEcopolTotSemester2.Text = pointArrayGlobalVariable[14, 10]; //Sociologie / Ecopol (1)

                    xrTableAnglaisTotSemester2.Text = pointArrayGlobalVariable[16, 10]; //Anglais
                    xrTableGrecTotSemester2.Text = pointArrayGlobalVariable[17, 10]; //Grec
                    xrTableMathématiques2TotSemester2.Text = pointArrayGlobalVariable[18, 10]; //Mathématiques (1)
                    xrTableFrançaisTotSemester2.Text = pointArrayGlobalVariable[20, 10]; //Français
                    xrTableLatinTotSemester2.Text = pointArrayGlobalVariable[21, 10]; //Latin

                    //Tot General
                    xrTableMaximaGenerauxTotGen.Text = pointArrayGlobalVariable[34, 12]; //MaximaGeneraux Periode;
                    xrTableTotauxTotGen.Text = pointArrayGlobalVariable[35, 12];
                    xrTablePourcentageTotGen.Text = pointArrayGlobalVariable[36, 12];
                    xrTablePlaceTotGen.Text = pointArrayGlobalVariable[37, 12];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionTotGen.Text = pointArrayGlobalVariable[2, 12]; //Religion
                    xrTableEdCivMoraleTotGen.Text = pointArrayGlobalVariable[3, 12]; //Ed. Civ. & Morale
                    xrTableEducationLaVieTotGen.Text = pointArrayGlobalVariable[4, 12]; //Education à la Vie
                    xrTableInformatiqueTotGen.Text = pointArrayGlobalVariable[5, 12]; //Informatique (1)

                    xrTableMicrobiologieTotGen.Text = pointArrayGlobalVariable[7, 12]; //Microbiologie (1)
                    xrTableChimieTotGen.Text = pointArrayGlobalVariable[8, 12]; //Chimie
                    xrTableEduPhysiqueGenTot.Text = pointArrayGlobalVariable[9, 12]; //Education Physique
                    xrTableGéographieTotGen.Text = pointArrayGlobalVariable[10, 12]; //Géographie

                    xrTableHistoireTotGen.Text = pointArrayGlobalVariable[11, 12]; //Histoire
                    xrTableMathématiquesTotGen.Text = pointArrayGlobalVariable[12, 12]; //Mathématiques (1)
                    xrTablePhysiqueTotGen.Text = pointArrayGlobalVariable[13, 12]; //Physique
                    xrTableEcopolTotGen.Text = pointArrayGlobalVariable[14, 12]; //Sociologie / Ecopol (1)

                    xrTableAnglaisTotGen.Text = pointArrayGlobalVariable[16, 12]; //Anglais
                    xrTableGrecTotGen.Text = pointArrayGlobalVariable[17, 12]; //Grec
                    xrTableMathématiques2TotGen.Text = pointArrayGlobalVariable[18, 12]; //Mathématiques (1)
                    xrTableFrançaisTotGen.Text = pointArrayGlobalVariable[20, 12]; //Français
                    xrTableLatinTotGen.Text = pointArrayGlobalVariable[21, 12]; //Latin

                }
                catch (Exception ex)
                {

                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            

        }
    }
}
