using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

namespace Учет_цистерн.Forms.СНО
{
    public partial class SnoUpdateForm : Form
    {
        public SnoUpdateForm()
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

        public int SelectContragentID { get; set; }

        private void FillCombobox()
        {
            string Contragent = "select * from d__Owner";
            DataTable dTs = DbConnection.DBConnect(Contragent);
            comboBox1.DataSource = dTs;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectContragentID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SnoUpdateForm_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UpdateSNO = "exec dbo.UpdateSNO " + comboBox1.SelectedValue.ToString() + "," + textBox1.Text.Replace(",", ".") + "," + textBox2.Text.Replace(",", ".") + "," + textBox3.Text.Replace(",", ".") + "," + textBox4.Text.Replace(",", ".") + "," + textBox5.Text.Replace(",", ".") + ",'" + dateTimePicker1.Value.Date.ToString() + "'," + selectID;
            DataTable dT = DbConnection.DBConnect(UpdateSNO);
            OkForm ok = new OkForm();
            ok.label1.Text = "Запись изменена!";
            ok.Show();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = (checkBox4.CheckState == CheckState.Checked);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = (checkBox5.CheckState == CheckState.Checked);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = (checkBox6.CheckState == CheckState.Checked);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = (checkBox7.CheckState == CheckState.Checked);
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            e.Handled = (e.KeyChar == (char)Keys.Space);
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
            e.Handled = (e.KeyChar == (char)Keys.Space);
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
            e.Handled = (e.KeyChar == (char)Keys.Space);
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
            e.Handled = (e.KeyChar == (char)Keys.Space);
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
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
    }
}
