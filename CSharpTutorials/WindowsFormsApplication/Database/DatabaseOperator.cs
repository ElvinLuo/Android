using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WindowsFormsApplication.Database
{
    public class DatabaseOperator
    {
        private string connectionString = null;

        public DatabaseOperator(string server, string database)
        {
            this.connectionString =
                string.Format("Server={0};Database={1};Integrated Security=True;", server, database);
        }

        public DataSet GetDataSet(string commandText)
        {
            return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
        }
    }
}
