using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class CarriageUpdateForm : Form
    {
        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        public int SelectOwnerID { get; set; }

        public CarriageUpdateForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        private void FillCombobox()
        {
            String OwnerName = "Select * from d__Owner";
            DataTable dT = DbConnection.DBConnect(OwnerName);
            comboBox1.DataSource = dT;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string Update = "update d__Carriage set CarNumber = " + textBox1.Text.Trim() + ", AXIS = " + textBox2.Text.Trim() + ", Owner_ID = " + comboBox1.SelectedValue.ToString() + " where ID = " + selectID;
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(Update);
            this.Close();
            MessageBox.Show("Запись изменена!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void CarriageUpdateForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
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
            comboBox1.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }
    }
}
