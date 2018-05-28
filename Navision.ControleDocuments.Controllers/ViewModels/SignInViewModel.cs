using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.UserModels;
using Navision.ControleDocuments.Services.IServices;
using Navision.ControleDocuments.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        #region Properties

        private readonly IPageService _pageService;
        private readonly IUserLoginService _userLoginService;

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
                Task.Run(async () => await CheckPasswordAsync());
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

        public ICommand CreateAccountCommand
        {
            get { return new Command(async() => await CreateAccountAsync()); }
        }
        #endregion

        public SignInViewModel()
        {
            _userLoginService = new UserLoginService();
            _pageService = new PageService();
        }
        /// <summary>
        /// Check Passwords
        /// </summary>
        /// <returns></returns>
        private async Task CheckPasswordAsync()
        {
            if (ConfirmPassword.Equals(""))
                IsPasswordImageVisible = false;
            else
            {
                IsPasswordCorrect = ConfirmPassword.Equals(Password);
                //IsPasswordCorrect = await CheckPasswordsAsync();
                IsPasswordImageVisible = true;
            }
        }
        /// <summary>
        /// Create an account
        /// </summary>
        /// <returns></returns>
        private async Task CreateAccountAsync()
        {
            var passwordCrypted = Convert.ToBase64String(Utils.EncryptStringToBytes_Aes(Password));
            if ( await _userLoginService.AddUser( new UserModel { UserName=UserName,Password= passwordCrypted }))
            {
                await _pageService.DisplayAlert("Compte Créee", "Vous etes autorisé à vous connecter avec ce compte", "Ok", "Cancel");
            }
            else
            {
                await _pageService.DisplayAlert("Compte Non Crée", "Vous n'etes pas autorisé à vous connecter avec ce compte", "Ok", "Cancel");
            }
            return;
        }

       

    }
}
