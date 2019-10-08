using System;
using System.Data;
using Учет_цистерн.Forms.Оповещения;

using System.Windows.Forms;

namespace Учет_цистерн.Forms.СНО
{
    public partial class SnoAddForm : Form
    {
        public SnoAddForm()
        {
            InitializeComponent();
            FillComboBox();
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox4.Text = "0";
        }

        void FillComboBox()
        {
            String Contragent = "Select * from d__Owner";
            DataTable dT = DbConnection.DBConnect(Contragent);
            comboBox1.DataSource = dT;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String FillSNO = "exec dbo.FillSNO "+comboBox1.SelectedValue.ToString()+","+textBox1.Text.Replace(",", ".") + ","+textBox2.Text.Replace(",", ".") + ","+textBox3.Text.Replace(",", ".") + ","+textBox4.Text.Replace(",", ".") + ","+textBox5.Text.Replace(",", ".") + ",'"+dateTimePicker1.Value.Date.ToString()+"'";
            DataTable dT = DbConnection.DBConnect(FillSNO);
            OkForm ok = new OkForm();
            ok.label1.Text = "Запись добавлена!";
            ok.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                textBox3.Text = (Convert.ToDecimal(textBox1.Text) * Convert.ToDecimal(textBox2.Text)).ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            textBox3.Text = (Convert.ToDecimal(textBox1.Text) * Convert.ToDecimal(textBox2.Text)).ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string temp = "";
            string sum = "";
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                textBox4.Text = "0";
            }
            temp = ((Convert.ToDecimal(textBox3.Text) * Convert.ToDecimal(textBox4.Text)) / 100).ToString();
            sum = (Convert.ToDecimal(textBox3.Text) + Convert.ToDecimal(temp)).ToString();
            textBox5.Text = sum;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
