using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Обработанные_вагоны
{
    public partial class ChangeCurrentOwner : Form
    {
        public ChangeCurrentOwner()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            Journal journal = this.Owner as Journal;
            journal.ChangeOwner(Id);
            this.Close();
        }

        private void ChangeCurrentOwner_Load(object sender, EventArgs e)
        {
            string Owner = "select * from d__Owner";
            DataTable dt = DbConnection.DBConnect(Owner);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
        }
    }
}
