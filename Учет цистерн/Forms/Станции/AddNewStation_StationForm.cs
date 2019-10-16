using System;
using System.Data;
using System.Windows.Forms;

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
                MessageBox.Show("Запись добавлена!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Станция с кодом: " + textBox_Add_Code_StationForm.Text.Trim() + " имеется в справочнике","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button_Add_Cancel_StationForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
