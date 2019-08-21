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
    public partial class AddNewStation_StationForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public AddNewStation_StationForm()
        {
            InitializeComponent();
        }

        private void button_OK_StationForm_Click(object sender, EventArgs e)
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
            cmd.CommandText = "insert into d__Station values ("+ Convert.ToInt32(textBox_Add_Code_StationForm.Text.Trim())+ ","+Convert.ToInt32(textBox_Add_Code6_StationForm.Text.Trim())+",'"+textBox_Add_Name_StationForm.Text.Trim()+"',"+user_aid+",getdate())";
            //cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(dt);
            MessageBox.Show("Станция добавлена!");
            con.Close();
        }

        private void button_Add_Cancel_StationForm_Click(object sender, EventArgs e)
        {
            AddNewStation_StationForm AddNewStation_StationForm = new AddNewStation_StationForm();
            AddNewStation_StationForm.Close();
        }
    }
}
