﻿namespace EllipseLabourCostingExcelAddIn
{
    partial class RibbonEllipse : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonEllipse()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tabEllipse = this.Factory.CreateRibbonTab();
            this.grpEllipse = this.Factory.CreateRibbonGroup();
            this.menuFormat = this.Factory.CreateRibbonMenu();
            this.menuGroupFormat = this.Factory.CreateRibbonMenu();
            this.btnFormatHeader = this.Factory.CreateRibbonButton();
            this.btnFormatDefault = this.Factory.CreateRibbonButton();
            this.btnFormatMso850 = this.Factory.CreateRibbonButton();
            this.btnFormatElecsa = this.Factory.CreateRibbonButton();
            this.drpEnviroment = this.Factory.CreateRibbonDropDown();
            this.menuActions = this.Factory.CreateRibbonMenu();
            this.btnLoadLaborSheet = this.Factory.CreateRibbonButton();
            this.btnReviewWorkOrder = this.Factory.CreateRibbonButton();
            this.btnCleanSheet = this.Factory.CreateRibbonButton();
            this.btnStopThread = this.Factory.CreateRibbonButton();
            this.tabEllipse.SuspendLayout();
            this.grpEllipse.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabEllipse
            // 
            this.tabEllipse.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabEllipse.Groups.Add(this.grpEllipse);
            this.tabEllipse.Label = "ELLIPSE 8";
            this.tabEllipse.Name = "tabEllipse";
            // 
            // grpEllipse
            // 
            this.grpEllipse.Items.Add(this.menuFormat);
            this.grpEllipse.Items.Add(this.drpEnviroment);
            this.grpEllipse.Items.Add(this.menuActions);
            this.grpEllipse.Label = "Labour Costing v1.0.7";
            this.grpEllipse.Name = "grpEllipse";
            // 
            // menuFormat
            // 
            this.menuFormat.Items.Add(this.menuGroupFormat);
            this.menuFormat.Items.Add(this.btnFormatMso850);
            this.menuFormat.Items.Add(this.btnFormatElecsa);
            this.menuFormat.Label = "&Formato";
            this.menuFormat.Name = "menuFormat";
            // 
            // menuGroupFormat
            // 
            this.menuGroupFormat.Items.Add(this.btnFormatHeader);
            this.menuGroupFormat.Items.Add(this.btnFormatDefault);
            this.menuGroupFormat.Label = "Formato &Grupo";
            this.menuGroupFormat.Name = "menuGroupFormat";
            this.menuGroupFormat.ShowImage = true;
            // 
            // btnFormatHeader
            // 
            this.btnFormatHeader.Label = "Formatear &Encabezado";
            this.btnFormatHeader.Name = "btnFormatHeader";
            this.btnFormatHeader.ShowImage = true;
            this.btnFormatHeader.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatHeader_Click);
            // 
            // btnFormatDefault
            // 
            this.btnFormatDefault.Label = "Formato &Predeterminado";
            this.btnFormatDefault.Name = "btnFormatDefault";
            this.btnFormatDefault.ShowImage = true;
            this.btnFormatDefault.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatDefault_Click);
            // 
            // btnFormatMso850
            // 
            this.btnFormatMso850.Label = "Formato &MSO850";
            this.btnFormatMso850.Name = "btnFormatMso850";
            this.btnFormatMso850.ShowImage = true;
            this.btnFormatMso850.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatMso850_Click);
            // 
            // btnFormatElecsa
            // 
            this.btnFormatElecsa.Label = "Formato &Elecsa";
            this.btnFormatElecsa.Name = "btnFormatElecsa";
            this.btnFormatElecsa.ShowImage = true;
            this.btnFormatElecsa.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatElecsa_Click);
            // 
            // drpEnviroment
            // 
            this.drpEnviroment.Label = "&Env.";
            this.drpEnviroment.Name = "drpEnviroment";
            // 
            // menuActions
            // 
            this.menuActions.Items.Add(this.btnLoadLaborSheet);
            this.menuActions.Items.Add(this.btnReviewWorkOrder);
            this.menuActions.Items.Add(this.btnCleanSheet);
            this.menuActions.Items.Add(this.btnStopThread);
            this.menuActions.Label = "&Acciones";
            this.menuActions.Name = "menuActions";
            // 
            // btnLoadLaborSheet
            // 
            this.btnLoadLaborSheet.Label = "&Cargar Hoja de Labor";
            this.btnLoadLaborSheet.Name = "btnLoadLaborSheet";
            this.btnLoadLaborSheet.ShowImage = true;
            this.btnLoadLaborSheet.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoadLaborSheet_Click);
            // 
            // btnReviewWorkOrder
            // 
            this.btnReviewWorkOrder.Label = "Verificar OTs";
            this.btnReviewWorkOrder.Name = "btnReviewWorkOrder";
            this.btnReviewWorkOrder.ShowImage = true;
            this.btnReviewWorkOrder.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnReviewWorkOrder_Click);
            // 
            // btnCleanSheet
            // 
            this.btnCleanSheet.Label = "&Limpiar Hoja";
            this.btnCleanSheet.Name = "btnCleanSheet";
            this.btnCleanSheet.ShowImage = true;
            this.btnCleanSheet.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCleanSheet_Click);
            // 
            // btnStopThread
            // 
            this.btnStopThread.Label = "&Detener Procesos";
            this.btnStopThread.Name = "btnStopThread";
            this.btnStopThread.ShowImage = true;
            this.btnStopThread.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnStopThread_Click);
            // 
            // RibbonEllipse
            // 
            this.Name = "RibbonEllipse";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabEllipse);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonEllipse_Load);
            this.tabEllipse.ResumeLayout(false);
            this.tabEllipse.PerformLayout();
            this.grpEllipse.ResumeLayout(false);
            this.grpEllipse.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabEllipse;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpEllipse;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuFormat;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuGroupFormat;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatHeader;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatDefault;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatMso850;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatElecsa;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown drpEnviroment;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuActions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLoadLaborSheet;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCleanSheet;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnStopThread;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnReviewWorkOrder;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonEllipse RibbonEllipse
        {
            get { return this.GetRibbon<RibbonEllipse>(); }
        }
    }
}