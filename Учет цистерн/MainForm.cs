﻿using System;
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
        public MainForm(string FIO)
        {
            InitializeComponent();
            this.Text = "Учет вагонов-цистерн. Батыс Петролеум ТОО - "+FIO;
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
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
                string UpdateAuditUser = "UPDATE AUDIT_USER SET DATE_OUT = GETDATE(), IS_DEAD = 1 WHERE ID_SESSION = @@spid and (IS_DEAD IS NULL OR DATE_OUT IS NULL)";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(UpdateAuditUser);
                Environment.Exit(0);
            }
            else
            {
                //
            }
        }

        private void ToolStripMenuItem1_Product_Click(object sender, EventArgs e)
        {
            Form_Product frm = new Form_Product();
            tabControl1.Show();
            TabPage ProductTabPage = new TabPage("Продукты");
            tabControl1.TabPages.Add(ProductTabPage);
            tabControl1.SelectedTab = ProductTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            ProductTabPage.Controls.Add(frm);
        }

        private void toolStripMenuItem2_Station_Click(object sender, EventArgs e)
        {
            StationForm frm = new StationForm();
            tabControl1.Show();
            TabPage StationTabPage = new TabPage("Станции");
            tabControl1.TabPages.Add(StationTabPage);
            tabControl1.SelectedTab = StationTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            StationTabPage.Controls.Add(frm);
        }

        private void ToolStripMenuItem3_Brigade_Click(object sender, EventArgs e)
        {
            BrigadeForm frm = new BrigadeForm();
            tabControl1.Show();
            TabPage StationTabPage = new TabPage("Бригады");
            tabControl1.TabPages.Add(StationTabPage);
            tabControl1.SelectedTab = StationTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            StationTabPage.Controls.Add(frm);
        }
    }
}
