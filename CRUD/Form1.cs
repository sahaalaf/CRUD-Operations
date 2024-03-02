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

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGrid.CellClick += Cell_Clicked;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Sahal Sajeed\\Documents\\CRUD.mdf\";Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string Querry = "INSERT INTO stdData values(@RegNo, @name, @fname, @semester, @section, @gpa)";
                SqlCommand command = new SqlCommand(Querry, connection);
                command.Parameters.AddWithValue("@RegNo", int.Parse(regNoTxt.Text));
                command.Parameters.AddWithValue("@name", nameTxt.Text);
                command.Parameters.AddWithValue("@fname", fNameTxt.Text);
                command.Parameters.AddWithValue("@semester", semTxt.Text);
                command.Parameters.AddWithValue("@section", sectionCombo.Text);
                command.Parameters.AddWithValue("@gpa", float.Parse(gpaTxt.Text));
                command.ExecuteNonQuery();
                connection.Close();
                clearFields();
                MessageBox.Show("Student Added Successfully.");
            }
            catch(SqlException ex)
            {
                if(ex.Number == 267)
                {
                    MessageBox.Show("Registration ID: " + regNoTxt.Text + " is taken already.");
                }
                else
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Sahal Sajeed\\Documents\\CRUD.mdf\";Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string Querry = "UPDATE stdData SET name = @name, fname = @fname, semester = @semester, section = @section, gpa = @gpa WHERE gpa = @gpa";
                SqlCommand command = new SqlCommand(Querry, connection);
                command.Parameters.AddWithValue("@RegNo", int.Parse(regNoTxt.Text));
                command.Parameters.AddWithValue("@name", nameTxt.Text);
                command.Parameters.AddWithValue("@fname", fNameTxt.Text);
                command.Parameters.AddWithValue("@semester", semTxt.Text);
                command.Parameters.AddWithValue("@section", sectionCombo.Text);
                command.Parameters.AddWithValue("@gpa", gpaTxt.Text);
                command.ExecuteNonQuery();
                connection.Close();
                clearFields();
                MessageBox.Show("Student Updated Successfully.");
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Sahal Sajeed\\Documents\\CRUD.mdf\";Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string Querry = "DELETE stdData WHERE RegNo = @RegNo";
                SqlCommand command = new SqlCommand(Querry, connection);
                command.Parameters.AddWithValue("RegNo", int.Parse(regNoTxt.Text));
                command.ExecuteNonQuery();
                connection.Close();
                clearFields();
                MessageBox.Show("Student Data Deleted Successfully");
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Sahal Sajeed\\Documents\\CRUD.mdf\";Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Querry = "SELECT * FROM stdData";
            SqlCommand command = new SqlCommand(Querry, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGrid.DataSource = dataTable;
        }

        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGrid.Rows[e.RowIndex];
                regNoTxt.Text = row.Cells["RegNo"].Value.ToString();
                nameTxt.Text = row.Cells["Name"].Value.ToString();
                fNameTxt.Text = row.Cells["FName"].Value.ToString();
                semTxt.Text = row.Cells["Semester"].Value.ToString();
                sectionCombo.Text = row.Cells["Section"].Value.ToString();
                gpaTxt.Text = row.Cells["GPA"].Value.ToString();
            }
        }

        private void clearFields()
        {
            regNoTxt.Clear();
            nameTxt.Clear();
            fNameTxt.Clear();
            semTxt.Clear();
            sectionCombo.SelectedIndex = -1;
            gpaTxt.Clear();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
