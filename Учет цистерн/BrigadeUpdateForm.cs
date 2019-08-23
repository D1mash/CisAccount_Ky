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
    public partial class BrigadeUpdateForm : Form
    {
        public BrigadeUpdateForm()
        {
            InitializeComponent();
        }

        int selectID;
        int yes;
        int not;
        string UpdateCurrentBrigade;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                yes = 1;
                UpdateCurrentBrigade = "update d__Brigade Name = '" + textBox1.Text.Trim() + "', Surname = " + textBox1.Text.Trim() + "Lastname = " + textBox1.Text.Trim() + " FIO = " + textBox1.Text.Trim() + "Active = "+yes+" where ID = " + selectID;
            }
            else
            {
                not = 0;
                UpdateCurrentBrigade = "update d__Brigade Name = '" + textBox1.Text.Trim() + "', Surname = " + textBox1.Text.Trim() + "Lastname = " + textBox1.Text.Trim() + " FIO = " + textBox1.Text.Trim() + "Active = "+not+" where ID = " + selectID;
            }
                //SqlDataAdapter sda = new SqlDataAdapter(UpdateCurrentProduct, con);
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(UpdateCurrentBrigade);
            //sda.Fill(dtbl);
            //con.Close();
            this.Close();
            MessageBox.Show("Изменён!");
        }
    }
}
