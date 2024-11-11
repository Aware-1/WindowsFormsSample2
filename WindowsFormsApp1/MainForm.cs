using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Data;
using Date.Repositories;
using Doamin.Entities;
using Doamin.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Net.Http;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAdminRepository _adminRepository;
        LoggerConfig _loggerConfig;
        private Admin _currentAdmin;

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
            Hide();

            Login login = new Login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                Show();
                _currentAdmin = (Admin)login.Tag;
                AccessCurentAdmin();
            }
            else
                Application.Exit();
            LoadJson();
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
        private void AccessCurentAdmin()
        {
            if (_currentAdmin.IsLimit)
            {
                toolStripMenuItem1.Visible = false;
                limitingToolStripMenuItem.Visible = false;
            }
            else
            {
                btnReport.Visible = false;
                SearchBtnAdmin.Visible = false;
                textBox1.Visible = false;
            }
        }
        private void LoadAdminGrid()
        {
            dataGridView3.Columns.Add("Id", "شناسه");
            dataGridView3.Columns.Add("UserName", "نام");
            dataGridView3.Columns.Add("IsLimit", "محدودیت");
            var users = _adminRepository.GetUsers();
            foreach (var admin in users)
            {
                var limitStatus = admin.IsLimit ? "محدود" : "نا محدود";
                dataGridView3.Rows.Add(admin.Id, admin.UserName, limitStatus);
            }

            dataGridView3.ContextMenuStrip = Strip2;
            dataGridView3.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView3_CellMouseDown);

        }
        private void SearchBtnAdmin_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();

            string searchText = textBox1.Text;

            var Admins = _adminRepository.GetAdmins(searchText);
            foreach (var admin in Admins)
            {
                dataGridView3.Rows.Add(admin.Id, admin.UserName, admin.IsLimit);
            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedUserId = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells["Id"].Value);
                DialogResult result = MessageBox.Show("آیا مطمئن هستید که می‌خواهید این آیتم را حذف کنید؟", "تایید حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _adminRepository.DeleteAdmin(selectedUserId);
                        MessageBox.Show("آیتم حذف شد.", "حذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshAdmin();
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


        private void btnReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Save as PDF";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                GeneratePdfFromDataGridView(saveFileDialog.FileName);
            }
        }
        private void GeneratePdfFromDataGridView(string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "DataGridView Export";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);

            int yPoint = 0;

            // Write header
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                gfx.DrawString(dataGridView3.Columns[i].HeaderText, font, XBrushes.Black,
                    new XRect(40 + i * 100, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            }
            yPoint += 40;

            // Write rows
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    gfx.DrawString(dataGridView1.Rows[i].Cells[j].Value?.ToString(), font, XBrushes.Black,
                        new XRect(40 + j * 100, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                }
                yPoint += 40;
            }

            document.Save(filePath);
            MessageBox.Show("PDF saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        #region json 
        private async void LoadJson()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            dataGridView4.Columns.Clear();

            dataGridView4.Columns.Add("id", "شناسه");
            dataGridView4.Columns.Add("name", "نام");
            dataGridView4.Columns.Add("email", "ایمیل");

            List <User> users = JsonConvert.DeserializeObject<List<User>>(json);
                dataGridView4.DataSource = users;



            dataGridView4.ContextMenuStrip = Strip3;
            dataGridView4.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);

        }








        #endregion

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Client selectedUser = dataGridView4.CurrentRow.DataBoundItem as Client;
            //int selectedUserId = Convert.ToInt32(dataGridView4.SelectedRows[0].Cells["Id"].Value);

            DetailsJson userDetailsForm = new DetailsJson(selectedUser);
            userDetailsForm.ShowDialog();

        }
    }
}
