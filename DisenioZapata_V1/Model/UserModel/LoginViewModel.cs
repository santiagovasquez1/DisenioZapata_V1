using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DisenioZapata_V1.Model.UserModel
{
    public class LoginViewModel:NotificationObject
    {
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

        public MiComando NuevoUsuarioCommand { get; set; }

        public LoginViewModel()
        {
            Countries = GetCountryList();
            NuevoUsuarioCommand = new MiComando(NuevoUsuarioCommandExecute);
        }

        private void NuevoUsuarioCommandExecute()
        {
            MessagingCenter.Send(this, "GoToNewUser");
        }

        public List<string> GetCountryList()
        {
            List<string> cultureList = new List<string>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            cultureList = cultures.Select(cult => (new RegionInfo(cult.LCID)).DisplayName).Distinct().OrderBy(q => q).ToList();
            //foreach (CultureInfo culture in cultures)
            //{
            //    RegionInfo region = new RegionInfo(culture.LCID);

            //    if (!(cultureList.Contains(region.EnglishName)))
            //    {
            //        cultureList.Add(region.EnglishName);
            //    }
            //}

            return cultureList;
        }
    }
}
