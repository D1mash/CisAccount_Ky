using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            string UpdateCurrentProduct = "update d__Product set Name = '" + textBox1.Text.Trim() + "', Handling_id = "+comboBox1.SelectedValue+" where ID = "+selectID;
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(UpdateCurrentProduct);
            this.Close();
            MessageBox.Show("Продукт изменён!");

        }

        private void UpdateProductForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "batysDataSet11.qHangling". При необходимости она может быть перемещена или удалена.
            this.qHanglingTableAdapter1.Fill(this.batysDataSet11.qHangling);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
