using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid;

namespace Учет_цистерн.Forms.СНО
{
    public partial class SnoImplForm : Form
    {
        string role;

        BindingSource source = new BindingSource();
        public SnoImplForm(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        int SelectItemRow;
        int SelectContragentID;
        int SelectNdsRate;

        public void GetSNO()
        {
            try
            {
                string GetSNO = "exec dbo.GetSNO";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetSNO);
                source.DataSource = dataTable;
                gridControl1.DataSource = source;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
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

        private void SnoImplForm_Load(object sender, EventArgs e)
        {
            if(role == "1")
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                if(role == "2")
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
            GridColumnSummaryItem Capacity = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Объем", "{0}");
            GridColumnSummaryItem Cost = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Цена", "{0}");
            GridColumnSummaryItem SumOutVat = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Сумма, без НДС", "{0}");
            GridColumnSummaryItem SumVat = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Сумма, с НДС", "{0}");
            gridView1.Columns["Объем"].Summary.Add(Capacity);
            gridView1.Columns["Цена"].Summary.Add(Cost);
            gridView1.Columns["Сумма, без НДС"].Summary.Add(SumOutVat);
            gridView1.Columns["Сумма, с НДС"].Summary.Add(SumVat);
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string Id = gridView1.GetFocusedDataRow()[0].ToString();
                string ContragentID = gridView1.GetFocusedDataRow()[1].ToString();
                string NdsRate = gridView1.GetFocusedDataRow()[7].ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectContragentID = Convert.ToInt32(ContragentID);
                SelectNdsRate = Convert.ToInt32(NdsRate);
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
                    if (form.GetType() == typeof(SnoImplAddFormForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                SnoImplAddFormForm SnoImplAddFormForm = new SnoImplAddFormForm();
                SnoImplAddFormForm.Owner = this;
                SnoImplAddFormForm.ShowDialog();
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
                    if (form.GetType() == typeof(SnoImplUpdateForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                SnoImplUpdateForm SnoImplUpdateForm = new SnoImplUpdateForm();
                SnoImplUpdateForm.SelectID = SelectItemRow;
                SnoImplUpdateForm.SelectContragentID = SelectContragentID;
                SnoImplUpdateForm.SelectNdsRate = SelectNdsRate;
                SnoImplUpdateForm.textBox6.Text = gridView1.GetFocusedDataRow()[3].ToString();
                SnoImplUpdateForm.textBox1.Text = gridView1.GetFocusedDataRow()[4].ToString();
                SnoImplUpdateForm.textBox2.Text = gridView1.GetFocusedDataRow()[5].ToString();
                SnoImplUpdateForm.textBox3.Text = gridView1.GetFocusedDataRow()[6].ToString();
                SnoImplUpdateForm.textBox4.Text = gridView1.GetFocusedDataRow()[7].ToString();
                SnoImplUpdateForm.textBox5.Text = gridView1.GetFocusedDataRow()[8].ToString();
                SnoImplUpdateForm.dateTimePicker1.Text = gridView1.GetFocusedDataRow()[9].ToString();
                SnoImplUpdateForm.Owner = this;
                SnoImplUpdateForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                        string Delete = "delete from d__SNO where ID = " + SelectItemRow;
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
