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
        int Active;

        private void BtnBrigadeAdd_Click(object sender, EventArgs e)
        {
            BrigadeAddForm brigadeAddForm = new BrigadeAddForm();
            brigadeAddForm.Show();
        }

        private void BtnBrigadeUpdate_Click(object sender, EventArgs e)
        {
            string GetActiveBrigade = "select Active from d__Brigade where ID = " + SelectItemRow;
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetActiveBrigade);
            Active = Convert.ToInt32(dataTable.Rows[0][0]);

            BrigadeUpdateForm brigadeUpdateForm = new BrigadeUpdateForm
            {
                SelectID = SelectItemRow
            };
            brigadeUpdateForm.textBox1.Text = dataGVBrigade.CurrentRow.Cells[1].Value.ToString();
            brigadeUpdateForm.textBox2.Text = dataGVBrigade.CurrentRow.Cells[2].Value.ToString();
            brigadeUpdateForm.textBox3.Text = dataGVBrigade.CurrentRow.Cells[3].Value.ToString();
            if (Active == 1)
            {
                brigadeUpdateForm.checkBox1.Checked = true;
            }
            else
            {
                brigadeUpdateForm.checkBox1.Checked = false;
            }
            brigadeUpdateForm.Show();
        }

        private void BtnBrigadeDelete_Click(object sender, EventArgs e)
        {
            string Delete = "delete from d__Brigade where ID = " + SelectItemRow;
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Delete);
            MessageBox.Show("Запись удалена!");
        }

        private void BtnBrigadeReffresh_Click(object sender, EventArgs e)
        {
            string Reffresh = "SELECT ID,Name [Имя],Surname [Фамилия],Lastname [Отчество],FIO [ФИО],Active [Активный] FROM [Batys].[dbo].[d__Brigade]";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Reffresh);
            dataGVBrigade.DataSource = dataTable;
            dataGVBrigade.Columns[0].Visible = false;
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

        private void BrigadeForm_Load(object sender, EventArgs e)
        {
            string Reffresh = "SELECT ID,Name [Имя],Surname [Фамилия],Lastname [Отчество],FIO [ФИО],Active [Активный] FROM [Batys].[dbo].[d__Brigade]";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Reffresh);
            dataGVBrigade.DataSource = dataTable;
            dataGVBrigade.Columns[0].Visible = false;
        }
    }
}
