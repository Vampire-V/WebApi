using ClosedXML.Excel;
namespace WebApi.Extensions
{
    public static class Excel<T>
    {
        public static List<T> ImportExcel(Stream file, string sheetName = ""){
            List<T> list = new List<T>();
            Type typeOfObject = typeof(T) ?? throw new Exception("Import Excel not fount model.");
            var workbook = new XLWorkbook(file);
            var worksheet = String.IsNullOrEmpty(sheetName) ? workbook.Worksheets.First() : workbook.Worksheets.Where(w => w.Name == sheetName).First();
            var properties = typeOfObject.GetProperties();

            // Header column texts
            var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 }) ?? throw new ArgumentNullException(nameof(workbook),"Template mismatch.");

            foreach (IXLRow row in worksheet.RowsUsed().Skip(2))//skip 2 row top in sheet
            {
                T? obj = (T?)Activator.CreateInstance(typeOfObject);
                foreach (var prop in properties)
                {
                    var column = columns.SingleOrDefault(c => c.Value.ToString() == prop.Name.ToString()) ?? throw new ArgumentNullException(nameof(prop.Name),"Column mismatch.");
                    // int colIndex = column.Index;

                    var val = row.Cell(column.Index).Value;
                    var type = prop.PropertyType;
                    if (CheckType.IsNumericType(prop.PropertyType) && String.IsNullOrEmpty(val.ToString()))
                    {
                        prop.SetValue(obj, Convert.ChangeType(0.00, type));
                    }
                    else
                    {
                        prop.SetValue(obj, Convert.ChangeType(val, type));
                    }

                }
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj),"Object must not be empity.");
                }
                list.Add(obj);
            }
            return list;
        }

    }
}