using HangMen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HangMen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        string button_characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ-.";
        string hidden_password = "";
        string password_guess = "";
        string category_name;
        int mistakes = 0;
        List<PasswordModel> passwords = new List<PasswordModel>()
        {
            new PasswordModel("cow","animal"),
            new PasswordModel("dog","animal"),
            new PasswordModel("cat","animal"),
            new PasswordModel("elephant","animal"),
            new PasswordModel("hammer","item"),
            new PasswordModel("knife","item"),
            new PasswordModel("fork","item"),
            new PasswordModel("headphones","item"),
            new PasswordModel("new york","city"),
            new PasswordModel("warsaw","city"),
            new PasswordModel("pekin","city"),
            new PasswordModel("rose","plant"),
        };
        public GamePage(string category_name)
        {
            InitializeComponent();
            GenerateButtons();
            this.category_name = category_name;
            hidden_password = GetRandomPassword();
            SetPasswordInvisible();
        }

        private string GetRandomPassword()
        {
            var password_list = passwords.Where(x => x.Category == category_name).ToList();
            Random random = new Random();
            int randomIndex = random.Next(0, password_list.Count);
            PasswordModel randomItem = password_list[randomIndex];
            return randomItem.Password;
        }

        private void SetPasswordInvisible()
        {
            for (int i = 0; i < hidden_password.Length; i++)
            {
                if (hidden_password[i] != ' ')
                {
                    password_guess += '_';
                }
                else
                {
                    password_guess += ' ';
                }
                password_label.Text = password_guess;
            }
        }

        private void GenerateButtons()
        {
            for (int i = 0; i < button_characters.Length; i++)
            {
                var button = new Button()
                {
                    Text = button_characters[i].ToString(),
                    BackgroundColor = Color.Transparent,
                    BorderWidth = 3,
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Color.Black,
                    CornerRadius = 15
                };
                button.Clicked += LetterButton_Clicked;
                Grid.SetColumn(button, i%7);
                Grid.SetRow(button, i/7);
                button_grid.Children.Add(button);
            }
        }

        private void LetterButton_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (SetLettersInGuess(button.Text))
            {
                button.BorderColor = Color.Green;
                button.TextColor = Color.Green;
            }
            else
            {
                button.BorderColor = Color.Red;
                button.TextColor = Color.Red;
            }

        }

        private bool SetLettersInGuess(string letter)
        {
            bool any_correct = false;
            for (int i = 0; i < hidden_password.Length; i++)
            {
                if (hidden_password.ToUpper()[i] == letter.ToUpper()[0])
                {
                    StringBuilder stringBuilder = new StringBuilder(password_guess);
                    stringBuilder[i] = letter[0];
                    password_guess = stringBuilder.ToString();
                    any_correct = true;
                }
            }
            if (!any_correct)
            {
                mistakes++;
            }
            DisplayInfo();
            CheckEndGame();
            return any_correct;
        }

        private void DisplayInfo()
        {
            password_label.Text = password_guess;
            hangmen_image_container.Source = $"s{mistakes}.jpg";
            mistakes_label.Text = "mistakes: " + mistakes;
        }

        private void CheckEndGame()
        {
            if(password_guess.ToUpper() == hidden_password.ToUpper())
            {
                var button = new Button()
                {
                    TextColor = Color.White,
                    BackgroundColor = Color.Green,
                    CornerRadius = 15,
                    Text = "Wróć do menu"
                };
                button.Clicked += NavigateToMainMenu;
                button_area.Children.Add(button);
                button_grid.IsEnabled = false;
            }

            if(mistakes >= 9)
            {
                var button = new Button()
                {
                    TextColor = Color.White,
                    BackgroundColor = Color.Red,
                    CornerRadius = 15,
                    Text = "Wróć do menu"
                };
                button.Clicked += NavigateToMainMenu;
                button_area.Children.Add(button);
                button_grid.IsEnabled = false;
            }
        }

        private void NavigateToMainMenu(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}