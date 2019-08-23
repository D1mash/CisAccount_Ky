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
    public partial class BrigadeForm : Form
    {
        public BrigadeForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        private void BtnBrigadeAdd_Click(object sender, EventArgs e)
        {
            BrigadeAddForm brigadeAddForm = new BrigadeAddForm();
            brigadeAddForm.Show();
        }

        private void BtnBrigadeUpdate_Click(object sender, EventArgs e)
        {
            BrigadeUpdateForm brigadeUpdateForm = new BrigadeUpdateForm();
            brigadeUpdateForm.SelectID = SelectItemRow;
            brigadeUpdateForm.textBox1.Text = dataGVBrigade.CurrentRow.Cells[1].Value.ToString();
            brigadeUpdateForm.textBox2.Text = dataGVBrigade.CurrentRow.Cells[2].Value.ToString();
            brigadeUpdateForm.textBox3.Text = dataGVBrigade.CurrentRow.Cells[3].Value.ToString();
            // надо дописать для чекбокса
            brigadeUpdateForm.Show();
        }

        private void BtnBrigadeDelete_Click(object sender, EventArgs e)
        {
            string DeleteCurrentProduct = "delete from d__Brigade where ID = " + SelectItemRow;
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(DeleteCurrentProduct);
            MessageBox.Show("Продукт удалён!");
        }

        private void BtnBrigadeReffresh_Click(object sender, EventArgs e)
        {
            string GetProduct = "SELECT * FROM [Batys].[dbo].[d__Brigade]";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetProduct);
            dataGVBrigade.DataSource = dataTable;
        }

        private void DataGVBrigade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGVBrigade.Rows[
                    e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
            }
        }
    }
}
