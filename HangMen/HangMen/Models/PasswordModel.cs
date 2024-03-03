using System;
using System.Collections.Generic;
using System.Text;

namespace HangMen.Models
{
    public class PasswordModel
    {
        public string Password { get; set; }
        public string Category { get; set; }

        public PasswordModel(string password, string category)
        {
            Password = password;
            Category = category;
        }
    }
}
