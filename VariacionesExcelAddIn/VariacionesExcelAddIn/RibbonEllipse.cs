﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using EllipseCommonsClassLibrary;
using EllipseCommonsClassLibrary.Classes;
using EllipseCommonsClassLibrary.Connections;
using LinqToExcel;
using LinqToExcel.Attributes;
using LinqToExcel.Query;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace VariacionesExcelAddIn
{
    public partial class RibbonEllipse
    {

        private Application _excelApp;
        private ListObject _excelSheetItems;
        private ExcelStyleCells _cells;
        private EllipseFunctions _eFunctions = new EllipseFunctions();


        private const string SheetName = "por centro detalle";

        private const int TittleRow = 5;
        private const int ResultColumn = 12;

        private void RibbonEllipse_Load(object sender, RibbonUIEventArgs e)
        {
            _excelApp = Globals.ThisAddIn.Application;
        }

        private void btnImportFile_Click(object sender, RibbonControlEventArgs e)
        {
            ImportFile();
        }

        private void ImportFile()
        {
            var excelBook = _excelApp.ActiveWorkbook;
            Worksheet excelSheet = excelBook.ActiveSheet;

            _excelApp = Globals.ThisAddIn.Application;
            if (_cells == null)
                _cells = new ExcelStyleCells(_excelApp);
            _cells.SetCursorWait();

            if (_excelSheetItems != null)
                _cells.GetRange(1, TittleRow + 1, ResultColumn, _excelSheetItems.ListRows.Count + TittleRow).Delete();

            var openFileDialog1 = new OpenFileDialog
            {
                Filter = @"Archivos xlsx |*.xlsx",
                Title = @"Programa de Lectura",
                InitialDirectory = @"D:\"
            };

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            var filePath = openFileDialog1.FileName;

            var excel = new ExcelQueryFactory(filePath) { TrimSpaces = TrimSpacesType.Both };
            var data = from c in excel.WorksheetRangeNoHeader("B15", "M5000", SheetName)
                       where c[0] != null
                       select c;

            var allAccounts = data.Select(a => new Account()
            {
                AccountCode = a[0],
                ExpElement = a[1],
                Year = "2018",
                Month = "07",
                Budget = a[2],
                Actual = a[3],
                Variation = a[4],
                Forex = a[5],
                InputPrice = a[6],
                Volume = a[7],
                Sustainable = a[8],
                OtherEff = a[9],
                OtherOver = a[10],
                Timing = a[11]
            }).ToList();

            var mdcAccounts = GetMdcAccounts("PUERTO BOLIVAR");

            var expelements = GetExpElements();
            
            var accounts = allAccounts.Where(x => mdcAccounts.Any(y => y.AccountCode == x.AccountCode));
            accounts = accounts.Where(x => expelements.Any(y => y == x.ExpElement));

            foreach (var a in accounts)
            {
                try
                {
                    SetAccountInfo(a);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            excelSheet.Cells.Columns.AutoFit();
            excelSheet.Cells.Rows.AutoFit();
            _cells.SetCursorDefault();
        }

        public void SetAccountInfo(Account account)
        {
            var ef = new EllipseFunctions();
            ef.SetDBSettings(Environments.SigcorProductivo);
            var sqlQuery = Queries.SetMdcAccountsInfoQuery(ef.dbReference, ef.dbLink, account);
            ef.GetQueryResult(sqlQuery);
        }

        private static IEnumerable<string> GetExpElements()
        {
            var ef = new EllipseFunctions();
            ef.SetDBSettings(Environments.SigcorProductivo);
            var sqlQuery = Queries.GetExpElements(ef.dbReference, ef.dbLink);
            var drResources = ef.GetQueryResult(sqlQuery);
            var list = new List<string>();

            if (drResources == null || drResources.IsClosed || !drResources.HasRows) return list;
            while (drResources.Read())
            {
                list.Add(drResources["EXPELEMENT"].ToString().Trim());
            }

            return list;
        }

        public List<Account> GetMdcAccounts(string superintendencia)
        {
            var ef = new EllipseFunctions();
            ef.SetDBSettings(Environments.SigcorProductivo);
            var sqlQuery = Queries.GetMdcAccountsQuery(ef.dbReference, ef.dbLink, superintendencia);
            var drResources = ef.GetQueryResult(sqlQuery);
            var list = new List<Account>();

            if (drResources == null || drResources.IsClosed || !drResources.HasRows) return list;
            while (drResources.Read())
            {
                var a = new Account
                {
                    AccountCode = drResources["CENTRO_RESP"].ToString().Trim()
                };
                list.Add(a);
            }   
            return list;
        }

        public class Queries
        {
            public static string GetMdcAccountsQuery(string efDbReference, string efDbLink, string superintendencia = "")
            {
                var query = "SELECT " +
                            "     CEN.CENTRO_RESP " +
                            " FROM " +
                            "     SIGMDC.MDC_CENTROS CEN " +
                            " WHERE " +
                            "     CEN.SUPERINTENDENCIA   = '" + superintendencia + "' ";
                return query;
            }

            public static string SetMdcAccountsInfoQuery(string efDbReference, string efDbLink, Account a)
            {
                var query = "MERGE INTO CONSULBO.MDC_PRESUPUESTO T USING ( " +
                            "                                                 SELECT " +
                            "                                                     '" + a.AccountCode + "' ACCOUNT, " +
                            "                                                     '" + a.ExpElement + "' EXPELEMENT, " +
                            "                                                     '" + a.Year + "' YEAR, " +
                            "                                                     '" + a.Month + "' MONTH, " +
                            "                                                     '" + a.Budget + "' BUDGET, " +
                            "                                                     '" + a.Actual + "' ACTUAL, " +
                            "                                                     '" + a.Variation + "' VARIATION, " +
                            "                                                     '" + a.Forex + "' FOREX, " +
                            "                                                     '" + a.InputPrice + "' INPUTPRICE, " +
                            "                                                     '" + a.Volume + "' VOLUME, " +
                            "                                                     '" + a.Sustainable + "' SUSTAINABLE, " +
                            "                                                     '" + a.OtherEff + "' OTHEREFF, " +
                            "                                                     '" + a.OtherOver + "' OTHEROVER, " +
                            "                                                     '" + a.Timing + "' TIMING " +
                            "                                                 FROM " +
                            "                                                     DUAL " +
                            "                                             ) " +
                            "S ON ( T.ACCOUNT = S.ACCOUNT " +
                            "       AND T.EXPELEMENT = S.EXPELEMENT " +
                            "       AND T.YEAR = S.YEAR " +
                            "       AND T.MONTH = S.MONTH ) " +
                            "WHEN MATCHED THEN UPDATE SET T.BUDGET = S.BUDGET, " +
                            "T.ACTUAL = S.ACTUAL, " +
                            "T.VARIATION = S.VARIATION, " +
                            "T.FOREX = S.FOREX, " +
                            "T.INPUTPRICE = S.INPUTPRICE, " +
                            "T.VOLUME = S.VOLUME, " +
                            "T.SUSTAINABLE = S.SUSTAINABLE, " +
                            "T.OTHEREFF = S.OTHEREFF, " +
                            "T.OTHEROVER = S.OTHEROVER, " +
                            "                             T.TIMING = S.TIMING " +
                            "WHEN NOT MATCHED THEN INSERT ( " +
                            "    ACCOUNT, " +
                            "    EXPELEMENT, " +
                            "    YEAR, " +
                            "    MONTH, " +
                            "    BUDGET, " +
                            "    ACTUAL, " +
                            "    VARIATION, " +
                            "    FOREX, " +
                            "    INPUTPRICE, " +
                            "    VOLUME, " +
                            "    SUSTAINABLE, " +
                            "    OTHEREFF, " +
                            "    OTHEROVER, " +
                            "    TIMING ) VALUES ( " +
                            "    S.ACCOUNT, " +
                            "    S.EXPELEMENT, " +
                            "    S.YEAR, " +
                            "    S.MONTH, " +
                            "    S.BUDGET, " +
                            "    S.ACTUAL, " +
                            "    S.VARIATION, " +
                            "    S.FOREX, " +
                            "    S.INPUTPRICE, " +
                            "    S.VOLUME, " +
                            "    S.SUSTAINABLE, " +
                            "    S.OTHEREFF, " +
                            "    S.OTHEROVER, " +
                            "    S.TIMING )";
                return query;
            }

            public static string GetExpElements(string efDbReference, string efDbLink)
            {
                const string query = "SELECT EXPELEMENT FROM MDC_PRESUPUESTO_DETALLE";
                return query;
            }
        }

        public class Account
        {
            [ExcelColumn("Account Code")]
            public string AccountCode { get; set; }

            [ExcelColumn("Expense Element")]
            public string ExpElement { get; set; }

            [ExcelColumn("Year")]
            public string Year { get; set; }

            [ExcelColumn("Month")]
            public string Month { get; set; }

            [ExcelColumn("Budget")]
            public string Budget { get; set; }

            [ExcelColumn("Actual")]
            public string Actual { get; set; }

            [ExcelColumn("Variation")]
            public string Variation { get; set; }

            [ExcelColumn("Forex")]
            public string Forex { get; set; }

            [ExcelColumn("Input Price")]
            public string InputPrice { get; set; }

            [ExcelColumn("Volume")]
            public string Volume { get; set; }

            [ExcelColumn("Sustainable")]
            public string Sustainable { get; set; }

            [ExcelColumn("Other Eff")]
            public string OtherEff { get; set; }

            [ExcelColumn("Other Over")]
            public string OtherOver { get; set; }

            [ExcelColumn("Timing")]
            public string Timing { get; set; }
        }
    }
}
