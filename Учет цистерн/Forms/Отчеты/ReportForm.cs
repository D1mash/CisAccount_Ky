using System;
using System.Data;
using System.Windows.Forms;


namespace Учет_цистерн
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        BindingSource source = new BindingSource();

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dateTimePicker1.Value = startDate;
            dateTimePicker2.Value = endDate;

            string Refresh = "dbo.GetReportRenderedServices '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Refresh);
            source.DataSource = dataTable;
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;

        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            //String Station = "Select * from d__Station";
            //DataTable StationDT = DbConnection.DBConnect(Station);
            //comboBox1.DataSource = StationDT;
            //comboBox1.DisplayMember = "Name";
            //comboBox1.ValueMember = "ID";

            String Owner = "Select * from d__Owner";
            DataTable OwnerDT = DbConnection.DBConnect(Owner);
            comboBox2.DataSource = OwnerDT;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
        }

        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            
            try
            {
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Обработанные вагоны";

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check.
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx|All files(*.*)|*.*";
                saveFileDialog.FilterIndex = 2;
                
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Успешно выгружен");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                app.Quit();
                workbook = null;
                app = null;
            }
            
        }
    }
}
