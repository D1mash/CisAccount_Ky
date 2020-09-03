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
    public partial class Update_Product : Form
    {
        private string id = string.Empty;
        private string carnum = string.Empty;
        private int len;

        public Update_Product(string iD, string carNum)
        {
            InitializeComponent();
            this.id = iD;
            this.carnum = carNum;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string update = "exec dbo.Update_Rent_Status '" + id + "','" + carnum + "','"+textEdit1.Text+"'";
                DbConnection.DBConnect(update);

                Rent_Brodcast_Car main = this.Owner as Rent_Brodcast_Car;
                main.simpleButton1_Click(null, null);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 32 && number != 8 && (e.KeyChar <= 39 || e.KeyChar >= 46) && number != 47 && number != 61 && (e.KeyChar < 'А' || e.KeyChar > 'я'))
            {
                e.Handled = true;
            }
        }
    }
}
