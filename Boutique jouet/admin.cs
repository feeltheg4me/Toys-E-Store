using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boutique_jouet
{
    public partial class admin : Form
    {
        internal static DataClasses1DataContext BoutiqueDB = new DataClasses1DataContext();
        public admin()
        {
            InitializeComponent();
        }

        private void bunifuPictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            new login().Show();
        }


        Random random = new Random();

        private void secondQuater(Random r)
        {
            var canvas = new Bunifu.Dataviz.WinForms.BunifuDatavizAdvanced.Canvas();
            var datapoint = new Bunifu.Dataviz.WinForms.BunifuDatavizAdvanced.DataPoint(Bunifu.Dataviz.WinForms.BunifuDatavizAdvanced._type.Bunifu_stepArea);
         
            datapoint.addLabely("Sales Revenue",r.Next(0,1000).ToString());
            
            canvas.addData(datapoint);
            bunifuDatavizAdvanced1.colorSet.Add(Color.DarkSeaGreen);
            bunifuDatavizAdvanced1.Render(canvas);
        }
            private void btntoys_Click(object sender, EventArgs e)
        {
            lblNsellers.Text = sellersN().ToString();
            lblsalesN.Text = salesN().ToString();
            lblsuppliersN.Text = suppliersN().ToString();
            lblSalesRevenues.Text = SalesRevenue();

            lblbestseller.Text = BestSeller();
            lblToys.Text = ToysN();
            toys.SetPage(((Control)sender).Text);

            secondQuater(random);
        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void adminPages_Click(object sender, EventArgs e)
        {
            SuppliercomboBox1.Text = "Select Supplier ID";
            txtnameJ.Focus();
        }

      
        SqlConnection conn;
        
        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            
           
            
            if (txtadresse.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtemail.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtphone.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtname.Text == "")
                MessageBox.Show("Missing data!");
            else if (checkedListBox1.SelectedIndex==-1)
                MessageBox.Show("Missing data!");
            

            else
            { 

            try
            {
                String type = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        type += " " + checkedListBox1.Items[i].ToString();

                    }

                }





                //connected mode
                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "INSERT INTO [fournisseur](nom,email,adresse,typeJouet,tels) VALUES(@nom,@email,@adresse,@typeJouet,@tels)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nom", txtname.Text);
                    cmd.Parameters.AddWithValue("@email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@adresse", txtadresse.Text);
                    cmd.Parameters.AddWithValue("@typeJouet", type);
                    cmd.Parameters.AddWithValue("@tels", txtphone.Text);



                    cmd.ExecuteNonQuery();//For Update , Insert or Delete

                    SuplliersDataGridView.DataSource = null;
                    SuplliersDataGridView.DataSource = admin.BoutiqueDB.fournisseur;
                        this.fournisseurTableAdapter.Fill(this.boutiqueDBDataSet.fournisseur);
                        SuplliersDataGridView.Refresh();
                    SuplliersDataGridView.Update();
                    MessageBox.Show("Supplier added successfully!");
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }

     
        private int sellersN()
        {
            try
            {



                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM caissier";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int count = (int)cmd.ExecuteScalar();

                    return count;
                }


            }
            catch (Exception)
            {

                throw;
            }

        }
        private int salesN()
        {
            try
            {



                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM bill";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int count = (int)cmd.ExecuteScalar();

                    return count;
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        private String ToysN()
        {
            try
            {



                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM jouet " ;
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int count = (int)cmd.ExecuteScalar();

                    return count.ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "Error";

            }

        }
        private String BestSeller()
        {
            try
            {



                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "SELECT SellerLogin FROM bill where totAmount=(SELECT MAX(totAmount) FROM bill)";

                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    String count = (String)cmd.ExecuteScalar();

                    return count;
                }

    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "Error";

            }

        }
            private String SalesRevenue()
        {
            try
            {



                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "SELECT SUM(totAmount)FROM bill";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    double count = (double)cmd.ExecuteScalar();

                    return count.ToString();
                }


            }
            catch (Exception ex)
            {
               
                return "Error";
               
            }

        }
     

            private int suppliersN()
        {
            try
            {



                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM fournisseur";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int count = (int)cmd.ExecuteScalar();

                    return count;
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

      

        private void admin_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'boutiqueDBDataSet6.bill'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.billTableAdapter.Fill(this.boutiqueDBDataSet6.bill);

            // TODO: cette ligne de code charge les données dans la table 'boutiqueDBDataSet2.jouet'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.jouetTableAdapter.Fill(this.boutiqueDBDataSet2.jouet);
            // TODO: cette ligne de code charge les données dans la table 'boutiqueDBDataSet1.caissier'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.caissierTableAdapter.Fill(this.boutiqueDBDataSet1.caissier);
            // TODO: cette ligne de code charge les données dans la table 'boutiqueDBDataSet.fournisseur'. Vous pouvez la déplacer ou la supprimer selon les besoins.

            SuplliersDataGridView.Refresh();
            // TODO: cette ligne de code charge les données dans la table 'worldcupDBDataSet.Robot'. Vous pouvez la déplacer ou la supprimer selon les besoins.


            SuplliersDataGridView.DataSource = admin.BoutiqueDB.fournisseur;




           






        }



        private void fillCategorycombo()
        {

            String id = SuppliercomboBox1.Text.ToString();

            using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
            {
                conn.Open();
                string query = "SELECT typeJouet FROM fournisseur where id='"+int.Parse(id)+"'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();



                DataTable dt = new DataTable();
                dt.Columns.Add("typeJouet", typeof(String));
                dt.Load(rdr);

                foreach (DataRow row in dt.Rows)
                {
                    if(row["typeJouet"].ToString().Contains("Cars"))
                    {
                        categorycomboBox2.Items.Add("Cars");
                    }
                    if (row["typeJouet"].ToString().Contains("Animals"))
                    {
                        categorycomboBox2.Items.Add("Animals");
                    }
                    if (row["typeJouet"].ToString().Contains("Electronic"))
                    {
                        categorycomboBox2.Items.Add("Electronic");
                    }
                    if (row["typeJouet"].ToString().Contains("Dolls"))
                    {
                        categorycomboBox2.Items.Add("Dolls");
                    }
                }


              
            }
        }


        private void fillSupplierIdcombo()
        {
            using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
            {
                conn.Open();
                string query = "SELECT id FROM fournisseur";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Load(rdr);
                SuppliercomboBox1.ValueMember = "id";
                SuppliercomboBox1.DataSource = dt;
            }
        }
        private void SuplliersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtname.Text = SuplliersDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtemail.Text = SuplliersDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtphone.Text = SuplliersDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtadresse.Text = SuplliersDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtSupplierId.Text = SuplliersDataGridView.SelectedRows[0].Cells[0].Value.ToString();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            if (SuplliersDataGridView.SelectedRows[0].Cells[4].Value.ToString().Contains("Cars"))
            {
                checkedListBox1.SetItemChecked(0, true);
            }
            if (SuplliersDataGridView.SelectedRows[0].Cells[4].Value.ToString().Contains("Animals"))
            {
                checkedListBox1.SetItemChecked(1, true);
            }
            if (SuplliersDataGridView.SelectedRows[0].Cells[4].Value.ToString().Contains("Electronic"))
            {
                checkedListBox1.SetItemChecked(3, true);
            }
            if (SuplliersDataGridView.SelectedRows[0].Cells[4].Value.ToString().Contains("Dolls"))
            {
                checkedListBox1.SetItemChecked(2, true);
            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            if (txtSupplierId.Text == "")
            {
                MessageBox.Show("Enter Supplier ID");
            }
            else
            {
                try
                {
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        string query = "DELETE FROM fournisseur WHERE id=" + int.Parse(txtSupplierId.Text) + "";
                        SqlCommand cmd = new SqlCommand(query, conn);




                        cmd.ExecuteNonQuery();//For Update , Insert or Delete


                        this.fournisseurTableAdapter.Fill(this.boutiqueDBDataSet.fournisseur);
                        SuplliersDataGridView.Refresh();
                        SuplliersDataGridView.Update();
                        MessageBox.Show("Supplier deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            SuplliersDataGridView.DataSource = null;
            SuplliersDataGridView.DataSource = admin.BoutiqueDB.fournisseur;
            SuplliersDataGridView.Refresh();
            SuplliersDataGridView.Update();
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            try
            {
                String type = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        type += " " + checkedListBox1.Items[i].ToString();

                    }

                }

                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "UPDATE fournisseur set nom='" + txtSupplierId.Text + "',email='" + txtemail.Text + "',adresse='" + txtadresse.Text + "',tels='" + txtphone.Text + "',typeJouet='" + type + "' where nom='" + txtname.Text + "' ";
                    SqlCommand cmd = new SqlCommand(query, conn);




                    cmd.ExecuteNonQuery();//For Update , Insert or Delete


                    this.fournisseurTableAdapter.Fill(this.boutiqueDBDataSet.fournisseur);
                    SuplliersDataGridView.Refresh();
                    SuplliersDataGridView.Update();
                    MessageBox.Show("Supplier edited successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuButton13_Click_1(object sender, EventArgs e)
        {
            if (txtloginC.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtnomC.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtnssC.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtpasswordC.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtphoneC.Text == "")
                MessageBox.Show("Missing data!");
            else if (txtprenomC.Text == "")
                MessageBox.Show("Missing data!");

            else
            {


                try
                {

                    //connected mode
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        string query = "INSERT INTO [caissier](login,password,photo,nom,prenom,tel,nss) VALUES(@login,@password,@photo,@nom,@prenom,@tel,@nss)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@login", txtloginC.Text);
                        cmd.Parameters.AddWithValue("@password", txtpasswordC.Text);
                        cmd.Parameters.AddWithValue("@nom", txtnomC.Text);
                        cmd.Parameters.AddWithValue("@prenom", txtprenomC.Text);
                        cmd.Parameters.AddWithValue("@tel", txtphoneC.Text);
                        cmd.Parameters.AddWithValue("@nss", txtnssC.Text);
                        cmd.Parameters.AddWithValue("@photo", SavePhotoS());



                        cmd.ExecuteNonQuery();//For Update , Insert or Delete


                        this.caissierTableAdapter.Fill(this.boutiqueDBDataSet1.caissier);
                        SellersDatagridview.Refresh();
                        SellersDatagridview.Update();
                        MessageBox.Show("Seller added successfully!");
                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtnssC_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtnssC.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtnssC.Text = txtnssC.Text.Remove(txtnssC.Text.Length - 1);
            }
        }

        private void txtphoneC_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtphoneC.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtphoneC.Text = txtphoneC.Text.Remove(txtphoneC.Text.Length - 1);
            }

        }

        private void txtphone_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(txtphone.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtphone.Text = txtphone.Text.Remove(txtphone.Text.Length - 1);
            }
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            

            try
            {


                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "UPDATE caissier set login='" + txtloginC.Text + "',password='" + txtpasswordC.Text + "',photo=@photo,nom='" + txtnomC.Text + "',prenom='" + txtprenomC.Text + "',tel='" + txtphoneC.Text + "',nss='" + txtnssC.Text + "' where login='" + txtloginC.Text + "' ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@photo", SavePhotoS());



                    cmd.ExecuteNonQuery();//For Update , Insert or Delete

                    this.caissierTableAdapter.Fill(this.boutiqueDBDataSet1.caissier);

                    SellersDatagridview.Refresh();
                    SellersDatagridview.Update();
                    MessageBox.Show("Seller edited successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuDataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtpasswordC.Text = SellersDatagridview.SelectedRows[0].Cells[1].Value.ToString();
            photoS.ImageLocation = SellersDatagridview.SelectedRows[0].Cells[2].Value.ToString();
            txtloginC.Text = SellersDatagridview.SelectedRows[0].Cells[0].Value.ToString();
            txtnomC.Text = SellersDatagridview.SelectedRows[0].Cells[3].Value.ToString();
            txtprenomC.Text = SellersDatagridview.SelectedRows[0].Cells[4].Value.ToString();
            txtphoneC.Text = SellersDatagridview.SelectedRows[0].Cells[5].Value.ToString();
            txtnssC.Text = SellersDatagridview.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            this.caissierTableAdapter.Fill(this.boutiqueDBDataSet1.caissier);
            SellersDatagridview.Update();
            SellersDatagridview.Refresh();
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            if (txtloginC.Text == "")
            {
                MessageBox.Show("Enter Seller login");
            }
            else
            {
                try
                {
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        string query = "DELETE FROM caissier WHERE login='" + txtloginC.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);




                        cmd.ExecuteNonQuery();//For Update , Insert or Delete


                        this.caissierTableAdapter.Fill(this.boutiqueDBDataSet1.caissier);
                        SellersDatagridview.Refresh();
                        SellersDatagridview.Update();
                        MessageBox.Show("Supplier deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (txtnameJ.Text == "")
                MessageBox.Show("Missing data!");
            else if(txtpriceJ.Text=="")
                MessageBox.Show("Missing data!");
            else if (TxtStockJ.Text == "")
                MessageBox.Show("Missing data!");
            else if (SuppliercomboBox1.Text == "Select Supplier ID")
                MessageBox.Show("Missing data!");
            else if (categorycomboBox2.Text == "Select Category")
                MessageBox.Show("Missing data!");
            else if (ciblecomboBox3.Text == "Select Age target")
                MessageBox.Show("Missing data!");
            else { 
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("toyseshop420@gmail.com", "TOYSeshopAdmin420"),
                    EnableSsl = true,
                };
                foreach(var email in Program.list)
                {
                   
                    smtpClient.Send("toyseshop420@gmail.com", email, "New Toy is HERE! ", "We added a new toy if you are intrested you should have a look :)." +
                        "Have a nice day.");
                }
               
                MessageBox.Show("email sent to all clients !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            try
            {

                //connected mode
                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "INSERT INTO [jouet](nom,type,cible,photo,prix,idFournisseur,stock) VALUES(@nom,@type,@cible,@photo,@prix,@idFournisseur,@stock)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nom", txtnameJ.Text);
                    cmd.Parameters.AddWithValue("@type", categorycomboBox2.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@cible", ciblecomboBox3.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@prix", double.Parse(txtpriceJ.Text));
                    cmd.Parameters.AddWithValue("@idFournisseur", Convert.ToInt32(SuppliercomboBox1.Text));
                    cmd.Parameters.AddWithValue("@stock", int.Parse(TxtStockJ.Text));
                    cmd.Parameters.AddWithValue("@photo", SavePhotoT());



                    cmd.ExecuteNonQuery();//For Update , Insert or Delete


                    this.jouetTableAdapter.Fill(this.boutiqueDBDataSet2.jouet);
                    toysdatagridview.Refresh();
                    toysdatagridview.Update();
                    MessageBox.Show("Toy added successfully!");


                    




                }

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }

        private void SuppliercomboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            fillSupplierIdcombo();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (txttoyid.Text == "")
            {
                MessageBox.Show("Enter Toy ID");
            }
            else
            {
                try
                {
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        string query = "DELETE FROM jouet WHERE id=" + int.Parse(txttoyid.Text) + "";
                        SqlCommand cmd = new SqlCommand(query, conn);




                        cmd.ExecuteNonQuery();//For Update , Insert or Delete


                        this.jouetTableAdapter.Fill(this.boutiqueDBDataSet2.jouet);
                        toysdatagridview.Refresh();
                        toysdatagridview.Update();
                        MessageBox.Show("Toy deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void toysdatagridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtnameJ.Text = toysdatagridview.SelectedRows[0].Cells[1].Value.ToString();
            txttoyid.Text = toysdatagridview.SelectedRows[0].Cells[0].Value.ToString();
            TxtStockJ.Text = toysdatagridview.SelectedRows[0].Cells[7].Value.ToString();
            categorycomboBox2.Text = toysdatagridview.SelectedRows[0].Cells[2].Value.ToString();
            ciblecomboBox3.Text = toysdatagridview.SelectedRows[0].Cells[3].Value.ToString();
            photoT.ImageLocation = toysdatagridview.SelectedRows[0].Cells[4].Value.ToString();
            txtpriceJ.Text = toysdatagridview.SelectedRows[0].Cells[5].Value.ToString();
            SuppliercomboBox1.Text = toysdatagridview.SelectedRows[0].Cells[6].Value.ToString();
        }
                

        private byte[] SavePhotoT()
        {
            MemoryStream ms = new MemoryStream();
            photoT.Image.Save(ms, photoT.Image.RawFormat);
            return ms.GetBuffer();
        }
        private byte[] SavePhotoS()
        {
            MemoryStream ms = new MemoryStream();
            photoS.Image.Save(ms, photoS.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {


                using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                {
                    conn.Open();
                    string query = "UPDATE jouet set nom='" + txtnameJ.Text + "',type='" + categorycomboBox2.SelectedItem.ToString() + "',cible='" + ciblecomboBox3.SelectedItem.ToString() + "',photo=@photo,prix='" + double.Parse(txtpriceJ.Text) + "',idFournisseur='" + Convert.ToInt32(SuppliercomboBox1.Text) + "',stock='" + int.Parse(TxtStockJ.Text) + "' where id='" + int.Parse(txttoyid.Text) + "' ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@photo", SavePhotoT());



                    cmd.ExecuteNonQuery();//For Update , Insert or Delete

                    this.jouetTableAdapter.Fill(this.boutiqueDBDataSet2.jouet);

                    SellersDatagridview.Refresh();
                    SellersDatagridview.Update();
                    MessageBox.Show("Toy edited successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        String imgL = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgL = dialog.FileName.ToString();
                photoT.ImageLocation = imgL;
            }

        }
        String imgSL = "";
        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgSL = dialog.FileName.ToString();
                photoS.ImageLocation = imgSL;
            }
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            billid.Text = BillsDGV.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void bunifuButton15_Click(object sender, EventArgs e)
        {
            if (billid.Text == "Bill ID")
            {
                MessageBox.Show("Select Bill from Table");
            }
            else
            {
                try
                {
                    using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
                    {
                        conn.Open();
                        string query = "DELETE FROM bill WHERE billId=" + int.Parse(billid.Text) + "";
                        SqlCommand cmd = new SqlCommand(query, conn);




                        cmd.ExecuteNonQuery();//For Update , Insert or Delete


                        this.billTableAdapter.Fill(this.boutiqueDBDataSet6.bill);
                        BillsDGV.Refresh();
                        BillsDGV.Update();
                        MessageBox.Show("Bill deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
        private void fillcategoryIdcombo()
        {
            using (conn = new SqlConnection(@"Data Source=DESKTOP-S1C2ODL\SQLEXPRESS;Initial Catalog=BoutiqueDB;Integrated Security=True;Pooling=False"))
            {
                conn.Open();
                string query = "SELECT id FROM fournisseur";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Load(rdr);
                SuppliercomboBox1.ValueMember = "id";
                SuppliercomboBox1.DataSource = dt;




            }
        }
        private void SuppliercomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            categorycomboBox2.Items.Clear();
            fillCategorycombo();
        }

        private void bunifuDatavizAdvanced1_Load(object sender, EventArgs e)
        {

        }

        private void categorycomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

