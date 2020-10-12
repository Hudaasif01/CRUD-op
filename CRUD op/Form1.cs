using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace CRUD_op
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-318DT4T\MSSQL;Initial Catalog=test;Integrated Security=True");
        public int cid;
        private void Form1_Load(object sender, EventArgs e)
        {
            getcustomerdata();
        }

        private void getcustomerdata()
        {
            SqlCommand cmd = new SqlCommand("select * from  customer", con);
            DataTable dt = new DataTable() ;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(isvalid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO customer values (@name,@email,@city,@mobileno)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@city", txtcity.Text);
                cmd.Parameters.AddWithValue("@mobileno", txtphoneno.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Customer is added successfully","saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                getcustomerdata();
                clearrecords();
            }
        }

        private bool isvalid()
        {
            if (txtname.Text == string.Empty)
            {
                MessageBox.Show("customer name is required", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            txtname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtemail.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtcity.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtphoneno.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (cid > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE customer SET Name=@name,email=@email,city=@city,mobileno=@mobileno  WHERE CustomerId=@CustomerId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@city", txtcity.Text);
                cmd.Parameters.AddWithValue("@mobileno", txtphoneno.Text);
                cmd.Parameters.AddWithValue("@CustomerId", this.cid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Customer is updated successfully", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getcustomerdata();
                clearrecords();
            }
            else
            {
                MessageBox.Show("Select customer first", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clearrecords()
        {
            txtname.Clear();
            txtemail.Clear();
            txtphoneno.Clear();
            txtcity.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cid > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM customer  WHERE CustomerId=@CustomerId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CustomerId", this.cid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Customer is deleted successfully", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getcustomerdata();
                clearrecords();
            }
            else
            {
                MessageBox.Show("Select customer first", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

