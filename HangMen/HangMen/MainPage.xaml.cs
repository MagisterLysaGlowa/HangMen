using HangMen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HangMen
{
    public partial class MainPage : ContentPage
    {
        List<string> categories = new List<string>()
        {
            "item","animal","city","plant"
        };
        public MainPage()
        {
            InitializeComponent();
            category_picker.ItemsSource = categories;
            category_picker.SelectedItem = categories[0];
        }

        private void StartGame_Clicked(object sender, EventArgs e)
        {
            if (category_picker.SelectedItem == null) return;
            Navigation.PushAsync(new GamePage(category_picker.SelectedItem.ToString()));
        }
    }
}
