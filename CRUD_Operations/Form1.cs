﻿using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Security.AccessControl;

namespace CRUD_Operations
{
	public partial class Form1 : Form
	{
		SqlConnection con = new SqlConnection(@"Data Source=SAIM-LEGION\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True");

		public int StudentId;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			GetStudentsRecrod();
		}

		private void GetStudentsRecrod()
		{
			SqlCommand cmd = new SqlCommand("Select * from PatientTb", con);
			DataTable dt = new DataTable();
			con.Open();
			SqlDataReader sdr = cmd.ExecuteReader();
			dt.Load(sdr);
			con.Close();

			StudentRecordView.DataSource = dt;

		}

		private void button1_Click(object sender, EventArgs e) // Update Button
		{
			if (StudentId > 0)
			{
				SqlCommand cmd = new SqlCommand("Update PatientTb SET Patient = @Name,CNIC = @FatherName,Blood = @RollNumber,Mobile = @Address,Age = @Mobile Where PatientID = @ID", con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Name", txt_student_name.Text);
				cmd.Parameters.AddWithValue("@FatherName", txt_father_name.Text);
				cmd.Parameters.AddWithValue("@RollNumber", txt_rollno.Text);
				cmd.Parameters.AddWithValue("@Address", txt_address.Text);
				cmd.Parameters.AddWithValue("@Mobile", txt_mobile.Text);
				cmd.Parameters.AddWithValue("@ID", StudentId);

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();


				GetStudentsRecrod();
				ResetValues();
				MessageBox.Show("Patient Information Is Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			MessageBox.Show("Please Select a Patient to Update The Information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

		}

		private void button3_Click(object sender, EventArgs e) //Delete Button
		{
			if(StudentId >0)
			{
				SqlCommand cmd = new SqlCommand("Delete FROM PatientTb Where PatientID = @ID",  con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@ID", StudentId);
				cmd.Parameters.RemoveAt(1);

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();


				GetStudentsRecrod();
				ResetValues();
				MessageBox.Show("Patient Is Deleted From The System", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			MessageBox.Show("Please Select a Patient to Delete", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

		}

		private void button2_Click(object sender, EventArgs e) //Insert Button
		{
			if(IsValid())
			{
				SqlCommand cmd = new SqlCommand("Insert Into PatientTb Values(@Patient,@CNIC,@Blood,@Mobile,@Age)", con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Patient", txt_student_name.Text);
				cmd.Parameters.AddWithValue("@CNIC", txt_father_name.Text);
				cmd.Parameters.AddWithValue("@Blood", txt_rollno.Text);
				cmd.Parameters.AddWithValue("@Mobile", txt_address.Text);
				cmd.Parameters.AddWithValue("@Age", txt_mobile.Text);

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();


				GetStudentsRecrod();
				ResetValues();
				MessageBox.Show("New Patient is successfully saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private bool IsValid() //Insert Button method
		{
			if(txt_student_name.Text ==  string.Empty)
			{
				MessageBox.Show("Patient Name Is Required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			ResetValues();
		}

		private void label5_Click(object sender, EventArgs e)
		{
		}

		private void textBox5_TextChanged(object sender, EventArgs e)
		{
		}

		private void ResetValues()
		{
			StudentId = 0;
			txt_student_name.Clear();
			txt_father_name.Clear();
			txt_rollno.Clear();
			txt_address.Clear();
			txt_mobile.Clear();

			txt_student_name.Focus();
		}

		private void StudentRecordView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			StudentId = Convert.ToInt32(StudentRecordView.SelectedRows[0].Cells[0].Value);
			txt_student_name.Text = StudentRecordView.SelectedRows[0].Cells[1].Value.ToString();
			txt_father_name.Text = StudentRecordView.SelectedRows[0].Cells[2].Value.ToString();
			txt_rollno .Text = StudentRecordView.SelectedRows[0].Cells[3].Value.ToString();
			txt_address.Text = StudentRecordView.SelectedRows[0].Cells[4].Value.ToString();
			txt_mobile.Text = StudentRecordView.SelectedRows[0].Cells[5].Value.ToString();
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}