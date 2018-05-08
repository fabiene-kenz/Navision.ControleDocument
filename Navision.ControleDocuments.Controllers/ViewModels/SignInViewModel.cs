using Navision.ControleDocuments.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class SignInViewModel: BaseViewModel
    {

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _confirmPassword;
        
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                if (ConfirmPassword.Equals(""))
                    IsPasswordImageVisible = false;
                else
                {
                    Task.Run(async () => IsPasswordCorrect = await checkPasswordsAsync());
                    IsPasswordImageVisible = true;
                }
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private bool _isPasswordCorrect;

        public bool IsPasswordCorrect
        {
            get { return _isPasswordCorrect; }
            set
            {
                _isPasswordCorrect = value;
                OnPropertyChanged("IsPasswordCorrect");
            }
        }

        private bool _isPasswordImageVisible;

        public bool IsPasswordImageVisible
        {
            get { return _isPasswordImageVisible; }
            set
            {
                _isPasswordImageVisible = value;
                OnPropertyChanged("IsPasswordImageVisible");
            }
        }


        public SignInViewModel()
        {

        }

        async Task<bool> checkPasswordsAsync()
        {
            //bool isEqual = ConfirmPassword.Equals(Password);
            //if (IsPasswordImageVisible != isEqual)
            //    IsPasswordCorrect = isEqual;

            return ConfirmPassword.Equals(Password);
        }

    }
}
