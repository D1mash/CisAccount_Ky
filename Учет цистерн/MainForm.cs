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
using System.Diagnostics;

namespace Учет_цистерн
{
    public partial class MainForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public MainForm(string FIO)
        {
            InitializeComponent();
            //IntPtr UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Token;
            this.Text = "Учет вагонов-цистерн. Батыс Петролеум ТОО - "+FIO;
            
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

        private void button7_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close this window?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string get_user_aid = "select dbo.get_user_aid() S";
                SqlDataAdapter sda = new SqlDataAdapter(get_user_aid, con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                string user_aid = dtbl.Rows[0][0].ToString();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE AUDIT_USER SET DATE_OUT = GETDATE(), IS_DEAD = 1 WHERE ID_SESSION = @@spid AND ID_USER ="+user_aid;
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter sa = new SqlDataAdapter(cmd);
                    sa.Fill(dt);

                con.Close();
                Environment.Exit(0);
            }
            else
            {
                // Do something  
            }
        }
    }
}
