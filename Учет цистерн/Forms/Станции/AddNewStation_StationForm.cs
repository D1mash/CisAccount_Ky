using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class AddNewStation_StationForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        string User_AID;
        public AddNewStation_StationForm(string User_ID)
        {
            InitializeComponent();
            this.User_AID = User_ID;
        }

        private void button_OK_StationForm_Click(object sender, EventArgs e)
        {
            try
            {
                string FillStation = "exec [dbo].[FillStation] " + textBox_Add_Code_StationForm.Text.Trim() + "," + textBox_Add_Code6_StationForm.Text.Trim() + "," + textBox_Add_Name_StationForm.Text.Trim() +"','"+User_AID+"'";
                string SelectDubl = "select * from d__Station where Code = " + textBox_Add_Code_StationForm.Text.Trim();
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(SelectDubl);
                if (dt.Rows.Count == 0)
                {
                    DbConnection.DBConnect(FillStation);
                    this.Close();
                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StationForm main = this.Owner as StationForm;
                    main.btn_refsh_station_form_Click_1(null,null);
                }
                else
                {
                    MessageBox.Show("Станция с кодом: " + textBox_Add_Code_StationForm.Text.Trim() + " имеется в справочнике", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Add_Cancel_StationForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
