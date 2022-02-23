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
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using System.Resources;

namespace EduXpress.Admin
{
    public partial class userControlSchoolDetails : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlSchoolDetails).Assembly);

        public userControlSchoolDetails()
        {
            InitializeComponent();
        }

        //private void btnExitApplication_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            pictureSchoolLogo.EditValue = null;
           // pictureSchoolLogo.Image = Properties.Resources.Company_Logo;
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            XtraOpenFileDialog OpenFile = new XtraOpenFileDialog();

            try
           {
                //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
                //   InvokeMenuMethod(GetMenu(pictureSchoolLogo), "OnClickedLoad");
                OpenFile.FileName = "";
                             OpenFile.Title = "Logo:";
                             OpenFile.Filter = "Image files: (*.jpg)|*.jpg|(*.jpeg)|*.jpeg|(*.png)|*.png|(*.Gif)|*.Gif|(*.bmp)|*.bmp| All Files (*.*)|*.*";
                              DialogResult res = OpenFile.ShowDialog();
                              if (res == DialogResult.OK)
                           {
                                 this.pictureSchoolLogo.Image = System.Drawing.Image.FromFile(OpenFile.FileName);
                                 pictureSchoolLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
                            }
            }
            catch (Exception ex)
                 {
                     XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
            }

        private DevExpress.XtraEditors.Controls.PictureMenu GetMenu(DevExpress.XtraEditors.PictureEdit edit)
        {
            PropertyInfo pi = typeof(DevExpress.XtraEditors.PictureEdit).GetProperty("Menu", BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi != null)
                return pi.GetValue(edit, null) as DevExpress.XtraEditors.Controls.PictureMenu;
            return null;
        }

        private void InvokeMenuMethod(DevExpress.XtraEditors.Controls.PictureMenu menu, String name)
        {
            MethodInfo mi = typeof(DevExpress.XtraEditors.Controls.PictureMenu).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
            if (mi != null && menu != null)
                mi.Invoke(menu, new object[] { menu, new EventArgs() });
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            enableControls();
            btnNew.Enabled = false;
            btnSave.Enabled = true;
           txtSchoolMoto.Focus();
        }
        private void enableControls()
        {
            groupSchoolDetails.Enabled = true;
            groupTaxInfo.Enabled = true;
            groupSchoolAddress.Enabled = true;
            groupBankingDetails.Enabled = true;
            btnBrowseImage.Enabled = true;
            btnRemoveImage.Enabled = true;
            pictureSchoolLogo.Enabled = true;
            //txtSchoolName.Enabled = true;
            //txtSchoolName.Properties.ReadOnly = false;
        }
        private void disableControls()
        {
            groupSchoolDetails.Enabled = false;
            groupTaxInfo.Enabled = false;
            groupSchoolAddress.Enabled = false;
            groupBankingDetails.Enabled = false;
            btnBrowseImage.Enabled = false;
            btnRemoveImage.Enabled = false;
            pictureSchoolLogo.Enabled = false;
            //txtSchoolName.Enabled = false;
            //txtSchoolName.Properties.ReadOnly = true;
        }
        private void reset()
        {
            txtSchoolName.Text = "";
                txtSchoolMoto.Text = "";
            txtOfficePhone.Text = "";
            txtCellphoneNo.Text = "";
            txtOfficeFax.Text = "";
            txtEmailAddress.Text = "";
            txtSchoolWebsite.Text = "";
            txtStreetName.Text = "";
            txtSuburbName.Text = "";
            txtTownName.Text = "";
            comboCountry.Text = "";
            txtRegistrationNo.Text = "";
            txtTaxNumber.Text = "";
            txtSchoolVAT.Text = "";
            txtVATRate.Text = "";
            rdNotVATRegistered.Checked = false;
            cmbBankName.SelectedIndex = -1;
                txtBankAccName.Text = "";
            txtAccountNumbers.Text = "";
            cmbAccountType.SelectedIndex = -1;
            txtBranch.Text = "";
            txtBranchCode.Text = "";
            txtSwiftCode.Text = "";
            pictureSchoolLogo.Image = null;
        }
        bool companyProfileExist = false;
        private void loadSchoolDetails()
        {
            //fill countries
            fillCountries();
            try
            {

                //Check if Company Profile has data in database

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select * from CompanyProfile ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        companyProfileExist = true;
                        btnNew.Enabled = false;
                        btnEdit.Enabled = true;
                        //load data from database to controls
                        if (rdr.Read())
                        {
                            txtSchoolName.Text = pf.Decrypt(rdr.GetString(1));
                            txtSchoolMoto.Text = rdr.GetString(2);
                            txtOfficePhone.Text = rdr.GetString(3);
                            txtOfficeFax.Text = rdr.GetString(4);
                            txtEmailAddress.Text = pf.Decrypt(rdr.GetString(5));
                            txtCellphoneNo.Text = pf.Decrypt(rdr.GetString(6));                         
                            txtSchoolWebsite.Text = rdr.GetString(7);
                            txtStreetName.Text = pf.Decrypt(rdr.GetString(8));
                            txtSuburbName.Text = rdr.GetString(9);
                            txtTownName.Text = pf.Decrypt(rdr.GetString(10));
                            comboCountry.Text = (rdr.GetString(11));
                            txtRegistrationNo.Text = (rdr.GetString(12));
                            txtTaxNumber.Text = (rdr.GetString(13));
                            txtSchoolVAT.Text = (rdr.GetString(14));
                            txtVATRate.Text = (rdr.GetValue(15).ToString());

                            if (rdr.GetBoolean(16) == true)
                            {
                                rdVATRegistered.Checked = true;
                                txtSchoolVAT.ReadOnly = false;
                            }
                            else
                            {
                                rdNotVATRegistered.Checked = true;
                                txtSchoolVAT.ReadOnly = true;
                                txtSchoolVAT.Text = "";
                            }
                            cmbBankName.Text = (rdr.GetString(17));
                            txtBankAccName.Text = (rdr.GetString(18));
                            txtAccountNumbers.Text = (rdr.GetString(19));

                            cmbAccountType.Text = (rdr.GetString(20));
                            txtBranch.Text = (rdr.GetString(21));

                            txtBranchCode.Text = (rdr.GetString(22));
                            txtSwiftCode.Text = (rdr.GetString(23));

                            if (!Convert.IsDBNull(rdr["SchoolLogo"]))
                            {
                                byte[] x = (byte[])rdr["SchoolLogo"];
                                using (MemoryStream ms = new MemoryStream(x))
                                {
                                    pictureSchoolLogo.Image = Image.FromStream(ms);
                                    pictureSchoolLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
                                    pictureSchoolLogo.Properties.PictureAlignment = ContentAlignment.MiddleCenter;
                                }                                   
                                
                            }
                        }
                    }
                    else
                    {
                        companyProfileExist = false;
                        btnNew.Enabled = true;
                        btnEdit.Enabled = false;
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
            private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtSchoolName.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSchoolName"), LocRM.GetString("strSchoolProfile")  , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSchoolName.Focus();
                return;
            }

            if (txtCellphoneNo.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSchoolCellphoneNo"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCellphoneNo.Focus();
                return;
            }
            if (txtEmailAddress.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSchoolEmail"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailAddress.Focus();
                return;
            }
            if (txtStreetName.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSchoolStreet"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStreetName.Focus();
                return;
            }
            if (txtTownName.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterTown"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTownName.Focus();
                return;
            }
            if (comboCountry.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterCountry"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboCountry.Focus();
                return;
            }
            if (rdVATRegistered.Checked==true)
            {
                if (txtSchoolVAT.Text == "")
                {
                    XtraMessageBox.Show( LocRM.GetString("strEnterSchoolVAT"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSchoolVAT.Focus();
                    return;
                }
            }
            if (txtSchoolVAT.Text.Trim() != "")
            {
                if (txtVATRate.Text == "")
                {
                    XtraMessageBox.Show( LocRM.GetString("strEnterSchoolVATRate"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVATRate.Focus();
                    return;
                }
            }

            if (!companyProfileExist)
            {
                //save School profile
                try
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving") );
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string cb = "insert into CompanyProfile( SchoolName, SchoolMotto, OfficePhone, OfficeFax, Email, " +
                            "Cellphone, SchoolWebsite,AddressStreet, AddressSuburb, AddressTown,Country,RegistrationNo," +
                            "TaxNumber,VATNumber,VATRate,VATRegistered,BankName,BankAccName,AccountNumber,AccountType," +
                            "Branch,BranchCode,SwiftCode,SchoolLogo) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10," +
                            "@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23, @d24)";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@d1", pf.Encrypt(txtSchoolName.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d2", txtSchoolMoto.Text.Trim());
                        cmd.Parameters.AddWithValue("@d3", txtOfficePhone.Text.Trim());                        
                        cmd.Parameters.AddWithValue("@d4", txtOfficeFax.Text.Trim());
                        cmd.Parameters.AddWithValue("@d5", pf.Encrypt(txtEmailAddress.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d6", pf.Encrypt(txtCellphoneNo.Text.Trim()));                        
                        cmd.Parameters.AddWithValue("@d7", txtSchoolWebsite.Text.Trim());
                        cmd.Parameters.AddWithValue("@d8", pf.Encrypt(txtStreetName.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d9", txtSuburbName.Text.Trim());
                        cmd.Parameters.AddWithValue("@d10", pf.Encrypt(txtTownName.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d11", comboCountry.Text);
                        cmd.Parameters.AddWithValue("@d12", txtRegistrationNo.Text);
                        cmd.Parameters.AddWithValue("@d13", txtTaxNumber.Text);
                        if (rdNotVATRegistered.Checked == true)//VAT registered
                        {
                            cmd.Parameters.AddWithValue("@d14", txtSchoolVAT.Text);
                            cmd.Parameters.AddWithValue("@d15", Convert.ToDecimal(txtVATRate.Text.Trim()));
                            cmd.Parameters.AddWithValue("@d16", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@d14", "");
                            cmd.Parameters.AddWithValue("@d15", Convert.ToDecimal("0"));
                            cmd.Parameters.AddWithValue("@d16", false);
                        }

                        cmd.Parameters.AddWithValue("@d17", cmbBankName.Text);
                        cmd.Parameters.AddWithValue("@d18", txtBankAccName.Text);
                        cmd.Parameters.AddWithValue("@d19", txtAccountNumbers.Text);
                        cmd.Parameters.AddWithValue("@d20", cmbAccountType.Text);
                        cmd.Parameters.AddWithValue("@d21", txtBranch.Text);
                        cmd.Parameters.AddWithValue("@d22", txtBranchCode.Text);
                        cmd.Parameters.AddWithValue("@d23", txtSwiftCode.Text);

                        if (pictureSchoolLogo.Image==null)
                        {
                            cmd.Parameters.Add("@d24", SqlDbType.Image).Value = DBNull.Value;
                        }
                        else
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (Bitmap bmpImage = new Bitmap(pictureSchoolLogo.Image))
                                {
                                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                byte[] data = ms.GetBuffer();
                                SqlParameter p = new SqlParameter("@d24", SqlDbType.Image);
                                p.Value = data;
                                cmd.Parameters.Add(p);
                            }
                        }
                            
                        cmd.ExecuteNonQuery();
                        con.Close();

                        //clear bitmap
                        //bmpImage.Dispose();
                       // ms.Dispose();
                    }

                    //save profile details in application settings               
                    Properties.Settings.Default.Country = comboCountry.Text.Trim();
                    Properties.Settings.Default.BusinessName = txtSchoolName.Text.Trim();
                    // ----- Save any updated settings.
                    Properties.Settings.Default.Save();

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }

                    XtraMessageBox.Show( LocRM.GetString("strSuccessfullySaved"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Log record transaction in logs
                    string st =  LocRM.GetString("strNewSchoolProfile")  + ": " + txtSchoolName.Text + " "+ LocRM.GetString("strHasBeenSaved")  ;
                    pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                    
                    //Business Name in Title Bar
                   // this.Text = this.Text + " - " + pf.Decrypt(rdr.GetString(0));

                    disableControls();
                    btnSave.Enabled = false;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = true;
                    companyProfileExist = true;
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
            else
            {
                try
                {
                    //Update company profile

                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating") );
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string cb = "update  CompanyProfile set SchoolMotto=@d2, OfficePhone=@d3, OfficeFax=@d4, Email=@d5, " +
                            "Cellphone=@d6, SchoolWebsite=@d7, AddressStreet=@d8,AddressSuburb=@d9,AddressTown=@d10," +
                            "Country=@d11,RegistrationNo=@d12,TaxNumber=@d13,VATNumber=@d14,VATRate=@d15,VATRegistered=@d16," +
                            "BankName=@d17,BankAccName=@d18,AccountNumber=@d19,AccountType=@d20,Branch=@d21,BranchCode=@d22," +
                            "SwiftCode=@d23,SchoolLogo=@d24 where SchoolName =@d1";

                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@d1", pf.Encrypt(txtSchoolName.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d2", txtSchoolMoto.Text.Trim());
                        cmd.Parameters.AddWithValue("@d3", txtOfficePhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@d4", txtOfficeFax.Text.Trim());
                        cmd.Parameters.AddWithValue("@d5", pf.Encrypt(txtEmailAddress.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d6", pf.Encrypt(txtCellphoneNo.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d7", txtSchoolWebsite.Text.Trim());
                        cmd.Parameters.AddWithValue("@d8", pf.Encrypt(txtStreetName.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d9", txtSuburbName.Text.Trim());
                        cmd.Parameters.AddWithValue("@d10", pf.Encrypt(txtTownName.Text.Trim()));
                        cmd.Parameters.AddWithValue("@d11", comboCountry.Text);
                        cmd.Parameters.AddWithValue("@d12", txtRegistrationNo.Text);
                        cmd.Parameters.AddWithValue("@d13", txtTaxNumber.Text);
                        
                        if (rdVATRegistered.Checked ==true)//VAT registered
                        {
                            cmd.Parameters.AddWithValue("@d14", txtSchoolVAT.Text);
                            cmd.Parameters.AddWithValue("@d15", Convert.ToDecimal(txtVATRate.Text.Trim()));                            
                            cmd.Parameters.AddWithValue("@d16", true);  
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@d14", "");
                            cmd.Parameters.AddWithValue("@d15", Convert.ToDecimal("0"));
                            cmd.Parameters.AddWithValue("@d16", false);
                        }

                        cmd.Parameters.AddWithValue("@d17", cmbBankName.Text);
                        cmd.Parameters.AddWithValue("@d18", txtBankAccName.Text);
                        cmd.Parameters.AddWithValue("@d19", txtAccountNumbers.Text);
                        cmd.Parameters.AddWithValue("@d20", cmbAccountType.Text);
                        cmd.Parameters.AddWithValue("@d21", txtBranch.Text);
                        cmd.Parameters.AddWithValue("@d22", txtBranchCode.Text);
                        cmd.Parameters.AddWithValue("@d23", txtSwiftCode.Text);

                        if (pictureSchoolLogo.Image == null)
                        {
                            //SqlParameter p = new SqlParameter("@d24", SqlDbType.Image);
                            //p.Value = DBNull.Value;
                            //cmd.Parameters.Add(p);
                            //cmd.Parameters.Add(p);
                            cmd.Parameters.Add("@d24", SqlDbType.Image).Value = DBNull.Value;
                        }
                        else
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (Bitmap bmpImage = new Bitmap(pictureSchoolLogo.Image))
                                {
                                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                byte[] data = ms.GetBuffer();
                                SqlParameter p = new SqlParameter("@d24", SqlDbType.Image);
                                p.Value = data;
                                cmd.Parameters.Add(p);
                            }
                        }

                        cmd.ExecuteNonQuery();
                        con.Close();
                        //clear bitmap
                        // bmpImage.Dispose();
                        // ms.Dispose();

                        //save profile details in application settings               
                        Properties.Settings.Default.Country = comboCountry.Text.Trim();
                        Properties.Settings.Default.BusinessName = txtSchoolName.Text.Trim();
                        // ----- Save any updated settings.
                        Properties.Settings.Default.Save();
                    }
                        

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }

                    XtraMessageBox.Show( LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolProfile"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Log record transaction in logs
                    string st = LocRM.GetString("strSchoolProfile")  +": " + txtSchoolName.Text + " "+ LocRM.GetString("strHasBeenUpdated")  ;
                    pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);


                    disableControls();
                    btnSave.Enabled = false;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = true;
                    companyProfileExist = true;
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

            if ((rdr != null))
            {
                rdr.Close();
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            enableControls();
            txtSchoolMoto.SelectAll();
            txtSchoolMoto.Focus();
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
        }

        private void rdVATRegistered_CheckedChanged(object sender, EventArgs e)
        {
            if (rdVATRegistered.Checked==true)
            {
                txtSchoolVAT.Properties.ReadOnly = false;
                txtVATRate.Properties.ReadOnly = false;
            }
            else
            {
                txtSchoolVAT.Text = "";
                txtVATRate.Text = "";
                txtSchoolVAT.Properties.ReadOnly = false;
                txtVATRate.Properties.ReadOnly = false;
            }
        }

        private void rdNotVATRegistered_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNotVATRegistered.Checked == true)
            {
                txtSchoolVAT.Text = "";
                txtVATRate.Text = "";
                txtSchoolVAT.Properties.ReadOnly = true;
                txtVATRate.Properties.ReadOnly = true;
            }
        }

        private void userControlSchoolDetails_Load(object sender, EventArgs e)
        {
            loadSchoolDetails();
        }
        //Fill Countries
        private void fillCountries()
        {
           comboCountry.Properties.Items.Clear();
            comboCountry.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strAngola"),            
            LocRM.GetString("strBotswana"),
            LocRM.GetString("strBurundi"),
            LocRM.GetString("strCameroon"),
            LocRM.GetString("strCAR"),
            LocRM.GetString("strChad"),
            LocRM.GetString("strCongoBrazzaville"),
            LocRM.GetString("strDRC"),
            LocRM.GetString("strEquatorialGuinea"),
            LocRM.GetString("strEthiopia"),
            LocRM.GetString("strGabon"),
            LocRM.GetString("strKenya"),
            LocRM.GetString("strLesotho"),
            LocRM.GetString("strMadagascar"),
            LocRM.GetString("strMalawi"),
            LocRM.GetString("strMauritius"),
            LocRM.GetString("strMozambique"),
            LocRM.GetString("strNamibia"),
            LocRM.GetString("strRwanda"),
            LocRM.GetString("strSãoToméPríncipe"),
            LocRM.GetString("strSouthAfrica"),
            LocRM.GetString("strSwaziland"),
            LocRM.GetString("strTanzania"),
            LocRM.GetString("strUganda"), 
            LocRM.GetString("strZambia"),
            LocRM.GetString("strZimbabwe"),
            LocRM.GetString("strOther")});
        }
        private void fillBanksDRC()
        {
            cmbBankName.Properties.Items.Clear();
            cmbBankName.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strAccessBank"),
            LocRM.GetString("strAdvansBank"),
            LocRM.GetString("strAfrilandFirstBank"),
            LocRM.GetString("strAllianceBankSA"),
            LocRM.GetString("strBankAfrica"),
            LocRM.GetString("strBanqueCommercialeCongo"),
            LocRM.GetString("strBanqueInternationalCredit"),
            LocRM.GetString("strBanqueInternationaleAfrique"),
            LocRM.GetString("strBanqueInternationaleAfriqueCongo"),
            LocRM.GetString("strCaisseCentraleCoopEconomique"),
            LocRM.GetString("strCitibank"),
            LocRM.GetString("strEcobank"),
            LocRM.GetString("strEquityBankCongo"),
            LocRM.GetString("strFransabank"),
            LocRM.GetString("strRawbank"),
            LocRM.GetString("strSofiBank"),
            LocRM.GetString("strStanbicBank"),
            LocRM.GetString("strTrustMerchantBank"),
            LocRM.GetString("strUnitedBankAfrica")});
        }
        private void fillBanksSA()
        {
            cmbBankName.Properties.Items.Clear();
            cmbBankName.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strAbsa"),
            LocRM.GetString("strAfricanBank"),
            LocRM.GetString("strBidvestBank"),
            LocRM.GetString("strCapitec"),
            LocRM.GetString("strDiscovery"),
            LocRM.GetString("strFNB"),
            LocRM.GetString("strGrindrodBank"),
            LocRM.GetString("strImperialBankSA"),
            LocRM.GetString("strInvestec"),
            LocRM.GetString("strNedbank"),
            LocRM.GetString("strSasfin"),
            LocRM.GetString("strStandardBank"),
            LocRM.GetString("strTymeBank"),
            LocRM.GetString("strUbank")});
        }
        private void fillBanksBotswana()
        {
            cmbBankName.Properties.Items.Clear();
            cmbBankName.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strAbsaBankBotswana"),
            LocRM.GetString("strBancABC"),
            LocRM.GetString("strBankBarodaBotswana"),
            LocRM.GetString("strBankGaborone"),
            LocRM.GetString("strBankIndiaBotswana"),
            LocRM.GetString("strBankSBIBotswana"),
            LocRM.GetString("strFirstCapitalBank"),
            LocRM.GetString("strFNBBotswana"),
            LocRM.GetString("strStanbicBankBotswana"),
            LocRM.GetString("strStandardCharteredBankBotswana")});
        }
        private void fillBanksTypes()
        {
            cmbAccountType.Properties.Items.Clear();
            cmbAccountType.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strChequeAccount"),
            LocRM.GetString("strCurrentAccount"),
            LocRM.GetString("strSavingsAccount")});
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void txtEmailAddress_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtEmailAddress.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmailAddress.Text))
                {
                    XtraMessageBox.Show( LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmailAddress.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void userControlSchoolDetails_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
              // reset();
                disableControls();
                btnNew.Enabled = true ;
                btnSave.Enabled = false;
                loadSchoolDetails();
                
            }
        }
       //Enable New or edit schoolprofile by clicking school Name while pressing Ctrl + Shift
        private void txtSchoolName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.ShiftKey)
            {
               if( companyProfileExist == false)
                {
                    btnNew.Enabled = true;
                }
               else
                {
                    btnEdit.Enabled = true;
                }
            }
        }

        private void comboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCountry.Text== LocRM.GetString("strBotswana"))
            {
                fillBanksBotswana();
            }
            else if (comboCountry.Text == LocRM.GetString("strDRC"))
            {
                fillBanksDRC();
            }
            else if (comboCountry.Text == LocRM.GetString("strSouthAfrica"))
            {
                fillBanksSA();
            }
            else
            {
                cmbAccountType.Properties.Items.Clear();
            }
        }

        private void cmbBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillBanksTypes();
        }
    }
}
