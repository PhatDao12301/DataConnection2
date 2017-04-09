using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataConnection2
{
    public partial class Form1 : Form
    {
        //SqlCommand mComm;
        // SqlConnection mConn;
        DataTable mTable;
        //SqlDataAdapter mAdapter;
        SqlCommandBuilder cb;

        MySqlConnection mConn;
        MySqlCommand mComm;
        MySqlDataAdapter mAdapter;

        String tableName = "qlnv2";
        //String connectionString = "Server=.;Database=MyDatabase;Trusted_Connection=true";
        String connectionString = "";
        //String str;
        String tableCommand;
        String tableSelection = "select * from qlnv2";

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // dataGridView1.Dock = DockStyle.Fill;
            this.ActiveControl = btOpen;
            //mConn = new SqlConnection("Server=.;Integrated Security=SSPI;Database=master");
            cb = new SqlCommandBuilder();

            //    str = "CREATE DATABASE mydatabase ON PRIMARY " +
            //"(NAME = DataConnection2, " +
            //"FILENAME = 'D:\\DataConnectionData.mdf', " +
            //"SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
            //"LOG ON (NAME = DataConnection2_Log, " +
            //"FILENAME = 'D:\\DataConnectionLog.ldf', " +
            //"SIZE = 1MB, " +
            //"MAXSIZE = 5MB, " +
            //"FILEGROWTH = 10%)";

            tableCommand = "CREATE TABLE IF NOT EXISTS " + tableName + " (" +
            "ID char(10) primary key, " +
            "Name char(50));";

            //SqlConnectionStringBuilder strBuilder = new SqlConnectionStringBuilder();
            //strBuilder.DataSource = "localhost";
            //connectionString =
            //    //@"Data Source=localhost;User Name=root;Password=;Database=mydatabase;";
            //    @"data source=127.0.0.1;Trusted_Connection=True;Database=mydatabase;";

            //mComm = new SqlCommand(str, mConn);

            //try
            //{
            //    mConn.Open();
            //    mComm.ExecuteNonQuery();
            //    MessageBox.Show("DataBase is Created Successfully"
            //        , "MyProgram"
            //        , MessageBoxButtons.OK
            //        , MessageBoxIcon.Information);

            //}catch(Exception ex)
            //{
            //    //MessageBox.Show(ex.ToString()
            //    //    , "MyProgram"
            //    //    , MessageBoxButtons.OK
            //    //    , MessageBoxIcon.Information);
            //}
            //finally
            //{
            //    if (mConn.State == ConnectionState.Open)
            //    {
            //        //mConn.Close();
            //    }
            //}

            //openDatabase();

        }

        public void queryTable(String cmd)
        {
            this.Validate();
            mConn = new MySqlConnection(connectionString);
            
            mConn.Open();
            //createTable();

            mComm = new MySqlCommand(cmd, mConn);

            mAdapter = new MySqlDataAdapter();
            mAdapter.SelectCommand = mComm;
            
            mTable = new DataTable();

            mTable.Load(mComm.ExecuteReader());

            MySqlCommandBuilder builder = new MySqlCommandBuilder(mAdapter);

            mAdapter.Update(mTable);
            builder.GetUpdateCommand();
            //dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = mTable;

            mConn.Close();
            
        }

        public void createTable()
        {
            try
            {
                MySqlCommand command = new MySqlCommand(tableCommand, mConn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                //mTable = mTable.GetChanges();
                //mAdapter.Update(mTable);
                //dataGridView1.Update();
                //cb.RefreshSchema();
                //mAdapter.UpdateCommand = cb.GetUpdateCommand();
                //mAdapter.Update(mTable);
                //dataGridView1.DataSource = mTable;

                mConn = new MySqlConnection(connectionString);
                mConn.Open();
                
                mAdapter = new MySqlDataAdapter(tableSelection, mConn);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(mAdapter);

                mAdapter.Update(mTable);
                builder.GetUpdateCommand();
                mConn.Close();
                dataGridView1.Update();
                this.Validate();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mAdapter.Update(mTable);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mConn.Open();
            //delete();
            delete2();
            mConn.Close();
        }

        public void delete2()
        {
            try
            {

                Int32 selectedCellCount = this.dataGridView1
                    .GetCellCount(DataGridViewElementStates.Selected);

                foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(row.Index);

                    mConn = new MySqlConnection(connectionString);

                    String data = row.Cells[0].Value.ToString();

                    using (row)
                    {
                        queryTable("delete from qlnv2 where 'name' = "
                            + row.Cells["NAME"]);
                    }

                    //mAdapter = new MySqlDataAdapter(tableSelection, mConn);
                    //MySqlCommandBuilder builder = new MySqlCommandBuilder(mAdapter);
                    //builder.GetDeleteCommand();
                    //builder.GetUpdateCommand();
                    
                    dataGridView1.Update();
                    }
                

                    //mAdapter.DeleteCommand = mComm;

                    mConn.Close();
                    this.Validate();
                

            }
            catch (Exception ex) { };
            this.mAdapter.Update(this.mTable);
        }

        public void delete()
        {
            try
            {

                Int32 selectedCellCount = this.dataGridView1
                    .GetCellCount(DataGridViewElementStates.Selected);

                foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(row.Index);

                    mConn = new MySqlConnection(connectionString);

                    String data = row.Cells[0].Value.ToString();

                    //mComm = new MySqlCommand("DELETE FROM 'qlnv2' WHERE ID = "
                    //+ row.Cells[0] + ";",
                    //mConn);

                    using (mConn)
                    {
                        //    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mAdapter);

                        //    mComm = new MySqlCommand("DELETE FROM 'qlnv2' WHERE ID = "
                        //    + row.Cells[0] + ";",
                        //    mConn);

                        //    mComm = cmd.GetDeleteCommand();
                        //}
                        mAdapter.DeleteCommand = new MySqlCommand(
                        "DELETE FROM 'qlnv2' WHERE ID = "
                        + row.Cells[0] + ";",
                        mConn);
                        //mConn.Open();

                        MySqlCommandBuilder builder = new MySqlCommandBuilder(
                            mAdapter);
                        mAdapter.Update(mTable);
                        //mConn.Close();
                        dataGridView1.Update();
                    }
                    //mComm.ExecuteNonQuery();

                    mAdapter.DeleteCommand = mComm;

                    mConn.Close();
                    this.Validate();

                }

                this.mAdapter.Update(this.mTable);

            }
            catch (Exception ex) { };
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            //connectionString = @"data source=" + tbIP.Text
            //    + ";Trusted_Connection=True;Database=mydatabase;"
            //    + "username=root;"
            //    + "password=;";

            //connect to xampp database
            connectionString = "datasource=127.0.0.1;username=root;"
                + "password=;database=mydatabase;";

            //connectionString = "Data Source=DESKTOP-DM3AGPH\\SQLEXPRESS;Initial Catalog=Employee;Integrated Security=True";

            //connectionString = "datasource=185.28.21.151;username=u920308848_bang;"
            //    + "password=nhoem115;database=u920308848_data;";

            //connectionString = "server=mysql.hostinger.vn;"
            //    +"port=1234;"
            //    + "database=u920308848_data;"
            //    + "uid=u920308848_bang;"
            //    + "password=********;";

            //connectionString = "datasource=basebear.com/login.aspx?ext=1&g=dc1cc10a-4ff1-4276-bdf8-5160dbc7bba1;"
            //    + "trustedconnection=true;"
            //    + "database=qlnv2";

            //connect database 4free
            //connectionString = "datasource=db4free.net;username=phatnvm;"
            //    + "password=magickiss;database=phatnvm_database;"
            //    + "Connection Timeout=60";

            queryTable(tableSelection);

            foreach(DataRow row in mTable.Rows)
            {
                tbIP.Text = row["NAME"].ToString();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
