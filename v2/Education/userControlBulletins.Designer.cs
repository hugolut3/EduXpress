namespace EduXpress.Education
{
    partial class userControlBulletins
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemProcess = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnSaveClass = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageSchoolReports = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSubjects = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(507, 305);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.SearchEditItem,
            this.ribbonControl1.ExpandCollapseItem,
            this.btnAdd,
            this.btnUpdate,
            this.btnSave,
            this.btnRemove,
            this.barStaticItemProcess,
            this.barStaticItem1,
            this.btnSaveClass});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 10;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageSchoolReports});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.Size = new System.Drawing.Size(1011, 150);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "Add";
            this.btnAdd.Id = 1;
            this.btnAdd.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnAdd.Name = "btnAdd";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Caption = "Update";
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 2;
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            // 
            // btnSave
            // 
            this.btnSave.Id = 8;
            this.btnSave.Name = "btnSave";
            // 
            // btnRemove
            // 
            this.btnRemove.Caption = "Remove";
            this.btnRemove.Enabled = false;
            this.btnRemove.Id = 4;
            this.btnRemove.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_removecircled;
            this.btnRemove.Name = "btnRemove";
            // 
            // barStaticItemProcess
            // 
            this.barStaticItemProcess.Caption = "Ready!";
            this.barStaticItemProcess.Id = 5;
            this.barStaticItemProcess.Name = "barStaticItemProcess";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnSaveClass
            // 
            this.btnSaveClass.Caption = "Save";
            this.btnSaveClass.Enabled = false;
            this.btnSaveClass.Id = 9;
            this.btnSaveClass.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSaveClass.Name = "btnSaveClass";
            // 
            // ribbonPageSchoolReports
            // 
            this.ribbonPageSchoolReports.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSubjects});
            this.ribbonPageSchoolReports.MergeOrder = 2;
            this.ribbonPageSchoolReports.Name = "ribbonPageSchoolReports";
            this.ribbonPageSchoolReports.Text = "School Reports";
            // 
            // ribbonPageGroupSubjects
            // 
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnAdd);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnSaveClass);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupSubjects.Name = "ribbonPageGroupSubjects";
            this.ribbonPageGroupSubjects.Text = "School Reports";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 621);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1011, 27);
            // 
            // userControlBulletins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.simpleButton1);
            this.Name = "userControlBulletins";
            this.Size = new System.Drawing.Size(1011, 648);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.BarStaticItem barStaticItemProcess;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnSaveClass;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageSchoolReports;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSubjects;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
    }
}
