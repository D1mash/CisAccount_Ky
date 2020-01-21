using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class UpdateProductForm : Form
    {
        public UpdateProductForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        public int SelectHandlingID { get; set; }

        private void FillCombobox()
        {
            String OwnerName = "Select * from qHangling";
            DataTable dT = DbConnection.DBConnect(OwnerName);
            comboBox1.DataSource = dT;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectHandlingID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string UpdateCurrentProduct = "update d__Product set Name = '" + textBox1.Text.Trim() + "', Handling_id = " + comboBox1.SelectedValue + " where ID = " + selectID;
                DataTable dtbl = new DataTable();
                dtbl = DbConnection.DBConnect(UpdateCurrentProduct);
                this.Close();
                MessageBox.Show("Продукт изменён!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form_Product owner = this.Owner as Form_Product;
                owner.button4_Click_Refresh_Table(null,null);
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

        private void UpdateProductForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }
    }
}
