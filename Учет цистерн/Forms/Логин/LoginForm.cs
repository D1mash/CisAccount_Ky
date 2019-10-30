using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            FillCombobox();
            this.ControlBox = false;
            textBox2.Select();
        }

        public int UserLastID { get; set; }

        public void FillCombobox()
        {
            string Hostname = System.Environment.MachineName;
            string GetLastUser = "select top 1 ID_USER from AUDIT_USER where HostIns = '"+Hostname+"' order by DATE_IN desc";
            DataTable dt1 = DbConnection.DBConnect(GetLastUser);
            UserLastID = Convert.ToInt32(dt1.Rows[0][0]);
            string GetUser = "select * from dbo.Users";
            DataTable dt = DbConnection.DBConnect(GetUser);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "FIO";
            comboBox1.ValueMember = "AID";
            comboBox1.DataBindings.Add("SelectedValue", this, "UserLastID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string getUsers = "Select * from dbo.Users where AID = '" + comboBox1.SelectedValue.ToString() + "' and pass = '" + textBox2.Text.Trim() + "'";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(getUsers);

                if (dataTable.Rows.Count == 1)
                {
                    this.Hide();
                    string User_AID = dataTable.Rows[0][0].ToString();
                    string ExecLogin = "exec dbo.Login " + User_AID;
                    DataTable dt = new DataTable();
                    dt = DbConnection.DBConnect(ExecLogin);
                    MainForm objFrmMain = new MainForm(dataTable.Rows[0][3].ToString());
                    objFrmMain.Show();
                }
                else
                {
                    MessageBox.Show("Неправильные имя пользователя или пароль!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Clear();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
