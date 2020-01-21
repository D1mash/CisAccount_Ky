using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class ServiceCostAddForm : Form
    {
        public ServiceCostAddForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        private void FillCombobox()
        {
            string Season = "select * from d__Season";
            DataTable dTs = DbConnection.DBConnect(Season);

            comboBox2.DataSource = dTs;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string FillServiceCost = "exec dbo.FillServiceCost '" + textBox2.Text.Trim() + "','" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + textBox1.Text.Replace(",", ".") + "," + comboBox2.SelectedValue.ToString();
                DbConnection.DBConnect(FillServiceCost);
                this.Close();
                MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ServiceCostForm main = this.Owner as ServiceCostForm;
                main.Btn_Refresh_Click(null,null);
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
