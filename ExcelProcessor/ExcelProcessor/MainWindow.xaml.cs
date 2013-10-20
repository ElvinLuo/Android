using System.Data;
using System.Windows;

namespace ExcelProcessor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataTable dataTable;
            ExcelReader excelReader = new ExcelReader();
            ExcelWriter excelWriter = new ExcelWriter();

            //dataTable = excelReader.ReadXLSFile("专项备货消化表.xls", "BID");
            //excelWriter.SaveToExcel(
            //    excelReader.GetBIDDataTable(dataTable),
            //    "BID.xlsx",
            //    "BID");

            //dataTable = excelReader.ReadXLSFile("专项备货消化表.xls", "PROM");
            //excelWriter.SaveToExcel(
            //    excelReader.GetBIDDataTable(dataTable),
            //    "PROM.xlsx",
            //    "PROM");

            //dataTable = excelReader.ReadXLSFile("专项备货消化表.xls", "SDI");
            //excelWriter.SaveToExcel(
            //    excelReader.GetBIDDataTable(dataTable),
            //    "SDI.xlsx",
            //    "SDI");

            dataTable = excelReader.ReadXLSFile("回款跟进表.xls", "应收款");
            excelWriter.SaveToExcel(
                excelReader.GetFilterDataTable(dataTable),
                "回款表.xlsx",
                "回款表");

            //excelReader.ReadXLSXFile(@"D:\Projects\BID====.xlsx");
            //excelReader.ReadXLSFile(@"D:\Projects\HCF销售管理部秘密-分销一部回款跟进表.xls");
            InitializeComponent();
        }
    }
}
