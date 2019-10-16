using System;
using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class UpdtCurrentStation_StationForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public UpdtCurrentStation_StationForm()
        {
            InitializeComponent();
        }

        int selectStationID;

        public int SelectStationID_Method
        {
            get { return SelectStationID_Method; }
            set { selectStationID = value; }
        }

        private void button_Updt_OK_StationForm_Click(object sender, EventArgs e)
        {
            string Updt_CurrentStation =
                "update d__Station " +
                "set Name = '" + textBox_Updt_Name_StationForm.Text.Trim() + "', Code = " + Convert.ToInt32(textBox_Updt_Code_StationForm.Text.Trim()) + ", Code6 = " + Convert.ToInt32(textBox_Updt_Code6_StationForm.Text.Trim()) + " " +
                "where ID = " + selectStationID;
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(Updt_CurrentStation);
            this.Close();
            MessageBox.Show("Станция изменена!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button_Updt_Cancel_StationForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdtCurrentStation_StationForm_Load(object sender, EventArgs e)
        {
            textBox_Updt_Name_StationForm.Enabled = false;
            textBox_Updt_Code_StationForm.Enabled = false;
            textBox_Updt_Code6_StationForm.Enabled = false;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            textBox_Updt_Name_StationForm.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            textBox_Updt_Code_StationForm.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            textBox_Updt_Code6_StationForm.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }
    }
}
