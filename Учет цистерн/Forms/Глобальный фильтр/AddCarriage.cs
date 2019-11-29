using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Глобальный_фильтр
{
    public partial class AddCarriage : Form
    {
        public AddCarriage()
        {
            InitializeComponent();
        }

        private void AddCarriage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string AddCarriage = "exec AddGlobalFilter " + textBox1.Text.Trim();
                DbConnection.DBConnect(AddCarriage);
                MainForm main = this.Owner as MainForm;
                if(main != null)
                {
                    main.GetFilter();
                }
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
