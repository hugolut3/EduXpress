namespace EduXpress.Education
{
    partial class frmConduct
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
            this.ribbonPageGroupConduct = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClose = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label3 = new System.Windows.Forms.Label();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtConductAbbreviation = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentConduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtConductDescription = new DevExpress.XtraEditors.TextEdit();
            this.gridControlConduct = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConductAbbreviation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConductDescription.Properties)).BeginInit();
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
            this.ribbonControl1.Size = new System.Drawing.Size(657, 158);
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
            this.ribbonPageGroupConduct,
            this.ribbonPageGroupClose});
            this.ribbonPageFile.Name = "ribbonPageFile";
            this.ribbonPageFile.Text = "File";
            // 
            // ribbonPageGroupConduct
            // 
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroupConduct.Name = "ribbonPageGroupConduct";
            this.ribbonPageGroupConduct.Text = "Conduct";
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
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 469);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(657, 24);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.txtConductAbbreviation);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtCurrentConduct);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtConductDescription);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 158);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(657, 117);
            this.panelControl1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(628, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 15);
            this.label3.TabIndex = 516;
            this.label3.Text = "*";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(84, 82);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(143, 19);
            this.labelControl6.TabIndex = 515;
            this.labelControl6.Text = "Conduct Abbreviation:";
            // 
            // txtConductAbbreviation
            // 
            this.txtConductAbbreviation.Enabled = false;
            this.txtConductAbbreviation.Location = new System.Drawing.Point(233, 75);
            this.txtConductAbbreviation.MenuManager = this.ribbonControl1;
            this.txtConductAbbreviation.Name = "txtConductAbbreviation";
            this.txtConductAbbreviation.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductAbbreviation.Properties.Appearance.Options.UseFont = true;
            this.txtConductAbbreviation.Size = new System.Drawing.Size(389, 26);
            this.txtConductAbbreviation.TabIndex = 1;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(628, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 454;
            this.label1.Text = "*";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(92, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(135, 19);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Conduct Description:";
            // 
            // txtConductDescription
            // 
            this.txtConductDescription.Enabled = false;
            this.txtConductDescription.Location = new System.Drawing.Point(233, 43);
            this.txtConductDescription.MenuManager = this.ribbonControl1;
            this.txtConductDescription.Name = "txtConductDescription";
            this.txtConductDescription.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductDescription.Properties.Appearance.Options.UseFont = true;
            this.txtConductDescription.Size = new System.Drawing.Size(389, 26);
            this.txtConductDescription.TabIndex = 0;
            // 
            // gridControlConduct
            // 
            this.gridControlConduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlConduct.Location = new System.Drawing.Point(0, 275);
            this.gridControlConduct.MainView = this.gridView1;
            this.gridControlConduct.MenuManager = this.ribbonControl1;
            this.gridControlConduct.Name = "gridControlConduct";
            this.gridControlConduct.Size = new System.Drawing.Size(657, 194);
            this.gridControlConduct.TabIndex = 8;
            this.gridControlConduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlConduct.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlConduct_MouseClick);
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
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmConduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 493);
            this.Controls.Add(this.gridControlConduct);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.employeequickwelcome;
            this.MaximizeBox = false;
            this.Name = "frmConduct";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Conduct";
            this.Load += new System.EventHandler(this.frmConduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConductAbbreviation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConductDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlConduct)).EndInit();
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
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupConduct;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClose;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtConductAbbreviation;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox txtCurrentConduct;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtConductDescription;
        private DevExpress.XtraGrid.GridControl gridControlConduct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}