using System;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Доп.параметры
{
    public partial class LastRent_Days : Form
    {
        public LastRent_Days()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string Update = "update d__Parameter set Parameter = "+textEdit1.Text.Trim()+", ID_USER_LAST = dbo.GET_USER_AID(), DATE_LAST = getdate() where ID = 1";
            DbConnection.DBConnect(Update);
            MessageBox.Show("Параметр изменён!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEdit1_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                char chr = e.KeyChar;
                if (!Char.IsDigit(chr) && chr != 8)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
