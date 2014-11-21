using DogBookApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DogBookApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private const string dbName = "SQLiteUsers";

        public List<User> Users { get; set; }
        public User currentUser { get; set; }

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Check login data
            this.GotoMainPage();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Check register data
            this.GotoMainPage();
        }

        private void GotoMainPage()
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            // GET USER FROM SQLite
            bool dbExists = await CheckDbAsync(dbName);
            if (!dbExists)
            {
                await CreateDatabaseAsync();
                await AddUsersAsync();
            }

            // Get Users
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            var query = conn.Table<User>();
            // UNCOMMENT IF YOU RUN FOR FIRST TIME
            //await conn.CreateTableAsync<User>();
            //await AddUserAsync();
            Users = await query.ToListAsync();
            foreach (var user in Users)
            {
                if (user.IsLoggedIn)
                {
                    currentUser = user;
                    break;
                }
            }
        }

        #region SQLite Methods
        private async Task<bool> CheckDbAsync(string dbName)
        {
            bool dbExist = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }

        private async Task CreateDatabaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.CreateTableAsync<User>();
        }

        private async Task AddUsersAsync()
        {
            // Create a Users list
            var list = new List<User>()
            {
                new User()
                {
                    Username = "User1",
                    Password = "Security1"
                },
                new User()
                {
                    Username = "User2",
                    Password = "Security2",
                    IsLoggedIn = true
                }
            };

            // Add rows to the User Table
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.InsertAllAsync(list);
        }

        private async Task AddUserAsync()
        {
            // Create a Users list
            var user = new User()
            {
                Username = "User3",
                Password = "Security3",
                IsLoggedIn = true
            };

            // Add rows to the User Table
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.InsertAsync(user);
        }

        private async Task SearchUserByTitleAsync(string username)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);

            AsyncTableQuery<User> query = conn.Table<User>().Where(x => x.Username.Contains(username));
            List<User> result = await query.ToListAsync();
            foreach (var User in result)
            {
                // ...
            }

            var allUsers = await conn.QueryAsync<User>("SELECT * FROM Users");
            foreach (var User in allUsers)
            {
                // ...
            }

            var otherUsers = await conn.QueryAsync<User>(
                "SELECT Password FROM Users WHERE Username = ?", new object[] { "User2" });
            foreach (var User in otherUsers)
            {
                // ...
            }
        }

        private async Task UpdateUserTitleAsync(string oldUsername, string newUsername)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);

            // Retrieve User
            var User = await conn.Table<User>()
                .Where(x => x.Username == oldUsername).FirstOrDefaultAsync();
            if (User != null)
            {
                // Modify User
                User.Username = newUsername;

                // Update record
                await conn.UpdateAsync(User);
            }
        }

        private async Task DeleteUserAsync(string name)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);

            // Retrieve User
            var User = await conn.Table<User>().Where(x => x.Username == name).FirstOrDefaultAsync();
            if (User != null)
            {
                // Delete record
                await conn.DeleteAsync(User);
            }
        }

        private async Task DropTableAsync(string name)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.DropTableAsync<User>();
        }
        #endregion
    }
}
