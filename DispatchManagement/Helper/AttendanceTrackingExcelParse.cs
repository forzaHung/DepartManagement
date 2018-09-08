using ClosedXML.Excel;
using Dispatch;
using Framework.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DispatchManagement.Helper
{
    public class AttendanceTrackingExcelParse
    {
        private readonly IXLWorksheet _worksheet;
        private readonly Dictionary<string, string> _dictMapColumns;
        public AttendanceTrackingExcelParse(Stream excelStream, Dictionary<string, string> dictMapColumns)
        {
            _dictMapColumns = dictMapColumns;
            var workbook = new XLWorkbook(excelStream, XLEventTracking.Disabled);
            _worksheet = workbook.Worksheet(1); //Get first worksheet
        }
        public List<AttendanceTrackingEntity> Parse(ref List<string> invalid)
        {
            var result = new List<AttendanceTrackingEntity>();
            var range = _worksheet.RangeUsed();
            var rowCount = range.RowCount();
            Dictionary<int, string> dictColumns = new Dictionary<int, string>();
            bool isHeader = true;
            for (int i = 1; i <= rowCount; i++)
            {
                if (isHeader)
                {
                    dictColumns = range.Row(i).RowToDictionary();
                    isHeader = false;
                    continue;
                }
                var entity = new AttendanceTrackingEntity();
                var sb = new StringBuilder("{");
                bool isValid = true;
                foreach (var column in dictColumns)
                {
                    var valueCol = _worksheet.Cell(i, column.Key).ValueCached == null ? _worksheet.Cell(i, column.Key).Value : _worksheet.Cell(i, column.Key).ValueCached;
                    var value = valueCol.ToString();

                    if (_dictMapColumns.ContainsKey(column.Value))
                    {
                        switch (_dictMapColumns[column.Value])
                        {
                            case "Ngày Chấm Công":
                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    invalid.Add("Ngày chấm công không được trống, dòng " + i);
                                    isValid = false;
                                    break;
                                }
                                try
                                {
                                    entity.DateCheck = DateTime.Parse(value);
                                }
                                catch (Exception)
                                {
                                    invalid.Add("Nhập đúng định dạng ngày chấm công, dòng " + i);
                                    isValid = false;
                                    break;
                                }
                                break;
                            case "Giờ Vào":

                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    break;
                                }
                                try
                                {
                                    entity.CheckIn = DateTime.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture).ToString("HH:mm");
                                }
                                catch (Exception)
                                {
                                    invalid.Add("Nhập đúng định dạng giờ vào, dòng " + i);
                                    isValid = false;
                                    break;
                                }
                                break;
                            case "Giờ Ra":
                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    break;
                                }
                                try
                                {
                                    entity.CheckOut = DateTime.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture).ToString("HH:mm");
                                }
                                catch (Exception)
                                {
                                    invalid.Add("Nhập đúng định dạng giờ ra, dòng " + i);
                                    isValid = false;
                                    break;
                                }
                                break;
                            case "Mã nhân viên":
                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    break;
                                }
                                try
                                {
                                    entity.IdEmployee = int.Parse(value.ToString());
                                }
                                catch (Exception)
                                {
                                    invalid.Add("Nhập đúng định dạng mã nhân viên, dòng " + i);
                                    isValid = false;
                                    break;
                                }
                                break;
                        }
                    }
                    if (sb.Length > 1)
                        sb.Append(", ");
                    sb.AppendFormat("'{0}': '{1}'", column.Value, value);
                } //End for dictColumns
                sb.Append("}");
                if (!isValid)
                {
                    continue;
                }
                entity.Id = long.Parse(entity.DateCheck.ToString("yyyyMMdd") + entity.IdEmployee.ToString());
                result.Add(entity);
            } //End for each row
            return result;
        }
    }
}
