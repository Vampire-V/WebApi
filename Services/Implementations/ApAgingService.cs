using ClosedXML.Excel;
using WebApi.Data.Accounting.Entities;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class ApAgingService : IApAgingService
    {
        private readonly IAccountingUniOfWork _accountingUniOfWork;
        public ApAgingService(IAccountingUniOfWork accountingUniOfWork)
        {
            _accountingUniOfWork = accountingUniOfWork;
        }
        public async Task<List<ApAgingDetail>> GetApAgingDetailFilterAsync(DateTime checkDate, DateTime checkRate)
        {
            List<ApAgingDetail> apAgingDetail = await _accountingUniOfWork.ApAgingRepository.GetApAgingDetailFilterAsync(checkDate, checkRate);
            return apAgingDetail;
        }
        public async Task<List<ApAgingPackage>> GetApAgingPackageFilterAsync(DateTime checkDate, DateTime checkRate)
        {
            List<ApAgingPackage> apAgingPackages = await _accountingUniOfWork.ApAgingRepository.GetApAgingPackageFilterAsync(checkDate, checkRate);
            return apAgingPackages;
        }
        public async Task<List<ApAgingPbc>> GetApAgingPbcFilterAsync(DateTime checkDate, DateTime checkRate, string checkPbc)
        {
            List<ApAgingPbc> apAgingPbc = await _accountingUniOfWork.ApAgingRepository.GetApAgingPbcFilterAsync(checkDate, checkRate, checkPbc);
            return apAgingPbc;
        }
        public async Task<byte[]> GetApAgingReportForExcel(DateTime checkDate, DateTime checkRate)
        {
            var workbook = new XLWorkbook();
            // Package
            var wsPackage = workbook.Worksheets.Add("AP Aging (Package)").SetTabColor(XLColor.FromHtml("#A9D08E"));
            var packages = await _accountingUniOfWork.ApAgingRepository.GetApAgingPackageFilterAsync(checkDate, checkRate);
            var rowPackage = 5;
            wsPackage.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
            wsPackage.Cell(1, 2).SetValue("Company :");
            wsPackage.Cell(1, 3).SetValue("Haier Electric (Thailand) Public Company Limited");
            wsPackage.Cell(2, 2).SetValue($"Aging AP as of {checkDate.ToString("MMMM yyyy")}");
            wsPackage.Range("B2:C2").Merge();
            wsPackage.Range("B1:C2").Style.Fill.BackgroundColor = XLColor.FromHtml("#0070C0");
            wsPackage.Range("B1:C2").Style.Font.FontColor = XLColor.White;
            // Header
            wsPackage.Cell("B4").SetValue("Customer ID");
            wsPackage.Range("B4:B5").Merge();
            wsPackage.Cell("C4").SetValue("Customer Name");
            wsPackage.Range("C4:C5").Merge();
            wsPackage.Cell("D4").SetValue("Balance");
            wsPackage.Range("D4:D5").Merge();
            wsPackage.Cell("E4").SetValue("Business description");
            wsPackage.Range("E4:E5").Merge();
            wsPackage.Cell("F4").SetValue("Aging");
            wsPackage.Range("F4:J4").Merge();
            wsPackage.Cell("F5").SetValue("<1M");
            wsPackage.Cell("G5").SetValue("1-6M");
            wsPackage.Cell("H5").SetValue("6M-1Y");
            wsPackage.Cell("I5").SetValue("1-2Y");
            wsPackage.Cell("J5").SetValue(">2Y");
            wsPackage.Cell("K4").SetValue("Comments");
            wsPackage.Range("K4:K5").Merge();
            wsPackage.Cell("L4").SetValue("Remark");
            wsPackage.Range("L4:L5").Merge();
            wsPackage.Columns("B").Width = 12;
            wsPackage.Columns("C").Width = 60;
            wsPackage.Columns("D").Width = 18;
            wsPackage.Columns("E").Width = 18;
            wsPackage.Columns("F").Width = 18;
            wsPackage.Columns("G").Width = 18;
            wsPackage.Columns("H").Width = 18;
            wsPackage.Columns("I").Width = 18;
            wsPackage.Columns("J").Width = 18;
            wsPackage.Columns("K").Width = 12;
            wsPackage.Columns("L").Width = 12;
            wsPackage.Range("B4:L5").Style.Fill.BackgroundColor = XLColor.FromHtml("#FFC000");
            wsPackage.Range("F5:J5").Style.Fill.BackgroundColor = XLColor.FromHtml("#FCD5B4");
            wsPackage.Range("B4:L5").Style.Font.SetBold();
            wsPackage.Range("B4:L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPackage.Range("B4:L5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            // Style Border Header
            wsPackage.Range("B4:L6").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            wsPackage.Range("B4:L6").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            wsPackage.Range("B4:L6").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            // Detail
            foreach (var value in packages)
            {
                rowPackage++;
                wsPackage.Cell(rowPackage, 2).SetValue(value.VendorCode);
                wsPackage.Cell(rowPackage, 3).SetValue(value.VendorName);
                wsPackage.Cell(rowPackage, 4).SetValue(value.TotalBalance);
                wsPackage.Cell(rowPackage, 5).SetValue(value.BusinessDescription);
                wsPackage.Cell(rowPackage, 6).SetValue(value.Range1);
                wsPackage.Cell(rowPackage, 7).SetValue(value.Range2);
                wsPackage.Cell(rowPackage, 8).SetValue(value.Range3);
                wsPackage.Cell(rowPackage, 9).SetValue(value.Range4);
                wsPackage.Cell(rowPackage, 10).SetValue(value.Range5);
            }
            wsPackage.Cell(rowPackage + 1, 2).SetValue("Grand Total").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPackage.Range($"B{rowPackage + 1}:C{rowPackage + 1}").Merge();
            if (rowPackage >= 6)
            {
                wsPackage.Cell(rowPackage + 1, 4).FormulaA1 = $"SUM(D6:D{rowPackage})";
                wsPackage.Cell(rowPackage + 1, 6).FormulaA1 = $"SUM(F6:F{rowPackage})";
                wsPackage.Cell(rowPackage + 1, 7).FormulaA1 = $"SUM(G6:G{rowPackage})";
                wsPackage.Cell(rowPackage + 1, 8).FormulaA1 = $"SUM(H6:H{rowPackage})";
                wsPackage.Cell(rowPackage + 1, 9).FormulaA1 = $"SUM(I6:I{rowPackage})";
                wsPackage.Cell(rowPackage + 1, 10).FormulaA1 = $"SUM(J6:J{rowPackage})";
            }
            // Style Border Detail
            wsPackage.Range($"B6:L{rowPackage + 1}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            wsPackage.Range($"B6:L{rowPackage + 1}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            wsPackage.Range($"B7:L{rowPackage + 1}").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
            wsPackage.Range($"B6:L{rowPackage + 1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wsPackage.Range($"B{rowPackage + 1}:L{rowPackage + 1}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            wsPackage.Range($"B{rowPackage + 2}:L{rowPackage + 2}").Style.Border.TopBorder = XLBorderStyleValues.Double;
            wsPackage.Range($"B{rowPackage + 1}:L{rowPackage + 1}").Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050");
            wsPackage.Range($"B{rowPackage + 1}:L{rowPackage + 1}").Style.Font.SetBold();
            // Number Format
            wsPackage.Columns("D").Style.NumberFormat.Format = "#,##0.00";
            wsPackage.Columns("D").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPackage.Range($"F6:J{rowPackage + 1}").Style.NumberFormat.Format = "#,##0.00";
            wsPackage.Range($"F6:J{rowPackage + 1}").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPackage.SheetView.ZoomScale = 80;
            // wsPackage.Columns("B:J").AdjustToContents();
            wsPackage.SheetView.FreezeRows(5);

            // PBC72
            var wsPbc72 = workbook.Worksheets.Add("PBC72").SetTabColor(XLColor.FromHtml("#002060"));
            var pbc72 = await _accountingUniOfWork.ApAgingRepository.GetApAgingPbcFilterAsync(checkDate, checkRate, "72");
            var rowPbc72 = 4;
            wsPbc72.SheetView.FreezeRows(4);
            wsPbc72.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
            wsPbc72.Cell(1, 2).SetValue("Company :");
            wsPbc72.Cell(1, 3).SetValue("Haier Electric (Thailand) Public Company Limited");
            wsPbc72.Cell(2, 2).SetValue($"Aging AP as of {checkDate.ToString("MMMM yyyy")}");
            wsPbc72.Range("B2:C2").Merge();
            wsPbc72.Range("B1:C2").Style.Fill.BackgroundColor = XLColor.FromHtml("#0070C0");
            wsPbc72.Range("B1:C2").Style.Font.FontColor = XLColor.White;
            // Header
            wsPbc72.Row(4).Height = 30;
            wsPbc72.Cell("B4").SetValue("Customer ID");
            wsPbc72.Cell("C4").SetValue("Customer Name");
            wsPbc72.Cell("D4").SetValue("PBC");
            wsPbc72.Cell("E4").SetValue("Amount in doc.");
            wsPbc72.Cell("F4").SetValue("Curr.");
            wsPbc72.Cell("G4").SetValue("Amount in local");
            wsPbc72.Cell("H4").SetValue("LCurr.");
            wsPbc72.Cell("I4").SetValue("INDUE");
            wsPbc72.Cell("J4").SetValue("Within 1 months");
            wsPbc72.Cell("K4").SetValue("1-2 months");
            wsPbc72.Cell("L4").SetValue("2-3 months");
            wsPbc72.Cell("M4").SetValue("3-6 months");
            wsPbc72.Cell("N4").SetValue("6-12 months");
            wsPbc72.Cell("O4").SetValue("12-24 months");
            wsPbc72.Cell("P4").SetValue("24-36 months");
            wsPbc72.Cell("Q4").SetValue("Over 36 months");
            wsPbc72.Cell("R4").SetValue("Grand Total");
            wsPbc72.Cell("S4").SetValue("check diff");
            wsPbc72.Cell("T4").SetValue("rate");
            wsPbc72.Columns("B").Width = 12;
            wsPbc72.Columns("C").Width = 60;
            wsPbc72.Columns("D").Width = 8;
            wsPbc72.Columns("E").Width = 16;
            wsPbc72.Columns("F").Width = 5;
            wsPbc72.Columns("G").Width = 16;
            wsPbc72.Columns("H").Width = 5;
            wsPbc72.Columns("I").Width = 15.5;
            wsPbc72.Columns("J").Width = 15.5;
            wsPbc72.Columns("K").Width = 15.5;
            wsPbc72.Columns("L").Width = 15.5;
            wsPbc72.Columns("M").Width = 15.5;
            wsPbc72.Columns("N").Width = 15.5;
            wsPbc72.Columns("O").Width = 15.5;
            wsPbc72.Columns("P").Width = 15.5;
            wsPbc72.Columns("Q").Width = 15.5;
            wsPbc72.Columns("R").Width = 15.5;
            wsPbc72.Columns("S").Width = 12;
            wsPbc72.Columns("T").Width = 8;
            wsPbc72.Range("B4:H4").Style.Fill.BackgroundColor = XLColor.FromHtml("#A9D08E");
            wsPbc72.Range("I4:I4").Style.Fill.BackgroundColor = XLColor.FromHtml("#BDD7EE");
            wsPbc72.Range("J4:T4").Style.Fill.BackgroundColor = XLColor.FromHtml("#F8CBAD");
            wsPbc72.Range("B4:T4").Style.Font.SetBold();
            wsPbc72.Range("B4:T4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPbc72.Range("B4:T4").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            // Style Border Header
            wsPbc72.Range("B4:T5").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            wsPbc72.Range("B4:T5").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            wsPbc72.Range("B4:T5").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            foreach (var value in pbc72)
            {
                rowPbc72++;
                wsPbc72.Cell(rowPbc72, 2).SetValue(value.VendorCode);
                wsPbc72.Cell(rowPbc72, 3).SetValue(value.VendorName);
                wsPbc72.Cell(rowPbc72, 4).SetValue(value.BusinessDescription);
                wsPbc72.Cell(rowPbc72, 5).SetValue(value.AmountDc);
                wsPbc72.Cell(rowPbc72, 6).SetValue(value.DocumentCurrency);
                wsPbc72.Cell(rowPbc72, 7).SetValue(value.AmountLc);
                wsPbc72.Cell(rowPbc72, 8).SetValue(value.LocalCurrency);
                wsPbc72.Cell(rowPbc72, 9).SetValue(value.Range1);
                wsPbc72.Cell(rowPbc72, 10).SetValue(value.Range2);
                wsPbc72.Cell(rowPbc72, 11).SetValue(value.Range3);
                wsPbc72.Cell(rowPbc72, 12).SetValue(value.Range4);
                wsPbc72.Cell(rowPbc72, 13).SetValue(value.Range5);
                wsPbc72.Cell(rowPbc72, 14).SetValue(value.Range6);
                wsPbc72.Cell(rowPbc72, 15).SetValue(value.Range7);
                wsPbc72.Cell(rowPbc72, 16).SetValue(value.Range8);
                wsPbc72.Cell(rowPbc72, 17).SetValue(value.Range9);
                wsPbc72.Cell(rowPbc72, 18).FormulaA1 = $"SUM(I{rowPbc72}:Q{rowPbc72})";
                wsPbc72.Cell(rowPbc72, 19).FormulaA1 = $"+G{rowPbc72}-R{rowPbc72}";
                wsPbc72.Cell(rowPbc72, 20).FormulaA1 = $"+G{rowPbc72}/E{rowPbc72}";
            }
            wsPbc72.Cell(rowPbc72 + 1, 2).SetValue("Grand Total").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPbc72.Range($"B{rowPbc72 + 1}:D{rowPbc72 + 1}").Merge();
            if (rowPbc72 >= 5)
            {
                wsPbc72.Cell(rowPbc72 + 1, 5).FormulaA1 = $"SUM(E5:E{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 7).FormulaA1 = $"SUM(G5:G{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 9).FormulaA1 = $"SUM(I5:I{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 10).FormulaA1 = $"SUM(J5:J{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 11).FormulaA1 = $"SUM(K5:K{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 12).FormulaA1 = $"SUM(L5:L{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 13).FormulaA1 = $"SUM(M5:M{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 14).FormulaA1 = $"SUM(N5:N{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 15).FormulaA1 = $"SUM(O5:O{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 16).FormulaA1 = $"SUM(P5:P{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 17).FormulaA1 = $"SUM(Q5:Q{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 18).FormulaA1 = $"SUM(R5:R{rowPbc72})";
                wsPbc72.Cell(rowPbc72 + 1, 19).FormulaA1 = $"SUM(S5:S{rowPbc72})";
            }
            // Style Border Detail
            wsPbc72.Range($"B5:T{rowPbc72 + 1}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            wsPbc72.Range($"B5:T{rowPbc72 + 1}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            wsPbc72.Range($"B6:T{rowPbc72 + 1}").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
            wsPbc72.Range($"B5:T{rowPbc72 + 1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wsPbc72.Range($"B{rowPbc72 + 1}:T{rowPbc72 + 1}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            wsPbc72.Range($"B{rowPbc72 + 2}:T{rowPbc72 + 2}").Style.Border.TopBorder = XLBorderStyleValues.Double;
            wsPbc72.Range($"B{rowPbc72 + 1}:T{rowPbc72 + 1}").Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050");
            wsPbc72.Range($"B{rowPbc72 + 1}:T{rowPbc72 + 1}").Style.Font.SetBold();
            // Number Format
            wsPbc72.Columns("E").Style.NumberFormat.Format = "#,##0.00";
            wsPbc72.Columns("E").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPbc72.Columns("G").Style.NumberFormat.Format = "#,##0.00";
            wsPbc72.Columns("G").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPbc72.Range($"I5:S{rowPbc72 + 1}").Style.NumberFormat.Format = "#,##0.00";
            wsPbc72.Range($"I5:S{rowPbc72 + 1}").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPbc72.Columns("T").Style.NumberFormat.Format = "#,##0.0000";
            wsPbc72.Columns("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPbc72.SheetView.ZoomScale = 80;
            // wsPbc72.Columns("B:Q").AdjustToContents();

            // PBC74
            var wsPbc74 = workbook.Worksheets.Add("PBC74").SetTabColor(XLColor.FromHtml("#002060"));
            var pbc74 = await _accountingUniOfWork.ApAgingRepository.GetApAgingPbcFilterAsync(checkDate, checkRate, "74");
            var rowPbc74 = 4;
            wsPbc74.SheetView.FreezeRows(4);
            wsPbc74.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
            wsPbc74.Cell(1, 2).SetValue("Company :");
            wsPbc74.Cell(1, 3).SetValue("Haier Electric (Thailand) Public Company Limited");
            wsPbc74.Cell(2, 2).SetValue($"Aging AP as of {checkDate.ToString("MMMM yyyy")}");
            wsPbc74.Range("B2:C2").Merge();
            wsPbc74.Range("B1:C2").Style.Fill.BackgroundColor = XLColor.FromHtml("#0070C0");
            wsPbc74.Range("B1:C2").Style.Font.FontColor = XLColor.White;
            // Header
            wsPbc74.Row(4).Height = 30;
            wsPbc74.Cell("B4").SetValue("Customer ID");
            wsPbc74.Cell("C4").SetValue("Customer Name");
            wsPbc74.Cell("D4").SetValue("PBC");
            wsPbc74.Cell("E4").SetValue("Amount in doc.");
            wsPbc74.Cell("F4").SetValue("Curr.");
            wsPbc74.Cell("G4").SetValue("Amount in local");
            wsPbc74.Cell("H4").SetValue("LCurr.");
            wsPbc74.Cell("I4").SetValue("INDUE");
            wsPbc74.Cell("J4").SetValue("Within 1 months");
            wsPbc74.Cell("K4").SetValue("1-2 months");
            wsPbc74.Cell("L4").SetValue("2-3 months");
            wsPbc74.Cell("M4").SetValue("3-6 months");
            wsPbc74.Cell("N4").SetValue("6-12 months");
            wsPbc74.Cell("O4").SetValue("12-24 months");
            wsPbc74.Cell("P4").SetValue("24-36 months");
            wsPbc74.Cell("Q4").SetValue("Over 36 months");
            wsPbc74.Cell("R4").SetValue("Grand Total");
            wsPbc74.Cell("S4").SetValue("check diff");
            wsPbc74.Cell("T4").SetValue("rate");
            wsPbc74.Columns("B").Width = 12;
            wsPbc74.Columns("C").Width = 60;
            wsPbc74.Columns("D").Width = 8;
            wsPbc74.Columns("E").Width = 16;
            wsPbc74.Columns("F").Width = 5;
            wsPbc74.Columns("G").Width = 16;
            wsPbc74.Columns("H").Width = 5;
            wsPbc74.Columns("I").Width = 15.5;
            wsPbc74.Columns("J").Width = 15.5;
            wsPbc74.Columns("K").Width = 15.5;
            wsPbc74.Columns("L").Width = 15.5;
            wsPbc74.Columns("M").Width = 15.5;
            wsPbc74.Columns("N").Width = 15.5;
            wsPbc74.Columns("O").Width = 15.5;
            wsPbc74.Columns("P").Width = 15.5;
            wsPbc74.Columns("Q").Width = 15.5;
            wsPbc74.Columns("R").Width = 15.5;
            wsPbc74.Columns("S").Width = 12;
            wsPbc74.Columns("T").Width = 8;

            wsPbc74.Range("B4:H4").Style.Fill.BackgroundColor = XLColor.FromHtml("#A9D08E");
            wsPbc74.Range("I4:I4").Style.Fill.BackgroundColor = XLColor.FromHtml("#BDD7EE");
            wsPbc74.Range("J4:T4").Style.Fill.BackgroundColor = XLColor.FromHtml("#F8CBAD");
            wsPbc74.Range("B4:T4").Style.Font.SetBold();
            wsPbc74.Range("B4:T4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPbc74.Range("B4:T4").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            // Style Border Header
            wsPbc74.Range("B4:T5").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            wsPbc74.Range("B4:T5").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            wsPbc74.Range("B4:T5").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            foreach (var value in pbc74)
            {
                rowPbc74++;
                wsPbc74.Cell(rowPbc74, 2).SetValue(value.VendorCode);
                wsPbc74.Cell(rowPbc74, 3).SetValue(value.VendorName);
                wsPbc74.Cell(rowPbc74, 4).SetValue(value.BusinessDescription);
                wsPbc74.Cell(rowPbc74, 5).SetValue(value.AmountDc);
                wsPbc74.Cell(rowPbc74, 6).SetValue(value.DocumentCurrency);
                wsPbc74.Cell(rowPbc74, 7).SetValue(value.AmountLc);
                wsPbc74.Cell(rowPbc74, 8).SetValue(value.LocalCurrency);
                wsPbc74.Cell(rowPbc74, 9).SetValue(value.Range1);
                wsPbc74.Cell(rowPbc74, 10).SetValue(value.Range2);
                wsPbc74.Cell(rowPbc74, 11).SetValue(value.Range3);
                wsPbc74.Cell(rowPbc74, 12).SetValue(value.Range4);
                wsPbc74.Cell(rowPbc74, 13).SetValue(value.Range5);
                wsPbc74.Cell(rowPbc74, 14).SetValue(value.Range6);
                wsPbc74.Cell(rowPbc74, 15).SetValue(value.Range7);
                wsPbc74.Cell(rowPbc74, 16).SetValue(value.Range8);
                wsPbc74.Cell(rowPbc74, 17).SetValue(value.Range9);
                wsPbc74.Cell(rowPbc74, 18).FormulaA1 = $"SUM(I{rowPbc74}:Q{rowPbc74})";
                wsPbc74.Cell(rowPbc74, 19).FormulaA1 = $"+G{rowPbc74}-R{rowPbc74}";
                wsPbc74.Cell(rowPbc74, 20).FormulaA1 = $"+G{rowPbc74}/E{rowPbc74}";
            }
            wsPbc74.Cell(rowPbc74 + 1, 2).SetValue("Grand Total").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPbc74.Range($"B{rowPbc74 + 1}:D{rowPbc74 + 1}").Merge();
            if (rowPbc74 >= 5)
            {
                wsPbc74.Cell(rowPbc74 + 1, 5).FormulaA1 = $"SUM(E5:E{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 7).FormulaA1 = $"SUM(G5:G{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 9).FormulaA1 = $"SUM(I5:I{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 10).FormulaA1 = $"SUM(J5:J{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 11).FormulaA1 = $"SUM(K5:K{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 12).FormulaA1 = $"SUM(L5:L{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 13).FormulaA1 = $"SUM(M5:M{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 14).FormulaA1 = $"SUM(N5:N{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 15).FormulaA1 = $"SUM(O5:O{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 16).FormulaA1 = $"SUM(P5:P{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 17).FormulaA1 = $"SUM(Q5:Q{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 18).FormulaA1 = $"SUM(R5:R{rowPbc74})";
                wsPbc74.Cell(rowPbc74 + 1, 19).FormulaA1 = $"SUM(S5:S{rowPbc74})";
            }
            // Style Border Detail
            wsPbc74.Range($"B5:T{rowPbc74 + 1}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            wsPbc74.Range($"B5:T{rowPbc74 + 1}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            wsPbc74.Range($"B6:T{rowPbc74 + 1}").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
            wsPbc74.Range($"B5:T{rowPbc74 + 1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wsPbc74.Range($"B{rowPbc74 + 1}:T{rowPbc74 + 1}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            wsPbc74.Range($"B{rowPbc74 + 2}:T{rowPbc74 + 2}").Style.Border.TopBorder = XLBorderStyleValues.Double;
            wsPbc74.Range($"B{rowPbc74 + 1}:T{rowPbc74 + 1}").Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050");
            wsPbc74.Range($"B{rowPbc74 + 1}:T{rowPbc74 + 1}").Style.Font.SetBold();
            // Number Format
            wsPbc74.Columns("E").Style.NumberFormat.Format = "#,##0.00";
            wsPbc74.Columns("E").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPbc74.Columns("G").Style.NumberFormat.Format = "#,##0.00";
            wsPbc74.Columns("G").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPbc74.Range($"I5:S{rowPbc74 + 1}").Style.NumberFormat.Format = "#,##0.00";
            wsPbc74.Range($"I5:S{rowPbc74 + 1}").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
            wsPbc74.Columns("T").Style.NumberFormat.Format = "#,##0.0000";
            wsPbc74.Columns("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsPbc74.SheetView.ZoomScale = 80;
            // wsPbc74.Columns("B:Q").AdjustToContents();

            // Detail
            var wsDetail = workbook.Worksheets.Add("Detail Balance AP Aging").SetTabColor(XLColor.FromHtml("#2F75B5"));
            var detail = await _accountingUniOfWork.ApAgingRepository.GetApAgingDetailFilterAsync(checkDate, checkRate);
            var rowDetail = 2;
            wsDetail.SheetView.FreezeRows(2);
            // Header
            wsDetail.Row(2).Height = 30;
            wsDetail.Cell(2, 2).SetValue("G/L Acct");
            wsDetail.Cell(2, 3).SetValue("G/L");
            wsDetail.Cell(2, 4).SetValue("G/L Name");
            wsDetail.Cell(2, 5).SetValue("Year");
            wsDetail.Cell(2, 6).SetValue("Period");
            wsDetail.Cell(2, 7).SetValue("Vendor Code");
            wsDetail.Cell(2, 8).SetValue("Vendor Name");
            wsDetail.Cell(2, 9).SetValue("Special G/L");
            wsDetail.Cell(2, 10).SetValue("Document Header Text");
            wsDetail.Cell(2, 11).SetValue("Document Type");
            wsDetail.Cell(2, 12).SetValue("Reference");
            wsDetail.Cell(2, 13).SetValue("Document No");
            wsDetail.Cell(2, 14).SetValue("Document Date");
            wsDetail.Cell(2, 15).SetValue("Pay Terms");
            wsDetail.Cell(2, 16).SetValue("Day 1");
            wsDetail.Cell(2, 17).SetValue("Posting Date");
            wsDetail.Cell(2, 18).SetValue("Net Due Date");
            wsDetail.Cell(2, 19).SetValue("Month Due");
            wsDetail.Cell(2, 20).SetValue("PBC Type");
            wsDetail.Cell(2, 21).SetValue("PBC");
            wsDetail.Cell(2, 22).SetValue("Package Type");
            wsDetail.Cell(2, 23).SetValue("Exchange Rate");
            wsDetail.Cell(2, 24).SetValue("Amount in doc.");
            wsDetail.Cell(2, 25).SetValue("Curr.");
            wsDetail.Cell(2, 26).SetValue("Rate Bot");
            wsDetail.Cell(2, 27).SetValue("Amount in local");
            wsDetail.Cell(2, 28).SetValue("Amount After Adj. Rate");
            wsDetail.Cell(2, 29).SetValue("Amount Adj. Rate");
            wsDetail.Cell(2, 30).SetValue("LCurr.");
            wsDetail.Cell(2, 31).SetValue("Text");
            wsDetail.Cell(2, 32).SetValue("Assignment");
            wsDetail.Cell(2, 33).SetValue("Clearing Document");
            wsDetail.Cell(2, 34).SetValue("Clearing Date");
            wsDetail.Cell(2, 35).SetValue("User Name");
            wsDetail.Cell(2, 36).SetValue("Account Type");
            wsDetail.Cell(2, 37).SetValue("Debit / Credit");
            wsDetail.Cell(2, 38).SetValue("Vat");
            wsDetail.Cell(2, 39).SetValue("Special G/L Assignment");
            wsDetail.Range("B2:AM2").Style.Fill.BackgroundColor = XLColor.FromHtml("#A9D08E");
            wsDetail.Range("B2:AM2").Style.Font.SetBold();
            wsDetail.Range("B2:AM2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range("B2:AM2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            // Style Border Header
            wsDetail.Range("B2:AM2").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            wsDetail.Range("B2:AM2").Style.Border.RightBorder = XLBorderStyleValues.Thin;
            wsDetail.Range("B2:AM2").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            foreach (var value in detail)
            {
                rowDetail++;
                wsDetail.Cell(rowDetail, 2).SetValue(value.GlAcct);
                wsDetail.Cell(rowDetail, 3).SetValue(value.Gl);
                wsDetail.Cell(rowDetail, 4).SetValue(value.GlName);
                wsDetail.Cell(rowDetail, 5).SetValue(value.Year);
                wsDetail.Cell(rowDetail, 6).SetValue(value.Period);
                wsDetail.Cell(rowDetail, 7).SetValue(value.VendorCode);
                wsDetail.Cell(rowDetail, 8).SetValue(value.VendorName);
                wsDetail.Cell(rowDetail, 9).SetValue(value.SpecialGl);
                wsDetail.Cell(rowDetail, 10).SetValue(value.DocumentHeaderText);
                wsDetail.Cell(rowDetail, 11).SetValue(value.DocumentType);
                wsDetail.Cell(rowDetail, 12).SetValue(value.Reference);
                wsDetail.Cell(rowDetail, 13).SetValue(value.DocumentNo);
                wsDetail.Cell(rowDetail, 14).SetValue(value.DocumentDate);
                wsDetail.Cell(rowDetail, 15).SetValue(value.PayTerms);
                wsDetail.Cell(rowDetail, 16).SetValue(value.DayOne);
                wsDetail.Cell(rowDetail, 17).SetValue(value.PostingDate);
                wsDetail.Cell(rowDetail, 18).SetValue(value.NetDueDate);
                wsDetail.Cell(rowDetail, 19).SetValue(value.MonthDue);
                wsDetail.Cell(rowDetail, 20).SetValue(value.PbcType);
                wsDetail.Cell(rowDetail, 21).SetValue(value.Pbc);
                wsDetail.Cell(rowDetail, 22).SetValue(value.PackageType);
                wsDetail.Cell(rowDetail, 23).SetValue(value.ExchangeRate);
                wsDetail.Cell(rowDetail, 24).SetValue(value.AmountDc);
                wsDetail.Cell(rowDetail, 25).SetValue(value.DocumentCurrency);
                wsDetail.Cell(rowDetail, 26).SetValue(value.RateBot);
                wsDetail.Cell(rowDetail, 27).SetValue(value.AmountLc);
                wsDetail.Cell(rowDetail, 28).SetValue(value.AmountAfterAdjRate);
                wsDetail.Cell(rowDetail, 29).SetValue(value.AmountAdjRate);
                wsDetail.Cell(rowDetail, 30).SetValue(value.LocalCurrency);
                wsDetail.Cell(rowDetail, 31).SetValue(value.Text);
                wsDetail.Cell(rowDetail, 32).SetValue(value.Assignment);
                wsDetail.Cell(rowDetail, 33).SetValue(value.ClearingDocument);
                wsDetail.Cell(rowDetail, 34).SetValue(value.ClearingDate);
                wsDetail.Cell(rowDetail, 35).SetValue(value.UserName);
                wsDetail.Cell(rowDetail, 36).SetValue(value.AccountType);
                wsDetail.Cell(rowDetail, 37).SetValue(value.DebitCredit);
                wsDetail.Cell(rowDetail, 38).SetValue(value.Vat);
                wsDetail.Cell(rowDetail, 39).SetValue(value.SpecialGlAssignment);
            }
            wsDetail.Columns("S").Style.NumberFormat.Format = "#,##0.00";
            wsDetail.Columns("X").Style.NumberFormat.Format = "#,##0.00";
            wsDetail.Columns("Z").Style.NumberFormat.Format = "#,##0.0000";
            wsDetail.Columns("AA").Style.NumberFormat.Format = "#,##0.00";
            wsDetail.Columns("AB").Style.NumberFormat.Format = "#,##0.00";
            wsDetail.Columns("AC").Style.NumberFormat.Format = "#,##0.00";
            wsDetail.Range($"B3:C{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"E3:G{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"I3:I{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"K3:K{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"M3:R{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"V3:W{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"Y3:Y{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"AD3:AD{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.Range($"AG3:AM{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsDetail.SheetView.ZoomScale = 80;
            wsDetail.Columns("B:AM").AdjustToContents();

            wsPackage.Columns("A").Width = 2;
            wsPbc72.Columns("A").Width = 2;
            wsPbc74.Columns("A").Width = 2;
            wsDetail.Columns("A").Width = 2;
            // Save file
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
    }
}