using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace DisconnectedArch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //You have to change your win autentication's pwd and userid in app.config
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private void btnEmp_Click(object sender, EventArgs e)
        {
            string command = "SELECT EmployeeID,FirstName,LastName,HomePhone,Photo FROM Employees";
            string name = "Employees_table";
            SqlBasicsDataSet(command,name);

        }

        private void btnCat_Click(object sender, EventArgs e)
        {
            string command = "Select * FROM Categories";
            string name = "Categories_table";
            SqlBasicDataTable(command, name);
        }


        #region DataSet
        void SqlBasicsDataSet(string command,string tableName)
        {

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command, ConfigurationManager.ConnectionStrings["con"].ConnectionString);
                
                if (ds.Tables[tableName] == null)
                {   
                    da.Fill(ds, tableName);
                    // dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = tableName;
                }
                else
                {
                    
                    dataGridView1.DataSource = ds.Tables[tableName];
                   // dataGridView1.DataMember = dt.TableName;
                }
                da.Dispose();

            }
            
            catch(SystemException sye)
            {
                MessageBox.Show(sye.Message);
            }
            
            
        }
        #endregion

        #region DataTable

        
        void SqlBasicDataTable(string command, string tableName)
        {

            SqlDataAdapter da = new SqlDataAdapter(command, ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            try
            {
                if (dt.TableName!=tableName)
                {
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dt.TableName= tableName;
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    
                }
                da.Dispose();
                
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
        }
        #endregion
    }
}
