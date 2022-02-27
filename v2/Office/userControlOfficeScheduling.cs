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
using DevExpress.XtraScheduler;
using System.Data.SqlClient;
using System.Resources;

namespace EduXpress.Office
{
    public partial class userControlOfficeScheduling : DevExpress.XtraEditors.XtraUserControl
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        DataSet eduXpressDatabaseDataSet;
        SqlDataAdapter AppointmentDataAdapter;
        SqlDataAdapter ResourceDataAdapter;
        SqlConnection DXSchedulerConn;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlOfficeScheduling).Assembly);


        public userControlOfficeScheduling()
        {
            InitializeComponent();
            this.schedulerDataStorage1.AppointmentsInserted += OnApptChangedInsertedDeleted;
            this.schedulerDataStorage1.AppointmentsChanged += OnApptChangedInsertedDeleted;
            this.schedulerDataStorage1.AppointmentsDeleted += OnApptChangedInsertedDeleted;

            // schedulerControl1.Start = System.DateTime.Now;
            schedulerControl1.WorkWeekView.TimeIndicatorDisplayOptions.Visibility = TimeIndicatorVisibility.CurrentDate;
            schedulerControl1.WorkWeekView.TimeIndicatorDisplayOptions.ShowOverAppointment = true;
        }

        private void userControlOfficeScheduling_Load(object sender, EventArgs e)
        {
            try
            {
            this.schedulerDataStorage1.Appointments.ResourceSharing = true;
                // Disable data grouping.
                this.schedulerControl1.GroupType = SchedulerGroupType.None;
                // Group data by dates.
                // this.schedulerControl1.GroupType = SchedulerGroupType.Date;
                // Group data by resources.
                //this.schedulerControl1.GroupType = SchedulerGroupType.Resource;

                this.schedulerControl1.Start = DateTime.Today;

            eduXpressDatabaseDataSet = new DataSet();
            string selectAppointments = "SELECT * FROM Appointments";
            string selectResources = "SELECT * FROM resources";

            DXSchedulerConn = new SqlConnection(databaseConnectionString);
            DXSchedulerConn.Open();

            AppointmentDataAdapter = new SqlDataAdapter(selectAppointments, DXSchedulerConn);
            // Subscribe to RowUpdated event to retrieve identity value for an inserted row.
            AppointmentDataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(AppointmentDataAdapter_RowUpdated);
           
            AppointmentDataAdapter.Fill(eduXpressDatabaseDataSet, "Appointments");

            ResourceDataAdapter = new SqlDataAdapter(selectResources, DXSchedulerConn);
            ResourceDataAdapter.Fill(eduXpressDatabaseDataSet, "Resources");

            // Specify mappings.
               MapAppointmentData();
               MapResourceData();

            // Generate commands using CommandBuilder.  
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(AppointmentDataAdapter);
            AppointmentDataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
            AppointmentDataAdapter.DeleteCommand = cmdBuilder.GetDeleteCommand();
            AppointmentDataAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand();

            DXSchedulerConn.Close();

            this.schedulerDataStorage1.Appointments.DataSource = eduXpressDatabaseDataSet;
            this.schedulerDataStorage1.Appointments.DataMember = "Appointments";
            this.schedulerDataStorage1.Resources.DataSource = eduXpressDatabaseDataSet;
            this.schedulerDataStorage1.Resources.DataMember = "Resources";
            }
            catch (Exception ex)
            {                
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MapAppointmentData()
        {
            this.schedulerDataStorage1.Appointments.Mappings.AllDay = "AllDay";
            this.schedulerDataStorage1.Appointments.Mappings.Description = "Description";
            // Required mapping.
            this.schedulerDataStorage1.Appointments.Mappings.End = "EndDate";
            this.schedulerDataStorage1.Appointments.Mappings.Label = "Label";
            this.schedulerDataStorage1.Appointments.Mappings.Location = "Location";
            this.schedulerDataStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo";
            this.schedulerDataStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo";
            // Required mapping.
            this.schedulerDataStorage1.Appointments.Mappings.Start = "StartDate";
            this.schedulerDataStorage1.Appointments.Mappings.Status = "Status";
            this.schedulerDataStorage1.Appointments.Mappings.Subject = "Subject";
            this.schedulerDataStorage1.Appointments.Mappings.Type = "Type";
            this.schedulerDataStorage1.Appointments.Mappings.ResourceId = "ResourceIDs";
            this.schedulerDataStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MyNote", "CustomField1"));
        }

        private void MapResourceData()
        {
            this.schedulerDataStorage1.Resources.Mappings.Id = "ResourceID";
            this.schedulerDataStorage1.Resources.Mappings.Caption = "ResourceName";
        }
        // Retrieve identity value for an inserted appointment.
        void AppointmentDataAdapter_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                int id = 0;
                using (SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Appointments')", DXSchedulerConn))
                {
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                e.Row["UniqueID"] = id;
            }
        }
        // Store modified data in the database
        private void OnApptChangedInsertedDeleted(object sender, PersistentObjectsEventArgs e)
        {
            try
            {
                AppointmentDataAdapter.Update(eduXpressDatabaseDataSet.Tables["Appointments"]);
            eduXpressDatabaseDataSet.AcceptChanges();
            }
            catch (Exception ex)
            {                
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
       

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            DevExpress.XtraScheduler.SchedulerControl scheduler = ((DevExpress.XtraScheduler.SchedulerControl)(sender));
            EduXpress.Office.OutlookAppointmentForm form = new EduXpress.Office.OutlookAppointmentForm(scheduler, e.Appointment, e.OpenRecurrenceForm);
            try
            {
                e.DialogResult = form.ShowDialog();
                e.Handled = true;
            }
            finally
            {
                form.Dispose();
            }

        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}
    }
}
