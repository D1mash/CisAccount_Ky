using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class ServiceAddForm : Form
    {
        public ServiceAddForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ServiceAdd = "insert into d__Service values ('" + textBox1.Text.Trim() + "',1)";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(ServiceAdd);
            this.Close();
            MessageBox.Show("Запись добавлена");
        }
    }
}
