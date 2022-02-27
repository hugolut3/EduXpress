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
using System.Data.SqlClient;
using static EduXpress.Functions.PublicVariables;
using System.Resources;

namespace EduXpress.Students
{
    public partial class frmListClasses : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmListClasses).Assembly);


        public frmListClasses()
        {
            InitializeComponent();
        }

        //sql connection
        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(databaseConnectionString);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        //Fill dataGridViewEmails
        public DataView Getdata()
        {
            string CycleColumn = LocRM.GetString("strCycle").ToUpper();
            string SectionColumn = LocRM.GetString("strSection").ToUpper();
            string ClassColumn = LocRM.GetString("strClass").ToUpper();
            string SectionAbbreviationColumn = LocRM.GetString("strSectionAbbreviation").ToUpper();

            dynamic SelectQry = "SELECT  RTRIM(ClassName) as [" + ClassColumn + "], RTRIM(SectionName) as [" + SectionColumn + "], " +
                "RTRIM(SectionAbrev) as [" + SectionAbbreviationColumn + "], RTRIM(Cycle) as [" + CycleColumn + "] FROM Classes order by ClassName";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                SqlCommand SampleCommand = new SqlCommand();
                dynamic SampleDataAdapter = new SqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
                if ((Connection.State == ConnectionState.Open))
                {
                    Connection.Close();
                    Connection.Dispose();
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
            return TableView;
        }

        private void gridControlClasses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    className = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, LocRM.GetString("strClass").ToUpper()).ToString();
                    educationCycle = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, LocRM.GetString("strCycle").ToUpper()).ToString();
                    section = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, LocRM.GetString("strSection").ToUpper()).ToString();
                    Close();
                    Owner.Show();  //Show the previous form                    
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmListClasses_Load(object sender, EventArgs e)
        {
            gridControlClasses.DataSource = Getdata();
            fillCycle();
            fillSection();            
        }
        //Fill cmbcycle
        private void fillCycle()
        {
            comboCycle.Properties.Items.Clear();
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                comboCycle.Properties.Items.AddRange(new object[]
                            {
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") ,
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
                            });
            }
            else
            {
                comboCycle.Properties.Items.AddRange(new object[]
            {
                LocRM.GetString("strPrePrimary"),
                LocRM.GetString("strPreparatory") ,
                LocRM.GetString("strHighSchoolGET"),
                LocRM.GetString("strHighSchoolFET"),
                LocRM.GetString("strTVETCollege")
            });
            }
                
        }
        //Autocomplete Section
        private void fillSection()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
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
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SectionName) FROM Sections", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    comboSection.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        comboSection.Properties.Items.Add(drow[0].ToString());
                    }
                    comboSection.SelectedIndex = -1;
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
       

        private void comboSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboCycle.SelectedIndex = -1;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string CycleColumn = LocRM.GetString("strCycle").ToUpper();
                string SectionColumn = LocRM.GetString("strSection").ToUpper();
                string ClassColumn = LocRM.GetString("strClass").ToUpper();
                string SectionAbbreviationColumn = LocRM.GetString("strSectionAbbreviation").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT  RTRIM(ClassName) as [" + ClassColumn + "], RTRIM(SectionName) as [" + SectionColumn + "], " +
                    "RTRIM(SectionAbrev) as [" + SectionAbbreviationColumn + "], RTRIM(Cycle) as [" + CycleColumn + "] FROM Classes" +
                    " where SectionName=@d1 order by ClassName", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, "SectionName").Value = comboSection.Text;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "Classes");
                    gridControlClasses.DataSource = myDataSet.Tables["Classes"].DefaultView;

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

        private void comboCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSection.SelectedIndex = -1;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string CycleColumn = LocRM.GetString("strCycle").ToUpper();
                string SectionColumn = LocRM.GetString("strSection").ToUpper();
                string ClassColumn = LocRM.GetString("strClass").ToUpper();
                string SectionAbbreviationColumn = LocRM.GetString("strSectionAbbreviation").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT  RTRIM(ClassName) as [" + ClassColumn + "], RTRIM(SectionName) as [" + SectionColumn + "], " +
                    "RTRIM(SectionAbrev) as [" + SectionAbbreviationColumn + "], RTRIM(Cycle) as [" + CycleColumn + "] FROM Classes" +
                    " where Cycle=@d1 order by ClassName", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, "Cycle").Value = comboCycle.Text;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "Classes");
                    gridControlClasses.DataSource = myDataSet.Tables["Classes"].DefaultView;

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

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            gridControlClasses.DataSource = Getdata();
            comboCycle.SelectedIndex = -1;
            comboSection.SelectedIndex = -1;
        }
    }
}