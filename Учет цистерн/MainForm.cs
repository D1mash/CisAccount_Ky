using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;


namespace Учет_цистерн
{
    public partial class MainForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public MainForm(string FIO)
        {
            InitializeComponent();
            //IntPtr UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Token;
            this.Text = "Учет вагонов-цистерн. Батыс Петролеум ТОО - "+FIO;
            tabControl1.Hide();
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            qCargo qCargoForm = new qCargo();
            qCargoForm.Show();
            */

            contextMenuStrip_Product.Show(button1, new Point(0, button1.Height));
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Вы действительно хотите закрыть программу?";
            string title = "Закрытие программы";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string get_user_aid = "select dbo.get_user_aid() S";
                SqlDataAdapter sda = new SqlDataAdapter(get_user_aid, con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                string user_aid = dtbl.Rows[0][0].ToString();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUDIT_USER SET DATE_OUT = GETDATE(), IS_DEAD = 1 WHERE ID_SESSION = @@spid and (IS_DEAD IS NULL OR DATE_OUT IS NULL)";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);

                con.Close();
                Environment.Exit(0);
            }
            else
            {
                // Do something  
            }
        }

        private void ToolStripMenuItem1_Product_Click(object sender, EventArgs e)
        {
            Form_Product frm = new Form_Product();
            tabControl1.Show();
            TabPage firstPage = new TabPage("Продукты");
            tabControl1.TabPages.Add(firstPage);
            tabControl1.SelectedTab = firstPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            firstPage.Controls.Add(frm);
        }
    }
}
