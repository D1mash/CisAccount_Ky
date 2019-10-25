using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.заявки_на_обработку
{
    public partial class OrderAddForm : Form
    {
        TradeWright.UI.Forms.TabControlExtra TabControlExtra;
        public OrderAddForm(TradeWright.UI.Forms.TabControlExtra tabControlExtra)
        {
            InitializeComponent();
            TabControlExtra = tabControlExtra;
        }
        public string GetStatus { get; set; }
        public string GetDate { get; set; }
        public int SelectedID { get; set; }
        int SelectItemRow;
        //Вставить вагоны в таблицу
        private void button4_Click(object sender, EventArgs e)
        {
            string GetID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
            DataTable dt = DbConnection.DBConnect(GetID);
            string Insert = "exec dbo.InsertCarnumber '" + Clipboard.GetText() + "'," + dt.Rows[0][0].ToString();
            DbConnection.DBConnect(Insert);
            dataGridView1.DataSource = null;
            GetData();
        }
        //Кнопка обновить
        private void button6_Click(object sender, EventArgs e)
        {
            UpdateBody();
        }
        //метод для обновления
        private void UpdateBody()
        {
            dataGridView1.DataSource = null;
            GetData();
        }
        //Добавить строку в таблицу
        private void button3_Click(object sender, EventArgs e)
        {
            string GetID = "select ID from d__RenderedServiceHead where NUM = '" + GetStatus + "'";
            DataTable dt = DbConnection.DBConnect(GetID);
            string ID = dt.Rows[0][0].ToString();
            string RenrederServiceBody = "exec dbo.FillRenderedServiceBody NULL, NULL, NULL," + ID;
            DbConnection.DBConnect(RenrederServiceBody);
            UpdateBody();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string GetID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
            DataTable dt = DbConnection.DBConnect(GetID);

            string CheckProduct = "select Carnumber from d__RenderedServiceBody where Product_ID is NULL and Head_ID = " + dt.Rows[0][0].ToString();
            DataTable dt1 = DbConnection.DBConnect(CheckProduct);

            if (dt1.Rows.Count > 0)
            {
                MessageBox.Show("У вагона " + dt1.Rows[0][0].ToString() + " не выбран продукт!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string RenrederServiceHead = "exec dbo.RenderedServiceCreate '" + comboBox1.SelectedValue.ToString() + "','" + textBox1.Text.Trim() + "','" + dateTimePicker1.Value.Date.ToString() + "','" + comboBox2.SelectedValue.ToString() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','" + comboBox3.SelectedValue.ToString() + "',1,1";
                DbConnection.DBConnect(RenrederServiceHead);
                UpdateBody();
                TabControlExtra.TabPages.Remove(TabControlExtra.SelectedTab);
            }
        }
        //Удаление строки из таблицы в документе
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить выделенную запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string GetID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
                DataTable dt = DbConnection.DBConnect(GetID);
                string DeleteRow = "delete from d__RenderedServiceBody where ID = " + SelectItemRow + " and Head_ID = " + dt.Rows[0][0].ToString();
                DbConnection.DBConnect(DeleteRow);
                string DeleteTemp = "delete from temp where body_id = " + SelectItemRow;
                DbConnection.DBConnect(DeleteRow);
                MessageBox.Show("Запись удалена!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateBody();
            }
        }

        private void OrderAddForm_Load(object sender, EventArgs e)
        {
            GetData();
            FillCombobox();
            string GetDocStatus = "select ds.Name, u.FIO from d__RenderedServiceHead h left join dbo.Users u on u.AID = h.ID_USER_INS left join dbo.d__Docstate ds on ds.ID = h.ID_DocState where h.NUM = '" + GetStatus + "'";
            DataTable GetDocStatusDT = DbConnection.DBConnect(GetDocStatus);
            label9.Text = GetDocStatusDT.Rows[0][0].ToString();
            label10.Text = GetDocStatusDT.Rows[0][1].ToString();
        }
        //Получаю данные из таблицы d__RenderedServiceBody, также комбобоксы по внешним ключам - продукты и услуги
        private void GetData()
        {
            string ID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
            DataTable GetIDDT = DbConnection.DBConnect(ID);
            string GetDocument = "exec dbo.GetRenderedServiceBody_v1 " + GetIDDT.Rows[0][0].ToString();
            DataTable GetDocumentDT = DbConnection.DBConnect(GetDocument);
            dataGridView1.DataSource = GetDocumentDT;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            //услуги
            //dataGridView1.Columns[3].Visible = false;
            //Продукты
            string GetProduct = "select * from d__Product";
            DataTable GetProductDT = DbConnection.DBConnect(GetProduct);
            DataGridViewComboBoxColumn ProductComboBox = new DataGridViewComboBoxColumn();
            ProductComboBox.DataSource = GetProductDT;
            ProductComboBox.FlatStyle = FlatStyle.Flat;
            ProductComboBox.HeaderText = "Продукт";
            ProductComboBox.Name = "ProductComboBox";
            ProductComboBox.DisplayMember = "Name";
            ProductComboBox.ValueMember = "ID";
            ProductComboBox.DataPropertyName = "Product_ID";
            dataGridView1.Columns.Add(ProductComboBox);

            //Услуги
            //string GetService = "select * from d__ServiceCost";
            //DataTable GetServiceDT = DbConnection.DBConnect(GetService);
            //DataGridViewComboBoxColumn ServiceComboBox = new DataGridViewComboBoxColumn();
            //ServiceComboBox.DataSource = GetServiceDT;
            //ServiceComboBox.FlatStyle = FlatStyle.Flat;
            //ServiceComboBox.HeaderText = "Услуга";
            //ServiceComboBox.Name = "ServiceComboBox";
            //ServiceComboBox.DisplayMember = "ServiceName";
            //ServiceComboBox.ValueMember = "ID";
            //ServiceComboBox.DataPropertyName = "ServiceCost_ID";
            //ServiceComboBox.DropDownWidth = 300;
            //ServiceComboBox.Width = 300;
            //dataGridView1.Columns.Add(ServiceComboBox);
        }
        //заполнение комбобоксов данными из таблиц
        private void FillCombobox()
        {
            //Контрагенты
            string Owner = "select * from d__Owner";
            DataTable OwnerDT = DbConnection.DBConnect(Owner);
            comboBox1.DataSource = OwnerDT;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            //Бригадиры
            string Brigade = "select * from d__Brigade";
            DataTable BrigadeDT = DbConnection.DBConnect(Brigade);
            comboBox2.DataSource = BrigadeDT;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
            //Станции
            string Station = "select * from d__Station";
            DataTable StationDT = DbConnection.DBConnect(Station);
            comboBox3.DataSource = StationDT;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "ID";
        }
        //Рисование строки в datagridview с выводом "Кол-во строк"
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int Count = 0;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                Count = dataGridView1.RowCount;
            }

            foreach (var scroll in dataGridView1.Controls.OfType<HScrollBar>())
            {
                if (scroll.Visible)
                {
                    panel1.Width = this.dataGridView1.RowHeadersWidth + 1;
                    panel1.Location = new Point(5, this.dataGridView1.Height - (panel1.Height - 25));
                    panel1.Visible = true;

                    textBox4.Text = "Всего строк: " + Count.ToString();
                    int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    textBox4.Width = this.dataGridView1.Columns[1].Width + 1;
                    Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                    textBox4.Location = new Point(Xdgvx1, this.dataGridView1.Height - (textBox4.Height - 25));
                    textBox4.Visible = true;

                    int Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                    panel2.Width = this.dataGridView1.Columns[3].Width + 1;
                    Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                    panel2.Location = new Point(Xdgvx2, this.dataGridView1.Height - (panel2.Height - 25));
                    panel2.Visible = true;

                    int Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                    panel3.Width = this.dataGridView1.Columns[4].Width + 1;
                    Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
                    panel3.Location = new Point(Xdgvx3, this.dataGridView1.Height - (panel3.Height - 25));
                    panel3.Visible = true;

                    int Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
                    panel4.Width = this.dataGridView1.Columns[5].Width + 1;
                    Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
                    panel4.Location = new Point(Xdgvx4, this.dataGridView1.Height - (panel4.Height - 25));
                    panel4.Visible = true;

                    int Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
                    panel5.Width = this.dataGridView1.Columns[6].Width + 1;
                    Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
                    panel5.Location = new Point(Xdgvx5, this.dataGridView1.Height - (panel5.Height - 25));
                    panel5.Visible = true;

                    int Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
                    panel6.Width = this.dataGridView1.Columns[7].Width + 1;
                    Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
                    panel6.Location = new Point(Xdgvx6, this.dataGridView1.Height - (panel6.Height - 25));
                    panel6.Visible = true;

                    int Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
                    panel7.Width = this.dataGridView1.Columns[8].Width + 2;
                    Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
                    panel7.Location = new Point(Xdgvx7, this.dataGridView1.Height - (panel7.Height - 25));
                    panel7.Visible = true;
                }
                else
                {
                    panel1.Width = this.dataGridView1.RowHeadersWidth + 1;
                    panel1.Location = new Point(5, this.dataGridView1.Height - (panel1.Height - 43));
                    panel1.Visible = true;

                    textBox4.Text = "Всего строк: " + Count.ToString();
                    int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    textBox4.Width = this.dataGridView1.Columns[1].Width + 1;
                    Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                    textBox4.Location = new Point(Xdgvx1, this.dataGridView1.Height - (textBox4.Height - 43));
                    textBox4.Visible = true;

                    int Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                    panel2.Width = this.dataGridView1.Columns[3].Width + 1;
                    Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                    panel2.Location = new Point(Xdgvx2, this.dataGridView1.Height - (panel2.Height - 43));
                    panel2.Visible = true;

                    int Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                    panel3.Width = this.dataGridView1.Columns[4].Width + 1;
                    Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
                    panel3.Location = new Point(Xdgvx3, this.dataGridView1.Height - (panel3.Height - 43));
                    panel3.Visible = true;

                    int Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
                    panel4.Width = this.dataGridView1.Columns[5].Width + 1;
                    Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
                    panel4.Location = new Point(Xdgvx4, this.dataGridView1.Height - (panel4.Height - 43));
                    panel4.Visible = true;

                    int Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
                    panel5.Width = this.dataGridView1.Columns[6].Width + 1;
                    Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
                    panel5.Location = new Point(Xdgvx5, this.dataGridView1.Height - (panel5.Height - 43));
                    panel5.Visible = true;

                    int Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
                    panel6.Width = this.dataGridView1.Columns[7].Width + 1;
                    Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
                    panel6.Location = new Point(Xdgvx6, this.dataGridView1.Height - (panel6.Height - 43));
                    panel6.Visible = true;

                    int Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
                    panel7.Width = this.dataGridView1.Columns[8].Width + 2;
                    Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
                    panel7.Location = new Point(Xdgvx7, this.dataGridView1.Height - (panel7.Height - 43));
                    panel7.Visible = true;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[
                    e.RowIndex];
                string Id = row.Cells["BodyID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Проверка для вагона
            if (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() == string.Empty)
            {
                string UpdateBody = "exec dbo.UpdateRenderedServiceBody NULL, 1," + SelectItemRow;
                DbConnection.DBConnect(UpdateBody);
            }
            else
            {
                string CarNumber = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string ServiceDate = dateTimePicker1.Value.Date.ToShortDateString();

                string CheckCarnumber = "select * from d__Carriage where CarNumber = "+CarNumber;
                DataTable dt1 = DbConnection.DBConnect(CheckCarnumber);
                if(dt1.Rows.Count > 0)
                {
                    string GetCarnumber = "select * from d__RenderedServiceHead h left join d__RenderedServiceBody b on h.ID = b.Head_ID where b.CarNumber = " + CarNumber + " and h.ServiceDate = '" + ServiceDate + "' and h.NUM != '" + GetStatus + "'";
                    DataTable dt = DbConnection.DBConnect(GetCarnumber);
                    if (dt.Rows.Count == 0)
                    {
                        string UpdateBody = "exec dbo.UpdateRenderedServiceBody '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + "',1," + SelectItemRow;
                        DbConnection.DBConnect(UpdateBody);
                    }
                    else
                    {
                        MessageBox.Show("Вагон " + CarNumber + " уже имеется в заявках на эту дату " + ServiceDate, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UpdateBody();
                    }
                }
                else
                {
                    MessageBox.Show(CarNumber+" отсутствует в справочнике Вагоны!","",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateBody();
                }
            }
            //Проверка для продукта
            if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString() == string.Empty)
            {
                string UpdateBody = "exec dbo.UpdateRenderedServiceBody NULL, 2, " + SelectItemRow;
                DbConnection.DBConnect(UpdateBody);
            }
            else
            {
                string UpdateBody = "exec dbo.UpdateRenderedServiceBody '" + dataGridView1.Rows[e.RowIndex].Cells[8].Value + "',2," + SelectItemRow;
                DbConnection.DBConnect(UpdateBody);
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string FillBody = "exec dbo.UpdateRenderedServiceBody_Filter " + dataGridView1.Rows[i].Cells[3].Value + "," + dataGridView1.Rows[i].Cells[4].Value + "," + dataGridView1.Rows[i].Cells[5].Value + "," + dataGridView1.Rows[i].Cells[6].Value + "," + dataGridView1.Rows[i].Cells[7].Value + "," + SelectItemRow;
                DbConnection.DBConnect(FillBody);
            }
        }
    }
}
