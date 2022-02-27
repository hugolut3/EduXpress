using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Resources;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace EduXpress.Students
{
    public partial class frmImportStudents : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmImportStudents).Assembly);

        public frmImportStudents()
        {
            InitializeComponent();
        }
        string importPath = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            XtraOpenFileDialog OpenFile = new XtraOpenFileDialog();

            try
            {
                OpenFile.FileName = "";
                OpenFile.Title = LocRM.GetString("strImportStudentsFromExcel") + ": ";
                OpenFile.Filter = LocRM.GetString("strExcelWorkbook") + ": " + "(*.xls)|*.xls";
                DialogResult res = OpenFile.ShowDialog();
                if (res == DialogResult.OK)
                {
                   txtImportPath.Text = OpenFile.FileName.ToString();
                    importPath = txtImportPath.Text;
                    btnImport.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void InsertExcelRecords()
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            if (splashScreenManager1.IsSplashFormVisible == false)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strImporting"));
            }

            try
            {
                //create excel connection string

                //microsoft.jet.oledb.4.0 uses Excel 197-2003, Microsoft.ACE.OLEDB.12.0 is for new excel 2007
                // "HDR=Yes;" indicates that the first row contains columnnames, not data. "HDR=No;" indicates the opposite.
                // "IMEX=1;" tells the driver to always read "intermixed"(numbers, dates, strings etc) data columns as text.Note that this option might affect excel sheet write access negative.
                string constr = string.Format(@"provider=microsoft.jet.oledb.4.0;data source=" + importPath +
                   ";extended properties=" + "\"excel 8.0;hdr=yes;\"");

                //string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", _path);
                OleDbConnection Econ = new OleDbConnection(constr);
                
                //string Query = string.Format("select [Numéro d'élève],[Option], [Classe], [Nom de l'élève],[Postnom et prenom de l'élève]," +
                //    "[Sexe],[Age],[Pays de nationalité], [Langue parlée à la maison], [Religion], [Nom du père],[Postnom et prenom du père], " +
                //    "[Numéro de téléphone du père], [Adresse e-mail du père], [Numéro de téléphone de notification]," +
                //    "[Adresse e-mail de notification],[Nom de la mère],[Postnom et prenom de la mère], [Numéro de téléphone de la mère], " +
                //    "[Adresse e-mail de la mère], [Adresse du domicile], [Numéro de téléphone en cas d'urgence, absence ou maladie], " +
                //   "[Numéro de téléphone alternatif en cas d'urgence, absence ou maladie], [Année scolaire], [Section], " +
                //   "[Profession du père], [Profession de la mère],[No ID de l'élève], [No Perm de l'élève], [Né(e) à]" +
                //   " from [Sheet1$]");
                string Query = string.Format("select * from [Sheet1$]");
                OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                Econ.Open();

                DataSet ds = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
                Econ.Close();//close Excel connection after adding to data set  
                oda.Fill(ds);
                DataTable Exceldt = ds.Tables[0]; //copy data set to datatable

                //If these 5 columns which are required are blank, remove the row.
                for (int i = Exceldt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Exceldt.Rows[i]["Numéro d'élève"] == DBNull.Value ||
                        Exceldt.Rows[i]["Nom de l'élève"] == DBNull.Value ||
                        Exceldt.Rows[i]["Section"] == DBNull.Value        ||
                        Exceldt.Rows[i]["Classe"] == DBNull.Value         ||
                        Exceldt.Rows[i]["Postnom et prenom de l'élève"] == DBNull.Value)
                    {
                        Exceldt.Rows[i].Delete();
                    }
                }
                Exceldt.AcceptChanges();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    // creating object of SqlBulkCopy
                    SqlBulkCopy objbulk = new SqlBulkCopy(con);
                    //assigning Destination table name      
                    objbulk.DestinationTableName = "Students";

                    //Mapping Table column   
                   
                    objbulk.ColumnMappings.Add("[Numéro d'élève]", "StudentNumber");
                    objbulk.ColumnMappings.Add("[Option]", "Section");
                    objbulk.ColumnMappings.Add("[Classe]", "Class");
                    objbulk.ColumnMappings.Add("[Nom de l'élève]", "StudentSurname");
                    objbulk.ColumnMappings.Add("[Postnom et prenom de l'élève]", "StudentFirstNames");
                    objbulk.ColumnMappings.Add("[Sexe]", "Gender");
                    objbulk.ColumnMappings.Add("[Age]", "Age");
                    objbulk.ColumnMappings.Add("[Pays de nationalité]", "Nationality");
                    objbulk.ColumnMappings.Add("[Langue parlée à la maison]", "HomeLanguage");
                    objbulk.ColumnMappings.Add("[Religion]", "Religion");
                    objbulk.ColumnMappings.Add("[Nom du père]", "FatherSurname");
                    objbulk.ColumnMappings.Add("[Postnom et prenom du père]", "FatherNames");
                    objbulk.ColumnMappings.Add("[Numéro de téléphone du père]", "FatherContactNo");
                    objbulk.ColumnMappings.Add("[Adresse e-mail du père]", "FatherEmail");
                    objbulk.ColumnMappings.Add("[Numéro de téléphone de notification]", "NotificationNo");
                    objbulk.ColumnMappings.Add("[Adresse e-mail de notification]", "NotificationEmail");
                    objbulk.ColumnMappings.Add("[Nom de la mère]", "MotherSurname");
                    objbulk.ColumnMappings.Add("[Postnom et prenom de la mère]", "MotherNames");
                    objbulk.ColumnMappings.Add("[Numéro de téléphone de la mère]", "MotherContactNo");
                    objbulk.ColumnMappings.Add("[Adresse e-mail de la mère]", "MotherEmail");
                    objbulk.ColumnMappings.Add("[Adresse du domicile]", "HomeAddress");
                    objbulk.ColumnMappings.Add("[Numéro de téléphone en cas d'urgence ou absence]", "EmergencyPhoneNo");
                    objbulk.ColumnMappings.Add("[Numéro de téléphone alternatif en cas d'urgence ou absence]", "AbsencePhoneNo");
                    objbulk.ColumnMappings.Add("[Année scolaire]", "SchoolYear");
                    objbulk.ColumnMappings.Add("[Section]", "Cycle");
                    objbulk.ColumnMappings.Add("[Profession du père]", "FatherProfession");
                    objbulk.ColumnMappings.Add("[Profession de la mère]", "MotherProfession");
                    objbulk.ColumnMappings.Add("[No ID de l'élève]", "NoID");
                    objbulk.ColumnMappings.Add("[No Perm de l'élève]", "NoPerm");
                    objbulk.ColumnMappings.Add("[Né(e) à]", "BornAt");

                    objbulk.WriteToServer(Exceldt); //inserting Datatable Records to DataBase 

                    //Close DataBase conection 
                    con.Close();

                }
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                MessageBox.Show(LocRM.GetString("strDataImportedSuccessfully") , LocRM.GetString("strImportStudentsExcel"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnImport.Enabled = false;
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                MessageBox.Show(ex.ToString());  //handle exception   
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            InsertExcelRecords();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        
    }
    

}

