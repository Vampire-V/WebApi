using AutoMapper;
using ClosedXML.Excel;
using WebApi.Services.Interface;
using WebApi.Models.Vendor;
using WebApi.Models.ChequeBNP.Example;
using System.Globalization;
using WebApi.Data.ChequeBNP.Entities;
using WebApi.Data.ChequeDirect.Entities;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class VendorService : IVendorService
    {
        private readonly IChequeDirectUnitOfWork _chequeDirectUnitOfWork;
        private readonly IChequeBNPUnitOfWork _ChequeBNPUnitOfWork;
        public readonly IMapper _mapper;
        public VendorService(IMapper mapper, IChequeDirectUnitOfWork chequeDirectUnitOfWork, IChequeBNPUnitOfWork ChequeBNPUnitOfWork)
        {
            _mapper = mapper;
            _chequeDirectUnitOfWork = chequeDirectUnitOfWork;
            _ChequeBNPUnitOfWork = ChequeBNPUnitOfWork;
        }

        public async Task CreateVendor(VendorCreate vendorCreate)
        {
            var vendor = _mapper.Map<Vendor>(vendorCreate);
            await _chequeDirectUnitOfWork.VendorRepository.Add(vendor);
            await _chequeDirectUnitOfWork.SaveAsync();
        }

        public async Task Delete(string vendorCode)
        {
            var etity = await _chequeDirectUnitOfWork.VendorRepository.GetById(vendorCode);
            _chequeDirectUnitOfWork.VendorRepository.Remove(etity);
            await _chequeDirectUnitOfWork.SaveAsync();
        }

        public async Task<VendorView> GetVendor(string vendorCode)
        {
            var item = await _chequeDirectUnitOfWork.VendorRepository.GetById(vendorCode);
            return _mapper.Map<VendorView>(item);
        }

        public async Task<byte[]> GetVendorForExcel(VendorParameter vendorParameter)
        {
            var vendors = await _chequeDirectUnitOfWork.VendorRepository.GetVendorsFilterAsync(vendorParameter);
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Vendors List");
            var currentRow = 1;

            worksheet.Cell(currentRow, 1).SetValue("VendorCode").Style.Font.SetBold();
            worksheet.Cell(currentRow, 2).SetValue("Name").Style.Font.SetBold();
            worksheet.Cell(currentRow, 3).SetValue("Address").Style.Font.SetBold();
            worksheet.Cell(currentRow, 4).SetValue("Fax").Style.Font.SetBold();
            worksheet.Cell(currentRow, 5).SetValue("TaxId").Style.Font.SetBold();
            worksheet.Cell(currentRow, 6).SetValue("Tel").Style.Font.SetBold();
            worksheet.Cell(currentRow, 7).SetValue("PND").Style.Font.SetBold();
            worksheet.Cell(currentRow, 8).SetValue("TaxIdVendor1").Style.Font.SetBold();
            worksheet.Cell(currentRow, 9).SetValue("TaxIdVendor2").Style.Font.SetBold();
            worksheet.Cell(currentRow, 10).SetValue("TaxIdVendor3").Style.Font.SetBold();
            worksheet.Cell(currentRow, 11).SetValue("VATRegisNo").Style.Font.SetBold();
            foreach (var vendor in vendors)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).SetValue(vendor.VendorCode);
                worksheet.Cell(currentRow, 2).SetValue(vendor.Name);
                worksheet.Cell(currentRow, 3).SetValue(vendor.Address);
                worksheet.Cell(currentRow, 4).SetValue(vendor.Fax);
                worksheet.Cell(currentRow, 5).SetValue(vendor.TaxId);
                worksheet.Cell(currentRow, 6).SetValue(vendor.Tel);
                worksheet.Cell(currentRow, 7).SetValue(vendor.PND);
                worksheet.Cell(currentRow, 8).SetValue(vendor.TaxIdVendor1);
                worksheet.Cell(currentRow, 9).SetValue(vendor.TaxIdVendor2);
                worksheet.Cell(currentRow, 10).SetValue(vendor.TaxIdVendor3);
                worksheet.Cell(currentRow, 11).SetValue(vendor.VATRegisNo);
            }
            worksheet.Columns("A:K").AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }

        public async Task<List<VendorView>> GetVendors(VendorParameter vendorParameter)
        {
            return _mapper.Map<List<VendorView>>(await _chequeDirectUnitOfWork.VendorRepository.GetVendorsFilterAsync(vendorParameter));
        }

        public async Task UpdateVendor(string vendorCode, VendorUpdate vendorUpdate)
        {
            var item = await _chequeDirectUnitOfWork.VendorRepository.GetById(vendorCode);
            if (item is null)
            {
                throw new ArgumentNullException("Vendor not fount.");
            }
            item.TaxId = vendorUpdate.TaxId;
            item.Name = vendorUpdate.Name;
            item.Address = vendorUpdate.Address;
            item.Fax = vendorUpdate.Fax;
            item.Tel = vendorUpdate.Tel;
            item.PND = vendorUpdate.PND;
            item.TaxIdVendor1 = vendorUpdate.TaxIdVendor1;
            item.TaxIdVendor2 = vendorUpdate.TaxIdVendor2;
            item.TaxIdVendor3 = vendorUpdate.TaxIdVendor3;
            item.VATRegisNo = vendorUpdate.VATRegisNo;
            await _chequeDirectUnitOfWork.SaveAsync();
        }


        public async Task<Document> TestChequeBNP()
        {
            var results = await _ChequeBNPUnitOfWork.ResultDataRepository.GetAll();
            // var resultSort = results.OrderBy(s => s.Id).ToList();
            // Header header = new Header();
            Document document1 = new Document
            {
                CstmrCdtTrfInitn = new CstmrCdtTrfInitn
                {
                    GrpHdr = new GrpHdr(),
                    PmtInf = new List<PmtInf>()
                }
            };
            List<CdtTrfTxInf> trfs = new List<CdtTrfTxInf>();
            // List<DocumentDetail> documents = new List<DocumentDetail>();
            // List<InvoiceDetail> invoices = new List<InvoiceDetail>();

            string vendorCode = string.Empty;
            string vendorName = string.Empty;
            string vendorAddress = string.Empty;
            string country = string.Empty;
            foreach (ResultData result in results.OrderBy(r => r.Id))
            {
                List<string> item = result.Result.Split('~').Select(i => i.Trim()).ToList();

                // สร้าง Header Cheque
                if (item.First<string>() == "001")
                {
                    document1.CstmrCdtTrfInitn.GrpHdr.MsgId = "INTTT9300098 (ไม่รู้)";
                    document1.CstmrCdtTrfInitn.GrpHdr.CreDtTm = DateTime.ParseExact($"{item[6]}{item[7]}", "ddMMyyyyHHmmss", CultureInfo.InvariantCulture).ToString("s"); ;
                    document1.CstmrCdtTrfInitn.GrpHdr.NbOfTxs = "1 (ไม่รู้)";
                    document1.CstmrCdtTrfInitn.GrpHdr.InitgPty = new InitgPty
                    {
                        Nm = "ABCD (ไม่รู้)"
                    };
                    // header.RecordType = item.First<string>();
                    // header.CompanyId = item[1];
                    // header.CompanyTaxId = item[2];
                    // header.CompanyAccount = item[3];
                    // header.CustomerBatchReference = item[4];
                    // header.BatchBroadcastMessage = item[5];
                    // header.FileDate = item[6];
                    // header.FileTimestamp = item[7];
                }
                // สร้าง Doc Cheque
                if (item.First<string>() == "003")
                {
                    vendorCode = item[22];
                    vendorName = item[43];
                    vendorAddress = item[44] + item[44];
                    country = item[52].Substring(0, 2);
                    PmtInf pmtInf = new PmtInf();
                    pmtInf.PmtMtd = "TRF"; // PaymentMethod master data BNP
                    pmtInf.PmtInfId = item[8]; // เลข Doc
                    pmtInf.BtchBookg = false; // BatchBooking (ไม่รู้ ถามแบงค์)
                    pmtInf.NbOfTxs = result.Id.Trim(); // number transaction
                    pmtInf.CtrlSum = Decimal.Parse(item[33]); // Amount จำนวนเงินสุทธิ
                    pmtInf.PmtTpInf = new PmtTpInf
                    {
                        InstrPrty = "NORM" // Only "NORM" is allowed.
                    };
                    pmtInf.ReqdExctnDt = item[5]; // Pre-advice date

                    // ลูกหนี้ (Haier)
                    pmtInf.Dbtr = new Dbtr
                    {
                        Nm = "Haier",
                        PstlAdr = new PstlAdr
                        {
                            Ctry = "TH",
                            AdrLine = new string[] { "Nong Ki, Kabin Buri District, Prachin Buri 25110" }
                        }
                    };

                    // DebtorAccount
                    pmtInf.DbtrAcct = new DbtrAcct
                    {
                        Id = new Id
                        {
                            Othr = new Othr
                            {
                                Id = "0001000121204069USD" // เลขบัญชี Haier
                            }
                        }
                    };
                    // DebtorAgent (ตัวแทนไฮเออร์)
                    pmtInf.DbtrAgt = new DbtrAgt
                    {
                        FinInstnId = new FinInstnId
                        {
                            BIC = "BNPATHBKXXX", // มาจากแบงค์
                            PstlAdr = new PstlAdr
                            {
                                Ctry = "TH"
                            }
                        }
                    };
                    pmtInf.ChrgBr = "SHAR"; // ลูกหนี้ เจ้าหนี้ share ค่าธรรมเนียม

                    pmtInf.CdtTrfTxInf = new List<CdtTrfTxInf>();
                    document1.CstmrCdtTrfInitn.PmtInf.Add(pmtInf);
                    /** DocumentDetail
                        DocumentDetail document = new DocumentDetail
                        {
                            RecordType = item.First(),
                            CompanyId = item[1],
                            CreditSequenceNumber = item[2],
                            ProductCode = item[3],
                            BeneficiaryAccountNumber = item[4],
                            ValueDate = item[5],
                            ValueTime = item[6],
                            CreditCurrency = item[7],
                            DocumentNumber = item[8],
                            PreAdviceDate = item[9],
                            DeliveryMethod = item[10],
                            DispatchTo = item[11],
                            ChequeDepositRequired = item[12],
                            CopyIdCardPresent = item[13],
                            WHTPresent = item[14],
                            InvoiceDetailsPresent = item[15],
                            VATPresen = item[16],
                            ReceiptPresent = item[17],
                            CreditAdviceRequired = item[18],
                            ChequeDrawnOnLocation = item[19],
                            DispatchToCode = item[20],
                            WHTFormType = item[21],
                            WHTSerialNo = item[22],
                            WHTBookNo = item[23],
                            WHTRunningNo = item[24],
                            BahtNetPaymentTypeCode = item[25],
                            BOTServiceTypeOfPayment = item[26],
                            NoOfWHTDetails = Int32.Parse(item[27]),
                            TotalWHTAmount = Decimal.Parse(item[28]),
                            NoOfInvoiceDetails = Int32.Parse(item[29]),
                            TotalInvoiceAmount = Decimal.Parse(item[30]),
                            TotalDiscountAmount = Decimal.Parse(String.IsNullOrEmpty(item[31]) ? "0" : item[31]),
                            PayeeChargeCode = item[32],
                            PaymentNetamount = Decimal.Parse(item[33]),
                            WHTPayType = item[34],
                            WHTRemark = item[35],
                            WHTDeductDate = item[36],
                            ReceivingBankCode = item[37],
                            ReceivingBranchCode = item[38],
                            WHTSignatory = item[39],
                            ServiceCode = item[40],
                            BeneficiaryCode = item[41],
                            PayeeIdCard = item[42],
                            PayeeName = item[43],
                            PayeeAddress1 = item[44],
                            PayeeAddress2 = item[45],
                            PayeeAddress3 = item[46],
                            PayeeAddress4 = item[47],
                            DispatchAddress1 = item[48],
                            DispatchAddress2 = item[49],
                            DispatchAddress3 = item[50],
                            DispatchAddress4 = item[51],
                            PayeeTaxID = item[52],
                            PayeeFaxNumber = item[53],
                            PayeeMobilePhoneNumber = item[54],
                            PayeeEmailAddress = item[55],
                            CollectorIdType = item[56],
                            CollectorId = item[57],
                            RequiredReturnDocuments = item[58],
                            OthersRequiredReturnDocuments = item[59],
                            Option1 = item[60],
                            Option2 = item[61],
                            Option3 = item[62],
                            Option4 = item[63],
                            Option5 = item[64],
                        };
                        documents.Add(document);
                    */
                }

                // สร้าง Invoice Cheque
                if (item.First<string>() == "006")
                {
                    CdtTrfTxInf invoice = new CdtTrfTxInf();
                    invoice.PmtId = new PmtId
                    {
                        InstrId = "1xxx (ไม่รู้)", // number 
                        EndToEndId = item[1].Split('_').ToList()[1] // เลข Customer
                    };
                    invoice.Amt = new Amt
                    {
                        InstdAmt = Decimal.Parse(item[2]) // Invoice Amount 
                    };
                    invoice.CdtrAgt = new CdtrAgt
                    {
                        FinInstnId = new FinInstnId
                        {
                            BIC = "ได้จากเอกสารบัญชี (ใน SAP ไม่มีเก็บ)", // ได้จากเอกสารบัญชี (ใน SAP ไม่มีเก็บ)
                            Nm = vendorName,
                            PstlAdr = new PstlAdr
                            {
                                Ctry = country
                            }
                        }
                    };
                    invoice.Cdtr = new Cdtr
                    {
                        Nm = vendorName,
                        PstlAdr = new PstlAdr
                        {
                            Ctry = country,
                            AdrLine = new string[] { vendorAddress }
                        }
                    };
                    invoice.CdtrAcct = new CdtrAcct
                    {
                        Id = new Id
                        {
                            Othr = new Othr
                            {
                                Id = "เลขบัญชี Vendor ยังดึงไม่ได้"
                            }
                        }
                    };
                    invoice.Purp = new Purp
                    {
                        Prtry = "318231"
                    };
                    // RmtLctnMtd : Optional Use this code to activate remittance advie send through email Ex. "EMAIL"
                    // RmtLctnElctrncAdr : Optional Email address of recipient, separate by ";" semicolon and NO any space in between each email. 
                    // Ex. email1@hotmail.com;email2@hotmail.com;email3@hotmail.com
                    invoice.RltdRmtInf = new RltdRmtInf
                    {
                        RmtLctnMtd = "EMAIL",
                        RmtLctnElctrncAdr = "email1@hotmail.com;email2@hotmail.com;email3@hotmail.com"
                    };

                    // trfs.Add(invoice);
                    // document1.CstmrCdtTrfInitn.PmtInf.FindIndex(d => d.PmtInfId == vendorCode);
                    document1.CstmrCdtTrfInitn.PmtInf[document1.CstmrCdtTrfInitn.PmtInf.FindIndex(d => d.PmtInfId == item[5])].CdtTrfTxInf.Add(invoice);
                    // int PmtInfIndex = document1.CstmrCdtTrfInitn.PmtInf.FindIndex(p => p.PmtInfId == item[5]);
                    // if (PmtInfIndex >= 0)
                    // {
                    //     document1.CstmrCdtTrfInitn.PmtInf[PmtInfIndex].CdtTrfTxInf = new List<CdtTrfTxInf>();
                    // }
                    // InvoiceDetail invoice = new InvoiceDetail
                    // {
                    //     RecordIdentifier = item.First<string>(),
                    //     InvoiceNumber = item[1].Split('_').ToList().First<string>(),
                    //     PoNumber = item[1].Split('_').ToList()[1],
                    //     InvoiceDate = item[1].Split('_').ToList()[2],
                    //     Amount = Decimal.Parse(item[2]),
                    //     Description = item[3],
                    //     VatAmount = Decimal.Parse(item[4]),
                    //     DocumentNumber = item[5]
                    // };
                    // invoices.Add(invoice);
                }
            }

            // header.Documents = documents;
            // header.Documents?.AddRange(documents);
            // header.Documents?.ForEach(d =>
            // {
            //     d.Invoices = new List<InvoiceDetail>();
            //     List<InvoiceDetail> items = invoices.Where(i => i.DocumentNumber == d.DocumentNumber).ToList();
            //     d.Invoices.AddRange(items);
            // });
            // list = ssss[1].Result.Split('~').ToList();
            // return JsonSerializer.Serialize(results.Select(r => r.Result));
            document1.CstmrCdtTrfInitn.GrpHdr.CtrlSum = document1.CstmrCdtTrfInitn.PmtInf.Sum(s => s.CtrlSum);
            return document1;
        }
    }
}