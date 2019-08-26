using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class BrigadeAddForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public BrigadeAddForm()
        {
            InitializeComponent();
        }

        int yes;
        int not;

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
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

            if (checkBox1.Checked)
            {
                yes = 1;
                cmd.CommandText = "insert into d__Brigade values('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','"+textBox3.Text.Trim()+' '+textBox1.Text.Substring(0,1)+'.'+textBox2.Text.Substring(0,1)+'.'+"',"+ yes + ")";
            }
            else
            {
                not = 0;
                cmd.CommandText = "insert into d__Brigade values('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','"+textBox3.Text.Trim()+' '+textBox1.Text.Substring(0, 1)+'.'+textBox2.Text.Substring(0, 1)+'.'+"',"+ not + ")";
            }
            
            //cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(dt);
            MessageBox.Show("Добавлен сотрудник!");
            con.Close();
        }
    }
}
