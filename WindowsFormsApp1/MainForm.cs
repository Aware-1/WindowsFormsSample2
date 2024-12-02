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
using Date.Service;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Threading;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ICityRepository _cityRepository;
        private readonly IAdminRepository _adminRepository;
        LoggerConfig _loggerConfig;
        private Admin _currentAdmin;


        private static readonly object _lockObj = new object();
        private static Semaphore _semaphore = new Semaphore(1, 1);
        private static Mutex _mutex = new Mutex();


        public MainForm()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
            _loggerConfig = new LoggerConfig();
            _adminRepository = new AdminRepository();
            _cityRepository = new CityRepository();
            _userService = new UserService();
            ApplyPersianCulture(this.Controls);

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
            //{
            //    Show();
            //    _currentAdmin = (Admin)login.Tag;
            //    AccessCurentAdmin();
            //}
            //else
            //    Application.Exit();
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





        private string ConvertToPersianNumbers(string input) 
        { 
            return input.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲")
                .Replace("3", "۳").Replace("4", "۴").Replace("5", "۵")
                .Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹"); }

        #region employ

        private void AddBtn_Click(object sender, EventArgs e)
        {
            //_semaphore.WaitOne();
            // _mutex.WaitOne();



            AddUser addUser = new AddUser(0);
            addUser.ShowDialog();

            try
            {
       
                lock (_lockObj)
                {
   
                    SearchBtn_Click_1(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Main thread: {ex.Message}");
            }
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
                var idFarsi = ConvertToPersianNumbers(user.Id.ToString());
                var DateFarsi = ConvertToPersianNumbers(user.BirthDate.ToString("yyyy-MM-dd"));
                var marriageStatus = user.marriage ? "متاهل" : "مجرد";
                dataGridView1.Rows.Add(idFarsi, user.Name, user.City.Name, DateFarsi, marriageStatus);
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

            List<Client> users = await _userService.LoadJson();

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
        //adimn
        private void ExelBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save as Excel File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportDataGridViewToExcel(dataGridView4, saveFileDialog.FileName);
                }
            }
        }
        private void ExportDataGridViewToExcel(DataGridView dgv, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dgv.Columns[i].HeaderText;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                File.WriteAllBytes(filePath, package.GetAsByteArray());
                MessageBox.Show("افرین ", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExlBtmEmpl_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save as Excel File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportDataGridViewToExcel(dataGridView1, saveFileDialog.FileName);
                }
            }
        }

        private void importExlBtn_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx";
                openFileDialog.Title = "Select an Excel File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportDataFromExcel(openFileDialog.FileName);
                }
            }
        }
        private void ImportDataFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.First();
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                for (int row = 2; row <= rowCount; row++)
                {
                    string name = worksheet.Cells[row, 2].Text;
                    int cityId = int.Parse(worksheet.Cells[row, 3].Text);
                    //DateTime birthDate = DateTime.Now;
                    DateTime birthDate = DateTime.Parse(worksheet.Cells[row, 4].Text);
                    // bool marriage = bool.Parse(worksheet.Cells[row, 5].Text);
                    bool marriage = worksheet.Cells[row, 5].Text == "1" ? false : true;

                    _userRepository.InsertUser(name, birthDate, marriage, cityId);
                }
                MessageBox.Show("افرین", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



       

            private void ApplyPersianCulture(Control.ControlCollection controls)
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.Font = new Font("Tahoma", 12, FontStyle.Regular);
                        textBox.RightToLeft = RightToLeft.Yes;
                    }
                    else if (control is Label label)
                    {
                        label.Font = new Font("Tahoma", 12, FontStyle.Regular);
                        label.RightToLeft = RightToLeft.Yes;
                    }
                    // تنظیمات مشابه را برای سایر کنترل‌های متنی اعمال کنید

                    // بازگشتی برای کنترل‌های داخلی
                    if (control.HasChildren)
                    {
                        ApplyPersianCulture(control.Controls);
                    }
                }
            }
        

    }
}