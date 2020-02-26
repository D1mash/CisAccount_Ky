using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Учет_цистерн.Forms.Смена_собственника;

namespace Учет_цистерн.Forms
{

    public partial class Change_of_Ownership : Form
    {
        private TradeWright.UI.Forms.TabControlExtra TabControlExtra;
        DataTable dt;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        string role;

        public Change_of_Ownership(TradeWright.UI.Forms.TabControlExtra tabControl1, string role)
        {
            InitializeComponent();
            this.TabControlExtra = tabControl1;
            this.role = role;
            this.TabControlExtra.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(this.tabControl1_TabClosing);
        }

        private void Change_of_Ownership_Load(object sender, EventArgs e)
        {
            try
            {

                if (role == "1")
                {
                    button1.Enabled = true;
                }
                else
                {
                    if (role == "2")
                    {

                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = true;
                    }
                }

                dateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                dateEdit1.Properties.Mask.EditMask = "d"; //'Short date' format 
                dateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
                dateEdit1.EditValue = DateTime.Today;

                FillCombobox();

                textEdit2.Text = String.Empty;
                textEdit3.Text = String.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "Change_of_Ownership_Load");
            }
        }


        //Создание заявки
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textEdit2.Text != String.Empty && textEdit3.Text != String.Empty && comboBox1.SelectedValue.ToString() != "-1")
                {
                    string NewHead = "declare @Id int; exec dbo.Rent_Add_Head '" + textEdit2.Text + "','" + dateEdit1.DateTime.ToShortDateString() + "','" + comboBox1.SelectedValue.ToString() + "','" + textEdit3.Text + "', @CurrentID = @Id output; select @Id";
                    DataTable HeadID = DbConnection.DBConnect(NewHead);

                    //Список вагонов для передачи в БД
                    string Id = HeadID.Rows[0][0].ToString();

                    ArrayList list = new ArrayList();
                    string Arrays = string.Empty;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(dt.Rows[i][1].ToString());
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        Arrays = string.Join(" ", list[i]);
                        string newRow = "exec dbo.Rent_ADD_Body '" + Arrays + "'," + Id;
                        DbConnection.DBConnect(newRow);
                    }

                    RefreshGrid();

                    Change_of_Ownership_Load(null, null);
                }
                else
                {
                    if (textEdit2.Text == String.Empty)
                    {
                        MessageBox.Show("Введите номер заявки!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if(textEdit3.Text == String.Empty)
                        {
                            MessageBox.Show("Введите продукт!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не выбран собственник!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "Change_of_Ownership_Load_btn1");
            }
        }

        private void FillCombobox()
        {
            try
            {
                string getOwner = "Select ID, Name from d__Owner";
                DataTable dt = DbConnection.DBConnect(getOwner);

                
                var dr = dt.NewRow();
                dr["Id"] = -1;
                dr["Name"] = String.Empty;
                dt.Rows.InsertAt(dr, 0);
                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "ID";
                comboBox1.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Множественная вставка
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList list = new ArrayList();

                string Arrays = string.Empty;

                string Arr = string.Empty;

                Arr = Clipboard.GetText();

                string[] word = Arr.Split(new char[] {'\r','\n'}, StringSplitOptions.RemoveEmptyEntries);

                foreach(string s in word)
                {
                    list.Add(s);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    Arrays = string.Join(" ", list[i]);
                    string newRow = "exec dbo.InsertMultiple_Carriage '" + Arrays + "'";
                    DbConnection.DBConnect(newRow);
                }

                RefreshGrid();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(exp, "Множественная вставка вагонов, вставить вагонов");
            }
        }

        //Обновление
        private void RefreshGrid()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            string Refresh = "exec Get_MultiCar";
            dt = DbConnection.DBConnect(Refresh);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;

            GridColumnSummaryItem Carnumber = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Номер вагона", "Кол.во: {0}");
            gridView1.Columns["Номер вагона"].Summary.Add(Carnumber);
        }

        //Выпадающее меню
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(gridControl1, new Point(e.X, e.Y));
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(exp, "dataGridView1_MouseClick_1");
            }
        }

        //Построчная вставка
        private void button2_Click_1(object sender, EventArgs e)
        {
            string Insert = "exec dbo.InsertMultiple_Carriage '" + textEdit1.Text + "'";
            DbConnection.DBConnect(Insert);

            textEdit1.Text = String.Empty;

            RefreshGrid();
        }

        //Мульти удаление
        private void уадилитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList rows = new ArrayList();
            List<Object> aList = new List<Object>();
            string Arrays = string.Empty;

            Int32[] selectedRowHandles = gridView1.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                    rows.Add(gridView1.GetDataRow(selectedRowHandle));
            }
            foreach (DataRow row in rows)
            {
                aList.Add(row["ID"]);
                Arrays = string.Join(" ", aList);
                string delete = "exec dbo.Remove_TempMultiCar '" + Arrays + "'";
                DbConnection.DBConnect(delete);
            }

            RefreshGrid();
        }

        private void tabControl1_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text == "Смена собственника от " + GetDate)
            {
                if (gridView1.RowCount > 1)
                {
                    DialogResult result = MessageBox.Show("Вы хотите закрыть вкладку? Данные будут удалены. Если вы хотите сохранить заявку нажмите кнопку Добавить", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        //Очищение промежуточной таблицы
                        string Truncate = "exec dbo.Delete_Temp_MultiCar";
                        DbConnection.DBConnect(Truncate);
                        TabControlExtra.TabPages.Remove(TabControlExtra.SelectedTab);
                    }
                    else 
                    {
                        if(result == DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    TabControlExtra.TabPages.Remove(TabControlExtra.SelectedTab);
                }
            }
        }


        public string GetDate { get; set; }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click_1(null, null);
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            
            if (View.IsRowSelected(e.RowHandle))
            {
                e.Appearance.ForeColor = Color.DarkBlue;
                e.Appearance.BackColor = Color.LightBlue;
                //e.HighPriority = true;
            }
        }
    }
}
  
