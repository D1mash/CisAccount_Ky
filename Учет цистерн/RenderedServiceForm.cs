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
    public partial class RenderedServiceForm : Form
    {
        public RenderedServiceForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        int SelectItemRow;
        int SelectBrigadeID;
        int SelectCarriageID;
        int SelectStationID;
        int SelectProductID;
        int SelectServiceCostID;

        private void FillCombobox()
        {
            String Carriage = "Select * from d__Carriage";
            DataTable CarDT = DbConnection.DBConnect(Carriage);
            comboBox1.DataSource = CarDT;
            comboBox1.DisplayMember = "CarNumber";
            comboBox1.ValueMember = "ID";
            //comboBox1.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Owner = "Select * from d__Owner";
            DataTable OwnerDT = DbConnection.DBConnect(Owner);
            comboBox2.DataSource = OwnerDT;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
            //comboBox2.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Brigade = "Select * from d__Brigade";
            DataTable BrigadeDT = DbConnection.DBConnect(Brigade);
            comboBox3.DataSource = BrigadeDT;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "ID";
            //comboBox3.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Product = "Select * from d__Product";
            DataTable ProductDT = DbConnection.DBConnect(Product);
            comboBox4.DataSource = ProductDT;
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "ID";
            //comboBox1.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Service = "Select * from d__Service";
            DataTable ServiceDT = DbConnection.DBConnect(Service);
            comboBox5.DataSource = ServiceDT;
            comboBox5.DisplayMember = "Name";
            comboBox5.ValueMember = "ID";
            //comboBox1.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Station = "Select * from d__Station";
            DataTable StationDT = DbConnection.DBConnect(Station);
            comboBox6.DataSource = StationDT;
            comboBox6.DisplayMember = "Name";
            comboBox6.ValueMember = "ID";
            //comboBox1.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void RenderedServiceForm_Load(object sender, EventArgs e)
        {
            string Refresh = "dbo.GetRenderedService";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Refresh);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Refresh = "dbo.GetRenderedService";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Refresh);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Add = "exec dbo.FillRenderedService '"+ dateTimePicker1.Value.Date.ToString()+ "'," + comboBox3.SelectedValue.ToString() + "," +comboBox1.SelectedValue.ToString()+",'"+textBox2.Text.Trim()+"','"+textBox1.Text.Trim()+"',"+comboBox6.SelectedValue.ToString()+","+comboBox4.SelectedValue.ToString()+","+comboBox5.SelectedValue.ToString();
            DbConnection.DBConnect(Add);
            MessageBox.Show("Запись добавлена!");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[
                    e.RowIndex];
                string Id = row.Cells["RenderedID"].Value.ToString();
                string BrigadeID = row.Cells["BrigadeID"].Value.ToString();
                string CarriageID = row.Cells["CarriageID"].Value.ToString();
                string StationID = row.Cells["StationID"].Value.ToString();
                string ProductID = row.Cells["ProductID"].Value.ToString();
                string ServiceCostID = row.Cells["ServiceID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectBrigadeID = Convert.ToInt32(BrigadeID);
                SelectCarriageID = Convert.ToInt32(CarriageID);
                SelectStationID = Convert.ToInt32(StationID);
                SelectProductID = Convert.ToInt32(ProductID);
                SelectServiceCostID = Convert.ToInt32(ServiceCostID);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RenderedServiceUpdtForm RenderedServiceUpdtForm = new RenderedServiceUpdtForm();
            RenderedServiceUpdtForm.SelectID = SelectItemRow;
            RenderedServiceUpdtForm.SelectBrigadeID = SelectBrigadeID;
            RenderedServiceUpdtForm.SelectCarriageID = SelectCarriageID;
            RenderedServiceUpdtForm.SelectStationID = SelectStationID;
            RenderedServiceUpdtForm.SelectProductID = SelectProductID;
            RenderedServiceUpdtForm.SelectServiceID = SelectServiceCostID;
            RenderedServiceUpdtForm.textBox1.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            RenderedServiceUpdtForm.textBox2.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            RenderedServiceUpdtForm.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            RenderedServiceUpdtForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Delete = "delete from d__RenderedService where ID = " + SelectItemRow ;
            DbConnection.DBConnect(Delete);
            MessageBox.Show("Запись добавлена!");
        }
    }
}
