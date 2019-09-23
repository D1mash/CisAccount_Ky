using System;
using System.Data;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

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
            string AddNewCarriage = "exec dbo.FillCarriage '" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + comboBox1.SelectedValue.ToString() + "'";
            string SelectDubl = "select * from d__Carriage where Carnumber = " + textBox1.Text.Trim();
            DataTable dt = new DataTable();
            dt = DbConnection.DBConnect(SelectDubl);
            if (dt.Rows.Count == 0)
            {
                DbConnection.DBConnect(AddNewCarriage);
                this.Close();
                OkForm okForm = new OkForm();
                okForm.label1.Text = "Запись добавлена!";
                okForm.Show();
                //MessageBox.Show("Запись добавлена!");
            }
            else
            {
                ExceptionForm exf = new ExceptionForm();
                exf.label1.Text = "Вагон с номером: " + textBox1.Text.Trim() + " уже имеется в справочнике!";
                exf.Show();
                //MessageBox.Show("Вагон с номером: " + textBox1.Text.Trim() + " уже имеется в справочнике!");
            }
        }
    }
}
