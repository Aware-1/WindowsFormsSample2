
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Date.Repositories;
using Doamin.Interfaces;
using Microsoft.Extensions.Logging;

namespace WindowsFormsApp1
{
    public partial class AddUser : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";

        private readonly ICityRepository _cityRepository;
     
        public int userId = 0;

        public AddUser(int selectedRowId)
        {
            InitializeComponent();
            _cityRepository = new CityRepository();
            userId = selectedRowId;
            if (userId != 0)
            {
                //LoadCustomerDetails();
            }
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

        }



        private void AddBtn_Click(object sender, EventArgs e)
        {
            string userName = NameBox.Text;
            DateTime birthDate = dateTimePicker.Value;
            bool isMarried = marriageBox.Checked;
            string cityName = cityComboBox.SelectedItem != null ? cityComboBox.SelectedItem.ToString() : "";

            var cityId = _cityRepository.GetCityIdByName(cityName);
            if (cityId == -1)
            { MessageBox.Show("شهر انتخابی یافت نشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            _cityRepository.AddUserToDatabase(userName, birthDate, isMarried, cityId);
            this.Close();
            new MainForm().refresh1(); 
        }

        

       

        private void cityComboBox_Click_1(object sender, EventArgs e)
        {
            List<string> cityNames = _cityRepository.GetCityNames();
            cityComboBox.Items.Clear(); 
            cityComboBox.Items.AddRange(cityNames.ToArray());
        }
    }
}
