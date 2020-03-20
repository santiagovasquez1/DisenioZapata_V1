using DisenioZapata_V1.Model;
using DisenioZapata_V1.Model.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Xamarin.Forms;

namespace DisenioZapata_V1.View
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<LoginViewModel>(this, "GoToNewUser", (a) =>
            {
                var NewUser = new CrearUsuarioPage();
                NewUser.Show();
                Close();
            });

            MessagingCenter.Subscribe<LoginViewModel>(this, "GoToMainWindow", (a) =>
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();
                Close();
            });
        }

        private void PasswordChangedHandler(object sender, RoutedEventArgs e)
        {
            var login = GetLoginModel();
            login.Password = P1.Password;
        }

        private LoginViewModel GetLoginModel()
        {
            var recursos = this.Resources;
            int cont = 0;
            LoginViewModel variables = null;

            foreach (DictionaryEntry i in recursos)
            {
                if (i.Key.ToString() == "LoginViewModel")
                {
                    variables = i.Value as LoginViewModel;
                    return variables;
                }
                cont++;
            }

            return variables;
        }
    }
}
