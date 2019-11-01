using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeWright.UI.Forms;

namespace Учет_цистерн
{
    public partial class RenderedServiceForm : Form
    {
        private TabControlExtra tabControlExtra;
        private ToolStripProgressBar progBar;
        private ToolStripLabel TlStpLabel;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn6;
        int SelectItemRow;
        int SelectBrigadeID;
        int SelectCarriageID;
        int SelectStationID;
        int SelectProductID;
        int SelectServiceCostID;
        int Rows;


        //DataTable dataTable = new DataTable();
        BindingSource source = new BindingSource();

        public RenderedServiceForm(ToolStripProgressBar progressBar1, ToolStripLabel toolStripLabel, Button button1, Button button2, Button button3, Button button4, Button button6, TabControlExtra tabControl1)
        {
            InitializeComponent();
            FillCombobox();
            progBar = progressBar1;
            TlStpLabel = toolStripLabel;
            btn1 = button1;
            btn2 = button2;
            btn3 = button3;
            btn4 = button4;
            btn6 = button6;
            tabControlExtra = tabControl1;
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

            textBox4.Visible = false;
            textBox5.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            panel9.Visible = false;

            progBar.Visible = false;

            //if (!backgroundWorker1.IsBusy)
            //{
            //    progBar.Visible = true;
            //    progBar.Maximum = GetTotalRecords();
            //    string Refresh = "dbo.GetRenderedService '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";
            //    backgroundWorker1.RunWorkerAsync(Refresh);
            //}
            
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            decimal sum = 0;
            int Count = 0;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[15].Value != string.Empty)
                {
                    sum += Convert.ToDecimal(this.dataGridView1[15, i].Value);
                }
                Count = dataGridView1.RowCount;
            }

            //Количество вагонов
            textBox4.Text = "Всего строк: " + Count.ToString();
            int Xdgvx = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
            textBox4.Width = this.dataGridView1.Columns[7].Width+1;
            Xdgvx = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
            textBox4.Location = new Point(Xdgvx, this.dataGridView1.Height - (textBox4.Height - 15));
            textBox4.Visible = true;

            int Xdgvx_Panel1 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
            panel1.Width = this.dataGridView1.Columns[8].Width+1;
            Xdgvx_Panel1 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
            panel1.Location = new Point(Xdgvx_Panel1, this.dataGridView1.Height - (panel1.Height - 15));
            panel1.Visible = true;

            int Xdgvx_Panel2 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
            panel2.Width = this.dataGridView1.Columns[9].Width+1;
            Xdgvx_Panel2 = this.dataGridView1.GetCellDisplayRectangle(9, -1, true).Location.X;
            panel2.Location = new Point(Xdgvx_Panel2, this.dataGridView1.Height - (panel2.Height - 15));
            panel2.Visible = true;

            int Xdgvx_Panel3 = this.dataGridView1.GetCellDisplayRectangle(9, -1, true).Location.X;
            panel3.Width = this.dataGridView1.Columns[9].Width+1;
            Xdgvx_Panel3 = this.dataGridView1.GetCellDisplayRectangle(10, -1, true).Location.X;
            panel3.Location = new Point(Xdgvx_Panel3, this.dataGridView1.Height - (panel3.Height - 15));
            panel3.Visible = true;

            int Xdgvx_Panel4 = this.dataGridView1.GetCellDisplayRectangle(10, -1, true).Location.X;
            panel4.Width = this.dataGridView1.Columns[11].Width+1;
            Xdgvx_Panel4 = this.dataGridView1.GetCellDisplayRectangle(11, -1, true).Location.X;
            panel4.Location = new Point(Xdgvx_Panel4, this.dataGridView1.Height - (panel4.Height - 15));
            panel4.Visible = true;

            int Xdgvx_Panel5 = this.dataGridView1.GetCellDisplayRectangle(11, -1, true).Location.X;
            panel5.Width = this.dataGridView1.Columns[12].Width+1;
            Xdgvx_Panel5 = this.dataGridView1.GetCellDisplayRectangle(12, -1, true).Location.X;
            panel5.Location = new Point(Xdgvx_Panel5, this.dataGridView1.Height - (panel5.Height - 15));
            panel5.Visible = true;

            int Xdgvx_Panel6 = this.dataGridView1.GetCellDisplayRectangle(12, -1, true).Location.X;
            panel6.Width = this.dataGridView1.Columns[13].Width+1;
            Xdgvx_Panel6 = this.dataGridView1.GetCellDisplayRectangle(13, -1, true).Location.X;
            panel6.Location = new Point(Xdgvx_Panel6, this.dataGridView1.Height - (panel6.Height - 15));
            panel6.Visible = true;

            int Xdgvx_Panel7 = this.dataGridView1.GetCellDisplayRectangle(13, -1, true).Location.X;
            panel7.Width = this.dataGridView1.Columns[14].Width+1;
            Xdgvx_Panel7 = this.dataGridView1.GetCellDisplayRectangle(14, -1, true).Location.X;
            panel7.Location = new Point(Xdgvx_Panel7, this.dataGridView1.Height - (panel7.Height - 15));
            panel7.Visible = true;

            //Сумма услуг
            textBox5.Text = "Сумма: " + sum.ToString();
            int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(14, -1, true).Location.X;
            textBox5.Width = this.dataGridView1.Columns[15].Width+1;
            Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(15, -1, true).Location.X;
            textBox5.Location = new Point(Xdgvx1, this.dataGridView1.Height - (textBox5.Height - 15));
            textBox5.Visible = true;

            int Xdgvx_Panel8 = this.dataGridView1.GetCellDisplayRectangle(15, -1, true).Location.X;
            panel8.Width = this.dataGridView1.Columns[16].Width+2;
            Xdgvx_Panel8 = this.dataGridView1.GetCellDisplayRectangle(16, -1, true).Location.X;
            panel8.Location = new Point(Xdgvx_Panel8, this.dataGridView1.Height - (panel8.Height - 15));
            panel8.Visible = true;

            //int Xdgvx_Panel9 = this.dataGridView1.GetCellDisplayRectangle(-1, -1, true).Location.X;
            panel9.Width = this.dataGridView1.RowHeadersWidth - 3;
            panel9.Location = new Point(5, this.dataGridView1.Height - (panel9.Height - 15));
            //panel9.Location = new Point(Xdgvx_Panel9, this.dataGridView1.Height - (panel9.Height - 15));
            panel9.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                progBar.Visible = false;

                if (!backgroundWorker1.IsBusy)
                {
                    int yes = 1;
                    btn1.Enabled = false;
                    btn2.Enabled = false;
                    btn3.Enabled = false;
                    btn4.Enabled = false;
                    btn6.Enabled = false;
                    tabControlExtra.DisplayStyleProvider.ShowTabCloser = false;
                    progBar.Visible = true;
                    progBar.Maximum = GetTotalRecords();
                    string RefreshGF = "dbo.GetRenderedServiceGlobalFilter '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "','" + yes + "'";
                    backgroundWorker1.RunWorkerAsync(RefreshGF);
                }
            }
            else
            {
                if (!backgroundWorker1.IsBusy)
                {
                    btn1.Enabled = false;
                    btn2.Enabled = false;
                    btn3.Enabled = false;
                    btn4.Enabled = false;
                    btn6.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    tabControlExtra.DisplayStyleProvider.ShowTabCloser = false;
                    progBar.Visible = true;
                    progBar.Maximum = GetTotalRecords();
                    string Refresh = "dbo.GetRenderedService '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";
                    backgroundWorker1.RunWorkerAsync(Refresh);
                }
            }

            searchToolBar1.SetColumns(dataGridView1.Columns);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Add = "exec dbo.FillRenderedService '" + dateTimePicker1.Value.Date.ToString() + "'," + comboBox3.SelectedValue.ToString() + "," + comboBox1.SelectedValue.ToString() + ",'" + textBox2.Text.Trim() + "','" + textBox1.Text.Trim() + "'," + comboBox6.SelectedValue.ToString() + "," + comboBox4.SelectedValue.ToString() + "," + comboBox5.SelectedValue.ToString() + ";";
            DbConnection.DBConnect(Add);
            MessageBox.Show("Запись добавлена!","",MessageBoxButtons.OK,MessageBoxIcon.Information);         
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
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Delete = "delete from d__RenderedService where ID = " + SelectItemRow;
            DbConnection.DBConnect(Delete);
            MessageBox.Show("Запись Удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);          
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


        public int GetTotalRecords()
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

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Application.UseWaitCursor = true; //keeps waitcursor even when the thread ends.
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            string Query = (string)e.Argument;
            //string Refresh = "dbo.GetRenderedService '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";
            
            int i = 1;
            try
            {
                DataTable dataTable = DbConnection.DBConnect(Query);
                foreach (DataRow dr in dataTable.Rows)
                {
                    backgroundWorker1.ReportProgress(i);
                    Thread.Sleep(5);
                    i++;
                }
                e.Result = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
            {
                progBar.Value = e.ProgressPercentage;
                TlStpLabel.Text = "Обработка строки.. " + e.ProgressPercentage.ToString() + " из " + GetTotalRecords();
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                TlStpLabel.Text = "Ошибка" + e.Error.Message;
            }
            else
            {
                Application.UseWaitCursor = false;
                System.Windows.Forms.Cursor.Current = Cursors.Default;

                source.DataSource = e.Result;
                dataGridView1.DataSource = source;
                dataGridView1.RowHeadersWidth = 15;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;

                progBar.Visible = false;
                btn1.Enabled = true;
                btn2.Enabled = true;
                btn3.Enabled = true;
                btn4.Enabled = true;
                btn6.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                tabControlExtra.DisplayStyleProvider.ShowTabCloser = true;
                TlStpLabel.Text = "Данные загружены...";
            }
        }
    }
}
