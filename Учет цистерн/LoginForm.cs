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
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string getUsers = "Select * from dbo.Users where FIO = '" + comboBox1.Text.Trim() + "' and pass = '" + textBox2.Text.Trim() + "'";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(getUsers);

            if(dataTable.Rows.Count == 1)
            {
                this.Hide();
                string User_AID = dataTable.Rows[0][0].ToString();
                string ExecLogin = "exec dbo.Login "+User_AID;
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(ExecLogin);
                MainForm objFrmMain = new MainForm(dataTable.Rows[0][3].ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
