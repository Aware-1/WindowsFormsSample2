using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Data;
using Date.Repositories;
using Doamin.Interfaces;
using Microsoft.Extensions.Logging;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";
        private readonly ILogger<MainForm> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;

        public MainForm()
        { 
            InitializeComponent();
            _userRepository = new UserRepository(); 
            _cityRepository = new CityRepository();
            LoadGrid();
            CityGridLoad();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
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
 
            dataGridView1.Rows.Clear();

            var users = _userRepository.GetUsers();
            foreach (var user in users)
            {
                dataGridView1.Rows.Add(user.Id, user.Name, user.City.Name, user.BirthDate.ToString("yyyy-MM-dd"), user.marriage);
            }

            dataGridView1.ContextMenuStrip = Strip1;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
        }
        #region click
        public void refresh1()
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
            List<string> cityNames = _cityRepository.GetCityNames();
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

            var users = _userRepository.GetUsers(searchText, selectedCity);
            foreach (var user in users)
            {
                dataGridView1.Rows.Add(user.Id, user.Name, user.City.Name, user.BirthDate.ToString("yyyy-MM-dd"), user.marriage);
            }
        }

        private void CityGridLoad()
        {
            if (dataGridView2.Columns.Count == 0)
            {
                dataGridView2.Columns.Add("Id", "شناسه");
                dataGridView2.Columns.Add("Name", "شهر");
                dataGridView2.Columns.Add("Province", "استان");
            }

            dataGridView2.Rows.Clear();

            var cities = _cityRepository.GetCities();
            foreach (var city in cities)
            {
                dataGridView2.Rows.Add(city.Id, city.Name, city.Province);
            }

            dataGridView1.ContextMenuStrip = Strip1;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
        }

        private void SearchBtn1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();

            string searchText = CityBox2.Text;

            var cities = _cityRepository.GetCities(searchText);
            foreach (var city in cities)
            {
                dataGridView2.Rows.Add(city.Id, city.Name, city.Province);
            }
        }


    }
}