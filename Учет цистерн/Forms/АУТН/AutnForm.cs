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
        public AutnForm()
        {
            InitializeComponent();
        }
        private void AutnForm_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = now;

            RefreshBody();

            Block();

            simpleButton11.Visible = false;
        }

        public void RefreshBody()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            string query = "exec dbo.GetAutn '" + dateTimePicker1.Value.ToShortDateString() + "'";
            DataTable dt = DbConnection.DBConnect(query);
            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
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

            checkEdit3.Visible = true;
            checkEdit12.Visible = true;
            checkEdit13.Visible = true;
            checkEdit14.Visible = true;
            checkEdit15.Visible = true;
            checkEdit16.Visible = true;
            checkEdit17.Visible = true;
            checkEdit18.Visible = true;
            checkEdit19.Visible = true;
            checkEdit20.Visible = true;
            checkEdit21.Visible = true;
            checkEdit22.Visible = true;
            checkEdit23.Visible = true;
        }
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            simpleButton11.Visible = false;
            simpleButton10.Visible = true;
            checkEdit3.Visible = false;
            checkEdit12.Visible = false;
            checkEdit13.Visible = false;
            checkEdit14.Visible = false;
            checkEdit15.Visible = false;
            checkEdit16.Visible = false;
            checkEdit17.Visible = false;
            checkEdit18.Visible = false;
            checkEdit19.Visible = false;
            checkEdit20.Visible = false;
            checkEdit21.Visible = false;
            checkEdit22.Visible = false;
            checkEdit23.Visible = false;

            checkEdit3.Checked = false;
            checkEdit12.Checked = false;
            checkEdit13.Checked = false;
            checkEdit14.Checked = false;
            checkEdit15.Checked = false;
            checkEdit16.Checked = false;
            checkEdit17.Checked = false;
            checkEdit18.Checked = false;
            checkEdit19.Checked = false;
            checkEdit20.Checked = false;
            checkEdit21.Checked = false;
            checkEdit22.Checked = false;
            checkEdit23.Checked = false;

            Block();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DateTime dateTime = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());

            if (dateTime > today)
            {
                dateTimePicker1.Value = DateTime.Today;

                RefreshBody();
            }
            else
            {
                RefreshBody();
            }
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string Id = gridView1.GetFocusedDataRow()[0].ToString();
            int CurrentId = Convert.ToInt32(Id);

            string GetAutn = "select * from d__Autn_2 where Head_ID = " + CurrentId;
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

            if (checkEdit17.Checked) { Avail = textEdit18.Text.Trim(); } else { Avail = ""; } //Пригодность 
            if (checkEdit3.Checked) { Klapan = textEdit12.Text.Trim(); } else { Klapan = ""; } //3 кл 
            if (checkEdit12.Checked) { DemSkob = textEdit13.Text.Trim(); } else { DemSkob = ""; } //дем скобы
            if (checkEdit13.Checked) { PTC = textEdit14.Text.Trim(); } else { PTC = ""; } //трафар ПТС
            if (checkEdit14.Checked) { Ushki = textEdit24.Text.Trim(); } else { Ushki = ""; } //Ушки 
            if (checkEdit15.Checked) { Skobi = textEdit15.Text.Trim(); } else { Skobi = ""; } //Скобы 
            if (checkEdit22.Checked) { Shaiba = textEdit16.Text.Trim(); } else { Shaiba = ""; } //Шайба и валик 
            if (checkEdit21.Checked) { Lest = textEdit23.Text.Trim(); } else { Lest = ""; } //ЛЕстница 
            if (checkEdit20.Checked) { Greben = textEdit22.Text.Trim(); } else { Greben = ""; } //Гребнь 
            if (checkEdit19.Checked) { Barash = textEdit21.Text.Trim(); } else { Barash = ""; } //ВЦ бараш тип
            if (checkEdit18.Checked) { TriBolt = textEdit20.Text.Trim(); } else { TriBolt = ""; } //3 болта
            if (checkEdit16.Checked) { Exp = textEdit19.Text.Trim(); } else { Exp = ""; } //годный на эксп
            if (checkEdit23.Checked) { Note = textEdit17.Text.Trim(); } else { Note = ""; } //Причина негод

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
                string UpdateAutnAll = "exec dbo.UpdateAutnAll '" + Avail + "','" + Klapan + "','" + DemSkob + "','" + PTC + "','" + Ushki + "','" + Skobi + "','" + Shaiba + "','" + Lest + "','" + Greben + "','" + Barash + "','" + TriBolt + "','" + Exp + "','" + Note + "','" + Arrays + "'";
                DbConnection.DBConnect(UpdateAutnAll);
            }
            RefreshBody();
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
                    RefreshBody();
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
        private void checkEdit3_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit12.Enabled = (checkEdit3.CheckState == CheckState.Checked);
        }

        private void checkEdit17_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit18.Enabled = (checkEdit17.CheckState == CheckState.Checked);
        }

        private void textEdit18_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit12_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit13.Enabled = (checkEdit12.CheckState == CheckState.Checked);
        }

        private void textEdit13_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit13_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit14.Enabled = (checkEdit13.CheckState == CheckState.Checked);
        }

        private void textEdit14_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit14_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit24.Enabled = (checkEdit14.CheckState == CheckState.Checked);
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

        private void checkEdit15_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit15.Enabled = (checkEdit15.CheckState == CheckState.Checked);
        }

        private void textEdit16_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit22_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit16.Enabled = (checkEdit22.CheckState == CheckState.Checked);
        }

        private void textEdit23_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit21_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit23.Enabled = (checkEdit21.CheckState == CheckState.Checked);
        }

        private void textEdit22_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit20_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit22.Enabled = (checkEdit20.CheckState == CheckState.Checked);
        }

        private void textEdit21_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit19_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit21.Enabled = (checkEdit19.CheckState == CheckState.Checked);
        }

        private void textEdit20_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit18_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit20.Enabled = (checkEdit18.CheckState == CheckState.Checked);
        }

        private void textEdit19_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }

        private void checkEdit16_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit19.Enabled = (checkEdit16.CheckState == CheckState.Checked);
        }

        private void checkEdit23_Properties_CheckStateChanged(object sender, EventArgs e)
        {
            textEdit17.Enabled = (checkEdit23.CheckState == CheckState.Checked);
        }

        private void textEdit12_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D0, Keys.D1 };

            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
    }
}
