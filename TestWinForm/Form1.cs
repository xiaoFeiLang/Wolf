using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADOX;
using System.Data.OleDb;

namespace TestWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateTestDB_Click(object sender, EventArgs e)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string dbName = dir + "test.mdb";
            ADOX.CatalogClass catlog = new ADOX.CatalogClass();
            catlog.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbName + ";");
            ADOX.TableClass tableClass = new ADOX.TableClass();
            tableClass.ParentCatalog = catlog;
            tableClass.Name = "TestTable";

            ADOX.ColumnClass columnID = new ADOX.ColumnClass();
            columnID.ParentCatalog = catlog;
            columnID.Type = ADOX.DataTypeEnum.adInteger;
            columnID.Name = "Id";
            columnID.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
            columnID.Properties["AutoIncrement"].Value = true;
            tableClass.Columns.Append(columnID, ADOX.DataTypeEnum.adInteger, 0);

            ADOX.ColumnClass columnName = addCol(catlog, "Name");
            tableClass.Columns.Append(columnName, ADOX.DataTypeEnum.adVarChar, 30);

            catlog.Tables.Append(tableClass);
            MessageBox.Show("Successful");
            tableClass = null;
            catlog = null;
        }

        private ADOX.ColumnClass addCol(ADOX.CatalogClass cat, string colName)
        {
            ADOX.ColumnClass col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            // col.Type = ADOX.DataTypeEnum.adInteger; // 必须先设置字段类型
            col.Name = colName;
            col.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
            return col;
        }

        private void InsertData_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = getConn();
                conn.Open();
                for(int i = 0; i< 10; i++)
                {
                    string sql = "insert into TestTable (name) values ('";
                    sql += "A" + i.ToString();
                    sql += "')";
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private OleDbConnection getConn()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "test.mdb";
            string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dir + ";";
            OleDbConnection conn = new OleDbConnection(connstring);
            return conn;
        }

        private void SelectData_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = getConn();
            conn.Open();
            string sql = "select * from TestTable";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "testTable");
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
    }
}
