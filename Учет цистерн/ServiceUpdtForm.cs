using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class ServiceUpdtForm : Form
    {
        public ServiceUpdtForm()
        {
            InitializeComponent();
        }

        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        private void ServiceUpdtForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Update = "Update d__Service Set Name = " + textBox1.Text.Trim() + " where ID = " + selectID;
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(Update);
            this.Close();
            MessageBox.Show("Запись изменена!");
        }
    }
}
