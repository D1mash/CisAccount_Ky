using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
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
                dateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                dateEdit1.Properties.Mask.EditMask = "d"; //'Short date' format 
                dateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
                dateEdit1.EditValue = DateTime.Today;

                FillCombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "Change_of_Ownership_Load");
            }
        }


        //Создание заявки
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != String.Empty)
                {
                    string NewHead = "declare @Id int; exec dbo.Rent_Add_Head '" + textBox1.Text + "','" + dateEdit1.DateTime.ToShortDateString() + "','" + comboBox1.SelectedValue.ToString() + "','" + textBox2.Text + "'";
                    DataTable HeadID = DbConnection.DBConnect(NewHead);

                    //Список вагонов для передачи в БД
                    string Id = HeadID.Rows[0][0].ToString();

                    ArrayList list = new ArrayList();
                    string Arrays = string.Empty;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(dt.Rows[i][0].ToString());
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        Arrays = string.Join(" ", list[i]);
                        string newRow = "exec dbo.Rent_ADD_Body '" + Arrays + "'," + Id;
                        DbConnection.DBConnect(newRow);
                    }

                    //Очищение промежуточной таблицы
                    string Truncate = "exec dbo.Delete_Temp_MultiCar";
                    DbConnection.DBConnect(Truncate);

                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Введите номер заявки", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "ID";
                comboBox1.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Кнопка удаления
        private void button4_Click(object sender, EventArgs e)
        {
            //if (SelectItemRow > 0)
            //{
            //    if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            string Delete = "delete from [dbo].[d__Rent_Status] where ID = " + SelectItemRow + " delete from [dbo].[Rent_Carriage] where Status_Rent = " + SelectItemRow;
            //            DbConnection.DBConnect(Delete);
            //            MessageBox.Show("Документ удалён!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            update();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        //Множественная вставка
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Insert = "exec dbo.InsertMultiple_Carriage '" + Clipboard.GetText() + "'";
                DbConnection.DBConnect(Insert);

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
            string Refresh = "exec Get_MultiCar";
            dt = DbConnection.DBConnect(Refresh);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
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
        private void button2_Click(object sender, EventArgs e)
        {
            string Insert = "exec dbo.InsertMultiple_Carriage '" + textBox3.Text + "'";
            DbConnection.DBConnect(Insert);

            RefreshGrid();
        }

        //Построчная вставка нажатием на Enter
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(null, null);
            }
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
                if (gridView1.RowCount > 0)
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
        
    }
}
  
