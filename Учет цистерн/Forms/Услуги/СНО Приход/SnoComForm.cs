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

namespace Учет_цистерн.Forms.Услуги.СНО_Приход
{
    public partial class SnoComForm : Form
    {
        BindingSource source = new BindingSource();
        string role;

        public SnoComForm(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        int SelectItemRow;
 
        private void dataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            this.source.Filter = this.dataGridView1.FilterString;
        }

        private void dataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            this.source.Sort = this.dataGridView1.SortString;
        }

        private void SnoComForm_Load(object sender, EventArgs e)
        {
            if (role == "1")
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                if (role == "2")
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
            }

            GetSNO();

            panel3.Visible = false;
            panel4.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
        }

        private void GetSNO()
        {
            try
            {
                string GetSNO = "exec dbo.GetCurrentSNO";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetSNO);
                source.DataSource = dataTable;
                dataGridView1.DataSource = source;
                dataGridView1.Columns[0].Visible = false;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    string Id = row.Cells["ID"].Value.ToString();
                    SelectItemRow = Convert.ToInt32(Id);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(SnoComAddForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                SnoComAddForm snoComAddForm = new SnoComAddForm();
                snoComAddForm.ShowDialog();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(SnoComUpdateFrom))
                    {
                        form.Activate();
                        return;
                    }
                }
                SnoComUpdateFrom snoComUpdateFrom = new SnoComUpdateFrom();
                snoComUpdateFrom.SelectID = SelectItemRow;
                snoComUpdateFrom.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                snoComUpdateFrom.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                snoComUpdateFrom.textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                snoComUpdateFrom.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                snoComUpdateFrom.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Delete = "delete from d__CurrentSNO where ID = " + SelectItemRow;
                        DbConnection.DBConnect(Delete);
                        MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetSNO();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetSNO();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                Decimal SumAll = 0;
                Decimal SumOne = 0;
                Decimal SumTwo = 0;
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() != string.Empty)
                    {
                        SumAll += Convert.ToDecimal(this.dataGridView1[1, i].Value);
                    }
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString() != string.Empty)
                    {
                        SumOne += Convert.ToDecimal(this.dataGridView1[2, i].Value);
                    }
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() != string.Empty)
                    {
                        SumTwo += Convert.ToDecimal(this.dataGridView1[3, i].Value);
                    }
                }

                panel4.Width = this.dataGridView1.RowHeadersWidth;
                panel4.Location = new Point(5, this.dataGridView1.Height - (panel4.Height - 15));
                panel4.Visible = true;

                int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
                textBox1.Width = this.dataGridView1.Columns[1].Width;
                Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                textBox1.Location = new Point(Xdgvx1, this.dataGridView1.Height - (textBox1.Height - 15));
                textBox1.Visible = true;
                textBox1.Text = SumAll.ToString();

                int Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
                textBox2.Width = this.dataGridView1.Columns[2].Width;
                Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
                textBox2.Location = new Point(Xdgvx2, this.dataGridView1.Height - (textBox2.Height - 15));
                textBox2.Visible = true;
                textBox2.Text = SumOne.ToString();

                int Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
                textBox3.Width = this.dataGridView1.Columns[3].Width;
                Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                textBox3.Location = new Point(Xdgvx3, this.dataGridView1.Height - (textBox3.Height - 15));
                textBox3.Visible = true;
                textBox3.Text = SumTwo.ToString();

                int Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
                panel3.Width = this.dataGridView1.Columns[4].Width + 2;
                Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
                panel3.Location = new Point(Xdgvx4, this.dataGridView1.Height - (panel3.Height - 15));
                panel3.Visible = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
