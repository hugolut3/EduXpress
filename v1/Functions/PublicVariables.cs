using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;


namespace EduXpress.Functions
{
    // public static class PublicVariables
    public class PublicVariables
    {

        public const string ProgramTitle = "EduXpress Système de gestion scolaire";

        public static string userLogged = "Personne n'est connecté";

        public static string UserLogged
        {
            get { return userLogged; }
            set { userLogged = value; }
        }
        private static string userType = "";
        public static string UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        // public string UserLoggedSurname { get; set; }
        private static string userLoggedSurname = "";
        public static string UserLoggedSurname   //used 80 reference
        {
            get { return userLoggedSurname; }
            set { userLoggedSurname = value; }
        }
        private static string userLoggedName = "";
        public static string UserLoggedName
        {
            get { return userLoggedName; }
            set { userLoggedName = value; }
        }

        private static bool Authenticated = false;
        public static bool authenticated
        {
            get { return Authenticated; }
            set { Authenticated = value; }
        }
        private static bool LoginCanceled = false;
        public static bool loginCanceled
        {
            get { return LoginCanceled; }
            set { LoginCanceled = value; }
        }
        private static bool changePassword = false;
        public static bool ChangePassword
        {
            get { return changePassword; }
            set { changePassword = value; }
        }
        private static int logAuthenticated = 0;
        public static int LogAuthenticated
        {
            get { return logAuthenticated; }
            set { logAuthenticated = value; }
        }
        //Role descriptions
        #region RoleDescriptions
        //Role == 0 No one logged in
        //Role == 1 Administrator
        //Role == 2 Administrator Assistant
        //Role == 3 Accountant
        //Role == 4 Account & Admission
        //Role == 5 Account & HR
        //Role == 6 Account, Admission & HR
        //Role == 7 Admission Officer
        //Role == 8 Human Resources
        //Role == 9 Librarian
        //Role == 10 Librarian & Admission
        //Role == 11 Stock Clerk
        //Role == 12 Teacher
        #endregion
        private static int role = 0;
        public static int Role
        {
            get { return role; }
            set { role = value; }
        }

        //Student Enrolment
        private static string StudentNumber = "";
        public static string studentNumber
        {
            get { return StudentNumber; }
            set { StudentNumber = value; }
        }
        //Student Fee payment
        private static string ReceiptNumber = "";
        public static string receiptNumber
        {
            get { return ReceiptNumber; }
            set { ReceiptNumber = value; }
        }
        private static string PayemntID = "";
        public static string payemntID
        {
            get { return PayemntID; }
            set { PayemntID = value; }
        }
        private static string PaymentMonths = "";
        public static string paymentMonths
        {
            get { return PaymentMonths; }
            set { PaymentMonths = value; }
        }

        private static string PaymentClassName = "";
        public static string paymentClassName
        {
            get { return PaymentClassName; }
            set { PaymentClassName = value; }
        }

        private static string EnrolmentDate = "";
        public static string enrolmentDate
        {
            get { return EnrolmentDate; }
            set { EnrolmentDate = value; }
        }
        //Customer Contact email
        private static string ContactEmail = "";
        public static string contactEmail
        {
            get { return ContactEmail; }
            set { ContactEmail = value; }
        }

        //Customer Address
        private static string CustomerAddress = "";
        public static string customerAddress
        {
            get { return CustomerAddress; }
            set { CustomerAddress = value; }
        }

        //Customer City
        private static string CustomerCity = "";
        public static string customerCity
        {
            get { return CustomerCity; }
            set { CustomerCity = value; }
        }

        //Customer Postal Code
        private static string CustomerPostalCode = "";
        public static string customerPostalCode
        {
            get { return CustomerPostalCode; }
            set { CustomerPostalCode = value; }
        }

        //Products public variables
        private static string ProductID = "";
        public static string productID
        {
            get { return ProductID; }
            set { ProductID = value; }
        }

        private static string ProductCode = "";
        public static string productCode
        {
            get { return ProductCode; }
            set { ProductCode = value; }
        }

        private static string ProductName = "";
        public static string productName
        {
            get { return ProductName; }
            set { ProductName = value; }
        }

        private static string Price = "";
        public static string price
        {
            get { return Price; }
            set { Price = value; }
        }

        private static string GrandTotal = "";
        public static string grandTotal
        {
            get { return GrandTotal; }
            set { GrandTotal = value; }
        }

        private static string Quantity = "";
        public static string quantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }

        private static string Amount = "";
        public static string amount
        {
            get { return Amount; }
            set { Amount = value; }
        }

        private static string Remarks = "";
        public static string remarks
        {
            get { return Remarks; }
            set { Remarks = value; }
        }

        //Company Profile
        private static string CompanyName = "";
        public static string companyName
        {
            get { return CompanyName; }
            set { CompanyName = value; }
        }

        private static string CompanyTelephone = "";
        public static string companyTelephone
        {
            get { return CompanyTelephone; }
            set { CompanyTelephone = value; }
        }

        private static string CompanyEmail = "";
        public static string companyEmail
        {
            get { return CompanyEmail; }
            set { CompanyEmail = value; }
        }

        private static string CompanyAddress = "";
        public static string companyAddress
        {
            get { return CompanyAddress; }
            set { CompanyAddress = value; }
        }

        private static string CompanyPostalAddress = "";
        public static string companyPostalAddress
        {
            get { return CompanyPostalAddress; }
            set { CompanyPostalAddress = value; }
        }


        private static string CompanyVAT = "";
        public static string companyVAT
        {
            get { return CompanyVAT; }
            set { CompanyVAT = value; }
        }

        private static string Currency = "";
        public static string currency
        {
            get { return Currency; }
            set { Currency = value; }
        }


        
        private static string Section = "";
        public static string section
        {
            get { return Section; }
            set { Section = value; }
        }

        private static string EducationCycle = "";
        public static string educationCycle
        {
            get { return EducationCycle; }
            set { EducationCycle = value; }
        }
        private static string ClassName = "";
        public static string className
        {
            get { return ClassName; }
            set { ClassName = value; }
        }

        private static string SQLServerStatus = "";
        public static string sqlServerStatus
        {
            get { return SQLServerStatus; }
            set { SQLServerStatus = value; }
        }

        //School name/class/period: school fee report
        private static string _schoolYear;

        public static string SchoolYear
        {
            get { return _schoolYear; }
            set { _schoolYear = value; }
        }

        private static string _schoolClass;

        public static string SchoolClass
        {
            get { return _schoolClass; }
            set { _schoolClass = value; }
        }
        private static string _feePeriod;

        public static string FeePeriod
        {
            get { return _feePeriod; }
            set { _feePeriod = value; }
        }
        private static bool _isFeesDuePercentage;

        public static bool IsFeesDuePercentage
        {
            get { return _isFeesDuePercentage; }
            set { _isFeesDuePercentage = value; }
        }
        private static bool _feesDueFull;

        public static bool FeesDueFull
        {
            get { return _feesDueFull; }
            set { _feesDueFull = value; }
        }
        private static int _percentageFeesDue;

        public static int PercentageFeesDue
        {
            get { return _percentageFeesDue; }
            set { _percentageFeesDue = value; }
        }
        private static string _notificationNo;

        public static string NotificationNo
        {
            get { return _notificationNo; }
            set { _notificationNo = value; }
        }

        private static int StudentSearchBy ;
        public static int studentSearchBy
        {
            get { return StudentSearchBy; }
            set { StudentSearchBy = value; }
        }

        private static string StudentSurName;
        public static string studentSurName
        {
            get { return StudentSurName; }
            set { StudentSurName = value; }
        }

        private static string StudentName;
        public static string studentName
        {
            get { return StudentName; }
            set { StudentName = value; }
        }

        private static string SearchbyDateFrom;
        public static string searchbyDateFrom
        {
            get { return SearchbyDateFrom; }
            set { SearchbyDateFrom = value; }
        }

        private static string SearchbyDateTo;
        public static string searchbyDateTo
        {
            get { return SearchbyDateTo; }
            set { SearchbyDateTo = value; }
        }

        private static string SearchbyCashier;
        public static string searchbyCashier
        {
            get { return SearchbyCashier; }
            set { SearchbyCashier = value; }
        }
        //License modules
        private static bool EduxpressLiteStudent = false;
        public static bool eduxpressLiteStudent
        {
            get { return EduxpressLiteStudent; }
            set { EduxpressLiteStudent = value; }
        }
        private static string licenseFeature = "";
        public static string LicenseFeature
        {
            get { return licenseFeature; }
            set { licenseFeature = value; }
            //Trial Version        :0 
            //Express Version      :1
            //Standard Version     :2
            //Professional Version :3
            //Lite Version         :4
            //Ultimate Version     :5
        }

        //Calculate bulletins
        private static int MaximaSubjectGlobalVariable;
        public static int maximaSubjectGlobalVariable
        {
            get { return MaximaSubjectGlobalVariable; }
            set { MaximaSubjectGlobalVariable = value; }
        }
        private static int NumberStudentsGlobalVariable;
        public static int numberStudentsGlobalVariable
        {
            get { return NumberStudentsGlobalVariable; }
            set { NumberStudentsGlobalVariable = value; }
        }
        private static int TotalPointsGlobalVariable;
        public static int totalPointsGlobalVariable
        {
            get { return TotalPointsGlobalVariable; }
            set { TotalPointsGlobalVariable = value; }
        }
        
        private static string TotalAvaragePercClassGlobalVariable;
        public static string totalAvaragePercClassGlobalVariable
        {
            get { return TotalAvaragePercClassGlobalVariable; }
            set { TotalAvaragePercClassGlobalVariable = value; }
        }

        private static string TotalAvaragePerClassSemTrimGlobalVariable;
        public static string totalAvaragePerClassSemTrimGlobalVariable
        {
            get { return TotalAvaragePerClassSemTrimGlobalVariable; }
            set { TotalAvaragePerClassSemTrimGlobalVariable = value; }
        }

        private static string TotalAvaragePerClassTotGenGlobalVariable; 
        public static string totalAvaragePerClassTotGenGlobalVariable  //Avarage percentage of total general.
        {
            get { return TotalAvaragePerClassTotGenGlobalVariable; }
            set { TotalAvaragePerClassTotGenGlobalVariable = value; }
        }

        private static string AssessmentPeriodGlobalVariable;
        public static string assessmentPeriodGlobalVariable
        {
            get { return AssessmentPeriodGlobalVariable; }
            set { AssessmentPeriodGlobalVariable = value; }
        }

        private static string TeacherGlobalVariable;
        public static string teacherGlobalVariable
        {
            get { return TeacherGlobalVariable; }
            set { TeacherGlobalVariable = value; }
        }
        private static string SubjectGlobalVariable;
        public static string subjectGlobalVariable
        {
            get { return SubjectGlobalVariable; }
            set { SubjectGlobalVariable = value; }  
        }
        private static int PassedGlobalVariable;
        public static int passedGlobalVariable
        {
            get { return PassedGlobalVariable; }
            set { PassedGlobalVariable = value; }
            
        }
        private static bool IsSemesterGlobalVariable;
        public static bool isSemesterGlobalVariable
        {
            get { return IsSemesterGlobalVariable; }
            set { IsSemesterGlobalVariable = value; }

        }
        private static bool UseSchoolStampGlobalVariable;
        public static bool useSchoolStampGlobalVariable
        {
            get { return UseSchoolStampGlobalVariable; }
            set { UseSchoolStampGlobalVariable = value; }

        }
        private static bool AddTimeBulletinGlobalVariable;
        public static bool addTimeBulletinGlobalVariable
        {
            get { return AddTimeBulletinGlobalVariable; }
            set { AddTimeBulletinGlobalVariable = value; }

        }
        private static string ReportDateGlobalVariable;
        public static string reportDateGlobalVariable
        {
            get { return ReportDateGlobalVariable; }
            set { ReportDateGlobalVariable = value; }

        }
        private static string SubjectTotAvarageGlobalVariable;
        public static string subjectTotAvarageGlobalVariable
        {
            get { return SubjectTotAvarageGlobalVariable; }
            set { SubjectTotAvarageGlobalVariable = value; }

        }
        private static bool StudentPhotoGlobalVariable;
        public static bool studentPhotoGlobalVariable
        {
            get { return StudentPhotoGlobalVariable; }
            set { StudentPhotoGlobalVariable = value; }

        }

        private static string ConduiteGlobalVariable;
        public static string conduiteGlobalVariable
        {
            get { return ConduiteGlobalVariable; }
            set { ConduiteGlobalVariable = value; }

        }
        private static string ApplicationGlobalVariable;
        public static string applicationGlobalVariable
        {
            get { return ApplicationGlobalVariable; }
            set { ApplicationGlobalVariable = value; }

        }

        private static string PlaceGlobalVariable;
        public static string placeGlobalVariable
        {
            get { return PlaceGlobalVariable; }
            set { PlaceGlobalVariable = value; }

        }
        private static string PercentageGlobalVariable;
        public static string percentageGlobalVariable
        {
            get { return PercentageGlobalVariable; }
            set { PercentageGlobalVariable = value; }
        }

        private static string TotauxGlobalVariable;
        public static string totauxGlobalVariable
        {
            get { return TotauxGlobalVariable; }
            set { TotauxGlobalVariable = value; }
        }
        private static string MaximaGenerauxGlobalVariable;
        public static string maximaGenerauxGlobalVariable
        {
            get { return MaximaGenerauxGlobalVariable; }
            set { MaximaGenerauxGlobalVariable = value; }
        }

        private static string[,] PointArrayGlobalVariable = new string[40,15];
        public static string [,] pointArrayGlobalVariable
        {
            get { return PointArrayGlobalVariable; }
            set { PointArrayGlobalVariable = value; }
        }

        private static bool IsViewTemplateGlobalVariable;
        public static bool isViewTemplateGlobalVariable
        {
            get { return IsViewTemplateGlobalVariable; }
            set { IsViewTemplateGlobalVariable = value; }

        }
    }
    
}
