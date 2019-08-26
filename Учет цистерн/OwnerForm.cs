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
    public partial class OwnerForm : Form
    {
        public OwnerForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        //загрузка данных в DataGridView
        private void OwnerForm_Load(object sender, EventArgs e)
        {
            string Reffresh = "SELECT ID,Name [Наименование],FullName [Полное наименование],SpecialCost [Специальная цена] FROM [Batys].[dbo].[d__Owner]";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Reffresh);
            dataGVOwner.DataSource = dataTable;
            dataGVOwner.Columns[0].Visible = false;
        }

        //Добавление
        private void btnOwnerAdd_Click(object sender, EventArgs e)
        {
            OwnerAddForm OwnerAddForm = new OwnerAddForm();
            OwnerAddForm.Show();
        }

        //Изменение
        private void btnOwnerUpdate_Click(object sender, EventArgs e)
        {
            OwnerUpdtForm OwnerUpdtForm = new OwnerUpdtForm();
            OwnerUpdtForm.SelectID = SelectItemRow;
            OwnerUpdtForm.textBox1.Text = dataGVOwner.CurrentRow.Cells[1].Value.ToString();
            OwnerUpdtForm.textBox2.Text = dataGVOwner.CurrentRow.Cells[2].Value.ToString();
            OwnerUpdtForm.textBox3.Text = dataGVOwner.CurrentRow.Cells[3].Value.ToString();
            OwnerUpdtForm.Show();
        }

        private void dataGVOwner_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGVOwner.Rows[
                    e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
            }
        }

        //Обновление
        private void btnOwnerReffresh_Click(object sender, EventArgs e)
        {
            string Reffresh = "SELECT ID,Name [Наименование],FullName [Полное наименование],SpecialCost [Специальная цена] FROM [Batys].[dbo].[d__Owner]";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Reffresh);
            dataGVOwner.DataSource = dataTable;
            dataGVOwner.Columns[0].Visible = false;
        }

        //Удаление
        private void btnOwnerDelete_Click(object sender, EventArgs e)
        {
            string message = "Вы действительно хотите удалить эту запись?";
            string title = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                string Delete = "delete from d__Owner where ID = "+SelectItemRow;
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Delete);
                MessageBox.Show("Запись удалена!");
            }
        }
    }
}
