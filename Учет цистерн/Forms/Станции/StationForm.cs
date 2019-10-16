using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class StationForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public StationForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        private void btn_add_station_form_Click_1(object sender, EventArgs e)
        {
            AddNewStation_StationForm AddNewStation_StationForm = new AddNewStation_StationForm();
            AddNewStation_StationForm.Show();
        }

        private void dataGridView_Station_Form_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView_Station_Form.Rows[e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
            }
        }

        private void StationForm_Load(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(connectionString);
            //con.Open();
            string GetStation = "select ID, Name as [Наименование], Code, Code6 from d__Station";
            //SqlDataAdapter sda = new SqlDataAdapter(GetStation, con);
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetStation);
            //sda.Fill(dtbl);
            dataGridView_Station_Form.DataSource = dataTable;
            dataGridView_Station_Form.Columns[0].Visible = false;
            //con.Close();
        }

        private void btn_refsh_station_form_Click_1(object sender, EventArgs e)
        {
            string GetStation = "select ID, Name as [Наименование], Code, Code6 from d__Station";
            DataTable dTl = new DataTable();
            dTl = DbConnection.DBConnect(GetStation);
            dataGridView_Station_Form.DataSource = dTl;
            dataGridView_Station_Form.Columns[0].Visible = false;
        }

        private void btn_dlt_station_form_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string CheckReference = "select * from d__RenderedServiceHead where Station_ID = " + SelectItemRow;
                string DeleteCurrentStation = "delete from d__Station where ID = " + SelectItemRow;
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(CheckReference);
                if (dt.Rows.Count == 0)
                {
                    DbConnection.DBConnect(DeleteCurrentStation);
                    MessageBox.Show("Запись удалена!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Невозможно удалить, т.к. станция привязана в таблице Обработанные вагоны", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btn_upd_station_form_Click(object sender, EventArgs e)
        {
            try
            {
                UpdtCurrentStation_StationForm UpdtCurrentStation_StationForm = new UpdtCurrentStation_StationForm();
                UpdtCurrentStation_StationForm.textBox_Updt_Name_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[1].Value.ToString();
                UpdtCurrentStation_StationForm.textBox_Updt_Code_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[2].Value.ToString();
                UpdtCurrentStation_StationForm.textBox_Updt_Code6_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[3].Value.ToString();
                UpdtCurrentStation_StationForm.SelectStationID_Method = SelectItemRow;
                UpdtCurrentStation_StationForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
