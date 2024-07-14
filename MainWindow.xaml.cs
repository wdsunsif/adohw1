using System.Data.SqlClient;
using System.Windows;
using System.Windows.Navigation;
namespace WpfApp1;
public partial class MainWindow : NavigationWindow
{
    public static List<User> Users { get; set; } = new();

    public MainWindow()
    {
        InitializeComponent();
        ReadFromDB();
    }
    public static void ReadFromDB()
    {
        using (SqlConnection connection = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;"))
        {
            var query = "SELECT * FROM User_Table";
            var cmd = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Users.Clear();
                while (reader.Read())
                    Users.Add(new User
                    {
                        Username = reader["Username"].ToString()!,
                        Firstname = reader["FirstName"].ToString()!,
                        Lastname = reader["LastName"].ToString()!,
                        Password = reader["Password"].ToString()!,
                        Age = Convert.ToInt32(reader["Age"].ToString())
                    });
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}