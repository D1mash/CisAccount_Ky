using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using Учет_цистерн.Forms.Пользователи;

namespace Учет_цистерн.Forms.Услуги.СНО_Приход
{
    public partial class SnoComForm : Form
    {
        BindingSource source = new BindingSource();
        string role;

        public SnoComForm(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        int SelectItemRow;

        private void SnoComForm_Load(object sender, EventArgs e)
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
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
            }

            GetSNO();

            GridColumnSummaryItem CapacitySum = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Остаток", "{0}");
            GridColumnSummaryItem FirstContainerSum = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Резервуар № 1", "{0}");
            GridColumnSummaryItem SecondContainerSum = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Резервуар № 2", "{0}");
            gridView1.Columns["Остаток"].Summary.Add(CapacitySum);
            gridView1.Columns["Резервуар № 1"].Summary.Add(FirstContainerSum);
            gridView1.Columns["Резервуар № 2"].Summary.Add(SecondContainerSum);
        }

        public void GetSNO()
        {
            try
            {
                string GetSNO = "exec dbo.GetCurrentSNO";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetSNO);
                source.DataSource = dataTable;
                gridControl1.DataSource = source;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(AddUserForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                AddUserForm add = new AddUserForm();
                add.Owner = this;
                add.ShowDialog();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(SnoComUpdateFrom))
                    {
                        form.Activate();
                        return;
                    }
                }
                SnoComUpdateFrom snoComUpdateFrom = new SnoComUpdateFrom();
                snoComUpdateFrom.SelectID = SelectItemRow;
                snoComUpdateFrom.textBox1.Text = gridView1.GetFocusedDataRow()[1].ToString();
                snoComUpdateFrom.textBox2.Text = gridView1.GetFocusedDataRow()[2].ToString();
                snoComUpdateFrom.textBox3.Text = gridView1.GetFocusedDataRow()[3].ToString();
                snoComUpdateFrom.dateTimePicker1.Text = gridView1.GetFocusedDataRow()[4].ToString();
                snoComUpdateFrom.Owner = this;
                snoComUpdateFrom.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(gridView1.SelectedRowsCount > 0)
            {
                try
                {
                    if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Delete = "delete from d__CurrentSNO where ID = " + SelectItemRow;
                        DbConnection.DBConnect(Delete);
                        MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetSNO();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetSNO();
        }
    }
}
