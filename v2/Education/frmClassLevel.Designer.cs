namespace EduXpress.Education
{
    partial class frmClassLevel
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
            this.components = new System.ComponentModel.Container();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageFile = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSections = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClose = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentClassLevel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtClassLevel = new DevExpress.XtraEditors.TextEdit();
            this.gridControlClassLevel = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClassLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnNew,
            this.btnSave,
            this.btnUpdate,
            this.btnDelete,
            this.btnClose});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile});
            this.ribbonControl1.Size = new System.Drawing.Size(621, 158);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            this.btnNew.Caption = "New";
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Enabled = false;
            this.btnSave.Id = 2;
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Caption = "Update";
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 3;
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Delete";
            this.btnDelete.Enabled = false;
            this.btnDelete.Id = 4;
            this.btnDelete.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.delete;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "Close";
            this.btnClose.Id = 5;
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // ribbonPageFile
            // 
            this.ribbonPageFile.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSections,
            this.ribbonPageGroupClose});
            this.ribbonPageFile.Name = "ribbonPageFile";
            this.ribbonPageFile.Text = "File";
            // 
            // ribbonPageGroupSections
            // 
            this.ribbonPageGroupSections.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupSections.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupSections.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupSections.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroupSections.Name = "ribbonPageGroupSections";
            this.ribbonPageGroupSections.Text = "Class Level";
            // 
            // ribbonPageGroupClose
            // 
            this.ribbonPageGroupClose.AllowTextClipping = false;
            this.ribbonPageGroupClose.ItemLinks.Add(this.btnClose, true);
            this.ribbonPageGroupClose.Name = "ribbonPageGroupClose";
            this.ribbonPageGroupClose.Text = "Close";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 433);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(621, 24);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtCurrentClassLevel);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtClassLevel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 158);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(621, 102);
            this.panelControl1.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Location = new System.Drawing.Point(222, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(177, 13);
            this.labelControl2.TabIndex = 506;
            this.labelControl2.Text = "Fields with an <color=255, 0, 0>*</color> are required fields";
            // 
            // txtCurrentClassLevel
            // 
            this.txtCurrentClassLevel.Location = new System.Drawing.Point(25, 6);
            this.txtCurrentClassLevel.Name = "txtCurrentClassLevel";
            this.txtCurrentClassLevel.Size = new System.Drawing.Size(100, 22);
            this.txtCurrentClassLevel.TabIndex = 473;
            this.txtCurrentClassLevel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(555, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 454;
            this.label1.Text = "*";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(81, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(87, 23);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Class Level:";
            // 
            // txtClassLevel
            // 
            this.txtClassLevel.Enabled = false;
            this.txtClassLevel.Location = new System.Drawing.Point(174, 43);
            this.txtClassLevel.MenuManager = this.ribbonControl1;
            this.txtClassLevel.Name = "txtClassLevel";
            this.txtClassLevel.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.txtClassLevel.Properties.Appearance.Options.UseFont = true;
            this.txtClassLevel.Size = new System.Drawing.Size(379, 30);
            this.txtClassLevel.TabIndex = 0;
            // 
            // gridControlClassLevel
            // 
            this.gridControlClassLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlClassLevel.Location = new System.Drawing.Point(0, 260);
            this.gridControlClassLevel.MainView = this.gridView1;
            this.gridControlClassLevel.MenuManager = this.ribbonControl1;
            this.gridControlClassLevel.Name = "gridControlClassLevel";
            this.gridControlClassLevel.Size = new System.Drawing.Size(621, 173);
            this.gridControlClassLevel.TabIndex = 6;
            this.gridControlClassLevel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlClassLevel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlClassLevel_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlClassLevel;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmClassLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 457);
            this.Controls.Add(this.gridControlClassLevel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.bo_position;
            this.Name = "frmClassLevel";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Class Level";
            this.Load += new System.EventHandler(this.frmClassLevel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClassLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSections;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClose;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox txtCurrentClassLevel;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtClassLevel;
        private DevExpress.XtraGrid.GridControl gridControlClassLevel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}