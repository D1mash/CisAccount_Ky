using System;
using System.Data;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

namespace Учет_цистерн
{
    public partial class AddNewStation_StationForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public AddNewStation_StationForm()
        {
            InitializeComponent();
        }

        private void button_OK_StationForm_Click(object sender, EventArgs e)
        {
            string FillStation = "exec [dbo].[FillStation] " + textBox_Add_Code_StationForm.Text.Trim() + "," + textBox_Add_Code6_StationForm.Text.Trim() + "," + textBox_Add_Name_StationForm.Text.Trim();
            string SelectDubl = "select * from d__Station where Code = " + textBox_Add_Code_StationForm.Text.Trim();
            DataTable dt = new DataTable();
            dt = DbConnection.DBConnect(SelectDubl);
            if (dt.Rows.Count == 0)
            {
                DbConnection.DBConnect(FillStation);
                this.Close();
                OkForm ok = new OkForm();
                ok.label1.Text = "Запись добавлена!";
                ok.Show();
                //MessageBox.Show("Запись добавлена!");
            }
            else
            {
                ExceptionForm exf = new ExceptionForm();
                exf.label1.Text = "Станция с кодом: " + textBox_Add_Code_StationForm.Text.Trim() + " имеется в справочнике";
                exf.Show();
                //MessageBox.Show("Станция с кодом: " + textBox_Add_Code_StationForm.Text.Trim() + " имеется в справочнике");
            }
        }

        private void button_Add_Cancel_StationForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
