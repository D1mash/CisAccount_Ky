using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Услуги.СНО_Приход
{
    public partial class SnoComUpdateFrom : Form
    {
        public SnoComUpdateFrom()
        {
            InitializeComponent();
        }

        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        private void SnoComUpdateFrom_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox7_CheckStateChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = (checkBox7.CheckState == CheckState.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string UpdateCurrentSNO = "exec dbo.UpdateCurrentSNO '" + textBox1.Text.Replace(",", ".") + "','" + dateTimePicker1.Value.Date.ToString() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "'," + selectID;
                DbConnection.DBConnect(UpdateCurrentSNO);
                MessageBox.Show("Изменено!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
                SnoComForm main = this.Owner as SnoComForm;
                main.GetSNO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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

                if (Regex.IsMatch(textBox1.Text, @"\,\d\d") && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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

                if (Regex.IsMatch(textBox2.Text, @"\,\d\d") && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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

                if (Regex.IsMatch(textBox3.Text, @"\,\d\d") && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
