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
using System.Data.Sql;

namespace Учет_цистерн
{
    public partial class LoginForm : Form
    {
        public static string constring = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        SqlConnection con = new SqlConnection(constring);
        //SqlCommand cmd;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string getUsers = "Select * from dbo.Users where FIO = '" + comboBox1.Text.Trim() + "' and pass = '" + textBox2.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(getUsers, con);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if(dtbl.Rows.Count == 1)
            {
                this.Hide();
                string User_AID = dtbl.Rows[0][0].ToString();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "exec dbo.Login "+User_AID;
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);
                MainForm objFrmMain = new MainForm(dtbl.Rows[0][3].ToString());
                objFrmMain.Show();
            }
            else
            {
                MessageBox.Show("Неправильные имя пользователя или пароль.");
                textBox2.Clear();
            }
        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "batysDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.batysDataSet.Users);
            textBox2.Select();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
