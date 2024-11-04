
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Doamin.Interfaces;

namespace WindowsFormsApp1
{
    public partial class AddUser : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";

        private readonly ICityRepository _cityRepository;
        public AddUser( ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public int userId = 0;

        public AddUser(int selectedRowId)
        {
            InitializeComponent();
            userId = selectedRowId;
            if (userId != 0)
            {
                //LoadCustomerDetails();
            }
        }
        //private void LoadUSerDetails()
        //{

        //    string query = "SELECT * FROM Users WHERE IsDelete = 0 AND Id=@Id";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@Id", customerId);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            textBox2.Text = reader["UserName"].ToString();
        //            textBox1.Text = reader["lastName"].ToString();
        //            textBox4.Text = reader["Name"].ToString();
        //            textBox3.Text = reader["nationalCode"].ToString();
        //        }
        //        connection.Close();
        //    }
        //}

        //private void AddBtn_Click(object sender, System.EventArgs e)

        //{
        //    string UserName = text.Text;
        //    string lastName = textBox1.Text;
        //    string name = textBox4.Text;
        //    int nationalCode;
        //    if (!int.TryParse(textBox3.Text, out nationalCode))
        //    {
        //        MessageBox.Show("فقط عدد");
        //        return;
        //    }

        //    if (customerId == 0)
        //    {
        //        string query = " INSERT INTO Users (UserName, lastName, Name,nationalCode,IsDelete) VALUES (@UserName, @lastName, @Name,@nationalCode,0)";
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            SqlCommand command = new SqlCommand(query, connection);
        //            command.Parameters.AddWithValue("@UserName", UserName);
        //            command.Parameters.AddWithValue("@lastName", lastName);
        //            command.Parameters.AddWithValue("@Name", name);
        //            command.Parameters.AddWithValue("@nationalCode", nationalCode);
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //            connection.Close();
        //        }

        //        MessageBox.Show("شد");
        //    }
        //    else
        //    {
        //        string query = @"
        //            UPDATE Users
        //            SET UserName = @UserName, Name = @Name, lastName = @lastName, nationalCode=@nationalCode
        //            WHERE Id = @Id";
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            SqlCommand command = new SqlCommand(query, connection);
        //            command.Parameters.AddWithValue("@UserName", UserName);
        //            command.Parameters.AddWithValue("@lastName", lastName);
        //            command.Parameters.AddWithValue("@Name", name);
        //            command.Parameters.AddWithValue("@nationalCode", nationalCode);
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //            connection.Close();
        //        }

        //        MessageBox.Show("اچدیت شد");
        //    }

        //    Close();
        //}



        private void AddBtn_Click(object sender, EventArgs e)
        {
            // گرفتن مقادیر ورودی از کنترل‌ها
            string userName = NameBox.Text;
            DateTime birthDate = dateTimePicker.Value;
            bool isMarried = marriageBox.Checked;
            string cityName = cityComboBox.SelectedItem != null ? cityComboBox.SelectedItem.ToString() : "";

            // یافتن ID شهر انتخابی
            int cityId = GetCityIdByName(cityName);
            if (cityId == -1)
            {
                MessageBox.Show("City not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // افزودن کاربر به دیتابیس
            AddUserToDatabase(userName, birthDate, isMarried, cityId);

            // به روزرسانی DataGridView بعد از افزودن کاربر
            RefreshUserGrid();
        }

        private int GetCityIdByName(string cityName)
        {
            int cityId = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id FROM Cities WHERE Name = @CityName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CityName", cityName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cityId = (int)reader["Id"];
                }
                reader.Close();
            }

            return cityId;
        }

        private void AddUserToDatabase(string name, DateTime birthDate, bool marriage, int cityId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Name, BirthDate, marriage, CityId) VALUES (@Name, @BirthDate, @marriage, @CityId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@BirthDate", birthDate);
                command.Parameters.AddWithValue("@marriage", marriage);
                command.Parameters.AddWithValue("@CityId", cityId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
