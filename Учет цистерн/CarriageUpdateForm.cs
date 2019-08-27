using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class CarriageUpdateForm : Form
    {
        public CarriageUpdateForm()
        {
            InitializeComponent();
        }

        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void CarriageUpdateForm_Load(object sender, EventArgs e)
        {
            string GetCarriageUpd = "Select dc.CarNumber,AXIS, do.Name,FullName From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID where dc.ID = ";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetCarriageUpd+SelectID);
            textBox1.Text = dataTable.Rows[0]["CarNumber"].ToString();
        }
    }
}
