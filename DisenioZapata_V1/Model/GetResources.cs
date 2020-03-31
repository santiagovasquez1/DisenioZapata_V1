using DisenioZapata_V1.Model.UserModel;
using System.Collections;

namespace DisenioZapata_V1.Model
{
    public static class GetResources
    {
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

        public static Datos_Zapatas Get_DatosZapatas()
        {
            var recursos = App.Current.Resources;
            int cont = 0;
            Datos_Zapatas variables = null;

            foreach (DictionaryEntry i in recursos)
            {
                if (i.Key.ToString() == "DatosZapatas")
                {
                    variables = i.Value as Datos_Zapatas;
                    return variables;
                }
                cont++;
            }

            return variables;
        }
    }
}