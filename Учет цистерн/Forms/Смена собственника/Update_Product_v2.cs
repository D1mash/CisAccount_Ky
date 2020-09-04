using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Смена_собственника
{
    public partial class Update_Product_v2 : Form
    {
        string carnum = string.Empty;

        public Update_Product_v2(string arrays)
        {
            InitializeComponent();
            this.carnum = arrays;
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 32 && number != 8 && (e.KeyChar <= 39 || e.KeyChar >= 46) && number != 47 && number != 61 && (e.KeyChar < 'А' || e.KeyChar > 'я'))
            {
                e.Handled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string update = "exec dbo.Update_Temp_Multicar '"+carnum.Trim()+"','"+textEdit1.Text+"'";
                DbConnection.DBConnect(update);

                Change_of_Ownership change_Of_Ownership = this.Owner as Change_of_Ownership;
                change_Of_Ownership.RefreshGrid();

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
