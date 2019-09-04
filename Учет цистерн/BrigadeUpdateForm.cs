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
                UpdateCurrentBrigade = "update d__Brigade " +
                                       "set Name = '" + textBox1.Text.Trim() + "', " +
                                       "Surname = '" + textBox2.Text.Trim() + "'," +
                                       "Lastname = '" + textBox3.Text.Trim() + "', " +
                                       "FIO = '"+textBox3.Text.Trim()+' '+textBox1.Text.Substring(0,1)+'.'+textBox2.Text.Substring(0,1)+'.'+"', " +
                                       "Active = "+yes+" " +
                                       "where ID = " + selectID;
            }
            else
            {
                not = 0;
                UpdateCurrentBrigade = "update d__Brigade " +
                                       "set Name = '" + textBox1.Text.Trim() + "', " +
                                       "Surname = '" + textBox2.Text.Trim() + "'," +
                                       "Lastname = '" + textBox3.Text.Trim() + "', " +
                                       "FIO = '" + textBox3.Text.Trim() + ' ' + textBox1.Text.Substring(0, 1) + '.' + textBox2.Text.Substring(0, 1) + '.' + "', " +
                                       "Active = " + not + " " +
                                       "where ID = " + selectID;
            }
            DataTable dtbl = new DataTable();
            dtbl = DbConnection.DBConnect(UpdateCurrentBrigade);
            this.Close();
            MessageBox.Show("Запись изменена!");
        }

        private void BrigadeUpdateForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = (checkBox4.CheckState == CheckState.Checked);
        }
    }
}
