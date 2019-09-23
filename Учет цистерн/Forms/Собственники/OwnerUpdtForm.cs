using System;
using System.Data;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

namespace Учет_цистерн
{
    public partial class OwnerUpdtForm : Form
    {
        public OwnerUpdtForm()
        {
            InitializeComponent();
        }

        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        private void OwnerUpdtForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox3.Visible = false;
            checkBox3.Visible = false;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        //изменить
        private void btnOk_Click(object sender, EventArgs e)
        {
            string UpdateCurrentOwner = "update d__Owner set Name = '"+textBox1.Text.Trim()+"', FullName = '"+textBox2.Text.Trim()+"' where ID = " + selectID;
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(UpdateCurrentOwner);
            this.Close();
            OkForm ok = new OkForm();
            ok.label1.Text = "Запись изменена!";
            ok.Show();
            //MessageBox.Show("Запись изменена!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
