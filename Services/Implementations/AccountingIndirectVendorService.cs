using AutoMapper;
using ClosedXML.Excel;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.AccountingIndirectVendor;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class AccountingIndirectVendorService : IAccountingIndirectVendorService
    {
        private readonly IAccountingUniOfWork _accountingUniOfWork;
        public readonly IMapper _mapper;
        public AccountingIndirectVendorService(IMapper mapper, IAccountingUniOfWork accountingUniOfWork)
        {
            _mapper = mapper;
            _accountingUniOfWork = accountingUniOfWork;
        }

        public async Task<List<IndirectVendorView>> GetIndirectVendors(IndirectVendorParameter indirectVendorParameter)
        {
            var item = await _accountingUniOfWork.AccountingIndirectVendorRepository.GetIndirectVendorsFilterAsync(indirectVendorParameter);
            return _mapper.Map<List<IndirectVendorView>>(item);
        }

        public async Task<byte[]> GetIndirectVendorForExcel(IndirectVendorParameter indirectVendorParameter)
        {
            var indirectVendors = await _accountingUniOfWork.AccountingIndirectVendorRepository.GetIndirectVendorsFilterAsync(indirectVendorParameter);
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("indirectVendors List");
            var currentRow = 1;

            worksheet.Cell(currentRow, 1).SetValue("VendorCode").Style.Font.SetBold();
            worksheet.Cell(currentRow, 2).SetValue("VendorName").Style.Font.SetBold();
            worksheet.Cell(currentRow, 3).SetValue("TaxId").Style.Font.SetBold();
            worksheet.Cell(currentRow, 4).SetValue("HeadOfficeId").Style.Font.SetBold();
            worksheet.Cell(currentRow, 5).SetValue("BranchId").Style.Font.SetBold();
            foreach (var indirectVendor in indirectVendors)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).SetValue(indirectVendor.VendorCode);
                worksheet.Cell(currentRow, 2).SetValue(indirectVendor.VendorName);
                worksheet.Cell(currentRow, 3).SetValue(indirectVendor.TaxId);
                worksheet.Cell(currentRow, 4).SetValue(indirectVendor.HeadOfficeId);
                worksheet.Cell(currentRow, 5).SetValue(indirectVendor.BranchId);
            }
            worksheet.Columns("A:E").AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }

        public async Task<IndirectVendorView> GetIndirectVendor(string vendorCode)
        {
            var item = await _accountingUniOfWork.AccountingIndirectVendorRepository.GetById(vendorCode);
            return _mapper.Map<IndirectVendorView>(item);
        }

        public async Task CreateIndirectVendor(IndirectVendorCreate indirectVendorCreate)
        {
            var indirectVendor = _mapper.Map<AccountingIndirectVendor>(indirectVendorCreate);
            await _accountingUniOfWork.AccountingIndirectVendorRepository.Add(indirectVendor);
            await _accountingUniOfWork.SaveAsync();
        }

        public async Task UpdateIndirectVendor(string vendorCode, IndirectVendorUpdate indirectVendorUpdate)
        {
            var item = await _accountingUniOfWork.AccountingIndirectVendorRepository.GetById(vendorCode);
            if (item is null)
            {
                throw new ArgumentNullException("Indirect Vendor not fount.");
            }
            if (indirectVendorUpdate.TaxId is null || indirectVendorUpdate.HeadOfficeId is null || indirectVendorUpdate.BranchId is null)
            {
                throw new ArgumentNullException("Not fount.");
            }
            item.VendorCode = indirectVendorUpdate.VendorCode;
            item.VendorName = indirectVendorUpdate.VendorName;
            item.TaxId = indirectVendorUpdate.TaxId;
            item.HeadOfficeId = indirectVendorUpdate.HeadOfficeId;
            item.BranchId = indirectVendorUpdate.BranchId;
            await _accountingUniOfWork.SaveAsync();
        }

        public async Task Delete(string vendorCode)
        {
            var etity = await _accountingUniOfWork.AccountingIndirectVendorRepository.GetById(vendorCode);
            _accountingUniOfWork.AccountingIndirectVendorRepository.Remove(etity);
            await _accountingUniOfWork.SaveAsync();
        }

    }
}