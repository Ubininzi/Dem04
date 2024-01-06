using dem04.EFModel;
using System.Linq;
using System.Windows;


namespace dem04
{
    public partial class LoginWindow : Window
    {
        Dem04DbContext db = new Dem04DbContext();
        public LoginWindow()
        {
            InitializeComponent();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckAuthtorization(LoginTextBox.Text, PasswordTextBox.Text))
            {
                MessageBox.Show("Указанный логин или пароль не найден!","Авторизация не удалась");
                return;
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private bool CheckAuthtorization(string Login, string Password) {
            if (db.Logins.Any(log => log.Login1 == Login && log.Password == Password))
            {
                return true;
            }
            return false;
        }
    }
}
