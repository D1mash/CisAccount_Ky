using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class OwnerAddForm : Form
    {
        public OwnerAddForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string AddNewOwner = "insert into d__Owner " +
                                 "values ('"+textBox1.Text.Trim()+"','"+textBox2.Text.Trim()+"',"+textBox3.Text.Replace(",",".")+")";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(AddNewOwner);
            this.Close();
            MessageBox.Show("Запись добавлена!");
        }
    }
}
