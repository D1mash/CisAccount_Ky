using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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

        private void btn_add_station_form_Click(object sender, EventArgs e)
        {
            AddNewStation_StationForm AddNewStation_StationForm = new AddNewStation_StationForm();
            AddNewStation_StationForm.Show();
        }

        private void dataGridView1_Station_Form_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void btn_upd_station_form_Click(object sender, EventArgs e)
        {
            UpdtCurrentStation_StationForm UpdtCurrentStation_StationForm = new UpdtCurrentStation_StationForm();
            UpdtCurrentStation_StationForm.textBox_Updt_Name_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[1].Value.ToString();
            UpdtCurrentStation_StationForm.textBox_Updt_Code_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[2].Value.ToString();
            UpdtCurrentStation_StationForm.textBox_Updt_Code6_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[3].Value.ToString();
            UpdtCurrentStation_StationForm.SelectStationID_Method = SelectItemRow;
            UpdtCurrentStation_StationForm.Show();
        }

        private void btn_refsh_station_form_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(connectionString);
            //con.Open();
            string GetStation = "select ID, Name as [Наименование], Code, Code6 from d__Station";
            //SqlDataAdapter sda = new SqlDataAdapter(GetStation, con);
            DataTable dTl = new DataTable();
            dTl = DbConnection.DBConnect(GetStation);
            //sda.Fill(dtbl);
            dataGridView_Station_Form.DataSource = dTl;
            dataGridView_Station_Form.Columns[0].Visible = false;
            //con.Close();
        }

        private void btn_dlt_station_form_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(connectionString);
            //con.Open();
            string DeleteCurrentStation = "delete from d__Station where ID = " + SelectItemRow;
            //SqlDataAdapter sda = new SqlDataAdapter(DeleteCurrentStation, con);
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(DeleteCurrentStation);
            //sda.Fill(dtbl);
            //con.Close();
            MessageBox.Show("Станция удалена!");
        }
    }
}
