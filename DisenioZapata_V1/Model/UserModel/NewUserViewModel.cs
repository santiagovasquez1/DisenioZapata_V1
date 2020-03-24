using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Xamarin.Forms;

namespace DisenioZapata_V1.Model.UserModel
{
    public class NewUserViewModel : NotificationObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; CrearUsuarioCommand.ReevaluateCanExecute(); OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; CrearUsuarioCommand.ReevaluateCanExecute(); OnPropertyChanged(); }
        }

        private string industry;

        public string Industry
        {
            get { return industry; }
            set { industry = value; OnPropertyChanged(); }
        }

        private List<string> countries;

        public List<string> Countries
        {
            get { return countries; }
            set { countries = value; OnPropertyChanged(); }
        }

        private string selectedcountry;

        public string Selectedcountry
        {
            get { return selectedcountry; }
            set { selectedcountry = value; CrearUsuarioCommand.ReevaluateCanExecute(); OnPropertyChanged(); }
        }

        private string password1;

        public string Password1
        {
            get { return password1; }
            set { password1 = value; CrearUsuarioCommand.ReevaluateCanExecute(); OnPropertyChanged(); }
        }

        private string password2;

        public string Password2
        {
            get { return password2; }
            set { password2 = value; CrearUsuarioCommand.ReevaluateCanExecute(); OnPropertyChanged(); }
        }

        public MiComando CrearUsuarioCommand { get; set; }
        public MiComando MainWindowCommand { get; set; }
        private CUser User { get; set; }

        public NewUserViewModel()
        {
            Countries = GetCountryList();
            CrearUsuarioCommand = new MiComando(CrearUsuarioCommandExecute, CrearUsuarioCommandcanExecute);
            MainWindowCommand = new MiComando(MainWindowExecute);
        }

        private void MainWindowExecute()
        {
            MessagingCenter.Send(this, "GoToMainWindow");
        }

        private void CrearUsuarioCommandExecute()
        {
            DataBase data = new DataBase();

            if (data.CheckEmail(Email) == false)
            {
                List<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).ToList();
                var LCIDCountry = cultures.FindIndex(x => (new RegionInfo(x.LCID)).DisplayName == selectedcountry);
                RegionInfo region = new RegionInfo(cultures[LCIDCountry].LCID);

                User = CUser.GetUser();
                User.Name = Name;
                User.Email = Email;
                User.Country = region.ThreeLetterISORegionName;
                User.Industry = Industry;
                User.Password = Password1;

                int id = 0;
                data.InsertUser(User.Name, User.Password, User.Email, User.Industry, User.Country, out id);

                User.User_id = id;

                MainWindowExecute();
            }
            else
                MessageBox.Show("El correo ya se encuentra registrado.");
        }

        private bool CrearUsuarioCommandcanExecute()
        {
            if (Name == null | email == null | Selectedcountry == null | Password1 == null | Password2 == null)
                return false;
            else if (Password1 != Password2)
                return false;
            else if (Email.Contains("@") == false)
                return false;
            else
                return true;
        }

        public List<string> GetCountryList()
        {
            List<string> cultureList = new List<string>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            cultureList = cultures.Select(cult => (new RegionInfo(cult.LCID)).DisplayName).Distinct().OrderBy(q => q).ToList();

            return cultureList;
        }
    }
}