using System;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;

namespace DisenioZapata_V1.Model.UserModel
{
    public class CUser : NotificationObject
    {
        private int user_id;

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

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

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string industry;

        public string Industry
        {
            get { return industry; }
            set { industry = value; OnPropertyChanged(); }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; OnPropertyChanged(); }
        }

        private string UserIp { get; set; }
        private DateTime create_ad;

        public DateTime Create_ad
        {
            get { return create_ad; }
            set { create_ad = value; OnPropertyChanged(); }
        }

        private DateTime update_at;

        public DateTime Update_at
        {
            get { return update_at; }
            set { update_at = value; OnPropertyChanged(); }
        }
        public int IpUser { get; set; }

        public CUser()
        {
            IpUser = GetUserIp();
        }

        public CUser(string name, string email, string country, string password)
        {
            Name = name;
            Email = email;
            Country = country;
            Password = password;
        }

        public static CUser GetUser()
        {
            var recursos = App.Current.Resources;
            int cont = 0;
            CUser variables = null;

            foreach (DictionaryEntry i in recursos)
            {
                if (i.Key.ToString() == "User")
                {
                    variables = i.Value as CUser;
                    return variables;
                }

                cont++;
            }

            return variables;
        }

        private int GetUserIp()
        {
            int IP_Servidor = 0;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                string hostname = "FCSAS.COM";

                IPAddress[] addresses = Dns.GetHostAddresses(hostname);
                foreach (IPAddress address in addresses)
                {
                   IP_Servidor = (int)BitConverter.ToUInt32( address.GetAddressBytes(),0);
                }
            }

            IPAddress ip = new IPAddress(0x2414188f);
            int address2 = BitConverter.ToInt32(ip.GetAddressBytes(), 0);

            return address2;
        }
    }
}