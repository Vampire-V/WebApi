using AutoMapper;
using ClosedXML.Excel;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.TaxReportBSEG;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class TaxReportService : ITaxReportService
    {
        private readonly IAccountingUniOfWork _AccountingUniOfWork;
        public readonly IMapper _mapper;

        public TaxReportService(IAccountingUniOfWork AccountingUniOfWork, IMapper mapper)
        {
            _AccountingUniOfWork = AccountingUniOfWork;
            _mapper = mapper;
        }

        public async Task<List<TaxReportView>> GetTaxReport(TaxReportParameter taxReportParameter)
        {
            // List<TaxReportView> TaxReportNew = await _AccountingUniOfWork.TaxReportRepository.GetTaxReportFilterAsync(taxReportParameter);
            // return _mapper.Map<List<TaxReportView>>(TaxReportNew);
            return await _AccountingUniOfWork.TaxReportRepository.GetTaxReportFilterAsync(taxReportParameter);
        }

        public async Task<byte[]> GetTaxReportForExcel(TaxReportParameter taxReportParameter)
        {
            var taxReports = await _AccountingUniOfWork.TaxReportRepository.GetTaxReportFilterAsync(taxReportParameter);
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("TaxReport");
            var currentRow = 4;
            var index = 0;

            // Header
            worksheet.Cell("A1").SetValue("ลำดับที่").Style.Font.SetBold();
            worksheet.Range("A1:A4").Merge();
            worksheet.Columns("A").Width = 8;

            worksheet.Cell("B1").SetValue("Doc.No.-FI").Style.Font.SetBold();
            worksheet.Range("B1:B4").Merge();
            worksheet.Columns("B").Width = 12;

            worksheet.Cell("C1").SetValue("ใบกำกับภาษี").Style.Font.SetBold();
            worksheet.Range("C1:C2").Merge();
            worksheet.Cell("C3").SetValue("วัน เดือน ปี").Style.Font.SetBold();
            worksheet.Range("C3:C4").Merge();
            worksheet.Columns("C").Width = 13;

            worksheet.Cell("D1").SetValue("เลขที่ใบกำกับภาษี").Style.Font.SetBold();
            worksheet.Range("D1:D4").Merge();
            worksheet.Columns("D").Width = 18;

            worksheet.Cell("E1").SetValue("Name").Style.Font.SetBold();
            worksheet.Range("E1:E4").Merge();
            worksheet.Columns("E").Width = 12;

            worksheet.Cell("F1").SetValue("ชื่อผู้ขายสินค้า/").Style.Font.SetBold();
            worksheet.Range("F1:F2").Merge();
            worksheet.Cell("F3").SetValue("ผู้ให้บริการ").Style.Font.SetBold();
            worksheet.Range("F3:F4").Merge();
            worksheet.Columns("F").Width = 60;

            worksheet.Cell("G1").SetValue("เลขประจำตัวผู้เสียภาษี").Style.Font.SetBold();
            worksheet.Range("G1:G2").Merge();
            worksheet.Cell("G3").SetValue("ของผู้ขายสินค้า/").Style.Font.SetBold();
            worksheet.Cell("G4").SetValue("ผู้ให้บริการ").Style.Font.SetBold();
            worksheet.Columns("G").Width = 23;

            worksheet.Cell("H1").SetValue("สถานประกอบการ").Style.Font.SetBold();
            worksheet.Range("H1:I2").Merge();
            worksheet.Cell("H3").SetValue("สนญ.").Style.Font.SetBold();
            worksheet.Range("H3:H4").Merge();
            worksheet.Columns("H").Width = 8;
            worksheet.Cell("I3").SetValue("สาขาที่").Style.Font.SetBold();
            worksheet.Range("I3:I4").Merge();
            worksheet.Columns("I").Width = 8;

            worksheet.Cell("J1").SetValue("มูลค่าสินค้า").Style.Font.SetBold();
            worksheet.Range("J1:J2").Merge();
            worksheet.Cell("J3").SetValue("หรือบริการ").Style.Font.SetBold();
            worksheet.Range("J3:J4").Merge();
            worksheet.Columns("J").Width = 14;

            worksheet.Cell("K1").SetValue("จำนวนเงิน").Style.Font.SetBold();
            worksheet.Range("K1:K2").Merge();
            worksheet.Cell("K3").SetValue("ภาษีมูลค่าเพิ่ม").Style.Font.SetBold();
            worksheet.Range("K3:K4").Merge();
            worksheet.Columns("K").Width = 14;

            worksheet.Cell("L1").SetValue("Check vat").Style.Font.SetBold();
            worksheet.Range("L1:L4").Merge();
            worksheet.Columns("L").Width = 14;

            worksheet.Cell("M1").SetValue("diff").Style.Font.SetBold();
            worksheet.Range("M1:M4").Merge();
            worksheet.Columns("M").Width = 14;

            worksheet.Cell("O1").SetValue("Assignment").Style.Font.SetBold();
            worksheet.Range("O1:O4").Merge();
            worksheet.Columns("O").Width = 22;

            worksheet.Cell("P1").SetValue("Posting Date").Style.Font.SetBold();
            worksheet.Range("P1:P4").Merge();
            worksheet.Columns("P").Width = 22;

            foreach (var value in taxReports)
            {
                index++;
                currentRow++;
                worksheet.Cell(currentRow, 1).SetValue(index); // ลำดับที่
                worksheet.Cell(currentRow, 2).SetValue(value.DocumentNo);
                worksheet.Cell(currentRow, 3).SetValue(value.InvoiceDate);
                worksheet.Cell(currentRow, 4).SetValue(value.InvoiceNo);
                worksheet.Cell(currentRow, 5).SetValue(value.VendorCode);
                worksheet.Cell(currentRow, 6).SetValue(value.VendorName);
                worksheet.Cell(currentRow, 7).SetValue(value.TaxId);
                worksheet.Cell(currentRow, 8).SetValue(value.HeadOfficeId);
                worksheet.Cell(currentRow, 9).SetValue(value.BranchId);
                worksheet.Cell(currentRow, 10).SetValue(value.TaxBase);
                worksheet.Cell(currentRow, 11).SetValue(value.VatAmount);

                worksheet.Cell(currentRow, 12).FormulaA1 = $"=ROUND(J{currentRow} * 0.07,2)"; // Check vat
                worksheet.Cell(currentRow, 13).FormulaA1 = $"=K{currentRow} - L{currentRow}"; // diff

                worksheet.Cell(currentRow, 15).SetValue(value.Assignment);
                worksheet.Cell(currentRow, 16).SetValue(value.PostingDate);
            }
            // Detail
            // foreach (var indirectVendor in indirectVendors)
            // {
            //     index++;
            //     currentRow++;
            //     worksheet.Cell(currentRow, 1).SetValue(index); // ลำดับที่
            //     worksheet.Cell(currentRow, 2).SetValue(indirectVendor.DocumentNo); // Doc.No.-FI

            //     if (indirectVendor.DocumentNo.Substring(0, 2) == "19")
            //     {
            //         // เอาค่าจาก TaxReportBSEG
            //         worksheet.Cell(currentRow, 3).SetValue(indirectVendor.TaxInvoiceDate?.Replace(".", "/")); //ใบกำกับภาษี วัน เดือน ปี
            //         worksheet.Cell(currentRow, 4).SetValue(indirectVendor.TaxInvoiceId); //เลขที่ใบกำกับภาษี
            //         worksheet.Cell(currentRow, 5).SetValue(indirectVendor.VendorCode); // Name (vendor code)

            //         // เอาค่าจาก AccountingIndirectVendor
            //         worksheet.Cell(currentRow, 6).SetValue(indirectVendor.AccountingIndirectVendor?.VendorName); // ชื่อผู้ขายสินค้า/ผู้ให้บริการ
            //         worksheet.Cell(currentRow, 7).SetValue(indirectVendor.AccountingIndirectVendor?.TaxId); // เลขประจำตัวผู้เสียภาษี
            //         worksheet.Cell(currentRow, 8).SetValue(indirectVendor.AccountingIndirectVendor?.HeadOfficeId); // สนญ (สำนักงานใหญ่)
            //         worksheet.Cell(currentRow, 9).SetValue(indirectVendor.AccountingIndirectVendor?.BranchId); // สาขาที่
            //     }
            //     else
            //     {
            //         // เอาค่าจาก TaxReportBSEG
            //         worksheet.Cell(currentRow, 3).SetValue(indirectVendor.DocumentDate.ToString("dd/MM/yyyy")); //ใบกำกับภาษี วัน เดือน ปี
            //         worksheet.Cell(currentRow, 4).SetValue(indirectVendor.TaxReportBKPF?.Reference); // เลขที่ใบกำกับภาษี

            //         if (indirectVendor.DocumentNo.Substring(0, 2) == "07" && indirectVendor.Assignment?.Substring(0, 2) == "B0")
            //         {
            //             if (indirectVendor.Assignment.Substring(0, 2) == "B0" || indirectVendor.Assignment.Substring(0, 2) == "00")
            //             {
            //                 // เอาค่าจาก TaxReportBSEG.Assignment ไป Join
            //                 worksheet.Cell(currentRow, 5).SetValue(indirectVendor.Assignment); // Name (vendor code)
            //                 worksheet.Cell(currentRow, 6).SetValue(indirectVendor.VendorAssignment?.VendorName); // ชื่อผู้ขายสินค้า/ผู้ให้บริการ
            //                 worksheet.Cell(currentRow, 7).SetValue(indirectVendor.VendorAssignment?.TaxNumber1); // เลขประจำตัวผู้เสียภาษี
            //                 worksheet.Cell(currentRow, 8).SetValue(indirectVendor.VendorAssignment?.HeadOfficeId); // สนญ (สำนักงานใหญ่)
            //                 worksheet.Cell(currentRow, 9).SetValue(indirectVendor.VendorAssignment?.BranchId); // สาขาที่
            //             }

            //         }
            //         else
            //         {
            //             if (indirectVendor.OffsetAcct?.Substring(0, 2) == "B0" || indirectVendor.OffsetAcct?.Substring(0, 2) == "00")
            //             {
            //                 // เอาค่าจาก TaxReportBSEG.OffsetAcct ไป Join
            //                 worksheet.Cell(currentRow, 5).SetValue(indirectVendor.OffsetAcct); // Name (vendor code)
            //                 worksheet.Cell(currentRow, 6).SetValue(indirectVendor.AccountingVendor?.VendorName); // ชื่อผู้ขายสินค้า/ผู้ให้บริการ
            //                 worksheet.Cell(currentRow, 7).SetValue(indirectVendor.AccountingVendor?.TaxNumber1); // เลขประจำตัวผู้เสียภาษี
            //                 worksheet.Cell(currentRow, 8).SetValue(indirectVendor.AccountingVendor?.HeadOfficeId); // สนญ (สำนักงานใหญ่)
            //                 worksheet.Cell(currentRow, 9).SetValue(indirectVendor.AccountingVendor?.BranchId); // สาขาที่
            //             }
            //         }
            //     }

            //     if (indirectVendor.DebitCredit == "S") // Debit
            //     {
            //         worksheet.Cell(currentRow, 10).SetValue(indirectVendor.TaxBase); // มูลค่าสินค้าหรือบริการ
            //         worksheet.Cell(currentRow, 11).SetValue(indirectVendor.VatAmount); // จำนวนเงินภาษีมูลค่าเพิ่ม
            //     }
            //     else if (indirectVendor.DebitCredit == "H") // Credit
            //     {
            //         worksheet.Cell(currentRow, 10).SetValue(-Math.Abs(indirectVendor.TaxBase)); // มูลค่าสินค้าหรือบริการ (ค่าติดลบ)
            //         worksheet.Cell(currentRow, 11).SetValue(-Math.Abs(indirectVendor.VatAmount)); // จำนวนเงินภาษีมูลค่าเพิ่ม (ค่าติดลบ)
            //         worksheet.Cell(currentRow, 14).SetValue("Credit");
            //     }


            //     worksheet.Cell(currentRow, 12).FormulaA1 = $"=ROUND(J{currentRow} * 0.07,2)"; // Check vat
            //     worksheet.Cell(currentRow, 13).FormulaA1 = $"=K{currentRow} - L{currentRow}"; // diff

            //     worksheet.Cell(currentRow, 15).SetValue(indirectVendor.Assignment); // Assignment
            //     worksheet.Cell(currentRow, 16).SetValue(indirectVendor.PostingDate); // Posting Date
            // }

            // Total
            worksheet.Cell($"A{currentRow + 1}").SetValue("Total").Style.Font.SetBold();
            worksheet.Cell($"A{currentRow + 1}").Style.Fill.BackgroundColor = XLColor.CoolBlack;
            worksheet.Cell($"A{currentRow + 1}").Style.Font.FontColor = XLColor.White;
            worksheet.Cell($"A{currentRow + 1}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell($"A{currentRow + 1}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Range($"A{currentRow + 1}:I{currentRow + 1}").Merge();

            // Summary base amount
            worksheet.Cell(currentRow + 1, 10).FormulaA1 = $"SUM(J5:J{currentRow})";
            worksheet.Cell(currentRow + 1, 10).Style.Fill.BackgroundColor = XLColor.DarkGray;
            worksheet.Cell(currentRow + 1, 10).Style.Font.SetBold();
            worksheet.Cell(currentRow + 1, 10).Style.NumberFormat.Format = "#,##0.00";

            // Summary vat7
            worksheet.Cell(currentRow + 1, 11).FormulaA1 = $"SUM(K5:K{currentRow})";
            worksheet.Cell(currentRow + 1, 11).Style.Fill.BackgroundColor = XLColor.DarkGray;
            worksheet.Cell(currentRow + 1, 11).Style.Font.SetBold();
            worksheet.Cell(currentRow + 1, 11).Style.NumberFormat.Format = "#,##0.00";

            // Summary check vat
            worksheet.Cell(currentRow + 1, 12).FormulaA1 = $"SUM(L5:L{currentRow})";
            worksheet.Cell(currentRow + 1, 12).Style.Fill.BackgroundColor = XLColor.ArylideYellow;
            worksheet.Cell(currentRow + 1, 12).Style.Font.SetBold();
            worksheet.Cell(currentRow + 1, 12).Style.NumberFormat.Format = "#,##0.00";

            // Summary diff
            worksheet.Cell(currentRow + 1, 13).FormulaA1 = $"SUM(M5:M{currentRow})";
            worksheet.Cell(currentRow + 1, 13).Style.Fill.BackgroundColor = XLColor.Alizarin;
            worksheet.Cell(currentRow + 1, 13).Style.Font.SetBold();
            worksheet.Cell(currentRow + 1, 13).Style.NumberFormat.Format = "#,##0.00";

            worksheet.Cells($"J2:M{currentRow}").Style.NumberFormat.Format = "#,##0.00"; // format number 
            worksheet.Cells($"L2:L{currentRow}").Style.Fill.BackgroundColor = XLColor.LightGoldenrodYellow; // BG Color check vat
            worksheet.Cells($"M2:M{currentRow}").Style.Fill.BackgroundColor = XLColor.BabyPink; // BG Color diff

            // Alignment
            worksheet.Columns("A:C").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Columns("G:I").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cells($"P5:P{currentRow}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cells($"P5:P{currentRow}").Style.DateFormat.Format = "dd/MM/yyyy"; // Format date

            // BG & font color header
            worksheet.Cells("A1:M4").Style.Fill.BackgroundColor = XLColor.CoolBlack;
            worksheet.Cells("A1:M4").Style.Font.FontColor = XLColor.White;
            worksheet.Cells("A1:P4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cells("A1:P4").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Cell("M1").Style.Font.FontColor = XLColor.Red; // Font color diff
            worksheet.Column("N").Style.Font.FontColor = XLColor.Red;

            // FreezeRows
            worksheet.SheetView.FreezeRows(4);
            worksheet.RangeUsed().Row(4).SetAutoFilter();

            // Zoom Scale
            worksheet.SheetView.ZoomScale = 80;

            // Auto Fit Columns
            worksheet.Columns().AdjustToContents(5, currentRow + 1);
            worksheet.Columns("G").Width = 23;
            worksheet.Columns("H").Width = 8;
            worksheet.Columns("I").Width = 8;

            // SaveAs
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
    }
}