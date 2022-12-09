using Microsoft.EntityFrameworkCore;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Data.CosmoIm9773.Repositories.Interfaces;
using WebApi.Models.ProductionOrder;

namespace WebApi.Data.CosmoIm9773.Repositories.Implementations
{
    public class ProductionOrderRepository : BaseRepository<FGProductionOrder>, IProductionOrderRepository
    {
        public ProductionOrderRepository(CosmoIm9773 db) : base(db)
        {
        }

        public async Task<List<FGProductionOrder>> GetFGProductionOrderFilterAsync(ProductionOrderParameter Parameter)
        {
            string orderNo = "";
            if (Parameter.OrderNo != null)
            {
                orderNo = $"AND a.Code = '{Parameter.OrderNo}'";
            }
            string lineCode = "";
            if (Parameter.LineCode != null)
            {
                lineCode = $"AND a.LineCode = '{Parameter.LineCode.ToUpper()}'";
            }
            string startDate = "";
            if (Parameter.StartDate != null && Parameter.EndDate != null)
            {
                startDate = $"AND DATE_FORMAT(a.EST,'%Y-%m-%d') BETWEEN '{Parameter.StartDate}' AND '{Parameter.EndDate}'";
            }
            string material = "";
            if (Parameter.Material != null)
            {
                material = $"AND a.prod_code = '{Parameter.Material.ToUpper()}'";
            }
            string description = "";
            if (Parameter.Description != null)
            {
                description = $"AND a.prod_desc  LIKE  '%{Parameter.Description.ToUpper()}%'";
            }

            var query = _dbcontext.FGProductionOrder
            .FromSqlRaw<FGProductionOrder>
            ($@"
                SELECT 
                    a.Site_code AS 'Plant',
                	a.Code AS 'Order No.',
                    DATE_FORMAT(a.EST, '%Y-%m-%d') AS 'Start Date',
                    a.prod_code AS 'Material',
                    a.prod_desc AS 'Description',
                    a.Quantity AS 'Require Qty',
                    COUNT(b.WorkUser_Barcode) AS 'Product Qty',
                    a.Quantity - COUNT(b.WorkUser_Barcode) AS 'Different Qty',
                    a.ProdPlanType AS 'Order Type',
                    CASE WHEN c.finAmount IS NULL THEN 0 ELSE c.finAmount END AS 'Send SAP Qty',
                    a.EU AS 'Unit',
                    a.CreateDate AS 'Create Date',
                    a.Edition AS 'Production Version',
                    a.LineCode AS 'Line Code'
                FROM cosmo_im_9773.bns_pm_productionorder a 
                LEFT JOIN cosmo_im_9773.bns_pm_scanhistory_month b ON a.Code = b.Code AND a.Site_code = b.Site_code 
                LEFT JOIN (
                	        SELECT Factory, orderid, SUM(finAmount) AS finAmount 
                            FROM cosmo_im_9773.bns_pm_wmswork 
                            WHERE ZTYPE = 'I' 
                            GROUP BY Factory, orderid 
                          ) c ON a.Code = c.orderid AND a.Site_code = c.Factory
                WHERE a.LineCode IN ('W1', 'W2') AND b.type = '1'
	                    {orderNo}
	                    {lineCode}
	                    {startDate}
	                    {material}
	                    {description}
                GROUP BY a.Code 
                ORDER BY a.EST; 
            ");

            var results = await query.ToListAsync();

            // if (Parameter.OrderNo != null)
            // {
            //     results = results.Where(v => v.OrderNo == Parameter.OrderNo).ToList();
            // }
            // if (Parameter.LineCode != null)
            // {
            //     results = results.Where(v => v.LineCode == Parameter.LineCode.ToUpper()).ToList();
            // }
            // if (Parameter.StartDate != null && Parameter.EndDate != null)
            // {
            //     results = results.Where(v => v.StartDate >= Parameter.StartDate && v.StartDate <= Parameter.EndDate).ToList();
            // }
            // if (Parameter.Material != null)
            // {
            //     results = results.Where(v => v.Material == Parameter.Material.ToUpper()).ToList();
            // }
            // if (Parameter.Description != null)
            // {
            //     results = results.Where(v => v.Description!.Contains(Parameter.Description.ToUpper())).ToList();
            // }

            return results;
        }

        public async Task<List<SFGProductionOrder>> GetSFGProductionOrderFilterAsync(ProductionOrderParameter Parameter)
        {
            string orderNo = "";
            if (Parameter.OrderNo != null)
            {
                orderNo = $"AND a.Code = '{Parameter.OrderNo}'";
            }
            string lineCode = "";
            if (Parameter.LineCode != null)
            {
                lineCode = $"AND a.LineCode = '{Parameter.LineCode.ToUpper()}'";
            }
            string startDate = "";
            if (Parameter.StartDate != null && Parameter.EndDate != null)
            {
                startDate = $"AND DATE_FORMAT(a.EST,'%Y-%m-%d') BETWEEN '{Parameter.StartDate}' AND '{Parameter.EndDate}'";
            }
            string material = "";
            if (Parameter.Material != null)
            {
                material = $"AND a.prod_code = '{Parameter.Material.ToUpper()}'";
            }
            string description = "";
            if (Parameter.Description != null)
            {
                description = $"AND a.prod_desc  LIKE  '%{Parameter.Description.ToUpper()}%'";
            }

            var query = _dbcontext.SFGProductionOrder
            .FromSqlRaw<SFGProductionOrder>
            ($@"
                SELECT a.Site_code AS 'Plant',
                	a.Code AS 'Order No.',
                    DATE_FORMAT(a.EST, '%Y-%m-%d') AS 'Start Date',
                    a.prod_code AS 'Material',
                    a.prod_desc AS 'Description',
                    a.Quantity AS 'Require Qty',
                    SUM(b.Offline_Num) AS 'Product Qty',
                    a.Quantity - SUM(b.Offline_Num) AS 'Different Qty',
                    a.ProdPlanType AS 'Order Type',
                    CASE WHEN c.finAmount IS NULL THEN 0 ELSE c.finAmount END AS 'Send SAP Qty',
                    a.EU AS 'Unit',
                    a.CreateDate AS 'Create Date',
                    a.Edition AS 'Production Version',
                    a.LineCode AS 'Line Code'
                FROM cosmo_im_9773.bns_pm_productionorder a 
                LEFT JOIN cosmo_im_9773.bns_pm_semioffline b ON a.Code = b.WorkUser_MOrderCode AND a.Site_code = b.site_code 
                LEFT JOIN (
                	        SELECT Factory, orderid, SUM(finAmount) AS finAmount 
                            FROM cosmo_im_9773.bns_pm_wmswork 
                            WHERE ZTYPE = 'I' 
                            GROUP BY Factory, orderid 
                          ) c ON a.Code = c.orderid AND a.Site_code = c.Factory 
                WHERE a.LineCode NOT IN ('W1', 'W2') AND b.type = '1'
	                    {orderNo}
	                    {lineCode}
	                    {startDate}
	                    {material}
	                    {description}
                GROUP BY a.Code 
                ORDER BY a.EST; 
            ");

            var results = await query.ToListAsync();

            // if (Parameter.OrderNo != null)
            // {
            //     results = results.Where(v => v.OrderNo == Parameter.OrderNo).ToList();
            // }
            // if (Parameter.LineCode != null)
            // {
            //     results = results.Where(v => v.LineCode == Parameter.LineCode.ToUpper()).ToList();
            // }
            // if (Parameter.StartDate != null && Parameter.EndDate != null)
            // {
            //     results = results.Where(v => v.StartDate >= Parameter.StartDate && v.StartDate <= Parameter.EndDate).ToList();
            // }
            // if (Parameter.Material != null)
            // {
            //     results = results.Where(v => v.Material == Parameter.Material.ToUpper()).ToList();
            // }
            // if (Parameter.Description != null)
            // {
            //     results = results.Where(v => v.Description!.Contains(Parameter.Description.ToUpper())).ToList();
            // }

            return results;
        }

        public async Task<List<FGOffline>> GetFGOfflineFilterAsync(OfflineParameter Parameter)
        {
            string lineCode = "";
            if (Parameter.LineCode != null)
            {
                lineCode = $"AND b.LineCode = '{Parameter.LineCode}'";
            }
            string orderNo = "";
            if (Parameter.OrderNo != null)
            {
                orderNo = $"AND a.Code = '{Parameter.OrderNo}'";
            }
            string offlineDate = "";
            if (Parameter.StartDate != null && Parameter.EndDate != null)
            {
                offlineDate = $"AND DATE_FORMAT(a.Create_Date,'%Y-%m-%d') BETWEEN '{Parameter.StartDate}' AND '{Parameter.EndDate}'";
            }

            var query = _dbcontext.FGOffline
            .FromSqlRaw<FGOffline>
            (@$"
                select
                    a.WorkUser_BarCode as 'barcode',
                    a.Prod_Code as 'material',
                    b.prod_desc as 'description',
                    a.Code as 'order_no',
                    '1' as 'qty',
                    b.EU as 'unit',
                    b.Edition as 'prod_version',
                    a.Create_By as 'offline_by',
                    a.Create_Date as 'offline_date',
                    a.Site_Code as 'plant',
                    b.LineCode as 'line_code'
                from
                    cosmo_im_9773.bns_pm_scanhistory_month a
                left join cosmo_im_9773.bns_pm_productionorder b on
                    a.Code = b.Code
                    and a.Prod_Code = b.prod_code
                where
                    a.type = '1'
                    {lineCode}
                    {orderNo}
                    {offlineDate}
                ORDER BY a.Create_Date DESC
            ");

            var results = await query.ToListAsync();

            return results;
        }

        public async Task<List<SFGOffline>> GetSFGOfflineFilterAsync(OfflineParameter Parameter)
        {
            string lineCode = "";
            if (Parameter.LineCode != null)
            {
                lineCode = $"AND b.LineCode = '{Parameter.LineCode}'";
            }
            string orderNo = "";
            if (Parameter.OrderNo != null)
            {
                orderNo = $"AND a.WorkUser_MOrderCode = '{Parameter.OrderNo}'";
            }
            string offlineDate = "";
            if (Parameter.StartDate != null && Parameter.EndDate != null)
            {
                offlineDate = $"AND DATE_FORMAT(a.Create_Date,'%Y-%m-%d') BETWEEN '{Parameter.StartDate}' AND '{Parameter.EndDate}'";
            }

            var query = _dbcontext.SFGOffline
            .FromSqlRaw<SFGOffline>
            (@$"
                select
                    a.WorkUser_BarCode as 'barcode',
                    a.Prod_Code as 'material',
                    a.Prod_Desc as 'description',
                    a.WorkUser_MOrderCode as 'order_no',
                    a.offline_num as 'qty',
                    b.EU as 'unit',
                    b.Edition as 'prod_version',
                    a.Create_By as 'offline_by',
                    a.Create_Date as 'offline_date',
                    a.Site_Code as 'plant',
                    b.LineCode as 'line_code'
                from
                    cosmo_im_9773.bns_pm_semioffline a
                left join cosmo_im_9773.bns_pm_productionorder b on
                    a.WorkUser_MOrderCode = b.Code
                    and a.Prod_Code = b.prod_code
                where
                    a.type = '1'
                    {lineCode}
                    {orderNo}
                    {offlineDate}
                ORDER BY a.Create_Date DESC
            ");

            var results = await query.ToListAsync();

            return results;
        }

        public async Task<List<OfflineSummarize>> GetOfflineSummarizeFilterAsync(string OrderNo)
        {
            var query = await _dbcontext.OfflineSummarize
            .FromSqlRaw<OfflineSummarize>
            ($@"
                select
                    a.orderid as 'order_no',
                    a.zyh as 'material',
                    b.prod_desc as 'description',
                    a.finamount as 'quantity',
                    b.eu as 'unit',
                    a.create_date as 'offline_date',
                    a.ztype as 'sap_flag',
                    a.message as 'sap_message',
                    a.last_update_date as 'sap_up_time',
                    a.execute_time as 'sap_up_num',
                    a.site_code as 'plant' 
                from cosmo_im_9773.bns_pm_wmswork a 
                left join cosmo_im_9773.bns_pm_productionorder b on a.orderid = b.code and a.site_code = b.plant 
                where a.orderid in ('{OrderNo}')
                order by a.orderid, a.create_date DESC; 
            ").ToListAsync();
            return query;
        }
    }
}