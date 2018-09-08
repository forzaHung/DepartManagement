using ClosedXML.Excel;
using marketplace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Prototype
{
    public class ClosedXmlReader
    {
        private XLWorkbook _workbook;
        private IXLWorksheet _worksheet;
        public ClosedXmlReader(Stream stream)
        {
            _workbook = new XLWorkbook(stream, XLEventTracking.Disabled);
            _worksheet = _workbook.Worksheet(1); //Get first worksheet
        }
        private Dictionary<string, string> _dictMapColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"Name", "Name" },
            {"Code", "ProductCode" },
            {"Maker", "CategoryName" }
        };
        public List<Productentity> ToProductList(ref int invalidCount)
        {
            var result = new List<Productentity>();

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
                var entity = new Productentity();
                var sb = new StringBuilder("{");
                bool isValid = true;
                foreach (var column in dictColumns)
                {
                    var value = _worksheet.Cell(i, column.Key).GetString();

                    if (_dictMapColumns.ContainsKey(column.Value))
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            isValid = false;
                            break;
                        }
                        switch (_dictMapColumns[column.Value])
                        {
                            case "Maker":
                                entity.MakerName = value;
                                break;
                            case "ProductName":
                                entity.ProductName = value;
                                break;
                            case "JAN":
                                entity.ProductCode = value;
                                break;
                            case "Image":
                                entity.ImageLink = value;
                                break;
                        }
                    }
                    if (sb.Length > 1)
                        sb.Append(", ");
                    sb.AppendFormat("'{0}': '{1}'", column.Value, value);
                } //End for dictColumns
                sb.Append("}");
                if (!isValid || string.IsNullOrWhiteSpace(entity.ProductCode))
                {
                    invalidCount++;
                    continue;
                }
               // entity.JsonData = sb.ToString();
                result.Add(entity);
            } //End for each row

            return result;
        }
        public void UpdateDictMap(string maker,string columnCode, string columnName, string image)
        {
            _dictMapColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {maker, "Maker" },
                {columnName, "ProductName" },
                {columnCode, "JAN" },
                {image, "Image" }
            };
        }
    }
}