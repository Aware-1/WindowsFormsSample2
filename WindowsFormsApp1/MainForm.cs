using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Data;
using Date.Repositories;
using Doamin.Interfaces;
using Microsoft.Extensions.Logging;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAdminRepository _adminRepository;
        LoggerConfig _loggerConfig;

        public MainForm()
        {
            InitializeComponent();
            _loggerConfig = new LoggerConfig();
            _userRepository = new UserRepository();
            _adminRepository = new AdminRepository();
            _cityRepository = new CityRepository();


            string ipAddress = GetLocalIPAddress();
            string computerName = Environment.MachineName;
            _loggerConfig.Information($"Form loaded successfully with IP: {ipAddress} and ComputerName: {computerName}");

        }





        #region click
        private string GetLocalIPAddress()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var networkInterface in networkInterfaces)
            {
                var properties = networkInterface.GetIPProperties();
                var addresses = properties.UnicastAddresses;
                foreach (var address in addresses)
                {
                    if (address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    { return address.Address.ToString(); }
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            //Hide();

            //Login login = new Login();
            //if (login.ShowDialog() == DialogResult.OK)
            //    Show();

            //else
            //    Application.Exit();

            LoadAdminGrid();
            LoadGrid();
        }
        private void کارکنان_Click(object sender, EventArgs e)
        {
            CityGridLoad();
        }


        public void Refresh()
        {
            dataGridView1.Rows.Clear();
            SearchBtn_Click_1(null, null);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView3_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView3.ClearSelection();
                dataGridView3.Rows[e.RowIndex].Selected = true;
            }
        }
        #endregion

        #region admin

        public void RefreshAdmin()
        {
            dataGridView3.Rows.Clear();
            LoadAdminGrid();
        }
        private void limitingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedUserId = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells["Id"].Value);

            EditAdmin editAdmin = new EditAdmin(selectedUserId);
            editAdmin.ShowDialog();
            RefreshAdmin();

        }

        private void LoadAdminGrid()
        {
            dataGridView3.Columns.Add("Id", "شناسه");
            dataGridView3.Columns.Add("UserName", "نام");
            dataGridView3.Columns.Add("IsLimit", "نام");
            var users = _adminRepository.GetUsers();
            foreach (var admin in users)
            {
                var limitStatus = admin.IsLimit ? "محدود" : "نا محدود";
                dataGridView3.Rows.Add(admin.Id, admin.UserName, limitStatus);
            }

            dataGridView3.ContextMenuStrip = Strip2;
            dataGridView3.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView3_CellMouseDown);

        }


        #endregion




        #region employ

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser(0);
            addUser.ShowDialog();
            SearchBtn_Click_1(null, null);
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
                var marriageStatus = user.marriage ? "متاهل" : "مجرد";
                dataGridView1.Rows.Add(user.Id, user.Name, user.City.Name, user.BirthDate.ToString("yyyy-MM-dd"), marriageStatus);
            }

            dataGridView1.ContextMenuStrip = Strip1;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
        }

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
                    try
                    {
                        _userRepository.DeleteUser(selectedUserId);
                        MessageBox.Show("آیتم حذف شد.", "حذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Refresh();
                    }
                    catch (Exception ex)
                    { MessageBox.Show($"خطا در حذف آیتم: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    MessageBox.Show("حذف لغو شد.", "لغو حذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            { MessageBox.Show("لطفاً یک کاربر برای حذف انتخاب کنید."); }
        }

        #endregion


        #region City

        //add


        private void SearchBtn_Click_1(object sender, EventArgs e)
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

        #endregion


    }
}