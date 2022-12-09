using WebApi.Data.Accounting.Entities;
using WebApi.Models.SaleRebate;
using ClosedXML.Excel;
using WebApi.Services.Interface;
using WebApi.Extensions;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class SaleRebateService : ISaleRebateService
    {
        private readonly IAccountingUniOfWork _accountingUniOfWork;
        public SaleRebateService(IAccountingUniOfWork accountingUniOfWork)
        {
            _accountingUniOfWork = accountingUniOfWork;
        }

        public async Task<List<SaleRebateView>> GetSaleRebateViewFilterAsync(SaleRebateParameter SaleRebateParameter)
        {
            List<SaleRebateView> SaleRebateView = await _accountingUniOfWork.SaleRebateRepository.GetSaleRebateViewFilterAsync(SaleRebateParameter);
            return SaleRebateView;
        }

        public async Task<List<SaleRebateDetail>> GetSaleRebateDetailFilterAsync(SaleRebateParameter SaleRebateParameter)
        {
            List<SaleRebateDetail> SaleRebateDetail = await _accountingUniOfWork.SaleRebateRepository.GetSaleRebateDetailFilterAsync(SaleRebateParameter);
            return SaleRebateDetail;
        }

        public async Task<byte[]> GetSaleRebateForExcel(SaleRebateParameter SaleRebateParameter)
        {
            var details = await _accountingUniOfWork.SaleRebateRepository.GetSaleRebateDetailFilterAsync(SaleRebateParameter);
            var summarys = await _accountingUniOfWork.SaleRebateRepository.GetSaleRebateViewFilterAsync(SaleRebateParameter);
            var workbook = new XLWorkbook();

            var wsSummary = workbook.Worksheets.Add("Summary");
            if (wsSummary != null)
            {
                wsSummary.Columns("A:AZ").Style.Fill.BackgroundColor = XLColor.White;
                wsSummary.SheetView.ZoomScale = 90;
                wsSummary.Columns("A").Width = 2;
                wsSummary.Cell(2, 2).SetValue("Sales Singer Thai ...").Style.Font.SetBold();
                // Month
                wsSummary.Cell(3, 2).SetValue("Month");
                wsSummary.Range("B3:B5").Merge();
                wsSummary.Columns("B").Width = 12;
                // -> value
                wsSummary.Cell(6, 2).SetValue("Jan");
                wsSummary.Cell(7, 2).SetValue("Feb");
                wsSummary.Cell(8, 2).SetValue("Mar");
                wsSummary.Cell(9, 2).SetValue("Apr");
                wsSummary.Cell(10, 2).SetValue("May");
                wsSummary.Cell(11, 2).SetValue("Jun");
                wsSummary.Cell(12, 2).SetValue("Jul");
                wsSummary.Cell(13, 2).SetValue("Aug");
                wsSummary.Cell(14, 2).SetValue("Sep");
                wsSummary.Cell(15, 2).SetValue("Oct");
                wsSummary.Cell(16, 2).SetValue("Nov");
                wsSummary.Cell(17, 2).SetValue("Dec");
                // -> Total
                wsSummary.Cell(18, 2).SetValue("Total").Style.Font.SetBold();
                // Quantity
                wsSummary.Cell(3, 3).SetValue("Quantity");
                wsSummary.Range("C3:E3").Merge();
                wsSummary.Cell(4, 3).SetValue("B000063490");
                wsSummary.Cell(5, 3).SetValue("RF (Units)");
                wsSummary.Columns("C").Width = 12;
                wsSummary.Cell(4, 4).SetValue("B000063665");
                wsSummary.Cell(5, 4).SetValue("AC (Sets)");
                wsSummary.Columns("D").Width = 12;
                wsSummary.Cell(4, 5).SetValue("TOTAL");
                wsSummary.Range("E4:E5").Merge();
                wsSummary.Columns("E").Width = 12;
                // Sales Amt. in LC (THB)
                wsSummary.Cell(3, 6).SetValue("Sales Amt. in LC (THB)");
                wsSummary.Range("F3:H3").Merge();
                wsSummary.Cell(4, 6).SetValue("B000063490");
                wsSummary.Cell(5, 6).SetValue("RF");
                wsSummary.Columns("F").Width = 15;
                wsSummary.Cell(4, 7).SetValue("B000063665");
                wsSummary.Cell(5, 7).SetValue("AC");
                wsSummary.Columns("G").Width = 15;
                wsSummary.Cell(4, 8).SetValue("TOTAL");
                wsSummary.Range("H4:H5").Merge();
                wsSummary.Columns("H").Width = 16;
                // Sales promotion Amt. in LC (THB)
                wsSummary.Cell(3, 9).SetValue("Sales promotion Amt. in LC (THB)");
                wsSummary.Range("I3:M3").Merge();
                wsSummary.Cell(4, 9).SetValue("RF");
                wsSummary.Range("I4:J4").Merge();
                wsSummary.Cell(5, 9).SetValue("%");
                wsSummary.Columns("I").Width = 3;
                wsSummary.Cell(5, 10).SetValue("Amt.");
                wsSummary.Columns("J").Width = 14;
                wsSummary.Cell(4, 11).SetValue("AC");
                wsSummary.Range("K4:L4").Merge();
                wsSummary.Cell(5, 11).SetValue("%");
                wsSummary.Columns("K").Width = 3;
                wsSummary.Cell(5, 12).SetValue("Amt.");
                wsSummary.Columns("L").Width = 14;
                wsSummary.Cell(4, 13).SetValue("TOTAL");
                wsSummary.Range("M4:M5").Merge();
                wsSummary.Columns("M").Width = 14;
                // Royalty Amt. in LC (THB)
                wsSummary.Cell(3, 14).SetValue("Royalty Amt. in LC (THB)");
                wsSummary.Range("N3:P3").Merge();
                wsSummary.Cell(4, 14).SetValue("RF");
                wsSummary.Range("N4:O4").Merge();
                wsSummary.Cell(5, 14).SetValue("%");
                wsSummary.Columns("N").Width = 3;
                wsSummary.Cell(5, 15).SetValue("Amt.");
                wsSummary.Columns("O").Width = 12;
                wsSummary.Cell(4, 16).SetValue("TOTAL");
                wsSummary.Range("P4:P5").Merge();
                wsSummary.Columns("P").Width = 12;
                // Total Rebate & Royalty
                wsSummary.Cell(3, 17).SetValue("Total Rebate & Royalty");
                wsSummary.Range("Q3:Q5").Merge();
                wsSummary.Columns("Q").Width = 21;
                var rowSum = 5;
                foreach (var value in summarys)
                {
                    wsSummary.Cell(2, 2).SetValue($"Sales Singer Thai {value.Month!.Substring(0, 4)}").Style.Font.SetBold();
                    rowSum++;
                    wsSummary.Cell(rowSum, 3).SetValue(value.RFQty);
                    wsSummary.Cell(rowSum, 4).SetValue(value.ACQty);
                    wsSummary.Cell(rowSum, 5).SetValue(value.TotalQty);
                    wsSummary.Cell(rowSum, 6).SetValue(value.RFAmt);
                    wsSummary.Cell(rowSum, 7).SetValue(value.ACAmt);
                    wsSummary.Cell(rowSum, 8).SetValue(value.TotalAmt);
                    wsSummary.Cell(rowSum, 9).SetValue(value.RFPromotionPercent);
                    wsSummary.Cell(rowSum, 10).SetValue(value.RFPromotionAmt);
                    wsSummary.Cell(rowSum, 11).SetValue(value.ACPromotionPercent);
                    wsSummary.Cell(rowSum, 12).SetValue(value.ACPromotionAmt);
                    wsSummary.Cell(rowSum, 13).SetValue(value.TotalPromotionAmt);
                    wsSummary.Cell(rowSum, 14).SetValue(value.RFRoyaltyPercent);
                    wsSummary.Cell(rowSum, 15).SetValue(value.RFRoyaltyAmt);
                    wsSummary.Cell(rowSum, 16).FormulaA1 = $"=O{rowSum}";
                    wsSummary.Cell(rowSum, 17).FormulaA1 = $"=M{rowSum}+P{rowSum}";
                }
                // Total summary
                wsSummary.Cell(18, 3).FormulaA1 = "=sum(C6:C17)";
                wsSummary.Cell(18, 4).FormulaA1 = "=sum(D6:D17)";
                wsSummary.Cell(18, 5).FormulaA1 = "=sum(E6:E17)";
                wsSummary.Cell(18, 6).FormulaA1 = "=sum(F6:F17)";
                wsSummary.Cell(18, 7).FormulaA1 = "=sum(G6:G17)";
                wsSummary.Cell(18, 8).FormulaA1 = "=sum(H6:H17)";
                wsSummary.Cell(18, 10).FormulaA1 = "=sum(J6:J17)";
                wsSummary.Cell(18, 12).FormulaA1 = "=sum(L6:L17)";
                wsSummary.Cell(18, 13).FormulaA1 = "=sum(M6:M17)";
                wsSummary.Cell(18, 15).FormulaA1 = "=sum(O6:O17)";
                wsSummary.Cell(18, 16).FormulaA1 = "=sum(P6:P17)";
                wsSummary.Cell(18, 17).FormulaA1 = "=sum(Q6:Q17)";
                // Style Header
                wsSummary.Range("B3:Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSummary.Range("B3:Q5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                wsSummary.Range("B3:Q5").Style.Font.SetBold();
                wsSummary.Range("B18:Q18").Style.Fill.BackgroundColor = XLColor.FromHtml("#B7DEE8"); // Color
                wsSummary.Range("B3:B5").Style.Fill.BackgroundColor = XLColor.FromHtml("#B7DEE8"); // Color
                wsSummary.Range("F3:H5").Style.Fill.BackgroundColor = XLColor.FromHtml("#B7DEE8"); // Color
                wsSummary.Range("N3:P5").Style.Fill.BackgroundColor = XLColor.FromHtml("#B7DEE8"); // Color

                wsSummary.Range("C3:E5").Style.Fill.BackgroundColor = XLColor.FromHtml("#4BACC6"); // Color
                wsSummary.Range("I3:M5").Style.Fill.BackgroundColor = XLColor.FromHtml("#4BACC6"); // Color
                wsSummary.Range("Q3:Q5").Style.Fill.BackgroundColor = XLColor.FromHtml("#4BACC6"); // Color
                // Style Rows
                wsSummary.Range("B3:B18").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSummary.Range("I6:I18").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSummary.Range("K6:K18").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSummary.Range("N6:N18").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSummary.Range("C6:H18").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
                wsSummary.Range("J6:J18").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
                wsSummary.Range("L6:M18").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";
                wsSummary.Range("O6:Q18").Style.NumberFormat.Format = "_(#,##0.00_);_((#,##0.00);_(\"-\"??_);_(@_)";

                wsSummary.Range("B3:Q5").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                wsSummary.Range("B3:Q5").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                wsSummary.Range("B3:Q5").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                wsSummary.Range("B6:Q18").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                wsSummary.Range("B6:Q18").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                wsSummary.Range("B6:Q17").Style.Border.TopBorder = XLBorderStyleValues.Dotted;
                wsSummary.Range("B6:Q17").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsSummary.Range("B19:Q19").Style.Border.TopBorder = XLBorderStyleValues.Thin;
            }

            var wsDetail = workbook.Worksheets.Add("Detail");
            if (wsDetail != null)
            {
                var rowDetail = 1;
                wsDetail.Cell(rowDetail, 1).SetValue("ProfSeg");
                wsDetail.Cell(rowDetail, 2).SetValue("Payer");
                wsDetail.Cell(rowDetail, 3).SetValue("BillDoc");
                wsDetail.Cell(rowDetail, 4).SetValue("BillItem");
                wsDetail.Cell(rowDetail, 5).SetValue("Return");
                wsDetail.Cell(rowDetail, 6).SetValue("BillQty");
                wsDetail.Cell(rowDetail, 7).SetValue("BillUnit");
                wsDetail.Cell(rowDetail, 8).SetValue("BillQtySKU");
                wsDetail.Cell(rowDetail, 9).SetValue("NetValue");
                wsDetail.Cell(rowDetail, 10).SetValue("RefDoc");
                wsDetail.Cell(rowDetail, 11).SetValue("RefItem");
                wsDetail.Cell(rowDetail, 12).SetValue("SalesDoc");
                wsDetail.Cell(rowDetail, 13).SetValue("SalesItem");
                wsDetail.Cell(rowDetail, 14).SetValue("Material");
                wsDetail.Cell(rowDetail, 15).SetValue("ItemDescription");
                wsDetail.Cell(rowDetail, 16).SetValue("MatlGroup");
                wsDetail.Cell(rowDetail, 17).SetValue("ItemCategory");
                wsDetail.Cell(rowDetail, 18).SetValue("ShippingPoint");
                wsDetail.Cell(rowDetail, 19).SetValue("Plant");
                wsDetail.Cell(rowDetail, 20).SetValue("AssignmentGroup");
                wsDetail.Cell(rowDetail, 21).SetValue("CreatedBy");
                wsDetail.Cell(rowDetail, 22).SetValue("CreatedOn");
                wsDetail.Cell(rowDetail, 23).SetValue("CreatedTime");
                wsDetail.Cell(rowDetail, 24).SetValue("StorageLocation");
                wsDetail.Cell(rowDetail, 25).SetValue("Cost");
                wsDetail.Cell(rowDetail, 26).SetValue("ProfitCenter");
                wsDetail.Cell(rowDetail, 27).SetValue("TaxAmount");
                wsDetail.Cell(rowDetail, 28).SetValue("BillType");
                wsDetail.Cell(rowDetail, 29).SetValue("SaleOrg");
                wsDetail.Cell(rowDetail, 30).SetValue("BillingDate");

                wsDetail.Range("A1:AD1").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                wsDetail.Range("A1:AD1").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                wsDetail.Range("A1:AD1").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                foreach (var value in details)
                {
                    rowDetail++;
                    wsDetail.Cell(rowDetail, 1).SetValue(value.ProfSeg);
                    wsDetail.Cell(rowDetail, 2).SetValue(value.Payer);
                    wsDetail.Cell(rowDetail, 3).SetValue(value.BillDoc);
                    wsDetail.Cell(rowDetail, 4).SetValue(value.BillItem);
                    wsDetail.Cell(rowDetail, 5).SetValue(value.Return);
                    wsDetail.Cell(rowDetail, 6).SetValue(value.BillQty);
                    wsDetail.Cell(rowDetail, 7).SetValue(value.BillUnit);
                    wsDetail.Cell(rowDetail, 8).SetValue(value.BillQtySKU);
                    wsDetail.Cell(rowDetail, 9).SetValue(value.NetValue);
                    wsDetail.Cell(rowDetail, 10).SetValue(value.RefDoc);
                    wsDetail.Cell(rowDetail, 11).SetValue(value.RefItem);
                    wsDetail.Cell(rowDetail, 12).SetValue(value.SalesDoc);
                    wsDetail.Cell(rowDetail, 13).SetValue(value.SalesItem);
                    wsDetail.Cell(rowDetail, 14).SetValue(value.Material);
                    wsDetail.Cell(rowDetail, 15).SetValue(value.ItemDescription);
                    wsDetail.Cell(rowDetail, 16).SetValue(value.MatlGroup);
                    wsDetail.Cell(rowDetail, 17).SetValue(value.ItemCategory);
                    wsDetail.Cell(rowDetail, 18).SetValue(value.ShippingPoint);
                    wsDetail.Cell(rowDetail, 19).SetValue(value.Plant);
                    wsDetail.Cell(rowDetail, 20).SetValue(value.AssignmentGroup);
                    wsDetail.Cell(rowDetail, 21).SetValue(value.CreatedBy);
                    wsDetail.Cell(rowDetail, 22).SetValue(value.CreatedOn);
                    wsDetail.Cell(rowDetail, 23).SetValue(value.CreatedTime);
                    wsDetail.Cell(rowDetail, 24).SetValue(value.StorageLocation);
                    wsDetail.Cell(rowDetail, 25).SetValue(value.Cost);
                    wsDetail.Cell(rowDetail, 26).SetValue(value.ProfitCenter);
                    wsDetail.Cell(rowDetail, 27).SetValue(value.TaxAmount);
                    wsDetail.Cell(rowDetail, 28).SetValue(value.BillType);
                    wsDetail.Cell(rowDetail, 29).SetValue(value.SaleOrg);
                    wsDetail.Cell(rowDetail, 30).SetValue(value.BillingDate);
                }
                // Format style sheet
                wsDetail.SheetView.ZoomScale = 80;
                wsDetail.SheetView.Freeze(1, 1);
                wsDetail.Range("A1:AD1").Style.Fill.BackgroundColor = XLColor.FromHtml("#B7DEE8"); // Color
                wsDetail.Range("A1:AD1").Style.Font.SetBold();
                wsDetail.Range($"A1:AD{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsDetail.Range($"I2:I{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                wsDetail.Range($"N2:O{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                wsDetail.Range($"Y2:Y{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                wsDetail.Range($"AA2:AA{rowDetail}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                wsDetail.Range($"I2:I{rowDetail}").Style.NumberFormat.Format = "#,##0.00";
                wsDetail.Range($"Y2:Y{rowDetail}").Style.NumberFormat.Format = "#,##0.00";
                wsDetail.Range($"AA2:AA{rowDetail}").Style.NumberFormat.Format = "#,##0.00";
                wsDetail.Columns("A:AD").AdjustToContents();
            }

            // Save file
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }

        public async Task<byte[]> GetSaleRebateTemplateForExcel(SaleRebateParameter SaleRebateParameter)
        {
            var SaleRebateTemplate = await _accountingUniOfWork.SaleRebateRepository.GetSaleRebateViewFilterAsync(SaleRebateParameter);
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Template");

            worksheet.SheetView.ZoomScale = 90;
            worksheet.Cell(1, 1).SetValue("SN");
            worksheet.Cell(1, 2).SetValue("Company code");
            worksheet.Cell(1, 3).SetValue("Doc date");
            worksheet.Cell(1, 4).SetValue("Post date");
            worksheet.Cell(1, 5).SetValue("doc type");
            worksheet.Cell(1, 6).SetValue("reference");
            worksheet.Cell(1, 7).SetValue("curr");
            worksheet.Cell(1, 8).SetValue("Period");
            worksheet.Cell(1, 9).SetValue("Header text");
            worksheet.Cell(1, 10).SetValue("Ref.key(head) 1");
            worksheet.Cell(1, 11).SetValue("Ref.key(head) 2");
            worksheet.Cell(1, 12).SetValue("Number of pages of invoice");
            worksheet.Cell(1, 13).SetValue("Account typeG/D/K");
            worksheet.Cell(1, 14).SetValue("S/H");
            worksheet.Cell(1, 15).SetValue("PK");
            worksheet.Cell(1, 16).SetValue("Account");
            worksheet.Cell(1, 17).SetValue("SGL Ind.");
            worksheet.Cell(1, 18).SetValue("G/L account ");
            worksheet.Cell(1, 19).SetValue("Alternative reconciliation account");
            worksheet.Cell(1, 20).SetValue("Cost center");
            worksheet.Cell(1, 21).SetValue("Profit center");
            worksheet.Cell(1, 22).SetValue("Internal order");
            worksheet.Cell(1, 23).SetValue("Reason code");
            worksheet.Cell(1, 24).SetValue("Amount in DC");
            worksheet.Cell(1, 25).SetValue("Amount in LC");
            worksheet.Cell(1, 26).SetValue("Tax Code");
            worksheet.Cell(1, 27).SetValue("Base Amount");
            worksheet.Cell(1, 28).SetValue("text");
            worksheet.Cell(1, 29).SetValue("BASELINE DATE");
            worksheet.Cell(1, 30).SetValue("Assgienment");
            worksheet.Cell(1, 31).SetValue("Reference key1");
            worksheet.Cell(1, 32).SetValue("Reference key2");
            worksheet.Cell(1, 33).SetValue("Reference key3");
            worksheet.Cell(1, 34).SetValue("Payment term");
            worksheet.Cell(1, 35).SetValue("Payment method.");
            worksheet.Cell(1, 36).SetValue("Business Place");
            worksheet.Cell(1, 37).SetValue("Section Code");
            worksheet.Cell(1, 38).SetValue("Quantity");
            worksheet.Cell(1, 39).SetValue("Unit");
            worksheet.Cell(1, 40).SetValue("value Date");
            worksheet.Range("A1:AN1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range("A1:AN1").Style.Font.SetBold();
            worksheet.Range("A1:AN1").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");

            foreach (var value in SaleRebateTemplate)
            {
                var Date = DateTimeSystem.ToDateTime($"{value.Month!}01");
                var firstDayOfMonth = new DateTime(Date.Year, Date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                for (int i = 2; i <= 5; i++)
                {
                    worksheet.Cell(i, 1).SetValue("1");
                    worksheet.Cell(i, 2).SetValue("9770");
                    worksheet.Cell(i, 3).SetValue(lastDayOfMonth.ToString("yyyyMMdd")); //
                    worksheet.Cell(i, 4).SetValue(lastDayOfMonth.ToString("yyyyMMdd")); //
                    worksheet.Cell(i, 5).SetValue("SA");
                    worksheet.Cell(i, 6).SetValue($"REBATE{value.Month}");
                    worksheet.Cell(i, 7).SetValue("THB");
                    worksheet.Cell(i, 8).SetValue(value.Month);
                    worksheet.Cell(i, 13).SetValue("G");
                }
                for (int i = 2; i <= 4; i++)
                {
                    worksheet.Cell(i, 14).SetValue("S");
                    worksheet.Cell(5, 14).SetValue("H");
                    worksheet.Cell(i, 15).SetValue("40");
                    worksheet.Cell(5, 15).SetValue("50");
                }
                worksheet.Cell(2, 16).SetValue("5101030999");
                worksheet.Cell(3, 16).SetValue("5101030999");
                worksheet.Cell(4, 16).SetValue("6666092200");
                worksheet.Cell(5, 16).SetValue("2191000001");
                worksheet.Cell(4, 20).SetValue("6977021601");
                worksheet.Cell(2, 21).SetValue("9770010000");
                worksheet.Cell(3, 21).SetValue("9770050000");
                worksheet.Cell(4, 21).SetValue("9770010000");
                worksheet.Cell(2, 24).SetValue(value.RFPromotionAmt);
                worksheet.Cell(3, 24).SetValue(value.ACPromotionAmt);
                worksheet.Cell(4, 24).SetValue(value.RFRoyaltyAmt);
                worksheet.Cell(5, 24).SetValue(value.TotalPromotionAmt + value.RFRoyaltyAmt);

                worksheet.Cell(2, 28).SetValue($"SET SALES PRO SINGER RF {Date.ToString("MMM").ToUpper()} {Date.ToString("yyyy")}");
                worksheet.Cell(3, 28).SetValue($"SET SALES PRO SINGER AC {Date.ToString("MMM").ToUpper()} {Date.ToString("yyyy")}");
                worksheet.Cell(4, 28).SetValue($"SET ROYALTY SINGER RF {Date.ToString("MMM").ToUpper()} {Date.ToString("yyyy")}");
                worksheet.Cell(5, 28).SetValue($"SET SALES PRO & ROYALTY SINGER THAI {Date.ToString("MMM").ToUpper()} {Date.ToString("yyyy")}");
            }
            worksheet.Range("X2:X5").Style.NumberFormat.Format = "#,##0.00";
            worksheet.Columns("A:AN").AdjustToContents();

            // Save file
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
    }
}