using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages;
public partial class SignInPage : Page, INotifyPropertyChanged
{
    private string username;
    private string password;

    public string Username { get => username; set { username = value; OnPropertyChanged(); } }
    public string Password { get => password; set { password = value; OnPropertyChanged(); } }
    public SignInPage()
    {
        InitializeComponent();
        DataContext = this;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void SignUp_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new SignUpPage());
    }

    private void SignIn_Click(object sender, RoutedEventArgs e)
    {
        var user = MainWindow.Users.FirstOrDefault(u => u.Username == Username);
        if (user is null)
            MessageBox.Show("User can not be found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        else
        {
            if (user.Password == Password)
            {
                MessageBox.Show($"Welcome {user.Firstname} {user.Lastname} !!!", "Successful Login!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                Password = "";
                Username = "";
            }


            else
                MessageBox.Show($"Wrong Password check it again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
