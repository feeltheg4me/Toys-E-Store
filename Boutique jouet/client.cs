using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boutique_jouet
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

     
        private void Home_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'tOYSclientDataSet7.jouet'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.jouetTableAdapter.Fill(this.tOYSclientDataSet7.jouet);

        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btntoys_Click(object sender, EventArgs e)
        {

            Toys.SetPage(((Control)sender).Text);
        }

       

        private void bunifuPictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            new login().Show();
        }

       
        SqlConnection conn = new SqlConnection();
      
       
        private void addEmail_Click(object sender, EventArgs e)
        {
            
           
            try
            {
                var eMailValidator = new System.Net.Mail.MailAddress(txtemail.Text);
            }
            catch (FormatException ex)
            {
                
                MessageBox.Show(ex.Message);
                txtemail.Text = "";
                // wrong e-mail address
                return;
            }
            
            if (txtemail.Text == "")
            {
                MessageBox.Show("Enter Email");
            }
            else
            {


                try
                {

                    //connected mode
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        string query = "INSERT INTO [client](email) VALUES(@email)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@email", txtemail.Text);
                        cmd.ExecuteNonQuery();//For Update , Insert or Delete
                        MessageBox.Show("email added successfully!");
                        Program.list.Add(txtemail.Text);

                    }
                }
                catch (SqlException ex)
                {
                   

                    if (ex.Number== 2601 || ex.Number == 2627)
                    {
                        MessageBox.Show("Email already added");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                       
                    }
                }

            }
        }
    }
}
