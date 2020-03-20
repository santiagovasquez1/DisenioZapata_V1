using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DisenioZapata_V1.Model.UserModel
{
    public class NewUserViewModel : NotificationObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
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
            set { selectedcountry = value; OnPropertyChanged(); }
        }

        private string password1;

        public string Password1
        {
            get { return password1; }
            set { password1 = value; OnPropertyChanged(); }
        }

        private string password2;

        public string Password2
        {
            get { return password2; }
            set { password2 = value; OnPropertyChanged(); }
        }

        public MiComando CrearUsuarioCommand { get; set; }

        private CUser User { get; set; }

        public NewUserViewModel()
        {
            Countries = GetCountryList();
            CrearUsuarioCommand = new MiComando(CrearUsuarioCommandExecute);
        }

        private void CrearUsuarioCommandExecute()
        {            
            List<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).ToList();
            var LCIDCountry = cultures.FindIndex(x => (new RegionInfo(x.LCID)).DisplayName == selectedcountry);
            RegionInfo region = new RegionInfo(cultures[LCIDCountry].LCID);

            User = new CUser(Name, Email, region.ThreeLetterISORegionName, Password1);
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