using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.Pages
{
    public partial class SignUpPage : Page
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public int? Age { get; set; }
        public SignUpPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if ((UserName != "" || FirstName != "" || LastName != "" || Password1 != "" || Password2 != "") && (Age >= 18 && Age < 100))
            {
                if (Password1 == Password2)
                    using (SqlConnection connection = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;"))
                    {
                        try
                        {
                            connection.Open();
                            string query = @$"INSERT INTO User_Table (Username, Firstname, Lastname, Password, Age)
                                      VALUES ('{UserName}', '{FirstName}', '{LastName}', '{Password1}', {Age})";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("User successfully registered", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.Navigate(new SignInPage());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        MainWindow.ReadFromDB();
                        NavigationService.Navigate(new SignInPage());
                    }
                else
                    MessageBox.Show("Passwords are not equal","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
                    MessageBox.Show("All parametrs are not entered equal","Error",MessageBoxButton.OK,MessageBoxImage.Error);
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage());
        }
    }
}
