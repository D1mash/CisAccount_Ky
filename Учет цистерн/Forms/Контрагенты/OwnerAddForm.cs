using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class OwnerAddForm : Form
    {
        public OwnerAddForm()
        {
            InitializeComponent();
            textBox3.Visible = false;
            label3.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string AddNewOwner = "insert into d__Owner " +
                                 "values ('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "',NULL)";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(AddNewOwner);
                this.Close();
                MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OwnerForm main = this.Owner as OwnerForm;
                main.btnOwnerReffresh_Click(null,null);
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
