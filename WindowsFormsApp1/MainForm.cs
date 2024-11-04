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
                JOIN Cities ON Users.CityId = Cities.Id";

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

            // تنظیمات اضافی برای DataGridView
            dataGridView1.ContextMenuStrip = Strip1;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
        }
        #region click
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

        }
    }
}
