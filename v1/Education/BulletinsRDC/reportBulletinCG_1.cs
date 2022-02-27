using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using static EduXpress.Functions.PublicVariables;
using EduXpress.Functions;
using System.Resources;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace EduXpress.Education.BulletinsRDC
{
    public partial class reportBulletinCG_1 : DevExpress.XtraReports.UI.XtraReport
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportBulletinCG_1).Assembly);
        public reportBulletinCG_1()
        {
            InitializeComponent();
        }

        private void reportBulletinCG_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

            if (isViewTemplateGlobalVariable == false)
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
                                xrlblPlace.ForeColor = Color.Blue;
                                xrlblDate.ForeColor = Color.Blue;
                                //string townCommune = "";

                                if (town != "")  //check if not null,or blank
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

                    //1e Period
                    //update from maxima generaux to conduite Periode 1.
                    xrTableMaximaGenerauxPeriode1.Text = pointArrayGlobalVariable[34, 0]; //Maxima Generaux Periode;
                    xrTableTotauxPeriode1.Text = pointArrayGlobalVariable[35, 0];
                    xrTablePourcentagePeriode1.Text = pointArrayGlobalVariable[36, 0];
                    xrTablePlacePeriode1.Text = pointArrayGlobalVariable[37, 0];
                    xrTableApplicationPeriode1.Text = pointArrayGlobalVariable[38, 0];
                    xrTableConduitePeriode1.Text = pointArrayGlobalVariable[39, 0];

                    //Populate points for 1e Period
                    int maxima1 = (Convert.ToInt16(xrTableMaxima1Periode1.Text));
                    int intmarkSubject = 0;
                    string stmarkSubject = "";
                    bool canConvert = false;
                    xrTableReligionPeriode1.Text = pointArrayGlobalVariable[2, 0]; //Religion
                    stmarkSubject = xrTableReligionPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableReligionPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableReligionPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableDroitPeriode1.Text = pointArrayGlobalVariable[3, 0]; //Droit
                    stmarkSubject = xrTableDroitPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitPeriode1.ForeColor = Color.Red;
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

                    xrTableEdCivMoralePeriode1.Text = pointArrayGlobalVariable[5, 0]; //Ed Civ Morale
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

                    xrTableEducationPhysiquePeriode1.Text = pointArrayGlobalVariable[6, 0]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiquePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiquePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiquePeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiquePeriode1.Text = pointArrayGlobalVariable[7, 0]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiquePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiquePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiquePeriode1.ForeColor = Color.Red;
                        }
                    }

                    int maxima2 = (Convert.ToInt16(xrTableMaxima2Periode1.Text));

                    xrTableCorrespondanceAnglPeriode1.Text = pointArrayGlobalVariable[9, 0]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglPeriode1.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancPeriode1.Text = pointArrayGlobalVariable[10, 0]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéPeriode1.Text = pointArrayGlobalVariable[11, 0]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresPeriode1.Text = pointArrayGlobalVariable[12, 0]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresPeriode1.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesPeriode1.Text = pointArrayGlobalVariable[13, 0]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesPeriode1.ForeColor = Color.Red;
                        }
                    }
                    
                    int maxima3 = (Convert.ToInt16(xrTableMaxima3Periode1.Text));

                    xrTableActivitéscomplVisitesGuidéesPeriode1.Text = pointArrayGlobalVariable[15, 0]; //Activités compl Visites Guidées
                    stmarkSubject = xrTableActivitéscomplVisitesGuidéesPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode1.ForeColor = Color.Red;
                        }
                    }
                    
                    int maxima4 = (Convert.ToInt16(xrTableMaxima4Periode1.Text));

                    xrTableAnglaisPeriode1.Text = pointArrayGlobalVariable[17, 0]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialePeriode1.Text = pointArrayGlobalVariable[18, 0]; //Documentation Commerciale
                    stmarkSubject = xrTableDocumentationCommercialePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialePeriode1.ForeColor = Color.Red;
                        }
                    }


                    int maxima5 = (Convert.ToInt16(xrTableMaxima5Periode1.Text));

                    xrTableFrançaisPeriode1.Text = pointArrayGlobalVariable[20, 0]; //Français
                    stmarkSubject = xrTableFrançaisPeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode1.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode1.Text = pointArrayGlobalVariable[21, 0]; //Informatique
                    stmarkSubject = xrTableInformatiquePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode1.ForeColor = Color.Red;
                        }
                    }

                    int maxima6 = (Convert.ToInt16(xrTableMaxima6Periode1.Text));

                    xrTableComptabilitéGénéralePeriode1.Text = pointArrayGlobalVariable[23, 0]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéralePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéralePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéralePeriode1.ForeColor = Color.Red;
                        }
                    }

                    int maxima7 = (Convert.ToInt16(xrTableMaxima7Periode1.Text));

                    xrTablePratiqueProfessionnellePeriode1.Text = pointArrayGlobalVariable[25, 0]; //Pratique Professionnelle
                    stmarkSubject = xrTablePratiqueProfessionnellePeriode1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima7;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePratiqueProfessionnellePeriode1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePratiqueProfessionnellePeriode1.ForeColor = Color.Red;
                        }
                    }


                    //Periode 2
                    //update from maxima Generaux to conduite Periode 2.
                    xrTableMaximaGenerauxPeriode2.Text = pointArrayGlobalVariable[34, 1]; //Maxima Generaux Periode;
                    xrTableTotauxPeriode2.Text = pointArrayGlobalVariable[35, 1];
                    xrTablePourcentagePeriode2.Text = pointArrayGlobalVariable[36, 1];
                    xrTablePlacePeriode2.Text = pointArrayGlobalVariable[37, 1];
                    xrTableApplicationPeriode2.Text = pointArrayGlobalVariable[38, 1];
                    xrTableConduitePeriode2.Text = pointArrayGlobalVariable[39, 1];

                    //Populate points for 2e Period
                    
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

                    xrTableDroitPeriode2.Text = pointArrayGlobalVariable[3, 1]; //Droit
                    stmarkSubject = xrTableDroitPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitPeriode2.ForeColor = Color.Red;
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

                    xrTableEdCivMoralePeriode2.Text = pointArrayGlobalVariable[5, 1]; //Ed Civ Morale
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

                    xrTableEducationPhysiquePeriode2.Text = pointArrayGlobalVariable[6, 1]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiquePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiquePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiquePeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiquePeriode2.Text = pointArrayGlobalVariable[7, 1]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiquePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiquePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiquePeriode2.ForeColor = Color.Red;
                        }
                    }



                    xrTableCorrespondanceAnglPeriode2.Text = pointArrayGlobalVariable[9, 1]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglPeriode2.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancPeriode2.Text = pointArrayGlobalVariable[10, 1]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéPeriode2.Text = pointArrayGlobalVariable[11, 1]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresPeriode2.Text = pointArrayGlobalVariable[12, 1]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresPeriode2.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesPeriode2.Text = pointArrayGlobalVariable[13, 1]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableActivitéscomplVisitesGuidéesPeriode2.Text = pointArrayGlobalVariable[15, 1]; //Activités compl Visites Guidées
                    stmarkSubject = xrTableActivitéscomplVisitesGuidéesPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode2.ForeColor = Color.Red;
                        }
                    }


                    xrTableAnglaisPeriode2.Text = pointArrayGlobalVariable[17, 1]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialePeriode2.Text = pointArrayGlobalVariable[18, 1]; //Latin
                    stmarkSubject = xrTableDocumentationCommercialePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialePeriode2.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableFrançaisPeriode2.Text = pointArrayGlobalVariable[20, 1]; //Français
                    stmarkSubject = xrTableFrançaisPeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode2.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode2.Text = pointArrayGlobalVariable[21, 1]; //Informatique
                    stmarkSubject = xrTableInformatiquePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode2.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableComptabilitéGénéralePeriode2.Text = pointArrayGlobalVariable[23, 1]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéralePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéralePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéralePeriode2.ForeColor = Color.Red;
                        }
                    }


                    xrTablePratiqueProfessionnellePeriode2.Text = pointArrayGlobalVariable[25, 1]; //Pratique Professionnelle
                    stmarkSubject = xrTablePratiqueProfessionnellePeriode2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima7;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePratiqueProfessionnellePeriode2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePratiqueProfessionnellePeriode2.ForeColor = Color.Red;
                        }
                    }


                    //Exam1
                    //update from maxima generaux to conduite Exam1.
                    xrTableMaximaGenerauxExamSemester1.Text = pointArrayGlobalVariable[34, 2]; //Maxima Generaux Periode;
                    xrTableTotauxExamSemester1.Text = pointArrayGlobalVariable[35, 2];
                    xrTablePourcentageExamSemester1.Text = pointArrayGlobalVariable[36, 2];
                    xrTablePlaceExamSemester1.Text = pointArrayGlobalVariable[37, 2];

                    //Populate points for Exam1
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

                    xrTableDroitSemester1Exam.Text = pointArrayGlobalVariable[3, 2]; //Droit
                    stmarkSubject = xrTableDroitSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitSemester1Exam.ForeColor = Color.Red;
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

                    xrTableEdCivMoraleSemester1Exam.Text = pointArrayGlobalVariable[5, 2]; //Ed Civ Morale
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

                    xrTableEducationPhysiqueSemester1Exam.Text = pointArrayGlobalVariable[6, 2]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiqueSemester1Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiqueSemester1Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiqueSemester1Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiqueExamSemester1.Text = pointArrayGlobalVariable[7, 2]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiqueExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiqueExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiqueExamSemester1.ForeColor = Color.Red;
                        }
                    }
                    
                   
                    int maximaEx2 = (Convert.ToInt16(xrTableMaxima2Exam1.Text));

                    xrTableCorrespondanceAnglExamSemester1.Text = pointArrayGlobalVariable[9, 2]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglExamSemester1.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancExamSemester1.Text = pointArrayGlobalVariable[10, 2]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéExamSemester1.Text = pointArrayGlobalVariable[11, 2]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresExamSemester1.Text = pointArrayGlobalVariable[12, 2]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresExamSemester1.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesExamSemester1.Text = pointArrayGlobalVariable[13, 2]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    

                    int maximaEx4 = (Convert.ToInt16(xrTableMaxima4Exam1.Text));

                    xrTableAnglaisExamSemester1.Text = pointArrayGlobalVariable[17, 2]; //Anglais
                    stmarkSubject = xrTableAnglaisExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialeExamSemester1.Text = pointArrayGlobalVariable[18, 2]; //Latin
                    stmarkSubject = xrTableDocumentationCommercialeExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialeExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialeExamSemester1.ForeColor = Color.Red;
                        }
                    }


                    int maximaEx5 = (Convert.ToInt16(xrTableMaxima5Exam1.Text));

                    xrTableFrançaisExamSemester1.Text = pointArrayGlobalVariable[20, 2]; //Français
                    stmarkSubject = xrTableFrançaisExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueExamSemester1.Text = pointArrayGlobalVariable[21, 2]; //Informatique
                    stmarkSubject = xrTableInformatiqueExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueExamSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaEx6 = (Convert.ToInt16(xrTableMaxima6Exam1.Text));

                    xrTableComptabilitéGénéraleExamSemester1.Text = pointArrayGlobalVariable[23, 2]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéraleExamSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéraleExamSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéraleExamSemester1.ForeColor = Color.Red;
                        }
                    }


                    //3e Period
                    //update from maxima generaux to conduite Periode 3.
                    xrTableMaximaGenerauxPeriode3.Text = pointArrayGlobalVariable[34, 3]; //Maxima Generaux Periode;
                    xrTableTotauxPeriode3.Text = pointArrayGlobalVariable[35, 3];
                    xrTablePourcentagePeriode3.Text = pointArrayGlobalVariable[36, 3];
                    xrTablePlacePeriode3.Text = pointArrayGlobalVariable[37, 3];
                    xrTableApplicationPeriode3.Text = pointArrayGlobalVariable[38, 3];
                    xrTableConduitePeriode3.Text = pointArrayGlobalVariable[39, 3];

                    //Populate points for 3e Period                    
                    xrTableReligionPeriode3.Text = pointArrayGlobalVariable[2,3]; //Religion
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

                    xrTableDroitPeriode3.Text = pointArrayGlobalVariable[3, 3]; //Droit
                    stmarkSubject = xrTableDroitPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitPeriode3.ForeColor = Color.Red;
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

                    xrTableEdCivMoralePeriode3.Text = pointArrayGlobalVariable[5, 3]; //Ed Civ Morale
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

                    xrTableEducationPhysiquePeriode3.Text = pointArrayGlobalVariable[6, 3]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiquePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiquePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiquePeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiquePeriode3.Text = pointArrayGlobalVariable[7, 3]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiquePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiquePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiquePeriode3.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceAnglPeriode3.Text = pointArrayGlobalVariable[9, 3]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglPeriode3.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancPeriode3.Text = pointArrayGlobalVariable[10, 3]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéPeriode3.Text = pointArrayGlobalVariable[11, 3]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresPeriode3.Text = pointArrayGlobalVariable[12, 3]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresPeriode3.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesPeriode3.Text = pointArrayGlobalVariable[13, 3]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesPeriode3.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableActivitéscomplVisitesGuidéesPeriode3.Text = pointArrayGlobalVariable[15, 3]; //Activités compl Visites Guidées
                    stmarkSubject = xrTableActivitéscomplVisitesGuidéesPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode3.ForeColor = Color.Red;
                        }
                    }

                   
                    xrTableAnglaisPeriode3.Text = pointArrayGlobalVariable[17, 3]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialePeriode3.Text = pointArrayGlobalVariable[18, 3]; //Latin
                    stmarkSubject = xrTableDocumentationCommercialePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialePeriode3.ForeColor = Color.Red;
                        }
                    }


                    
                    xrTableFrançaisPeriode3.Text = pointArrayGlobalVariable[20, 3]; //Français
                    stmarkSubject = xrTableFrançaisPeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode3.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode3.Text = pointArrayGlobalVariable[21, 3]; //Informatique
                    stmarkSubject = xrTableInformatiquePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode3.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableComptabilitéGénéralePeriode3.Text = pointArrayGlobalVariable[23, 3]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéralePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéralePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéralePeriode3.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTablePratiqueProfessionnellePeriode3.Text = pointArrayGlobalVariable[25, 3]; //Pratique Professionnelle
                    stmarkSubject = xrTablePratiqueProfessionnellePeriode3.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima7;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePratiqueProfessionnellePeriode3.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePratiqueProfessionnellePeriode3.ForeColor = Color.Red;
                        }
                    }


                    //4e Period
                    //update from maxima generaux to conduite Periode 4.
                    xrTableMaximaGenerauxPeriode4.Text = pointArrayGlobalVariable[34, 4]; //Maxima Generaux Periode;
                    xrTableTotauxPeriode4.Text = pointArrayGlobalVariable[35, 4];
                    xrTablePourcentagePeriode4.Text = pointArrayGlobalVariable[36, 4];
                    xrTablePlacePeriode4.Text = pointArrayGlobalVariable[37, 4];
                    xrTableApplicationPeriode4.Text = pointArrayGlobalVariable[38, 4];
                    xrTableConduitePeriode4.Text = pointArrayGlobalVariable[39, 4];

                    //Populate points for 4 e Period                    
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

                    xrTableDroitPeriode4.Text = pointArrayGlobalVariable[3, 4]; //Droit
                    stmarkSubject = xrTableDroitPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitPeriode4.ForeColor = Color.Red;
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

                    xrTableEdCivMoralePeriode4.Text = pointArrayGlobalVariable[5, 4]; //Ed Civ Morale
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

                    xrTableEducationPhysiquePeriode4.Text = pointArrayGlobalVariable[6, 4]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiquePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiquePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiquePeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiquePeriode4.Text = pointArrayGlobalVariable[7, 4]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiquePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiquePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiquePeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceAnglPeriode4.Text = pointArrayGlobalVariable[9, 4]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglPeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancPeriode4.Text = pointArrayGlobalVariable[10, 4]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéPeriode4.Text = pointArrayGlobalVariable[11, 4]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresPeriode4.Text = pointArrayGlobalVariable[12, 4]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresPeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesPeriode4.Text = pointArrayGlobalVariable[13, 4]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesPeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableActivitéscomplVisitesGuidéesPeriode4.Text = pointArrayGlobalVariable[15, 4]; //Activités compl Visites Guidées
                    stmarkSubject = xrTableActivitéscomplVisitesGuidéesPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableActivitéscomplVisitesGuidéesPeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableAnglaisPeriode4.Text = pointArrayGlobalVariable[17, 4]; //Anglais
                    stmarkSubject = xrTableAnglaisPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialePeriode4.Text = pointArrayGlobalVariable[18,4]; //Latin
                    stmarkSubject = xrTableDocumentationCommercialePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialePeriode4.ForeColor = Color.Red;
                        }
                    }



                    xrTableFrançaisPeriode4.Text = pointArrayGlobalVariable[20, 4]; //Français
                    stmarkSubject = xrTableFrançaisPeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisPeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisPeriode4.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiquePeriode4.Text = pointArrayGlobalVariable[21, 4]; //Informatique
                    stmarkSubject = xrTableInformatiquePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiquePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiquePeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTableComptabilitéGénéralePeriode4.Text = pointArrayGlobalVariable[23, 4]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéralePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéralePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéralePeriode4.ForeColor = Color.Red;
                        }
                    }


                    xrTablePratiqueProfessionnellePeriode4.Text = pointArrayGlobalVariable[25, 4]; //Pratique Professionnelle
                    stmarkSubject = xrTablePratiqueProfessionnellePeriode4.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maxima7;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePratiqueProfessionnellePeriode4.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePratiqueProfessionnellePeriode4.ForeColor = Color.Red;
                        }
                    }


                    //Exam2
                    //update from maxima generaux to conduite Exam2.
                    xrTableMaximaGenerauxExamSemester2.Text = pointArrayGlobalVariable[34, 5]; //Maxima Generaux Periode;
                    xrTableTotauxExamSemester2.Text = pointArrayGlobalVariable[35, 5];
                    xrTablePourcentageExamSemester2.Text = pointArrayGlobalVariable[36, 5];
                    xrTablePlaceExamSemester2.Text = pointArrayGlobalVariable[37, 5];

                    //Populate points for Exam2
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

                    xrTableDroitExamSemester2.Text = pointArrayGlobalVariable[3, 5]; //Droit
                    stmarkSubject = xrTableDroitExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitExamSemester2.ForeColor = Color.Red;
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

                    xrTableEdCivMoraleSemester2Exam.Text = pointArrayGlobalVariable[5, 5]; //Ed Civ Morale
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

                    xrTableEducationPhysiqueSemester2Exam.Text = pointArrayGlobalVariable[6, 5]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiqueSemester2Exam.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiqueSemester2Exam.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiqueSemester2Exam.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiqueExamSemester2.Text = pointArrayGlobalVariable[7, 5]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiqueExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiqueExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiqueExamSemester2.ForeColor = Color.Red;
                        }
                    }

                                        
                    xrTableCorrespondanceAnglExamSemester2.Text = pointArrayGlobalVariable[9, 5]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglExamSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancExamSemester2.Text = pointArrayGlobalVariable[10, 5]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéExamSemester2.Text = pointArrayGlobalVariable[11, 5]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresExamSemester2.Text = pointArrayGlobalVariable[12, 5]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresExamSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesExamSemester2.Text = pointArrayGlobalVariable[13, 5]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesExamSemester2.ForeColor = Color.Red;
                        }
                    }




                    xrTableAnglaisExamSemester2.Text = pointArrayGlobalVariable[17, 5]; //Anglais
                    stmarkSubject = xrTableAnglaisExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialeExamSemester2.Text = pointArrayGlobalVariable[18, 5]; //Latin
                    stmarkSubject = xrTableDocumentationCommercialeExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialeExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialeExamSemester2.ForeColor = Color.Red;
                        }
                    }



                    xrTableFrançaisExamSemester2.Text = pointArrayGlobalVariable[20, 5]; //Français
                    stmarkSubject = xrTableFrançaisExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisExamSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueExamSemester2.Text = pointArrayGlobalVariable[21, 5]; //Informatique
                    stmarkSubject = xrTableInformatiqueExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueExamSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableComptabilitéGénéraleExamSemester2.Text = pointArrayGlobalVariable[23, 5]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéraleExamSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaEx6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéraleExamSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéraleExamSemester2.ForeColor = Color.Red;
                        }
                    }



                    //Tot semester 1
                    //update from maxima generaux to conduite Tot semester 1.
                    xrTableMaximaGenerauxTotSemester1.Text = pointArrayGlobalVariable[34, 9]; //Maxima Generaux Periode;
                    xrTableTotauxTotSemester1.Text = pointArrayGlobalVariable[35, 9];
                    xrTablePourcentageTotSemester1.Text = pointArrayGlobalVariable[36, 9];
                    xrTablePlaceTotSemester1.Text = pointArrayGlobalVariable[37, 9];                    

                    //Populate points for 1e Period
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

                    xrTableDroitSemester1Tot.Text = pointArrayGlobalVariable[3, 9]; //Droit
                    stmarkSubject = xrTableDroitSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitSemester1Tot.ForeColor = Color.Red;
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

                    xrTableEdCivMoraleSemester1Tot.Text = pointArrayGlobalVariable[5, 9]; //Ed Civ Morale
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

                    xrTableEducationPhysiqueSemester1Tot.Text = pointArrayGlobalVariable[6, 9]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiqueSemester1Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiqueSemester1Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiqueSemester1Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiqueTotSemester1.Text = pointArrayGlobalVariable[7, 9]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiqueTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiqueTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiqueTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot2 = (Convert.ToInt16(xrTableMaxima2Tot1.Text));

                    xrTableCorrespondanceAnglTotSemester1.Text = pointArrayGlobalVariable[9, 9]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglTotSemester1.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancTotSemester1.Text = pointArrayGlobalVariable[10, 9]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéTotSemester1.Text = pointArrayGlobalVariable[11, 9]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresTotSemester1.Text = pointArrayGlobalVariable[12, 9]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresTotSemester1.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesTotSemester1.Text = pointArrayGlobalVariable[13, 9]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot3 = (Convert.ToInt16(xrTableMaxima3Tot1.Text));

                    xrTableActivitéscomplVisitesGuidéesTotSemester1.Text = pointArrayGlobalVariable[15, 9]; //Activités compl Visites Guidées
                    stmarkSubject = xrTableActivitéscomplVisitesGuidéesTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableActivitéscomplVisitesGuidéesTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableActivitéscomplVisitesGuidéesTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot4 = (Convert.ToInt16(xrTableMaxima4Tot1.Text));

                    xrTableAnglaisTotSemester1.Text = pointArrayGlobalVariable[17, 9]; //Anglais
                    stmarkSubject = xrTableAnglaisTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialeTotSemester1.Text = pointArrayGlobalVariable[18, 9]; //Documentation Commerciale
                    stmarkSubject = xrTableDocumentationCommercialeTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialeTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialeTotSemester1.ForeColor = Color.Red;
                        }
                    }


                    int maximaTot5 = (Convert.ToInt16(xrTableMaxima5Tot1.Text));

                    xrTableFrançaisTotSemester1.Text = pointArrayGlobalVariable[20, 9]; //Français
                    stmarkSubject = xrTableFrançaisTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueTotSemester1.Text = pointArrayGlobalVariable[21, 9]; //Informatique
                    stmarkSubject = xrTableInformatiqueTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot6 = (Convert.ToInt16(xrTableMaxima6Tot1.Text));

                    xrTableComptabilitéGénéraleTotSemester1.Text = pointArrayGlobalVariable[23, 9]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéraleTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéraleTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéraleTotSemester1.ForeColor = Color.Red;
                        }
                    }

                    int maximaTot7 = (Convert.ToInt16(xrTableMaxima7Tot1.Text));

                    xrTablePratiqueProfessionnelleTotSemester1.Text = pointArrayGlobalVariable[25, 9]; //Pratique Professionnelle
                    stmarkSubject = xrTablePratiqueProfessionnelleTotSemester1.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot7;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePratiqueProfessionnelleTotSemester1.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePratiqueProfessionnelleTotSemester1.ForeColor = Color.Red;
                        }
                    }



                    //Tot semester 2
                    //update from maxima generaux to conduite Tot semester 2.
                    xrTableMaximaGenerauxTotSemester2.Text = pointArrayGlobalVariable[34, 10]; //Maxima Generaux Periode;
                    xrTableTotauxTotSemester2.Text = pointArrayGlobalVariable[35, 10];
                    xrTablePourcentageTotSemester2.Text = pointArrayGlobalVariable[36, 10];
                    xrTablePlaceTotSemester2.Text = pointArrayGlobalVariable[37, 10];

                    //Populate points for Tot semester 2                   

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

                    xrTableDroitSemester2Tot.Text = pointArrayGlobalVariable[3, 10]; //Droit
                    stmarkSubject = xrTableDroitSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitSemester2Tot.ForeColor = Color.Red;
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

                    xrTableEdCivMoraleSemester2Tot.Text = pointArrayGlobalVariable[5, 10]; //Ed Civ Morale
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

                    xrTableEducationPhysiqueSemester2Tot.Text = pointArrayGlobalVariable[6, 10]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiqueSemester2Tot.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiqueSemester2Tot.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiqueSemester2Tot.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiqueTotSemester2.Text = pointArrayGlobalVariable[7, 10]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiqueTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiqueTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiqueTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableCorrespondanceAnglTotSemester2.Text = pointArrayGlobalVariable[9, 10]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancTotSemester2.Text = pointArrayGlobalVariable[10, 10]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéTotSemester2.Text = pointArrayGlobalVariable[11, 10]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresTotSemester2.Text = pointArrayGlobalVariable[12, 10]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesTotSemester2.Text = pointArrayGlobalVariable[13, 10]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableActivitéscomplVisitesGuidéesTotSemester2.Text = pointArrayGlobalVariable[15, 10]; //Activités compl Visites Guidées
                    stmarkSubject = xrTableActivitéscomplVisitesGuidéesTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableActivitéscomplVisitesGuidéesTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableActivitéscomplVisitesGuidéesTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableAnglaisTotSemester2.Text = pointArrayGlobalVariable[17, 10]; //Anglais
                    stmarkSubject = xrTableAnglaisTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialeTotSemester2.Text = pointArrayGlobalVariable[18, 10]; //Documentation Commerciale
                    stmarkSubject = xrTableDocumentationCommercialeTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialeTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialeTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    
                    xrTableFrançaisTotSemester2.Text = pointArrayGlobalVariable[20, 10]; //Français
                    stmarkSubject = xrTableFrançaisTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueTotSemester2.Text = pointArrayGlobalVariable[21, 10]; //Informatique
                    stmarkSubject = xrTableInformatiqueTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTableComptabilitéGénéraleTotSemester2.Text = pointArrayGlobalVariable[23, 10]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéraleTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéraleTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéraleTotSemester2.ForeColor = Color.Red;
                        }
                    }

                    
                    xrTablePratiqueProfessionnelleTotSemester2.Text = pointArrayGlobalVariable[25, 10]; //Pratique Professionnelle
                    stmarkSubject = xrTablePratiqueProfessionnelleTotSemester2.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTot7;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePratiqueProfessionnelleTotSemester2.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePratiqueProfessionnelleTotSemester2.ForeColor = Color.Red;
                        }
                    }


                    //Tot General
                    //update from maxima generaux to conduite Tot General.
                    xrTableMaximaGenerauxTotGen.Text = pointArrayGlobalVariable[34, 12]; //Maxima Generaux Tot General;
                    xrTableTotauxTotGen.Text = pointArrayGlobalVariable[35, 12];
                    xrTablePourcentageTotGen.Text = pointArrayGlobalVariable[36, 12];
                    xrTablePlaceTotGen.Text = pointArrayGlobalVariable[37, 12];

                    //Populate points for Tot General
                    int maximaTotGen1 = (Convert.ToInt16(xrTableMaxima1TotGen.Text));

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

                    xrTableDroitTotGen.Text = pointArrayGlobalVariable[3, 12]; //Droit
                    stmarkSubject = xrTableDroitTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDroitTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDroitTotGen.ForeColor = Color.Red;
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

                    xrTableEdCivMoraleTotGen.Text = pointArrayGlobalVariable[5, 12]; //Ed Civ Morale
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

                    xrTableEducationPhysiqueTotGen.Text = pointArrayGlobalVariable[6, 12]; //Education Physique
                    stmarkSubject = xrTableEducationPhysiqueTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableEducationPhysiqueTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableEducationPhysiqueTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableGéographieEconomiqueTotGen.Text = pointArrayGlobalVariable[7, 12]; //Géographie Economique
                    stmarkSubject = xrTableGéographieEconomiqueTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen1;

                        if (intmarkSubject >= 50)
                        {
                            xrTableGéographieEconomiqueTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableGéographieEconomiqueTotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen2 = (Convert.ToInt16(xrTableMaxima2TotGen.Text));

                    xrTableCorrespondanceAnglTotGen.Text = pointArrayGlobalVariable[9, 12]; //Correspondance Angl                    
                    stmarkSubject = xrTableCorrespondanceAnglTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceAnglTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceAnglTotGen.ForeColor = Color.Red;
                        }
                    }


                    xrTableCorrespondanceFrancTotGen.Text = pointArrayGlobalVariable[10, 12]; //Correspondance Franc
                    stmarkSubject = xrTableCorrespondanceFrancTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableCorrespondanceFrancTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableCorrespondanceFrancTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableFiscalitéTotGen.Text = pointArrayGlobalVariable[11, 12]; //Fiscalité
                    stmarkSubject = xrTableFiscalitéTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFiscalitéTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFiscalitéTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableMathématiquesFinancièresTotGen.Text = pointArrayGlobalVariable[12, 12]; //Mathématiques Financières
                    stmarkSubject = xrTableMathématiquesFinancièresTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesFinancièresTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesFinancièresTotGen.ForeColor = Color.Red;
                        }
                    }


                    xrTableMathématiquesGénéralesTotGen.Text = pointArrayGlobalVariable[13, 12]; //Mathématiques Générales
                    stmarkSubject = xrTableMathématiquesGénéralesTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen2;

                        if (intmarkSubject >= 50)
                        {
                            xrTableMathématiquesGénéralesTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableMathématiquesGénéralesTotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen3 = (Convert.ToInt16(xrTableMaxima3TotGen.Text));

                    xrTableActivitéscomplVisitesGuidéesTotGen.Text = pointArrayGlobalVariable[15, 12]; //Activités compl Visites Guidées
                    stmarkSubject = xrTableActivitéscomplVisitesGuidéesTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen3;

                        if (intmarkSubject >= 50)
                        {
                            xrTableActivitéscomplVisitesGuidéesTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableActivitéscomplVisitesGuidéesTotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen4 = (Convert.ToInt16(xrTableMaxima4TotGen.Text));

                    xrTableAnglaisTotGen.Text = pointArrayGlobalVariable[17, 12]; //Anglais
                    stmarkSubject = xrTableAnglaisTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableAnglaisTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableAnglaisTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableDocumentationCommercialeTotGen.Text = pointArrayGlobalVariable[18, 12]; //Documentation Commerciale
                    stmarkSubject = xrTableDocumentationCommercialeTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen4;

                        if (intmarkSubject >= 50)
                        {
                            xrTableDocumentationCommercialeTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableDocumentationCommercialeTotGen.ForeColor = Color.Red;
                        }
                    }


                    int maximaTotGen5 = (Convert.ToInt16(xrTableMaxima5TotGen.Text));

                    xrTableFrançaisTotGen.Text = pointArrayGlobalVariable[20, 12]; //Français
                    stmarkSubject = xrTableFrançaisTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableFrançaisTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableFrançaisTotGen.ForeColor = Color.Red;
                        }
                    }

                    xrTableInformatiqueTotGen.Text = pointArrayGlobalVariable[21, 12]; //Informatique
                    stmarkSubject = xrTableInformatiqueTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen5;

                        if (intmarkSubject >= 50)
                        {
                            xrTableInformatiqueTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableInformatiqueTotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen6 = (Convert.ToInt16(xrTableMaxima6TotGen.Text));

                    xrTableComptabilitéGénéraleTotGen.Text = pointArrayGlobalVariable[23, 12]; //Comptabilité Générale
                    stmarkSubject = xrTableComptabilitéGénéraleTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen6;

                        if (intmarkSubject >= 50)
                        {
                            xrTableComptabilitéGénéraleTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTableComptabilitéGénéraleTotGen.ForeColor = Color.Red;
                        }
                    }

                    int maximaTotGen7 = (Convert.ToInt16(xrTableMaxima7TotGen.Text));

                    xrTablePratiqueProfessionnelleTotGen.Text = pointArrayGlobalVariable[25, 12]; //Pratique Professionnelle
                    stmarkSubject = xrTablePratiqueProfessionnelleTotGen.Text;
                    canConvert = int.TryParse(stmarkSubject, out intmarkSubject);
                    if (canConvert == true)
                    {
                        intmarkSubject = (intmarkSubject * 100) / maximaTotGen7;

                        if (intmarkSubject >= 50)
                        {
                            xrTablePratiqueProfessionnelleTotGen.ForeColor = Color.Blue;
                        }
                        else
                        {
                            xrTablePratiqueProfessionnelleTotGen.ForeColor = Color.Red;
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
