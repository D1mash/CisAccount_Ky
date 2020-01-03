using DevExpress.XtraEditors.Repository;
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
        TradeWright.UI.Forms.TabControlExtra TabControlExtra;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        int SelectItemRow;
        string SelectNumber_Rent;
        int OwId;

        public Change_of_Ownership(TradeWright.UI.Forms.TabControlExtra tabControl1)
        {
            InitializeComponent();
            this.TabControlExtra = tabControl1;
        }

        private void Change_of_Ownership_Load(object sender, EventArgs e)
        {
            try
            {
                dateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                dateEdit1.Properties.Mask.EditMask = "d"; //'Short date' format 
                dateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
                dateEdit1.EditValue = DateTime.Today;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                dateEdit2.EditValue = startDate;
                dateEdit3.EditValue = endDate;

                FillCombobox();
            }
            catch(Exception ex)
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
                    string newRow = "exec dbo.Rent_Add_Head '" + textBox1.Text + "','" + dateEdit1.DateTime.ToShortDateString() + "','" + comboBox1.SelectedValue.ToString() + "'";
                    DbConnection.DBConnect(newRow);

                    //Получаю id для вагонов что бы добавить и обновить
                    string id_Rent_Status = "SELECT [ID] FROM [Batys].[dbo].[d__Rent_Status] WHERE Number = '" + textBox1.Text.Trim() + "'";
                    DataTable dt = DbConnection.DBConnect(id_Rent_Status);
                    string id_Status = dt.Rows[0][0].ToString();

                    New_Rent new_Rent = new New_Rent(id_Status);
                    TabControlExtra.Show();
                    TabPage RentTabPage = new TabPage("Заявка № " + textBox1.Text);
                    TabControlExtra.TabPages.Add(RentTabPage);
                    TabControlExtra.SelectedTab = RentTabPage;
                    new_Rent.TopLevel = false;
                    new_Rent.Visible = true;
                    new_Rent.FormBorderStyle = FormBorderStyle.None;
                    new_Rent.Dock = DockStyle.Fill;
                    RentTabPage.Controls.Add(new_Rent);

                    button3_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Введите номер заявки", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Кнопка удалить
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Удалить выделенную запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        ArrayList rows = new ArrayList();
        //        List<Object> aList = new List<Object>();
        //        string Arrays = string.Empty;

        //        Int32[] selectedRowHandles = gridView1.GetSelectedRows();
        //        for (int i = 0; i < selectedRowHandles.Length; i++)
        //        {
        //            int selectedRowHandle = selectedRowHandles[i];
        //            if (selectedRowHandle >= 0)
        //                rows.Add(gridView1.GetDataRow(selectedRowHandle));
        //        }
        //        foreach (DataRow row in rows)
        //        {
        //            aList.Add(row["Id"]);
        //            Arrays = string.Join(" ", aList);

        //            //string delete = "exec dbo.DeleteRenderedBody '" + Arrays + "'";
        //            //DbConnection.DBConnect(delete);
        //        }
        //        RefreshGrid();
        //    }
        //}

        //Выбор строки
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string Id = gridView1.GetFocusedDataRow()[0].ToString();
            SelectItemRow = Convert.ToInt32(Id);

            SelectNumber_Rent = gridView1.GetFocusedDataRow()[1].ToString();

            string OwnerID = gridView1.GetFocusedDataRow()[4].ToString();
            OwId = Convert.ToInt32(OwnerID);
        }
      
        //Обновить
        private void button3_Click(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            string refresh_Ch_of_Own = "exec [dbo].[Refresh_Rent_Head] '" + dateEdit2.DateTime.ToShortDateString() + "', '" + dateEdit3.DateTime.ToShortDateString() + "'";
            DataTable dt = DbConnection.DBConnect(refresh_Ch_of_Own);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[4].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(SelectItemRow > 0)
            {
                Update_Ch_of_Ow(SelectItemRow, SelectNumber_Rent);
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Update_Ch_of_Ow(int selectItemRow, string number)
        {

            Update_Change_of_Ownership update_change_Of_Ownership = new Update_Change_of_Ownership(SelectItemRow, SelectNumber_Rent, TabControlExtra);
            TabControlExtra.Show();
            TabPage Up_RentTabPage = new TabPage("Редактирование заявки № " + SelectNumber_Rent);
            TabControlExtra.TabPages.Add(Up_RentTabPage);
            TabControlExtra.SelectedTab = Up_RentTabPage;
            update_change_Of_Ownership.SelectOwId = OwId;
            update_change_Of_Ownership.TopLevel = false;
            update_change_Of_Ownership.Visible = true;
            update_change_Of_Ownership.FormBorderStyle = FormBorderStyle.None;
            update_change_Of_Ownership.Dock = DockStyle.Fill;
            Up_RentTabPage.Controls.Add(update_change_Of_Ownership);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (SelectItemRow > 0)
            {
                Update_Ch_of_Ow(SelectItemRow, SelectNumber_Rent);
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SelectItemRow > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Delete = "delete from [dbo].[d__Rent_Status] where ID = " + SelectItemRow + " delete from [dbo].[Rent_Carriage] where Status_Rent = " + SelectItemRow;
                        DbConnection.DBConnect(Delete);
                        MessageBox.Show("Документ удалён!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        update();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
  
