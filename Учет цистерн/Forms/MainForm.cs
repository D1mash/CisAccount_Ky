using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

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
        }

        private void GetFilter()
        {
            string Reffresh = "exec dbo.GetFilter";
            advancedDataGridView1.DataSource = DbConnection.DBConnect(Reffresh); ;
            advancedDataGridView1.RowHeadersVisible = false;
            advancedDataGridView1.Columns[1].Visible = false;
        }

        private void advancedDataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = advancedDataGridView1.HitTest(e.X, e.Y).RowIndex;
                contextMenuStrip_GlobalFilter.Show(advancedDataGridView1, new Point(e.X, e.Y));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip_Product.Show(button1, new Point(0, button1.Height));
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Insert = "exec dbo.InsertGlobalFilter '" + Clipboard.GetText() + "'";
            DbConnection.DBConnect(Insert);
            GetFilter();
        }


        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFilter();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string IDs = string.Empty;
            List<Object> aList = new List<Object>();
            foreach (DataGridViewRow row in advancedDataGridView1.Rows)
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
                    MessageBox.Show("Выбирите строки!");
                }
            }
            GetFilter();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ExitForm exit = new ExitForm();
            exit.Show();

            //string message = "Вы действительно хотите закрыть программу?";
            //string title = "Закрытие программы";
            //e.Cancel = true;
            //MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            //DialogResult result = MessageBox.Show(message, title, buttons);
            //if (result == DialogResult.OK)
            //{
            //    string UpdateAuditUser = "UPDATE AUDIT_USER SET DATE_OUT = GETDATE(), IS_DEAD = 1 WHERE ID_SESSION = @@spid and (IS_DEAD IS NULL OR DATE_OUT IS NULL)";
            //    DataTable dataTable = new DataTable();
            //    dataTable = DbConnection.DBConnect(UpdateAuditUser);
            //    Application.Exit();
            //}
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
            CarriageForm carriageForm = new CarriageForm();
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

        private void button4_Click(object sender, EventArgs e)
        {
            //RenderedServiceForm RenderedServiceForm = new RenderedServiceForm();
            RenderedServiceForm RenderedServiceForm = new RenderedServiceForm(this.toolStripProgressBar1, this.toolStripLabel1);
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
                this.splitContainer1.SplitterDistance = 188;
            }
            else if (tabControl2.SelectedTab == tabPage2)
            {
                this.splitContainer1.SplitterDistance = 25;
            }
        }
    }
}
