using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.СНО
{
    public partial class SnoImplUpdateForm : Form
    {
        public SnoImplUpdateForm()
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
        public int SelectNdsRate { get; set; }

        private void FillCombobox()
        {
            string Contragent = "select * from d__Owner";
            DataTable dTs = DbConnection.DBConnect(Contragent);
            comboBox1.DataSource = dTs;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectContragentID", true, DataSourceUpdateMode.OnPropertyChanged);

            string NdsRates = "select * from d__NDS_Rates";
            DataTable NdsDt = DbConnection.DBConnect(NdsRates);
            comboBox2.DataSource = NdsDt;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Value";
            comboBox2.DataBindings.Add("SelectedValue", this, "SelectNdsRate", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SnoImplUpdateForm_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string UpdateSNO = "exec dbo.UpdateSNO " + comboBox1.SelectedValue.ToString() + ",'"+textBox6.Text.Trim()+"'," + textBox1.Text.Replace(",", ".") + "," + textBox2.Text.Replace(",", ".") + "," + textBox3.Text.Replace(",", ".") + "," + comboBox2.SelectedValue.ToString() + "," + textBox5.Text.Replace(",", ".") + ",'" + dateTimePicker1.Value.Date.ToString() + "'," + selectID;
                DataTable dT = DbConnection.DBConnect(UpdateSNO);
                MessageBox.Show("Запись изменена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                SnoImplForm main = this.Owner as SnoImplForm;
                main.GetSNO();
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
            comboBox2.Enabled = (checkBox5.CheckState == CheckState.Checked);
            //textBox4.Enabled = (checkBox5.CheckState == CheckState.Checked);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = (checkBox6.CheckState == CheckState.Checked);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = (checkBox7.CheckState == CheckState.Checked);
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = (checkBox8.CheckState == CheckState.Checked);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                    textBox3.Text = (Convert.ToDecimal(textBox1.Text) * Convert.ToDecimal(textBox2.Text)).ToString();
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
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                    textBox3.Text = (Convert.ToDecimal(textBox1.Text) * Convert.ToDecimal(textBox2.Text)).ToString();
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
            string temp = "";
            string sum = "";

            temp = ((Convert.ToDecimal(textBox3.Text) * Convert.ToDecimal(comboBox2.SelectedValue.ToString())) / 100).ToString();
            sum = (Convert.ToDecimal(textBox3.Text) + Convert.ToDecimal(temp)).ToString();
            textBox5.Text = sum;
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
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
