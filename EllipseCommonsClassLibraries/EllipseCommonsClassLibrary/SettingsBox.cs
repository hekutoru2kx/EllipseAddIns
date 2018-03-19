﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using EllipseCommonsClassLibrary.Connections;
using EllipseCommonsClassLibrary.Utilities.RuntimeConfigSettings;

namespace EllipseCommonsClassLibrary
{
    partial class SettingsBox : Form
    {
        public SettingsBox(string productName)
        {
            InitializeComponent();
            this.Text = @"Opciones de Configuración";
            this.labelProductName.Text = productName;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void SettingsBox_Load(object sender, EventArgs e)
        {
            cbDebugErrors.Checked = Debugger.DebugErrors;
            cbDebugWarnings.Checked = Debugger.DebugWarnings;
            cbDebugQueries.Checked = Debugger.DebugQueries;
            tbLocalDataPath.Text = Configuration.LocalDataPath;
            tbServiceFileNetworkUrl.Text = Configuration.ServiceFilePath;
            tbTnsNameUrl.Text = Configuration.TnsnamesFilePath;
            cbForceRegionConfig.Checked = Debugger.ForceRegionalization;
            cbForceServerList.Checked = Configuration.IsServiceListForced;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Configuration.LocalDataPath = tbLocalDataPath.Text;
            Debugger.DebugErrors = cbDebugErrors.Checked;
            Debugger.DebugWarnings = cbDebugWarnings.Checked;
            Debugger.DebugQueries = cbDebugQueries.Checked;
            Configuration.ServiceFilePath = tbServiceFileNetworkUrl.Text;
            Configuration.IsServiceListForced = cbForceServerList.Checked;
            Debugger.DebugErrors = cbDebugErrors.Checked;
            Debugger.ForceRegionalization = cbForceRegionConfig.Checked;
            RuntimeConfigSettings.UpdateTnsUrlValue(tbTnsNameUrl.Text);
        }

        private void btnGenerateFromUrl_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration.GenerateEllipseConfigurationXmlFile(Configuration.DefaultServiceFilePath, tbServiceFileNetworkUrl.Text);
                MessageBox.Show(@"Se ha generado el archivo local de configuración de Ellipse a partir del archivo de red " + Configuration.DefaultServiceFilePath + Configuration.ConfigXmlFileName + @" al directorio especificado", @"Generate Local Ellipse Settings File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Generate Ellipse Settings File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGenerateDefault_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration.GenerateEllipseConfigurationXmlFile(tbServiceFileNetworkUrl.Text);
                MessageBox.Show(@"Se ha generado el archivo local de configuración de Ellipse de forma predeterminada", @"Generate Local Ellipse Settings File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Generate Ellipse Settings File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteLocalEllipseSettings_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration.DeleteEllipseConfigurationXmlFile();
                MessageBox.Show(@"Se ha eliminado el archivo local de configuración de Ellipse", @"Delete Local Ellipse Settings File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Delete Local Ellipse Settings File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRestoreServicePath_Click(object sender, EventArgs e)
        {
            tbServiceFileNetworkUrl.Text = Configuration.DefaultServiceFilePath;
        }

        private void btnRestoreLocalPath_Click(object sender, EventArgs e)
        {
            tbLocalDataPath.Text = Configuration.DefaultLocalDataPath;
        }

        private void btnOpenTnsnamesPath_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(tbTnsNameUrl.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"No se puede abrir la ruta especificada. Asegúrese que la ruta es correcta e intente de nuevo." + ex.Message, @"Abrir directorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpenLocalPath_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(tbLocalDataPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"No se puede abrir la ruta especificada. Asegúrese que la ruta es correcta e intente de nuevo." + ex.Message, @"Abrir directorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpenServicesPath_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(tbServiceFileNetworkUrl.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"No se puede abrir la ruta especificada. Asegúrese que la ruta es correcta e intente de nuevo." + ex.Message, @"Abrir directorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGenerateTnsnamesFile_Click(object sender, EventArgs e)
        {
            try
            {
                RuntimeConfigSettings.UpdateTnsUrlValue(tbTnsNameUrl.Text);
                Configuration.GenerateEllipseTnsnamesFile(Configuration.TnsnamesFilePath);
                MessageBox.Show(@"Se ha generado el archivo local de TNSNAMES de forma predeterminada", @"Generate Local Ellipse Tnsnames File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Generate Ellipse Tnsnames File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestoreTnsnamesUrl_Click(object sender, EventArgs e)
        {
            try
            {
                RuntimeConfigSettings.UpdateTnsUrlValue(Configuration.DefaultTnsnamesFilePath);
                tbTnsNameUrl.Text = Configuration.DefaultTnsnamesFilePath;
                MessageBox.Show(@"Se ha restaurado la ruta del archivo de TNSNAMES a su ubicación predeterminada. En caso de que este archivo no exista comuníquese con el administrador de sistemas", @"Restore Ellipse Tnsnames File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Restore Ellipse Tnsnames File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateCustomDb_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteCustomDb_Click(object sender, EventArgs e)
        {

        }
    }
}