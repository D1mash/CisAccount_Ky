using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class Form_Product : Form
    {
        string role;
        public Form_Product(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        int SelectItemRow;
        int SelectHandlingID;

        private void button1_Click_Add_Product(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(addNewCargo))
                    {
                        form.Activate();
                        return;
                    }
                }
                addNewCargo addCargo = new addNewCargo();
                addCargo.Owner = this;
                addCargo.ShowDialog();
                
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

        public void button4_Click_Refresh_Table(object sender, EventArgs e)
        {
            try
            {
                string GetProduct = "select dp.ID, qh.ID as [Hangling_id],dp.Name [dpName], qh.Name [qhName] from d__Product dp left join qHangling qh on qh.ID = dp.Handling_id";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetProduct);
                gridControl1.DataSource = dataTable;
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

        private void button2_Click_Update_Product(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(UpdateProductForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                UpdateProductForm UpdateProductForm = new UpdateProductForm();
                UpdateProductForm.textBox1.Text = gridView1.GetFocusedDataRow()[2].ToString();
                UpdateProductForm.SelectID = SelectItemRow;
                UpdateProductForm.SelectHandlingID = SelectHandlingID;
                UpdateProductForm.Owner = this;
                UpdateProductForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string Id = gridView1.GetFocusedDataRow()[0].ToString();
                string HandID = gridView1.GetFocusedDataRow()[1].ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectHandlingID = Convert.ToInt32(HandID);
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

        private void button3_Click_Delete_Product(object sender, EventArgs e)
        {
            if(gridView1.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string DeleteCurrentProduct = "delete from d__Product where ID = " + SelectItemRow;
                        DbConnection.DBConnect(DeleteCurrentProduct);
                        MessageBox.Show("Продукт удалён!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button4_Click_Refresh_Table(null,null);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form_Product_Load(object sender, EventArgs e)
        {
            try
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
                        button1.Enabled = true;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = true;
                    }
                }

                string GetProduct = "select dp.ID, qh.ID as [Hangling_id],dp.Name [dpName], qh.Name [qhName] from d__Product dp left join qHangling qh on qh.ID = dp.Handling_id";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetProduct);
                gridControl1.DataSource = dataTable;
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
    }
}
