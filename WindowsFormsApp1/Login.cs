using Date.Repositories;
using Doamin.Entities;
using Doamin.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        private readonly IAdminRepository _adminRepository;

        public Login()
        {

            InitializeComponent();

            _adminRepository = new AdminRepository();

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string pass = PassTxtBox.Text.Trim();
            string userName = UserTxtBox.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pass)) 
            { MessageBox.Show("لطفاً نام کاربری و رمز عبور را وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            Admin admin = _adminRepository.GetAdmin(userName, pass);
            if (admin!=null)
            {
                DialogResult = DialogResult.OK;
                Tag = admin;
                Hide();
            }
            else { MessageBox.Show("نام کاربری یا رمز عبور نادرست است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
