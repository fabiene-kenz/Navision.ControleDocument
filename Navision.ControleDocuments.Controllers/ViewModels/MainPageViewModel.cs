using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Controllers.Constants;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.UserModels;
using Navision.ControleDocuments.Services.IServices;
using Navision.ControleDocuments.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Properties
        private readonly IPageService _pageService;
        private readonly Type _signInPage;
        private readonly Type _dashboardPage;
        private readonly INavigation _navigation;
        private readonly IUserLoginService _userLoginService;
        private readonly IReadFileService _readFileService;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

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
        /// <summary>
        /// Check if the user can connect
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() => await StartLoading());
            }
        }
        /// <summary>
        /// Switch to SignIn Page
        /// </summary>
        public ICommand FirstConnectCommand
        {
            get { return new Command(async () => await FirstConnectAsync()); }
        }
        #endregion
        #region CTR
        public MainPageViewModel(Type signinpage, Type dashboardpage, INavigation navigation)
        {
            _pageService = new PageService();
            _signInPage = signinpage;
            _dashboardPage = dashboardpage;
            _navigation = navigation;
            _userLoginService = new UserLoginService();
            _readFileService = new ReadFileService();


            //var stream = _readFileService.GetFileStream("Navision.ControleDocuments.Services.DB.db.sqlite3");

            var dbSql = DependencyService.Get<ISQLite>().GetLocalFilePath("db.sqlite3");

            GetClientParamService t = new GetClientParamService(dbSql);

            var result = t.GetClient();
            IsBusy = false;
        }
        #endregion

        /// <summary>
        /// Start displaying login, then do LoginAsync()
        /// </summary>
        /// <returns></returns>
        private async Task StartLoading()
        {
            IsBusy = true;
            await LoginAsync();
        }

        /// <summary>
        /// Check if the user can connect
        /// </summary>
        /// <returns></returns>
        private async Task LoginAsync()
        {
            if (await GetTokenForUser())
            {
                var page = (Page)Activator.CreateInstance(_dashboardPage);
                // Main become a NavigationPage and the DashBoardPage is the root
                Application.Current.MainPage = new NavigationPage(page);
            }
            else
            {
                await _pageService.DisplayAlert("Refus", "Vous n'etes pas autorisé à vous connecter", "Ok", "Cancel");
            }
            IsBusy = false;
        }
        /// <summary>
        /// Switch to SignIn Page
        /// </summary>
        /// <returns></returns>
        private async Task FirstConnectAsync()
        {
            var page = (Page)Activator.CreateInstance(_signInPage);
            await _pageService.PushAsync(_navigation, page);
        }

        /// <summary>
        /// Get Token and Store in Cache
        /// </summary>
        /// <returns></returns>
        private async Task<bool> GetTokenForUser()
        {
            // Crypt password
            var passwordCrypted = Convert.ToBase64String(Utils.EncryptStringToBytes_Aes(Password));
            string token = await _userLoginService.GetToken(new UserModel { UserName = UserName, Password = passwordCrypted });

            if (!String.IsNullOrEmpty(token))
            {
                Application.Current.Properties["UserData"] = Utils.SerializeToJson(new UserModel { UserName = UserName, Password = passwordCrypted, Token = token });
                return true;
            }
            else
                return false;
        }
    }
}
