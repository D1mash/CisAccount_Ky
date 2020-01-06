using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using NLog;

namespace Учет_цистерн.Forms
{
    public partial class AddGlobalFilterForm : Form
    {
        public AddGlobalFilterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string AddCarriage = "exec dbo.AddGlobalFilter "+textBox1.Text.Trim();
            DbConnection.DBConnect(AddCarriage);
            MainForm main = this.Owner as MainForm;
            main.GetFilter();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1.PerformClick();
            }
        }
    }
}
