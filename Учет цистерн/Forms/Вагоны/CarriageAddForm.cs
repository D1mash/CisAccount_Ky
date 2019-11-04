using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class CarriageAddForm : Form
    {
        public CarriageAddForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCombobox()
        {
            try
            {
                String OwnerName = "Select * from d__Owner";
                DataTable dT = DbConnection.DBConnect(OwnerName);
                comboBox1.DataSource = dT;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string AddNewCarriage = "exec dbo.FillCarriage '" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + comboBox1.SelectedValue.ToString() + "'";
                string SelectDubl = "select * from d__Carriage where Carnumber = " + textBox1.Text.Trim();
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(SelectDubl);
                if (dt.Rows.Count == 0)
                {
                    DbConnection.DBConnect(AddNewCarriage);
                    this.Close();
                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Вагон с номером: " + textBox1.Text.Trim() + " уже имеется в справочнике!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number!=8)
            {
                e.Handled = true;
            }
        }
    }
}
