using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace MolotovSportWeb.Components.Classes.CheakData
{
    public class CheakingDataRegistration
    {

        public string cheakName(string name)
        {
            string errorName = "";
            if (name != null)
            {
                if (name.Length < 5) errorName = "Имя должно быть не короче 5 символов";
                else
                {
                    errorName = "";
                }
            }
            return errorName;
        }

        public string cheakEmail(string email)
        {
            string errorEmail = "";

            var cheakEmail = new EmailAddressAttribute();
            if (!cheakEmail.IsValid(email))
            {
                errorEmail = "Некоректный адресс электронной почты";
            }
            else
            {
                errorEmail = "";
            }
            return errorEmail;
        }

        public string cheakPassword(string password)
        {
            string errorPassword;
            if (password.Length < 6)
            {
                errorPassword = "Пароль должен быть не менее 6 символов";
            }
            else
            {
                errorPassword = "";
            }
            return errorPassword;
        }

        public bool DisableButton(string errorName, string errorEmail, string errorPassword)
        {
            string errorAll = errorName + errorEmail + errorPassword;
            if (errorAll.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
