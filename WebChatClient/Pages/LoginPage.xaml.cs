using System.Security;
using System.Windows.Controls;

namespace WebChatClient
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IHavePassword
    {
        public LoginPage()
        {
            InitializeComponent();

            DataContext = new LoginPageVM();
        }

        // Надежный пароль для этой страницы входа.
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
