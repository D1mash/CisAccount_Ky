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
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                string UpdateAuditUser = "UPDATE AUDIT_USER SET DATE_OUT = GETDATE(), IS_DEAD = 1 WHERE ID_SESSION = @@spid and (IS_DEAD IS NULL OR DATE_OUT IS NULL)";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(UpdateAuditUser);
                Application.Exit();
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
            TabPage BrigadeTabPage = new TabPage("Бригады");
            tabControl1.TabPages.Add(BrigadeTabPage);
            tabControl1.SelectedTab = BrigadeTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            BrigadeTabPage.Controls.Add(frm);
        }

        private void toolStripMenuItem4_Owner_Click(object sender, EventArgs e)
        {
            OwnerForm frm = new OwnerForm();
            tabControl1.Show();
            TabPage OwnerTabPage = new TabPage("Собственники");
            tabControl1.TabPages.Add(OwnerTabPage);
            tabControl1.SelectedTab = OwnerTabPage;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            OwnerTabPage.Controls.Add(frm);
        }

        private void ToolStripMenuItem_Carriage_Click(object sender, EventArgs e)
        {
            CarriageForm carriageForm = new CarriageForm();
            tabControl1.Show();
            TabPage CarriageTabPage = new TabPage("Вагоны");
            tabControl1.TabPages.Add(CarriageTabPage);
            tabControl1.SelectedTab = CarriageTabPage;
            carriageForm.TopLevel = false;
            carriageForm.Visible = true;
            carriageForm.FormBorderStyle = FormBorderStyle.None;
            carriageForm.Dock = DockStyle.Fill;
            CarriageTabPage.Controls.Add(carriageForm);
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceCostForm ServiceCostForm = new ServiceCostForm();
            tabControl1.Show();
            TabPage CarriageTabPage = new TabPage("Расценки");
            tabControl1.TabPages.Add(CarriageTabPage);
            tabControl1.SelectedTab = CarriageTabPage;
            ServiceCostForm.TopLevel = false;
            ServiceCostForm.Visible = true;
            ServiceCostForm.FormBorderStyle = FormBorderStyle.None;
            ServiceCostForm.Dock = DockStyle.Fill;
            CarriageTabPage.Controls.Add(ServiceCostForm);
        }

        private void toolStripMenuItem_Service_Click(object sender, EventArgs e)
        {
            ServiceForm ServiceForm = new ServiceForm();
            tabControl1.Show();
            TabPage CarriageTabPage = new TabPage("Услуги");
            tabControl1.TabPages.Add(CarriageTabPage);
            tabControl1.SelectedTab = CarriageTabPage;
            ServiceForm.TopLevel = false;
            ServiceForm.Visible = true;
            ServiceForm.FormBorderStyle = FormBorderStyle.None;
            ServiceForm.Dock = DockStyle.Fill;
            CarriageTabPage.Controls.Add(ServiceForm);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RenderedServiceForm RenderedServiceForm = new RenderedServiceForm();
            tabControl1.Show();
            TabPage CarriageTabPage = new TabPage("Обработанные вагоны");
            tabControl1.TabPages.Add(CarriageTabPage);
            tabControl1.SelectedTab = CarriageTabPage;
            RenderedServiceForm.TopLevel = false;
            RenderedServiceForm.Visible = true;
            RenderedServiceForm.FormBorderStyle = FormBorderStyle.None;
            RenderedServiceForm.Dock = DockStyle.Fill;
            CarriageTabPage.Controls.Add(RenderedServiceForm);
        }
    }
}
