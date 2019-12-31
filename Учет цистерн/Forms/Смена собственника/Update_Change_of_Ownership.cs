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
    public partial class Update_Change_of_Ownership : Form
    {
        int id;
        string selectNumber;

        public Update_Change_of_Ownership(int selectItemRow, string selectNumber_Rent)
        {
            InitializeComponent();
            this.id = selectItemRow;
            this.selectNumber = selectNumber_Rent;
        }

        private void Update_Change_of_Ownership_Load(object sender, EventArgs e)
        {
            string GetRentStatus = "select * from d__Rent_Status where Number = " + selectNumber;
            DataTable dataTable = DbConnection.DBConnect(GetRentStatus);

            textBox1.Text = dataTable.Rows[0][1].ToString();
            dateEdit1.EditValue = dataTable.Rows[0][2].ToString();
           
            string getOwner = "Select ID, Name from d__Owner";
            DataTable data = DbConnection.DBConnect(getOwner);
            comboBox1.DataSource = data;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";

        string GetRentCrriage = "select* from Rent_Carriage where Status_Rent = " + id;
            DataTable dt = DbConnection.DBConnect(GetRentCrriage);

            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
        }
    }
}
