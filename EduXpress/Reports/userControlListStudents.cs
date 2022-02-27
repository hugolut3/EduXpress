using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Spreadsheet;
using System.Data.SqlClient;
using System.Resources;
using System.Globalization;

namespace EduXpress.Reports
{
    public partial class userControlListStudents : DevExpress.XtraEditors.XtraUserControl
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlListStudents).Assembly);
        public userControlListStudents()
        {
            InitializeComponent();
        }

        private void btnLoadStudents_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            // Populate the "Students" data table with data.
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                                
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT * FROM Students", con);
                    ds = new DataSet();
                    adp.Fill(ds, "Students");
                    dtable = ds.Tables[0];

                    IWorkbook workbook = spreadsheetControl1.Document;
                    // Load the template document into the SpreadsheetControl. 
                    //Load the  template based on language
                    string templatePath = Application.StartupPath + @"\Reports\Template";
                    if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
                    {
                        workbook.LoadDocument(templatePath + @"\EduXpressData_fr.xlsx");
                        //workbook.LoadDocument(@"Reports\Template\EduXpressData_fr.xlsx");
                    }
                    else
                    {
                        workbook.LoadDocument(templatePath + @"\EduXpressData_en.xlsx");
                        //workbook.LoadDocument(@"Reports\Template\EduXpressData_en.xlsx");
                    }
                    Worksheet sheet = workbook.Worksheets[0];

                    sheet.DataBindings.BindToDataSource(dtable, 3, 0);
                    con.Close();
                }
                    

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

            }
            catch (Exception ex)
            {
                 if (splashScreenManager1.IsSplashFormVisible == true)
                 {
                     splashScreenManager1.CloseWaitForm();
                  }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}
    }
}
