using Date.Repositories;
using Doamin.Entities;
using Doamin.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class EditAdmin : Form
    {


        public int rowId = 0;
        private readonly IAdminRepository _adminRepository;

        public EditAdmin(int selectedRowId)
        {
            InitializeComponent();
            _adminRepository = new AdminRepository();

            rowId = selectedRowId;
            if (rowId != 0)
            {
                LoadAdminDetails();
            }
        }

      


        private void LoadAdminDetails()
        {
            Admin admin = _adminRepository.GetAdminDetails(rowId);
            if (admin != null)
            {
                LblUser.Text = admin.UserName;
                checkBox2.Checked = admin.IsLimit;
                checkBox1.Checked = admin.Active;
            }
            else
            { MessageBox.Show("Admin not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string userName = LblUser.Text.Trim();
            bool active  = checkBox1.Checked;
            bool isLimit =  checkBox2.Checked;
            
            try
            {
                _adminRepository.UpdateAdmin(rowId, userName, isLimit, active);
                MessageBox.Show(rowId == 0 ? "اطلاعات Admin با موفقیت اضافه شد." : "اطلاعات Admin با موفقیت به‌روزرسانی شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره‌سازی اطلاعات Admin: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
    }
}
