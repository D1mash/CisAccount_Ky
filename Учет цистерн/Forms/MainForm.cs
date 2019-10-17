using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Учет_цистерн.Forms.заявки_на_обработку;
using Учет_цистерн.Forms.СНО;

namespace Учет_цистерн
{
    public partial class MainForm : Form
    {
        public MainForm(string FIO)
        {
            InitializeComponent();
            this.tabControl2.SelectedTab = tabPage2;
            this.splitContainer1.SplitterDistance = 25;
            this.Text = "Учет вагонов-цистерн. Батыс Петролеум ТОО - " + FIO;
            string GetConnection = "exec dbo.GetConnection";
            DataTable dt = DbConnection.DBConnect(GetConnection);
            toolStripTextBox1.Text = "SPID: " + dt.Rows[0][1].ToString() + "; UID: " + dt.Rows[0][0].ToString() + ";";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GetFilter();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            textBox1.Visible = false;
        }

        private void GetFilter()
        {
            string Reffresh = "exec dbo.GetFilter";
            dataGridView1.DataSource = DbConnection.DBConnect(Reffresh); ;
            dataGridView1.RowHeadersWidth = 15;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 80;
        }

        private void dataGridView1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                contextMenuStrip_GlobalFilter.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip_Product.Show(button1, new Point(0, button1.Height));
        }
        //Глобальный фильтр, вставить вагоны
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string Insert = "exec dbo.InsertGlobalFilter '" + Clipboard.GetText() + "'";
            DbConnection.DBConnect(Insert);
            GetFilter();
        }
        //Глобальный фильтр, обновить вагоны
        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFilter();
        }
        //Глобальный фильтр, удалить выделенные вагоны
        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string IDs = string.Empty;
            List<Object> aList = new List<Object>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                if (isSelected)
                {
                    aList.Add(row.Cells[1].Value.ToString());
                    IDs = string.Join(" ", aList);
                    string delete = "exec dbo.RemoveGlobalFilter '" + IDs + "'";
                    DbConnection.DBConnect(delete);
                }
                else
                {
                }
            }
            GetFilter();
        }
        //Глобальный фильтр, удалить все вагоны
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DeleteAll = "delete from dbo.GlobalFilter where UserID = dbo.GET_USER_AID()";
            DbConnection.DBConnect(DeleteAll);
            GetFilter();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (MessageBox.Show("Завершить работу с программой?", "Завершение программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string UpdateAuditUser = "UPDATE AUDIT_USER SET DATE_OUT = GETDATE(), IS_DEAD = 1 WHERE ID_SESSION = @@spid and (IS_DEAD IS NULL OR DATE_OUT IS NULL)";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(UpdateAuditUser);
                Environment.Exit(0);
            }
        }

        private void ToolStripMenuItem1_Product_Click(object sender, EventArgs e)
        {
            Form_Product frm = new Form_Product();
            tabControl1.Show();
            TabPage ProductTabPage = new TabPage("Продукты");
            tabControl1.TabPages.Add(ProductTabPage);
            tabControl1.SelectedTab = ProductTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            ProductTabPage.Controls.Add(frm);
        }

        private void toolStripMenuItem2_Station_Click(object sender, EventArgs e)
        {
            StationForm frm = new StationForm();
            tabControl1.Show();
            TabPage StationTabPage = new TabPage("Станции");
            tabControl1.TabPages.Add(StationTabPage);
            tabControl1.SelectedTab = StationTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            StationTabPage.Controls.Add(frm);
        }

        private void ToolStripMenuItem3_Brigade_Click(object sender, EventArgs e)
        {
            BrigadeForm frm = new BrigadeForm();
            tabControl1.Show();
            TabPage BrigadeTabPage = new TabPage("Бригады");
            tabControl1.TabPages.Add(BrigadeTabPage);
            tabControl1.SelectedTab = BrigadeTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            BrigadeTabPage.Controls.Add(frm);
        }

        private void toolStripMenuItem4_Owner_Click(object sender, EventArgs e)
        {
            OwnerForm frm = new OwnerForm();
            tabControl1.Show();
            TabPage OwnerTabPage = new TabPage("Собственники");
            tabControl1.TabPages.Add(OwnerTabPage);
            tabControl1.SelectedTab = OwnerTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            OwnerTabPage.Controls.Add(frm);
        }

        private void ToolStripMenuItem_Carriage_Click(object sender, EventArgs e)
        {
            CarriageForm carriageForm = new CarriageForm(this.toolStripProgressBar1, this.toolStripLabel1, this.button1, this.button2, this.button3, this.button4, this.button6, this.tabControl1);
            tabControl1.Show();
            TabPage CarriageTabPage = new TabPage("Вагоны");
            tabControl1.TabPages.Add(CarriageTabPage);
            tabControl1.SelectedTab = CarriageTabPage;
            carriageForm.TopLevel = false;
            carriageForm.Visible = true;
            carriageForm.FormBorderStyle = FormBorderStyle.None;
            carriageForm.Dock = DockStyle.Fill;
            CarriageTabPage.Controls.Add(carriageForm);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuStrip_Services.Show(button2, new Point(0, button2.Height));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RenderedServiceForm RenderedServiceForm = new RenderedServiceForm(this.toolStripProgressBar1, this.toolStripLabel1, this.button1, this.button2, this.button3, this.button4, this.button6, this.tabControl1);
            tabControl1.Show();
            TabPage CarriageTabPage = new TabPage("Обработанные вагоны");
            tabControl1.TabPages.Add(CarriageTabPage);
            tabControl1.SelectedTab = CarriageTabPage;
            RenderedServiceForm.TopLevel = false;
            RenderedServiceForm.Visible = true;
            RenderedServiceForm.FormBorderStyle = FormBorderStyle.None;
            RenderedServiceForm.Dock = DockStyle.Fill;
            CarriageTabPage.Controls.Add(RenderedServiceForm);
        }


        private Point DragStartPosition = Point.Empty;

        private void tabControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DragStartPosition = new Point(e.X, e.Y);
        }


        private void tabControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Rectangle r = new Rectangle(DragStartPosition, Size.Empty);
            r.Inflate(SystemInformation.DragSize);

            TabPage tp = HoverTab();

            if (tp != null)
            {
                if (!r.Contains(e.X, e.Y))
                    tabControl1.DoDragDrop(tp, DragDropEffects.All);
            }
            DragStartPosition = Point.Empty;
        }


        private void tabControl1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            TabPage hover_Tab = HoverTab();
            if (hover_Tab == null)
                e.Effect = DragDropEffects.None;
            else
            {
                if (e.Data.GetDataPresent(typeof(TabPage)))
                {
                    e.Effect = DragDropEffects.Move;
                    TabPage drag_tab = (TabPage)e.Data.GetData(typeof(TabPage));

                    if (hover_Tab == drag_tab) return;

                    Rectangle TabRect = tabControl1.GetTabRect(tabControl1.TabPages.IndexOf(hover_Tab));
                    TabRect.Inflate(-3, -3);
                    if (TabRect.Contains(tabControl1.PointToClient(new Point(e.X, e.Y))))
                    {
                        SwapTabPages(drag_tab, hover_Tab);
                        tabControl1.SelectedTab = drag_tab;
                    }
                }
            }
        }


        private TabPage HoverTab()
        {
            for (int index = 0; index <= tabControl1.TabCount - 1; index++)
            {
                if (tabControl1.GetTabRect(index).Contains(tabControl1.PointToClient(Cursor.Position)))
                    return tabControl1.TabPages[index];
            }
            return null;
        }


        private void SwapTabPages(TabPage tp1, TabPage tp2)
        {
            int Index1 = tabControl1.TabPages.IndexOf(tp1);
            int Index2 = tabControl1.TabPages.IndexOf(tp2);
            tabControl1.TabPages[Index1] = tp2;
            tabControl1.TabPages[Index2] = tp1;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            contextMenuStrip_Report.Show(button3, new Point(0, button3.Height));
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.Show();
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            this.button4.BackColor = System.Drawing.Color.Pink;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            this.button4.BackColor = System.Drawing.SystemColors.Control;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            this.button4.BackColor = System.Drawing.Color.Pink;
        }

        private void tabControl2_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tabPage1)
            {
                this.splitContainer1.SplitterDistance = 250;
                GetFilter();
            }
            else if (tabControl2.SelectedTab == tabPage2)
            {
                this.splitContainer1.SplitterDistance = 25;
            }
        }

        private void РасценкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceCostForm ServiceCostForm = new ServiceCostForm();
            tabControl1.Show();
            TabPage CarriageTabPage = new TabPage("Расценки");
            tabControl1.TabPages.Add(CarriageTabPage);
            tabControl1.SelectedTab = CarriageTabPage;
            ServiceCostForm.TopLevel = false;
            ServiceCostForm.Visible = true;
            ServiceCostForm.FormBorderStyle = FormBorderStyle.None;
            ServiceCostForm.Dock = DockStyle.Fill;
            CarriageTabPage.Controls.Add(ServiceCostForm);
        }

        private void СНОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnoForm SnoForm = new SnoForm();
            tabControl1.Show();
            TabPage SnotabPage = new TabPage("СНО");
            tabControl1.TabPages.Add(SnotabPage);
            tabControl1.SelectedTab = SnotabPage;
            SnoForm.TopLevel = false;
            SnoForm.Visible = true;
            SnoForm.FormBorderStyle = FormBorderStyle.None;
            SnoForm.Dock = DockStyle.Fill;
            SnotabPage.Controls.Add(SnoForm);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OrderAllForm orderAllForm = new OrderAllForm(this.tabControl1);
            tabControl1.Show();
            TabPage OrderAllTabPage = new TabPage("Заявки на обработку");
            tabControl1.TabPages.Add(OrderAllTabPage);
            OrderAllTabPage.Name = "OrderAllTabPage";
            tabControl1.SelectedTab = OrderAllTabPage;
            orderAllForm.TopLevel = false;
            orderAllForm.Visible = true;
            orderAllForm.FormBorderStyle = FormBorderStyle.None;
            orderAllForm.Dock = DockStyle.Fill;
            OrderAllTabPage.Controls.Add(orderAllForm);
        }

        private void dataGridView1_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
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
                    panel1.Width = this.dataGridView1.RowHeadersWidth + 2;
                    panel1.Location = new Point(5, this.dataGridView1.Height - (panel1.Height+15));
                    panel1.Visible = true;

                    int Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    panel2.Width = this.dataGridView1.Columns[0].Width + 1;
                    Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    panel2.Location = new Point(Xdgvx2, this.dataGridView1.Height - (panel2.Height + 15));
                    panel2.Visible = true;

                    textBox1.Text = "Всего строк: " + Count.ToString();
                    int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    textBox1.Width = this.dataGridView1.Columns[2].Width + 1;
                    Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
                    textBox1.Location = new Point(Xdgvx1, this.dataGridView1.Height - (textBox1.Height + 15));
                    textBox1.Visible = true;

                    int Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
                    panel3.Width = this.dataGridView1.Columns[3].Width + 2;
                    Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                    panel3.Location = new Point(Xdgvx3, this.dataGridView1.Height - (panel3.Height + 15));
                    panel3.Visible = true;
                }
                else
                {
                    panel1.Width = this.dataGridView1.RowHeadersWidth + 1;
                    panel1.Location = new Point(5, this.dataGridView1.Height - (panel1.Height - 1));
                    panel1.Visible = true;

                    int Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    panel2.Width = this.dataGridView1.Columns[0].Width + 1;
                    Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    panel2.Location = new Point(Xdgvx2, this.dataGridView1.Height - (panel2.Height - 1));
                    panel2.Visible = true;

                    textBox1.Text = "Всего строк: " + Count.ToString();
                    int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                    textBox1.Width = this.dataGridView1.Columns[2].Width + 1;
                    Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
                    textBox1.Location = new Point(Xdgvx1, this.dataGridView1.Height - (textBox1.Height-1));
                    textBox1.Visible = true;

                    int Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
                    panel3.Width = this.dataGridView1.Columns[3].Width + 2;
                    Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                    panel3.Location = new Point(Xdgvx3, this.dataGridView1.Height - (panel3.Height - 1));
                    panel3.Visible = true;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                this.dataGridView1.Rows[e.RowIndex].Cells["checkBoxColumn"].Value = true;
        }
    }
}
