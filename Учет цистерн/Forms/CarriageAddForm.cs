using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class CarriageAddForm : Form
    {
        public CarriageAddForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCombobox()
        {
            String OwnerName = "Select * from d__Owner";
            DataTable dT = DbConnection.DBConnect(OwnerName);
            comboBox1.DataSource = dT;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string AddNewCarriage = "insert into d__Carriage (CarNumber, AXIS,Owner_ID)" + "values (" + textBox1.Text.Trim() + "," + textBox2.Text.Trim() + "," + comboBox1.SelectedValue.ToString() + ")";
            DbConnection.DBConnect(AddNewCarriage);
            this.Close();
            MessageBox.Show("Запись добавлена!");
        }
    }
}
