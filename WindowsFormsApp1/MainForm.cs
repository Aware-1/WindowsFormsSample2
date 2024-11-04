using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Doamin.Interfaces;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";

        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;

        public MainForm(IUserRepository userRepository, ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
        }

        public MainForm()
        {
            InitializeComponent();
            LoadGrid();
            GridLoad();
        }


        //add
        private void AddBtn_Click(object sender, EventArgs e)
        {

            AddUser addUser = new AddUser(0);
            addUser.ShowDialog();
        }
        //load
        private void LoadGrid()
        {

            dataGridView1.Columns.Add("Id", "شناسه");
            dataGridView1.Columns.Add("Name", "نام");
            dataGridView1.Columns.Add("CityName", "شهر");
            dataGridView1.Columns.Add("BirthDate", "تاریخ تولد");
            dataGridView1.Columns.Add("marriage", "تاهل");


            string query = @"
                SELECT Users.Id, Users.Name, Cities.Name AS CityName, Users.BirthDate, Users.marriage
                FROM Users
                JOIN Cities ON Users.CityId = Cities.Id
                WHERE Users.IsDelete = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["CityName"].ToString(),
                        reader["BirthDate"].ToString(),
                        reader["marriage"].ToString()
                    );
                }
                reader.Close();
            }

            dataGridView1.ContextMenuStrip = Strip1;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
        }
        #region click
        private void refresh1()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            LoadGrid();
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        #endregion



        private void cityComboBox_Click(object sender, EventArgs e)
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

        private void Deletetem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                DialogResult result = MessageBox.Show("آیا مطمئن هستید که می‌خواهید این آیتم را حذف کنید؟", "تایید حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Users SET IsDelete = 1 WHERE Id = @UserId";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@UserId", selectedUserId);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("آیتم حذف شد.", "حذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refresh1();
                }
                else
                {
                    MessageBox.Show("حذف لغو شد.", "لغو حذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("لطفاً یک کاربر برای حذف انتخاب کنید.");

            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            string searchText = NameBox.Text;
            string selectedCity = cityComboBox.SelectedItem != null ? cityComboBox.SelectedItem.ToString() : "";
            string query = $@"
                SELECT Users.Id, Users.Name, Cities.Name AS CityName, Users.BirthDate, Users.marriage
                FROM Users
                JOIN Cities ON Users.CityId = Cities.Id
                WHERE Users.IsDelete = 0 "
            + (string.IsNullOrEmpty(searchText) ? "" : $@"AND Users.Name LIKE N'%{searchText}%'")
            + (string.IsNullOrEmpty(selectedCity) ? "" : $"and  Cities.Name  LIKE N'%{selectedCity}%'  ");


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["CityName"].ToString(),
                        reader["BirthDate"].ToString(),
                        reader["marriage"].ToString()
                    );
                }

                reader.Close();
            }
        }














        private void GridLoad()
        {

            dataGridView2.Columns.Add("Id", "شناسه");
            dataGridView2.Columns.Add("Name", "شهر");
            dataGridView2.Columns.Add("Province", "استان");



            string query = @"
                SELECT Id, Name, Province
                FROM Cities
                WHERE IsDelete = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView2.Rows.Add(
                        reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["Province"].ToString()
                    );
                }
                reader.Close();
            }

            dataGridView1.ContextMenuStrip = Strip1;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
        }


        



        private void SearchBtn1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();

            string searchText = CityBox2.Text;
            string query = $@"
                SELECT Id, Name, Province
                FROM Cities
                WHERE IsDelete = 0 "
            + (string.IsNullOrEmpty(searchText) ? "" : $@"AND Name LIKE N'%{searchText}%'");


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView2.Rows.Add(
                        reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["Province"].ToString() 
                    );
                }

                reader.Close();
            }
        }

    }
}