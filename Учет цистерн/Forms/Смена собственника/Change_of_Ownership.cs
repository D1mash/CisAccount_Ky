using DevExpress.XtraEditors.Repository;
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

        int SelectItemRow;

        public Change_of_Ownership(TradeWright.UI.Forms.TabControlExtra tabControl1)
        {
            InitializeComponent();
            this.TabControlExtra = tabControl1;
        }

        private void Change_of_Ownership_Load(object sender, EventArgs e)
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


        //Создание заявки
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != String.Empty)
            {
                string newRow = "exec dbo.Rent_Add_Head '" + textBox1.Text + "','" + dateEdit1.DateTime.ToShortDateString() + "','" + comboBox1.SelectedValue.ToString() + "'";
                DbConnection.DBConnect(newRow);

                //Получаю id для вагонов что бы добавить и обновить
                string id_Rent_Status = "SELECT [ID] FROM [Batys].[dbo].[d__Rent_Status] WHERE Number = '" + textBox1.Text.Trim() + "'";
                DataTable dt = DbConnection.DBConnect(id_Rent_Status);
                string id_Status = dt.Rows[0][0].ToString();

                New_Rent new_Rent = new New_Rent(id_Status);
                TabControlExtra.Show();
                TabPage RentTabPage = new TabPage("Заявка №" + textBox1.Text);
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
                MessageBox.Show("Введите номер заявки", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        private void FillCombobox()
        {
            string getOwner = "Select ID, Name from d__Owner";
            DataTable dt = DbConnection.DBConnect(getOwner);

            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";
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
        }
      
        //Обновить
        private void button3_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            string refresh_Ch_of_Own = "exec [dbo].[Refresh_Rent_Head] '" + dateEdit2.DateTime.ToShortDateString() + "', '" + dateEdit3.DateTime.ToShortDateString() + "'";
            DataTable dt = DbConnection.DBConnect(refresh_Ch_of_Own);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
        }
    }
}
  
