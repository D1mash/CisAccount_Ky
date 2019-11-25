using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using NLog;

namespace Учет_цистерн
{
    public partial class LoginForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            try
            {
                string Hostname = System.Environment.MachineName;
                string GetLastUser = "select top 1 ID_USER from AUDIT_USER where HostIns = '" + Hostname + "' order by DATE_IN desc";
                DataTable dt1 = DbConnection.DBConnect(GetLastUser);

                string GetUser = "select * from dbo.Users";
                DataTable dt = DbConnection.DBConnect(GetUser);

                if (dt1.Rows.Count > 0)
                {
                    UserLastID = Convert.ToInt32(dt1.Rows[0][0]);
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "FIO";
                    comboBox1.ValueMember = "AID";
                    comboBox1.DataBindings.Add("SelectedValue", this, "UserLastID", true, DataSourceUpdateMode.OnPropertyChanged);
                }
                else
                {
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "FIO";
                    comboBox1.ValueMember = "AID";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "LoginForm_FillCombobox_SQL");
            }
            catch
            (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "LoginForm_FillCombobox");
                logger.Info(ex, "LoginForm_FillCombobox");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean result;
                string aid;
                string password;
                string FIO;

                aid = comboBox1.SelectedValue.ToString();
                FIO = comboBox1.Text;
                password = textBox2.Text.Trim();

                string CheckUser = "select dbo.CheckUser (" + aid + ", '" + password + "')";
                DataTable dt = DbConnection.DBConnect(CheckUser);
                result = Convert.ToBoolean(dt.Rows[0][0].ToString());
                if(result == true)
                {
                    this.Hide();
                    string ExecLogin = "exec dbo.Login " + aid;
                    DataTable ExecLoginDt = new DataTable();
                    ExecLoginDt = DbConnection.DBConnect(ExecLogin);
                    MainForm objFrmMain = new MainForm(FIO);
                    objFrmMain.Show();
                }
                else
                {
                    MessageBox.Show("Неправильные имя пользователя или пароль!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Clear();
                }
                //string getUsers = "Select * from dbo.Users where AID = '" + comboBox1.SelectedValue.ToString() + "' and pass = '" + textBox2.Text.Trim() + "'";
                //DataTable dataTable = new DataTable();
                //dataTable = DbConnection.DBConnect(getUsers);

                //if (dataTable.Rows.Count == 1)
                //{
                //    this.Hide();
                //    string User_AID = dataTable.Rows[0][0].ToString();
                //    string ExecLogin = "exec dbo.Login " + User_AID;
                //    DataTable dt = new DataTable();
                //    dt = DbConnection.DBConnect(ExecLogin);
                //    MainForm objFrmMain = new MainForm(dataTable.Rows[0][3].ToString());
                //    objFrmMain.Show();
                //}
                //else
                //{
                //    MessageBox.Show("Неправильные имя пользователя или пароль!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    textBox2.Clear();
                //}
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "LoginForm_Button1_Click_SQL");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(exp, "LoginForm_Button1_Click");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
