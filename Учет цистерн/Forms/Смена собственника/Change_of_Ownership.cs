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
        private ToolStripProgressBar progBar;
        private ToolStripLabel TlStpLabel;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn6;
        private Button btn7;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        int SelectItemRow;
        string SelectNumber_Rent;
        int OwId;
        string role;

        public Change_of_Ownership(ToolStripProgressBar toolStripProgressBar1, ToolStripLabel toolStripLabel1, Button button1, Button button2, Button button3, Button button4, Button btn_Refrence, TradeWright.UI.Forms.TabControlExtra tabControl1, Button button7, string role)
        {
            InitializeComponent();
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

        private void Change_of_Ownership_Load(object sender, EventArgs e)
        {
            try
            {
                //if (role == "1")
                //{
                //    button1.Enabled = true;
                //    button2.Enabled = true;
                //    button3.Enabled = true;
                //    button4.Enabled = true;
                //}
                //else
                //{
                //    if (role == "2")
                //    {
                //        button1.Enabled = true;
                //        button2.Enabled = false;
                //        button3.Enabled = true;
                //        button4.Enabled = false;
                //    }
                //    else
                //    {
                //        button1.Enabled = false;
                //        button2.Enabled = false;
                //        button3.Enabled = false;
                //        button4.Enabled = false;
                //    }
                //}

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
            //try
            //{
                if (textBox1.Text != String.Empty)
                {
                    string NewHead = "declare @Id int; exec dbo.Rent_Add_Head '" + textBox1.Text + "','" + dateEdit1.DateTime.ToShortDateString() + "','" + comboBox1.SelectedValue.ToString() + "','" + textBox2.Text + "', @CurrentID = @Id output select @Id";
                    DataTable HeadID = DbConnection.DBConnect(NewHead);

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
                        string newRow = "exec dbo.Rent_Update_Body '"+Arrays+"',"+Id;
                        DbConnection.DBConnect(newRow);
                    }

                button3_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Введите номер заявки", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    logger.Error(ex, "Change_of_Ownership_Load_btn1");
            //}
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

        //Выбор строки
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //string Id = gridView1.GetFocusedDataRow()[0].ToString();
            //SelectItemRow = Convert.ToInt32(Id);

            //SelectNumber_Rent = gridView1.GetFocusedDataRow()[1].ToString();

            //string OwnerID = gridView1.GetFocusedDataRow()[4].ToString();
            //OwId = Convert.ToInt32(OwnerID);
        }

        //Обновить
        private void button3_Click(object sender, EventArgs e)
        {
            //update();
        }

        private void update()
        {
            //progBar.Visible = false;
            //try
            //{
            //    if (!backgroundWorker1.IsBusy)
            //    {
            //        gridControl1.DataSource = null;
            //        gridView1.Columns.Clear();
            //        progBar.Visible = true;
            //        //progBar.Maximum = GetTotalRecords();
            //        btn1.Enabled = false;
            //        btn2.Enabled = false;
            //        btn3.Enabled = false;
            //        btn4.Enabled = false;
            //        btn6.Enabled = false;
            //        btn7.Enabled = false;
            //        button1.Enabled = false;
            //        button4.Enabled = false;
            //        TabControlExtra.DisplayStyleProvider.ShowTabCloser = false;
                    //string refresh_Ch_of_Own = "exec [dbo].[Refresh_Rent_Head] '" + dateEdit2.DateTime.ToShortDateString() + "', '" + dateEdit3.DateTime.ToShortDateString() + "'";
                    //backgroundWorker1.RunWorkerAsync(refresh_Ch_of_Own);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Application.UseWaitCursor = true; //keeps waitcursor even when the thread ends.
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                string Query = (string)e.Argument;

                int i = 1;
                DataTable dt = DbConnection.DBConnect(Query);
                foreach (DataRow dr in dt.Rows)
                {
                    backgroundWorker1.ReportProgress(i);
                    i++;
                }
                e.Result = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
            {
                progBar.Value = e.ProgressPercentage;
                TlStpLabel.Text = "Обработка строки.. " + e.ProgressPercentage.ToString() + " из " + GetTotalRecords();
            }
        }

        private int GetTotalRecords()
        {
            int i = 1;
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return i;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                TlStpLabel.Text = "Готов";
            }
            else if (e.Error != null)
            {
                TlStpLabel.Text = "Ошибка" + e.Error.Message;
            }
            else
            {
                Application.UseWaitCursor = false;
                System.Windows.Forms.Cursor.Current = Cursors.Default;

                gridControl1.DataSource = e.Result;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[4].Visible = false;

                GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Количество", "Кол.во={0}");
                gridView1.Columns["№ Заявки"].Summary.Add(item1);

                progBar.Visible = false;
                btn1.Enabled = true;
                btn2.Enabled = true;
                btn3.Enabled = true;
                btn4.Enabled = true;
                btn6.Enabled = true;
                btn7.Enabled = true;
                button1.Enabled = true;
                button4.Enabled = true;

                TabControlExtra.DisplayStyleProvider.ShowTabCloser = true;
                TlStpLabel.Text = "Данные загружены...";
            }
        }

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

        DataTable dt;

        private void RefreshGrid()
        {
            string Refresh = "exec Get_MultiCar";
            dt = DbConnection.DBConnect(Refresh);
            gridControl1.DataSource = dt;
        }

        //Выпадающее меню
        private void gridControl1_MouseClick_1(object sender, MouseEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            string Insert = "exec dbo.InsertMultiple_Carriage '" + textBox3.Text + "'";
            DbConnection.DBConnect(Insert);

            RefreshGrid();
        }

        //Список вагонов
        string Multi_Car()
        {
            ArrayList list = new ArrayList();
            string Arrays = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i][0].ToString());
            }

            for (int i = 0; i < list.Count; i++)
            {
                Arrays = string.Join(" ", list[i]);
            }

            return Arrays;
        }
    }
}
  
