namespace DrectSoft.Core.DoctorAdvice
{
   partial class AdviceEditor
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
            if (m_WaitDialog != null)
               m_WaitDialog.Dispose();
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdviceEditor));
          this.gridCtrl = new DevExpress.XtraGrid.GridControl();
          this.advGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
          this.bandBeginInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
          this.gridColCheckResult = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.repItemCheckResultPicture = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
          this.gridColStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.repItemStatusImage = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
          this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
          this.gridColGroupSerialNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColStartDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.repItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
          this.gridColStartTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.repItemTimeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
          this.gridColContent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColCreator = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.bandAuditInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
          this.gridColAuditDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColAuditTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColAuditor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.bandExecuteInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
          this.gridColExecuteDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColExecuteTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColExecutor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.bandCeaseInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
          this.gridColCeaseDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColCeaseTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColCeasor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColCeaseAuditor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColSerialNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColCeaseAuditDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColCeaseAuditorTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.gridColSynchFlag = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
          this.repItemContentEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
          this.imageListCheckFlag = new System.Windows.Forms.ImageList(this.components);
          this.orderBarManager = new DevExpress.XtraBars.BarManager(this.components);
          this.orderToolBar = new DevExpress.XtraBars.Bar();
          this.barItemSave = new DevExpress.XtraBars.BarButtonItem();
          this.barItemSubmit = new DevExpress.XtraBars.BarButtonItem();
          this.barItemPrint = new DevExpress.XtraBars.BarButtonItem();
          this.barItemRefresh = new DevExpress.XtraBars.BarButtonItem();
          this.barItemLongOrder = new DevExpress.XtraBars.BarCheckItem();
          this.barItemTempOrder = new DevExpress.XtraBars.BarCheckItem();
          this.barItemCut = new DevExpress.XtraBars.BarButtonItem();
          this.barItemCopy = new DevExpress.XtraBars.BarButtonItem();
          this.barItemPaste = new DevExpress.XtraBars.BarButtonItem();
          this.barItemDelete = new DevExpress.XtraBars.BarButtonItem();
          this.barItemUp = new DevExpress.XtraBars.BarButtonItem();
          this.barItemDown = new DevExpress.XtraBars.BarButtonItem();
          this.barItemCancel = new DevExpress.XtraBars.BarButtonItem();
          this.barItemCease = new DevExpress.XtraBars.BarButtonItem();
          this.barItemSetGroup = new DevExpress.XtraBars.BarButtonItem();
          this.barItemAutoGroup = new DevExpress.XtraBars.BarButtonItem();
          this.barItemCancelGroup = new DevExpress.XtraBars.BarButtonItem();
          this.barItemCheckOrder = new DevExpress.XtraBars.BarButtonItem();
          this.barItemDrugManual = new DevExpress.XtraBars.BarButtonItem();
          this.barItemStateAll = new DevExpress.XtraBars.BarCheckItem();
          this.barItemStateNew = new DevExpress.XtraBars.BarCheckItem();
          this.barItemStateAvailably = new DevExpress.XtraBars.BarCheckItem();
          this.barItemSkinTestInfo = new DevExpress.XtraBars.BarEditItem();
          this.repItemPopupSkinTestInfo = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
          this.popContainerSkinTestInfo = new DevExpress.XtraEditors.PopupContainerControl();
          this.gridAllergic = new DevExpress.XtraGrid.GridControl();
          this.gridViewAllergic = new DevExpress.XtraGrid.Views.Grid.GridView();
          this.gridColDrugName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.gridColBeginDate = new DevExpress.XtraGrid.Columns.GridColumn();
          this.gridColEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
          this.gridColResult = new DevExpress.XtraGrid.Columns.GridColumn();
          this.barItemExit = new DevExpress.XtraBars.BarButtonItem();
          this.statusBar = new DevExpress.XtraBars.Bar();
          this.barItemHint = new DevExpress.XtraBars.BarStaticItem();
          this.barItemContext = new DevExpress.XtraBars.BarStaticItem();
          this.barItemLegendNew = new DevExpress.XtraBars.BarStaticItem();
          this.barItemLegendAudit = new DevExpress.XtraBars.BarStaticItem();
          this.barItemLegendCancel = new DevExpress.XtraBars.BarStaticItem();
          this.barItemLegendExecuted = new DevExpress.XtraBars.BarStaticItem();
          this.barItemLegendCeased = new DevExpress.XtraBars.BarStaticItem();
          this.barItemLegendNotSynch = new DevExpress.XtraBars.BarStaticItem();
          this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
          this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
          this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
          this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
          this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
          this.barItemOrderCatalog = new DevExpress.XtraBars.BarEditItem();
          this.selectOrderCatalog = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
          this.barItemFilterStatus = new DevExpress.XtraBars.BarEditItem();
          this.selectOrderStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
          this.barItemMultiSelect = new DevExpress.XtraBars.BarButtonItem();
          this.barItemAudit = new DevExpress.XtraBars.BarButtonItem();
          this.barItemEditRegion = new DevExpress.XtraBars.BarCheckItem();
          this.barItemExtraInfo = new DevExpress.XtraBars.BarCheckItem();
          this.barItemDrugInfo = new DevExpress.XtraBars.BarButtonItem();
          this.barItemExpandHerbDetail = new DevExpress.XtraBars.BarButtonItem();
          this.barItemCollapseHerbDetail = new DevExpress.XtraBars.BarButtonItem();
          this.barItemExpandAllHerb = new DevExpress.XtraBars.BarButtonItem();
          this.barItemCollapseAllHerb = new DevExpress.XtraBars.BarButtonItem();
          this.toolTipCtrl = new DevExpress.Utils.DefaultToolTipController(this.components);
          this.panelContentEditor = new DevExpress.XtraEditors.PanelControl();
          this.popMenuOrderGridView = new DevExpress.XtraBars.PopupMenu(this.components);
          ((System.ComponentModel.ISupportInitialize)(this.gridCtrl)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.advGridView)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemCheckResultPicture)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemStatusImage)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemDateEdit)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemDateEdit.VistaTimeProperties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemTimeEdit)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemContentEdit)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.orderBarManager)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemPopupSkinTestInfo)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.popContainerSkinTestInfo)).BeginInit();
          this.popContainerSkinTestInfo.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.gridAllergic)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.gridViewAllergic)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.selectOrderCatalog)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.selectOrderStatus)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.panelContentEditor)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.popMenuOrderGridView)).BeginInit();
          this.SuspendLayout();
          // 
          // gridCtrl
          // 
          this.gridCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
          this.gridCtrl.Location = new System.Drawing.Point(0, 26);
          this.gridCtrl.LookAndFeel.SkinName = "Blue";
          this.gridCtrl.MainView = this.advGridView;
          this.gridCtrl.Name = "gridCtrl";
          this.gridCtrl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemDateEdit,
            this.repItemTimeEdit,
            this.repItemContentEdit,
            this.repItemStatusImage,
            this.repItemCheckResultPicture});
          this.gridCtrl.Size = new System.Drawing.Size(961, 240);
          this.gridCtrl.TabIndex = 1;
          this.gridCtrl.TabStop = false;
          this.gridCtrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advGridView});
          // 
          // advGridView
          // 
          this.advGridView.Appearance.BandPanel.Options.UseTextOptions = true;
          this.advGridView.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.advGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandBeginInfo,
            this.bandAuditInfo,
            this.bandExecuteInfo,
            this.bandCeaseInfo});
          this.advGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColSerialNo,
            this.gridColStartDate,
            this.gridColStartTime,
            this.gridColContent,
            this.gridColCreator,
            this.gridColAuditDate,
            this.gridColAuditTime,
            this.gridColAuditor,
            this.gridColExecuteDate,
            this.gridColExecuteTime,
            this.gridColExecutor,
            this.gridColCeaseDate,
            this.gridColCeaseTime,
            this.gridColCeasor,
            this.gridColCeaseAuditor,
            this.gridColCeaseAuditDate,
            this.gridColCeaseAuditorTime,
            this.gridColStatus,
            this.gridColGroupSerialNo,
            this.gridColCheckResult,
            this.gridColSynchFlag});
          this.advGridView.GridControl = this.gridCtrl;
          this.advGridView.IndicatorWidth = 40;
          this.advGridView.Name = "advGridView";
          this.advGridView.NewItemRowText = "����˴������ҽ��";
          this.advGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
          this.advGridView.OptionsBehavior.Editable = false;
          this.advGridView.OptionsCustomization.AllowColumnMoving = false;
          this.advGridView.OptionsCustomization.AllowColumnResizing = false;
          this.advGridView.OptionsCustomization.AllowFilter = false;
          this.advGridView.OptionsCustomization.AllowGroup = false;
          this.advGridView.OptionsFilter.AllowColumnMRUFilterList = false;
          this.advGridView.OptionsFilter.AllowMRUFilterList = false;
          this.advGridView.OptionsMenu.EnableColumnMenu = false;
          this.advGridView.OptionsMenu.EnableFooterMenu = false;
          this.advGridView.OptionsMenu.EnableGroupPanelMenu = false;
          this.advGridView.OptionsMenu.ShowAutoFilterRowItem = false;
          this.advGridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
          this.advGridView.OptionsMenu.ShowGroupSortSummaryItems = false;
          this.advGridView.OptionsSelection.MultiSelect = true;
          this.advGridView.OptionsView.ShowBands = false;
          this.advGridView.OptionsView.ShowDetailButtons = false;
          this.advGridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
          this.advGridView.OptionsView.ShowGroupPanel = false;
          this.advGridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.advGridView_CustomDrawRowIndicator);
          // 
          // bandBeginInfo
          // 
          this.bandBeginInfo.Caption = "��ʼ";
          this.bandBeginInfo.Columns.Add(this.gridColCheckResult);
          this.bandBeginInfo.Columns.Add(this.gridColStatus);
          this.bandBeginInfo.Columns.Add(this.gridColGroupSerialNo);
          this.bandBeginInfo.Columns.Add(this.gridColStartDate);
          this.bandBeginInfo.Columns.Add(this.gridColStartTime);
          this.bandBeginInfo.Columns.Add(this.gridColContent);
          this.bandBeginInfo.Columns.Add(this.gridColCreator);
          this.bandBeginInfo.Name = "bandBeginInfo";
          this.bandBeginInfo.Width = 702;
          // 
          // gridColCheckResult
          // 
          this.gridColCheckResult.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCheckResult.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCheckResult.Caption = "������ҩ�����";
          this.gridColCheckResult.ColumnEdit = this.repItemCheckResultPicture;
          this.gridColCheckResult.FieldName = "gridColCheckResult";
          this.gridColCheckResult.MinWidth = 12;
          this.gridColCheckResult.Name = "gridColCheckResult";
          this.gridColCheckResult.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCheckResult.OptionsColumn.ShowCaption = false;
          this.gridColCheckResult.UnboundType = DevExpress.Data.UnboundColumnType.Object;
          this.gridColCheckResult.Visible = true;
          this.gridColCheckResult.Width = 12;
          // 
          // repItemCheckResultPicture
          // 
          this.repItemCheckResultPicture.Name = "repItemCheckResultPicture";
          this.repItemCheckResultPicture.NullText = " ";
          this.repItemCheckResultPicture.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
          // 
          // gridColStatus
          // 
          this.gridColStatus.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColStatus.Caption = "State";
          this.gridColStatus.ColumnEdit = this.repItemStatusImage;
          this.gridColStatus.FieldName = "State";
          this.gridColStatus.ImageAlignment = System.Drawing.StringAlignment.Center;
          this.gridColStatus.Name = "gridColStatus";
          this.gridColStatus.OptionsColumn.AllowEdit = false;
          this.gridColStatus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColStatus.Visible = true;
          this.gridColStatus.Width = 25;
          // 
          // repItemStatusImage
          // 
          this.repItemStatusImage.AutoHeight = false;
          this.repItemStatusImage.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.repItemStatusImage.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("�����", 3201, 11),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("��ִ��", 3202, 13),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("��ȡ��", 3203, 6),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("��ֹͣ", 3204, 7)});
          this.repItemStatusImage.Name = "repItemStatusImage";
          this.repItemStatusImage.SmallImages = this.imageListSmall;
          // 
          // imageListSmall
          // 
          this.imageListSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
          this.imageListSmall.ImageSize = new System.Drawing.Size(16, 16);
          this.imageListSmall.TransparentColor = System.Drawing.Color.Magenta;
          // 
          // gridColGroupSerialNo
          // 
          this.gridColGroupSerialNo.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColGroupSerialNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColGroupSerialNo.FieldName = "GroupSerialNo";
          this.gridColGroupSerialNo.Name = "gridColGroupSerialNo";
          this.gridColGroupSerialNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColGroupSerialNo.Visible = true;
          this.gridColGroupSerialNo.Width = 25;
          // 
          // gridColStartDate
          // 
          this.gridColStartDate.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColStartDate.Caption = "��ʼ����";
          this.gridColStartDate.ColumnEdit = this.repItemDateEdit;
          this.gridColStartDate.Name = "gridColStartDate";
          this.gridColStartDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColStartDate.UnboundType = DevExpress.Data.UnboundColumnType.String;
          this.gridColStartDate.Visible = true;
          this.gridColStartDate.Width = 80;
          // 
          // repItemDateEdit
          // 
          this.repItemDateEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
          this.repItemDateEdit.AutoHeight = false;
          this.repItemDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.repItemDateEdit.Name = "repItemDateEdit";
          this.repItemDateEdit.ShowToday = false;
          this.repItemDateEdit.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
          // 
          // gridColStartTime
          // 
          this.gridColStartTime.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColStartTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColStartTime.Caption = "ʱ��";
          this.gridColStartTime.ColumnEdit = this.repItemTimeEdit;
          this.gridColStartTime.Name = "gridColStartTime";
          this.gridColStartTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColStartTime.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
          this.gridColStartTime.Visible = true;
          this.gridColStartTime.Width = 60;
          // 
          // repItemTimeEdit
          // 
          this.repItemTimeEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
          this.repItemTimeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
          this.repItemTimeEdit.Mask.EditMask = "t";
          this.repItemTimeEdit.Name = "repItemTimeEdit";
          this.repItemTimeEdit.ValidateOnEnterKey = true;
          // 
          // gridColContent
          // 
          this.gridColContent.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColContent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColContent.Caption = "ҽ������";
          this.gridColContent.Name = "gridColContent";
          this.gridColContent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColContent.UnboundType = DevExpress.Data.UnboundColumnType.String;
          this.gridColContent.Visible = true;
          this.gridColContent.Width = 300;
          // 
          // gridColCreator
          // 
          this.gridColCreator.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCreator.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCreator.Caption = "������";
          this.gridColCreator.FieldName = "Creator";
          this.gridColCreator.Name = "gridColCreator";
          this.gridColCreator.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCreator.Visible = true;
          this.gridColCreator.Width = 200;
          // 
          // bandAuditInfo
          // 
          this.bandAuditInfo.Caption = "���";
          this.bandAuditInfo.Columns.Add(this.gridColAuditDate);
          this.bandAuditInfo.Columns.Add(this.gridColAuditTime);
          this.bandAuditInfo.Columns.Add(this.gridColAuditor);
          this.bandAuditInfo.Name = "bandAuditInfo";
          this.bandAuditInfo.Width = 190;
          // 
          // gridColAuditDate
          // 
          this.gridColAuditDate.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColAuditDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColAuditDate.Caption = "�������";
          this.gridColAuditDate.FieldName = "AuditDate";
          this.gridColAuditDate.Name = "gridColAuditDate";
          this.gridColAuditDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColAuditDate.Visible = true;
          this.gridColAuditDate.Width = 70;
          // 
          // gridColAuditTime
          // 
          this.gridColAuditTime.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColAuditTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColAuditTime.Caption = "ʱ��";
          this.gridColAuditTime.FieldName = "AuditTime";
          this.gridColAuditTime.Name = "gridColAuditTime";
          this.gridColAuditTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColAuditTime.Visible = true;
          this.gridColAuditTime.Width = 60;
          // 
          // gridColAuditor
          // 
          this.gridColAuditor.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColAuditor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColAuditor.Caption = "�����";
          this.gridColAuditor.FieldName = "Auditor";
          this.gridColAuditor.Name = "gridColAuditor";
          this.gridColAuditor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColAuditor.Visible = true;
          this.gridColAuditor.Width = 60;
          // 
          // bandExecuteInfo
          // 
          this.bandExecuteInfo.Caption = "ִ��";
          this.bandExecuteInfo.Columns.Add(this.gridColExecuteDate);
          this.bandExecuteInfo.Columns.Add(this.gridColExecuteTime);
          this.bandExecuteInfo.Columns.Add(this.gridColExecutor);
          this.bandExecuteInfo.Name = "bandExecuteInfo";
          this.bandExecuteInfo.Width = 190;
          // 
          // gridColExecuteDate
          // 
          this.gridColExecuteDate.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColExecuteDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColExecuteDate.Caption = "ִ������";
          this.gridColExecuteDate.FieldName = "ExecuteDate";
          this.gridColExecuteDate.Name = "gridColExecuteDate";
          this.gridColExecuteDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColExecuteDate.Visible = true;
          this.gridColExecuteDate.Width = 70;
          // 
          // gridColExecuteTime
          // 
          this.gridColExecuteTime.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColExecuteTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColExecuteTime.Caption = "ʱ��";
          this.gridColExecuteTime.FieldName = "ExecuteTime";
          this.gridColExecuteTime.Name = "gridColExecuteTime";
          this.gridColExecuteTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColExecuteTime.Visible = true;
          this.gridColExecuteTime.Width = 60;
          // 
          // gridColExecutor
          // 
          this.gridColExecutor.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColExecutor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColExecutor.Caption = "ִ����";
          this.gridColExecutor.FieldName = "Executor";
          this.gridColExecutor.Name = "gridColExecutor";
          this.gridColExecutor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColExecutor.Visible = true;
          this.gridColExecutor.Width = 60;
          // 
          // bandCeaseInfo
          // 
          this.bandCeaseInfo.Caption = "ֹͣ";
          this.bandCeaseInfo.Columns.Add(this.gridColCeaseDate);
          this.bandCeaseInfo.Columns.Add(this.gridColCeaseTime);
          this.bandCeaseInfo.Columns.Add(this.gridColCeasor);
          this.bandCeaseInfo.Columns.Add(this.gridColCeaseAuditor);
          this.bandCeaseInfo.Name = "bandCeaseInfo";
          this.bandCeaseInfo.Width = 265;
          // 
          // gridColCeaseDate
          // 
          this.gridColCeaseDate.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCeaseDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCeaseDate.Caption = "ֹͣ����";
          this.gridColCeaseDate.ColumnEdit = this.repItemDateEdit;
          this.gridColCeaseDate.Name = "gridColCeaseDate";
          this.gridColCeaseDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCeaseDate.UnboundType = DevExpress.Data.UnboundColumnType.String;
          this.gridColCeaseDate.Visible = true;
          this.gridColCeaseDate.Width = 70;
          // 
          // gridColCeaseTime
          // 
          this.gridColCeaseTime.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCeaseTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCeaseTime.Caption = "ʱ��";
          this.gridColCeaseTime.ColumnEdit = this.repItemTimeEdit;
          this.gridColCeaseTime.Name = "gridColCeaseTime";
          this.gridColCeaseTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCeaseTime.UnboundType = DevExpress.Data.UnboundColumnType.String;
          this.gridColCeaseTime.Visible = true;
          this.gridColCeaseTime.Width = 60;
          // 
          // gridColCeasor
          // 
          this.gridColCeasor.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCeasor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCeasor.Caption = "ֹͣ��";
          this.gridColCeasor.FieldName = "Ceasor";
          this.gridColCeasor.Name = "gridColCeasor";
          this.gridColCeasor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCeasor.Visible = true;
          this.gridColCeasor.Width = 60;
          // 
          // gridColCeaseAuditor
          // 
          this.gridColCeaseAuditor.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCeaseAuditor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCeaseAuditor.Caption = "ֹͣ�����";
          this.gridColCeaseAuditor.FieldName = "CeaseAuditor";
          this.gridColCeaseAuditor.Name = "gridColCeaseAuditor";
          this.gridColCeaseAuditor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCeaseAuditor.Visible = true;
          // 
          // gridColSerialNo
          // 
          this.gridColSerialNo.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColSerialNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColSerialNo.Caption = "ҽ�����";
          this.gridColSerialNo.FieldName = "SerialNo";
          this.gridColSerialNo.MinWidth = 10;
          this.gridColSerialNo.Name = "gridColSerialNo";
          this.gridColSerialNo.OptionsColumn.AllowEdit = false;
          this.gridColSerialNo.OptionsColumn.AllowMove = false;
          this.gridColSerialNo.OptionsColumn.AllowSize = false;
          this.gridColSerialNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColSerialNo.Width = 20;
          // 
          // gridColCeaseAuditDate
          // 
          this.gridColCeaseAuditDate.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCeaseAuditDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCeaseAuditDate.Caption = "ֹͣ�������";
          this.gridColCeaseAuditDate.FieldName = "CeaseAuditorDate";
          this.gridColCeaseAuditDate.Name = "gridColCeaseAuditDate";
          this.gridColCeaseAuditDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCeaseAuditDate.Visible = true;
          // 
          // gridColCeaseAuditorTime
          // 
          this.gridColCeaseAuditorTime.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColCeaseAuditorTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColCeaseAuditorTime.Caption = "ֹͣ���ʱ��";
          this.gridColCeaseAuditorTime.FieldName = "CeaseAuditorTime";
          this.gridColCeaseAuditorTime.Name = "gridColCeaseAuditorTime";
          this.gridColCeaseAuditorTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColCeaseAuditorTime.Visible = true;
          // 
          // gridColSynchFlag
          // 
          this.gridColSynchFlag.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColSynchFlag.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColSynchFlag.Caption = "ͬ����־";
          this.gridColSynchFlag.FieldName = "HadSynch";
          this.gridColSynchFlag.Name = "gridColSynchFlag";
          this.gridColSynchFlag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          // 
          // repItemContentEdit
          // 
          this.repItemContentEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.repItemContentEdit.CloseOnLostFocus = false;
          this.repItemContentEdit.CloseOnOuterMouseClick = false;
          this.repItemContentEdit.Name = "repItemContentEdit";
          this.repItemContentEdit.PopupSizeable = false;
          this.repItemContentEdit.ShowPopupCloseButton = false;
          // 
          // imageListCheckFlag
          // 
          this.imageListCheckFlag.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCheckFlag.ImageStream")));
          this.imageListCheckFlag.TransparentColor = System.Drawing.Color.DarkGray;
          this.imageListCheckFlag.Images.SetKeyName(0, "0.bmp");
          this.imageListCheckFlag.Images.SetKeyName(1, "1.bmp");
          this.imageListCheckFlag.Images.SetKeyName(2, "2.bmp");
          this.imageListCheckFlag.Images.SetKeyName(3, "3.bmp");
          this.imageListCheckFlag.Images.SetKeyName(4, "4.bmp");
          // 
          // orderBarManager
          // 
          this.orderBarManager.AllowCustomization = false;
          this.orderBarManager.AllowMoveBarOnToolbar = false;
          this.orderBarManager.AllowQuickCustomization = false;
          this.orderBarManager.AllowShowToolbarsPopup = false;
          this.orderBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.orderToolBar,
            this.statusBar});
          this.orderBarManager.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("�༭", new System.Guid("4530fb88-b6a6-4580-a701-1ca5f3a9c1e3")),
            new DevExpress.XtraBars.BarManagerCategory("ҽ������", new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20")),
            new DevExpress.XtraBars.BarManagerCategory("ҽ������", new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f")),
            new DevExpress.XtraBars.BarManagerCategory("�ļ�", new System.Guid("1fee6fad-9037-48cf-89ce-7fbf445c3c64")),
            new DevExpress.XtraBars.BarManagerCategory("�������", new System.Guid("d4d50213-0b2a-4188-a1ca-c377aa3414b6")),
            new DevExpress.XtraBars.BarManagerCategory("����", new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f"))});
          this.orderBarManager.Controller = this.barAndDockingController1;
          this.orderBarManager.DockControls.Add(this.barDockControlTop);
          this.orderBarManager.DockControls.Add(this.barDockControlBottom);
          this.orderBarManager.DockControls.Add(this.barDockControlLeft);
          this.orderBarManager.DockControls.Add(this.barDockControlRight);
          this.orderBarManager.Form = this;
          this.orderBarManager.Images = this.imageListSmall;
          this.orderBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barItemCut,
            this.barItemCopy,
            this.barItemPaste,
            this.barItemDelete,
            this.barItemUp,
            this.barItemDown,
            this.barItemCancel,
            this.barItemCease,
            this.barItemSetGroup,
            this.barItemOrderCatalog,
            this.barItemFilterStatus,
            this.barItemSave,
            this.barItemExit,
            this.barItemAutoGroup,
            this.barItemCancelGroup,
            this.barItemMultiSelect,
            this.barItemAudit,
            this.barItemCheckOrder,
            this.barItemDrugManual,
            this.barItemEditRegion,
            this.barItemExtraInfo,
            this.barItemSubmit,
            this.barItemTempOrder,
            this.barItemLongOrder,
            this.barItemStateAll,
            this.barItemStateNew,
            this.barItemStateAvailably,
            this.barItemSkinTestInfo,
            this.barItemPrint,
            this.barItemHint,
            this.barItemLegendNew,
            this.barItemLegendAudit,
            this.barItemLegendExecuted,
            this.barItemLegendCeased,
            this.barItemLegendCancel,
            this.barItemLegendNotSynch,
            this.barItemRefresh,
            this.barItemDrugInfo,
            this.barItemExpandHerbDetail,
            this.barItemCollapseHerbDetail,
            this.barItemExpandAllHerb,
            this.barItemCollapseAllHerb,
            this.barItemContext});
          this.orderBarManager.MaxItemId = 55;
          this.orderBarManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.selectOrderCatalog,
            this.selectOrderStatus,
            this.repItemPopupSkinTestInfo});
          this.orderBarManager.StatusBar = this.statusBar;
          this.orderBarManager.ToolTipController = this.toolTipCtrl.DefaultController;
          // 
          // orderToolBar
          // 
          this.orderToolBar.BarName = "OrderToolBar";
          this.orderToolBar.DockCol = 0;
          this.orderToolBar.DockRow = 0;
          this.orderToolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
          this.orderToolBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSubmit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemLongOrder, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemTempOrder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemUp),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDown),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCancel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCease),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSetGroup, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemAutoGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCancelGroup),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.barItemCheckOrder, "��ҩ���", true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDrugManual),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.barItemStateAll, "ȫ��", true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.barItemStateNew, "�¿�"),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.barItemStateAvailably, "��Ч"),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSkinTestInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemExit, true)});
          this.orderToolBar.Text = "ҽ��������";
          // 
          // barItemSave
          // 
          this.barItemSave.Caption = "����";
          this.barItemSave.CategoryGuid = new System.Guid("1fee6fad-9037-48cf-89ce-7fbf445c3c64");
          this.barItemSave.Hint = "���������޸�";
          this.barItemSave.Id = 16;
          this.barItemSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
          this.barItemSave.Name = "barItemSave";
          this.barItemSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
          // 
          // barItemSubmit
          // 
          this.barItemSubmit.Caption = "����";
          this.barItemSubmit.CategoryGuid = new System.Guid("1fee6fad-9037-48cf-89ce-7fbf445c3c64");
          this.barItemSubmit.Hint = "��ʿվ���ͶԸò���ҽ�����޸�";
          this.barItemSubmit.Id = 28;
          this.barItemSubmit.Name = "barItemSubmit";
          this.barItemSubmit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
          // 
          // barItemPrint
          // 
          this.barItemPrint.Caption = "��ӡ";
          this.barItemPrint.CategoryGuid = new System.Guid("1fee6fad-9037-48cf-89ce-7fbf445c3c64");
          this.barItemPrint.Id = 36;
          this.barItemPrint.ImageIndex = 38;
          this.barItemPrint.ImageIndexDisabled = 39;
          this.barItemPrint.Name = "barItemPrint";
          // 
          // barItemRefresh
          // 
          this.barItemRefresh.Caption = "ˢ��";
          this.barItemRefresh.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemRefresh.Hint = "���¶�ȡ��ǰ���˵�ҽ������";
          this.barItemRefresh.Id = 47;
          this.barItemRefresh.Name = "barItemRefresh";
          // 
          // barItemLongOrder
          // 
          this.barItemLongOrder.Caption = "����ҽ��";
          this.barItemLongOrder.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemLongOrder.GroupIndex = 1;
          this.barItemLongOrder.Hint = "��ʾ����ҽ��";
          this.barItemLongOrder.Id = 31;
          this.barItemLongOrder.Name = "barItemLongOrder";
          // 
          // barItemTempOrder
          // 
          this.barItemTempOrder.Caption = "��ʱҽ��";
          this.barItemTempOrder.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemTempOrder.Checked = true;
          this.barItemTempOrder.GroupIndex = 1;
          this.barItemTempOrder.Hint = "��ʾ��ʱҽ��";
          this.barItemTempOrder.Id = 30;
          this.barItemTempOrder.Name = "barItemTempOrder";
          // 
          // barItemCut
          // 
          this.barItemCut.Caption = "����";
          this.barItemCut.CategoryGuid = new System.Guid("4530fb88-b6a6-4580-a701-1ca5f3a9c1e3");
          this.barItemCut.Hint = "����ѡ��ҽ��";
          this.barItemCut.Id = 0;
          this.barItemCut.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
          this.barItemCut.Name = "barItemCut";
          // 
          // barItemCopy
          // 
          this.barItemCopy.Caption = "����";
          this.barItemCopy.CategoryGuid = new System.Guid("4530fb88-b6a6-4580-a701-1ca5f3a9c1e3");
          this.barItemCopy.Hint = "����ѡ��ҽ��";
          this.barItemCopy.Id = 1;
          this.barItemCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
          this.barItemCopy.Name = "barItemCopy";
          // 
          // barItemPaste
          // 
          this.barItemPaste.Caption = "ճ��";
          this.barItemPaste.CategoryGuid = new System.Guid("4530fb88-b6a6-4580-a701-1ca5f3a9c1e3");
          this.barItemPaste.Hint = "ճ��ѡ��ҽ��";
          this.barItemPaste.Id = 2;
          this.barItemPaste.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
          this.barItemPaste.Name = "barItemPaste";
          // 
          // barItemDelete
          // 
          this.barItemDelete.Caption = "ɾ��";
          this.barItemDelete.CategoryGuid = new System.Guid("4530fb88-b6a6-4580-a701-1ca5f3a9c1e3");
          this.barItemDelete.Hint = "ɾ��ѡ��ҽ��";
          this.barItemDelete.Id = 3;
          this.barItemDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Delete);
          this.barItemDelete.Name = "barItemDelete";
          // 
          // barItemUp
          // 
          this.barItemUp.Caption = "����";
          this.barItemUp.CategoryGuid = new System.Guid("4530fb88-b6a6-4580-a701-1ca5f3a9c1e3");
          this.barItemUp.Hint = "ѡ��ҽ������һ��";
          this.barItemUp.Id = 4;
          this.barItemUp.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up));
          this.barItemUp.Name = "barItemUp";
          // 
          // barItemDown
          // 
          this.barItemDown.Caption = "����";
          this.barItemDown.CategoryGuid = new System.Guid("4530fb88-b6a6-4580-a701-1ca5f3a9c1e3");
          this.barItemDown.Hint = "ѡ��ҽ������һ��";
          this.barItemDown.Id = 5;
          this.barItemDown.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down));
          this.barItemDown.Name = "barItemDown";
          // 
          // barItemCancel
          // 
          this.barItemCancel.Caption = "ȡ��ҽ��";
          this.barItemCancel.CategoryGuid = new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20");
          this.barItemCancel.Hint = "ȡ��ѡ��ҽ��";
          this.barItemCancel.Id = 15;
          this.barItemCancel.Name = "barItemCancel";
          // 
          // barItemCease
          // 
          this.barItemCease.Caption = "ֹͣҽ��";
          this.barItemCease.CategoryGuid = new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20");
          this.barItemCease.Hint = "����ѡ��ҽ����ֹͣʱ��";
          this.barItemCease.Id = 6;
          this.barItemCease.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
          this.barItemCease.Name = "barItemCease";
          // 
          // barItemSetGroup
          // 
          this.barItemSetGroup.Caption = "����";
          this.barItemSetGroup.CategoryGuid = new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20");
          this.barItemSetGroup.Hint = "��ѡ��ҽ����Ϊһ��";
          this.barItemSetGroup.Id = 7;
          this.barItemSetGroup.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
          this.barItemSetGroup.Name = "barItemSetGroup";
          // 
          // barItemAutoGroup
          // 
          this.barItemAutoGroup.Caption = "�Զ�����";
          this.barItemAutoGroup.CategoryGuid = new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20");
          this.barItemAutoGroup.Hint = "���������������Զ�����";
          this.barItemAutoGroup.Id = 53;
          this.barItemAutoGroup.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
          this.barItemAutoGroup.Name = "barItemAutoGroup";
          // 
          // barItemCancelGroup
          // 
          this.barItemCancelGroup.Caption = "ȡ������";
          this.barItemCancelGroup.CategoryGuid = new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20");
          this.barItemCancelGroup.Hint = "ȡ����ҽ���ķ���";
          this.barItemCancelGroup.Id = 18;
          this.barItemCancelGroup.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11);
          this.barItemCancelGroup.Name = "barItemCancelGroup";
          // 
          // barItemCheckOrder
          // 
          this.barItemCheckOrder.Caption = "���ҽ��";
          this.barItemCheckOrder.CategoryGuid = new System.Guid("d4d50213-0b2a-4188-a1ca-c377aa3414b6");
          this.barItemCheckOrder.Hint = "��ҩ���";
          this.barItemCheckOrder.Id = 23;
          this.barItemCheckOrder.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
          this.barItemCheckOrder.Name = "barItemCheckOrder";
          this.barItemCheckOrder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
          // 
          // barItemDrugManual
          // 
          this.barItemDrugManual.Caption = "ҩƷ�ֲ�";
          this.barItemDrugManual.CategoryGuid = new System.Guid("d4d50213-0b2a-4188-a1ca-c377aa3414b6");
          this.barItemDrugManual.Hint = "ҩƷ�ֲ�";
          this.barItemDrugManual.Id = 22;
          this.barItemDrugManual.Name = "barItemDrugManual";
          this.barItemDrugManual.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
          // 
          // barItemStateAll
          // 
          this.barItemStateAll.Caption = "ȫ��ҽ��";
          this.barItemStateAll.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemStateAll.Checked = true;
          this.barItemStateAll.GroupIndex = 2;
          this.barItemStateAll.Hint = "��ʾȫ��ҽ��";
          this.barItemStateAll.Id = 32;
          this.barItemStateAll.Name = "barItemStateAll";
          // 
          // barItemStateNew
          // 
          this.barItemStateNew.Caption = "����ҽ��";
          this.barItemStateNew.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemStateNew.GroupIndex = 2;
          this.barItemStateNew.Hint = "ֻ��ʾ������ҽ��";
          this.barItemStateNew.Id = 33;
          this.barItemStateNew.Name = "barItemStateNew";
          // 
          // barItemStateAvailably
          // 
          this.barItemStateAvailably.Caption = "��Чҽ��";
          this.barItemStateAvailably.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemStateAvailably.GroupIndex = 2;
          this.barItemStateAvailably.Hint = "ֻ��ʾִ���е�ҽ��";
          this.barItemStateAvailably.Id = 34;
          this.barItemStateAvailably.Name = "barItemStateAvailably";
          // 
          // barItemSkinTestInfo
          // 
          this.barItemSkinTestInfo.Caption = "Ƥ����Ϣ";
          this.barItemSkinTestInfo.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemSkinTestInfo.Edit = this.repItemPopupSkinTestInfo;
          this.barItemSkinTestInfo.EditValue = "������Ϣ����";
          this.barItemSkinTestInfo.Hint = "��ʾ������ϸ�Ĺ�����Ϣ���";
          this.barItemSkinTestInfo.Id = 35;
          this.barItemSkinTestInfo.Name = "barItemSkinTestInfo";
          this.barItemSkinTestInfo.Width = 100;
          // 
          // repItemPopupSkinTestInfo
          // 
          this.repItemPopupSkinTestInfo.AutoHeight = false;
          this.repItemPopupSkinTestInfo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.repItemPopupSkinTestInfo.Name = "repItemPopupSkinTestInfo";
          this.repItemPopupSkinTestInfo.PopupControl = this.popContainerSkinTestInfo;
          this.repItemPopupSkinTestInfo.ShowPopupCloseButton = false;
          // 
          // popContainerSkinTestInfo
          // 
          this.toolTipCtrl.SetAllowHtmlText(this.popContainerSkinTestInfo, DevExpress.Utils.DefaultBoolean.Default);
          this.popContainerSkinTestInfo.Controls.Add(this.gridAllergic);
          this.popContainerSkinTestInfo.Location = new System.Drawing.Point(11, 100);
          this.popContainerSkinTestInfo.Name = "popContainerSkinTestInfo";
          this.popContainerSkinTestInfo.Size = new System.Drawing.Size(480, 89);
          this.popContainerSkinTestInfo.TabIndex = 6;
          // 
          // gridAllergic
          // 
          this.gridAllergic.Dock = System.Windows.Forms.DockStyle.Fill;
          this.gridAllergic.Location = new System.Drawing.Point(0, 0);
          this.gridAllergic.MainView = this.gridViewAllergic;
          this.gridAllergic.Name = "gridAllergic";
          this.gridAllergic.Size = new System.Drawing.Size(480, 89);
          this.gridAllergic.TabIndex = 0;
          this.gridAllergic.TabStop = false;
          this.gridAllergic.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAllergic});
          // 
          // gridViewAllergic
          // 
          this.gridViewAllergic.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColDrugName,
            this.gridColBeginDate,
            this.gridColEndDate,
            this.gridColResult});
          this.gridViewAllergic.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
          this.gridViewAllergic.GridControl = this.gridAllergic;
          this.gridViewAllergic.Name = "gridViewAllergic";
          this.gridViewAllergic.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
          this.gridViewAllergic.OptionsBehavior.Editable = false;
          this.gridViewAllergic.OptionsCustomization.AllowColumnMoving = false;
          this.gridViewAllergic.OptionsCustomization.AllowFilter = false;
          this.gridViewAllergic.OptionsCustomization.AllowGroup = false;
          this.gridViewAllergic.OptionsFilter.AllowColumnMRUFilterList = false;
          this.gridViewAllergic.OptionsFilter.AllowMRUFilterList = false;
          this.gridViewAllergic.OptionsMenu.EnableColumnMenu = false;
          this.gridViewAllergic.OptionsMenu.EnableFooterMenu = false;
          this.gridViewAllergic.OptionsMenu.EnableGroupPanelMenu = false;
          this.gridViewAllergic.OptionsMenu.ShowAutoFilterRowItem = false;
          this.gridViewAllergic.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
          this.gridViewAllergic.OptionsMenu.ShowGroupSortSummaryItems = false;
          this.gridViewAllergic.OptionsSelection.EnableAppearanceFocusedCell = false;
          this.gridViewAllergic.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
          this.gridViewAllergic.OptionsView.ShowGroupPanel = false;
          this.gridViewAllergic.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.CustomAllergicGridCell);
          // 
          // gridColDrugName
          // 
          this.gridColDrugName.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColDrugName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColDrugName.Caption = "ҩƷ����";
          this.gridColDrugName.FieldName = "ypmc";
          this.gridColDrugName.Name = "gridColDrugName";
          this.gridColDrugName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColDrugName.Visible = true;
          this.gridColDrugName.VisibleIndex = 0;
          this.gridColDrugName.Width = 200;
          // 
          // gridColBeginDate
          // 
          this.gridColBeginDate.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColBeginDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColBeginDate.Caption = "��ʼ����";
          this.gridColBeginDate.FieldName = "ksrq";
          this.gridColBeginDate.Name = "gridColBeginDate";
          this.gridColBeginDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColBeginDate.Visible = true;
          this.gridColBeginDate.VisibleIndex = 1;
          this.gridColBeginDate.Width = 100;
          // 
          // gridColEndDate
          // 
          this.gridColEndDate.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColEndDate.Caption = "��������";
          this.gridColEndDate.FieldName = "jsrq";
          this.gridColEndDate.Name = "gridColEndDate";
          this.gridColEndDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColEndDate.Visible = true;
          this.gridColEndDate.VisibleIndex = 2;
          this.gridColEndDate.Width = 100;
          // 
          // gridColResult
          // 
          this.gridColResult.AppearanceHeader.Options.UseTextOptions = true;
          this.gridColResult.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
          this.gridColResult.Caption = "Ƥ�Խ��";
          this.gridColResult.FieldName = "yxbz";
          this.gridColResult.Name = "gridColResult";
          this.gridColResult.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
          this.gridColResult.Visible = true;
          this.gridColResult.VisibleIndex = 3;
          this.gridColResult.Width = 100;
          // 
          // barItemExit
          // 
          this.barItemExit.Caption = "�˳�";
          this.barItemExit.CategoryGuid = new System.Guid("1fee6fad-9037-48cf-89ce-7fbf445c3c64");
          this.barItemExit.Id = 17;
          this.barItemExit.ImageIndex = 15;
          this.barItemExit.ImageIndexDisabled = 24;
          this.barItemExit.Name = "barItemExit";
          // 
          // statusBar
          // 
          this.statusBar.BarName = "״̬��";
          this.statusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
          this.statusBar.DockCol = 0;
          this.statusBar.DockRow = 0;
          this.statusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
          this.statusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemHint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemContext),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemLegendNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemLegendAudit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemLegendCancel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemLegendExecuted),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemLegendCeased),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemLegendNotSynch)});
          this.statusBar.OptionsBar.AllowQuickCustomization = false;
          this.statusBar.OptionsBar.DrawDragBorder = false;
          this.statusBar.OptionsBar.UseWholeRow = true;
          this.statusBar.Text = "״̬��";
          // 
          // barItemHint
          // 
          this.barItemHint.Appearance.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.barItemHint.Appearance.Options.UseFont = true;
          this.barItemHint.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
          this.barItemHint.Caption = "������ʾ";
          this.barItemHint.Description = "��ʾ��ǰ����ִ�еĲ���";
          this.barItemHint.Id = 39;
          this.barItemHint.Name = "barItemHint";
          this.barItemHint.TextAlignment = System.Drawing.StringAlignment.Near;
          this.barItemHint.Width = 32;
          // 
          // barItemContext
          // 
          this.barItemContext.Caption = "ҽ��������ʾ:";
          this.barItemContext.Id = 54;
          this.barItemContext.Name = "barItemContext";
          this.barItemContext.TextAlignment = System.Drawing.StringAlignment.Near;
          // 
          // barItemLegendNew
          // 
          this.barItemLegendNew.Appearance.Font = new System.Drawing.Font("����", 9F);
          this.barItemLegendNew.Appearance.Options.UseFont = true;
          this.barItemLegendNew.Caption = "��ҽ��";
          this.barItemLegendNew.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemLegendNew.Hint = "��ʾ������ҽ������û�б����";
          this.barItemLegendNew.Id = 40;
          this.barItemLegendNew.Name = "barItemLegendNew";
          this.barItemLegendNew.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
          this.barItemLegendNew.TextAlignment = System.Drawing.StringAlignment.Near;
          // 
          // barItemLegendAudit
          // 
          this.barItemLegendAudit.Appearance.Font = new System.Drawing.Font("����", 9F);
          this.barItemLegendAudit.Appearance.Options.UseFont = true;
          this.barItemLegendAudit.Caption = "���ͨ��";
          this.barItemLegendAudit.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemLegendAudit.Hint = "��ʾҽ�������ͨ��";
          this.barItemLegendAudit.Id = 41;
          this.barItemLegendAudit.Name = "barItemLegendAudit";
          this.barItemLegendAudit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
          this.barItemLegendAudit.TextAlignment = System.Drawing.StringAlignment.Near;
          // 
          // barItemLegendCancel
          // 
          this.barItemLegendCancel.Appearance.Font = new System.Drawing.Font("����", 9F);
          this.barItemLegendCancel.Appearance.Options.UseFont = true;
          this.barItemLegendCancel.Caption = "DCҽ��";
          this.barItemLegendCancel.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemLegendCancel.Hint = "��ʾҽ���ѱ�ȡ��";
          this.barItemLegendCancel.Id = 44;
          this.barItemLegendCancel.Name = "barItemLegendCancel";
          this.barItemLegendCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
          this.barItemLegendCancel.TextAlignment = System.Drawing.StringAlignment.Near;
          // 
          // barItemLegendExecuted
          // 
          this.barItemLegendExecuted.Appearance.Font = new System.Drawing.Font("����", 9F);
          this.barItemLegendExecuted.Appearance.Options.UseFont = true;
          this.barItemLegendExecuted.Caption = "��ִ��";
          this.barItemLegendExecuted.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemLegendExecuted.Hint = "��ʾҽ���Ѿ�ִ��";
          this.barItemLegendExecuted.Id = 42;
          this.barItemLegendExecuted.Name = "barItemLegendExecuted";
          this.barItemLegendExecuted.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
          this.barItemLegendExecuted.TextAlignment = System.Drawing.StringAlignment.Near;
          // 
          // barItemLegendCeased
          // 
          this.barItemLegendCeased.Appearance.Font = new System.Drawing.Font("����", 9F);
          this.barItemLegendCeased.Appearance.Options.UseFont = true;
          this.barItemLegendCeased.Caption = "��ֹͣ";
          this.barItemLegendCeased.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemLegendCeased.Hint = "��ʾҽ������ֹͣ״̬";
          this.barItemLegendCeased.Id = 43;
          this.barItemLegendCeased.Name = "barItemLegendCeased";
          this.barItemLegendCeased.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
          this.barItemLegendCeased.TextAlignment = System.Drawing.StringAlignment.Near;
          // 
          // barItemLegendNotSynch
          // 
          this.barItemLegendNotSynch.Appearance.Font = new System.Drawing.Font("����", 9F);
          this.barItemLegendNotSynch.Appearance.Options.UseFont = true;
          this.barItemLegendNotSynch.Caption = "δ����";
          this.barItemLegendNotSynch.CategoryGuid = new System.Guid("eba1302f-6105-4a38-82a3-47afd325220f");
          this.barItemLegendNotSynch.Hint = "��ʾ��ҽ�����޸ģ����ݻ�û�з��͵���ʿվ";
          this.barItemLegendNotSynch.Id = 45;
          this.barItemLegendNotSynch.Name = "barItemLegendNotSynch";
          this.barItemLegendNotSynch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
          this.barItemLegendNotSynch.TextAlignment = System.Drawing.StringAlignment.Near;
          // 
          // barAndDockingController1
          // 
          this.barAndDockingController1.LookAndFeel.SkinName = "Blue";
          this.barAndDockingController1.PaintStyleName = "Skin";
          this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
          // 
          // barDockControlTop
          // 
          this.toolTipCtrl.SetAllowHtmlText(this.barDockControlTop, DevExpress.Utils.DefaultBoolean.Default);
          this.barDockControlTop.CausesValidation = false;
          this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
          this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
          this.barDockControlTop.Size = new System.Drawing.Size(961, 26);
          // 
          // barDockControlBottom
          // 
          this.toolTipCtrl.SetAllowHtmlText(this.barDockControlBottom, DevExpress.Utils.DefaultBoolean.Default);
          this.barDockControlBottom.CausesValidation = false;
          this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
          this.barDockControlBottom.Location = new System.Drawing.Point(0, 377);
          this.barDockControlBottom.Size = new System.Drawing.Size(961, 23);
          // 
          // barDockControlLeft
          // 
          this.toolTipCtrl.SetAllowHtmlText(this.barDockControlLeft, DevExpress.Utils.DefaultBoolean.Default);
          this.barDockControlLeft.CausesValidation = false;
          this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
          this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
          this.barDockControlLeft.Size = new System.Drawing.Size(0, 351);
          // 
          // barDockControlRight
          // 
          this.toolTipCtrl.SetAllowHtmlText(this.barDockControlRight, DevExpress.Utils.DefaultBoolean.Default);
          this.barDockControlRight.CausesValidation = false;
          this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
          this.barDockControlRight.Location = new System.Drawing.Point(961, 26);
          this.barDockControlRight.Size = new System.Drawing.Size(0, 351);
          // 
          // barItemOrderCatalog
          // 
          this.barItemOrderCatalog.Caption = "ҽ�����";
          this.barItemOrderCatalog.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemOrderCatalog.Edit = this.selectOrderCatalog;
          this.barItemOrderCatalog.Id = 13;
          this.barItemOrderCatalog.Name = "barItemOrderCatalog";
          this.barItemOrderCatalog.Width = 100;
          // 
          // selectOrderCatalog
          // 
          this.selectOrderCatalog.AutoComplete = false;
          this.selectOrderCatalog.AutoHeight = false;
          this.selectOrderCatalog.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.selectOrderCatalog.ImmediatePopup = true;
          this.selectOrderCatalog.Name = "selectOrderCatalog";
          this.selectOrderCatalog.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
          // 
          // barItemFilterStatus
          // 
          this.barItemFilterStatus.Caption = "ѡ��ҽ��״̬";
          this.barItemFilterStatus.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemFilterStatus.Edit = this.selectOrderStatus;
          this.barItemFilterStatus.Id = 10;
          this.barItemFilterStatus.Name = "barItemFilterStatus";
          this.barItemFilterStatus.Width = 100;
          // 
          // selectOrderStatus
          // 
          this.selectOrderStatus.AutoComplete = false;
          this.selectOrderStatus.AutoHeight = false;
          this.selectOrderStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.selectOrderStatus.ImmediatePopup = true;
          this.selectOrderStatus.Name = "selectOrderStatus";
          this.selectOrderStatus.SmallImages = this.imageListSmall;
          // 
          // barItemMultiSelect
          // 
          this.barItemMultiSelect.Caption = "��ʼ��ѡ";
          this.barItemMultiSelect.CategoryGuid = new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20");
          this.barItemMultiSelect.Id = 25;
          this.barItemMultiSelect.ImageIndex = 34;
          this.barItemMultiSelect.ImageIndexDisabled = 35;
          this.barItemMultiSelect.Name = "barItemMultiSelect";
          // 
          // barItemAudit
          // 
          this.barItemAudit.Caption = "���";
          this.barItemAudit.CategoryGuid = new System.Guid("15bb3249-c862-46c3-9ec5-de86bd8f2b20");
          this.barItemAudit.Hint = "���ѡ��ҽ��";
          this.barItemAudit.Id = 19;
          this.barItemAudit.ImageIndex = 11;
          this.barItemAudit.Name = "barItemAudit";
          // 
          // barItemEditRegion
          // 
          this.barItemEditRegion.Caption = "�༭����";
          this.barItemEditRegion.CategoryGuid = new System.Guid("1fee6fad-9037-48cf-89ce-7fbf445c3c64");
          this.barItemEditRegion.Hint = "��ʾ������ҽ���༭����";
          this.barItemEditRegion.Id = 26;
          this.barItemEditRegion.Name = "barItemEditRegion";
          // 
          // barItemExtraInfo
          // 
          this.barItemExtraInfo.Caption = "������Ϣ";
          this.barItemExtraInfo.CategoryGuid = new System.Guid("1fee6fad-9037-48cf-89ce-7fbf445c3c64");
          this.barItemExtraInfo.Checked = true;
          this.barItemExtraInfo.Id = 27;
          this.barItemExtraInfo.Name = "barItemExtraInfo";
          // 
          // barItemDrugInfo
          // 
          this.barItemDrugInfo.Caption = "ҩƷ��Ϣ";
          this.barItemDrugInfo.CategoryGuid = new System.Guid("d4d50213-0b2a-4188-a1ca-c377aa3414b6");
          this.barItemDrugInfo.Id = 48;
          this.barItemDrugInfo.Name = "barItemDrugInfo";
          // 
          // barItemExpandHerbDetail
          // 
          this.barItemExpandHerbDetail.Caption = "չ����ҩ��ϸ";
          this.barItemExpandHerbDetail.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemExpandHerbDetail.Id = 49;
          this.barItemExpandHerbDetail.Name = "barItemExpandHerbDetail";
          // 
          // barItemCollapseHerbDetail
          // 
          this.barItemCollapseHerbDetail.Caption = "�۵���ҩ��ϸ";
          this.barItemCollapseHerbDetail.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemCollapseHerbDetail.Id = 50;
          this.barItemCollapseHerbDetail.Name = "barItemCollapseHerbDetail";
          // 
          // barItemExpandAllHerb
          // 
          this.barItemExpandAllHerb.Caption = "չ�����в�ҩ��ϸ";
          this.barItemExpandAllHerb.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemExpandAllHerb.Id = 51;
          this.barItemExpandAllHerb.Name = "barItemExpandAllHerb";
          // 
          // barItemCollapseAllHerb
          // 
          this.barItemCollapseAllHerb.Caption = "�۵����в�ҩ��ϸ";
          this.barItemCollapseAllHerb.CategoryGuid = new System.Guid("1326cc61-5bda-4e24-8653-0841c4db859f");
          this.barItemCollapseAllHerb.Id = 52;
          this.barItemCollapseAllHerb.Name = "barItemCollapseAllHerb";
          // 
          // panelContentEditor
          // 
          this.toolTipCtrl.SetAllowHtmlText(this.panelContentEditor, DevExpress.Utils.DefaultBoolean.Default);
          this.panelContentEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
          this.panelContentEditor.Location = new System.Drawing.Point(0, 266);
          this.panelContentEditor.LookAndFeel.SkinName = "Blue";
          this.panelContentEditor.Margin = new System.Windows.Forms.Padding(0);
          this.panelContentEditor.Name = "panelContentEditor";
          this.panelContentEditor.Size = new System.Drawing.Size(961, 111);
          this.panelContentEditor.TabIndex = 0;
          this.panelContentEditor.Visible = false;
          // 
          // popMenuOrderGridView
          // 
          this.popMenuOrderGridView.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDrugInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemUp, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDown),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSetGroup, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemAutoGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCancelGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemExpandHerbDetail, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCollapseHerbDetail),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemExpandAllHerb),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemCollapseAllHerb)});
          this.popMenuOrderGridView.Manager = this.orderBarManager;
          this.popMenuOrderGridView.Name = "popMenuOrderGridView";
          // 
          // AdviceEditor
          // 
          this.toolTipCtrl.SetAllowHtmlText(this, DevExpress.Utils.DefaultBoolean.Default);
          this.Appearance.Font = new System.Drawing.Font("����", 9F);
          this.Appearance.Options.UseFont = true;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoScroll = true;
          this.Controls.Add(this.popContainerSkinTestInfo);
          this.Controls.Add(this.gridCtrl);
          this.Controls.Add(this.panelContentEditor);
          this.Controls.Add(this.barDockControlLeft);
          this.Controls.Add(this.barDockControlRight);
          this.Controls.Add(this.barDockControlBottom);
          this.Controls.Add(this.barDockControlTop);
          this.LookAndFeel.SkinName = "Blue";
          this.MinimumSize = new System.Drawing.Size(800, 400);
          this.Name = "AdviceEditor";
          this.Size = new System.Drawing.Size(961, 400);
          ((System.ComponentModel.ISupportInitialize)(this.gridCtrl)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.advGridView)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemCheckResultPicture)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemStatusImage)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemDateEdit.VistaTimeProperties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemDateEdit)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemTimeEdit)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemContentEdit)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.orderBarManager)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repItemPopupSkinTestInfo)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.popContainerSkinTestInfo)).EndInit();
          this.popContainerSkinTestInfo.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.gridAllergic)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.gridViewAllergic)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.selectOrderCatalog)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.selectOrderStatus)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.panelContentEditor)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.popMenuOrderGridView)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraGrid.GridControl gridCtrl;
      private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advGridView;
      private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandBeginInfo;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColStartDate;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColStartTime;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColContent;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCreator;
      private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandAuditInfo;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColAuditDate;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColAuditTime;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColAuditor;
      private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandExecuteInfo;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColExecuteDate;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColExecuteTime;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColExecutor;
      private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandCeaseInfo;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCeaseDate;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCeaseTime;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCeasor;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColSerialNo;
      private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repItemDateEdit;
      private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repItemTimeEdit;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCeaseAuditor;
      private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repItemContentEdit;
      private DevExpress.XtraBars.BarManager orderBarManager;
      private DevExpress.XtraBars.Bar orderToolBar;
      private DevExpress.XtraBars.BarButtonItem barItemCut;
      private DevExpress.XtraBars.BarButtonItem barItemCopy;
      private DevExpress.XtraBars.BarButtonItem barItemPaste;
      private DevExpress.XtraBars.BarButtonItem barItemDelete;
      private DevExpress.XtraBars.BarButtonItem barItemUp;
      private DevExpress.XtraBars.BarButtonItem barItemDown;
      private DevExpress.XtraBars.BarButtonItem barItemCease;
      private DevExpress.XtraBars.BarButtonItem barItemSetGroup;
      private DevExpress.XtraBars.BarEditItem barItemFilterStatus;
      private DevExpress.XtraEditors.Repository.RepositoryItemComboBox selectOrderCatalog;
      private System.Windows.Forms.ImageList imageListSmall;
      private DevExpress.XtraBars.BarEditItem barItemOrderCatalog;
      private DevExpress.XtraBars.BarButtonItem barItemCancel;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColStatus;
      private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repItemStatusImage;
      private DevExpress.XtraBars.BarButtonItem barItemSave;
      private DevExpress.XtraBars.BarButtonItem barItemExit;
      private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox selectOrderStatus;
      private DevExpress.XtraBars.BarButtonItem barItemCancelGroup;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColGroupSerialNo;
      private DevExpress.XtraBars.BarButtonItem barItemAudit;
      private DevExpress.XtraBars.BarButtonItem barItemDrugManual;
      private DevExpress.XtraBars.BarButtonItem barItemCheckOrder;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCheckResult;
      private System.Windows.Forms.ImageList imageListCheckFlag;
      private DevExpress.XtraBars.BarButtonItem barItemMultiSelect;
      private DevExpress.Utils.DefaultToolTipController toolTipCtrl;
      private DevExpress.XtraGrid.GridControl gridAllergic;
      private DevExpress.XtraGrid.Views.Grid.GridView gridViewAllergic;
      private DevExpress.XtraGrid.Columns.GridColumn gridColDrugName;
      private DevExpress.XtraGrid.Columns.GridColumn gridColBeginDate;
      private DevExpress.XtraGrid.Columns.GridColumn gridColEndDate;
      private DevExpress.XtraGrid.Columns.GridColumn gridColResult;
      private DevExpress.XtraBars.BarCheckItem barItemEditRegion;
      private DevExpress.XtraBars.BarCheckItem barItemExtraInfo;
      private DevExpress.XtraEditors.PanelControl panelContentEditor;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraBars.BarButtonItem barItemSubmit;
      private DevExpress.XtraBars.BarCheckItem barItemTempOrder;
      private DevExpress.XtraBars.BarCheckItem barItemLongOrder;
      private DevExpress.XtraBars.BarCheckItem barItemStateAll;
      private DevExpress.XtraBars.BarCheckItem barItemStateNew;
      private DevExpress.XtraBars.BarCheckItem barItemStateAvailably;
      private DevExpress.XtraBars.PopupMenu popMenuOrderGridView;
      private DevExpress.XtraBars.BarEditItem barItemSkinTestInfo;
      private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repItemPopupSkinTestInfo;
      private DevExpress.XtraEditors.PopupContainerControl popContainerSkinTestInfo;
      private DevExpress.XtraBars.BarButtonItem barItemPrint;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCeaseAuditDate;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColCeaseAuditorTime;
      private DevExpress.XtraBars.Bar statusBar;
      private DevExpress.XtraBars.BarStaticItem barItemHint;
      private DevExpress.XtraBars.BarStaticItem barItemLegendNew;
      private DevExpress.XtraBars.BarStaticItem barItemLegendAudit;
      private DevExpress.XtraBars.BarStaticItem barItemLegendExecuted;
      private DevExpress.XtraBars.BarStaticItem barItemLegendCeased;
      private DevExpress.XtraBars.BarStaticItem barItemLegendCancel;
      private DevExpress.XtraBars.BarStaticItem barItemLegendNotSynch;
      private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColSynchFlag;
      private DevExpress.XtraBars.BarButtonItem barItemRefresh;
      private DevExpress.XtraBars.BarButtonItem barItemDrugInfo;
      private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repItemCheckResultPicture;
      private DevExpress.XtraBars.BarButtonItem barItemExpandHerbDetail;
      private DevExpress.XtraBars.BarButtonItem barItemCollapseHerbDetail;
      private DevExpress.XtraBars.BarButtonItem barItemExpandAllHerb;
      private DevExpress.XtraBars.BarButtonItem barItemCollapseAllHerb;
      private DevExpress.XtraBars.BarButtonItem barItemAutoGroup;
      private DevExpress.XtraBars.BarStaticItem barItemContext;
      private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;

   }
}