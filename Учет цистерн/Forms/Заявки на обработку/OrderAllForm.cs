using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using NLog;

namespace Учет_цистерн.Forms.заявки_на_обработку
{
    public partial class OrderAllForm : Form
    {
        TradeWright.UI.Forms.TabControlExtra TabControlExtra;
        private ToolStripProgressBar progBar;
        private ToolStripLabel TlStpLabel;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn6;
        private Button btn7;
        int SelectItemRow;
        string role;

        BindingSource source = new BindingSource();

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public OrderAllForm(TradeWright.UI.Forms.TabControlExtra tabControl1, ToolStripProgressBar toolStripProgressBar1, ToolStripLabel toolStripLabel1, Button button1, Button button2, Button button3, Button button4, Button btn_Refrence, Button button7, string role)
        {
            InitializeComponent();
            this.TabControlExtra = tabControl1;
            this.progBar = toolStripProgressBar1;
            this.TlStpLabel = toolStripLabel1;
            this.btn1 = button1;
            this.btn2 = button2;
            this.btn3 = button3;
            this.btn4 = button4;
            this.btn6 = btn_Refrence;
            this.btn7 = button7;
            this.TabControlExtra = tabControl1;
            this.role = role;
        }
        private void GetDocument()
        {
            try
            {
                string DateFrom = dateTimePicker1.Text;
                string DateTo = dateTimePicker2.Text;
                string GetDocument = "exec dbo.GetRenderedServiceDoc '" + DateFrom + "','" + DateTo + "'";
                DataTable dt = DbConnection.DBConnect(GetDocument);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
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

        private void GetDocumentGlobalFilter()
        {
            try
            {
                int yes = 1;
                string DateFrom = dateTimePicker1.Text;
                string DateTo = dateTimePicker2.Text;
                string GetDocument = "exec [dbo].[GetRenderedServiceDocGlobalFilter] '" + DateFrom + "','" + DateTo + "','" + yes + "'";
                DataTable dt = DbConnection.DBConnect(GetDocument);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void GetDocumentBody()
        {
            try
            {
                string DateFrom = dateTimePicker1.Text;
                string DateTo = dateTimePicker2.Text;
                string GetDocumentBody = "exec dbo.GetRenderedServiceDoc_Body '" + DateFrom + "','" + DateTo + "'";
                DataTable dt = DbConnection.DBConnect(GetDocumentBody);
                gridControl2.DataSource = dt;
                gridView2.Columns[0].Visible = false;
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

        private void GetDocumentBodyGlobalFilter()
        {
            try
            {
                int yes = 1;
                string DateFrom = dateTimePicker1.Text;
                string DateTo = dateTimePicker2.Text;
                string GetDocumentBody = "exec [dbo].[GetRenderedServiceDocGlobalFilter_Body] '" + DateFrom + "','" + DateTo + "','" + yes + "'";
                DataTable dt = DbConnection.DBConnect(GetDocumentBody);
                gridControl2.DataSource = dt;
                gridView2.Columns[0].Visible = false;
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

        private void OrderAllForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (role == "1")
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
                else
                {
                    if (role == "2")
                    {
                        button1.Enabled = true;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = false;
                    }
                }
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                dateTimePicker1.Value = startDate;
                dateTimePicker2.Value = endDate;

                if (CurrentTabControl.SelectedTab == tabPage1)
                {
                    if (checkBox1.Checked)
                    {
                        GetDocumentGlobalFilter();
                    }
                    else
                    {
                        GetDocument();
                    }
                }
                else if (CurrentTabControl.SelectedTab == tabPage2)
                {
                    if (checkBox1.Checked)
                    {
                        GetDocumentBodyGlobalFilter();
                    }
                    else
                    {
                        GetDocumentBody();
                    }
                    
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

        //Кнопка Добавить
        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewDocument();
        }
        //Форма нового документа
        private void CreateNewDocument()
        {
            try
            {
                string DocNum;
                string ServiceDate = System.DateTime.Now.ToShortDateString();
                string DateIns = System.DateTime.Now.ToString();
                string GetDocNum = "exec dbo.GetDocNum 1,'"+ ServiceDate + "'";
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(GetDocNum);
                DocNum = dt.Rows[0][0].ToString();

                string CreateDocHead = "exec dbo.RenderedServiceHeadCreate " + DocNum + ", '" + ServiceDate + "','" + DateIns + "'";
                DbConnection.DBConnect(CreateDocHead);

                OrderAddForm OrderAddForm = new OrderAddForm(this.TabControlExtra);
                TabControlExtra.Show();
                TabPage OrderAddTabPage = new TabPage("Акт № " + DocNum + " от " + DateIns);
                OrderAddForm.GetStatus = DocNum;
                OrderAddForm.GetDate = DateIns;
                TabControlExtra.TabPages.Add(OrderAddTabPage);
                TabControlExtra.SelectedTab = OrderAddTabPage;
                OrderAddForm.TopLevel = false;
                OrderAddForm.Visible = true;
                OrderAddForm.FormBorderStyle = FormBorderStyle.None;
                OrderAddForm.Dock = DockStyle.Fill;
                OrderAddTabPage.Controls.Add(OrderAddForm);
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (CurrentTabControl.SelectedTab == tabPage1)
            {
                if (checkBox1.Checked)
                {
                    GetDocumentGlobalFilter();
                }
                else
                {
                    GetDocument();
                }
            }
            else if (CurrentTabControl.SelectedTab == tabPage2)
            {
                if (checkBox1.Checked)
                {
                    GetDocumentBodyGlobalFilter();
                }
                else
                {
                    GetDocumentBody();
                }
            }
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string Id = gridView1.GetFocusedDataRow()[0].ToString();
                SelectItemRow = Convert.ToInt32(Id);
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
        //кнопка изменить
        private void button2_Click(object sender, EventArgs e)
        {
            if (CurrentTabControl.SelectedTab == tabPage1)
            {
                GetCurrentDocument();
            }
        }
        //форма имеющегося документа
        private void GetCurrentDocument()
        {
            try
            {
                string GetData = "select NUM, ServiceDate,Contragent_ID, Brigade_ID, Station_ID from d__RenderedServiceHead where ID = " + SelectItemRow;
                DataTable dt = DbConnection.DBConnect(GetData);
                string DocNum = dt.Rows[0][0].ToString();
                string GetDate = System.DateTime.Now.ToString();
                string Contr = dt.Rows[0][2].ToString();
                string Brigade = dt.Rows[0][3].ToString();
                string Station = dt.Rows[0][4].ToString();
                OrderUpdateForm orderUpdateForm = new OrderUpdateForm(this.TabControlExtra);
                TabControlExtra.Show();
                TabPage OrderAddTabPage = new TabPage("Акт № " + DocNum + ". Редактирование от " + GetDate);
                orderUpdateForm.GetStatus = DocNum;
                orderUpdateForm.GetDate = GetDate;
                orderUpdateForm.SelectedID = SelectItemRow;
                orderUpdateForm.SelectContrID = Contr;
                orderUpdateForm.SelectBrigadeID = Brigade;
                orderUpdateForm.SelectStationID = Station;
                TabControlExtra.TabPages.Add(OrderAddTabPage);
                TabControlExtra.SelectedTab = OrderAddTabPage;
                orderUpdateForm.TopLevel = false;
                orderUpdateForm.Visible = true;
                orderUpdateForm.FormBorderStyle = FormBorderStyle.None;
                orderUpdateForm.Dock = DockStyle.Fill;
                OrderAddTabPage.Controls.Add(orderUpdateForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку!" + ex.Message, "Исключение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //Кнопка удалить
        private void button3_Click(object sender, EventArgs e)
        {
            DeleteDoc();
            GetDocument();
        }
        //удаление документа
        private void DeleteDoc()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string DeleteDoc = "exec dbo.DeleteRenderedServiceDoc " + SelectItemRow;
                        DbConnection.DBConnect(DeleteDoc);
                        GetDocument();
                        MessageBox.Show("Документ удалён!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetCurrentDocument();
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    //int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                    contextMenuStrip1.Show(gridControl1, new Point(e.X, e.Y));
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

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewDocument();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentDocument();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteDoc();
            GetDocument();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetDocument();
        }

        private void провестиДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string GetDocState = "exec dbo.GetDocState " + SelectItemRow;
                DataTable DocStateDt = DbConnection.DBConnect(GetDocState);
                int DocState = Convert.ToInt32(DocStateDt.Rows[0][0]);
                if (DocState > 0 && DocState < 2)
                {
                    string UpdateDocState = "update d__RenderedServiceHead set ID_DocState = 2 where ID = " + SelectItemRow;
                    DbConnection.DBConnect(UpdateDocState);
                    MessageBox.Show("Документ проведен!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetDocument();
                }
                else
                {
                    MessageBox.Show("Документ проведен!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void отменитьПроведениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string GetDocState = "exec dbo.GetDocState " + SelectItemRow;
                DataTable DocStateDt = DbConnection.DBConnect(GetDocState);
                int DocState = Convert.ToInt32(DocStateDt.Rows[0][0]);
                if (DocState > 1 && DocState < 3)
                {
                    string UpdateDocState = "update d__RenderedServiceHead set ID_DocState = 1 where ID = " + SelectItemRow;
                    DbConnection.DBConnect(UpdateDocState);
                    MessageBox.Show("Проведение документа отменено!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetDocument();
                }
                else
                {
                    MessageBox.Show("Документ не проведен!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentTabControl.SelectedTab == tabPage1)
            {
                GetCurrentDocument();
            }
        }
    }
}
