namespace EduXpress.Office
{
    partial class userControlOfficeSMS
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlOfficeSMS));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnSendSMS = new DevExpress.XtraBars.BarButtonItem();
            this.btnClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnLoadStudents = new DevExpress.XtraBars.BarButtonItem();
            this.btnLoadEmployees = new DevExpress.XtraBars.BarButtonItem();
            this.btnPhoneNumbers = new DevExpress.XtraBars.BarButtonItem();
            this.btnUnsupportedCharacters = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageSMS = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSMS = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupData = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupHelp = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.gridControlListStudents = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.comboMessageID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupSMSTemplates = new DevExpress.XtraEditors.GroupControl();
            this.memoTemplateMessage = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.comboTemplateName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupSectionClass = new DevExpress.XtraEditors.GroupControl();
            this.comboCycle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbClass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchSectionClass = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.memoMessage = new DevExpress.XtraEditors.MemoEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPhoneNo = new DevExpress.XtraEditors.TextEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm2), true, true, typeof(System.Windows.Forms.UserControl));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gridControlListEmployees = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager2 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboMessageID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSMSTemplates)).BeginInit();
            this.groupSMSTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoTemplateMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboTemplateName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSectionClass)).BeginInit();
            this.groupSectionClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboCycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex")));
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex")));
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.SvgImage")));
            this.ribbonControl1.ExpandCollapseItem.SearchTags = resources.GetString("ribbonControl1.ExpandCollapseItem.SearchTags");
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnSendSMS,
            this.btnClear,
            this.btnLoadStudents,
            this.btnLoadEmployees,
            this.btnPhoneNumbers,
            this.btnUnsupportedCharacters});
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageSMS});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnSendSMS
            // 
            resources.ApplyResources(this.btnSendSMS, "btnSendSMS");
            this.btnSendSMS.Id = 2;
            this.btnSendSMS.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnSendSMS.ImageOptions.ImageIndex")));
            this.btnSendSMS.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnSendSMS.ImageOptions.LargeImageIndex")));
            this.btnSendSMS.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_phoneandroid;
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSendSMS_ItemClick);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Id = 3;
            this.btnClear.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnClear.ImageOptions.ImageIndex")));
            this.btnClear.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnClear.ImageOptions.LargeImageIndex")));
            this.btnClear.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.clearall;
            this.btnClear.Name = "btnClear";
            this.btnClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClear_ItemClick);
            // 
            // btnLoadStudents
            // 
            resources.ApplyResources(this.btnLoadStudents, "btnLoadStudents");
            this.btnLoadStudents.Id = 4;
            this.btnLoadStudents.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnLoadStudents.ImageOptions.ImageIndex")));
            this.btnLoadStudents.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnLoadStudents.ImageOptions.LargeImageIndex")));
            this.btnLoadStudents.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.changeview;
            this.btnLoadStudents.Name = "btnLoadStudents";
            this.btnLoadStudents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLoadStudents_ItemClick);
            // 
            // btnLoadEmployees
            // 
            resources.ApplyResources(this.btnLoadEmployees, "btnLoadEmployees");
            this.btnLoadEmployees.Id = 5;
            this.btnLoadEmployees.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnLoadEmployees.ImageOptions.ImageIndex")));
            this.btnLoadEmployees.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnLoadEmployees.ImageOptions.LargeImageIndex")));
            this.btnLoadEmployees.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.changeview;
            this.btnLoadEmployees.Name = "btnLoadEmployees";
            this.btnLoadEmployees.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLoadEmployees_ItemClick);
            // 
            // btnPhoneNumbers
            // 
            resources.ApplyResources(this.btnPhoneNumbers, "btnPhoneNumbers");
            this.btnPhoneNumbers.Enabled = false;
            this.btnPhoneNumbers.Id = 6;
            this.btnPhoneNumbers.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnPhoneNumbers.ImageOptions.ImageIndex")));
            this.btnPhoneNumbers.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnPhoneNumbers.ImageOptions.LargeImageIndex")));
            this.btnPhoneNumbers.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_contact;
            this.btnPhoneNumbers.Name = "btnPhoneNumbers";
            this.btnPhoneNumbers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPhoneNumbers_ItemClick);
            // 
            // btnUnsupportedCharacters
            // 
            resources.ApplyResources(this.btnUnsupportedCharacters, "btnUnsupportedCharacters");
            this.btnUnsupportedCharacters.Id = 7;
            this.btnUnsupportedCharacters.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnUnsupportedCharacters.ImageOptions.ImageIndex")));
            this.btnUnsupportedCharacters.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnUnsupportedCharacters.ImageOptions.LargeImageIndex")));
            this.btnUnsupportedCharacters.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.deletecomment;
            this.btnUnsupportedCharacters.Name = "btnUnsupportedCharacters";
            this.btnUnsupportedCharacters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUnsupportedCharacters_ItemClick);
            // 
            // ribbonPageSMS
            // 
            this.ribbonPageSMS.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSMS,
            this.ribbonPageGroupData,
            this.ribbonPageGroupHelp});
            this.ribbonPageSMS.MergeOrder = 2;
            this.ribbonPageSMS.Name = "ribbonPageSMS";
            resources.ApplyResources(this.ribbonPageSMS, "ribbonPageSMS");
            // 
            // ribbonPageGroupSMS
            // 
            this.ribbonPageGroupSMS.AllowTextClipping = false;
            this.ribbonPageGroupSMS.ItemLinks.Add(this.btnSendSMS);
            this.ribbonPageGroupSMS.ItemLinks.Add(this.btnClear);
            this.ribbonPageGroupSMS.Name = "ribbonPageGroupSMS";
            resources.ApplyResources(this.ribbonPageGroupSMS, "ribbonPageGroupSMS");
            // 
            // ribbonPageGroupData
            // 
            this.ribbonPageGroupData.AllowTextClipping = false;
            this.ribbonPageGroupData.ItemLinks.Add(this.btnLoadStudents);
            this.ribbonPageGroupData.ItemLinks.Add(this.btnLoadEmployees);
            this.ribbonPageGroupData.ItemLinks.Add(this.btnPhoneNumbers);
            this.ribbonPageGroupData.Name = "ribbonPageGroupData";
            resources.ApplyResources(this.ribbonPageGroupData, "ribbonPageGroupData");
            // 
            // ribbonPageGroupHelp
            // 
            this.ribbonPageGroupHelp.ItemLinks.Add(this.btnUnsupportedCharacters);
            this.ribbonPageGroupHelp.Name = "ribbonPageGroupHelp";
            resources.ApplyResources(this.ribbonPageGroupHelp, "ribbonPageGroupHelp");
            // 
            // ribbonStatusBar1
            // 
            resources.ApplyResources(this.ribbonStatusBar1, "ribbonStatusBar1");
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            // 
            // gridControlListStudents
            // 
            resources.ApplyResources(this.gridControlListStudents, "gridControlListStudents");
            this.gridControlListStudents.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlListStudents.EmbeddedNavigator.AccessibleDescription");
            this.gridControlListStudents.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlListStudents.EmbeddedNavigator.AccessibleName");
            this.gridControlListStudents.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlListStudents.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.Anchor")));
            this.gridControlListStudents.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.BackgroundImage")));
            this.gridControlListStudents.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlListStudents.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.ImeMode")));
            this.gridControlListStudents.EmbeddedNavigator.MaximumSize = ((System.Drawing.Size)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.MaximumSize")));
            this.gridControlListStudents.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.TextLocation")));
            this.gridControlListStudents.EmbeddedNavigator.ToolTip = resources.GetString("gridControlListStudents.EmbeddedNavigator.ToolTip");
            this.gridControlListStudents.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlListStudents.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlListStudents.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlListStudents.EmbeddedNavigator.ToolTipTitle");
            this.gridControlListStudents.MainView = this.gridView1;
            this.gridControlListStudents.Name = "gridControlListStudents";
            this.gridControlListStudents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlListStudents.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlListStudents_MouseClick);
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.GridControl = this.gridControlListStudents;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // xtraScrollableControl1
            // 
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Controls.Add(this.comboMessageID);
            this.xtraScrollableControl1.Controls.Add(this.groupSMSTemplates);
            this.xtraScrollableControl1.Controls.Add(this.groupSectionClass);
            this.xtraScrollableControl1.Controls.Add(this.label3);
            this.xtraScrollableControl1.Controls.Add(this.lblCount);
            this.xtraScrollableControl1.Controls.Add(this.textBox1);
            this.xtraScrollableControl1.Controls.Add(this.memoMessage);
            this.xtraScrollableControl1.Controls.Add(this.txtName);
            this.xtraScrollableControl1.Controls.Add(this.label10);
            this.xtraScrollableControl1.Controls.Add(this.label49);
            this.xtraScrollableControl1.Controls.Add(this.labelControl1);
            this.xtraScrollableControl1.Controls.Add(this.labelControl2);
            this.xtraScrollableControl1.Controls.Add(this.txtPhoneNo);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // comboMessageID
            // 
            resources.ApplyResources(this.comboMessageID, "comboMessageID");
            this.comboMessageID.MenuManager = this.ribbonControl1;
            this.comboMessageID.Name = "comboMessageID";
            this.comboMessageID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboMessageID.Properties.Buttons"))))});
            // 
            // groupSMSTemplates
            // 
            resources.ApplyResources(this.groupSMSTemplates, "groupSMSTemplates");
            this.groupSMSTemplates.Controls.Add(this.memoTemplateMessage);
            this.groupSMSTemplates.Controls.Add(this.labelControl3);
            this.groupSMSTemplates.Controls.Add(this.comboTemplateName);
            this.groupSMSTemplates.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupSMSTemplates.Name = "groupSMSTemplates";
            // 
            // memoTemplateMessage
            // 
            resources.ApplyResources(this.memoTemplateMessage, "memoTemplateMessage");
            this.memoTemplateMessage.MenuManager = this.ribbonControl1;
            this.memoTemplateMessage.Name = "memoTemplateMessage";
            this.memoTemplateMessage.Properties.ReadOnly = true;
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // comboTemplateName
            // 
            resources.ApplyResources(this.comboTemplateName, "comboTemplateName");
            this.comboTemplateName.MenuManager = this.ribbonControl1;
            this.comboTemplateName.Name = "comboTemplateName";
            this.comboTemplateName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboTemplateName.Properties.Buttons"))))});
            this.comboTemplateName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboTemplateName.SelectedIndexChanged += new System.EventHandler(this.comboTemplateName_SelectedIndexChanged);
            // 
            // groupSectionClass
            // 
            resources.ApplyResources(this.groupSectionClass, "groupSectionClass");
            this.groupSectionClass.Controls.Add(this.comboCycle);
            this.groupSectionClass.Controls.Add(this.label5);
            this.groupSectionClass.Controls.Add(this.cmbClass);
            this.groupSectionClass.Controls.Add(this.label4);
            this.groupSectionClass.Controls.Add(this.cmbSection);
            this.groupSectionClass.Controls.Add(this.label2);
            this.groupSectionClass.Controls.Add(this.btnSearchSectionClass);
            this.groupSectionClass.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupSectionClass.Name = "groupSectionClass";
            // 
            // comboCycle
            // 
            resources.ApplyResources(this.comboCycle, "comboCycle");
            this.comboCycle.Name = "comboCycle";
            this.comboCycle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboCycle.Properties.Buttons"))))});
            this.comboCycle.SelectedIndexChanged += new System.EventHandler(this.comboCycle_SelectedIndexChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Name = "label5";
            // 
            // cmbClass
            // 
            resources.ApplyResources(this.cmbClass, "cmbClass");
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbClass.Properties.Buttons"))))});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Name = "label4";
            // 
            // cmbSection
            // 
            resources.ApplyResources(this.cmbSection, "cmbSection");
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSection.Properties.Buttons"))))});
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // btnSearchSectionClass
            // 
            resources.ApplyResources(this.btnSearchSectionClass, "btnSearchSectionClass");
            this.btnSearchSectionClass.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchSectionClass.Appearance.Font")));
            this.btnSearchSectionClass.Appearance.Options.UseFont = true;
            this.btnSearchSectionClass.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchSectionClass.ImageOptions.Image")));
            this.btnSearchSectionClass.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.filter;
            this.btnSearchSectionClass.Name = "btnSearchSectionClass";
            this.btnSearchSectionClass.Click += new System.EventHandler(this.btnSearchSectionClass_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Name = "label3";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // memoMessage
            // 
            resources.ApplyResources(this.memoMessage, "memoMessage");
            this.memoMessage.MenuManager = this.ribbonControl1;
            this.memoMessage.Name = "memoMessage";
            this.memoMessage.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.memoMessage.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.memoMessage.Properties.AdvancedModeOptions.Label = resources.GetString("memoMessage.Properties.AdvancedModeOptions.Label");
            this.memoMessage.Properties.AdvancedModeOptions.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.memoMessage.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.memoMessage.EditValueChanged += new System.EventHandler(this.memoMessage_EditValueChanged);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.txtName.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.txtName.Properties.AdvancedModeOptions.Label = resources.GetString("txtName.Properties.AdvancedModeOptions.Label");
            this.txtName.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Name = "label10";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.ForeColor = System.Drawing.Color.Red;
            this.label49.Name = "label49";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // txtPhoneNo
            // 
            resources.ApplyResources(this.txtPhoneNo, "txtPhoneNo");
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.txtPhoneNo.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.txtPhoneNo.Properties.AdvancedModeOptions.Label = resources.GetString("txtPhoneNo.Properties.AdvancedModeOptions.Label");
            this.txtPhoneNo.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtPhoneNo.Properties.Mask.BeepOnError")));
            this.txtPhoneNo.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtPhoneNo.Properties.MaskSettings.Set("mask", "(\\d{7,},)*(\\d{7,})?");
            this.txtPhoneNo.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.txtPhoneNo.TextChanged += new System.EventHandler(this.txtPhoneNo_TextChanged);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // gridControlListEmployees
            // 
            resources.ApplyResources(this.gridControlListEmployees, "gridControlListEmployees");
            this.gridControlListEmployees.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlListEmployees.EmbeddedNavigator.AccessibleDescription");
            this.gridControlListEmployees.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlListEmployees.EmbeddedNavigator.AccessibleName");
            this.gridControlListEmployees.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlListEmployees.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.Anchor")));
            this.gridControlListEmployees.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.BackgroundImage")));
            this.gridControlListEmployees.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlListEmployees.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.ImeMode")));
            this.gridControlListEmployees.EmbeddedNavigator.MaximumSize = ((System.Drawing.Size)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.MaximumSize")));
            this.gridControlListEmployees.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.TextLocation")));
            this.gridControlListEmployees.EmbeddedNavigator.ToolTip = resources.GetString("gridControlListEmployees.EmbeddedNavigator.ToolTip");
            this.gridControlListEmployees.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlListEmployees.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlListEmployees.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlListEmployees.EmbeddedNavigator.ToolTipTitle");
            this.gridControlListEmployees.MainView = this.gridView2;
            this.gridControlListEmployees.Name = "gridControlListEmployees";
            this.gridControlListEmployees.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControlListEmployees.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlListEmployees_MouseClick);
            // 
            // gridView2
            // 
            resources.ApplyResources(this.gridView2, "gridView2");
            this.gridView2.GridControl = this.gridControlListEmployees;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsFind.AlwaysVisible = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager2
            // 
            this.splashScreenManager2.ClosingDelay = 500;
            // 
            // userControlOfficeSMS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlListEmployees);
            this.Controls.Add(this.gridControlListStudents);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlOfficeSMS";
            this.Load += new System.EventHandler(this.userControlOfficeSMS_Load);
            this.VisibleChanged += new System.EventHandler(this.userControlOfficeSMS_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboMessageID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSMSTemplates)).EndInit();
            this.groupSMSTemplates.ResumeLayout(false);
            this.groupSMSTemplates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoTemplateMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboTemplateName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSectionClass)).EndInit();
            this.groupSectionClass.ResumeLayout(false);
            this.groupSectionClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboCycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageSMS;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSMS;
        private DevExpress.XtraGrid.GridControl gridControlListStudents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraBars.BarButtonItem btnSendSMS;
        private DevExpress.XtraBars.BarButtonItem btnClear;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.MemoEdit memoMessage;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label49;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPhoneNo;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupData;
        private DevExpress.XtraBars.BarButtonItem btnLoadStudents;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraBars.BarButtonItem btnLoadEmployees;
        private DevExpress.XtraGrid.GridControl gridControlListEmployees;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager2;
        private DevExpress.XtraEditors.GroupControl groupSectionClass;
        private DevExpress.XtraEditors.ComboBoxEdit comboCycle;
        public System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit cmbClass;
        public System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSection;
        public System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnSearchSectionClass;
        private DevExpress.XtraBars.BarButtonItem btnPhoneNumbers;
        private DevExpress.XtraEditors.GroupControl groupSMSTemplates;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit comboTemplateName;
        private DevExpress.XtraEditors.MemoEdit memoTemplateMessage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupHelp;
        private DevExpress.XtraBars.BarButtonItem btnUnsupportedCharacters;
        private DevExpress.XtraEditors.ComboBoxEdit comboMessageID;
    }
}
