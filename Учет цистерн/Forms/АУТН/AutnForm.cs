using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.АУТН
{
    public partial class AutnForm : Form
    {
        private string Carnum = string.Empty;
        private int CurrentId;

        public AutnForm()
        {
            InitializeComponent();
        }
        private void AutnForm_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = now;

            RefreshBody(1);

            Block();

            simpleButton11.Visible = false;
        }

        public void RefreshBody(int section)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            string query = "exec dbo.GetAutn '" + dateTimePicker1.Value.ToShortDateString() + "'";
            DataTable dt = DbConnection.DBConnect(query);
            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            if (section == 1)
            {
                gridView1.MoveLast();
            }
            else
            {
                ColumnView View = (ColumnView)gridControl1.FocusedView;
                // obtaining the column bound to the Country field 
                GridColumn column = View.Columns["id"];
                if (column != null)
                {
                    int rhFound = View.LocateByDisplayText(View.FocusedRowHandle + 1, column, Convert.ToString(CurrentId));
                    View.FocusedRowHandle = rhFound;
                    View.FocusedColumn = column;
                }
            }
        }

        private void Block()
        {
            textEdit12.Enabled = false;
            textEdit13.Enabled = false;
            textEdit14.Enabled = false;
            textEdit15.Enabled = false;
            textEdit16.Enabled = false;
            textEdit17.Enabled = false;
            textEdit18.Enabled = false;
            textEdit19.Enabled = false;
            textEdit20.Enabled = false;
            textEdit21.Enabled = false;
            textEdit22.Enabled = false;
            textEdit23.Enabled = false;
            textEdit24.Enabled = false;
            simpleButton1.Enabled = false;
        }

        private void Unblock()
        {
            textEdit12.Enabled = true;
            textEdit13.Enabled = true;
            textEdit14.Enabled = true;
            textEdit15.Enabled = true;
            textEdit16.Enabled = true;
            textEdit17.Enabled = true;
            textEdit18.Enabled = true;
            textEdit19.Enabled = true;
            textEdit20.Enabled = true;
            textEdit21.Enabled = true;
            textEdit22.Enabled = true;
            textEdit23.Enabled = true;
            textEdit24.Enabled = true;
            simpleButton1.Enabled = true;
        }
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = true;
            simpleButton10.Visible = false;
            simpleButton11.Visible = true;
            Unblock();
        }
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            simpleButton11.Visible = false;
            simpleButton10.Visible = true;
            Block();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DateTime dateTime = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());

            if (dateTime > today)
            {
                dateTimePicker1.Value = DateTime.Today;

                RefreshBody(1);
            }
            else
            {
                RefreshBody(1);
            }
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string Id = gridView1.GetFocusedDataRow()[0].ToString();
            CurrentId = Convert.ToInt32(Id);

            Carnum = gridView1.GetFocusedDataRow()[1].ToString();
            memoEdit1.Text = Carnum;


            string GetAutn = "select * from d__Autn_2 where ID = " + CurrentId;
            DataTable Autn = DbConnection.DBConnect(GetAutn);

            textEdit18.Text = Autn.Rows[0][1].ToString();
            textEdit12.Text = Autn.Rows[0][2].ToString();
            textEdit13.Text = Autn.Rows[0][3].ToString();
            textEdit14.Text = Autn.Rows[0][4].ToString();
            textEdit24.Text = Autn.Rows[0][5].ToString();
            textEdit15.Text = Autn.Rows[0][6].ToString();
            textEdit16.Text = Autn.Rows[0][7].ToString();
            textEdit23.Text = Autn.Rows[0][8].ToString();
            textEdit22.Text = Autn.Rows[0][9].ToString();
            textEdit21.Text = Autn.Rows[0][10].ToString();
            textEdit20.Text = Autn.Rows[0][11].ToString();
            textEdit19.Text = Autn.Rows[0][12].ToString();
            textEdit17.Text = Autn.Rows[0][13].ToString();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string Avail, Klapan, DemSkob, PTC, Ushki, Skobi, Shaiba, Lest, Greben, Barash, TriBolt, Exp, Note = string.Empty;

            Avail = textEdit18.Text.Trim();
            Klapan = textEdit12.Text.Trim();
            DemSkob = textEdit13.Text.Trim();
            PTC = textEdit14.Text.Trim();
            Ushki = textEdit24.Text.Trim();
            Skobi = textEdit15.Text.Trim();
            Shaiba = textEdit16.Text.Trim();
            Lest = textEdit23.Text.Trim();
            Greben = textEdit22.Text.Trim();
            Barash = textEdit21.Text.Trim();
            TriBolt = textEdit20.Text.Trim();
            Exp = textEdit19.Text.Trim();
            Note = textEdit17.Text.Trim();

            if((Avail != "" && Klapan != "" && DemSkob != "" && PTC != "" && Ushki != "" && Skobi != "" && Shaiba != "" && Lest != "" && Greben != "" && Barash != "" && TriBolt != "" && Exp != "" && Note != ""))
            {
                ArrayList rows = new ArrayList();
                List<Object> aList = new List<Object>();
                string Arrays = string.Empty;

                Int32[] selectedRowHandles = gridView1.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                        rows.Add(gridView1.GetDataRow(selectedRowHandle));
                }
                foreach (DataRow row in rows)
                {
                    aList.Add(row["ID"]);
                    Arrays = string.Join(" ", aList);
                    string UpdateAutnAll = "exec dbo.UpdateAutn '" + Avail + "','" + Klapan + "','" + DemSkob + "','" + PTC + "','" + Ushki + "','" + Skobi + "','" + Shaiba + "','" + Lest + "','" + Greben + "','" + Barash + "','" + TriBolt + "','" + Exp + "','" + Note + "','" + Arrays + "'";
                    DbConnection.DBConnect(UpdateAutnAll);
                }
                RefreshBody(2);
            }
            else
            {
                MessageBox.Show("Заполните пустые поля АУТН!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Удалить выделенные записи?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ArrayList rows = new ArrayList();
                    List<Object> aList = new List<Object>();
                    string Arrays = string.Empty;

                    Int32[] selectedRowHandles = gridView1.GetSelectedRows();
                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                            rows.Add(gridView1.GetDataRow(selectedRowHandle));
                    }
                    foreach (DataRow row in rows)
                    {
                        aList.Add(row["ID"]);
                        Arrays = string.Join(" ", aList);
                        string delete = "exec dbo.DeleteAutn '" + Arrays + "'";
                        DbConnection.DBConnect(delete);
                    }
                    RefreshBody(1);
                    if (gridView1.RowCount > 0)
                    {
                        gridView1_RowCellClick(null, null);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //AUTN
        private void textEdit18_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void textEdit13_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void textEdit14_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void textEdit24_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void textEdit15_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
        private void textEdit16_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void textEdit23_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
        private void textEdit22_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
        private void textEdit21_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
        private void textEdit20_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
        private void textEdit19_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
        private void textEdit12_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                gridView1_RowCellClick(null, null);
            }
            else
            {
                if (e.KeyCode == Keys.Down)
                {
                    gridView1_RowCellClick(null, null);
                }
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (View.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.ForeColor = Color.DarkBlue;
                e.Appearance.BackColor = Color.LightBlue;
            }
        }
    }
}
