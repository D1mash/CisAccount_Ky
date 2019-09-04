using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            string Get = "select ID, Name [Наименование] from d__Service";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Get);
            dataGridView1.DataSource = dataTable;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Get = "select ID, Name [Наименование] from d__Service";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Get);
            dataGridView1.DataSource = dataTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Вы действительно хотите удалить эту запись?";
            string title = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                string Delete = "delete from d__Service where ID = " + SelectItemRow;
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Delete);
                MessageBox.Show("Запись удалена!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[
                    e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceAddForm serviceAddForm = new ServiceAddForm();
            serviceAddForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceUpdtForm ServiceUpdtForm = new ServiceUpdtForm();
            ServiceUpdtForm.SelectID = SelectItemRow;
            ServiceUpdtForm.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            ServiceUpdtForm.Show();
        }
    }
}
