using ClosedXML.Excel;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Models.ProductionOrder;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class ProductionOrderService : IProductionOrderService
    {
        private readonly ICosmoIm9773UniOfWork _cosmoIm9773UniOfWork;
        private readonly ICosmoWms9773UniOfWork _cosmoWms9773UniOfWork;

        public ProductionOrderService(
            ICosmoIm9773UniOfWork cosmoIm9773UniOfWork,
            ICosmoWms9773UniOfWork cosmoWms9773UniOfWork
        )
        {
            _cosmoIm9773UniOfWork = cosmoIm9773UniOfWork;
            _cosmoWms9773UniOfWork = cosmoWms9773UniOfWork;
        }

        public async Task<List<FGProductionOrder>> GetFGProductionOrderFilterAsync(ProductionOrderParameter Parameter)
        {
            List<FGProductionOrder> FGProductionOrder = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetFGProductionOrderFilterAsync(Parameter);
            return FGProductionOrder;
        }
        public async Task<List<SFGProductionOrder>> GetSFGProductionOrderFilterAsync(ProductionOrderParameter Parameter)
        {
            List<SFGProductionOrder> SFGProductionOrder = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetSFGProductionOrderFilterAsync(Parameter);
            return SFGProductionOrder;
        }
        public async Task<byte[]> GetFGProductionOrderForExcel(ProductionOrderParameter Parameter)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("FG Production Order");
            var row = 1;
            worksheet.Cell(row, 1).SetValue("Plant").Style.Font.SetBold();
            worksheet.Cell(row, 2).SetValue("Order No").Style.Font.SetBold();
            worksheet.Cell(row, 3).SetValue("Start Date").Style.Font.SetBold();
            worksheet.Cell(row, 4).SetValue("Material").Style.Font.SetBold();
            worksheet.Cell(row, 5).SetValue("Description").Style.Font.SetBold();
            worksheet.Cell(row, 6).SetValue("Require Qty").Style.Font.SetBold();
            worksheet.Cell(row, 7).SetValue("Product Qty").Style.Font.SetBold();
            worksheet.Cell(row, 8).SetValue("Different Qty").Style.Font.SetBold();
            worksheet.Cell(row, 9).SetValue("Order Type").Style.Font.SetBold();
            worksheet.Cell(row, 10).SetValue("Send SAP Qty").Style.Font.SetBold();
            worksheet.Cell(row, 11).SetValue("Unit").Style.Font.SetBold();
            worksheet.Cell(row, 12).SetValue("Create Date").Style.Font.SetBold();
            worksheet.Cell(row, 13).SetValue("Production Version").Style.Font.SetBold();
            worksheet.Cell(row, 14).SetValue("Line Code").Style.Font.SetBold();
            var values = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetFGProductionOrderFilterAsync(Parameter);
            foreach (var item in values)
            {
                row++;
                worksheet.Cell(row, 1).SetValue(item.Plant);
                worksheet.Cell(row, 2).SetValue(item.OrderNo);
                worksheet.Cell(row, 3).SetValue(item.StartDate);
                worksheet.Cell(row, 4).SetValue(item.Material);
                worksheet.Cell(row, 5).SetValue(item.Description);
                worksheet.Cell(row, 6).SetValue(item.RequireQty);
                worksheet.Cell(row, 7).SetValue(item.ProductQty);
                worksheet.Cell(row, 8).SetValue(item.DifferentQty);
                worksheet.Cell(row, 9).SetValue(item.OrderType);
                worksheet.Cell(row, 10).SetValue(item.SendSapQty);
                worksheet.Cell(row, 11).SetValue(item.Unit);
                worksheet.Cell(row, 12).SetValue(item.CreateDate);
                worksheet.Cell(row, 13).SetValue(item.ProductionVersion);
                worksheet.Cell(row, 14).SetValue(item.LineCode);
            }
            worksheet.Columns("A:N").AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }

        public async Task<byte[]> GetSFGProductionOrderForExcel(ProductionOrderParameter Parameter)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("SFG Production Order");
            var row = 1;
            worksheet.Cell(row, 1).SetValue("Plant").Style.Font.SetBold();
            worksheet.Cell(row, 2).SetValue("Order No").Style.Font.SetBold();
            worksheet.Cell(row, 3).SetValue("Start Date").Style.Font.SetBold();
            worksheet.Cell(row, 4).SetValue("Material").Style.Font.SetBold();
            worksheet.Cell(row, 5).SetValue("Description").Style.Font.SetBold();
            worksheet.Cell(row, 6).SetValue("Require Qty").Style.Font.SetBold();
            worksheet.Cell(row, 7).SetValue("Product Qty").Style.Font.SetBold();
            worksheet.Cell(row, 8).SetValue("Different Qty").Style.Font.SetBold();
            worksheet.Cell(row, 9).SetValue("Order Type").Style.Font.SetBold();
            worksheet.Cell(row, 10).SetValue("Send SAP Qty").Style.Font.SetBold();
            worksheet.Cell(row, 11).SetValue("Unit").Style.Font.SetBold();
            worksheet.Cell(row, 12).SetValue("Create Date").Style.Font.SetBold();
            worksheet.Cell(row, 13).SetValue("Production Version").Style.Font.SetBold();
            worksheet.Cell(row, 14).SetValue("Line Code").Style.Font.SetBold();
            var values = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetSFGProductionOrderFilterAsync(Parameter);
            foreach (var item in values)
            {
                row++;
                worksheet.Cell(row, 1).SetValue(item.Plant);
                worksheet.Cell(row, 2).SetValue(item.OrderNo);
                worksheet.Cell(row, 3).SetValue(item.StartDate);
                worksheet.Cell(row, 4).SetValue(item.Material);
                worksheet.Cell(row, 5).SetValue(item.Description);
                worksheet.Cell(row, 6).SetValue(item.RequireQty);
                worksheet.Cell(row, 7).SetValue(item.ProductQty);
                worksheet.Cell(row, 8).SetValue(item.DifferentQty);
                worksheet.Cell(row, 9).SetValue(item.OrderType);
                worksheet.Cell(row, 10).SetValue(item.SendSapQty);
                worksheet.Cell(row, 11).SetValue(item.Unit);
                worksheet.Cell(row, 12).SetValue(item.CreateDate);
                worksheet.Cell(row, 13).SetValue(item.ProductionVersion);
                worksheet.Cell(row, 14).SetValue(item.LineCode);
            }
            worksheet.Columns("A:N").AdjustToContents();

            // Save file
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }

        public async Task<List<FGOffline>> GetFGOfflineFilterAsync(OfflineParameter Parameter)
        {
            List<FGOffline> FGOffline = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetFGOfflineFilterAsync(Parameter);
            return FGOffline;
        }
        public async Task<List<SFGOffline>> GetSFGOfflineFilterAsync(OfflineParameter Parameter)
        {
            List<SFGOffline> SFGOffline = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetSFGOfflineFilterAsync(Parameter);
            return SFGOffline;
        }
        public async Task<List<OfflineSummarize>> GetOfflineSummarizeFilterAsync(string OrderNo)
        {
            List<OfflineSummarize> OfflineSummarize = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetOfflineSummarizeFilterAsync(OrderNo);
            return OfflineSummarize;
        }

        public async Task<byte[]> GetFGOfflineForExcel(OfflineParameter Parameter)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("FG Offline");
            var row = 1;
            worksheet.Cell(row, 1).SetValue("Barcode").Style.Font.SetBold();
            worksheet.Cell(row, 2).SetValue("Material").Style.Font.SetBold();
            worksheet.Cell(row, 3).SetValue("Description").Style.Font.SetBold();
            worksheet.Cell(row, 4).SetValue("Order No.").Style.Font.SetBold();
            worksheet.Cell(row, 5).SetValue("Qty").Style.Font.SetBold();
            worksheet.Cell(row, 6).SetValue("Unit").Style.Font.SetBold();
            worksheet.Cell(row, 7).SetValue("Prod Version").Style.Font.SetBold();
            worksheet.Cell(row, 8).SetValue("Offline By").Style.Font.SetBold();
            worksheet.Cell(row, 9).SetValue("Offline Date").Style.Font.SetBold();
            worksheet.Cell(row, 10).SetValue("Plant").Style.Font.SetBold();
            worksheet.Cell(row, 11).SetValue("Line Code").Style.Font.SetBold();
            var values = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetFGOfflineFilterAsync(Parameter);
            foreach (var item in values)
            {
                row++;
                worksheet.Cell(row, 1).SetValue(item.Barcode);
                worksheet.Cell(row, 2).SetValue(item.Material);
                worksheet.Cell(row, 3).SetValue(item.Description);
                worksheet.Cell(row, 4).SetValue(item.OrderNo);
                worksheet.Cell(row, 5).SetValue(item.Qty);
                worksheet.Cell(row, 6).SetValue(item.Unit);
                worksheet.Cell(row, 7).SetValue(item.ProdVersion);
                worksheet.Cell(row, 8).SetValue(item.OfflineBy);
                worksheet.Cell(row, 9).SetValue(item.OfflineDate);
                worksheet.Cell(row, 10).SetValue(item.Plant);
                worksheet.Cell(row, 11).SetValue(item.LineCode);
            }
            worksheet.Columns("A:K").AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
        public async Task<byte[]> GetSFGOfflineForExcel(OfflineParameter Parameter)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("SFG Offline");
            var row = 1;
            worksheet.Cell(row, 1).SetValue("Barcode").Style.Font.SetBold();
            worksheet.Cell(row, 2).SetValue("Material").Style.Font.SetBold();
            worksheet.Cell(row, 3).SetValue("Description").Style.Font.SetBold();
            worksheet.Cell(row, 4).SetValue("Order No.").Style.Font.SetBold();
            worksheet.Cell(row, 5).SetValue("Qty").Style.Font.SetBold();
            worksheet.Cell(row, 6).SetValue("Unit").Style.Font.SetBold();
            worksheet.Cell(row, 7).SetValue("Prod Version").Style.Font.SetBold();
            worksheet.Cell(row, 8).SetValue("Offline By").Style.Font.SetBold();
            worksheet.Cell(row, 9).SetValue("Offline Date").Style.Font.SetBold();
            worksheet.Cell(row, 10).SetValue("Plant").Style.Font.SetBold();
            worksheet.Cell(row, 11).SetValue("Line Code").Style.Font.SetBold();
            var values = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetSFGOfflineFilterAsync(Parameter);
            foreach (var item in values)
            {
                row++;
                worksheet.Cell(row, 1).SetValue(item.Barcode);
                worksheet.Cell(row, 2).SetValue(item.Material);
                worksheet.Cell(row, 3).SetValue(item.Description);
                worksheet.Cell(row, 4).SetValue(item.OrderNo);
                worksheet.Cell(row, 5).SetValue(item.Qty);
                worksheet.Cell(row, 6).SetValue(item.Unit);
                worksheet.Cell(row, 7).SetValue(item.ProdVersion);
                worksheet.Cell(row, 8).SetValue(item.OfflineBy);
                worksheet.Cell(row, 9).SetValue(item.OfflineDate);
                worksheet.Cell(row, 10).SetValue(item.Plant);
                worksheet.Cell(row, 11).SetValue(item.LineCode);
            }
            worksheet.Columns("A:K").AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
        public async Task<byte[]> GetOfflineSummarizeForExcel(string OrderNo)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Offline Summarize");
            var row = 1;
            worksheet.Cell(row, 1).SetValue("Order No.").Style.Font.SetBold();
            worksheet.Cell(row, 2).SetValue("Material").Style.Font.SetBold();
            worksheet.Cell(row, 3).SetValue("Description").Style.Font.SetBold();
            worksheet.Cell(row, 4).SetValue("Quantity").Style.Font.SetBold();
            worksheet.Cell(row, 5).SetValue("Unit").Style.Font.SetBold();
            worksheet.Cell(row, 6).SetValue("Offline Date").Style.Font.SetBold();
            worksheet.Cell(row, 7).SetValue("SAP Flag").Style.Font.SetBold();
            worksheet.Cell(row, 8).SetValue("SAP Message").Style.Font.SetBold();
            worksheet.Cell(row, 9).SetValue("SAP Up Time").Style.Font.SetBold();
            worksheet.Cell(row, 10).SetValue("SAP Up Num").Style.Font.SetBold();
            worksheet.Cell(row, 11).SetValue("Plant").Style.Font.SetBold();

            var values = await _cosmoIm9773UniOfWork.ProductionOrderRepository.GetOfflineSummarizeFilterAsync(OrderNo);
            foreach (var item in values)
            {
                row++;
                worksheet.Cell(row, 1).SetValue(item.OrderNo);
                worksheet.Cell(row, 2).SetValue(item.Material);
                worksheet.Cell(row, 3).SetValue(item.Description);
                worksheet.Cell(row, 4).SetValue(item.Quantity);
                worksheet.Cell(row, 5).SetValue(item.Unit);
                worksheet.Cell(row, 6).SetValue(item.OfflineDate);
                worksheet.Cell(row, 7).SetValue(item.SapFlag);
                worksheet.Cell(row, 8).SetValue(item.SapMessage);
                worksheet.Cell(row, 9).SetValue(item.SapUpTime);
                worksheet.Cell(row, 10).SetValue(item.SapUpNum);
                worksheet.Cell(row, 11).SetValue(item.Plant);

            }

            worksheet.Columns("A:K").AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }



        // PO Information
        public async Task<List<PoInformation>> GetPoInformationFilterAsync(PoInformationParameter Parameter)
        {
            List<PoInformation> PoInformation = await _cosmoWms9773UniOfWork.PoInformationRepository.GetPoInformationFilterAsync(Parameter);
            return PoInformation;
        }
        public async Task<byte[]> GetPoInformationForExcel (PoInformationParameter Parameter)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Offline Summarize");
            var row = 1;
            worksheet.Cell(row, 1).SetValue("PO_NO").Style.Font.SetBold();
            worksheet.Cell(row, 2).SetValue("ITEM").Style.Font.SetBold();
            worksheet.Cell(row, 3).SetValue("PO_TYPE").Style.Font.SetBold();
            worksheet.Cell(row, 4).SetValue("TYPE_DESC").Style.Font.SetBold();
            worksheet.Cell(row, 5).SetValue("MATERIAL").Style.Font.SetBold();
            worksheet.Cell(row, 6).SetValue("DESCRIPTION").Style.Font.SetBold();
            worksheet.Cell(row, 7).SetValue("UNIT").Style.Font.SetBold();
            worksheet.Cell(row, 8).SetValue("REQUEST_QTY").Style.Font.SetBold();
            worksheet.Cell(row, 9).SetValue("RECEIPT_QTY").Style.Font.SetBold();
            worksheet.Cell(row, 10).SetValue("DIFFERENCE_QTY").Style.Font.SetBold();
            worksheet.Cell(row, 11).SetValue("CREATE_QTY").Style.Font.SetBold();
            worksheet.Cell(row, 12).SetValue("RECEIPT_STATE").Style.Font.SetBold();
            worksheet.Cell(row, 13).SetValue("DELIVERY_DATE").Style.Font.SetBold();
            worksheet.Cell(row, 14).SetValue("PLANT").Style.Font.SetBold();
            worksheet.Cell(row, 15).SetValue("SUPPLIER_ID").Style.Font.SetBold();
            worksheet.Cell(row, 16).SetValue("SUPPLIER_NAME").Style.Font.SetBold();
            worksheet.Cell(row, 17).SetValue("PRICE").Style.Font.SetBold();
            worksheet.Cell(row, 18).SetValue("CURRENCY").Style.Font.SetBold();
            worksheet.Cell(row, 19).SetValue("ITEM_TYPE").Style.Font.SetBold();
            worksheet.Cell(row, 20).SetValue("CREATE_DATE").Style.Font.SetBold();
            worksheet.Cell(row, 21).SetValue("LAST_UPDATE").Style.Font.SetBold();
            var values = await _cosmoWms9773UniOfWork.PoInformationRepository.GetPoInformationFilterAsync(Parameter);
            foreach (var item in values)
            {
                row++;
                worksheet.Cell(row, 1).SetValue(item.PoNo);
                worksheet.Cell(row, 2).SetValue(item.Item);
                worksheet.Cell(row, 3).SetValue(item.PoType);
                worksheet.Cell(row, 4).SetValue(item.TypeDesc);
                worksheet.Cell(row, 5).SetValue(item.Material);
                worksheet.Cell(row, 6).SetValue(item.Description);
                worksheet.Cell(row, 7).SetValue(item.Unit);
                worksheet.Cell(row, 8).SetValue(item.RequestQty);
                worksheet.Cell(row, 9).SetValue(item.ReceiptQty);
                worksheet.Cell(row, 10).SetValue(item.DifferenceQty);
                worksheet.Cell(row, 11).SetValue(item.CreateQty);
                worksheet.Cell(row, 12).SetValue(item.ReceiptState);
                worksheet.Cell(row, 13).SetValue(item.DeliveryDate);
                worksheet.Cell(row, 14).SetValue(item.Plant);
                worksheet.Cell(row, 15).SetValue(item.SupplierId);
                worksheet.Cell(row, 16).SetValue(item.SupplierName);
                worksheet.Cell(row, 17).SetValue(item.Price);
                worksheet.Cell(row, 18).SetValue(item.Currency);
                worksheet.Cell(row, 19).SetValue(item.ItemType);
                worksheet.Cell(row, 20).SetValue(item.CreateDate);
                worksheet.Cell(row, 21).SetValue(item.LastUpdate);
            }
            
            worksheet.Columns("A:U").AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
    }
}