using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.СНО
{
    public partial class SnoImplAddFormForm : Form
    {
        public SnoImplAddFormForm()
        {
            InitializeComponent();
            FillComboBox();
            textBox1.Text = "0";
            textBox2.Text = "0";
            comboBox2.SelectedIndex = 8;
            //textBox4.Text = "0";
        }

        void FillComboBox()
        {
            string Contragent = "Select * from d__Owner";
            DataTable dT = DbConnection.DBConnect(Contragent);
            comboBox1.DataSource = dT;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";

            string NdsRates = "select * from d__NDS_Rates";
            DataTable NdsDt = DbConnection.DBConnect(NdsRates);
            comboBox2.DataSource = NdsDt;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Value";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text != String.Empty && textBox1.Text != String.Empty && textBox2.Text != String.Empty)
                {
                    string FillSNO = "exec dbo.FillSNO " + comboBox1.SelectedValue.ToString() + ",'" + textBox6.Text.Trim() + "'," + textBox1.Text.Replace(",", ".") + "," + textBox2.Text.Replace(",", ".") + "," + textBox3.Text.Replace(",", ".") + "," + comboBox2.SelectedValue.ToString() + "," + textBox5.Text.Replace(",", ".") + ",'" + dateTimePicker1.Value.Date.ToString() + "'";
                    DataTable dT = DbConnection.DBConnect(FillSNO);
                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    SnoImplForm main = this.Owner as SnoImplForm;
                    main.GetSNO();
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal result;
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    result = (Convert.ToDecimal(textBox1.Text) * Convert.ToDecimal(textBox2.Text));
                    textBox3.Text = result.ToString("0.##");
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal result;
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    result = (Convert.ToDecimal(textBox1.Text) * Convert.ToDecimal(textBox2.Text));
                    textBox3.Text = result.ToString("0.##");
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            decimal temp;
            decimal sum;

            temp = ((Convert.ToDecimal(textBox3.Text) * Convert.ToDecimal(comboBox2.SelectedValue.ToString())) / 100);
            sum = (Convert.ToDecimal(textBox3.Text) + Convert.ToDecimal(temp));
            textBox5.Text = sum.ToString("0.##");
        }

        //private void textBox4_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string temp = "";
        //        string sum = "";
        //        if (string.IsNullOrEmpty(textBox4.Text))
        //        {
        //            textBox4.Text = "0";
        //        }
        //        temp = ((Convert.ToDecimal(textBox3.Text) * Convert.ToDecimal(textBox4.Text)) / 100).ToString();
        //        sum = (Convert.ToDecimal(textBox3.Text) + Convert.ToDecimal(temp)).ToString();
        //        textBox5.Text = sum;
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (Exception exp)
        //    {
        //        MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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

                if (Regex.IsMatch(textBox1.Text, @"\,\d\d\d") && e.KeyChar != 8)
                {
                    e.Handled = true;
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
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Multi_Save(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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

                if (Regex.IsMatch(textBox5.Text, @"\,\d\d") && e.KeyChar != 8)
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
