using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Услуги.СНО_Приход
{
    public partial class SnoComAddForm : Form
    {
        public SnoComAddForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != string.Empty  && textBox3.Text != string.Empty && textBox4.Text != string.Empty) 
                {
                    string FillSNOCom = "exec dbo.FillCurrentSNO '" + textBox2.Text.Trim() + "','" + dateTimePicker1.Value.Date.ToString() + "','" + textBox3.Text.Trim() + "','" + textBox4.Text.Trim() + "'";
                    DataTable dataTable = DbConnection.DBConnect(FillSNOCom);
                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    SnoComForm main = this.Owner as SnoComForm;
                    main.GetSNO();
                }
                else
                {
                    MessageBox.Show("Заполните данные!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (Regex.IsMatch(textBox4.Text, @"\,\d\d") && e.KeyChar != 8)
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
