using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    [Table("Users")]
    public class SQLiteUserModel
    {
        public SQLiteUserModel() : this("Unknown", "free") { }

        public SQLiteUserModel(string name, string pass, bool logged = false)
        {
            this.Username = name;
            this.Password = pass;
            this.IsLoggedIn = logged;
        }

        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [Unique, MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}
