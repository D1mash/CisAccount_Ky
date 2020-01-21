using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            textBox3.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        private void checkBox7_CheckStateChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = (checkBox7.CheckState == CheckState.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string UpdateCurrentSNO = "exec dbo.UpdateCurrentSNO '" + textBox1.Text.Trim() + "','" + dateTimePicker1.Value.Date.ToString() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "'," + selectID;
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
    }
}
