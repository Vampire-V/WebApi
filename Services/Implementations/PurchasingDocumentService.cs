using AutoMapper;
using ClosedXML.Excel;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.GRIR;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class PurchasingDocumentService : IPurchasingDocumentService
    {
        public readonly IMapper _mapper;
        private readonly IAccountingUniOfWork _accountingUniOfWork;
        public PurchasingDocumentService(IAccountingUniOfWork accountingUniOfWork, IMapper mapper)
        {
            _accountingUniOfWork = accountingUniOfWork;
            _mapper = mapper;
        }

        public async Task<List<GrIrDetailView>> GetGrIrDetailViewFilterAsync(GrIrReportParameter GrIrReportParameter)
        {
            List<GrIrDetailView> GrIrDetailViews = await _accountingUniOfWork.GrIrReportRepository.GetGrIrDetailViewFilterAsync(GrIrReportParameter);
            return GrIrDetailViews;
        }

        public async Task<List<GrIrPlantView>> GetGrIrPlantViewFilterAsync(GrIrReportParameter GrIrReportParameter)
        {
            List<GrIrPlantView> GrIrPlantViews = await _accountingUniOfWork.GrIrReportRepository.GetGrIrPlantViewFilterAsync(GrIrReportParameter);
            return GrIrPlantViews;
        }

        public async Task<List<GrIrSummaryView>> GetGrIrSummaryViewFilterAsync(GrIrReportParameter GrIrReportParameter)
        {
            List<GrIrSummaryView> GrIrSummaryViews = await _accountingUniOfWork.GrIrReportRepository.GetGrIrSummaryViewFilterAsync(GrIrReportParameter);
            return GrIrSummaryViews;
        }

        public async Task<byte[]> GetGrIrReportForExcel(GrIrReportParameter GrIrReportParameter)
        {
            var workbook = new XLWorkbook();

            var wsSummary = workbook.Worksheets.Add("Summary").SetTabColor(XLColor.FromHtml("#002060"));
            if (wsSummary != null)
            {
                var rowSum = 4;
                wsSummary.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
                wsSummary.Cell(2, 2).SetValue("Aging GR/IR report");
                wsSummary.Range("B2:C2").Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00"); // Color
                wsSummary.Columns("A").Width = 2;
                wsSummary.Cell("B3").SetValue("Plant");
                wsSummary.Range("B3:B4").Merge();
                wsSummary.Columns("B").Width = 18;
                wsSummary.Cell("C3").SetValue("PO Type");
                wsSummary.Range("C3:C4").Merge();
                wsSummary.Columns("C").Width = 24;
                wsSummary.Cell("D3").SetValue("Total balance");
                wsSummary.Range("D3:D4").Merge();
                wsSummary.Columns("D").Width = 18;
                wsSummary.Cell("E3").SetValue("Outstanding  GR to IR (Day)");
                wsSummary.Range("E3:J3").Merge();
                wsSummary.Cell("E4").SetValue("1-30");
                wsSummary.Columns("E").Width = 18;
                wsSummary.Cell("F4").SetValue("31-60");
                wsSummary.Columns("F").Width = 18;
                wsSummary.Cell("G4").SetValue("61-90");
                wsSummary.Columns("G").Width = 18;
                wsSummary.Cell("H4").SetValue("91-180");
                wsSummary.Columns("H").Width = 18;
                wsSummary.Cell("I4").SetValue("181-360");
                wsSummary.Columns("I").Width = 18;
                wsSummary.Cell("J4").SetValue("361 up");
                wsSummary.Columns("J").Width = 18;
                // Style Header
                wsSummary.Range($"B{rowSum - 1}:J{rowSum}").Style.Font.SetBold();
                wsSummary.Range($"B{rowSum - 1}:J{rowSum}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSummary.Range($"B{rowSum - 1}:J{rowSum}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                wsSummary.Range($"B{rowSum - 1}:J{rowSum}").Style.Fill.BackgroundColor = XLColor.FromHtml("#31869B"); // Color
                wsSummary.Range($"B{rowSum - 1}:J{rowSum}").Style.Font.FontColor = XLColor.White;

                wsSummary.Range("B3:J4").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                wsSummary.Range("B3:J4").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                wsSummary.Range("B3:J4").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                // Data GRIR Summary
                var GrIrSummary = await _accountingUniOfWork.GrIrReportRepository.GetGrIrSummaryViewFilterAsync(GrIrReportParameter);
                foreach (var value in GrIrSummary)
                {
                    rowSum++;
                    wsSummary.Cell(rowSum, 2).SetValue(value.Plant); // Plant
                    wsSummary.Cell(rowSum, 3).SetValue(value.PurchaseTypeDesc); // PO Type
                    wsSummary.Cell(rowSum, 4).SetValue(value.TotalBalance); // Total balance
                    wsSummary.Cell(rowSum, 5).SetValue(value.Range1); // 1-30
                    wsSummary.Cell(rowSum, 6).SetValue(value.Range2); // 31-60
                    wsSummary.Cell(rowSum, 7).SetValue(value.Range3); // 61-90
                    wsSummary.Cell(rowSum, 8).SetValue(value.Range4); // 91-180
                    wsSummary.Cell(rowSum, 9).SetValue(value.Range5); // 181-360
                    wsSummary.Cell(rowSum, 10).SetValue(value.Range6); // 361 up
                }
                if (rowSum >= 5)
                {
                    // Grand Total
                    wsSummary.Cell($"B{rowSum + 1}").SetValue("Grand Total");
                    //value Summary
                    wsSummary.Cell(rowSum + 1, 4).FormulaA1 = $"SUM(D5:D{rowSum})"; // Summary Total balance
                    wsSummary.Cell(rowSum + 1, 5).FormulaA1 = $"SUM(E5:E{rowSum})"; // Summary 1-30
                    wsSummary.Cell(rowSum + 1, 6).FormulaA1 = $"SUM(F5:F{rowSum})"; // Summary 31-60 
                    wsSummary.Cell(rowSum + 1, 7).FormulaA1 = $"SUM(G5:G{rowSum})"; // Summary 61-90
                    wsSummary.Cell(rowSum + 1, 8).FormulaA1 = $"SUM(H5:H{rowSum})"; // Summary 91-180
                    wsSummary.Cell(rowSum + 1, 9).FormulaA1 = $"SUM(I5:I{rowSum})"; // Summary 181-360
                    wsSummary.Cell(rowSum + 1, 10).FormulaA1 = $"SUM(J5:J{rowSum})"; // Summary 361 up

                    wsSummary.Range($"B{rowSum + 1}:J{rowSum + 1}").Style.Font.SetBold();
                    wsSummary.Range($"B{rowSum + 1}:J{rowSum + 1}").Style.Fill.BackgroundColor = XLColor.FromHtml("#00B050"); // Color
                    wsSummary.Range($"B{rowSum + 1}:J{rowSum + 1}").Style.Font.FontColor = XLColor.White;
                }
                wsSummary.Range($"B5:J{rowSum+1}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                wsSummary.Range($"B5:J{rowSum+1}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                wsSummary.Range($"B5:J{rowSum+1}").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
                wsSummary.Range($"B5:J{rowSum+1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsSummary.Range($"B{rowSum+1}:J{rowSum+1}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                // Style
                wsSummary.Range($"B5:B{rowSum + 1}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSummary.Range($"D5:J{rowSum + 1}").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
                wsSummary.SheetView.ZoomScale = 80;
                wsSummary.SheetView.FreezeRows(4);
            }


            // GRIR Plant 9771
            var ws9771 = workbook.Worksheets.Add("9771").SetTabColor(XLColor.FromHtml("#FFFF00"));
            if (ws9771 != null)
            {
                var rowPlant = 4;
                ws9771.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
                ws9771.Cell(2, 2).SetValue("Plant").Style.Font.SetBold();
                ws9771.Cell(2, 3).SetValue("9771-RF");
                ws9771.Range("B2:C2").Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00"); // Color
                // Header GRIR Plant 9771
                ws9771.Columns("A").Width = 2;
                ws9771.Cell("B3").SetValue("PO Type"); // Purchase Type Desc
                ws9771.Range("B3:B4").Merge();
                ws9771.Columns("B").Width = 22;
                ws9771.Cell("C3").SetValue("PO.No"); // Purchasing Document
                ws9771.Range("C3:C4").Merge();
                ws9771.Columns("C").Width = 12;
                ws9771.Cell("D3").SetValue("Supplier/Supplying Plant"); // Vendor
                ws9771.Range("D3:D4").Merge();
                ws9771.Columns("D").Width = 55;
                ws9771.Cell("E3").SetValue("Total balance"); // Total balance
                ws9771.Range("E3:E4").Merge();
                ws9771.Columns("E").Width = 18;
                ws9771.Cell("F3").SetValue("Outstanding  GR to IR (Day)");
                ws9771.Range("F3:K3").Merge();
                ws9771.Cell("F4").SetValue("1-30"); // Range 1
                ws9771.Columns("F").Width = 18;
                ws9771.Cell("G4").SetValue("31-60"); // Range 2
                ws9771.Columns("G").Width = 18;
                ws9771.Cell("H4").SetValue("61-90"); // Range 3
                ws9771.Columns("H").Width = 18;
                ws9771.Cell("I4").SetValue("91-180"); // Range 4
                ws9771.Columns("I").Width = 18;
                ws9771.Cell("J4").SetValue("181-360"); // Range 5
                ws9771.Columns("J").Width = 18;
                ws9771.Cell("K4").SetValue("361 up"); // Range 6
                ws9771.Columns("K").Width = 18;
                ws9771.Cell("L3").SetValue("Note"); // Note
                ws9771.Range("L3:L4").Merge();
                ws9771.Columns("L").Width = 30;
                // Style Header
                ws9771.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Font.SetBold();
                ws9771.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws9771.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws9771.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Fill.BackgroundColor = XLColor.FromHtml("#C6E0B4"); // Color

                ws9771.Range("B3:L4").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws9771.Range("B3:L4").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws9771.Range("B3:L4").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                // Data GRIR Plant 9771
                var GrIrPlant = await _accountingUniOfWork.GrIrReportRepository.GetGrIrPlantViewFilterAsync(GrIrReportParameter);
                foreach (var value in GrIrPlant)
                {
                    if (value.Plant == "9771")
                    {
                        rowPlant++;
                        ws9771.Cell(rowPlant, 2).SetValue(value.PurchaseTypeDesc); // PO Type
                        ws9771.Cell(rowPlant, 3).SetValue(value.PurchasingDocument); // PO.No
                        ws9771.Cell(rowPlant, 4).SetValue(value.VendorCode + " " + value.Vendors?.VendorName); // Supplier/Supplying
                        ws9771.Cell(rowPlant, 5).SetValue(value.TotalBalance); // Total balance
                        ws9771.Cell(rowPlant, 6).SetValue(value.Range1); // 1-30
                        ws9771.Cell(rowPlant, 7).SetValue(value.Range2); // 31-60
                        ws9771.Cell(rowPlant, 8).SetValue(value.Range3); // 61-90
                        ws9771.Cell(rowPlant, 9).SetValue(value.Range4); // 91-180
                        ws9771.Cell(rowPlant, 10).SetValue(value.Range5); // 181-360
                        ws9771.Cell(rowPlant, 11).SetValue(value.Range6); // 361 up
                    }
                }
                // Grand Total
                ws9771.Cell($"B{rowPlant + 1}").SetValue("Grand Total").Style.Font.SetBold();
                ws9771.Cell($"B{rowPlant + 1}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws9771.Cell($"B{rowPlant + 1}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws9771.Range($"B{rowPlant + 1}:D{rowPlant + 1}").Merge();

                if (rowPlant >= 5)
                {
                    // value Summary
                    ws9771.Cell(rowPlant + 1, 5).FormulaA1 = $"SUM(E5:E{rowPlant})"; // Summary Total balance               
                    ws9771.Cell(rowPlant + 1, 6).FormulaA1 = $"SUM(F5:F{rowPlant})"; // Summary 1-30
                    ws9771.Cell(rowPlant + 1, 7).FormulaA1 = $"SUM(G5:G{rowPlant})"; // Summary 31-60
                    ws9771.Cell(rowPlant + 1, 8).FormulaA1 = $"SUM(H5:H{rowPlant})"; // Summary 61-90
                    ws9771.Cell(rowPlant + 1, 9).FormulaA1 = $"SUM(I5:I{rowPlant})"; // Summary 91-180
                    ws9771.Cell(rowPlant + 1, 10).FormulaA1 = $"SUM(J5:J{rowPlant})"; // Summary 181-360
                    ws9771.Cell(rowPlant + 1, 11).FormulaA1 = $"SUM(K5:K{rowPlant})"; // Summary 361 up
                }
                ws9771.Range($"B5:L{rowPlant+1}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws9771.Range($"B5:L{rowPlant+1}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws9771.Range($"B5:L{rowPlant+1}").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
                ws9771.Range($"B5:L{rowPlant+1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws9771.Range($"B{rowPlant+1}:L{rowPlant+1}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                // Style
                ws9771.Range($"E5:K{rowPlant + 1}").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
                ws9771.Range($"B{rowPlant + 1}:L{rowPlant + 1}").Style.Fill.BackgroundColor = XLColor.DarkGray;
                ws9771.Range($"B{rowPlant + 1}:L{rowPlant + 1}").Style.Font.SetBold();
                ws9771.SheetView.ZoomScale = 80;
                ws9771.SheetView.FreezeRows(4);
            }

            // GRIR Plant 9773
            var ws9773 = workbook.Worksheets.Add("9773").SetTabColor(XLColor.FromHtml("#FFFF00"));
            if (ws9773 != null)
            {
                var rowPlant = 4;
                ws9773.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
                // Detail Columns
                ws9773.Cell(2, 2).SetValue("Plant").Style.Font.SetBold();
                ws9773.Cell(2, 3).SetValue("9773-WAC");
                ws9773.Range("B2:C2").Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00"); // Color
                // Header GRIR Plant 9771
                ws9773.Columns("A").Width = 2;
                ws9773.Cell("B3").SetValue("PO Type"); // Purchase Type Desc
                ws9773.Range("B3:B4").Merge();
                ws9773.Columns("B").Width = 22;
                ws9773.Cell("C3").SetValue("PO.No"); // Purchasing Document
                ws9773.Range("C3:C4").Merge();
                ws9773.Columns("C").Width = 12;
                ws9773.Cell("D3").SetValue("Supplier/Supplying Plant"); // Vendor
                ws9773.Range("D3:D4").Merge();
                ws9773.Columns("D").Width = 55;
                ws9773.Cell("E3").SetValue("Total balance"); // Total balance
                ws9773.Range("E3:E4").Merge();
                ws9773.Columns("E").Width = 18;
                ws9773.Cell("F3").SetValue("Outstanding  GR to IR (Day)");
                ws9773.Range("F3:K3").Merge();
                ws9773.Cell("F4").SetValue("1-30"); // Range 1
                ws9773.Columns("F").Width = 18;
                ws9773.Cell("G4").SetValue("31-60"); // Range 2
                ws9773.Columns("G").Width = 18;
                ws9773.Cell("H4").SetValue("61-90"); // Range 3
                ws9773.Columns("H").Width = 18;
                ws9773.Cell("I4").SetValue("91-180"); // Range 4
                ws9773.Columns("I").Width = 18;
                ws9773.Cell("J4").SetValue("181-360"); // Range 5
                ws9773.Columns("J").Width = 18;
                ws9773.Cell("K4").SetValue("361 up"); // Range 6
                ws9773.Columns("K").Width = 18;
                ws9773.Cell("L3").SetValue("Note"); // Note
                ws9773.Range("L3:L4").Merge();
                ws9773.Columns("L").Width = 30;
                // Style Header
                ws9773.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Font.SetBold();
                ws9773.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws9773.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws9773.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Fill.BackgroundColor = XLColor.FromHtml("#C6E0B4"); // Color

                ws9773.Range("B3:L4").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws9773.Range("B3:L4").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws9773.Range("B3:L4").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                // Data GRIR Plant 9771
                var GrIrPlant = await _accountingUniOfWork.GrIrReportRepository.GetGrIrPlantViewFilterAsync(GrIrReportParameter);
                foreach (var value in GrIrPlant)
                {
                    if (value.Plant == "9773")
                    {
                        rowPlant++;
                        ws9773.Cell(rowPlant, 2).SetValue(value.PurchaseTypeDesc); // PO Type
                        ws9773.Cell(rowPlant, 3).SetValue(value.PurchasingDocument); // PO.No
                        ws9773.Cell(rowPlant, 4).SetValue(value.VendorCode + " " + value.Vendors?.VendorName); // Supplier/Supplying
                        ws9773.Cell(rowPlant, 5).SetValue(value.TotalBalance); // Total balance
                        ws9773.Cell(rowPlant, 6).SetValue(value.Range1); // 1-30
                        ws9773.Cell(rowPlant, 7).SetValue(value.Range2); // 31-60
                        ws9773.Cell(rowPlant, 8).SetValue(value.Range3); // 61-90
                        ws9773.Cell(rowPlant, 9).SetValue(value.Range4); // 91-180
                        ws9773.Cell(rowPlant, 10).SetValue(value.Range5); // 181-360
                        ws9773.Cell(rowPlant, 11).SetValue(value.Range6); // 361 up
                    }
                }
                // Grand Total
                ws9773.Cell($"B{rowPlant + 1}").SetValue("Grand Total").Style.Font.SetBold();
                ws9773.Cell($"B{rowPlant + 1}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws9773.Cell($"B{rowPlant + 1}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws9773.Range($"B{rowPlant + 1}:D{rowPlant + 1}").Merge();

                if (rowPlant >= 5)
                {
                    // value Summary
                    ws9773.Cell(rowPlant + 1, 5).FormulaA1 = $"SUM(E5:E{rowPlant})"; // Summary Total balance
                    ws9773.Cell(rowPlant + 1, 6).FormulaA1 = $"SUM(F5:F{rowPlant})"; // Summary 1-30
                    ws9773.Cell(rowPlant + 1, 7).FormulaA1 = $"SUM(G5:G{rowPlant})"; // Summary 31-60
                    ws9773.Cell(rowPlant + 1, 8).FormulaA1 = $"SUM(H5:H{rowPlant})"; // Summary 61-90
                    ws9773.Cell(rowPlant + 1, 9).FormulaA1 = $"SUM(I5:I{rowPlant})"; // Summary 91-180
                    ws9773.Cell(rowPlant + 1, 10).FormulaA1 = $"SUM(J5:J{rowPlant})"; // Summary 181-360
                    ws9773.Cell(rowPlant + 1, 11).FormulaA1 = $"SUM(K5:K{rowPlant})";// Summary 361 up
                }
                ws9773.Range($"B5:L{rowPlant+1}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws9773.Range($"B5:L{rowPlant+1}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws9773.Range($"B5:L{rowPlant+1}").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
                ws9773.Range($"B5:L{rowPlant+1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws9773.Range($"B{rowPlant+1}:L{rowPlant+1}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                // Style
                ws9773.Range($"E5:K{rowPlant + 1}").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
                ws9773.Range($"B{rowPlant + 1}:L{rowPlant + 1}").Style.Fill.BackgroundColor = XLColor.DarkGray;
                ws9773.Range($"B{rowPlant + 1}:L{rowPlant + 1}").Style.Font.SetBold();
                ws9773.SheetView.ZoomScale = 80;
                ws9773.SheetView.FreezeRows(4);
            }

            // GRIR Plant 9774
            var ws9774 = workbook.Worksheets.Add("9774").SetTabColor(XLColor.FromHtml("#FFFF00"));
            if (ws9774 != null)
            {
                var rowPlant = 4;
                ws9774.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
                // Detail Columns
                ws9774.Cell(2, 2).SetValue("Plant").Style.Font.SetBold();
                ws9774.Cell(2, 3).SetValue("9774-SAC");
                ws9774.Range("B2:C2").Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00"); // Color
                // Header GRIR Plant 9771
                ws9774.Columns("A").Width = 2;
                ws9774.Cell("B3").SetValue("PO Type"); // Purchase Type Desc
                ws9774.Range("B3:B4").Merge();
                ws9774.Columns("B").Width = 22;
                ws9774.Cell("C3").SetValue("PO.No"); // Purchasing Document
                ws9774.Range("C3:C4").Merge();
                ws9774.Columns("C").Width = 12;
                ws9774.Cell("D3").SetValue("Supplier/Supplying Plant"); // Vendor
                ws9774.Range("D3:D4").Merge();
                ws9774.Columns("D").Width = 55;
                ws9774.Cell("E3").SetValue("Total balance"); // Total balance
                ws9774.Range("E3:E4").Merge();
                ws9774.Columns("E").Width = 18;
                ws9774.Cell("F3").SetValue("Outstanding  GR to IR (Day)");
                ws9774.Range("F3:K3").Merge();
                ws9774.Cell("F4").SetValue("1-30"); // Range 1
                ws9774.Columns("F").Width = 18;
                ws9774.Cell("G4").SetValue("31-60"); // Range 2
                ws9774.Columns("G").Width = 18;
                ws9774.Cell("H4").SetValue("61-90"); // Range 3
                ws9774.Columns("H").Width = 18;
                ws9774.Cell("I4").SetValue("91-180"); // Range 4
                ws9774.Columns("I").Width = 18;
                ws9774.Cell("J4").SetValue("181-360"); // Range 5
                ws9774.Columns("J").Width = 18;
                ws9774.Cell("K4").SetValue("361 up"); // Range 6
                ws9774.Columns("K").Width = 18;
                ws9774.Cell("L3").SetValue("Note"); // Note
                ws9774.Range("L3:L4").Merge();
                ws9774.Columns("L").Width = 30;
                // Style Header
                ws9774.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Font.SetBold();
                ws9774.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws9774.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws9774.Range($"B{rowPlant - 1}:L{rowPlant}").Style.Fill.BackgroundColor = XLColor.FromHtml("#C6E0B4"); // Color
 
                ws9774.Range("B3:L4").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws9774.Range("B3:L4").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws9774.Range("B3:L4").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                // Data GRIR Plant 9771
                var GrIrPlant = await _accountingUniOfWork.GrIrReportRepository.GetGrIrPlantViewFilterAsync(GrIrReportParameter);
                foreach (var value in GrIrPlant)
                {
                    if (value.Plant == "9774")
                    {
                        rowPlant++;
                        ws9774.Cell(rowPlant, 2).SetValue(value.PurchaseTypeDesc); // PO Type
                        ws9774.Cell(rowPlant, 3).SetValue(value.PurchasingDocument); // PO.No
                        ws9774.Cell(rowPlant, 4).SetValue(value.VendorCode + " " + value.Vendors?.VendorName); // Supplier/Supplying
                        ws9774.Cell(rowPlant, 5).SetValue(value.TotalBalance); // Total balance
                        ws9774.Cell(rowPlant, 6).SetValue(value.Range1); // 1-30
                        ws9774.Cell(rowPlant, 7).SetValue(value.Range2); // 31-60
                        ws9774.Cell(rowPlant, 8).SetValue(value.Range3); // 61-90
                        ws9774.Cell(rowPlant, 9).SetValue(value.Range4); // 91-180
                        ws9774.Cell(rowPlant, 10).SetValue(value.Range5); // 181-360
                        ws9774.Cell(rowPlant, 11).SetValue(value.Range6); // 361 up
                    }

                }
                // Grand Total
                ws9774.Cell($"B{rowPlant + 1}").SetValue("Grand Total").Style.Font.SetBold();
                ws9774.Cell($"B{rowPlant + 1}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws9774.Cell($"B{rowPlant + 1}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws9774.Range($"B{rowPlant + 1}:D{rowPlant + 1}").Merge();

                if (rowPlant >= 5)
                {
                    // value Summary
                    ws9774.Cell(rowPlant + 1, 5).FormulaA1 = $"SUM(E5:E{rowPlant})"; // Summary Total balance
                    ws9774.Cell(rowPlant + 1, 6).FormulaA1 = $"SUM(F5:F{rowPlant})"; // Summary 1-30
                    ws9774.Cell(rowPlant + 1, 7).FormulaA1 = $"SUM(G5:G{rowPlant})"; // Summary 31-60
                    ws9774.Cell(rowPlant + 1, 8).FormulaA1 = $"SUM(H5:H{rowPlant})"; // Summary 61-90
                    ws9774.Cell(rowPlant + 1, 9).FormulaA1 = $"SUM(I5:I{rowPlant})"; // Summary 91-180
                    ws9774.Cell(rowPlant + 1, 10).FormulaA1 = $"SUM(J5:J{rowPlant})"; // Summary 181-360
                    ws9774.Cell(rowPlant + 1, 11).FormulaA1 = $"SUM(K5:K{rowPlant})";// Summary 361 up
                }
                ws9774.Range($"B5:L{rowPlant+1}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws9774.Range($"B5:L{rowPlant+1}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws9774.Range($"B5:L{rowPlant+1}").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
                ws9774.Range($"B5:L{rowPlant+1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws9774.Range($"B{rowPlant+1}:L{rowPlant+1}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                // Style
                ws9774.Range($"E5:K{rowPlant + 1}").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
                ws9774.Range($"B{rowPlant + 1}:L{rowPlant + 1}").Style.Fill.BackgroundColor = XLColor.DarkGray;
                ws9774.Range($"B{rowPlant + 1}:L{rowPlant + 1}").Style.Font.SetBold();
                ws9774.SheetView.ZoomScale = 80;
                ws9774.SheetView.FreezeRows(4);
            }

            // GRIR Detail
            var WS_Detail = workbook.Worksheets.Add("Detail").SetTabColor(XLColor.FromHtml("#92D050"));
            if (WS_Detail != null)
            {
                var rowDetail = 6;
                // Detail Columns
                WS_Detail.Cell(2, 2).SetValue("G/L Account").Style.Font.SetBold();
                WS_Detail.Cell(2, 3).SetValue("1201990100");
                WS_Detail.Cell(2, 4).SetValue("GR/IR");
                WS_Detail.Cell(3, 2).SetValue("Company Code").Style.Font.SetBold();
                WS_Detail.Cell(3, 3).SetValue("9770");
                WS_Detail.Cell(3, 4).SetValue("Haier Electric (Thailand)");
                WS_Detail.Cell(4, 2).SetValue("Ledger").Style.Font.SetBold();
                WS_Detail.Cell(4, 3).SetValue("0L");
                WS_Detail.Cell(4, 4).SetValue("Leading Ledger");
                WS_Detail.Range("B2:B4").Style.Fill.BackgroundColor = XLColor.FromHtml("#DDEBF7"); // Color
                WS_Detail.Range("C2:C4").Style.Fill.BackgroundColor = XLColor.FromHtml("#DDEBF7"); // Color
                WS_Detail.Range("D2:D4").Style.Fill.BackgroundColor = XLColor.FromHtml("#DDEBF7"); // Color
                // Header GRIR Detail
                WS_Detail.Columns("A").Width = 2;
                WS_Detail.Cell(rowDetail, 2).SetValue("Assignment");
                WS_Detail.Cell(rowDetail, 3).SetValue("Pur. Doc.");
                WS_Detail.Cell(rowDetail, 4).SetValue("Supplier/Supplying Plant");
                WS_Detail.Cell(rowDetail, 5).SetValue("Plnt");
                WS_Detail.Cell(rowDetail, 6).SetValue("Purchase Type");
                WS_Detail.Cell(rowDetail, 7).SetValue("G/L Acct");
                WS_Detail.Cell(rowDetail, 8).SetValue("Reference");
                WS_Detail.Cell(rowDetail, 9).SetValue("Document No");
                WS_Detail.Cell(rowDetail, 10).SetValue("Document Header Text");
                WS_Detail.Cell(rowDetail, 11).SetValue("Business Area");
                WS_Detail.Cell(rowDetail, 12).SetValue("Document Type");
                WS_Detail.Cell(rowDetail, 13).SetValue("Year/Month");
                WS_Detail.Cell(rowDetail, 14).SetValue("Pstng Date");
                WS_Detail.Cell(rowDetail, 15).SetValue("Doc. Date");
                WS_Detail.Cell(rowDetail, 16).SetValue("PK");
                WS_Detail.Cell(rowDetail, 17).SetValue("D/C");
                WS_Detail.Cell(rowDetail, 18).SetValue("NP");
                WS_Detail.Cell(rowDetail, 19).SetValue("Quantity");
                WS_Detail.Cell(rowDetail, 20).SetValue("Local Crcy Amt");
                WS_Detail.Cell(rowDetail, 21).SetValue("LCurr");
                WS_Detail.Cell(rowDetail, 22).SetValue("Amount in DC");
                WS_Detail.Cell(rowDetail, 23).SetValue("Curr.");
                WS_Detail.Cell(rowDetail, 24).SetValue("Clrng doc.");
                WS_Detail.Cell(rowDetail, 25).SetValue("Profit Ctr");
                WS_Detail.Cell(rowDetail, 26).SetValue("Offset Acct");
                WS_Detail.Cell(rowDetail, 27).SetValue("Text");
                WS_Detail.Cell(rowDetail, 28).SetValue("Obj. key");
                WS_Detail.Cell(rowDetail, 29).SetValue("Reference Key 3");
                WS_Detail.Cell(rowDetail, 30).SetValue("Date");
                WS_Detail.Cell(rowDetail, 31).SetValue("Aging");
                // Style Header
                WS_Detail.Range($"B{rowDetail}:AE{rowDetail}").Style.Font.SetBold();
                WS_Detail.Range($"B{rowDetail}:AE{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                WS_Detail.Range($"B{rowDetail}:AE{rowDetail}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                WS_Detail.Range($"B{rowDetail}:AE{rowDetail}").Style.Fill.BackgroundColor = XLColor.FromHtml("#DDEBF7"); // Color

                // Data GRIR Detail
                var GrIrDetails = await _accountingUniOfWork.GrIrReportRepository.GetGrIrDetailViewFilterAsync(GrIrReportParameter);
                foreach (var value in GrIrDetails)
                {
                    rowDetail++;
                    WS_Detail.Cell(rowDetail, 2).SetValue(value.Assingment); // Assingment
                    WS_Detail.Cell(rowDetail, 3).SetValue(value.PurchasingDocument); // Pur. Doc.
                    WS_Detail.Cell(rowDetail, 4).SetValue(value.VendorCode + " " + value.Vendors?.VendorName);
                    WS_Detail.Cell(rowDetail, 5).SetValue(value.Plant); // Plant
                    WS_Detail.Cell(rowDetail, 6).SetValue(value.PurchaseTypeDesc); // PurchaseType
                    WS_Detail.Cell(rowDetail, 7).SetValue(value.GlAcct); // G/L Acct
                    WS_Detail.Cell(rowDetail, 8).SetValue(value.Reference); // Reference
                    WS_Detail.Cell(rowDetail, 9).SetValue(value.DocumentNo); // Document No
                    WS_Detail.Cell(rowDetail, 10).SetValue(value.DocumentHeaderText); // Document Header Text
                    WS_Detail.Cell(rowDetail, 11).SetValue(value.BusinessArea); // Business Area
                    WS_Detail.Cell(rowDetail, 12).SetValue(value.DocumentType); // Document Type
                    WS_Detail.Cell(rowDetail, 13).SetValue(value.YearMonth); // Year/Month
                    WS_Detail.Cell(rowDetail, 14).SetValue(value.PostingDate); // Pstng Date
                    WS_Detail.Cell(rowDetail, 15).SetValue(value.DocumentDate); // Doc. Date
                    WS_Detail.Cell(rowDetail, 16).SetValue(""); // PK
                    WS_Detail.Cell(rowDetail, 17).SetValue(value.DebitCredit); // D/C
                    WS_Detail.Cell(rowDetail, 18).SetValue(""); // NP
                    WS_Detail.Cell(rowDetail, 19).SetValue(value.Quantity); // Quantity
                    WS_Detail.Cell(rowDetail, 20).SetValue(value.AmountLc); // Local Crcy Amt
                    WS_Detail.Cell(rowDetail, 21).SetValue(value.LocalCurrency); // LCurr
                    WS_Detail.Cell(rowDetail, 22).SetValue(value.AmountDc); // Amount in DC
                    WS_Detail.Cell(rowDetail, 23).SetValue(value.DocumentCurrency); // Curr.
                    WS_Detail.Cell(rowDetail, 24).SetValue(value.ClearingDocument); // Clrng doc.
                    WS_Detail.Cell(rowDetail, 25).SetValue(value.ProfitCenter); // Profit Ctr
                    WS_Detail.Cell(rowDetail, 26).SetValue(value.OffsetAcct); // Offset Acct
                    WS_Detail.Cell(rowDetail, 27).SetValue(value.Text); // Text
                    WS_Detail.Cell(rowDetail, 28).SetValue(value.ObjectKey); // Obj. key
                    WS_Detail.Cell(rowDetail, 29).SetValue(value.ReferenceKeyThree); // Reference Key 3
                    WS_Detail.Cell(rowDetail, 30).SetValue(value.DateDiff); // Date
                    WS_Detail.Cell(rowDetail, 31).SetValue(value.Aging);
                }
                WS_Detail.Range($"S7:S{rowDetail + 1}").Style.NumberFormat.Format = "#,##0.000";
                WS_Detail.Range($"T7:T{rowDetail + 1}").Style.NumberFormat.Format = "#,##0.00";
                WS_Detail.Range($"V7:V{rowDetail + 1}").Style.NumberFormat.Format = "#,##0.00";
                // Set Auto fit
                WS_Detail.Columns("B:AE").AdjustToContents();
                WS_Detail.SheetView.ZoomScale = 80;
                WS_Detail.SheetView.FreezeRows(6);
            }

            // Save file
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
    }

}