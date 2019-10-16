using System;
using System.Data;
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
            string Reffresh = "SELECT ID,Name [Наименование],FullName [Полное наименование] FROM [Batys].[dbo].[d__Owner]";
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
            try
            {
                OwnerUpdtForm OwnerUpdtForm = new OwnerUpdtForm();
                OwnerUpdtForm.SelectID = SelectItemRow;
                OwnerUpdtForm.textBox1.Text = dataGVOwner.CurrentRow.Cells[1].Value.ToString();
                OwnerUpdtForm.textBox2.Text = dataGVOwner.CurrentRow.Cells[2].Value.ToString();
                //OwnerUpdtForm.textBox3.Text = dataGVOwner.CurrentRow.Cells[3].Value.ToString();
                OwnerUpdtForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
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
            string Reffresh = "SELECT ID,Name [Наименование],FullName [Полное наименование] FROM [Batys].[dbo].[d__Owner]";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Reffresh);
            dataGVOwner.DataSource = dataTable;
            dataGVOwner.Columns[0].Visible = false;
        }

        //Удаление
        private void btnOwnerDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string CheckReference = "select * from d__Carriage where Owner_ID = " + SelectItemRow;
                string Delete = "delete from d__Owner where ID = " + SelectItemRow;
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(CheckReference);
                if (dt.Rows.Count == 0)
                {
                    DbConnection.DBConnect(Delete);
                    MessageBox.Show("Запись удалена!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Невозможно удалить, т.к. собственник привязан в таблице Вагоны", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
