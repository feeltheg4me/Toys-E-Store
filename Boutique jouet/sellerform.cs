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
    public partial class sellerform : Form
    {
        public sellerform()
        {
            InitializeComponent();
        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void fillDGV()
        {
            
                SqlConnection con;
                //connected mode
                using (con = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    con.Open();
                    string query = "SELECT * FROM bill where SellerLogin='"+ Program.SellerNameglobalString+ "'";
                    SqlCommand cmd = new SqlCommand(query, con);
               
                SqlDataAdapter sda = new SqlDataAdapter(query,con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                billdgv.DataSource = ds.Tables[0];
                }
            }
        private void caissier_Load(object sender, EventArgs e)
        {
            
            fillDGV();


            lblSellerlogin.Text = Program.SellerNameglobalString;
            // TODO: cette ligne de code charge les données dans la table 'boutiqueDBDataSet5.bill'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.billTableAdapter.Fill(this.boutiqueDBDataSet5.bill);
            // TODO: cette ligne de code charge les données dans la table 'boutiqueDBDataSet4.jouet'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.jouetTableAdapter1.Fill(this.boutiqueDBDataSet4.jouet);
            // TODO: cette ligne de code charge les données dans la table 'boutiqueDBDataSet3.jouet'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.jouetTableAdapter.Fill(this.boutiqueDBDataSet3.jouet);
            lbldate.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
        int flag = 0;
        private void btnaddSales_Click(object sender, EventArgs e)
        {
            caissierPages.SetPage(((Control)sender).Text);

        }

        private void bunifuPictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            new login().Show();
        }

     
        float grdtot = 0;
        int n = 0;
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if ((txttoyB.Text == "") || (txtpriceB.Text == "") || (txtquantityB.Text == ""))
            {
                MessageBox.Show("Missing data");
            }
            else
            {


                
                float tot = float.Parse(txtpriceB.Text) * int.Parse(txtquantityB.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(orderdgv);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = txttoyB.Text;
                newRow.Cells[2].Value = double.Parse(txtpriceB.Text);
                newRow.Cells[3].Value = int.Parse(txtquantityB.Text);
                newRow.Cells[4].Value = tot;
                orderdgv.Rows.Add(newRow);
                n++;
                grdtot += tot;
                lblRs.Text = grdtot.ToString();
            }
        }

      

        private void toysdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txttoyB.Text = toysdgv.SelectedRows[0].Cells[0].Value.ToString();
            txtpriceB.Text = toysdgv.SelectedRows[0].Cells[1].Value.ToString();
        }
        SqlConnection conn = new SqlConnection();
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
           


                try
                {

                    //connected mode
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        string query = "INSERT INTO [bill](SellerLogin,billDate,totAmount) VALUES(@SellerLogin,@billDate,@totAmount)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                      
                        cmd.Parameters.AddWithValue("@SellerLogin", lblSellerlogin.Text);
                        cmd.Parameters.AddWithValue("@billDate", lbldate.Text);
                        cmd.Parameters.AddWithValue("@totAmount", double.Parse(lblRs.Text));




                        cmd.ExecuteNonQuery();//For Update , Insert or Delete


                    billdgv.Update();
                    billdgv.Refresh();
                   
                    this.billTableAdapter.Fill(this.boutiqueDBDataSet5.bill);
                    MessageBox.Show("Bill added successfully!");
                    
                    
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

       

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
           if(printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Toys e-Shop", new Font("Century Gothic", 25, FontStyle.Bold),Brushes.Red, new Point(260));
            e.Graphics.DrawString("Bill id : "+billdgv.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Blue, new Point(100,70));
            e.Graphics.DrawString("Seller Login : "+billdgv.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Blue, new Point(100,100));
            e.Graphics.DrawString("Date : "+billdgv.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Blue, new Point(100,130));
            e.Graphics.DrawString("Amount : "+billdgv.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Blue, new Point(100,160));
            e.Graphics.DrawString("codeSpace", new Font("Century Gothic", 25, FontStyle.Italic), Brushes.Red, new Point(270,260)) ;
        }

        private void billdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            flag = 1;
        }

        private void lbldate_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
