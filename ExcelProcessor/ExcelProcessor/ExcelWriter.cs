using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Linq;
using System.Data;
using System.Data.OleDb;

namespace ExcelProcessor
{
    class ExcelWriter
    {
        public void SaveToExcel(
            DataTable dataTable,
            string fileName,
            string sheetName)
        {
            SpreadsheetDocument spreadsheetDocument =
                SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = sheetName
            };
            sheets.Append(sheet);

            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            int rowIndex = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                rowIndex++;
                char columnIndex = 'A';
                Row row = new Row();
                sheetData.Append(row);

                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    object value = dataRow[dataColumn.ColumnName] ?? "";
                    Cell newCell = new Cell()
                    {
                        CellReference = string.Format("{0}{1}", columnIndex, rowIndex)
                    };
                    row.Append(newCell);
                    newCell.CellValue = new CellValue(value.ToString());
                    newCell.DataType = new EnumValue<CellValues>(CellValues.String);
                    columnIndex++;
                }
            }

            workbookpart.Workbook.Save();
            spreadsheetDocument.Close();

        }
    }
}
