using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ExcelTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            data = new DataTable();
        }

        private DataTable data;

        /// <summary>
        /// Returns the connection string needed for connecting to the specified file and type
        /// </summary>
        private string GetConnectionString()
        {
            // Name of the Excel worksheet to open
            string filename = textBoxFilename.Text;

            // Note: the Types array exactly matches the entries in openFileDialog1.Filter
            string[] Types = {
                    "Excel 12.0 Xml", // For Excel 2007 XML (*.xlsx)
                    "Excel 12.0", // For Excel 2007 Binary (*.xlsb)
                    "Excel 12.0 Macro", // For Excel 2007 Macro-enabled (*.xlsm)
                    "Excel 8.0", // For Excel 97/2000/2003 (*.xls)
                    "Excel 5.0" }; // For Excel 5.0/95 (*.xls)
            // Note: openFileDialog1.FilterIndex was saved into textBoxFilename.Tag
            string Type = Types[(int)textBoxFilename.Tag - 1];

            // True if the first row in the Excel data is a header (used for column names, not data)
            bool Header = checkBoxHeader.Checked;

            // True if columns containing different data types are treated as text
            //  (note that columns containing only integer types are still treated as integer, etc)
            bool TreatIntermixedAsText = checkBoxIntermixed.Checked;

            // Build the actual connection string
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.DataSource = filename;
            if (Type == "Excel 5.0" || Type == "Excel 8.0")
                builder.Provider = "Microsoft.Jet.OLEDB.4.0";
            else
                builder.Provider = "Microsoft.ACE.OLEDB.12.0";
            builder["Extended Properties"] = Type +
                ";HDR=" + (Header ? "Yes" : "No") +
                ";IMEX=" + (TreatIntermixedAsText ? "1" : "0");

            // The "ACE" provider requires either Office 2007 or the following redistributable:
            //  Office 2007 Data Connectivity Components:
            //    http://www.microsoft.com/downloads/details.aspx?familyid=7554F536-8C28-4598-9B72-EF94E038C891&displaylang=en

            // The "ACE" provider can be used for older types (e.g., Excel 8.0) as well.

            // The connection strings used for Excel files are not clearly documented; see the following links for more information:
            //  Excel 2007 on ConnectionStrings.com:
            //    http://www.connectionstrings.com/excel-2007
            //  Excel on ConnectionStrings.com:
            //    http://www.connectionstrings.com/excel
            //  Microsoft OLE DB Provider for Microsoft Jet on MSDN:
            //    http://msdn.microsoft.com/en-us/library/ms810660.aspx
            //  KB247412 Methods for transferring data to Excel from Visual Basic:
            //    http://support.microsoft.com/kb/247412
            //  KB278973 ExcelADO demonstrates how to use ADO to read and write data in Excel workbooks:
            //    http://support.microsoft.com/kb/278973
            //  KB306023 How to transfer data to an Excel workbook by using Visual C# 2005 or Visual C# .NET:
            //    http://support.microsoft.com/kb/306023
            //  KB306572 How to query and display excel data by using ASP.NET, ADO.NET, and Visual C# .NET:
            //    http://support.microsoft.com/kb/306572
            //  KB316934 How to use ADO.NET to retrieve and modify records in an Excel workbook with Visual Basic .NET:
            //    http://support.microsoft.com/kb/316934

            return builder.ConnectionString;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear the DataGridView and the connection string TextBox
                data.Dispose();
                data = new DataTable();
                dataGridView1.DataSource = data;
                textBoxConnectionString.Text = "";

                // Fill the DataGridView and connection string TextBox
                using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
                {
                    connection.Open();
                    textBoxConnectionString.Text = GetConnectionString();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(textBoxSelect.Text, connection))
                    {
                        adapter.Fill(data);
                    }
                }
            }
            catch (Exception ex)
            {
                // Display any errors
                MessageBox.Show("[" + ex.GetType().Name + "] " + ex.Message + ex.StackTrace);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                // In case there's an error, make sure this button becomes disabled
                buttonExecute.Enabled = false;
                dataGridView1.DataSource = null;
                textBoxConnectionString.Text = "";

                // Save the filename and type (which is needed later)
                textBoxFilename.Text = openFileDialog1.FileName;
                textBoxFilename.Tag = openFileDialog1.FilterIndex;

                // Establish a connection to the file and read the sheets and named ranges
                comboBoxSheet.Items.Clear();
                try
                {
                    using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
                    {
                        connection.Open();

                        // See MSDN's "OLE DB Schema Collections (ADO.NET)" for the Microsoft Jet OLE DB Provider
                        //  http://msdn.microsoft.com/en-us/library/cc668764.aspx
                        using (DataTable tables = connection.GetSchema("Tables"))
                        {
                            foreach (DataRow row in tables.Rows)
                                comboBoxSheet.Items.Add((string)row["TABLE_NAME"]);
                        }
                    }

                    // This shouldn't ever happen, but we check for it anyway
                    if (comboBoxSheet.Items.Count == 0)
                        throw new Exception("Excel file has no sheets!");
                    comboBoxSheet.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    // Display any errors
                    MessageBox.Show("[" + ex.GetType().Name + "] " + ex.Message + ex.StackTrace);
                }

                // If everything went OK, then enable the Execute button and give it the focus
                buttonExecute.Enabled = true;
                buttonExecute.Focus();
            }
        }

        private void SetSelectString()
        {
            // Overwrites the select TextBox with the sheet selection along with the (optional) range
            textBoxSelect.Text = "SELECT * FROM [" + (string)comboBoxSheet.SelectedItem + textBoxRange.Text + "]";
        }

        private void comboBoxSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure that only sheets (not named ranges) can have ranges appended to them
            if (!((string)comboBoxSheet.SelectedItem).EndsWith("$"))
            {
                textBoxRange.Text = "";
                textBoxRange.ReadOnly = true;
            }
            else
                textBoxRange.ReadOnly = false;

            SetSelectString();
        }

        private void textBoxRange_TextChanged(object sender, EventArgs e)
        {
            SetSelectString();
        }
    }
}
