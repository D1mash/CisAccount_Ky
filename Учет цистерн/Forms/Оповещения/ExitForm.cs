using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Оповещения
{
    public partial class ExitForm : Form
    {
        public ExitForm()
        {
            InitializeComponent();
            this.ShowIcon = false;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UpdateAuditUser = "UPDATE AUDIT_USER SET DATE_OUT = GETDATE(), IS_DEAD = 1 WHERE ID_SESSION = @@spid and (IS_DEAD IS NULL OR DATE_OUT IS NULL)";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(UpdateAuditUser);
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
