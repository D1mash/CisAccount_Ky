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
        TradeWright.UI.Forms.TabControlExtra TabControlExtra;


        public Update_Change_of_Ownership(int selectItemRow, string selectNumber_Rent, TradeWright.UI.Forms.TabControlExtra tabControlExtra)
        {
            InitializeComponent();
            this.id = selectItemRow;
            this.selectNumber = selectNumber_Rent;
            this.TabControlExtra = tabControlExtra;
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

            RefreshGrid();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string Id = gridView1.GetFocusedDataRow()[0].ToString();
            SelectItemRow = Convert.ToInt32(Id);
        }


        // Не работает
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string Upadte_Rent_Body_1 = "exec dbo.Rent_Update_Body '" + gridView1.GetFocusedDataRow()[1] + "',1," + SelectItemRow;
            DbConnection.DBConnect(Upadte_Rent_Body_1);


            string Upadte_Rent_Body_2 = "exec dbo.Rent_Update_Body '" + gridView1.GetFocusedDataRow()[2] + "',2," + SelectItemRow;
            DbConnection.DBConnect(Upadte_Rent_Body_2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Update_Rent_Head = "exec Rent_Update_Head '" + textBox1.Text + "','" + dateEdit1.DateTime.ToShortDateString() + "', " +comboBox1.SelectedValue.ToString() + "," + id;
            DbConnection.DBConnect(Update_Rent_Head);

            TabControlExtra.TabPages.Remove(TabControlExtra.SelectedTab);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string new_row = "exec [dbo].[Rent_Add_Body] '" + id + "'";
            DbConnection.DBConnect(new_row);

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            string refresh = "Select Id,Number_Carriage [№ Вагона], Product [Продукт] from Rent_Carriage Where Status_Rent = '" + id + "'";
            DataTable dt = DbConnection.DBConnect(refresh);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
        }
    }
}
