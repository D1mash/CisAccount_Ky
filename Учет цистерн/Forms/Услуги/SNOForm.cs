using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;
using Учет_цистерн.Forms.СНО;

namespace Учет_цистерн.Forms.СНО
{
    public partial class SnoForm : Form
    {
        public SnoForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;
        int SelectContragentID;

        void GetSNO()
        {
            string GetSNO = "exec dbo.GetSNO";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetSNO);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        private void SnoForm_Load(object sender, EventArgs e)
        {
            GetSNO();

            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
        }

        private void dataGridView1_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Decimal Capacity = 0;
            Decimal Cost = 0;
            Decimal SumOutVat = 0;
            Decimal SumVat = 0;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() != string.Empty)
                {
                    Capacity += Convert.ToDecimal(this.dataGridView1[3, i].Value);
                }
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() != string.Empty)
                {
                    Cost += Convert.ToDecimal(this.dataGridView1[4, i].Value);
                }
                if (dataGridView1.Rows[i].Cells[5].Value.ToString() != string.Empty)
                {
                    SumOutVat += Convert.ToDecimal(this.dataGridView1[5, i].Value);
                }
                if (dataGridView1.Rows[i].Cells[7].Value.ToString() != string.Empty)
                {
                    SumVat += Convert.ToDecimal(this.dataGridView1[7, i].Value);
                }
            }

            panel4.Width = this.dataGridView1.RowHeadersWidth - 3;
            panel4.Location = new Point(5, this.dataGridView1.Height - (panel4.Height - 15));
            panel4.Visible = true;

            int Xdgvx_Panel1 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
            panel1.Width = this.dataGridView1.Columns[2].Width;
            Xdgvx_Panel1 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
            panel1.Location = new Point(Xdgvx_Panel1, this.dataGridView1.Height - (panel1.Height - 15));
            panel1.Visible = true;

            int Xdgvx_TextBox1 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
            textBox1.Width = this.dataGridView1.Columns[3].Width;
            Xdgvx_TextBox1 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
            textBox1.Location = new Point(Xdgvx_TextBox1, this.dataGridView1.Height - (textBox1.Height - 15));
            textBox1.Text = Capacity.ToString();
            textBox1.Visible = true;

            int Xdgvx_TextBox2 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
            textBox2.Width = this.dataGridView1.Columns[4].Width;
            Xdgvx_TextBox2 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
            textBox2.Location = new Point(Xdgvx_TextBox2, this.dataGridView1.Height - (textBox2.Height - 15));
            textBox2.Text = Cost.ToString();
            textBox2.Visible = true;

            int Xdgvx_TextBox3 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
            textBox3.Width = this.dataGridView1.Columns[5].Width;
            Xdgvx_TextBox3 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
            textBox3.Location = new Point(Xdgvx_TextBox3, this.dataGridView1.Height - (textBox3.Height - 15));
            textBox3.Text = SumOutVat.ToString();
            textBox3.Visible = true;

            int Xdgvx_Panel2 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
            panel2.Width = this.dataGridView1.Columns[6].Width;
            Xdgvx_Panel2 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
            panel2.Location = new Point(Xdgvx_Panel2, this.dataGridView1.Height - (panel2.Height - 15));
            panel2.Visible = true;

            int Xdgvx_TextBox4 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
            textBox4.Width = this.dataGridView1.Columns[7].Width;
            Xdgvx_TextBox4 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
            textBox4.Location = new Point(Xdgvx_TextBox4, this.dataGridView1.Height - (textBox4.Height - 15));
            textBox4.Text = SumVat.ToString();
            textBox4.Visible = true;

            int Xdgvx_Panel3 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
            panel3.Width = this.dataGridView1.Columns[8].Width + 2;
            Xdgvx_Panel3 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
            panel3.Location = new Point(Xdgvx_Panel3, this.dataGridView1.Height - (panel3.Height - 15));
            panel3.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[
                    e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                string ContragentID = row.Cells["ContragentID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectContragentID = Convert.ToInt32(ContragentID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SnoAddForm snoAddForm = new SnoAddForm();
            snoAddForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SnoUpdateForm SnoUpdateForm = new SnoUpdateForm();
                SnoUpdateForm.SelectID = SelectItemRow;
                SnoUpdateForm.SelectContragentID = SelectContragentID;
                SnoUpdateForm.textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                SnoUpdateForm.textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                SnoUpdateForm.textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                SnoUpdateForm.textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                SnoUpdateForm.textBox5.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                SnoUpdateForm.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                SnoUpdateForm.Show();
            }
            catch (Exception ex)
            {
                ExceptionForm exf = new ExceptionForm();
                exf.label1.Text = "Для редактирования записи, необходимо указать строку! " + ex.Message;
                exf.Show();
                //MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Вы действительно хотите удалить эту запись?";
            string title = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                string Delete = "delete from d__SNO where ID = " + SelectItemRow;
                DbConnection.DBConnect(Delete);
                OkForm ok = new OkForm();
                ok.label1.Text = "Запись удалена!";
                ok.Show();
                GetSNO();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetSNO();
        }
    }
}
