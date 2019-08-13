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

namespace Учет_цистерн
{
    public partial class MainForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public MainForm(string AID)
        {
            InitializeComponent();
            //IntPtr UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Token;
            this.Text = "Учет вагонов-цистерн. Батыс Петролеум ТОО - "+AID;
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            if(con.State==System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.Users";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qCargo qCargoForm = new qCargo();
            qCargoForm.Show();
        }
    }
}
