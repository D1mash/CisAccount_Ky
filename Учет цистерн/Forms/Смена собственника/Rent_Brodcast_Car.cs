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
    public partial class Rent_Brodcast_Car : Form
    {
        public Rent_Brodcast_Car()
        {
            InitializeComponent();
        }

        private void Rent_Brodcast_Car_Load(object sender, EventArgs e)
        {
            checkEdit1_CheckedChanged(null, null);
            checkEdit3_CheckedChanged(null, null);
            checkEdit4_CheckedChanged(null, null);
            checkEdit5_CheckedChanged(null, null);
            checkEdit6_CheckedChanged(null, null);
            checkEdit7_CheckedChanged(null, null);
            checkEdit8_CheckedChanged(null, null);
        }

        private void FillCombobox()
        {
            try
            {
                string getOwner = "Select ID, Name from d__Owner";
                DataTable dt = DbConnection.DBConnect(getOwner);

                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "ID";
                comboBox1.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                textEdit1.Enabled = (checkEdit1.CheckState == CheckState.Checked);
            }
            else
            {
                textEdit1.Enabled = false;
                textEdit1.Text = String.Empty;
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
            {
                dateEdit1.Enabled = (checkEdit3.CheckState == CheckState.Checked);

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                dateEdit1.EditValue = startDate;

                checkEdit5.Checked = false;
            }
            else
            {
                dateEdit1.Enabled = false;
                dateEdit1.EditValue = DBNull.Value;
            }
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked)
            {
                dateEdit2.Enabled = (checkEdit4.CheckState == CheckState.Checked);

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                dateEdit2.EditValue = endDate;

                checkEdit5.Checked = false;
            }
            else
            {
                dateEdit2.Enabled = false;
                dateEdit2.EditValue = DBNull.Value;
            }
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit5.Checked)
            {
                dateEdit3.Enabled = (checkEdit5.CheckState == CheckState.Checked);

                dateEdit3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                dateEdit3.Properties.Mask.EditMask = "d"; //'Short date' format 
                dateEdit3.Properties.Mask.UseMaskAsDisplayFormat = true;
                dateEdit3.EditValue = DateTime.Today;

                checkEdit3.Checked = false;
                checkEdit4.Checked = false;
            }
            else
            {
                dateEdit3.Enabled = false;
                dateEdit3.EditValue = DBNull.Value;
            }
        }

        private void checkEdit6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit6.Checked)
            {
                textEdit2.Enabled = (checkEdit6.CheckState == CheckState.Checked);
            }
            else
            {
                textEdit2.Enabled = false;
                textEdit2.Text = String.Empty;
            }
        }

        private void checkEdit7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit7.Checked)
            {
                comboBox1.Enabled = (checkEdit7.CheckState == CheckState.Checked);

                FillCombobox();
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.DataSource = null;
            }
        }

        private void checkEdit8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit8.Checked)
            {
                textEdit3.Enabled = (checkEdit8.CheckState == CheckState.Checked);
            }
            else
            {
                textEdit3.Enabled = false;
                textEdit3.Text = String.Empty;
            }
        }

        //Кнопка Поиск
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Поиск по номеру вагона
            if (checkEdit1.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Car_Num = '" + textEdit1.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if (checkEdit1.Checked & (checkEdit3.Checked | checkEdit4.Checked))
            {
                //Поиск по Start Date и End Date и номеру вагона
                if (checkEdit3.Checked & checkEdit4.Checked)
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();

                    string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Start = '" + dateEdit1.DateTime.ToShortDateString() + "', @Date_End = '" + dateEdit2.DateTime.ToShortDateString() + "', " + "@Car_Num = '" + textEdit1.Text + "'";
                    gridControl1.DataSource = DbConnection.DBConnect(Search);
                    gridView1.Columns[0].Visible = false;
                }
                else
                {
                    //Поиск по Start Date и номеру вагона
                    if (checkEdit3.Checked)
                    {
                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();

                        string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Start = '" + dateEdit1.DateTime.ToShortDateString() + "', " + "@Car_Num = '" + textEdit1.Text + "'";
                        gridControl1.DataSource = DbConnection.DBConnect(Search);
                        gridView1.Columns[0].Visible = false;
                    }
                    else
                    {
                        //Поиск по End Date и номеру вагона
                        if (checkEdit4.Checked)
                        {
                            gridControl1.DataSource = null;
                            gridView1.Columns.Clear();

                            string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_End = '" + dateEdit2.DateTime.ToShortDateString() + "', " + "@Car_Num = '" + textEdit1.Text + "'";
                            gridControl1.DataSource = DbConnection.DBConnect(Search);
                            gridView1.Columns[0].Visible = false;
                        }
                    }
                }
            }

            //Поиск по Заявке и номеру вагона
            if (checkEdit1.Checked & ((checkEdit5.Checked | checkEdit6.Checked) || (checkEdit7.Checked | checkEdit8.Checked)))
            {
                    //Поиск по дате заявки
                    if (checkEdit5.Checked)
                    {
                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();

                        string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Rec = '" + dateEdit3.DateTime.ToShortDateString() + "', " + " @Car_Num = '" + textEdit1.Text + "'";
                        gridControl1.DataSource = DbConnection.DBConnect(Search);
                        gridView1.Columns[0].Visible = false;
                    }

                    //Поиск по номеру заявки
                    if (checkEdit6.Checked)
                    {
                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();

                        string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Rent_Num '" + textEdit2.Text + "'," + " @Car_Num = '" + textEdit1.Text + "'";
                        gridControl1.DataSource = DbConnection.DBConnect(Search);
                        gridView1.Columns[0].Visible = false;
                    }

                    //Поиск по собственнику
                    if (checkEdit7.Checked)
                    {
                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();

                        string Search = "exec dbo.Rent_Search_By_Parametrs " + "@OwnerId = '" + comboBox1.SelectedValue + "'," + " @Car_Num = '" + textEdit1.Text + "'";
                        gridControl1.DataSource = DbConnection.DBConnect(Search);
                        gridView1.Columns[0].Visible = false;
                    }

                    //Поиск по продукту
                    if (checkEdit8.Checked)
                    {
                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();

                        string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Product = '" + textEdit3.Text + "'";
                        gridControl1.DataSource = DbConnection.DBConnect(Search);
                        gridView1.Columns[0].Visible = false;
                    }
            }


            //Поиск по Start Date и End Date
            if (checkEdit3.Checked & checkEdit4.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Start = '" + dateEdit1.DateTime.ToShortDateString() + "', @Date_End = '" + dateEdit2.DateTime.ToShortDateString() + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по Start Date
            if (checkEdit3.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Start = '" + dateEdit1.DateTime.ToShortDateString() + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }
            //Поиск по End Date
            if (checkEdit4.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_End = '" + dateEdit2.DateTime.ToShortDateString() + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по дате заявки
            if (checkEdit5.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Rec = '" + dateEdit3.DateTime.ToShortDateString() + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по номеру заявки
            if (checkEdit6.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Rent_Num = '" + textEdit2.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по собственнику
            if (checkEdit7.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@OwnerId = '" + comboBox1.SelectedValue + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по продукту
            if (checkEdit8.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Product = '" + textEdit3.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по дате заявки и номеру заявки
            if (checkEdit5.Checked & checkEdit6.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Rec = '" + dateEdit3.DateTime.ToShortDateString() + "', " + "@Rent_Num ='" + textEdit2.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по дате заявки и собственнику
            if (checkEdit5.Checked & checkEdit7.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Rec = '" + dateEdit3.DateTime.ToShortDateString() + "', " + "@OwnerId = '" + comboBox1.SelectedValue + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            //Поиск по дате заявки и продукту
            if (checkEdit5.Checked & checkEdit8.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Rec = '" + dateEdit3.DateTime.ToShortDateString() + "', " + "@Product = '" + textEdit3.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if(checkEdit6.Checked & checkEdit7.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Rent_Num = '" + textEdit2.Text + "', " + "@OwnerId = '" + comboBox1.SelectedValue + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if(checkEdit6.Checked & checkEdit8.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Rent_Num = '" + textEdit2.Text + "', " + "@Product = '" + textEdit3.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if (checkEdit7.Checked & checkEdit8.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@OwnerId = '" + comboBox1.SelectedValue + "', " + "@Product = '" + textEdit3.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if(checkEdit3.Checked & checkEdit6.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Start = '" + dateEdit1.DateTime.ToShortDateString() + "', " + "@Rent_Num = '" + textEdit2.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if (checkEdit3.Checked & checkEdit7.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Start = '" + dateEdit1.DateTime.ToShortDateString() + "', " + "@OwnerId = '" + comboBox1.SelectedValue + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if(checkEdit3.Checked & checkEdit8.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_Start = '" + dateEdit1.DateTime.ToShortDateString() + "', " + "@Product = '" + textEdit3.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if(checkEdit4.Checked & checkEdit6.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_End = '" + dateEdit2.DateTime.ToShortDateString() + "', " + "@Rent_Num = '" + textEdit2.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if(checkEdit4.Checked & checkEdit7.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_End = '" + dateEdit2.DateTime.ToShortDateString() + "', " + "@OwnerId = '" + comboBox1.SelectedValue + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

            if(checkEdit4.Checked & checkEdit8.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                string Search = "exec dbo.Rent_Search_By_Parametrs " + "@Date_End = '" + dateEdit2.DateTime.ToShortDateString() + "', " + "@Product = '" + textEdit3.Text + "'";
                gridControl1.DataSource = DbConnection.DBConnect(Search);
                gridView1.Columns[0].Visible = false;
            }

        }
    }
}
