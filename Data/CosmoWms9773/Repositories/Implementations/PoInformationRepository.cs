using Microsoft.EntityFrameworkCore;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Data.CosmoWms9773.Repositories.Interfaces;
using WebApi.Models.ProductionOrder;

namespace WebApi.Data.CosmoWms9773.Repositories.Implementations
{
    public class PoInformationRepository : BaseRepository<PoInformation>, IPoInformationRepository
    {
        public PoInformationRepository(CosmoWms9773 db) : base(db)
        {
        }
        public async Task<List<PoInformation>> GetPoInformationFilterAsync(PoInformationParameter Parameter)
        {
            // string poNo = "";
            // string iTem = "";
            string deliveryDate = "";
            // string receiptState = "";

            // if (Parameter.PoNo != null)
            // {
            //     poNo = $" po.po_no = '{Parameter.PoNo}'";
            // }
            // if (Parameter.Item != null)
            // {
            //     iTem = $" po.po_line = '{Parameter.Item}'";
            // }
            if (Parameter.StartDate != null && Parameter.EndDate != null)
            {
                deliveryDate = $"WHERE DATE_FORMAT(po.receipt_time,'%Y-%m-%d') BETWEEN '{Parameter.StartDate}' AND '{Parameter.EndDate}'";
            }
            // if (Parameter.ReceiptState != null)
            // {
            //     if (Parameter.ReceiptState.ToUpper() == "New".ToUpper())
            //     {
            //         receiptState = $" po.receipt_amount IS NULL OR po.receipt_amount = 0";
            //     }
            //     if (Parameter.ReceiptState.ToUpper() == "Receiving".ToUpper())
            //     {
            //         receiptState = $" po.receipt_amount > 0 AND po.receipt_amount < po.po_amount";
            //     }
            //     if (Parameter.ReceiptState.ToUpper() == "Completed".ToUpper())
            //     {
            //         receiptState = $" po.receipt_amount = po.po_amount";
            //     }
            // }

            var query = _dbcontext.PoInformation
            .FromSqlRaw<PoInformation>
            (@$"
                SELECT 
                    po.po_no AS 'PO_NO', 
                    po.po_line AS 'ITEM', 
                    po.po_type AS 'PO_TYPE', 
                    CASE 
                        WHEN po.po_type = 'ZT02' THEN 'HTC Domestic' 
                        WHEN po.po_type = 'ZT04' THEN 'HTC Free' 
                        WHEN po.po_type = 'ZT05' THEN 'HTC Purchase From China' 
                        WHEN po.po_type = 'ZT06' THEN 'HTC Import' 
                        WHEN po.po_type = 'ZT09' THEN 'HTC Subcontract' 
                        WHEN po.po_type = 'ZT11' THEN 'HTC Spare Part' 
                        ELSE '...' 
                    END AS 'TYPE_DESC', 
                    po.material_code AS 'MATERIAL', 
                    po.material_desc AS 'DESCRIPTION', 
                    po.unit AS 'UNIT', 
                    po.po_amount AS 'REQUEST_QTY', 
                    IFNULL(po.receipt_amount, 0) AS 'RECEIPT_QTY', 
                    IFNULL(po.po_amount - po.receipt_amount, 0) AS 'DIFFERENCE_QTY', 
                    IFNULL(dn.request_amount, 0) AS 'CREATE_QTY', 
                    CASE 
                        WHEN po.receipt_amount IS NULL OR po.receipt_amount = 0 THEN 'New' 
                        WHEN po.receipt_amount > 0 AND po.receipt_amount < po.po_amount THEN 'Receiving' 
                        WHEN po.receipt_amount = po.po_amount THEN 'Completed' 
                        ELSE '...' 
                    END AS 'RECEIPT_STATE', 
                    DATE_FORMAT(po.receipt_time,'%Y-%m-%d') AS 'DELIVERY_DATE', 
                    po.factory_code AS 'PLANT', 
                    po.supply_code AS 'SUPPLIER_ID', 
                    po.supply_name AS 'SUPPLIER_NAME', 
                    convert(po.price, DECIMAL(20,2)) AS 'PRICE',
                    po.waers AS 'CURRENCY', 
                    CASE 
                        WHEN po.line_type = 0 THEN 'Standard' 
                        WHEN po.line_type = 3 THEN 'Subcontract' 
                    END AS 'ITEM_TYPE', 
                    po.gmt_create AS 'CREATE_DATE', 
                    po.gmt_modified AS 'LAST_UPDATE' 
                FROM cosmo_wms_9773.ods_raw_order_po po 
                LEFT JOIN (
                    SELECT po_no, po_line, SUM(request_amount) AS request_amount 
                    FROM cosmo_wms_9773.ods_raw_order_in 
                    WHERE return_status = '0' 
                    GROUP BY po_no, po_line
                ) AS dn ON po.po_no = dn.po_no AND po.po_line = dn.po_line 
                    {deliveryDate}
                GROUP BY po.po_no, po.po_line 
                ORDER BY po.po_no, po.po_line
            ");
            
            var results = await query.ToListAsync();

            if (Parameter.PoNo != null)
            {
                results = results.Where(i => i.PoNo == Parameter.PoNo).ToList();
            }
            if (Parameter.Item != null)
            {
                results = results.Where(i => i.Item == Parameter.Item).ToList();
            }
            if (Parameter.ReceiptState != null)
            {
                results = results.Where(i => i.ReceiptState!.ToUpper() == Parameter.ReceiptState.ToUpper()).ToList();
            }

            return results;
        }
    }
}