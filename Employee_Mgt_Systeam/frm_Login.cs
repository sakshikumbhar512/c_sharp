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
    public partial class Frm_Login : Form
    {
        public Frm_Login()
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
        void Clear_Controls()
        {
            
        }

        private void Frm_Login_Load_1(object sender, EventArgs e)
        {
            lbl_Note.Text = "Enter Valid Username && Password";
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            Con_Open();
            int Cnt = 0;
            SqlCommand Cmd = new SqlCommand();

            Cmd.Connection = Con;
            Cmd.CommandText = "Select Count(*)From Login_Details Where Username= @Unm And Password=@Pwd";

            Cmd.Parameters.Add("Unm", SqlDbType.NVarChar).Value = tb_UserName.Text;
            Cmd.Parameters.Add("Pwd", SqlDbType.NVarChar).Value = tb_Password.Text;

            Cnt = Convert.ToInt32(Cmd.ExecuteScalar());

            if (Cnt > 0)
            {
                MessageBox.Show("Login Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Common_Content.Log_Uname = tb_UserName.Text;

                frm_Add_New_Employee obj = new frm_Add_New_Employee();
                obj.Show();
                this.Hide();
            }
            else 
            {
                lbl_Note.Text = "Incorrect Username Or Password !!!...";
                lbl_Note.ForeColor = Color.Red;
            }

            tb_UserName.Clear();
            tb_Password.Clear();
            Con_Close();
        }

        
    }
}
