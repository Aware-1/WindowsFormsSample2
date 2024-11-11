using Doamin.Entities;
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
    public partial class DetailsJson : Form
    {
        public int rowId = 0;
        public DetailsJson(Client selectedRow)
        {
            InitializeComponent();
            LoadDetailsJson(selectedRow);
        }

        private void LoadDetailsJson(Client client)
        {
            label28.Text = client.Name;
            label27.Text = client.Username;
            label26.Text = client.Email;
            label25.Text = client.Address.Street;
            label24.Text = client.Address.Suite;
            label23.Text = client.Address.City;
            label22.Text = client.Address.Zipcode;
            string mapUrl = $"https://www.google.com/maps?q={client.Address.Geo.Lat},{client.Address.Geo.Lng}";
            linkLabel1.Links.Add(0, mapUrl.Length, mapUrl);
            label20.Text = client.Phone;
            label19.Text = client.Website;
            label18.Text = client.Company.Name;
            label17.Text = client.Company.CatchPhrase;
            label12.Text = client.Company.Bs;

        }

      
    }
}

