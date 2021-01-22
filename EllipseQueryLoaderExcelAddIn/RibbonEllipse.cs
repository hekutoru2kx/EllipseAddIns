﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Tools.Ribbon;
using SharedClassLibrary;

using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Threading;
using SharedClassLibrary.Ellipse;
using SharedClassLibrary.Ellipse.Forms;
using SharedClassLibrary.Ellipse.Connections;
using SharedClassLibrary.Vsto.Excel;
using Debugger = SharedClassLibrary.Utilities.Debugger;

namespace EllipseQueryLoaderExcelAddIn
{
    public partial class RibbonEllipse
    {
        ExcelStyleCells _cells;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private EllipseFunctions _eFunctions;
        private Excel.Application _excelApp; 
        private const string SheetName01 = "QueryInformation";
        private const string SheetName02 = "QueryResults";
        private const string TableName02 = "QueryResultsTable";
        private const string ValidationSheetName = "ValidationSheet";

        private const string TableParameter = "ParametersTable";
        private const int TitleRow01 = 8;
        private const int TitleRow02 = 4;
        private Thread _thread;
        private string DefaultCharParameter = ":";
        private void RibbonEllipse_Load(object sender, RibbonUIEventArgs e)
        {
            LoadSettings();
            
            
        }
        public void LoadSettings()
        {
            var settings = new Settings();
            _eFunctions = new EllipseFunctions();

            _excelApp = Globals.ThisAddIn.Application;

            var environments = Environments.GetEnvironmentList();
            foreach (var env in environments)
            {
                var item = Factory.CreateRibbonDropDownItem();
                item.Label = env;
                drpEnvironment.Items.Add(item);
            }

            var otherDb = Factory.CreateRibbonDropDownItem();
            otherDb.Label = @"Other DB";
            drpEnvironment.Items.Add(otherDb);

            //settings.SetDefaultCustomSettingValue("OptionName1", "false");
            //settings.SetDefaultCustomSettingValue("OptionName2", "OptionValue2");
            //settings.SetDefaultCustomSettingValue("OptionName3", "OptionValue3");



            //Setting of Configuration Options from Config File (or default)
            try
            {
                settings.LoadCustomSettings();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, SharedResources.Settings_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //var optionItem1Value = MyUtilities.IsTrue(settings.GetCustomSettingValue("OptionName1"));
            //var optionItem1Value = settings.GetCustomSettingValue("OptionName2");
            //var optionItem1Value = settings.GetCustomSettingValue("OptionName3");

            //cbCustomSettingOption.Checked = optionItem1Value;
            //optionItem2.Text = optionItem2Value;
            //optionItem3 = optionItem3Value;

            //
            settings.SaveCustomSettings();


        }
        private void btnFormatSheet_Click(object sender, RibbonControlEventArgs e)
        {
            FormatSheet();
        }

        private void btnExecuteQuery_Click(object sender, RibbonControlEventArgs e)
        {
            //si ya hay un thread corriendo que no se ha detenido
            if (_thread != null && _thread.IsAlive) return;
            if (_excelApp.ActiveWorkbook.ActiveSheet.Name.Equals(SheetName01) || _excelApp.ActiveWorkbook.ActiveSheet.Name.Equals(SheetName02))
                _thread = new Thread(GetQueryResult);
            else
            {
                MessageBox.Show(@"La hoja de Excel no tiene el formato válido para realizar la acción");
                return;
            }
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
        }

        private void btnStopThread_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (_thread == null || !_thread.IsAlive) return;
                _thread.Abort();
                _cells.SetCursorDefault();
            }
            catch (ThreadAbortException ex)
            {
                MessageBox.Show(@"Se ha detenido el proceso. " + ex.Message);
            }
        }

        private void btnCleanSheet_Click(object sender, RibbonControlEventArgs e)
        {
            var cp = new ExcelStyleCells(_excelApp, SheetName01);//cells parameters
            var cr = new ExcelStyleCells(_excelApp, SheetName02);//cells results
            //Elimino los registros anteriores
            cr.ClearTableRange(TableName02);
            cr.DeleteTableRange(TableName02);
            cp.GetCell(2, 5).Value2 = "";
            cp.GetCell(2, 6).Value2 = "";
            LoadQueryParameters();
        }

        private void btnReadFromText_Click(object sender, RibbonControlEventArgs e)
        {
            //si ya hay un thread corriendo que no se ha detenido
            if (_thread != null && _thread.IsAlive) return;
            if (_excelApp.ActiveWorkbook.ActiveSheet.Name.Equals(SheetName01))
                _thread = new Thread(LoadQueryParameters);
            else
            {
                MessageBox.Show(@"La hoja de Excel no tiene el formato válido para realizar la acción");
                return;
            }
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
        }

        private void btnReadFromFile_Click(object sender, RibbonControlEventArgs e)
        {
            //si ya hay un thread corriendo que no se ha detenido
            if (_thread != null && _thread.IsAlive) return;
            if (_excelApp.ActiveWorkbook.ActiveSheet.Name.Equals(SheetName01))
                _thread = new Thread(LoadQueryFromFile);
            else
            {
                MessageBox.Show(@"La hoja de Excel no tiene el formato válido para realizar la acción");
                return;
            }
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
        }

        public void FormatSheet()
        {
            try
            {
                _excelApp = Globals.ThisAddIn.Application;
                _excelApp.Workbooks.Add();
                while (_excelApp.ActiveWorkbook.Sheets.Count < 3)
                    _excelApp.ActiveWorkbook.Worksheets.Add();
                if (_cells == null)
                    _cells = new ExcelStyleCells(_excelApp);
                _excelApp.ActiveWorkbook.ActiveSheet.Name = SheetName01;
                _eFunctions.SetDBSettings(drpEnvironment.SelectedItem.Label);
                _cells.CreateNewWorksheet(ValidationSheetName);

                _cells.GetCell("A1").Value = "CERREJÓN";
                _cells.GetCell("A1").Style = _cells.GetStyle(StyleConstants.HeaderDefault);
                _cells.MergeCells("A1", "A2");

                _cells.GetCell("B1").Value = "QUERY LOADER";
                _cells.GetCell("B1").Style = StyleConstants.HeaderDefault;

                _cells.GetCell(3, 1).Value = "OBLIGATORIO";
                _cells.GetCell(3, 1).Style = StyleConstants.TitleRequired;
                _cells.GetCell(3, 2).Value = "OPCIONAL";
                _cells.GetCell(3, 2).Style = StyleConstants.TitleOptional;
                _cells.GetCell(3, 3).Value = "INFORMATIVO";
                _cells.GetCell(3, 3).Style = StyleConstants.TitleInformation;

                _cells.GetCell(1, 5).Value = "QUERY";
                _cells.GetCell(1, 5).Style = StyleConstants.Option;
                _cells.GetCell(2, 5).Value = "";
                _cells.GetCell(2, 5).Style = StyleConstants.Select;
                _cells.GetCell(1, 6).Value = "DECODED QUERY";
                _cells.GetCell(1, 6).Style = StyleConstants.Option;
                _cells.GetCell(2, 6).Value = "";
                _cells.GetCell(2, 6).Style = StyleConstants.Option;

                _excelApp.ActiveWorkbook.ActiveSheet.Cells.Columns.AutoFit();
                _cells.GetCell(2, 6).ColumnWidth = 60;

                _cells.GetCell(1, TitleRow01).Value = "Parameter";
                _cells.GetCell(2, TitleRow01).Value = "Value";
                _cells.GetCell(3, TitleRow01).Value = "Operator";

                var paramList = Database.ParamType.GetParamList();
                _cells.SetValidationList(_cells.GetCell(TitleRow01 + 1, 2), paramList, ValidationSheetName, 1);

                _cells.FormatAsTable(_cells.GetRange(1, TitleRow01, 3, TitleRow01 + 1), TableParameter);

                //CONSTRUYO LA HOJA 2 - QUERYRESULT
                // ReSharper disable once UseIndexedProperty
                _excelApp.ActiveWorkbook.Sheets.get_Item(2).Activate();
                
                _excelApp.ActiveWorkbook.ActiveSheet.Name = SheetName02;
                _cells.GetCell("A1").Value = "CERREJÓN";
                _cells.GetCell("A1").Style = _cells.GetStyle(StyleConstants.HeaderDefault);
                _cells.MergeCells("A1", "A2");

                _cells.GetCell("B1").Value = "QUERY LOADER RESULTS";
                _cells.GetCell("B1").Style = StyleConstants.HeaderDefault;

                _excelApp.ActiveWorkbook.Sheets[1].Select(Type.Missing);

            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse:FormatSheet()", "\n\rMessage:" + ex.Message + "\n\rSource:" + ex.Source + "\n\rStackTrace:" + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error al intentar formatear la hoja. " + ex.Message);
            }
        }

        public void LoadQueryFromFile()
        {
            try
            {
                if (_cells == null)
                    _cells = new ExcelStyleCells(_excelApp);
                _cells.SetCursorWait();

                var openFileDialog = new OpenFileDialog
                {

                    InitialDirectory = @"c:\\",
                    Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*",
                    RestoreDirectory = true,
                };

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                var filePath = openFileDialog.FileName;
                var sqlQuery = File.ReadAllText(filePath, Encoding.UTF8);
                
                
                _cells.GetCell(2, 5).Value = sqlQuery;
                LoadQueryParameters();
            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse:LoadQueryFromFile()", "\n\rMessage:" + ex.Message + "\n\rSource:" + ex.Source + "\n\rStackTrace:" + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error. " + ex.Message);
            }
            finally
            {
                if (_cells != null) _cells.SetCursorDefault();
            }
        }
        public void GetQueryResult()
        {
            try
            {
                if (_cells == null)
                    _cells = new ExcelStyleCells(_excelApp);
                _cells.SetCursorWait();

                var cp = new ExcelStyleCells(_excelApp, SheetName01); //cells parameters
                var cr = new ExcelStyleCells(_excelApp, SheetName02); //cells results

                var sqlQueryEncoded = _cells.GetEmptyIfNull(cp.GetCell(2, 5).Value);
                var sqlQueryDecoded = DecodeSqlQueryParameters(sqlQueryEncoded);
                cp.GetCell(2, 6).Value = sqlQueryDecoded;
                

                // ReSharper disable once UseIndexedProperty
                _excelApp.ActiveWorkbook.Sheets.get_Item(SheetName02).Activate();

                //Elimino los registros anteriores
                cr.ClearTableRange(TableName02);
                cr.DeleteTableRange(TableName02);

                if (drpEnvironment.SelectedItem.Label.Equals("OTRA DB"))
                {
                    // ReSharper disable once UnusedVariable
                    var sqlDataReader = _eFunctions.GetSqlQueryResult(sqlQueryDecoded);
                    throw new NotImplementedException("Esta función no ha sido implementada");
                    //TO DO
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                {
                    _eFunctions.SetDBSettings(drpEnvironment.SelectedItem.Label);
                    var dataReader = _eFunctions.GetQueryResult(sqlQueryDecoded);

                    if (dataReader == null)
                        return;

                    //Cargo el encabezado de la tabla y doy formato
                    for (var i = 0; i < dataReader.FieldCount; i++)
                        cr.GetCell(i + 1, TitleRow02).Value2 = "'" + dataReader.GetName(i);

                    _cells.FormatAsTable(cr.GetRange(1, TitleRow02, dataReader.FieldCount, TitleRow02 + 1), TableName02);

                    //cargo los datos 
                    if (dataReader.IsClosed || !dataReader.HasRows) return;


                    var currentRow = TitleRow02 + 1;
                    while (dataReader.Read())
                    {
                        for (var i = 0; i < dataReader.FieldCount; i++)
                            cr.GetCell(i + 1, currentRow).Value2 = "'" + dataReader[i].ToString().Trim();
                        currentRow++;
                    }

                }

            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse:GetQueryResult()", "\n\rMessage:" + ex.Message + "\n\rSource:" + ex.Source + "\n\rStackTrace:" + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error. " + ex.Message);
            }
            finally
            {
                if (_cells != null) _cells.SetCursorDefault();
                _eFunctions.CloseConnection();
            }
        }

        private string DecodeSqlQueryParameters(string sqlQueryEncoded)
        {
            var query = sqlQueryEncoded;
            var currentRow = TitleRow01 + 1;

            while (!string.IsNullOrWhiteSpace(_cells.GetEmptyIfNull(_cells.GetCell(1, currentRow).Value2)))
            {
                
                
                var paramName = _cells.GetEmptyIfNull(_cells.GetCell(1, currentRow).Value2);
                var paramValue = _cells.GetEmptyIfNull(_cells.GetCell(2, currentRow).Value2);
                string operType = _cells.GetEmptyIfNull(_cells.GetCell(3, currentRow).Value2);
                var parameters = "";

                switch (operType)
                {
                    case Database.ParamType.InList:
                        var values = paramValue.Split(',');

                        // ReSharper disable once LoopCanBeConvertedToQuery
                        foreach (var v in values)
                            parameters = parameters + ",'" + v.Trim() + "'";
                        parameters = " " + Database.ParamType.InList + " (" + parameters + ")";
                        query = query.Replace(paramName, parameters);
                        query = query.Replace("(,", "(");
                        break;
                    case "": //none
                        query = query.Replace(paramName, "'" + paramValue + "'");
                        break;
                    default:
                        parameters = operType + " '" + paramValue + "'";
                        query = query.Replace(paramName, parameters);
                        break;
                }

                currentRow += 1;
            }
            return query;
        }

        public void LoadQueryParameters()
        {
            try
            {
                var cp = new ExcelStyleCells(_excelApp, SheetName01);

                _cells?.SetCursorWait();
                var query = cp.GetEmptyIfNull(cp.GetCell(2, 5).Value);

                cp.ClearTableRange(TableParameter);

                var currentRow = TitleRow01 + 1;
                var paramList = new List<string>();
                foreach (Match match in Regex.Matches(query, "(\\" + DefaultCharParameter + "\\w+)"))
                    if (!paramList.Contains(match.Groups[1].Value))
                        paramList.Add(match.Groups[1].Value);
                
                foreach (var param in paramList)
                {
                    cp.GetCell(1, currentRow).Value2 = param;
                    cp.GetCell(1, currentRow).NumberFormat = NumberFormatConstants.Text;

                    currentRow += 1;
                }
            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse:LoadQueryParameters()", "\n\rMessage:" + ex.Message + "\n\rSource:" + ex.Source + "\n\rStackTrace:" + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error. " + ex.Message);
            }
            finally
            {
                _cells?.SetCursorDefault();
            }
        }

        

        private void btnAbout_Click(object sender, RibbonControlEventArgs e)
        {
            new AboutBoxExcelAddIn().ShowDialog();
        }
    }
}
