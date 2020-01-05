using System.Data;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.заявки_на_обработку
{
    public partial class LastRenderedServiceForm : Form
    {
        public LastRenderedServiceForm(DataTable dt)
        {
            InitializeComponent();
            DataTable dTable = dt;
            gridControl1.DataSource = dTable;
            gridView1.Columns[0].Visible = false;
        }
    }
}
