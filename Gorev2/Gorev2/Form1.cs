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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gorev2
{
    public partial class Form1 : Form
    {
      
        private string connectionString = "Data Source=CAYIRAILESI\\SQLEXPRESS;Initial Catalog=StudensDB;Integrated Security=True;TrustServerCertificate=True";
        public Form1()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        private void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Students", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Students (Ad, Soyad, DogumTarihi, Sınıf, Bolüm, AktifDurum) VALUES (@FirstName, @LastName, @BirthDate, @Class, @Department, @IsActive)", con); 
                cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@BirthDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Class", textBox3.Text);
                cmd.Parameters.AddWithValue("@Department", textBox3.Text);
                cmd.Parameters.AddWithValue("@IsActive", comboBox1.SelectedItem.ToString() == "Active");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student added successfully!");
                LoadStudents();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE OgrenciNo=@ID", con);
                cmd.Parameters.AddWithValue("@ID", textBox5.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student deleted successfully!");
                LoadStudents();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            LoadStudents();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Students SET Ad=@FirstName, Soyad=@LastName, DogumTarihi=@BirthDate, Sınıf=@Class, Bolüm=@Department, AktifDurum=@IsActive WHERE OgrenciNo=@ID", con);
                cmd.Parameters.AddWithValue("@ID", textBox5.Text);
                cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@BirthDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Class", textBox3.Text);
                cmd.Parameters.AddWithValue("@Department", textBox4.Text);
                cmd.Parameters.AddWithValue("@IsActive", comboBox1.SelectedItem.ToString() == "Active");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student updated successfully!");
                LoadStudents();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                
                string query = "SELECT * FROM Students WHERE 1=1";  

                
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    query += " AND OgrenciNo = @ID";
                }

                
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    query += " AND Ad LIKE @FirstName";
                }

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    da.SelectCommand.Parameters.AddWithValue("@ID", textBox5.Text);
                }

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    da.SelectCommand.Parameters.AddWithValue("@FirstName", "%" + textBox1.Text + "%");
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

    }
}
