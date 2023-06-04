using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Employee_Mgt_Systeam
{
    public partial class frm_View_Employee_List : Form
    {
        public frm_View_Employee_List()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-T71U96F;Initial Catalog=dbo.Employee_Details;Integrated Security=True");

        private void btn_Add_New_Employee_Click(object sender, EventArgs e)
        {
            frm_Add_New_Employee obj = new frm_Add_New_Employee();
            obj.Show();
            this.Hide();
        }
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Frm_Login obj = new Frm_Login();
            obj.Show();
            this.Hide();
        }

        private void frm_View_Employee_List_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_dbo_Employee_DetailsDataSet1.Employee_Details' table. You can move, or remove it, as needed.
            this.employee_DetailsTableAdapter.Fill(this._dbo_Employee_DetailsDataSet1.Employee_Details);
            lbl_Uname.Text = Common_Content.Log_Uname;
        }
    }
}
