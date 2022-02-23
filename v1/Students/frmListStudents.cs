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
using System.IO;
using DevExpress.XtraGrid;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using static EduXpress.Functions.PublicVariables;
using System.Resources;
using DevExpress.XtraPrinting;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace EduXpress.Students
{
    public partial class frmListStudents : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmListStudents).Assembly);

        public frmListStudents()
        {
            InitializeComponent();
        }

        private void frmListStudents_Load(object sender, EventArgs e)
        {
            //Suspend layout: Basically it's if you want to adjust multiple layout-related properties - 
            //or add multiple children - but avoid the layout system repeatedly reacting to your changes. 
            //You want it to only perform the layout at the very end, when everything's "ready".
            //this.SuspendLayout();
            //gridControlListStudents.DataSource = Getdata();
            ////Resume layout
            //this.ResumeLayout();
            ////hide course ID column
            //gridView1.Columns[0].Visible = false;
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
            string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
            string CycleColumn = LocRM.GetString("strCycle").ToUpper();
            string SectionColumn = LocRM.GetString("strSection").ToUpper();
            string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
            string EnrolmentDateColumn = LocRM.GetString("strEnrolmentDate").ToUpper();
            string ClassColumn = LocRM.GetString("strClass").ToUpper();
            string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            string NameColumn = LocRM.GetString("strName").ToUpper();
            string GenderColumn = LocRM.GetString("strGender").ToUpper();
            string AgeColumn = LocRM.GetString("strAge").ToUpper();
            string NationalityColumn = LocRM.GetString("strNationality").ToUpper();
            string LastSchoolAttendedColumn = LocRM.GetString("strLastSchoolAttended").ToUpper();
            string ResultColumn = LocRM.GetString("strResults").ToUpper();
            string PassPercentageColumn = LocRM.GetString("strPassPercentage").ToUpper();
            string EmailColumn = LocRM.GetString("strEmail").ToUpper();
            string PhoneNumberColumn = LocRM.GetString("strPhoneNumber").ToUpper();
            string HomeLanguageColumn = LocRM.GetString("strHomeLanguage").ToUpper();
            string ReligionColumn = LocRM.GetString("strReligion").ToUpper();
            string MedicalConditionsColumn = LocRM.GetString("strMedicalConditions").ToUpper();
            string SiblingsSchoolColumn = LocRM.GetString("strSiblingsSchool").ToUpper();
            string NoSiblingsColumn = LocRM.GetString("strNoSiblings").ToUpper();
            string DocumentsSubmittedColumn = LocRM.GetString("strDocumentsSubmitted").ToUpper();
            string FatherSurnameColumn = LocRM.GetString("strFatherSurname").ToUpper();
            string FatherNameColumn = LocRM.GetString("strFatherName").ToUpper();
            string FatherContactNoColumn = LocRM.GetString("strFatherContactNo").ToUpper();
            string FatherEmailColumn = LocRM.GetString("strFatherEmail").ToUpper();
            string NotificationNoColumn = LocRM.GetString("strNotificationNo").ToUpper();
            string NotificationEmailColumn = LocRM.GetString("strNotificationEmail").ToUpper();
            string MotherSurnameColumn = LocRM.GetString("strMotherSurname").ToUpper();
            string MotherNamesColumn = LocRM.GetString("strMotherNames").ToUpper();
            string MotherContactNoColumn = LocRM.GetString("strMotherContactNo").ToUpper();
            string MotherEmailColumn = LocRM.GetString("strMotherEmail").ToUpper();
            string EmergencyPhoneNoColumn = LocRM.GetString("strEmergencyPhoneNo").ToUpper();
            string AbsencePhoneNoColumn = LocRM.GetString("strAbsencePhoneNo").ToUpper();
            string SchoolActivitiesColumn = LocRM.GetString("strSchoolActivities").ToUpper();
            string SchoolPhotosColumn = LocRM.GetString("strSchoolPhotos").ToUpper();
            string HomeAddressColumn = LocRM.GetString("strHomeAddress").ToUpper();
            string NameTutor = LocRM.GetString("strNameTutor").ToUpper();

            //dynamic SelectQry = "SELECT (StudentID) as [ID] , RTRIM(StudentNumber) as [" + StudentNoColumn + "]," +
            dynamic SelectQry = "SELECT (StudentID) as [ID] , RTRIM(StudentNumber) as [" + StudentNoColumn + "]," +
                "RTRIM(Cycle) as [" + CycleColumn + "], RTRIM(Section) as [" + SectionColumn + "]," +
                "RTRIM(SchoolYear) as [" + SchoolYearColumn + "],EnrolmentDate as [" + EnrolmentDateColumn + "]," +
                "RTRIM(Class) as [" + ClassColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "]," +
                "RTRIM(StudentFirstNames) as [" + NameColumn + "],RTRIM(Gender) as [" + GenderColumn + "],RTRIM(Age) as [" + AgeColumn + "]," +
                "RTRIM(Nationality) as [" + NationalityColumn + "],RTRIM(LastSchoolAttended) as [" + LastSchoolAttendedColumn + "] ," +
                "RTRIM(LastSchoolResult) as [" + ResultColumn + "],RTRIM(PassPercentage) as [" + PassPercentageColumn + "]," +
                "RTRIM(StudentEmail) as [" + EmailColumn + "],RTRIM(StudentPhoneNumber) as [" + PhoneNumberColumn + "]," +
                "RTRIM(HomeLanguage) as [" + HomeLanguageColumn + "],RTRIM(Religion) as [" + ReligionColumn + "]," +
                "RTRIM(TutorName) as [" + NameTutor + "],"+
                "RTRIM(MedicalAllergies) as [" + MedicalConditionsColumn + "]," +
                "RTRIM(StudentSibling) as [" + SiblingsSchoolColumn + "],RTRIM(SiblingsNo) as [" + NoSiblingsColumn + "]," +
                "RTRIM(DocumentSubmitted) as [" + DocumentsSubmittedColumn + "],RTRIM(FatherSurname) as [" + FatherSurnameColumn + "]," +
                "RTRIM(FatherNames) as [" + FatherNameColumn + "] ,RTRIM(FatherContactNo) as [" + FatherContactNoColumn + "]," +
                "RTRIM(FatherEmail) as [" + FatherEmailColumn + "],RTRIM(NotificationNo) as [" + NotificationNoColumn + "]," +
                "RTRIM(NotificationEmail) as [" + NotificationEmailColumn + "],RTRIM(MotherSurname) as [" + MotherSurnameColumn + "]," +
                "RTRIM(MotherNames) as [" + MotherNamesColumn + "],RTRIM(MotherContactNo) as [" + MotherContactNoColumn + "]," +
                "RTRIM(MotherEmail) as [" + MotherEmailColumn + "], RTRIM(EmergencyPhoneNo) as [" + EmergencyPhoneNoColumn + "]," +
                "RTRIM(AbsencePhoneNo) as [" + AbsencePhoneNoColumn + "]," +
                "RTRIM(SchoolActivities) as [" + SchoolActivitiesColumn + "]," +
                "RTRIM(SchoolPhotos) as [" + SchoolPhotosColumn + "], " +
                "RTRIM(HomeAddress) as [" + HomeAddressColumn + "] FROM Students order by StudentNumber";
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

        private void btnSearchFullnames_Click(object sender, EventArgs e)
        {
            if (cmbSchoolYearName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYearName.Focus();
                return;
            }

            if (txtSearchFullNames.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearchFullNames.Focus();
                return;
            }

            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();
            

            //load culumns in gridControlFeeInfo
            gridControlListStudents.DataSource = CreateDataStudentsBySurname();
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            filterStudentsByName();

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            //clear search fields     
            //cmbSchoolYear.SelectedIndex = -1;
            txtSearchSurname.Text = "";
            cmbSectionClass.Enabled = false;
            cmbClassby.Enabled = false;
            cmbSection.Enabled = false;
            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;
            cmbCycle.SelectedIndex = -1;
            dtSearchbyDateFrom.EditValue = DateTime.Today;
            dtSearchbyDateTo.EditValue = DateTime.Today;
           

            //clear Detail Fields
            clearDetailFields();

            //Set Report details   
            SchoolYear = schoolYear;
            studentSearchBy = 5;
            searchBy = 5;
            studentName = txtSearchFullNames.Text.Trim();
            
        }
                       
        private void clearControls()
        {
            txtSearchSurname.Text = "";
            txtSearchFullNames.Text = "";
            cmbSectionClass.Enabled = false;
            cmbClassby.Enabled = false;
            cmbSection.Enabled = false;
            cmbSchoolYear.SelectedIndex = -1;
            cmbCycle.SelectedIndex = -1;
            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;
            dtSearchbyDateFrom.EditValue = DateTime.Today;
            dtSearchbyDateTo.EditValue = DateTime.Today;

            txtStudentNumber.Text = "";
            txtSection.Text = "";
            txtClass.Text = "";
            txtCycle.Text = "";
            txtStudentSurname.Text = "";
            txtStudentNames.Text = "";
            pictureStudent.EditValue = null;
        }
        private void clearDetailFields()
        {
            txtStudentNumber.Text = "";
            txtSection.Text = "";
            txtClass.Text = "";
            txtCycle.Text = "";
            txtStudentSurname.Text = "";
            txtStudentNames.Text = "";
            pictureStudent.EditValue = null;
        }

        private void btnSearchSurname_Click(object sender, EventArgs e)
        {
            if (cmbSchoolYearSurname.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYearSurname.Focus();
                return;
            }

            if (txtSearchSurname.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSurname"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearchSurname.Focus();
                return;
            }

            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();
            

            //load culumns in gridControlFeeInfo
            gridControlListStudents.DataSource = CreateDataStudentsBySurname();
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            filterStudentsBySurname();            

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            //clear search fields   
            //cmbSchoolYear.SelectedIndex = -1;
            txtSearchFullNames.Text = "";
            cmbSectionClass.Enabled = false;
            cmbClassby.Enabled = false;
            cmbSection.Enabled = false;
            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;
            cmbCycle.SelectedIndex = -1;
            dtSearchbyDateFrom.EditValue = DateTime.Today;
            dtSearchbyDateTo.EditValue = DateTime.Today;
            

            //clear Detail Fields
            clearDetailFields();

            //Set Report details  
            SchoolYear = schoolYear;
            studentSearchBy = 4;
            searchBy = 4;
            studentSurName = txtSearchSurname.Text.Trim();
        }

        private void btnSearchAdmissionDate_Click(object sender, EventArgs e)
        {
            if (cmbSchoolYearEnrolmentYear.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYearEnrolmentYear.Focus();
                return;
            }
            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            //load culumns in gridControlFeeInfo
            gridControlListStudents.DataSource = CreateDataStudentsBySurname();
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            filterStudentsByEnrolmentDate();

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            //clear search fields  
            //cmbSchoolYear.SelectedIndex = -1;
            txtSearchSurname.Text = "";
            txtSearchFullNames.Text = "";
            cmbSectionClass.Enabled = false;
            cmbClassby.Enabled = false;
            cmbSection.Enabled = false;
            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;
            cmbCycle.SelectedIndex = -1;           
            

            //clear Detail Fields
            clearDetailFields();

            //Set Report details 
            SchoolYear = schoolYear;
            studentSearchBy = 6;
            searchBy = 6;
            searchbyDateFrom= dtSearchbyDateFrom.DateTime.ToString("dd/MM/yyyy");
            searchbyDateTo = dtSearchbyDateTo.DateTime.ToString("dd/MM/yyyy");

        }
        //studentSearchBy, searchBy public variable
        int searchBy = 0;
        //1: search by class
        //2: search by section
        //3: search by Cycle
        //4:search by surname
        //5:search by name
        //6:search by enrolment date
        //7:search by School year

        private void btnSearchSectionClass_Click(object sender, EventArgs e)
        {
            if (cmbSchoolYearClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYearClass.Focus();
                return;
            }

            if (cmbClassby.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClassby.Focus();
                return;
            }

            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();
            

            //load culumns in gridControlFeeInfo
            gridControlListStudents.DataSource = CreateDataStudentsByClass();
           
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            filterStudentsByClass();
            CalculateNoStudentsClass();

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            //clear search fields
            //cmbSchoolYear.SelectedIndex = -1;
            txtSearchSurname.Text = "";
            txtSearchFullNames.Text = "";
            cmbCycle.SelectedIndex = -1;
            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbSection.Enabled = false;
            dtSearchbyDateFrom.EditValue = DateTime.Today;
            dtSearchbyDateTo.EditValue = DateTime.Today;
            


            //clear Detail Fields
            clearDetailFields();

            //Set Report details
            section =cmbCycleClass.Text;
            className = cmbClassby.Text;
            SchoolYear = schoolYear;
            studentSearchBy = 1;
            searchBy = 1;


            #region MyRegion
            //try
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == false)
            //    {
            //        splashScreenManager1.ShowWaitForm();
            //    }

            //    string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
            //    string CycleColumn = LocRM.GetString("strCycle").ToUpper();
            //    string SectionColumn = LocRM.GetString("strSection").ToUpper();
            //    string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
            //    string EnrolmentDateColumn = LocRM.GetString("strEnrolmentDate").ToUpper();
            //    string ClassColumn = LocRM.GetString("strClass").ToUpper();
            //    string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            //    string NameColumn = LocRM.GetString("strName").ToUpper();
            //    string GenderColumn = LocRM.GetString("strGender").ToUpper();
            //    string AgeColumn = LocRM.GetString("strAge").ToUpper();
            //    string NationalityColumn = LocRM.GetString("strNationality").ToUpper();
            //    string LastSchoolAttendedColumn = LocRM.GetString("strLastSchoolAttended").ToUpper();
            //    string ResultColumn = LocRM.GetString("strResults").ToUpper();
            //    string PassPercentageColumn = LocRM.GetString("strPassPercentage").ToUpper();
            //    string EmailColumn = LocRM.GetString("strEmail").ToUpper();
            //    string PhoneNumberColumn = LocRM.GetString("strPhoneNumber").ToUpper();
            //    string HomeLanguageColumn = LocRM.GetString("strHomeLanguage").ToUpper();
            //    string ReligionColumn = LocRM.GetString("strReligion").ToUpper();
            //    string MedicalConditionsColumn = LocRM.GetString("strMedicalConditions").ToUpper();
            //    string SiblingsSchoolColumn = LocRM.GetString("strSiblingsSchool").ToUpper();
            //    string NoSiblingsColumn = LocRM.GetString("strNoSiblings").ToUpper();
            //    string DocumentsSubmittedColumn = LocRM.GetString("strDocumentsSubmitted").ToUpper();
            //    string FatherSurnameColumn = LocRM.GetString("strFatherSurname").ToUpper();
            //    string FatherNameColumn = LocRM.GetString("strFatherName").ToUpper();
            //    string FatherContactNoColumn = LocRM.GetString("strFatherContactNo").ToUpper();
            //    string FatherEmailColumn = LocRM.GetString("strFatherEmail").ToUpper();
            //    string NotificationNoColumn = LocRM.GetString("strNotificationNo").ToUpper();
            //    string NotificationEmailColumn = LocRM.GetString("strNotificationEmail").ToUpper();
            //    string MotherSurnameColumn = LocRM.GetString("strMotherSurname").ToUpper();
            //    string MotherNamesColumn = LocRM.GetString("strMotherNames").ToUpper();
            //    string MotherContactNoColumn = LocRM.GetString("strMotherContactNo").ToUpper();
            //    string MotherEmailColumn = LocRM.GetString("strMotherEmail").ToUpper();
            //    string EmergencyPhoneNoColumn = LocRM.GetString("strEmergencyPhoneNo").ToUpper();
            //    string AbsencePhoneNoColumn = LocRM.GetString("strAbsencePhoneNo").ToUpper();
            //    string SchoolActivitiesColumn = LocRM.GetString("strSchoolActivities").ToUpper();
            //    string SchoolPhotosColumn = LocRM.GetString("strSchoolPhotos").ToUpper();
            //    string HomeAddressColumn = LocRM.GetString("strHomeAddress").ToUpper();

            //    using (con = new SqlConnection(databaseConnectionString))
            //    {
            //        con.Open();
            //        cmd = new SqlCommand("SELECT (StudentID) as [ID] , RTRIM(StudentNumber) as [" + StudentNoColumn + "]," +
            //        "RTRIM(Cycle) as [" + CycleColumn + "], RTRIM(Section) as [" + SectionColumn + "]," +
            //        "RTRIM(SchoolYear) as [" + SchoolYearColumn + "],RTRIM(EnrolmentDate) as [" + EnrolmentDateColumn + "]," +
            //        "RTRIM(Class) as [" + ClassColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "]," +
            //        "RTRIM(StudentFirstNames) as [" + NameColumn + "],RTRIM(Gender) as [" + GenderColumn + "],RTRIM(Age) as [" + AgeColumn + "]," +
            //        "RTRIM(Nationality) as [" + NationalityColumn + "],RTRIM(LastSchoolAttended) as [" + LastSchoolAttendedColumn + "] ," +
            //        "RTRIM(LastSchoolResult) as [" + ResultColumn + "],RTRIM(PassPercentage) as [" + PassPercentageColumn + "]," +
            //        "RTRIM(StudentEmail) as [" + EmailColumn + "],RTRIM(StudentPhoneNumber) as [" + PhoneNumberColumn + "]," +
            //        "RTRIM(HomeLanguage) as [" + HomeLanguageColumn + "],RTRIM(Religion) as [" + ReligionColumn + "]," +
            //        "RTRIM(MedicalAllergies) as [" + MedicalConditionsColumn + "]," +
            //        "RTRIM(StudentSibling) as [" + SiblingsSchoolColumn + "],RTRIM(SiblingsNo) as [" + NoSiblingsColumn + "]," +
            //        "RTRIM(DocumentSubmitted) as [" + DocumentsSubmittedColumn + "],RTRIM(FatherSurname) as [" + FatherSurnameColumn + "]," +
            //        "RTRIM(FatherNames) as [" + FatherNameColumn + "] ,RTRIM(FatherContactNo) as [" + FatherContactNoColumn + "]," +
            //        "RTRIM(FatherEmail) as [" + FatherEmailColumn + "],RTRIM(NotificationNo) as [" + NotificationNoColumn + "]," +
            //        "RTRIM(NotificationEmail) as [" + NotificationEmailColumn + "],RTRIM(MotherSurname) as [" + MotherSurnameColumn + "]," +
            //        "RTRIM(MotherNames) as [" + MotherNamesColumn + "],RTRIM(MotherContactNo) as [" + MotherContactNoColumn + "]," +
            //        "RTRIM(MotherEmail) as [" + MotherEmailColumn + "], RTRIM(EmergencyPhoneNo) as [" + EmergencyPhoneNoColumn + "]," +
            //        "RTRIM(AbsencePhoneNo) as [" + AbsencePhoneNoColumn + "]," +
            //        "RTRIM(SchoolActivities) as [" + SchoolActivitiesColumn + "]," +
            //        "RTRIM(SchoolPhotos) as [" + SchoolPhotosColumn + "], " +
            //        "RTRIM(HomeAddress) as [" + HomeAddressColumn + "] FROM Students where Section=@d1 and Class=@d2 order by Class ", con);
            //        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Section").Value = cmbSection.Text;
            //        cmd.Parameters.Add("@d2", SqlDbType.NChar, 30, " Class").Value = cmbClass.Text;

            //        SqlDataAdapter myDA = new SqlDataAdapter(cmd);
            //        DataSet myDataSet = new DataSet();

            //        myDA.Fill(myDataSet, "Students");
            //        gridControlListStudents.DataSource = myDataSet.Tables["Students"].DefaultView;

            //        con.Close();
            //    }





            //}
            //catch (Exception ex)
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        private void CalculateNoStudentsAll()
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    //display number of students 
                    gridView1.Columns[LocRM.GetString("strSurname").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    gridView1.Columns[LocRM.GetString("strSurname").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strSurname").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberStudents").ToUpper() + ": {0}";

                    //set width of count column to fixed
                    gridView1.Columns[LocRM.GetString("strSurname").ToUpper()].OptionsColumn.FixedWidth = true;
                    gridView1.Columns[LocRM.GetString("strSurname").ToUpper()].Width = 200;
                    
                    //refresh summary
                    gridView1.UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateNoStudentsClass()
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    //display number of students 
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberStudents").ToUpper() + ": {0}"; 

                    //refresh summary
                    gridView1.UpdateSummary();                    
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalculateNoStudentsSection()
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    //display number of students 
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberStudents").ToUpper() + ": {0}";

                    //refresh summary
                    gridView1.UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalculateNoStudentsCycle()
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    //display number of students 
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberStudents").ToUpper() + ": {0}";

                    //refresh summary
                    gridView1.UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       
        //Autocomplete Surname
        private void AutocompleteSurname()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT StudentSurname FROM Students", con);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "Students");

                    AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        col.Add(ds.Tables[0].Rows[i]["StudentSurname"].ToString());

                    }
                    txtSearchSurname.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtSearchSurname.MaskBox.AutoCompleteCustomSource = col;
                    txtSearchSurname.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;

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
        //Autocomplete Full Names
        private void AutocompleteFullName()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT StudentFirstNames FROM Students", con);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "Students");

                    AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        col.Add(ds.Tables[0].Rows[i]["StudentFirstNames"].ToString());

                    }
                    txtSearchFullNames.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtSearchFullNames.MaskBox.AutoCompleteCustomSource = col;
                    txtSearchFullNames.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;

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
        //Autocomplete Section/class
        private void AutocompleteSection()
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
                    cmbSectionClass.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSectionClass.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSectionClass.SelectedIndex = -1;
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
        //Autocomplete cmbSection
        private void AutocompletecmbSection()
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
                    cmbSection.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSection.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSection.SelectedIndex = -1;
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
        //Autocomplete Class
        private void AutocompleteClass()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    if (cmbSectionClass.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = cmbCycleClass.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " SectionName").Value = cmbSectionClass.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = cmbCycleClass.Text.Trim();
                    }
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbClassby.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbClassby.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbClassby.SelectedIndex = -1;
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
        //Fill cmbcycle
        private void fillCycle()
        {
            cmbCycleClass.Properties.Items.Clear();
            cmbCycle.Properties.Items.Clear();
            cmbCycleSection.Properties.Items.Clear();
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                cmbCycleClass.Properties.Items.AddRange(new object[]
            {
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") ,
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
            });

                cmbCycle.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") ,
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
                });

                cmbCycleSection.Properties.Items.AddRange(new object[]
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
                cmbCycleClass.Properties.Items.AddRange(new object[]
            {
                LocRM.GetString("strPrePrimary"),
                LocRM.GetString("strPreparatory") ,
                LocRM.GetString("strHighSchoolGET"),
                LocRM.GetString("strHighSchoolFET"),
                LocRM.GetString("strTVETCollege")
            });

                cmbCycle.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("strPrePrimary"),
                LocRM.GetString("strPreparatory") ,
                LocRM.GetString("strHighSchoolGET"),
                LocRM.GetString("strHighSchoolFET"),
                LocRM.GetString("strTVETCollege")
                });

                cmbCycleSection.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("strPrePrimary"),
                LocRM.GetString("strPreparatory") ,
                LocRM.GetString("strHighSchoolGET"),
                LocRM.GetString("strHighSchoolFET"),
                LocRM.GetString("strTVETCollege")
                });
            }
                
        }

        private void gridControlListStudents_MouseClick(object sender, MouseEventArgs e)
        {
             try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                if (gridView1.DataRowCount > 0)
                {
                    txtStudentNumber.Text = txtStudentNumber.Text.TrimEnd();
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "SELECT * FROM Students WHERE StudentNumber = '" + gridView1.GetFocusedRowCellValue(LocRM.GetString("strStudentNo").ToUpper()).ToString() + "'";
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            txtStudentNumber.Text = (rdr.GetString(1).Trim());                           
                            txtCycle.Text = (rdr.GetString(38).Trim());
                            txtSection.Text = (rdr.GetString(2).Trim());
                            txtClass.Text = (rdr.GetString(4).Trim());
                            txtStudentSurname.Text = (rdr.GetString(5).Trim());
                            txtStudentNames.Text = (rdr.GetString(6).Trim());

                            if (!Convert.IsDBNull(rdr["StudentPicture"]))
                            {
                                byte[] x = (byte[])rdr["StudentPicture"];
                                MemoryStream ms = new MemoryStream(x);
                                pictureStudent.Image = Image.FromStream(ms);
                                //   picturePatientImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                            }
                            else
                            {
                                pictureStudent.EditValue = null;
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
       

        private void gridControlListStudents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        studentNumber = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, LocRM.GetString("strStudentNo").ToUpper()).ToString();                        
                        Close();
                        Owner.Show();  //Show the previous form                    
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
        //display submitted documents
        List<int> list = new List<int>();
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            //GridView view = sender as GridView;

            //  if (e.Column.FieldName != "DOCUMENTS SUBMITTED") return;

            //      int dataSourceRowIndex = view.GetDataSourceRowIndex(20);

            //if (list.Contains(dataSourceRowIndex)) return;
            //if (e.DisplayText == "0")
            //{
            //    e.DisplayText = "No document";
            //}
            //if (e.DisplayText == "1")
            //{
            //    e.DisplayText = "Birth Certificate";
            //}
            //if (e.DisplayText == "4")
            //{
            //    e.DisplayText = "Parents IDs";
            //}
            //if (e.DisplayText == "5")
            //{
            //    e.DisplayText = "Birth Certificate and Parents IDs";
            //}
            //if (e.DisplayText == "8")
            //{
            //    e.DisplayText = "Previous school Docs";
            //}
            //if (e.DisplayText == "9")
            //{
            //    e.DisplayText = "Birth Certificate and Previous school Docs";
            //}
            //if (e.DisplayText == "12")
            //{
            //    e.DisplayText = "Parents IDs and Previous school Docs";
            //}
            //if (e.DisplayText == "13")
            //{
            //    e.DisplayText = "All";
            //}


            //Display names in upper case  
            //GridView view = sender as GridView;
            //string columnName = LocRM.GetString("strNameSurname").ToUpper();

            //if (e.Column.FieldName != "columnName") return;

            //int dataSourceRowIndex = view.GetDataSourceRowIndex(1);
            //if (list.Contains(dataSourceRowIndex)) return;
            //// if (e.Column.FieldName == "ProductName" || e.Column.FieldName == "QuantityPerUnit")
            //if (e.Column.FieldName == "columnName")
            //    e.DisplayText = (Convert.ToString(e.Value)).ToUpper();            
        }

        private void comboCycle_SelectedIndexChanged(object sender, EventArgs e)
        {            
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (cmbCycleClass.SelectedIndex >= 3)
                {
                    cmbClassby.Properties.Items.Clear();
                    cmbSectionClass.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClassby.Enabled = true;
                    AutocompleteClass();
                    cmbSectionClass.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }
            else
            {
                if (cmbCycleClass.SelectedIndex >= 4)
                {
                    cmbClassby.Properties.Items.Clear();
                    cmbSectionClass.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClassby.Enabled = true;
                    AutocompleteClass();
                    cmbSectionClass.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }                
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClassby.Enabled = true;
            AutocompleteClass();
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            (e.PrintingSystem as PrintingSystemBase).PageSettings.Landscape = false;
        }

        //GridViewListStudents All students
        private DataTable CreateDataStudentsAllStudents()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));            
            dt.Columns.Add(LocRM.GetString("strSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strName").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strSchoolYear").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strEnrolmentDate").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strCycle").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strSection").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strClass").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strSex").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strDateBirth").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strNotificationNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNotificationEmail").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strEmergencyPhoneNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strAbsencePhoneNo").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strNationality").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strLastSchoolAttended").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strHomeLanguage").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strReligion").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strMedicalConditions").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strSiblingsSchool").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNoSiblings").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strEmail").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strPhoneNumber").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameTutor").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTutorProfession").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strHomeAddress").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strFatherNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strFatherProfession").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strFatherContactNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strFatherEmail").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strMotherNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strMotherProfession").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strMotherContactNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strMotherEmail").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strSchoolActivities").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strSchoolPhotos").ToUpper(), typeof(string));

            return dt;
        }
        //GridViewListStudents Students By Class
        private DataTable CreateDataStudentsByClass()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strDateBirth").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strSex").ToUpper(), typeof(string));                    
            return dt;
        }

        private DataTable CreateDataStudentsBySection()
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt2.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt2.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt2.Columns.Add(LocRM.GetString("strDateBirth").ToUpper(), typeof(string));
            dt2.Columns.Add(LocRM.GetString("strSex").ToUpper(), typeof(string));
            dt2.Columns.Add(LocRM.GetString("strClass").ToUpper(), typeof(string));
            return dt2;
        }
        private DataTable CreateDataStudentsBySchoolYear()
        {
            DataTable dt3 = new DataTable();
            dt3.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strDateBirth").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strSex").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strSchoolYear").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strClass").ToUpper(), typeof(string));
            return dt3;
        }
        private DataTable CreateDataStudentsByCycle()
        {
            DataTable dt3 = new DataTable();
            dt3.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strDateBirth").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strSex").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strSection").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strClass").ToUpper(), typeof(string));
            return dt3;
        }

        private DataTable CreateDataStudentsBySurname()
        {
            DataTable dt3 = new DataTable();
            dt3.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strSurname").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strName").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strDateBirth").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strSex").ToUpper(), typeof(string));            
            dt3.Columns.Add(LocRM.GetString("strClass").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strContactNo").ToUpper(), typeof(string));
            dt3.Columns.Add(LocRM.GetString("strHomeAddress").ToUpper(), typeof(string));
            return dt3;
        }
        private void filterStudentsAll()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,SchoolYear, " +
                        "EnrolmentDate,Cycle, Section, Class,Gender, DateBirth, NotificationNo," +
                        "NotificationEmail,EmergencyPhoneNo,AbsencePhoneNo,Nationality,LastSchoolAttended," +
                        "HomeLanguage,Religion,MedicalAllergies,StudentSibling,SiblingsNo," +
                        "StudentEmail,StudentPhoneNumber,TutorName,TutorProfession,HomeAddress," +
                        "FatherSurname,FatherNames,FatherProfession,FatherContactNo,FatherEmail," +
                        "MotherSurname,MotherNames,MotherProfession,MotherContactNo,MotherEmail," +
                        "SchoolActivities,SchoolPhotos FROM Students order by StudentNumber";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;                    

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }

                        // gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[1].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["StudentFirstNames"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strName").ToUpper()], rdr[2].ToString().ToUpper());
                        }

                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSchoolYear").ToUpper()], rdr[3].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["EnrolmentDate"]))
                        {
                            DateTime dtEnrolment = (DateTime)rdr.GetValue(4);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strEnrolmentDate").ToUpper()], dtEnrolment.ToString("dd/MM/yyyy"));
                        }
                        
                        if (!Convert.IsDBNull(rdr["Cycle"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCycle").ToUpper()], rdr[5].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["Section"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSection").ToUpper()], rdr[6].ToString().ToUpper());

                        }
                        
                        if (!Convert.IsDBNull(rdr["Class"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[7].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[8].ToString().ToUpper());
                        }
                        
                        
                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dtBirth = (DateTime)rdr.GetValue(9);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dtBirth.ToString("dd/MM/yyyy"));
                        }
                        
                        if (!Convert.IsDBNull(rdr["NotificationNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNotificationNo").ToUpper()], rdr[10].ToString().ToUpper());
                        }
                       
                        if (!Convert.IsDBNull(rdr["NotificationEmail"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNotificationEmail").ToUpper()], rdr[11].ToString().ToUpper());
                        }
                       
                        if (!Convert.IsDBNull(rdr["EmergencyPhoneNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strEmergencyPhoneNo").ToUpper()], rdr[12].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["AbsencePhoneNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strAbsencePhoneNo").ToUpper()], rdr[13].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["Nationality"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNationality").ToUpper()], rdr[14].ToString().ToUpper());
                        }
                       
                        if (!Convert.IsDBNull(rdr["LastSchoolAttended"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strLastSchoolAttended").ToUpper()], rdr[15].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["HomeLanguage"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strHomeLanguage").ToUpper()], rdr[16].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["Religion"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strReligion").ToUpper()], rdr[17].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["MedicalAllergies"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strMedicalConditions").ToUpper()], rdr[18].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["StudentSibling"]))
                        {
                            string haveSiblingsSchool = (rdr.GetString(19).Trim());
                            if (haveSiblingsSchool == "Yes")
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSiblingsSchool").ToUpper()], LocRM.GetString("strYes").ToUpper());
                            }
                            else
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSiblingsSchool").ToUpper()], LocRM.GetString("strNoYes").ToUpper());
                            }
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNoSiblings").ToUpper()], rdr[20].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["StudentEmail"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strEmail").ToUpper()], rdr[21].ToString().ToUpper());
                        }
                       
                        if (!Convert.IsDBNull(rdr["StudentPhoneNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPhoneNumber").ToUpper()], rdr[22].ToString().ToUpper());
                        }
                       
                        
                        if (!Convert.IsDBNull(rdr["TutorName"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameTutor").ToUpper()], rdr[23].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["TutorProfession"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTutorProfession").ToUpper()], rdr[24].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["HomeAddress"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strHomeAddress").ToUpper()], rdr[25].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["FatherSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strFatherNameSurname").ToUpper()], rdr[26].ToString().ToUpper() + " " + rdr[27].ToString().ToUpper());
                        }
                       
                        if (!Convert.IsDBNull(rdr["FatherProfession"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strFatherProfession").ToUpper()], rdr[28].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["FatherContactNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strFatherContactNo").ToUpper()], rdr[29].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["FatherEmail"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strFatherEmail").ToUpper()], rdr[30].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["MotherSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strMotherNameSurname").ToUpper()], rdr[31].ToString().ToUpper() + " " + rdr[32].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["MotherProfession"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strMotherProfession").ToUpper()], rdr[33].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["MotherContactNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strMotherContactNo").ToUpper()], rdr[34].ToString().ToUpper());
                        }
                        
                        if (!Convert.IsDBNull(rdr["MotherEmail"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strMotherEmail").ToUpper()], rdr[35].ToString().ToUpper());
                        }
                       

                        if (!Convert.IsDBNull(rdr["SchoolActivities"]))
                        {
                            string SchoolActivities = (rdr.GetString(36).Trim());
                            if (SchoolActivities == "Yes")
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSchoolActivities").ToUpper()], LocRM.GetString("strYes").ToUpper());
                            }
                            else
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSchoolActivities").ToUpper()], LocRM.GetString("strNoYes").ToUpper());
                            }
                        }
                        
                        if (!Convert.IsDBNull(rdr["SchoolPhotos"]))
                        {
                            string SchoolPhotos = (rdr.GetString(37).Trim());
                            if (SchoolPhotos == "Yes")
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSchoolPhotos").ToUpper()], LocRM.GetString("strYes").ToUpper());
                            }
                            else
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSchoolPhotos").ToUpper()], LocRM.GetString("strNoYes").ToUpper());
                            }
                        }
                                              
                        
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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

        string schoolYear = "";
        private void filterStudentsByClass()
        {            
            try
            {             

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                } 

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,DateBirth, Gender,Class,SchoolYear " +
                        "FROM Students where Class=@d1 and SchoolYear=@d2 order by StudentSurname, StudentNumber";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbClassby.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbSchoolYearClass.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        }

                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dt = (DateTime)rdr.GetValue(3);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        }
                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[4].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            schoolYear = rdr[6].ToString();
                        }
                        
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void filterStudentsBySection()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,DateBirth, Gender,Class,SchoolYear, " +
                        "Section FROM Students where Section=@d1 and SchoolYear=@d2 " +
                        "order by Class, StudentSurname, StudentNumber ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSection.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbSchoolYearFieldStudy.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }

                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        }
                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dt = (DateTime)rdr.GetValue(3);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        }
                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[4].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            schoolYear = rdr[6].ToString();
                        }
                        
                        if (!Convert.IsDBNull(rdr["Class"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[5].ToString());
                        }
                        
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        //Filter by school year
        private void filterStudentsBySchoolYear()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,DateBirth, Gender,Class,SchoolYear, " +
                        "Cycle FROM Students where SchoolYear=@d1 " +
                        "order by Cycle, Class, StudentSurname, StudentNumber";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYear.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        }
                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dt = (DateTime)rdr.GetValue(3);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        }
                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[4].ToString());
                        }
                        
                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSchoolYear").ToUpper()], rdr[6].ToString());
                            schoolYear = rdr[6].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["Class"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[5].ToString());
                        }                       

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void filterStudentsByCycle()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,DateBirth, Gender,Class,SchoolYear, " +
                        "Section, Cycle FROM Students where Cycle=@d1 and SchoolYear=@d2 " +
                        "order by Section, Class, StudentSurname, StudentNumber";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbCycle.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbSchoolYearCycle.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        }
                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dt = (DateTime)rdr.GetValue(3);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        }
                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[4].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            schoolYear = rdr[6].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["Class"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[5].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["Section"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSection").ToUpper()], rdr[7].ToString());
                        }
                        
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void filterStudentsBySurname()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,DateBirth, Gender,Class,SchoolYear, " +
                        "Cycle, EmergencyPhoneNo, HomeAddress FROM Students " +
                        "where StudentSurname=@d1 and SchoolYear=@d2 order by StudentSurname, Class, StudentNumber";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtSearchSurname.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbSchoolYearSurname.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                       
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }
                        
                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[1].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentFirstNames"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strName").ToUpper()], rdr[2].ToString());
                        }
                       
                        
                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dt = (DateTime)rdr.GetValue(3);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        }
                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[4].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            schoolYear = rdr[6].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["Class"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[5].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["Cycle"]))
                        {
                            section = rdr[7].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["EmergencyPhoneNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strContactNo").ToUpper()], rdr[8].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["HomeAddress"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strHomeAddress").ToUpper()], rdr[9].ToString());
                        }
                       
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void filterStudentsByName()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,DateBirth, Gender,Class,SchoolYear, " +
                        "Cycle, EmergencyPhoneNo, HomeAddress FROM Students " +
                        "where StudentFirstNames=@d1 and SchoolYear=@d2 order by StudentSurname, Class, StudentNumber";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtSearchFullNames.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbSchoolYearName.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[1].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentFirstNames"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strName").ToUpper()], rdr[2].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dt = (DateTime)rdr.GetValue(3);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        }

                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[4].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            schoolYear = rdr[6].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["Class"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[5].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["Cycle"]))
                        {
                            section = rdr[7].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["EmergencyPhoneNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strContactNo").ToUpper()], rdr[8].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["HomeAddress"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strHomeAddress").ToUpper()], rdr[9].ToString());
                        }                        
                        
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void filterStudentsByEnrolmentDate()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT StudentNumber,StudentSurname,StudentFirstNames,DateBirth, Gender,Class,SchoolYear, " +
                        "Cycle, EmergencyPhoneNo, HomeAddress,EnrolmentDate FROM Students " +
                        "where EnrolmentDate between @date1 and @date2 and SchoolYear=@d2 order by EnrolmentDate, " +
                        "Cycle, Class, StudentSurname";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    //cmd.Parameters.AddWithValue("@d1", txtSearchFullNames.Text.Trim());
                   
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " EnrolmentDate").Value = dtSearchbyDateFrom.EditValue;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " EnrolmentDate").Value = dtSearchbyDateTo.EditValue;
                    cmd.Parameters.AddWithValue("@d2", cmbSchoolYearEnrolmentYear.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        
                        if (!Convert.IsDBNull(rdr["StudentNumber"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentSurname"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[1].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["StudentFirstNames"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strName").ToUpper()], rdr[2].ToString());
                        }

                        if (!Convert.IsDBNull(rdr["DateBirth"]))
                        {
                            DateTime dt = (DateTime)rdr.GetValue(3);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateBirth").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        }
                        if (!Convert.IsDBNull(rdr["Gender"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSex").ToUpper()], rdr[4].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["SchoolYear"]))
                        {
                            schoolYear = rdr[6].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["Class"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[5].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["Cycle"]))
                        {
                            section = rdr[7].ToString();
                        }
                        if (!Convert.IsDBNull(rdr["EmergencyPhoneNo"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strContactNo").ToUpper()], rdr[8].ToString());
                        }
                        if (!Convert.IsDBNull(rdr["HomeAddress"]))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strHomeAddress").ToUpper()], rdr[9].ToString());
                        } 
                        
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void reportStudentByClassExcel()
        {
            if (gridView1.DataRowCount > 0)
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    //gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                     

                    // Create a new report instance.
                    reportListStudents report = new reportListStudents();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlListStudents;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Rename worksheet
                        // Get its XLS export options.
                     //   XlsxExportOptions xlsOptions = report.ExportOptions.Xlsx;

                        report.CreateDocument(false);
                       // report.CreateDocument();
                        //report.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        string fileName = saveFileDialog.FileName + ".xlsx";

                        if (searchBy == 1)
                        {
                            //Export to excel
                         //   xlsOptions.SheetName =  cmbClassby.Text.Trim();
                         //   xlsOptions.ExportMode = XlsxExportMode.SingleFile;
                           // report.ExportToXlsx(fileName);
                            report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = cmbClassby.Text.Trim() });
                        }
                        else if (searchBy == 2)
                        {
                            //Export to excel
                            report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = cmbSection.Text.Trim() });
                        }
                        else if (searchBy == 3)
                        {
                            //Export to excel
                            //xlsOptions.SheetName = cmbCycle.Text.Trim();
                         //   xlsOptions.SheetName = "test sheet";
                         //   xlsOptions.ExportMode = XlsxExportMode.SingleFile;
                            //report.ExportToXlsx(fileName);
                            //Export to excel
                            report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = cmbCycle.Text.Trim() }); 
                        }
                        else if (searchBy == 4)
                        {
                            //Export to excel
                            report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = txtSearchSurname.Text.Trim() }); 
                        }
                        else if (searchBy == 5)
                        {
                            //Export to excel
                            report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = txtSearchFullNames.Text.Trim() });
                        }
                        else if (searchBy == 6)
                        {
                            //Export to excel
                            report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = LocRM.GetString("strEnrollmentListByDate") });
                        }

                        //PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated; 
                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strListStudents"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (splashScreenManager1.IsSplashFormVisible == false)
                            {
                                splashScreenManager1.ShowWaitForm();
                            }
                            Process.Start(fileName);
                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                        }
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
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void reportStudentByClassPDF()
        {
            if (gridView1.DataRowCount > 0)
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                   // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                    reportListStudents report = new reportListStudents();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlListStudents;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        
                        string fileName = saveFileDialog.FileName + ".pdf";
                        //Export to pdf
                        report.ExportToPdf(fileName);

                       
                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strListStudents"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (splashScreenManager1.IsSplashFormVisible == false)
                            {
                                splashScreenManager1.ShowWaitForm();
                            }
                            Process.Start(fileName);
                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                        }
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
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void reportStudentByClassWord()
        {
            if (gridView1.DataRowCount > 0)
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    //gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                    reportListStudents report = new reportListStudents();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlListStudents;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        string fileName = saveFileDialog.FileName + ".docx";
                        //Export to pdf
                        report.ExportToDocx(fileName);


                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strListStudents"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (splashScreenManager1.IsSplashFormVisible == false)
                            {
                                splashScreenManager1.ShowWaitForm();
                            }
                            Process.Start(fileName);
                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                        }
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
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
                reportStudentByClassExcel();
        }
        

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
              reportStudentByClassPDF();
        }

        private void btnExportWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                reportStudentByClassWord();
            
        }

        private void btnSearchByCycle_Click(object sender, EventArgs e)
        {
            if (cmbSchoolYearCycle.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYearCycle.Focus();
                return;
            }

            if (cmbCycle.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterCycle"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCycle.Focus();
                return;
            }

            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();
            

            //gridView1.RefreshData();
            //load culumns in gridControlFeeInfo
            gridControlListStudents.DataSource = CreateDataStudentsByCycle();
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            filterStudentsByCycle();
            CalculateNoStudentsCycle();

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            //clear search fields
            //cmbSchoolYear.SelectedIndex = -1;
            txtSearchSurname.Text = "";
            txtSearchFullNames.Text = "";
            cmbSectionClass.Enabled = false;
            cmbClassby.Enabled = false;
            cmbSection.Enabled = false;
            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;
            dtSearchbyDateFrom.EditValue = DateTime.Today;
            dtSearchbyDateTo.EditValue = DateTime.Today;
            

            //clear Detail Fields
            clearDetailFields();

            //Set Report details
            SchoolYear = schoolYear;
            section = cmbCycle.Text;
            studentSearchBy = 3;
            searchBy = 3;
        }

        private void btnbtnFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControlFilterBy.Enabled = true;
            //    groupSurname.Enabled = true;
            //    groupNames.Enabled = true;
            //    groupSectionClass.Enabled = true;
            ////    groupCycle.Enabled = true;
            //    groupSection.Enabled = true;
            //    groupDates.Enabled = true;
            //Autocomplete
            AutocompleteSurname();
            AutocompleteFullName();
            fillCycle();
            AutocompleteAcademicYear();
        }
        //Get the current school year
        private void AutocompleteAcademicYear()
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
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SchoolYear) FROM AcademicYear", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbSchoolYear.Properties.Items.Clear();
                    cmbSchoolYearCycle.Properties.Items.Clear();
                    cmbSchoolYearFieldStudy.Properties.Items.Clear();
                    cmbSchoolYearClass.Properties.Items.Clear();
                    cmbSchoolYearSurname.Properties.Items.Clear();
                    cmbSchoolYearName.Properties.Items.Clear();
                    cmbSchoolYearEnrolmentYear.Properties.Items.Clear();
                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSchoolYear.Properties.Items.Add(drow[0].ToString());
                        cmbSchoolYearCycle.Properties.Items.Add(drow[0].ToString());
                        cmbSchoolYearFieldStudy.Properties.Items.Add(drow[0].ToString());
                        cmbSchoolYearClass.Properties.Items.Add(drow[0].ToString());
                        cmbSchoolYearSurname.Properties.Items.Add(drow[0].ToString());
                        cmbSchoolYearName.Properties.Items.Add(drow[0].ToString());
                        cmbSchoolYearEnrolmentYear.Properties.Items.Add(drow[0].ToString());
                    }
                    if (cmbSchoolYear.Properties.Items.Count > 0)
                    {
                        cmbSchoolYear.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1; //Select current school year.
                        cmbSchoolYearCycle.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1;
                        cmbSchoolYearFieldStudy.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1;
                        cmbSchoolYearClass.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1;
                        cmbSchoolYearSurname.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1;
                        cmbSchoolYearName.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1;
                        cmbSchoolYearEnrolmentYear.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1;
                    }
                    //cmbSchoolYear.SelectedIndex = -1;
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
        private void btnbtnResetList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = false; //Make widh wide for column with many columns, with horizontal scrollbar

            gridControlListStudents.DataSource = CreateDataStudentsAllStudents();
            clearControls();
            ribbonPageGroupPrint.Enabled = false;
            ribbonPageGroupExport.Enabled = false;
        }

        private void cmbCycleSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.SelectedIndex = -1;           
            
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (cmbCycleSection.SelectedIndex >=3)
                {
                    cmbSection.Enabled = true;
                    AutocompletecmbSection();
                }
                else
                {
                    cmbSection.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }
            else
            {
                if (cmbCycleSection.SelectedIndex >= 4)
                {
                    cmbSection.Enabled = true;
                    AutocompletecmbSection();
                }
                else
                {
                    cmbSection.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }
        }

        private void btnSearchBySection_Click(object sender, EventArgs e)
        {
            if (cmbSchoolYearFieldStudy.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYearFieldStudy.Focus();
                return;
            }

            if (cmbSection.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSection"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }

            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();
           

            // gridControlListStudents.RefreshDataSource();
            //load culumns in gridControlFeeInfo
            gridControlListStudents.DataSource = CreateDataStudentsBySection();
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            filterStudentsBySection();
            CalculateNoStudentsSection();

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            //clear search fields
           // cmbSchoolYear.SelectedIndex = -1;
            txtSearchSurname.Text = "";
            txtSearchFullNames.Text = "";
            cmbCycle.SelectedIndex = -1;
            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.Enabled = false;
            cmbClassby.Enabled = false;
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;
            dtSearchbyDateFrom.EditValue = DateTime.Today;
            dtSearchbyDateTo.EditValue = DateTime.Today;
            

            //clear Detail Fields
            clearDetailFields();

            //Set Report details
            section = cmbSection.Text;            
            SchoolYear = schoolYear;
            studentSearchBy = 2;
            searchBy = 2;
        }

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                // Create a new report instance.
                reportListStudents report = new reportListStudents();

                // Link the required control with the PrintableComponentContainers of a report.
                report.printableComponentContainer1.PrintableComponent = gridControlListStudents;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                report.Landscape = false;

                // Invoke a Print Preview for the created report document. 
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();                
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPreview"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                try
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                    }

                    // XtraReport.PrinterName
                    // Create a new report instance.
                    reportListStudents report = new reportListStudents();
                    PrinterSettings printerSettings = new PrinterSettings();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlListStudents;

                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report.Landscape = false;

                    // Specify the PrinterName if the target printer is not the default one.
                    printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;                    

                    // Invoke a Print Preview for the created report document. 
                    ReportPrintTool printTool = new ReportPrintTool(report);
                    // preview.ShowRibbonPreview();
                    printTool.Print(Properties.Settings.Default.ReportPrinter);
                    
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

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPrint"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadStudents_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = false; //Make widh wide for column with many columns, with horizontal scrollbar

            gridControlListStudents.DataSource = CreateDataStudentsAllStudents();

            filterStudentsAll();
            CalculateNoStudentsAll();

            clearControls();
            ribbonPageGroupPrint.Enabled = false;
            ribbonPageGroupExport.Enabled = false;
        }

        private void btnSearchBySchoolYear_Click(object sender, EventArgs e)
        {
            if (cmbSchoolYear.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYear.Focus();
                return;
            }

            //clear gridcontrol
            gridControlListStudents.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();


            //gridView1.RefreshData();
            //load culumns 
            gridControlListStudents.DataSource = CreateDataStudentsBySchoolYear();
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            filterStudentsBySchoolYear();
            CalculateNoStudentsCycle();

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            //clear search fields
            cmbCycle.SelectedIndex = -1;
            txtSearchSurname.Text = "";
            txtSearchFullNames.Text = "";
            cmbSectionClass.Enabled = false;
            cmbClassby.Enabled = false;
            cmbSection.Enabled = false;
            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.SelectedIndex = -1;
            cmbClassby.SelectedIndex = -1;
            dtSearchbyDateFrom.EditValue = DateTime.Today;
            dtSearchbyDateTo.EditValue = DateTime.Today;


            //clear Detail Fields
            clearDetailFields();

            //Set Report details 
            SchoolYear = schoolYear;
            studentSearchBy = 7;
            searchBy = 7;
        }
    }
}