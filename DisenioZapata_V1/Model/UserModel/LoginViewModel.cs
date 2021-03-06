﻿using System.Windows;
using Xamarin.Forms;

namespace DisenioZapata_V1.Model.UserModel
{
    public class LoginViewModel : NotificationObject
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; MainWindowCommand.ReevaluateCanExecute(); OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; MainWindowCommand.ReevaluateCanExecute(); OnPropertyChanged(); }
        }

        private CUser User { get; set; }
        public MiComando NuevoUsuarioCommand { get; set; }
        public MiComando MainWindowCommand { get; set; }

        public LoginViewModel()
        {
            NuevoUsuarioCommand = new MiComando(NuevoUsuarioCommandExecute);
            MainWindowCommand = new MiComando(MainWindowCommandExecute, ConectarUsuarioCommandcanExecute);
        }

        private void MainWindowCommandExecute()
        {
            int UserId;
            User = GetResources.GetUser();
            DataBase data = new DataBase();

            if (data.CheckEmail(Email, Password, out UserId))
            {
                User.User_id = UserId;
                MessagingCenter.Send(this, "GoToMainWindow");
            }
            else
                MessageBox.Show("Usuario o clave incorrectos");
        }

        private void NuevoUsuarioCommandExecute()
        {
            MessagingCenter.Send(this, "GoToNewUser");
        }

        private bool ConectarUsuarioCommandcanExecute()
        {
            if (Email == null | Password == null)
                return false;
            else
                return true;
        }
    }
}