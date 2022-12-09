using AutoMapper;
using ClosedXML.Excel;
using WebApi.Models.COGI;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class COGIService : ICOGIService
    {
        private readonly IS4UnitOfWork _s4UnitOfWork;
        public readonly IMapper _mapper;
        public COGIService(IMapper mapper, IS4UnitOfWork s4UnitOfWork)
        {
            _mapper = mapper;
            _s4UnitOfWork = s4UnitOfWork;
        }

        public List<COGIView> GetCOGI(DateTime TimeStamp)
        {
            return _mapper.Map<List<COGIView>>(_s4UnitOfWork.COGIRepository.GetCOGI(TimeStamp));
        }

        public byte[] GetCOGIForExcel(DateTime TimeStamp)
        {
            var COGIs = _s4UnitOfWork.COGIRepository.GetCOGI(TimeStamp);
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("COGI " + TimeStamp.ToString("yyyy-MM-dd"));
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).SetValue("PLANT").Style.Font.SetBold();
            worksheet.Cell(currentRow, 2).SetValue("RESERV_NO").Style.Font.SetBold();
            worksheet.Cell(currentRow, 3).SetValue("ORDER_NO").Style.Font.SetBold();
            worksheet.Cell(currentRow, 4).SetValue("MATERIAL").Style.Font.SetBold();
            worksheet.Cell(currentRow, 5).SetValue("LOCATION").Style.Font.SetBold();
            worksheet.Cell(currentRow, 6).SetValue("QUANTITY").Style.Font.SetBold();
            worksheet.Cell(currentRow, 7).SetValue("UNIT").Style.Font.SetBold();
            worksheet.Cell(currentRow, 8).SetValue("MOVEMENT_TYPE").Style.Font.SetBold();
            worksheet.Cell(currentRow, 9).SetValue("MESSAGE_NO").Style.Font.SetBold();
            worksheet.Cell(currentRow, 10).SetValue("MESSAGE_TYPE").Style.Font.SetBold();
            worksheet.Cell(currentRow, 11).SetValue("ERROR_MESSAGE").Style.Font.SetBold();
            worksheet.Cell(currentRow, 12).SetValue("MRP").Style.Font.SetBold();
            worksheet.Cell(currentRow, 13).SetValue("POSTING_DATE").Style.Font.SetBold();
            worksheet.Cell(currentRow, 14).SetValue("ROW_ID").Style.Font.SetBold();
            worksheet.Cell(currentRow, 15).SetValue("TIME_STAMP").Style.Font.SetBold();

            foreach (var COGI in COGIs)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).SetValue(COGI.Plant);
                worksheet.Cell(currentRow, 2).SetValue(COGI.ReservNo);
                worksheet.Cell(currentRow, 3).SetValue(COGI.OrderNo);
                worksheet.Cell(currentRow, 4).SetValue(COGI.Material);
                worksheet.Cell(currentRow, 5).SetValue(COGI.Location);
                worksheet.Cell(currentRow, 6).SetValue(COGI.Quantity);
                worksheet.Cell(currentRow, 7).SetValue(COGI.Unit);
                worksheet.Cell(currentRow, 8).SetValue(COGI.MovementType);
                worksheet.Cell(currentRow, 9).SetValue(COGI.MessageNo);
                worksheet.Cell(currentRow, 10).SetValue(COGI.MessageType);
                worksheet.Cell(currentRow, 11).SetValue(COGI.ErrorMessage);
                worksheet.Cell(currentRow, 12).SetValue(COGI.Mrp);
                worksheet.Cell(currentRow, 13).SetValue(COGI.PostingDate);
                worksheet.Cell(currentRow, 14).SetValue(COGI.RowId);
                worksheet.Cell(currentRow, 15).SetValue(COGI.TimeStamp);
            }

            // Format style sheet
            worksheet.Columns("A:O").AdjustToContents();
            worksheet.Ranges("A1:O1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Ranges("A1:O1").Style.Fill.BackgroundColor = XLColor.Blue;
            worksheet.Ranges("A1:O1").Style.Font.FontColor = XLColor.White;
            worksheet.SheetView.Freeze(1, 1);

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return content;
        }
    }
}