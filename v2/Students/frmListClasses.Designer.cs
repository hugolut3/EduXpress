namespace EduXpress.Students
{
    partial class frmListClasses
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListClasses));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnResetSearch = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.comboCycle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.comboSection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControlClasses = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboCycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnResetSearch);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // btnResetSearch
            // 
            this.btnResetSearch.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnResetSearch.Appearance.Font")));
            this.btnResetSearch.Appearance.Options.UseFont = true;
            this.btnResetSearch.Appearance.Options.UseTextOptions = true;
            this.btnResetSearch.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnResetSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnResetSearch.ImageOptions.Image")));
            this.btnResetSearch.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.clearfilter;
            resources.ApplyResources(this.btnResetSearch, "btnResetSearch");
            this.btnResetSearch.Name = "btnResetSearch";
            this.btnResetSearch.Click += new System.EventHandler(this.btnResetSearch_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.comboCycle);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Name = "groupControl2";
            // 
            // comboCycle
            // 
            resources.ApplyResources(this.comboCycle, "comboCycle");
            this.comboCycle.Name = "comboCycle";
            this.comboCycle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboCycle.Properties.Buttons"))))});
            this.comboCycle.SelectedIndexChanged += new System.EventHandler(this.comboCycle_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.comboSection);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // comboSection
            // 
            resources.ApplyResources(this.comboSection, "comboSection");
            this.comboSection.Name = "comboSection";
            this.comboSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboSection.Properties.Buttons"))))});
            this.comboSection.SelectedIndexChanged += new System.EventHandler(this.comboSection_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // gridControlClasses
            // 
            resources.ApplyResources(this.gridControlClasses, "gridControlClasses");
            this.gridControlClasses.MainView = this.gridView1;
            this.gridControlClasses.Name = "gridControlClasses";
            this.gridControlClasses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlClasses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridControlClasses_MouseDoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlClasses;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmListClasses
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlClasses);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmListClasses.IconOptions.Icon")));
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.bo_position;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListClasses";
            this.Load += new System.EventHandler(this.frmListClasses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboCycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControlClasses;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboSection;
        public System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboCycle;
        public System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnResetSearch;
    }
}