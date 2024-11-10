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

        private readonly ICityRepository _cityRepository;
        private readonly IUserRepository _userRepository;

        public int userId = 0;

        public AddUser(int selectedRowId)
        {
            InitializeComponent();
            _cityRepository = new CityRepository();
            _userRepository = new UserRepository();
            userId = selectedRowId;


        }



        private void AddBtn_Click(object sender, EventArgs e)
        {
            string userName = NameBox.Text;
            DateTime birthDate = dateTimePicker.Value;
            bool isMarried = marriageBox.Checked;
            string cityName = cityComboBox.SelectedItem != null ? cityComboBox.SelectedItem.ToString() : "";
            
            if (string.IsNullOrEmpty(userName))
            {  MessageBox.Show("لطفاً نام را وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); return;  }

            var cityId = _cityRepository.GetCityIdByName(cityName);
            if (cityId == -1)
            { MessageBox.Show("شهر انتخابی یافت نشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            _userRepository.AddUserToDatabase(userName, birthDate, isMarried, cityId);
            this.Close();
        }





        private void cityComboBox_Click_1(object sender, EventArgs e)
        {
            List<string> cityNames = _cityRepository.GetCityNames();
            cityComboBox.Items.Clear();
            cityComboBox.Items.AddRange(cityNames.ToArray());
        }
    }
}
