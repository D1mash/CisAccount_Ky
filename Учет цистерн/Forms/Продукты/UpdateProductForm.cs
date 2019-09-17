using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class UpdateProductForm : Form
    {
        public UpdateProductForm()
        {
            InitializeComponent();
        }

        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UpdateCurrentProduct = "update d__Product set Name = '" + textBox1.Text.Trim() + "', Handling_id = " + comboBox1.SelectedValue + " where ID = " + selectID;
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(UpdateCurrentProduct);
            this.Close();
            MessageBox.Show("Продукт изменён!");

        }

        private void UpdateProductForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "batysDataSet11.qHangling". При необходимости она может быть перемещена или удалена.
            this.qHanglingTableAdapter1.Fill(this.batysDataSet11.qHangling);
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
