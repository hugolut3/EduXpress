using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Resources;
using static EduXpress.Functions.PublicVariables;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;

namespace EduXpress.Students
{
    public partial class reportInvoiceSticker40_30mm : DevExpress.XtraReports.UI.XtraReport
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportInvoiceSticker40_30mm).Assembly);

        public reportInvoiceSticker40_30mm()
        {
            InitializeComponent();
        }

        private void populateForm()
        {
            try
            {
                //display cashier name               
                // xrLabelCashier.Text = xrLabelCashier.Text + UserLoggedSurname + " " + UserLoggedName;
                //display student and fee info
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = con.CreateCommand();

                    cmd.CommandText = "SELECT * FROM Students, CourseFeePayment  WHERE Students.StudentNumber=CourseFeePayment.StudentNumber and PaymentID = '" + receiptNumber + "' ";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        xrLabelName.Text = xrLabelName.Text + (rdr.GetString(5).Trim()).ToUpper() + " " + (rdr.GetString(6).Trim()).ToUpper();//Surname and name
                        xrLabelClass.Text = xrLabelClass.Text + (rdr.GetString(4).Trim());
                        DateTime dt = (DateTime)rdr["PaymentDate"];
                        xrLabelPaymentDate.Text = xrLabelPaymentDate.Text + dt.ToString("dd/MM/yyyy");

                        if (Properties.Settings.Default.CurrencySymbolPositionBefore == true)
                        {
                            //xrLabelTotalPaid.Text = xrLabelTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + (rdr.GetDecimal(54).ToString());                           
                            xrLabelTotalPaid.Text = xrLabelTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + rdr["TotalPaid"].ToString();
                        }
                        else
                        {
                           // xrLabelTotalPaid.Text = xrLabelTotalPaid.Text + (rdr.GetDecimal(54).ToString()) + Properties.Settings.Default.CurrencySymbol;
                            xrLabelTotalPaid.Text = xrLabelTotalPaid.Text + rdr["TotalPaid"].ToString() + Properties.Settings.Default.CurrencySymbol;
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

                //Get month and fee name 
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string sql = "Select RTRIM(Month),RTRIM(FeeName),CourseFeePayment_Join.Fee from CourseFeePayment, CourseFeePayment_Join  where  CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and CourseFeePayment.CourseFeePaymentID= @d1 ";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@d1", payemntID);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    int index = 0;
                    string monthName = LocRM.GetString("strPeriod").ToUpper() + ": ";
                    while ((rdr.Read() == true))
                    {
                        index = index + 1;
                        xrLabelMonth.Text = "";
                        xrLabelMonth.Text = monthName + rdr[0].ToString();
                        if (Properties.Settings.Default.CurrencySymbolPositionBefore == true)
                        {
                            if (index >= 2)
                            {
                                xrLabelDescription.Text = xrLabelDescription.Text + rdr[1].ToString() + ": " + Properties.Settings.Default.CurrencySymbol + rdr[2].ToString() + "\r\n";
                            }
                            else
                            {
                                xrLabelDescription.Text = rdr[1].ToString() + ": " + Properties.Settings.Default.CurrencySymbol + rdr[2].ToString() + "\r\n";
                            }
                        }
                        else
                        {
                            if (index >= 2)
                            {
                                xrLabelDescription.Text = xrLabelDescription.Text + rdr[1].ToString() + ": " + rdr[2].ToString() + Properties.Settings.Default.CurrencySymbol + "\r\n";
                            }
                            else
                            {
                                xrLabelDescription.Text = rdr[1].ToString() + ": " + rdr[2].ToString() + Properties.Settings.Default.CurrencySymbol + "\r\n";
                            }

                        }
                    }
                    con.Close();
                }
            
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         
        private void reportInvoiceSticker40_30mm_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            populateForm();
            try
            {
                //Display School info
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "select * from CompanyProfile ";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        xrLabelCompanyName.Text = pf.Decrypt(rdr.GetString(1));
                        //display watermark image  
                        if (Properties.Settings.Default.PrinterWatermark==true)
                        {
                            if (!Convert.IsDBNull(rdr["SchoolLogo"]))
                            {
                                byte[] x = (byte[])rdr["SchoolLogo"];
                                MemoryStream ms = new MemoryStream(x);
                                Image watermarkImage = Image.FromStream(ms);
                                // this.Watermark.Text = "Bindu";
                                this.Watermark.Image = watermarkImage;
                                this.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
                                this.Watermark.ImageTransparency = Properties.Settings.Default.PrinterWatermarkTransparency;
                            }
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
