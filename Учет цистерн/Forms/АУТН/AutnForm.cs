using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.АУТН
{
    public partial class AutnForm : Form
    {
        public AutnForm()
        {
            InitializeComponent();
        }
        private void AutnForm_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = now;

            RefreshBody();
        }

        public void RefreshBody()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            string query = "exec dbo.GetAutn '" + dateTimePicker1.Value.ToShortDateString() + "'";
            DataTable dt = DbConnection.DBConnect(query);
            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
        }

        private void checkEdit23_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit16_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit24_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit18_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit19_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit19_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit20_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit20_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit21_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit21_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit22_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit22_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit23_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit15_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit16_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit14_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit15_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit13_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit14_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit12_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit13_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit3_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit17_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit18_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit10_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit9_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit8_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit7_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit6_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit5_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit4_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit5_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textEdit11_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textEdit10_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit10_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textEdit9_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit9_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textEdit8_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit8_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textEdit7_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit7_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textEdit6_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit6_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkEdit11_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void textEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit2_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit1_Properties_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }
}
