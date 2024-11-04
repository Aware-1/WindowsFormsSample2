
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

        private void NameBox_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void cityComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            List<string> cityNames = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Cities";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cityNames.Add(reader["Name"].ToString());
                    }
                }
            }
            cityComboBox.Items.Clear();
            cityComboBox.Items.AddRange(cityNames.ToArray());
        }

        private void dateTimePicker_ValueChanged(object sender, System.EventArgs e)
        {

        }

        private void marriageBox_CheckedChanged(object sender, System.EventArgs e)
        {

        }
    }
}
