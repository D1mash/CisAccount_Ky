using System;
using System.Data;
using System.Data.SqlClient;
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
            this.TabControlExtra.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(this.tabControl1_TabClosing);
        }
        public string GetStatus { get; set; }
        public string GetDate { get; set; }
        public int SelectID { get; set; }
        int SelectItemRow;

        //Вставить вагоны в таблицу
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string GetID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
                DataTable dt = DbConnection.DBConnect(GetID);
                string Insert = "exec dbo.InsertCarnumber '" + Clipboard.GetText() + "'," + dt.Rows[0][0].ToString();
                DbConnection.DBConnect(Insert);
                dataGridView1.DataSource = null;
                GetData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Кнопка обновить
        private void button6_Click(object sender, EventArgs e)
        {
            UpdateBody();
        }
        //метод для обновления
        private void UpdateBody()
        {
            try
            {
                dataGridView1.DataSource = null;
                GetData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      
        }
        //Добавить строку в таблицу
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string GetID = "select ID from d__RenderedServiceHead where NUM = '" + GetStatus + "'";
                DataTable dt = DbConnection.DBConnect(GetID);
                string ID = dt.Rows[0][0].ToString();
                string RenrederServiceBody = "exec dbo.FillRenderedServiceBody NULL, NULL, NULL, " + ID;
                DbConnection.DBConnect(RenrederServiceBody);
                UpdateBody();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveDoc();
        }

        private void SaveDoc()
        {
            try
            {
                string GetID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
                DataTable dt = DbConnection.DBConnect(GetID);

                string CheckProduct = "select Carnumber from d__RenderedServiceBody where Product_ID is NULL and Head_ID = " + dt.Rows[0][0].ToString();
                DataTable dt1 = DbConnection.DBConnect(CheckProduct);

                string CheckCarNumber = "select * from d__RenderedServiceBody where CarNumber is NULL and Head_ID = " + dt.Rows[0][0].ToString();
                DataTable dt2 = DbConnection.DBConnect(CheckCarNumber);

                string CheckRows = "select * from d__RenderedServiceBody where Head_ID = " + dt.Rows[0][0].ToString();
                DataTable dt3 = DbConnection.DBConnect(CheckRows);
                if (dt3.Rows.Count > 0)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        MessageBox.Show("Введите вагон или удалите пустую строку!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            MessageBox.Show("У вагона " + dt1.Rows[0][0].ToString() + " не выбран продукт!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            string RenrederServiceHead = "exec dbo.RenderedServiceCreate '"+GetStatus+"','" + textBox1.Text.Trim() + "','" + dateTimePicker1.Value.Date.ToString() + "','" + textBox2.Text.Trim() + "','" + comboBox3.SelectedValue.ToString() + "','"+ comboBox2.SelectedValue.ToString() + "',1,1";
                            DbConnection.DBConnect(RenrederServiceHead);
                            UpdateBody();
                            TabControlExtra.TabPages.Remove(TabControlExtra.SelectedTab);
                        }
                    }
                }
                else
                {

                    MessageBox.Show("В табличной части отсутствуют вагоны!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Удаление строки из таблицы в документе
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Удалить выделенную запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string DeleteRow = "delete from d__RenderedServiceBody where ID = " + SelectItemRow + " delete from temp where body_id = "+SelectItemRow+" delete from d__AUTN where body_id = "+SelectItemRow;
                    DbConnection.DBConnect(DeleteRow);
                    UpdateBody();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //скопировать строчку
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string CopyBody = "exec dbo.CopyRenderedServiceBody "+SelectItemRow;
                DbConnection.DBConnect(CopyBody);
                UpdateBody();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OrderAddForm_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                FillCombobox();
                string GetDocStatus = "select ds.Name, u.FIO, h.Invoice from d__RenderedServiceHead h left join dbo.Users u on u.AID = h.ID_USER_INS left join dbo.d__Docstate ds on ds.ID = h.ID_DocState where h.NUM = '" + GetStatus + "'";
                DataTable GetDocStatusDT = DbConnection.DBConnect(GetDocStatus);
                label9.Text = GetDocStatusDT.Rows[0][0].ToString();
                label10.Text = GetDocStatusDT.Rows[0][1].ToString();
                textBox1.Text = GetDocStatusDT.Rows[0][2].ToString();
                dateTimePicker1.Text = GetDate;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            dataGridView1.Columns.Insert(3,ProductComboBox);
            ////Бригадир
            //string GetBrigade = "select * from d__Brigade";
            //DataTable GetBrigadeDT = DbConnection.DBConnect(GetBrigade);
            //DataGridViewComboBoxColumn BrigadeComboBox = new DataGridViewComboBoxColumn();
            //BrigadeComboBox.DataSource = GetBrigadeDT;
            //BrigadeComboBox.FlatStyle = FlatStyle.Flat;
            //BrigadeComboBox.HeaderText = "Бригадир";
            //BrigadeComboBox.Name = "BrigadeComboBox";
            //BrigadeComboBox.DisplayMember = "Name";
            //BrigadeComboBox.ValueMember = "ID";
            //BrigadeComboBox.DataPropertyName = "Brigade_ID";
            //dataGridView1.Columns.Add(BrigadeComboBox);
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

        private void tabControl1_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            if(e.TabPage.Text == "Акт № " + GetStatus + " от " + GetDate)
            {
                DialogResult result = MessageBox.Show("Документ изменён. Нажмите Да, если вы хотите сохранить изменения и закрыть документ, Нет - для закрытия без сохранения, или Отмена для возврата в документ.", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string GetID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
                        DataTable dt = DbConnection.DBConnect(GetID);

                        string CheckProduct = "select Carnumber from d__RenderedServiceBody where Product_ID is NULL and Head_ID = " + dt.Rows[0][0].ToString();
                        DataTable dt1 = DbConnection.DBConnect(CheckProduct);

                        string CheckCarNumber = "select * from d__RenderedServiceBody where CarNumber is NULL and Head_ID = " + dt.Rows[0][0].ToString();
                        DataTable dt2 = DbConnection.DBConnect(CheckCarNumber);

                        string CheckRows = "select * from d__RenderedServiceBody where Head_ID = " + dt.Rows[0][0].ToString();
                        DataTable dt3 = DbConnection.DBConnect(CheckRows);
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt2.Rows.Count > 0)
                            {
                                MessageBox.Show("Введите вагон или удалите пустую строку!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                e.Cancel = true;
                                return;
                            }
                            else
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    MessageBox.Show("У вагона " + dt1.Rows[0][0].ToString() + " не выбран продукт!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    e.Cancel = true;
                                    return;
                                }
                                else
                                {
                                    string RenrederServiceHead = "exec dbo.RenderedServiceCreate '" + GetStatus + "','" + textBox1.Text.Trim() + "','" + dateTimePicker1.Value.Date.ToString() + "','" + textBox2.Text.Trim() + "','" + comboBox3.SelectedValue.ToString() + "','" + comboBox2.SelectedValue.ToString() + "',1,1";
                                    DbConnection.DBConnect(RenrederServiceHead);
                                    UpdateBody();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("В табличной части отсутствуют вагоны!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            e.Cancel = true;
                            return;
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if(result == DialogResult.No)
                {
                    string GetID = "select ID from d__RenderedServiceHead where NUM = " + GetStatus;
                    DataTable GetDTID = DbConnection.DBConnect(GetID);
                    int ID = Convert.ToInt32(GetDTID.Rows[0][0].ToString());
                    string Delete = "delete from temp where head_id = " + ID + " delete from d__RenderedServiceBody where Head_ID = " + ID + " delete from d__RenderedServiceHead where ID = " + ID;
                    DbConnection.DBConnect(Delete);
                }
                else if(result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                
            }
        }

        //Рисование строки в datagridview с выводом "Кол-во строк"
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int sz = 0;
            try
            {
                int Count = 0;
                Decimal sum = 0;
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[10].Value.ToString() != string.Empty)
                    {
                        sum += Convert.ToDecimal(this.dataGridView1[10, i].Value);
                    }
                    Count = dataGridView1.RowCount;
                }

                foreach (var scroll in dataGridView1.Controls.OfType<HScrollBar>())
                {
                    if (scroll.Visible)
                    {
                        sz = -15;
                    }
                    else
                    {
                        sz = 43;
                    }
                }
                panel1.Width = this.dataGridView1.RowHeadersWidth + 1;
                panel1.Location = new Point(5, this.dataGridView1.Height - (panel1.Height - sz));
                panel1.Visible = true;

                textBox4.Text = "Всего строк: " + Count.ToString();
                int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                textBox4.Width = this.dataGridView1.Columns[1].Width + 1;
                Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                textBox4.Location = new Point(Xdgvx1, this.dataGridView1.Height - (textBox4.Height - sz));
                textBox4.Visible = true;

                int Xdgvx9 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                panel9.Width = this.dataGridView1.Columns[3].Width + 2;
                Xdgvx9 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                panel9.Location = new Point(Xdgvx9, this.dataGridView1.Height - (panel9.Height - sz));
                panel9.Visible = true;

                int Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                panel2.Width = this.dataGridView1.Columns[4].Width + 1;
                Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
                panel2.Location = new Point(Xdgvx2, this.dataGridView1.Height - (panel2.Height - sz));
                panel2.Visible = true;

                int Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
                panel3.Width = this.dataGridView1.Columns[5].Width + 1;
                Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
                panel3.Location = new Point(Xdgvx3, this.dataGridView1.Height - (panel3.Height - sz));
                panel3.Visible = true;

                int Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
                panel4.Width = this.dataGridView1.Columns[6].Width + 1;
                Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
                panel4.Location = new Point(Xdgvx4, this.dataGridView1.Height - (panel4.Height - sz));
                panel4.Visible = true;

                int Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
                panel5.Width = this.dataGridView1.Columns[7].Width + 1;
                Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
                panel5.Location = new Point(Xdgvx5, this.dataGridView1.Height - (panel5.Height - sz));
                panel5.Visible = true;

                int Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
                panel6.Width = this.dataGridView1.Columns[8].Width + 1;
                Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
                panel6.Location = new Point(Xdgvx6, this.dataGridView1.Height - (panel6.Height - sz));
                panel6.Visible = true;

                int Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
                panel8.Width = this.dataGridView1.Columns[9].Width + 1;
                Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(9, -1, true).Location.X;
                panel8.Location = new Point(Xdgvx7, this.dataGridView1.Height - (panel8.Height - sz));
                panel8.Visible = true;

                textBox5.Text = sum.ToString();
                int Xdgvx8 = this.dataGridView1.GetCellDisplayRectangle(9, -1, true).Location.X;
                textBox5.Width = this.dataGridView1.Columns[10].Width + 1;
                Xdgvx8 = this.dataGridView1.GetCellDisplayRectangle(10, -1, true).Location.X;
                textBox5.Location = new Point(Xdgvx8, this.dataGridView1.Height - (textBox5.Height - sz));
                textBox5.Visible = true;

                int Xdgvx10 = this.dataGridView1.GetCellDisplayRectangle(10, -1, true).Location.X;
                panel7.Width = this.dataGridView1.Columns[11].Width + 2;
                Xdgvx10 = this.dataGridView1.GetCellDisplayRectangle(11, -1, true).Location.X;
                panel7.Location = new Point(Xdgvx10, this.dataGridView1.Height - (panel7.Height - sz));
                panel7.Visible = true;

                int Xdgvx11 = this.dataGridView1.GetCellDisplayRectangle(11, -1, true).Location.X;
                panel10.Width = this.dataGridView1.Columns[12].Width + 2;
                Xdgvx11 = this.dataGridView1.GetCellDisplayRectangle(12, -1, true).Location.X;
                panel10.Location = new Point(Xdgvx11, this.dataGridView1.Height - (panel10.Height - sz));
                panel10.Visible = true;

                int Xdgvx12 = this.dataGridView1.GetCellDisplayRectangle(12, -1, true).Location.X;
                panel11.Width = this.dataGridView1.Columns[13].Width + 2;
                Xdgvx12 = this.dataGridView1.GetCellDisplayRectangle(13, -1, true).Location.X;
                panel11.Location = new Point(Xdgvx12, this.dataGridView1.Height - (panel11.Height - sz));
                panel11.Visible = true;

                int Xdgvx13 = this.dataGridView1.GetCellDisplayRectangle(13, -1, true).Location.X;
                panel12.Width = this.dataGridView1.Columns[14].Width + 2;
                Xdgvx13 = this.dataGridView1.GetCellDisplayRectangle(14, -1, true).Location.X;
                panel12.Location = new Point(Xdgvx13, this.dataGridView1.Height - (panel12.Height - sz));
                panel12.Visible = true;

                int Xdgvx14 = this.dataGridView1.GetCellDisplayRectangle(14, -1, true).Location.X;
                panel13.Width = this.dataGridView1.Columns[15].Width + 2;
                Xdgvx14 = this.dataGridView1.GetCellDisplayRectangle(15, -1, true).Location.X;
                panel13.Location = new Point(Xdgvx14, this.dataGridView1.Height - (panel13.Height - sz));
                panel13.Visible = true;

                int Xdgvx15 = this.dataGridView1.GetCellDisplayRectangle(15, -1, true).Location.X;
                panel14.Width = this.dataGridView1.Columns[16].Width + 2;
                Xdgvx15 = this.dataGridView1.GetCellDisplayRectangle(16, -1, true).Location.X;
                panel14.Location = new Point(Xdgvx15, this.dataGridView1.Height - (panel14.Height - sz));
                panel14.Visible = true;

                int Xdgvx16 = this.dataGridView1.GetCellDisplayRectangle(16, -1, true).Location.X;
                panel15.Width = this.dataGridView1.Columns[17].Width + 2;
                Xdgvx16 = this.dataGridView1.GetCellDisplayRectangle(17, -1, true).Location.X;
                panel15.Location = new Point(Xdgvx16, this.dataGridView1.Height - (panel15.Height - sz));
                panel15.Visible = true;

                int Xdgvx17 = this.dataGridView1.GetCellDisplayRectangle(17, -1, true).Location.X;
                panel16.Width = this.dataGridView1.Columns[18].Width + 2;
                Xdgvx17 = this.dataGridView1.GetCellDisplayRectangle(18, -1, true).Location.X;
                panel16.Location = new Point(Xdgvx17, this.dataGridView1.Height - (panel16.Height - sz));
                panel16.Visible = true;

                int Xdgvx18 = this.dataGridView1.GetCellDisplayRectangle(18, -1, true).Location.X;
                panel17.Width = this.dataGridView1.Columns[19].Width + 2;
                Xdgvx18 = this.dataGridView1.GetCellDisplayRectangle(19, -1, true).Location.X;
                panel17.Location = new Point(Xdgvx18, this.dataGridView1.Height - (panel17.Height - sz));
                panel17.Visible = true;

                int Xdgvx19 = this.dataGridView1.GetCellDisplayRectangle(19, -1, true).Location.X;
                panel18.Width = this.dataGridView1.Columns[20].Width + 2;
                Xdgvx19 = this.dataGridView1.GetCellDisplayRectangle(20, -1, true).Location.X;
                panel18.Location = new Point(Xdgvx19, this.dataGridView1.Height - (panel18.Height - sz));
                panel18.Visible = true;

                int Xdgvx20 = this.dataGridView1.GetCellDisplayRectangle(20, -1, true).Location.X;
                panel19.Width = this.dataGridView1.Columns[21].Width + 2;
                Xdgvx20 = this.dataGridView1.GetCellDisplayRectangle(21, -1, true).Location.X;
                panel19.Location = new Point(Xdgvx20, this.dataGridView1.Height - (panel19.Height - sz));
                panel19.Visible = true;

                int Xdgvx21 = this.dataGridView1.GetCellDisplayRectangle(21, -1, true).Location.X;
                panel20.Width = this.dataGridView1.Columns[22].Width + 2;
                Xdgvx21 = this.dataGridView1.GetCellDisplayRectangle(22, -1, true).Location.X;
                panel20.Location = new Point(Xdgvx21, this.dataGridView1.Height - (panel20.Height - sz));
                panel20.Visible = true;

                int Xdgvx22 = this.dataGridView1.GetCellDisplayRectangle(22, -1, true).Location.X;
                panel21.Width = this.dataGridView1.Columns[23].Width + 2;
                Xdgvx22 = this.dataGridView1.GetCellDisplayRectangle(23, -1, true).Location.X;
                panel21.Location = new Point(Xdgvx22, this.dataGridView1.Height - (panel21.Height - sz));
                panel21.Visible = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[
                        e.RowIndex];
                    string Id = row.Cells["BodyID"].Value.ToString();
                    SelectItemRow = Convert.ToInt32(Id);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
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

                    string CheckCarnumber = "select * from d__Carriage where CarNumber = " + CarNumber;
                    DataTable dt1 = DbConnection.DBConnect(CheckCarnumber);
                    if (dt1.Rows.Count > 0)
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
                        MessageBox.Show(CarNumber + " отсутствует в справочнике Вагоны!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UpdateBody();
                    }
                }
                //Проверка для продукта
                if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == string.Empty)
                {
                    string UpdateBody = "exec dbo.UpdateRenderedServiceBody NULL, 2, " + SelectItemRow;
                    DbConnection.DBConnect(UpdateBody);
                }
                else
                {
                    string UpdateBody = "exec dbo.UpdateRenderedServiceBody '" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "',2," + SelectItemRow;
                    DbConnection.DBConnect(UpdateBody);
                }

                ////Проверка для бригадира
                //if (dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString() == string.Empty)
                //{
                //    string UpdateBody = "exec dbo.UpdateRenderedServiceBody NULL, 3, " + SelectItemRow;
                //    DbConnection.DBConnect(UpdateBody);
                //}
                //else
                //{
                //    string UpdateBody = "exec dbo.UpdateRenderedServiceBody '" + dataGridView1.Rows[e.RowIndex].Cells[12].Value + "',3," + SelectItemRow;
                //    DbConnection.DBConnect(UpdateBody);
                //}

                //axis
                string UpdateAxis = "update temp set axis = " + dataGridView1.Rows[e.RowIndex].Cells[4].Value + " where body_id = " + SelectItemRow;
                DbConnection.DBConnect(UpdateAxis);
                //gor
                string UpdateGor = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[5].Value + "',2," + SelectItemRow;
                DbConnection.DBConnect(UpdateGor);
                //hol
                string UpdateHol = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[6].Value + "',3," + SelectItemRow;
                DbConnection.DBConnect(UpdateHol);
                //tor
                string UpdateTor = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[7].Value + "',4," + SelectItemRow;
                DbConnection.DBConnect(UpdateTor);
                //drkr
                string UpdateDrkr = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[8].Value + "',5," + SelectItemRow;
                DbConnection.DBConnect(UpdateDrkr);
                //AvailableAUTN
                string UpdateAvailableAUTN = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[11].Value + "',6," + SelectItemRow;
                DbConnection.DBConnect(UpdateAvailableAUTN);
                //Klapan
                string UpdateKlapan = "exec [dbo].[UpdateBodyAutn] '" + dataGridView1.Rows[e.RowIndex].Cells[12].Value + "',1," + SelectItemRow;
                DbConnection.DBConnect(UpdateKlapan);
                //DemSkobi
                string UpdateDemSkobi = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[13].Value + "',7," + SelectItemRow;
                DbConnection.DBConnect(UpdateDemSkobi);
                //Trafaret_PTC_Holding
                string UpdateTrafaret_PTC_Holding = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[14].Value + "',8," + SelectItemRow;
                DbConnection.DBConnect(UpdateTrafaret_PTC_Holding);
                //UshkiZavareni
                string UpdateUshkiZavareni = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[15].Value + "',9," + SelectItemRow;
                DbConnection.DBConnect(UpdateUshkiZavareni);
                //SkobiZavareni
                string UpdateSkobiZavareni = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[16].Value + "',10," + SelectItemRow;
                DbConnection.DBConnect(UpdateSkobiZavareni);
                //ShaibaValikZavareni
                string UpdateShaibaValikZavareni = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[17].Value + "',11," + SelectItemRow;
                DbConnection.DBConnect(UpdateShaibaValikZavareni);
                //VnutrLestnica
                string UpdateVnutrLestnica = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[18].Value + "',12," + SelectItemRow;
                DbConnection.DBConnect(UpdateVnutrLestnica);
                //Greben
                string UpdateGreben = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[19].Value + "',13," + SelectItemRow;
                DbConnection.DBConnect(UpdateGreben);
                //BarashkTip
                string UpdateBarashkTip = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[20].Value + "',14," + SelectItemRow;
                DbConnection.DBConnect(UpdateBarashkTip);
                //AvailTriBoltov
                string UpdateAvailTriBoltov = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[21].Value + "',15," + SelectItemRow;
                DbConnection.DBConnect(UpdateAvailTriBoltov);
                //AvailToExp
                string UpdateAvailToExp = "exec [dbo].[UpdateBodyTemp] '" + dataGridView1.Rows[e.RowIndex].Cells[22].Value + "',16," + SelectItemRow;
                DbConnection.DBConnect(UpdateAvailToExp);
                //Note
                string UpdateNote = "exec [dbo].[UpdateBodyAutn] '" + dataGridView1.Rows[e.RowIndex].Cells[23].Value + "',2," + SelectItemRow;
                DbConnection.DBConnect(UpdateNote);

                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    string FillBody = "exec dbo.UpdateRenderedServiceBody_Filter " + dataGridView1.Rows[i].Cells[3].Value + "," + dataGridView1.Rows[i].Cells[4].Value + "," + dataGridView1.Rows[i].Cells[5].Value + "," + dataGridView1.Rows[i].Cells[6].Value + "," + dataGridView1.Rows[i].Cells[7].Value + "," + SelectItemRow;
                //    DbConnection.DBConnect(FillBody);
                //}

                UpdateBody();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
