using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Смена_собственника
{
    public partial class Rent_Update_v2 : Form
    {
        public int SelectOwnerID { get; set; }
        public int HeadID { get; set; }
        public int BodyID { get; set; }

        public Rent_Update_v2()
        {
            InitializeComponent();
        }

        private void Rent_Update_v2_Load(object sender, EventArgs e)
        {
            Fillcombobox();
        }

        private void Fillcombobox()
        {
            try
            {
                string Owner = "select * from d__Owner";
                DataTable OwnerDT = DbConnection.DBConnect(Owner);
                comboBox1.DataSource = OwnerDT;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
                comboBox1.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string Update = "dbo.Rent_Update_v2 '" + textEdit3.Text.Trim() + "','" + dateTimePicker1.Value.Date.ToString() + "','" + textEdit1.Text.Trim() + "','" + textEdit2.Text.Trim() + "'," + comboBox1.SelectedValue.ToString() + "," + HeadID + "," + BodyID;
                DbConnection.DBConnect(Update);
                Rent_Brodcast_Car main = this.Owner as Rent_Brodcast_Car;
                main.simpleButton1_Click(null, null);
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
