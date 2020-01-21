using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class addNewCargo : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public addNewCargo()
        {
            InitializeComponent();
            FillCombobox();
        }

        private void FillCombobox()
        {
            string Season = "select * from qHangling";
            DataTable dTs = DbConnection.DBConnect(Season);
            comboBox1.DataSource = dTs;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            //comboBox2.DataBindings.Add("SelectedValue", this, "SelectSeasonID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string FillProduct = "exec [dbo].[FillProduct] '" + textBox1.Text.Trim() + "'," + comboBox1.SelectedValue.ToString();
                string SelectDubl = "select * from d__Product where Name = '" + textBox1.Text.Trim() + "'";
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(SelectDubl);
                if (dt.Rows.Count == 0)
                {
                    DbConnection.DBConnect(FillProduct);
                    this.Close();
                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Product main = this.Owner as Form_Product;
                    main.button4_Click_Refresh_Table(null,null);
                }
                else
                {
                    MessageBox.Show("Продукт с названием: " + textBox1.Text.Trim() + " уже имеется в справочнике", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
