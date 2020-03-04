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
    public partial class Rent_Update_v1 : Form
    {
        public int SelectOwnerID { get; set; }
        public int SelectedID { get; set; }
        string UserAID;

        public Rent_Update_v1(string User_ID)
        {
            InitializeComponent();
            this.UserAID = User_ID;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Rent_Update_v1_Load(object sender, EventArgs e)
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string Update = "dbo.Rent_Update_v1 '"+ UserAID + "','" + dateTimePicker1.Value.Date.ToString() + "','" + textEdit1.Text.Trim() + "','" + textEdit2.Text.Trim() + "'," + comboBox1.SelectedValue.ToString() + "," + SelectedID;
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
    }
}
