using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Пользователи
{
    public partial class UpdateUserForm : Form
    {
        int selectID;
        string role;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        public int SelectRoleId { get; set; }

        public UpdateUserForm(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        private void Fillcombobox()
        {
            string Role = "select * from d__Role";
            DataTable dt = DbConnection.DBConnect(Role);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectRoleId", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void UpdateUserForm_Load(object sender, EventArgs e)
        {
            Fillcombobox();
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = (checkBox5.CheckState == CheckState.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Update = "exec dbo.UpdateUser '" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','" + textBox4.Text.Trim() + "','" + textBox1.Text.Trim() + "'," + comboBox1.SelectedValue.ToString() + "," + selectID;
                DbConnection.DBConnect(Update);
                MessageBox.Show("Пользователь изменён!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
