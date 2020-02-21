using System;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                UpdateCurrentBrigade = "update d__Brigade " +
                                       "set Name = '" + textBox1.Text.Trim() + "', " +
                                       "Surname = '" + textBox2.Text.Trim() + "'," +
                                       "Lastname = '" + textBox3.Text.Trim() + "', " +
                                       "FIO = '" + textBox2.Text.Trim() + ' ' + textBox1.Text.Substring(0, 1) + '.' + textBox3.Text.Substring(0, 1) + '.' + "'" +
                                       "where ID = " + selectID;
                DataTable dtbl = new DataTable();
                dtbl = DbConnection.DBConnect(UpdateCurrentBrigade);
                this.Close();
                MessageBox.Show("Запись изменена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BrigadeForm main = this.Owner as BrigadeForm;
                main.Refreshh();
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
