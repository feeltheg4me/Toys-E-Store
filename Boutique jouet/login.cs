using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boutique_jouet
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }




        private void login_Load(object sender, EventArgs e)
        {
            txtlogin.Focus();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Home().Show();
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {
            comboBox1.Text="Select Role";
            txtlogin.Clear();
            txtpassword.Clear();
            txtlogin.Focus();
            


         
             

        }
        SqlConnection conn;
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if((txtpassword.Text=="")||(txtlogin.Text==""))
            {
                MessageBox.Show("Enter login and password");
            }
            else
            {


                if (comboBox1.SelectedIndex == 1)
                {
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        
                        string query = "SELECT * FROM caissier WHERE login=@login AND password=@pass ;";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@login", txtlogin.Text);
                        cmd.Parameters.AddWithValue("@pass", txtpassword.Text);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {

                                Program.SellerNameglobalString = txtlogin.Text;
                                this.Hide();
                                new sellerform().Show();
                                
                            }
                            else
                            {
                                MessageBox.Show("Wrong info for Seller");
                            }
                        }
                    }
                   
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    if ((txtlogin.Text == "admin")&&(txtpassword.Text== "admin"))
                    { 
                this.Hide();
                new admin().Show();
                    }
                    else
                    {
                        MessageBox.Show("Wrong login info for Admin");
                    }
                }
                else 
                {
                    MessageBox.Show("Seelect a Role");
                }
            

            }
        }

        private void bunifuPictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
