using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;
using static EduXpress.Functions.PublicVariables;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraPrinting;


namespace EduXpress.Students
{
    public partial class reportInvoice80mm : DevExpress.XtraReports.UI.XtraReport
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(reportInvoice80mm).Assembly);

        public reportInvoice80mm()
        {
            InitializeComponent();
        }
        private void populateForm()
        {
            try
            {
                string barcodeText;
                //display cashier name               
                xrLabelCashier.Text = xrLabelCashier.Text + UserLoggedSurname + " " + UserLoggedName;
                //display student and fee info
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = con.CreateCommand();

                    cmd.CommandText = "SELECT * FROM Students, CourseFeePayment  WHERE Students.StudentNumber=CourseFeePayment.StudentNumber and PaymentID = '" + receiptNumber + "' ";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        xrLabelStudentNo.Text = xrLabelStudentNo.Text + (rdr.GetString(1).Trim());
                        xrLabelSurname.Text = xrLabelSurname.Text + (rdr.GetString(5).Trim());
                        xrLabelName.Text = xrLabelName.Text + (rdr.GetString(6).Trim());
                        xrLabelClass.Text = xrLabelClass.Text + (rdr.GetString(4).Trim());
                        DateTime dt = (DateTime)rdr["PaymentDate"];
                        xrLabelPaymentDate.Text = xrLabelPaymentDate.Text + dt.ToString("dd/MM/yyyy");
                        if (Properties.Settings.Default.CurrencySymbolPositionBefore==true)
                        {
                            // xrLabelDiscount.Text = xrLabelDiscount.Text + Properties.Settings.Default.CurrencySymbol + (rdr.GetDecimal(50).ToString());
                            xrLabelDiscount.Text = xrLabelDiscount.Text + Properties.Settings.Default.CurrencySymbol + rdr["DiscountAmt"].ToString();
                            //xrLabelPreviousDue.Text = xrLabelPreviousDue.Text + Properties.Settings.Default.CurrencySymbol + (rdr.GetDecimal(51).ToString());
                            xrLabelPreviousDue.Text = xrLabelPreviousDue.Text + Properties.Settings.Default.CurrencySymbol + rdr["PreviousDue"].ToString();
                            //xrLabelFine.Text = xrLabelFine.Text + Properties.Settings.Default.CurrencySymbol + (rdr.GetDecimal(52).ToString());
                            xrLabelFine.Text = xrLabelFine.Text + Properties.Settings.Default.CurrencySymbol + rdr["Fine"].ToString();
                            // xrLabelTotalDue.Text = xrLabelTotalDue.Text + Properties.Settings.Default.CurrencySymbol + (rdr.GetDecimal(53).ToString());
                            xrLabelTotalDue.Text = xrLabelTotalDue.Text + Properties.Settings.Default.CurrencySymbol + rdr["GrandTotal"].ToString();
                            //xrLabelTotalPaid.Text = xrLabelTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + (rdr.GetDecimal(54).ToString());
                            xrLabelTotalPaid.Text = xrLabelTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + rdr["TotalPaid"].ToString();
                            //xrLabelBalance.Text = xrLabelBalance.Text + Properties.Settings.Default.CurrencySymbol + (rdr.GetDecimal(57).ToString());
                            xrLabelBalance.Text = xrLabelBalance.Text + Properties.Settings.Default.CurrencySymbol + rdr["PaymentDue"].ToString();
                        }
                        else
                        {
                            xrLabelDiscount.Text = xrLabelDiscount.Text + rdr["DiscountAmt"].ToString() + Properties.Settings.Default.CurrencySymbol;
                            xrLabelPreviousDue.Text = xrLabelPreviousDue.Text + rdr["PreviousDue"].ToString() + Properties.Settings.Default.CurrencySymbol;
                            xrLabelFine.Text = xrLabelFine.Text + rdr["Fine"].ToString() + Properties.Settings.Default.CurrencySymbol;
                            xrLabelTotalDue.Text = xrLabelTotalDue.Text + rdr["GrandTotal"].ToString() + Properties.Settings.Default.CurrencySymbol;
                            xrLabelTotalPaid.Text = xrLabelTotalPaid.Text + rdr["TotalPaid"].ToString() + Properties.Settings.Default.CurrencySymbol;
                            xrLabelBalance.Text = xrLabelBalance.Text + rdr["PaymentDue"].ToString() + Properties.Settings.Default.CurrencySymbol;
                        }

                        barcodeText = rdr["PaymentID"].ToString();
                        xrBarCode1.Text = barcodeText;
                        xrBarCode1.BackColor = Color.White;
                        xrBarCode1.ForeColor = Color.Black;                        
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
                    //string monthName = xrLabelMonth.Text.ToUpper(); LocRM.GetString("strError")
                    string monthName = LocRM.GetString("strPeriod").ToUpper() + ": ";
                    while ((rdr.Read() == true))
                    {
                        index = index + 1;
                        xrLabelMonth.Text = "";
                        xrLabelMonth.Text = monthName + rdr[0].ToString().ToUpper();
                        if (Properties.Settings.Default.CurrencySymbolPositionBefore == true)
                        {
                            if (index >= 2)
                            {
                                xrLabelDescription.Text = xrLabelDescription.Text  + rdr[1].ToString() + ": " + Properties.Settings.Default.CurrencySymbol + rdr[2].ToString() + "\r\n";
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
                        xrLabelCompanyMotto.Text = (rdr.GetString(2));
                        lblTel.Text = lblTel.Text + " " + pf.Decrypt(rdr.GetString(6));
                        xrLabelCompanyAddress.Text = pf.Decrypt(rdr.GetString(8)) + " " + (rdr.GetString(9)) + ", " + pf.Decrypt(rdr.GetString(10));

                        if (!Convert.IsDBNull(rdr["SchoolLogo"]))
                        {
                            byte[] x = (byte[])rdr["SchoolLogo"];
                            MemoryStream ms = new MemoryStream(x);
                           
                             xrPictureBox1.Image = Image.FromStream(ms);
                             xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Normal;
                            //Resize the picture box based on size of image
                             Image image = xrPictureBox1.ImageSource.Image;
                            const float mmPerInch = 25.4F;//number containing a decimal point that doesn't have a suffix is interpreted as a double
                            xrPictureBox1.HeightF = (image.Height / image.VerticalResolution * mmPerInch) * 10; //the report report unit is TenthsOfAMillimeter
                        }
                        else
                        {
                            xrPictureBox1.Image = null;
                            xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
                          //  xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
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

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            populateForm();
        }
        
    }
}
