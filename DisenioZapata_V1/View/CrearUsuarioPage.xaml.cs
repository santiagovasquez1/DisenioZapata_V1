using DisenioZapata_V1.Model.UserModel;
using System.Collections;
using System.Windows;
using Xamarin.Forms;

namespace DisenioZapata_V1.View
{
    /// <summary>
    /// Lógica de interacción para CrearUsuarioPage.xaml
    /// </summary>
    public partial class CrearUsuarioPage : Window
    {
        public CrearUsuarioPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<NewUserViewModel>(this, "GoToMainWindow", (a) =>
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();
                Close();
            });
        }

        private void PasswordChangedHandler1(object sender, RoutedEventArgs e)
        {
            var user = GetNewUser();
            user.Password1 = P1.Password;
        }

        private void PasswordChangedHandler2(object sender, RoutedEventArgs e)
        {
            var user = GetNewUser();
            user.Password2 = P2.Password;
        }

        private NewUserViewModel GetNewUser()
        {
            var recursos = this.Resources;
            int cont = 0;
            NewUserViewModel variables = null;

            foreach (DictionaryEntry i in recursos)
            {
                if (i.Key.ToString() == "NewUser")
                {
                    variables = i.Value as NewUserViewModel;
                    return variables;
                }
                cont++;
            }

            return variables;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var ventanas = App.Current.Windows;
            var user = GetNewUser();

            foreach (var ventana in ventanas)
            {
                if (ventana.ToString() == "DisenioZapata_V1.View.LoginPage")
                {
                    var LoginPage = ventana as LoginPage;
                    if (user.CrearUsuarioCommandcanExecute() == false)
                        LoginPage.Show();
                    else
                        LoginPage.Close();
                    break;
                }
            }
        }
    }
}