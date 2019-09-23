using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

namespace Учет_цистерн
{
    public partial class RenderedServiceForm : Form
    {
        private ProgressBar progBar;
        int SelectItemRow;
        int SelectBrigadeID;
        int SelectCarriageID;
        int SelectStationID;
        int SelectProductID;
        int SelectServiceCostID;
        int Rows;

        DataTable dataTable = new DataTable();
        BindingSource source = new BindingSource();

        public RenderedServiceForm(ProgressBar progressBar1)
        {
            InitializeComponent();
            FillCombobox();
            progBar = progressBar1;
        }
                
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

            String Service = "Select * from d__ServiceCost";
            DataTable ServiceDT = DbConnection.DBConnect(Service);
            comboBox5.DataSource = ServiceDT;
            comboBox5.DisplayMember = "ServiceName";
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
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dateTimePicker2.Value = startDate;
            dateTimePicker4.Value = endDate;

            //string Refresh = "dbo.GetRenderedService '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";
            //DataTable dataTable = new DataTable();
            //dataTable = DbConnection.DBConnect(Refresh);
            //source.DataSource = dataTable;
            //dataGridView1.DataSource = source;
            //dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[1].Visible = false;
            //dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
            //dataGridView1.Columns[4].Visible = false;
            //dataGridView1.Columns[5].Visible = false;
            //dataGridView1.Columns[6].Visible = false;
            progBar.Maximum = GetTotalRecords();
            backgroundWorker1.RunWorkerAsync();
            BackgroundWorker1_DoWork(null, null);

            searchToolBar1.SetColumns(dataGridView1.Columns);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Refresh = "dbo.GetRenderedService '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Refresh);
            source.DataSource = dataTable;
            dataGridView1.DataSource = source;
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
            string Add = "exec dbo.FillRenderedService '" + dateTimePicker1.Value.Date.ToString() + "'," + comboBox3.SelectedValue.ToString() + "," + comboBox1.SelectedValue.ToString() + ",'" + textBox2.Text.Trim() + "','" + textBox1.Text.Trim() + "'," + comboBox6.SelectedValue.ToString() + "," + comboBox4.SelectedValue.ToString() + "," + comboBox5.SelectedValue.ToString() + ";";
            DbConnection.DBConnect(Add);
            OkForm ok = new OkForm();
            ok.label1.Text = "Запись добавлена!";
            ok.Show();
            //MessageBox.Show("Запись добавлена!");
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
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
                string ServiceCostID = row.Cells["ServiceCostID"].Value.ToString();
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
            try
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
            catch (Exception ex)
            {
                ExceptionForm exf = new ExceptionForm();
                exf.label1.Text = "Для редактирования записи, необходимо указать строку! " + ex.Message;
                exf.Show();
                //MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Delete = "delete from d__RenderedService where ID = " + SelectItemRow;
            DbConnection.DBConnect(Delete);
            OkForm ok = new OkForm();
            ok.label1.Text = "Запись Удалена!";
            ok.Show();
            //MessageBox.Show("Запись Удалена!");
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string filter = Codeproject.RowFilterBuilder.BuildMultiColumnFilter(txtSearch.Text, ((DataTable)((BindingSource)this.dataGridView1.DataSource).DataSource).DefaultView);
                ((DataTable)((BindingSource)this.dataGridView1.DataSource).DataSource).DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                ExceptionForm exf = new ExceptionForm();
                exf.label1.Text = ex.Message;
                exf.Show();
                //MessageBox.Show(ex.Message);
            }
        }


        private void DataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            this.source.Sort = this.dataGridView1.SortString;
        }

        private void DataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            this.source.Filter = this.dataGridView1.FilterString;
        }

        private void SearchToolBar1_Search(object sender, ADGV.SearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dataGridView1.CurrentCell.ColumnIndex + 1 >= dataGridView1.ColumnCount;
                bool endrow = dataGridView1.CurrentCell.RowIndex + 1 >= dataGridView1.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dataGridView1.CurrentCell.ColumnIndex;
                    startRow = dataGridView1.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dataGridView1.CurrentCell.ColumnIndex + 1;
                    startRow = dataGridView1.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }

            DataGridViewCell c = dataGridView1.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = dataGridView1.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                dataGridView1.CurrentCell = c;
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string Refresh = "dbo.GetRenderedService '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";
            int i = 1;
            try
            {
                dataTable = DbConnection.DBConnect(Refresh);
                Application.UseWaitCursor = true; //keeps waitcursor even when the thread ends.
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                foreach (DataRow dr in dataTable.Rows)
                {
                    backgroundWorker1.ReportProgress(i);
                    Thread.Sleep(1);
                    i++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                progBar.Value = e.ProgressPercentage;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            source.DataSource = dataTable;
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;

            Application.UseWaitCursor = false;
            System.Windows.Forms.Cursor.Current = Cursors.Default;

        }

        private int GetTotalRecords()
        {
            try
            {
                string TotalRow = "dbo.TotalRowsRenderedServices '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";
                Rows = DbConnection.DatabseConnection(TotalRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Rows;
        }
    }
}
