namespace EduXpress.Education
{
    partial class userControlMarkSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlMarkSheet));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnAssignSubjectsClasses = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemProcess = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnSaveClass = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageMarkSheets = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSubjects = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentConduct = new System.Windows.Forms.TextBox();
            this.gridControlConduct = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlConduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnAssignSubjectsClasses,
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
            this.ribbonPageMarkSheets});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.Size = new System.Drawing.Size(833, 150);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnAssignSubjectsClasses
            // 
            this.btnAssignSubjectsClasses.Caption = "Assign Subjects to Classes";
            this.btnAssignSubjectsClasses.Id = 1;
            this.btnAssignSubjectsClasses.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAssignSubjectsClasses.ImageOptions.SvgImage")));
            this.btnAssignSubjectsClasses.Name = "btnAssignSubjectsClasses";
            this.btnAssignSubjectsClasses.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAssignSubjectsClasses_ItemClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Caption = "Update";
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 2;
            this.btnUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUpdate.ImageOptions.SvgImage")));
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
            this.btnRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRemove.ImageOptions.SvgImage")));
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
            this.btnSaveClass.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSaveClass.ImageOptions.SvgImage")));
            this.btnSaveClass.Name = "btnSaveClass";
            // 
            // ribbonPageMarkSheets
            // 
            this.ribbonPageMarkSheets.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSubjects});
            this.ribbonPageMarkSheets.MergeOrder = 2;
            this.ribbonPageMarkSheets.Name = "ribbonPageMarkSheets";
            this.ribbonPageMarkSheets.Text = "Mark Sheets";
            // 
            // ribbonPageGroupSubjects
            // 
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnAssignSubjectsClasses);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnSaveClass);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupSubjects.Name = "ribbonPageGroupSubjects";
            this.ribbonPageGroupSubjects.Text = "Mark Sheets";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 624);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(833, 27);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtCurrentConduct);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 150);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(833, 101);
            this.panelControl1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(771, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 509;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Location = new System.Drawing.Point(240, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(177, 13);
            this.labelControl2.TabIndex = 506;
            this.labelControl2.Text = "Fields with an <color=255, 0, 0>*</color> are required fields";
            // 
            // txtCurrentConduct
            // 
            this.txtCurrentConduct.Location = new System.Drawing.Point(25, 6);
            this.txtCurrentConduct.Name = "txtCurrentConduct";
            this.txtCurrentConduct.Size = new System.Drawing.Size(100, 22);
            this.txtCurrentConduct.TabIndex = 473;
            this.txtCurrentConduct.Visible = false;
            // 
            // gridControlConduct
            // 
            this.gridControlConduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlConduct.Location = new System.Drawing.Point(0, 251);
            this.gridControlConduct.MainView = this.gridView1;
            this.gridControlConduct.MenuManager = this.ribbonControl1;
            this.gridControlConduct.Name = "gridControlConduct";
            this.gridControlConduct.Size = new System.Drawing.Size(833, 373);
            this.gridControlConduct.TabIndex = 9;
            this.gridControlConduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlConduct;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // userControlMarkSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlConduct);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlMarkSheet";
            this.Size = new System.Drawing.Size(833, 651);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlConduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnAssignSubjectsClasses;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.BarStaticItem barStaticItemProcess;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnSaveClass;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMarkSheets;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSubjects;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox txtCurrentConduct;
        private DevExpress.XtraGrid.GridControl gridControlConduct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
