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
        int SelectItemRow;


        public Update_Change_of_Ownership(int selectItemRow, string selectNumber_Rent)
        {
            InitializeComponent();
            this.id = selectItemRow;
            this.selectNumber = selectNumber_Rent;
        }

        public int SelectOwId { get; set; }

        private void Update_Change_of_Ownership_Load(object sender, EventArgs e)
        {
            string GetRentStatus = "select * from d__Rent_Status where Number = " + selectNumber;
            DataTable dataTable = DbConnection.DBConnect(GetRentStatus);

            textBox1.Text = dataTable.Rows[0][1].ToString();
            dateEdit1.EditValue = dataTable.Rows[0][2].ToString();
           
            string getOwner = "Select * from d__Owner";
            DataTable data = DbConnection.DBConnect(getOwner);
            comboBox1.DataSource = data;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectOwId", true, DataSourceUpdateMode.OnPropertyChanged);

            string GetRentCrriage = "select* from Rent_Carriage where Status_Rent = " + id;
            DataTable dt = DbConnection.DBConnect(GetRentCrriage);

            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string Id = gridView1.GetFocusedDataRow()[0].ToString();
            SelectItemRow = Convert.ToInt32(Id);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string Upadte_Rent_Body_1 = "exec dbo.Rent_Update_Body '" + gridView1.GetFocusedDataRow()[1] + "',1," + SelectItemRow;
            DbConnection.DBConnect(Upadte_Rent_Body_1);


            string Upadte_Rent_Body_2 = "exec dbo.Rent_Update_Body '" + gridView1.GetFocusedDataRow()[2] + "',2," + SelectItemRow;
            DbConnection.DBConnect(Upadte_Rent_Body_2);
        }
    }
}
