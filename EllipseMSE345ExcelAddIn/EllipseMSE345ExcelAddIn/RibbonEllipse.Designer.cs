﻿namespace EllipseMSE345ExcelAddIn
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
            this.grpCondMonit = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.menuFormat = this.Factory.CreateRibbonMenu();
            this.btnFormatGeneral = this.Factory.CreateRibbonButton();
            this.btnFormatMntto = this.Factory.CreateRibbonButton();
            this.btnFormatPolines = this.Factory.CreateRibbonButton();
            this.btnAbout = this.Factory.CreateRibbonButton();
            this.drpEnviroment = this.Factory.CreateRibbonDropDown();
            this.btnCreate = this.Factory.CreateRibbonButton();
            this.tabEllipse.SuspendLayout();
            this.grpCondMonit.SuspendLayout();
            this.box1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabEllipse
            // 
            this.tabEllipse.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabEllipse.Groups.Add(this.grpCondMonit);
            this.tabEllipse.Label = "ELLIPSE 8";
            this.tabEllipse.Name = "tabEllipse";
            // 
            // grpCondMonit
            // 
            this.grpCondMonit.Items.Add(this.box1);
            this.grpCondMonit.Items.Add(this.drpEnviroment);
            this.grpCondMonit.Items.Add(this.btnCreate);
            this.grpCondMonit.Label = "MSE345";
            this.grpCondMonit.Name = "grpCondMonit";
            // 
            // box1
            // 
            this.box1.Items.Add(this.menuFormat);
            this.box1.Items.Add(this.btnAbout);
            this.box1.Name = "box1";
            // 
            // menuFormat
            // 
            this.menuFormat.Items.Add(this.btnFormatGeneral);
            this.menuFormat.Items.Add(this.btnFormatMntto);
            this.menuFormat.Items.Add(this.btnFormatPolines);
            this.menuFormat.Label = "Formatear";
            this.menuFormat.Name = "menuFormat";
            // 
            // btnFormatGeneral
            // 
            this.btnFormatGeneral.Label = "Estandar";
            this.btnFormatGeneral.Name = "btnFormatGeneral";
            this.btnFormatGeneral.ShowImage = true;
            this.btnFormatGeneral.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatGeneral_Click);
            // 
            // btnFormatMntto
            // 
            this.btnFormatMntto.Label = "Mantto";
            this.btnFormatMntto.Name = "btnFormatMntto";
            this.btnFormatMntto.ShowImage = true;
            this.btnFormatMntto.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatMntto_Click);
            // 
            // btnFormatPolines
            // 
            this.btnFormatPolines.Label = "Polines";
            this.btnFormatPolines.Name = "btnFormatPolines";
            this.btnFormatPolines.ShowImage = true;
            this.btnFormatPolines.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatPolines_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Label = "?";
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAbout_Click);
            // 
            // drpEnviroment
            // 
            this.drpEnviroment.Label = "Env.";
            this.drpEnviroment.Name = "drpEnviroment";
            // 
            // btnCreate
            // 
            this.btnCreate.Label = "Cargar Info";
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCreate_Click);
            // 
            // RibbonEllipse
            // 
            this.Name = "RibbonEllipse";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabEllipse);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonEllipse_Load);
            this.tabEllipse.ResumeLayout(false);
            this.tabEllipse.PerformLayout();
            this.grpCondMonit.ResumeLayout(false);
            this.grpCondMonit.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabEllipse;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpCondMonit;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown drpEnviroment;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCreate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatGeneral;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuFormat;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatMntto;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAbout;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatPolines;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonEllipse RibbonEllipse
        {
            get { return this.GetRibbon<RibbonEllipse>(); }
        }
    }
}
