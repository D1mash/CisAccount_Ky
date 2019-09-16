using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class addNewCargo : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public addNewCargo()
        {
            InitializeComponent();
        }

        private void addNewCargo_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "batysDataSet1.qHangling". При необходимости она может быть перемещена или удалена.
            this.qHanglingTableAdapter.Fill(this.batysDataSet1.qHangling);
        }

        private void button1_Click(object sender, EventArgs e)
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
            cmd.CommandText = "insert into d__Product values('" + textBox1.Text.Trim() + "','" + comboBox1.SelectedValue + "'," + user_aid + ",getdate())";
            //cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(dt);
            MessageBox.Show("Product added!");
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
