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
    public partial class frm_Add_New_Employee : Form
    {
        public frm_Add_New_Employee()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-T71U96F;Initial Catalog=dbo.Employee_Details;Integrated Security=True");
        void Con_Open()
        {
            if (Con.State != ConnectionState.Open)
            {
                Con.Open();
            }
        }
        void Con_Close()
        {
            if (Con.State != ConnectionState.Closed)
            {
                Con.Close();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Con_Open();

            if (tb_Employee_ID.Text != "" && tb_Name.Text != "" && tb_Mobile_No.Text != "")
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Con;
                Cmd.CommandText = "Insert into Employee_Details values(@EmpID ,@Name,@MobNo,@Dob,@Des)";

                Cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = tb_Employee_ID.Text;
                Cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = tb_Name.Text;
                Cmd.Parameters.Add("@MobNo", SqlDbType.Decimal).Value = tb_Mobile_No.Text;
                Cmd.Parameters.Add("@DOb", SqlDbType.Date).Value = dtp_DOB.Text;
                Cmd.Parameters.Add("@Des", SqlDbType.NVarChar).Value = cmb_Designation.Text;
                Cmd.ExecuteNonQuery();

                MessageBox.Show("Record Saved");

                Clear_Controls();
            }

            else
            {
                MessageBox.Show("Fill All Fields");
            }
            Con_Close();
        }


        private void Only_Text(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (Char)Keys.Space)))
            {
                e.Handled = true;
            }
        }

        private void Only_Numeric(object sender, KeyPressEventArgs e)
        {

            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                e.Handled = true;
            }

        }
        void Clear_Controls()
        {
            tb_Employee_ID.Text = Convert.ToString(Auto_Incr());
            tb_Name.Clear();
            tb_Mobile_No.Clear();
            dtp_DOB.Text = "31-12-2009";
            cmb_Designation.SelectedIndex = -1;

            tb_Employee_ID.Focus();
        }

        int Auto_Incr()
        {
            int Cnt = 0;
            Con_Open();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "Select Count(*)From Employee_Details";

            Cnt = Convert.ToInt32(Cmd.ExecuteScalar());
            Cmd.Dispose();

            if (Cnt > 0)
            {
                Cmd.Connection = Con;
                Cmd.CommandText = "Select Max (Emp_ID)from Employee_Details";

                Cnt = Convert.ToInt32(Cmd.ExecuteScalar());
                Cnt = Cnt + 1;
            }
            else
            {
                Cnt = 101;
            }
            tb_Employee_ID.Text = Convert.ToString(Cnt);

            Con_Close();
            return Cnt;

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Clear_Controls();
        }

        private void btn_Employee_List_Click(object sender, EventArgs e)
        {
            frm_View_Employee_List obj = new frm_View_Employee_List();
            obj.Show();
            this.Hide();
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Frm_Login obj = new Frm_Login();
            obj.Show();
            this.Hide();
        }

        private void frm_Add_New_Employee_Load(object sender, EventArgs e)
        {
            lbl_Uname.Text = Common_Content.Log_Uname;
            tb_Employee_ID.Focus();
            Auto_Incr();
        }
    }
}