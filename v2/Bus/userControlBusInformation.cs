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
using System.Resources;
using System.Data.SqlClient;

namespace EduXpress.Bus
{
    public partial class userControlBusInformation : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlBusInformation).Assembly);
        public userControlBusInformation()
        {
            InitializeComponent();
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GenerateBusNumber();
        }
        //Generate Bus Number
        string BusNumber;
        private void GenerateBusNumber()
        {
            DateTime moment = DateTime.Now;
            string year = moment.Year.ToString();
            try
            {
                BusNumber = GenerateID();                
                txtBusNo.Text = year + BusNumber;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GenerateID()
        {

            string value = "00";
            int IDindex = 0;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    // Fetch the latest ID from the database
                    con.Open();
                    cmd = new SqlCommand("SELECT TOP 1 StudentID FROM Students order BY StudentID DESC", con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        IDindex = Convert.ToInt16(rdr["StudentID"]);
                    }
                    rdr.Close();
                }

                IDindex++;
                // Because incrementing a string with an integer removes 0's
                // we need to replace them. If necessary.
                if (IDindex <= 9)
                {
                    value = "00" + value + IDindex.ToString();
                }
                else if (IDindex <= 99)
                {
                    value = "0" + value + IDindex.ToString();
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

                // If an error occurs, check the connection state and close it if necessary.
                if (con.State == ConnectionState.Open)
                    con.Close();
                value = "00";
            }
            return value;
        }
    }
}
