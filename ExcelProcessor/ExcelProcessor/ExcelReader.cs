using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExcelProcessor
{
    class ExcelReader
    {
        private List<string> columnNameListNeedToPrint = new List<string>
        {
            "客户名",
            "End user",
            " 合同金额 ",
            "销售_人员",
            "区域",
            "发货状态",
            "总应收款_(发货总金额-到款总金额)",
            "截至今天应收款",
            "本月截止应收款",
            "超期_天数"
        };

        public DataTable ReadXLSFile(string filename, string sheetName)
        {
            if (File.Exists(filename))
            {
                OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
                builder.DataSource = filename;
                builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                builder["Extended Properties"] = "Excel 8.0;HDR=Yes;IMEX=1";

                using (OleDbConnection connection = new OleDbConnection(builder.ConnectionString))
                {
                    connection.Open();
                    string selectCommandText = string.Format("SELECT * FROM [{0}$]", sheetName);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommandText, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }

            return null;
        }

        public DataTable GetBIDDataTable(DataTable dataTable)
        {
            List<int> indexListNeedToPrint = new List<int>();
            List<string> columnNameListNeedToPrint = new List<string>
            {
                "大区",
                "隶属小区",
                "最终用户",
                "提货日期",
                "on shore 提货金额\n (￥)"
            };

            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                string cellValue = dataTable.Rows[0][j].ToString();
                if (columnNameListNeedToPrint.Contains(cellValue))
                {
                    indexListNeedToPrint.Add(j);
                }
            }

            DataTable bidTable = new DataTable("bidTable");
            foreach (string columnName in columnNameListNeedToPrint)
            {
                bidTable.Columns.Add(columnName);
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                if (row[0] != null && row[0].ToString().Contains("已交付/取消"))
                {
                    break;
                }

                int keyIndex = indexListNeedToPrint[3];
                DateTime parsedDateTime;
                bool succeeded = DateTime.TryParse(row[keyIndex].ToString(), out parsedDateTime);
                if (i == 0 ||
                    (succeeded && parsedDateTime <= DateTime.Now.AddDays(-60)))
                {
                    object[] items = new Object[columnNameListNeedToPrint.Count];
                    for (int index = 0; index < indexListNeedToPrint.Count; index++)
                    {
                        items[index] =
                            row[indexListNeedToPrint[index]] ??
                            dataTable.Rows[i - 1][indexListNeedToPrint[index]];
                    }
                    bidTable.Rows.Add(items);
                }
            }

            return bidTable;
        }

        public DataTable GetFilterDataTable(DataTable dataTable)
        {
            DataTable finalTable = new DataTable("FilteredTable");
            foreach (string columnName in columnNameListNeedToPrint)
            {
                finalTable.Columns.Add(columnName);
            }
            finalTable.Rows.Add(columnNameListNeedToPrint.ToArray());

            for (int i = 2; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                Regex pattern = new Regex(@"-*\d+");
                Match m = pattern.Match(row["超期_天数"].ToString());
                if (m.Value != "" && Convert.ToInt32(m.Value) >= 60)
                {
                    int index = 0;
                    object[] items = new Object[columnNameListNeedToPrint.Count];
                    foreach (string columnName in columnNameListNeedToPrint)
                    {
                        items[index] = row[columnName] ?? "";
                        index++;
                    }
                    finalTable.Rows.Add(items);
                }
            }

            return finalTable;
        }

        public void ReadXLSFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
                builder.DataSource = filepath;
                builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                builder["Extended Properties"] = "Excel 8.0;HDR=Yes;IMEX=1";

                using (OleDbConnection connection = new OleDbConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [应收款$]", connection))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        for (int i = 2; i < dataTable.Rows.Count; i++)
                        {
                            DataRow row = dataTable.Rows[i];

                            Regex pattern = new Regex(@"\d+");
                            Match m = pattern.Match(row["超期_天数"].ToString());
                            if (m.Value != "" &&
                                Convert.ToInt32(m.Value) >= 60)
                            {
                                foreach (string columnName in columnNameListNeedToPrint)
                                {
                                    object value = row[columnName] ?? "";
                                    stringBuilder.Append(value + "     ");
                                }
                                stringBuilder.AppendLine();
                            }
                        }

                        string final = stringBuilder.ToString();
                    }
                }
            }
            else
            {
            }
        }

        public void ReadXLSXFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                string value;
                value = GetCellValue(filepath, "BID", "A1");
                value = GetCellValue(filepath, "BID", "A2");
                value = GetCellValue(filepath, "BID", "A3");
            }
            else
            {
            }
        }

        private string GetCellValue(
            string fileName,
            string sheetName,
            string addressName)
        {
            string value = null;
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart wbPart = document.WorkbookPart;
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().
                  Where(s => s.Name == sheetName).FirstOrDefault();

                // Throw an exception if there is no sheet.
                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }

                WorksheetPart wsPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
                Cell theCell = wsPart.Worksheet.Descendants<Cell>().
                  Where(c => c.CellReference == addressName).FirstOrDefault();

                if (theCell != null)
                {
                    value = theCell.InnerText;
                    if (theCell.DataType != null)
                    {
                        switch (theCell.DataType.Value)
                        {
                            case CellValues.SharedString:
                                var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                                if (stringTable != null)
                                {
                                    value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                                }
                                break;

                            case CellValues.Boolean:
                                switch (value)
                                {
                                    case "0":
                                        value = "FALSE";
                                        break;
                                    default:
                                        value = "TRUE";
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            return value;
        }

    }
}
