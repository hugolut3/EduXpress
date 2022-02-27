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
                        string town = "";
                        string commune = "";

                        if (rdr.Read())
                        {
                            xrLblEcole.Text = pf.Decrypt(rdr.GetString(0)).ToUpper();

                            if (!Convert.IsDBNull(rdr["AddressTown"]))
                            {
                                town = pf.Decrypt(rdr.GetString(1));
                                xrLblVille.Text = town.ToUpper();                                
                            }
                            if (!Convert.IsDBNull(rdr["AddressCommune"]))
                            {
                                commune = rdr.GetString(2);
                                xrLblCommune.Text = commune.ToUpper();
                            }
                            if (!Convert.IsDBNull(rdr["Province"]))
                            {
                                xrLlbProvince.Text = rdr.GetString(3).ToUpper();
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

                            //Show date and place on bulletin
                            if (addTimeBulletinGlobalVariable == true)
                            {
                                xrlblPlace.ForeColor = Color.Blue ;
                                xrlblDate.ForeColor = Color.Blue;
                                //string townCommune = "";

                                if (town !="")  //check if not null,or blank
                                {
                                    xrlblPlace.Text = town;
                                    xrlblDate.Text = reportDateGlobalVariable;                                    
                                }
                                else if (commune != "")  //check if not null,or blank
                                {
                                    xrlblPlace.Text = commune;
                                    xrlblDate.Text = reportDateGlobalVariable;
                                }
                                else
                                {
                                    xrlblPlace.Text = "................";
                                    xrlblPlace.ForeColor = Color.Black;
                                    xrlblDate.Text = "................";
                                    xrlblDate.ForeColor = Color.Black;
                                }                                
                                
                            }
                            else
                            {
                                xrlblPlace.Text = "................";
                                xrlblPlace.ForeColor = Color.Black;
                                xrlblDate.Text = "................";
                                xrlblDate.ForeColor = Color.Black;
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
                            if (!Convert.IsDBNull(rdr["StudentSurname"]))
                            {
                                if (!Convert.IsDBNull(rdr["StudentFirstNames"]))
                                {
                                    xrLblEleve.Text = rdr.GetString(1).ToUpper() + " " + rdr.GetString(2).ToUpper();
                                }                                    
                            }

                            if (!Convert.IsDBNull(rdr["Gender"]))
                            {
                                string sexe = rdr.GetString(3);
                                if (sexe == "Garçon")
                                {
                                    xrLblSexe.Text = " M";
                                }
                                else if (sexe == "Fille")
                                {
                                    xrLblSexe.Text = " F";
                                }
                            }                            

                            //DateTime dt = (DateTime)rdr.GetValue(4);
                            if (!Convert.IsDBNull(rdr["DateBirth"]))
                            {
                                DateTime dt;
                                if (DateTime.TryParse(rdr.GetValue(4).ToString(), out dt))
                                {
                                    xrLblBirthDate.Text = dt.ToString("dd/MM/yyyy");
                                }
                            }
                                                     
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
                                xrLblBirthPlace.Text = rdr.GetString(7).ToUpper();
                            }

                            xrLblClass.Text = className.ToUpper();
                            xrLblSchoolYear.Text = SchoolYear;
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
                    int intmarkSubject = 0;
                    string stmarkSubject = "";
                    bool canConvert = false;
                    xrTableReligionPeriode1.Text = pointArrayGlobalVariable[2, 0]; //Religion
                    stmarkSubject = xrTableReligionPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert ==true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionPeriode1.ForeColor  = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionPeriode1.ForeColor = Color.Red;
                        }
                    }
                    
                    xrTableEdCivMoralePeriode1.Text = pointArrayGlobalVariable[3, 0]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoralePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoralePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoralePeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaViePeriode1.Text = pointArrayGlobalVariable[4, 0]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaViePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaViePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaViePeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode1.Text = pointArrayGlobalVariable[5, 0]; //Informatique (1)
                    stmarkSubject = xrTableInformatiquePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode1.ForeColor = Color.Red;
                        }
                    }

                    int maxima2 = (Convert.ToInt16(xrTableMaxima2Periode1.Text));

                    xrTableMicrobiologiePeriode1.Text = pointArrayGlobalVariable[7, 0]; //Microbiologie (1)                    
                    stmarkSubject = xrTableMicrobiologiePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologiePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologiePeriode1.ForeColor = Color.Red;
                        }
                    }


                    xrTableChimiePeriode1.Text = pointArrayGlobalVariable[8, 0]; //Chimie
                    stmarkSubject = xrTableChimiePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimiePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimiePeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiquePeriode1.Text = pointArrayGlobalVariable[9, 0]; //Education Physique
                    stmarkSubject = xrTableEduPhysiquePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiquePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiquePeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographiePeriode1.Text = pointArrayGlobalVariable[10, 0]; //Géographie
                    stmarkSubject = xrTableGéographiePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographiePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographiePeriode1.ForeColor = Color.Red;
                        }
                    }


                    xrTableHistoirePeriode1.Text = pointArrayGlobalVariable[11, 0]; //Histoire
                    stmarkSubject = xrTableHistoirePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoirePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoirePeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesPeriode1.Text = pointArrayGlobalVariable[12, 0]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiquePeriode1.Text = pointArrayGlobalVariable[13, 0]; //Physique
                    stmarkSubject = xrTablePhysiquePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiquePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiquePeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolPeriode1.Text = pointArrayGlobalVariable[14, 0]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolPeriode1.ForeColor = Color.Red;
                        }
                    }

                    int maxima3 = (Convert.ToInt16(xrTableMaxima3Periode1.Text));

                    xrTableAnglaisPeriode1.Text = pointArrayGlobalVariable[16, 0]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecPeriode1.Text = pointArrayGlobalVariable[17, 0]; //Grec
                    stmarkSubject = xrTableGrecPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2Periode1.Text = pointArrayGlobalVariable[18, 0]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2Periode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2Periode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2Periode1.ForeColor = Color.Red;
                        }
                    }

                    
                    int maxima4 = (Convert.ToInt16(xrTableMaxima4Periode1.Text));

                    xrTableFrançaisPeriode1.Text = pointArrayGlobalVariable[20, 0]; //Français
                    stmarkSubject = xrTableFrançaisPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinPeriode1.Text = pointArrayGlobalVariable[21, 0]; //Latin
                    stmarkSubject = xrTableLatinPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinPeriode1.ForeColor = Color.Red;
                        }
                    }

                    //2e periode
                    xrTableMaximaGenerauxPeriode2.Text = pointArrayGlobalVariable[34, 1]; //MaximaGeneraux Periode;
                    xrTableTotauxPeriode2.Text = pointArrayGlobalVariable[35, 1];
                    xrTablePourcentagePeriode2.Text = pointArrayGlobalVariable[36, 1];
                    xrTablePlacePeriode2.Text = pointArrayGlobalVariable[37, 1];
                    xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    

                    xrTableReligionPeriode2.Text = pointArrayGlobalVariable[2, 1]; //Religion
                    stmarkSubject = xrTableReligionPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableEdCivMoralePeriode2.Text = pointArrayGlobalVariable[3, 1]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoralePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoralePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoralePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaViePeriode2.Text = pointArrayGlobalVariable[4, 1]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaViePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaViePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaViePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode2.Text = pointArrayGlobalVariable[5, 1]; //Informatique (1)
                    stmarkSubject = xrTableInformatiquePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode2.ForeColor = Color.Red;
                        }
                    }


                    xrTableMicrobiologiePeriode2.Text = pointArrayGlobalVariable[7, 1]; //Microbiologie (1)
                    stmarkSubject = xrTableMicrobiologiePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologiePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologiePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimiePeriode2.Text = pointArrayGlobalVariable[8, 1]; //Chimie
                    stmarkSubject = xrTableChimiePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimiePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimiePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiquePeriode2.Text = pointArrayGlobalVariable[9, 1]; //Education Physique
                    stmarkSubject = xrTableEduPhysiquePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiquePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiquePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographiePeriode2.Text = pointArrayGlobalVariable[10, 1]; //Géographie
                    stmarkSubject = xrTableGéographiePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographiePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographiePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableHistoirePeriode2.Text = pointArrayGlobalVariable[11, 1]; //Histoire
                    stmarkSubject = xrTableHistoirePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoirePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoirePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesPeriode2.Text = pointArrayGlobalVariable[12, 1]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiquePeriode2.Text = pointArrayGlobalVariable[13, 1]; //Physique
                    stmarkSubject = xrTablePhysiquePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiquePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiquePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolPeriode2.Text = pointArrayGlobalVariable[14, 1]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolPeriode2.ForeColor = Color.Red;
                        }
                    }


                    xrTableAnglaisPeriode2.Text = pointArrayGlobalVariable[16, 1]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecPeriode2.Text = pointArrayGlobalVariable[17, 1]; //Grec
                    stmarkSubject = xrTableGrecPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2Periode2.Text = pointArrayGlobalVariable[18, 1]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2Periode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2Periode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2Periode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableFrançaisPeriode2.Text = pointArrayGlobalVariable[20, 1]; //Français
                    stmarkSubject = xrTableFrançaisPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinPeriode2.Text = pointArrayGlobalVariable[21, 1]; //Latin
                    stmarkSubject = xrTableLatinPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinPeriode2.ForeColor = Color.Red;
                        }
                    }


                    //Exam1
                    xrTableMaximaGenerauxExamSemester1.Text = pointArrayGlobalVariable[34, 2]; //MaximaGeneraux Periode;
                    xrTableTotauxExamSemester1.Text = pointArrayGlobalVariable[35, 2];
                    xrTablePourcentageExamSemester1.Text = pointArrayGlobalVariable[36, 2];
                    xrTablePlaceExamSemester1.Text = pointArrayGlobalVariable[37, 2];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    int maximaEx1 = (Convert.ToInt16(xrTableMaxima1Exam1.Text));

                    xrTableReligionSemester1Exam.Text = pointArrayGlobalVariable[2, 2]; //Religion
                    stmarkSubject = xrTableReligionSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableEdCivMoraleSemester1Exam.Text = pointArrayGlobalVariable[3, 2]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoraleSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoraleSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoraleSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaVieSemester1Exam.Text = pointArrayGlobalVariable[4, 2]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaVieSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaVieSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaVieSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueSemester1Exam.Text = pointArrayGlobalVariable[5, 2]; //Informatique (1)
                    stmarkSubject = xrTableInformatiqueSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    int maximaEx2 = (Convert.ToInt16(xrTableMaxima2Exam1.Text));

                    xrTableMicrobiologieSemester1Exam.Text = pointArrayGlobalVariable[7, 2]; //Microbiologie (1)
                    stmarkSubject = xrTableMicrobiologieSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologieSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologieSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimieSemester1Exam.Text = pointArrayGlobalVariable[8, 2]; //Chimie
                    stmarkSubject = xrTableChimieSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimieSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimieSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiqueSemester1Exam.Text = pointArrayGlobalVariable[9, 2]; //Education Physique
                    stmarkSubject = xrTableEduPhysiqueSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiqueSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiqueSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieExamSemester1.Text = pointArrayGlobalVariable[10, 2]; //Géographie
                    stmarkSubject = xrTableGéographieExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieExamSemester1.ForeColor = Color.Red;
                        }
                    }


                    xrTableHistoireExamSemester1.Text = pointArrayGlobalVariable[11, 2]; //Histoire
                    stmarkSubject = xrTableHistoireExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoireExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoireExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesExamSemester1.Text = pointArrayGlobalVariable[12, 2]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiqueExamSemester1.Text = pointArrayGlobalVariable[13, 2]; //Physique
                    stmarkSubject = xrTablePhysiqueExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiqueExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiqueExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolExamSemester1.Text = pointArrayGlobalVariable[14, 2]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaEx3 = (Convert.ToInt16(xrTableMaxima3Exam1.Text));

                    xrTableAnglaisExamSemester1.Text = pointArrayGlobalVariable[16, 2]; //Anglais
                    stmarkSubject = xrTableAnglaisExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecExamSemester1.Text = pointArrayGlobalVariable[17, 2]; //Grec
                    stmarkSubject = xrTableGrecExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2ExamSemester1.Text = pointArrayGlobalVariable[18, 2]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2ExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2ExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2ExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaEx4 = (Convert.ToInt16(xrTableMaxima4Exam1.Text));

                    xrTableFrançaisExamSemester1.Text = pointArrayGlobalVariable[20, 2]; //Français
                    stmarkSubject = xrTableFrançaisExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinExamSemester1.Text = pointArrayGlobalVariable[21, 2]; //Latin
                    stmarkSubject = xrTableLatinExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinExamSemester1.ForeColor = Color.Red;
                        }
                    }


                    //3e periode
                    xrTableMaximaGenerauxPeriode3.Text = pointArrayGlobalVariable[34, 3]; //MaximaGeneraux Periode;
                    xrTableTotauxPeriode3.Text = pointArrayGlobalVariable[35, 3];
                    xrTablePourcentagePeriode3.Text = pointArrayGlobalVariable[36, 3];
                    xrTablePlacePeriode3.Text = pointArrayGlobalVariable[37, 3];
                    xrTableApplicationPeriode3.Text = pointArrayGlobalVariable[38, 3];
                    xrTableConduitePeriode3.Text = pointArrayGlobalVariable[39, 3];

                    xrTableReligionPeriode3.Text = pointArrayGlobalVariable[2, 3]; //Religion
                    stmarkSubject = xrTableReligionPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableEdCivMoralePeriode3.Text = pointArrayGlobalVariable[3, 3]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoralePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoralePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoralePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaViePeriode3.Text = pointArrayGlobalVariable[4, 3]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaViePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaViePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaViePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode3.Text = pointArrayGlobalVariable[5, 3]; //Informatique (1)
                    stmarkSubject = xrTableInformatiquePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode3.ForeColor = Color.Red;
                        }
                    }


                    xrTableMicrobiologiePeriode3.Text = pointArrayGlobalVariable[7, 3]; //Microbiologie (1)
                    stmarkSubject = xrTableMicrobiologiePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologiePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologiePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimiePeriode3.Text = pointArrayGlobalVariable[8, 3]; //Chimie
                    stmarkSubject = xrTableChimiePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimiePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimiePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiquePeriode3.Text = pointArrayGlobalVariable[9, 3]; //Education Physique
                    stmarkSubject = xrTableEduPhysiquePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiquePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiquePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographiePeriode3.Text = pointArrayGlobalVariable[10, 3]; //Géographie
                    stmarkSubject = xrTableGéographiePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographiePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographiePeriode3.ForeColor = Color.Red;
                        }
                    }


                    xrTableHistoirePeriode3.Text = pointArrayGlobalVariable[11, 3]; //Histoire
                    stmarkSubject = xrTableHistoirePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoirePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoirePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesPeriode3.Text = pointArrayGlobalVariable[12, 3]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiquePeriode3.Text = pointArrayGlobalVariable[13, 3]; //Physique
                    stmarkSubject = xrTablePhysiquePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiquePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiquePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolPeriode3.Text = pointArrayGlobalVariable[14, 3]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolPeriode3.ForeColor = Color.Red;
                        }
                    }


                    xrTableAnglaisPeriode3.Text = pointArrayGlobalVariable[16, 3]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecPeriode3.Text = pointArrayGlobalVariable[17, 3]; //Grec
                    stmarkSubject = xrTableGrecPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2Periode3.Text = pointArrayGlobalVariable[18, 3]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2Periode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2Periode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2Periode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableFrançaisPeriode3.Text = pointArrayGlobalVariable[20, 3]; //Français
                    stmarkSubject = xrTableFrançaisPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinPeriode3.Text = pointArrayGlobalVariable[21, 3]; //Latin
                    stmarkSubject = xrTableLatinPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinPeriode3.ForeColor = Color.Red;
                        }
                    }


                    //4e periode
                    xrTableMaximaGenerauxPeriode4.Text = pointArrayGlobalVariable[34, 4]; //MaximaGeneraux Periode;
                    xrTableTotauxPeriode4.Text = pointArrayGlobalVariable[35, 4];
                    xrTablePourcentagePeriode4.Text = pointArrayGlobalVariable[36, 4];
                    xrTablePlacePeriode4.Text = pointArrayGlobalVariable[37, 4];
                    xrTableApplicationPeriode4.Text = pointArrayGlobalVariable[38, 4];
                    xrTableConduitePeriode4.Text = pointArrayGlobalVariable[39, 4];

                    xrTableReligionPeriode4.Text = pointArrayGlobalVariable[2, 4]; //Religion
                    stmarkSubject = xrTableReligionPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableEdCivMoralePeriode4.Text = pointArrayGlobalVariable[3, 4]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoralePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoralePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoralePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaViePeriode4.Text = pointArrayGlobalVariable[4, 4]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaViePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaViePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaViePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode4.Text = pointArrayGlobalVariable[5, 4]; //Informatique (1)
                    stmarkSubject = xrTableInformatiquePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableMicrobiologiePeriode4.Text = pointArrayGlobalVariable[7, 4]; //Microbiologie (1)
                    stmarkSubject = xrTableMicrobiologiePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologiePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologiePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimiePeriode4.Text = pointArrayGlobalVariable[8, 4]; //Chimie
                    stmarkSubject = xrTableChimiePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimiePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimiePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiquePeriode4.Text = pointArrayGlobalVariable[9, 4]; //Education Physique
                    stmarkSubject = xrTableEduPhysiquePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiquePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiquePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographiePeriode4.Text = pointArrayGlobalVariable[10, 4]; //Géographie
                    stmarkSubject = xrTableGéographiePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographiePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographiePeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableHistoirePeriode4.Text = pointArrayGlobalVariable[11, 4]; //Histoire
                    stmarkSubject = xrTableHistoirePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoirePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoirePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesPeriode4.Text = pointArrayGlobalVariable[12, 4]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiquePeriode4.Text = pointArrayGlobalVariable[13, 4]; //Physique
                    stmarkSubject = xrTablePhysiquePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiquePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiquePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolPeriode4.Text = pointArrayGlobalVariable[14, 4]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableAnglaisPeriode4.Text = pointArrayGlobalVariable[16, 4]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecPeriode4.Text = pointArrayGlobalVariable[17, 4]; //Grec
                    stmarkSubject = xrTableGrecPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2Periode4.Text = pointArrayGlobalVariable[18, 4]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2Periode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2Periode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2Periode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableFrançaisPeriode4.Text = pointArrayGlobalVariable[20, 4]; //Français
                    stmarkSubject = xrTableFrançaisPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinPeriode4.Text = pointArrayGlobalVariable[21, 4]; //Latin
                    stmarkSubject = xrTableLatinPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinPeriode4.ForeColor = Color.Red;
                        }
                    }


                    //Exam2
                    xrTableMaximaGenerauxExamSemester2.Text = pointArrayGlobalVariable[34, 5]; //MaximaGeneraux Periode;
                    xrTableTotauxExamSemester2.Text = pointArrayGlobalVariable[35, 5];
                    xrTablePourcentageExamSemester2.Text = pointArrayGlobalVariable[36, 5];
                    xrTablePlaceExamSemester2.Text = pointArrayGlobalVariable[37, 5];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionSemester2Exam.Text = pointArrayGlobalVariable[2, 5]; //Religion
                    stmarkSubject = xrTableReligionSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionSemester2Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableEdCivMoraleSemester2Exam.Text = pointArrayGlobalVariable[3, 5]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoraleSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoraleSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoraleSemester2Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaVieSemester2Exam.Text = pointArrayGlobalVariable[4, 5]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaVieSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaVieSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaVieSemester2Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueSemester2Exam.Text = pointArrayGlobalVariable[5, 5]; //Informatique (1)
                    stmarkSubject = xrTableInformatiqueSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueSemester2Exam.ForeColor = Color.Red;
                        }
                    }


                    xrTableMicrobiologieSemester2Exam.Text = pointArrayGlobalVariable[7, 5]; //Microbiologie (1)                   
                    stmarkSubject = xrTableMicrobiologieSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologieSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologieSemester2Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimieSemester2Exam.Text = pointArrayGlobalVariable[8, 5]; //Chimie
                    stmarkSubject = xrTableChimieSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimieSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimieSemester2Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiqueSemester2Exam.Text = pointArrayGlobalVariable[9, 5]; //Education Physique
                    stmarkSubject = xrTableEduPhysiqueSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiqueSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiqueSemester2Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieExamSemester2.Text = pointArrayGlobalVariable[10, 5]; //Géographie
                    stmarkSubject = xrTableGéographieExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieExamSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableHistoireExamSemester2.Text = pointArrayGlobalVariable[11, 5]; //Histoire
                    stmarkSubject = xrTableHistoireExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoireExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoireExamSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesExamSemester2.Text = pointArrayGlobalVariable[12, 5]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiqueExamSemester2.Text = pointArrayGlobalVariable[13, 5]; //Physique
                    stmarkSubject = xrTablePhysiqueExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiqueExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiqueExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolExamSemester2.Text = pointArrayGlobalVariable[14, 5]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableAnglaisExamSemester2.Text = pointArrayGlobalVariable[16, 5]; //Anglais
                    stmarkSubject = xrTableAnglaisExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecExamSemester2.Text = pointArrayGlobalVariable[17, 5]; //Grec
                    stmarkSubject = xrTableGrecExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2ExamSemester2.Text = pointArrayGlobalVariable[18, 5]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2ExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2ExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2ExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableFrançaisExamSemester2.Text = pointArrayGlobalVariable[20, 5]; //Français
                    stmarkSubject = xrTableFrançaisExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinExamSemester2.Text = pointArrayGlobalVariable[21, 5]; //Latin
                    stmarkSubject = xrTableLatinExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    //Tot semester 1
                    xrTableMaximaGenerauxTotSemester1.Text = pointArrayGlobalVariable[34, 9]; //MaximaGeneraux Periode;
                    xrTableTotauxTotSemester1.Text = pointArrayGlobalVariable[35, 9];
                    xrTablePourcentageTotSemester1.Text = pointArrayGlobalVariable[36, 9];
                    xrTablePlaceTotSemester1.Text = pointArrayGlobalVariable[37, 9];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    int maximaTot1 = (Convert.ToInt16(xrTableMaxima1Tot1.Text));

                    xrTableReligionSemester1Tot.Text = pointArrayGlobalVariable[2, 9]; //Religion
                    stmarkSubject = xrTableReligionSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionSemester1Tot.ForeColor = Color.Red;
                        }
                    }


                    xrTableEdCivMoraleSemester1Tot.Text = pointArrayGlobalVariable[3, 9]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoraleSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoraleSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoraleSemester1Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaVieSemester1Tot.Text = pointArrayGlobalVariable[4, 9]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaVieSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaVieSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaVieSemester1Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueSemester1Tot.Text = pointArrayGlobalVariable[5, 9]; //Informatique (1)
                    stmarkSubject = xrTableInformatiqueSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueSemester1Tot.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot2 = (Convert.ToInt16(xrTableMaxima2Tot1.Text));

                    xrTableMicrobiologieSemester1Tot.Text = pointArrayGlobalVariable[7, 9]; //Microbiologie (1)
                    stmarkSubject = xrTableMicrobiologieSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologieSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologieSemester1Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimieSemester1Tot.Text = pointArrayGlobalVariable[8, 9]; //Chimie
                    stmarkSubject = xrTableChimieSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimieSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimieSemester1Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiqueSemester1Tot.Text = pointArrayGlobalVariable[9, 9]; //Education Physique
                    stmarkSubject = xrTableEduPhysiqueSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiqueSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiqueSemester1Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieTotSemester1.Text = pointArrayGlobalVariable[10, 9]; //Géographie
                    stmarkSubject = xrTableGéographieTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieTotSemester1.ForeColor = Color.Red;
                        }
                    }


                    xrTableHistoireTotSemester1.Text = pointArrayGlobalVariable[11, 9]; //Histoire
                    stmarkSubject = xrTableHistoireTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoireTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoireTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesTotSemester1.Text = pointArrayGlobalVariable[12, 9]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiqueTotSemester1.Text = pointArrayGlobalVariable[13, 9]; //Physique
                    stmarkSubject = xrTablePhysiqueTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiqueTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiqueTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolTotSemester1.Text = pointArrayGlobalVariable[14, 9]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot3 = (Convert.ToInt16(xrTableMaxima3Tot1.Text));

                    xrTableAnglaisTotSemester1.Text = pointArrayGlobalVariable[16, 9]; //Anglais
                    stmarkSubject = xrTableAnglaisTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecTotSemester1.Text = pointArrayGlobalVariable[17, 9]; //Grec
                    stmarkSubject = xrTableGrecTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2TotSemester1.Text = pointArrayGlobalVariable[18, 9]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2TotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2TotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2TotSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot4 = (Convert.ToInt16(xrTableMaxima4Tot1.Text));

                    xrTableFrançaisTotSemester1.Text = pointArrayGlobalVariable[20, 9]; //Français
                    stmarkSubject = xrTableFrançaisTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinTotSemester1.Text = pointArrayGlobalVariable[21, 9]; //Latin
                    stmarkSubject = xrTableLatinTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinTotSemester1.ForeColor = Color.Red;
                        }
                    }


                    //Tot semester 2
                    xrTableMaximaGenerauxTotSemester2.Text = pointArrayGlobalVariable[34, 10]; //MaximaGeneraux Periode;
                    xrTableTotauxTotSemester2.Text = pointArrayGlobalVariable[35, 10];
                    xrTablePourcentageTotSemester2.Text = pointArrayGlobalVariable[36, 10];
                    xrTablePlaceTotSemester2.Text = pointArrayGlobalVariable[37, 10];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    xrTableReligionSemester2Tot.Text = pointArrayGlobalVariable[2, 10]; //Religion
                    stmarkSubject = xrTableReligionSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionSemester2Tot.ForeColor = Color.Red;
                        }
                    }


                    xrTableEdCivMoraleSemester2Tot.Text = pointArrayGlobalVariable[3, 10]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoraleSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoraleSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoraleSemester2Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaVieSemester2Tot.Text = pointArrayGlobalVariable[4, 10]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaVieSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaVieSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaVieSemester2Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueSemester2Tot.Text = pointArrayGlobalVariable[5, 10]; //Informatique (1)
                    stmarkSubject = xrTableInformatiqueSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueSemester2Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableMicrobiologieSemester2Tot.Text = pointArrayGlobalVariable[7, 10]; //Microbiologie (1)
                    stmarkSubject = xrTableMicrobiologieSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologieSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologieSemester2Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimieSemester2Tot.Text = pointArrayGlobalVariable[8, 10]; //Chimie
                    stmarkSubject = xrTableChimieSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimieSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimieSemester2Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiqueSemester2Tot.Text = pointArrayGlobalVariable[9, 10]; //Education Physique
                    stmarkSubject = xrTableEduPhysiqueSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiqueSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiqueSemester2Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieTotSemester2.Text = pointArrayGlobalVariable[10, 10]; //Géographie
                    stmarkSubject = xrTableGéographieTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableHistoireTotSemester2.Text = pointArrayGlobalVariable[11, 10]; //Histoire
                    stmarkSubject = xrTableHistoireTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoireTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoireTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesTotSemester2.Text = pointArrayGlobalVariable[12, 10]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiqueTotSemester2.Text = pointArrayGlobalVariable[13, 10]; //Physique
                    stmarkSubject = xrTablePhysiqueTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiqueTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiqueTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolTotSemester2.Text = pointArrayGlobalVariable[14, 10]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableAnglaisTotSemester2.Text = pointArrayGlobalVariable[16, 10]; //Anglais
                    stmarkSubject = xrTableAnglaisTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableGrecTotSemester2.Text = pointArrayGlobalVariable[17, 10]; //Grec
                    stmarkSubject = xrTableGrecTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2TotSemester2.Text = pointArrayGlobalVariable[18, 10]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2TotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2TotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2TotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableFrançaisTotSemester2.Text = pointArrayGlobalVariable[20, 10]; //Français
                    stmarkSubject = xrTableFrançaisTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinTotSemester2.Text = pointArrayGlobalVariable[21, 10]; //Latin
                    stmarkSubject = xrTableLatinTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    //Tot General
                    xrTableMaximaGenerauxTotGen.Text = pointArrayGlobalVariable[34, 12]; //MaximaGeneraux Periode;
                    xrTableTotauxTotGen.Text = pointArrayGlobalVariable[35, 12];
                    xrTablePourcentageTotGen.Text = pointArrayGlobalVariable[36, 12];
                    xrTablePlaceTotGen.Text = pointArrayGlobalVariable[37, 12];
                    // xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    //xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    int maximaTotGen1 = (Convert.ToInt16(xrTableMaxima1TotGen1.Text));

                    xrTableReligionTotGen.Text = pointArrayGlobalVariable[2, 12]; //Religion
                    stmarkSubject = xrTableReligionTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableEdCivMoraleTotGen.Text = pointArrayGlobalVariable[3, 12]; //Ed. Civ. & Morale
                    stmarkSubject = xrTableEdCivMoraleTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEdCivMoraleTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEdCivMoraleTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableEducationLaVieTotGen.Text = pointArrayGlobalVariable[4, 12]; //Education à la Vie
                    stmarkSubject = xrTableEducationLaVieTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationLaVieTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationLaVieTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueTotGen.Text = pointArrayGlobalVariable[5, 12]; //Informatique (1)
                    stmarkSubject = xrTableInformatiqueTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueTotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen2 = (Convert.ToInt16(xrTableMaxima2TotGen.Text));

                    xrTableMicrobiologieTotGen.Text = pointArrayGlobalVariable[7, 12]; //Microbiologie (1)
                    stmarkSubject = xrTableMicrobiologieTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMicrobiologieTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMicrobiologieTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableChimieTotGen.Text = pointArrayGlobalVariable[8, 12]; //Chimie
                    stmarkSubject = xrTableChimieTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableChimieTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableChimieTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableEduPhysiqueGenTot.Text = pointArrayGlobalVariable[9, 12]; //Education Physique
                    stmarkSubject = xrTableEduPhysiqueGenTot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEduPhysiqueGenTot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEduPhysiqueGenTot.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieTotGen.Text = pointArrayGlobalVariable[10, 12]; //Géographie
                    stmarkSubject = xrTableGéographieTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableHistoireTotGen.Text = pointArrayGlobalVariable[11, 12]; //Histoire
                    stmarkSubject = xrTableHistoireTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableHistoireTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableHistoireTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesTotGen.Text = pointArrayGlobalVariable[12, 12]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiquesTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTablePhysiqueTotGen.Text = pointArrayGlobalVariable[13, 12]; //Physique
                    stmarkSubject = xrTablePhysiqueTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePhysiqueTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePhysiqueTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableEcopolTotGen.Text = pointArrayGlobalVariable[14, 12]; //Sociologie / Ecopol (1)
                    stmarkSubject = xrTableEcopolTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEcopolTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEcopolTotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen3 = (Convert.ToInt16(xrTableMaxima3TotGen.Text));

                    xrTableAnglaisTotGen.Text = pointArrayGlobalVariable[16, 12]; //Anglais
                    stmarkSubject = xrTableAnglaisTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableGrecTotGen.Text = pointArrayGlobalVariable[17, 12]; //Grec
                    stmarkSubject = xrTableGrecTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGrecTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGrecTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiques2TotGen.Text = pointArrayGlobalVariable[18, 12]; //Mathématiques (1)
                    stmarkSubject = xrTableMathématiques2TotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiques2TotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiques2TotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen4 = (Convert.ToInt16(xrTableMaxima4TotGen.Text));

                    xrTableFrançaisTotGen.Text = pointArrayGlobalVariable[20, 12]; //Français
                    stmarkSubject = xrTableFrançaisTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableLatinTotGen.Text = pointArrayGlobalVariable[21, 12]; //Latin
                    stmarkSubject = xrTableLatinTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableLatinTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableLatinTotGen.ForeColor = Color.Red;
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
}
