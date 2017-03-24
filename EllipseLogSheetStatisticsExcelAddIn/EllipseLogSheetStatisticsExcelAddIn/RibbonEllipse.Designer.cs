﻿namespace EllipseLogSheetStatisticsExcelAddIn
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
            this.grpLogSheetStatistics = this.Factory.CreateRibbonGroup();
            this.btnFormatLogSheet = this.Factory.CreateRibbonButton();
            this.drpEnviroment = this.Factory.CreateRibbonDropDown();
            this.menuActions = this.Factory.CreateRibbonMenu();
            this.btnLoadModel = this.Factory.CreateRibbonButton();
            this.btnCreateLogSheetStatistics = this.Factory.CreateRibbonButton();
            this.tabEllipse.SuspendLayout();
            this.grpLogSheetStatistics.SuspendLayout();
            // 
            // tabEllipse
            // 
            this.tabEllipse.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabEllipse.Groups.Add(this.grpLogSheetStatistics);
            this.tabEllipse.Label = "ELLIPSE 8";
            this.tabEllipse.Name = "tabEllipse";
            // 
            // grpLogSheetStatistics
            // 
            this.grpLogSheetStatistics.Items.Add(this.btnFormatLogSheet);
            this.grpLogSheetStatistics.Items.Add(this.drpEnviroment);
            this.grpLogSheetStatistics.Items.Add(this.menuActions);
            this.grpLogSheetStatistics.Label = "LogSheetStatistics v1.0.4";
            this.grpLogSheetStatistics.Name = "grpLogSheetStatistics";
            // 
            // btnFormatLogSheet
            // 
            this.btnFormatLogSheet.Label = "Formatear Hoja";
            this.btnFormatLogSheet.Name = "btnFormatLogSheet";
            this.btnFormatLogSheet.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatLogSheet_Click);
            // 
            // drpEnviroment
            // 
            this.drpEnviroment.Label = "Env.";
            this.drpEnviroment.Name = "drpEnviroment";
            // 
            // menuActions
            // 
            this.menuActions.Items.Add(this.btnLoadModel);
            this.menuActions.Items.Add(this.btnCreateLogSheetStatistics);
            this.menuActions.Label = "Acciones";
            this.menuActions.Name = "menuActions";
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Label = "Obtener Modelo";
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.ShowImage = true;
            this.btnLoadModel.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoadModel_Click);
            // 
            // btnCreateLogSheetStatistics
            // 
            this.btnCreateLogSheetStatistics.Label = "Create LogSheet";
            this.btnCreateLogSheetStatistics.Name = "btnCreateLogSheetStatistics";
            this.btnCreateLogSheetStatistics.ShowImage = true;
            this.btnCreateLogSheetStatistics.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCreateLogSheetStatistics_Click);
            // 
            // RibbonEllipse
            // 
            this.Name = "RibbonEllipse";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabEllipse);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonEllipse_Load);
            this.tabEllipse.ResumeLayout(false);
            this.tabEllipse.PerformLayout();
            this.grpLogSheetStatistics.ResumeLayout(false);
            this.grpLogSheetStatistics.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabEllipse;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpLogSheetStatistics;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown drpEnviroment;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCreateLogSheetStatistics;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatLogSheet;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLoadModel;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuActions;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonEllipse RibbonEllipse
        {
            get { return this.GetRibbon<RibbonEllipse>(); }
        }
    }
}