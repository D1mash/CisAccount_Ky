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
    public partial class CarriageForm : Form
    {
        public CarriageForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            CarriageAddForm carriageAddForm = new CarriageAddForm();
            carriageAddForm.Show();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            string GetCarriage = "Select dc.*, do.* From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID  ";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetCarriage);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void CarriageForm_Load(object sender, EventArgs e)
        {
            string GetCarriage = "Select dc.*, do.* From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID  ";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetCarriage);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[
                    e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string message = "Вы действительно хотите удалить эту запись?";
            string title = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                string Delete = "delete from d__Carriage where ID = " + SelectItemRow;
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Delete);
                MessageBox.Show("Запись удалена!");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            CarriageUpdateForm carriageUpdateForm = new CarriageUpdateForm();
            carriageUpdateForm.SelectID = SelectItemRow;
            carriageUpdateForm.Show();
        }
    }
}
