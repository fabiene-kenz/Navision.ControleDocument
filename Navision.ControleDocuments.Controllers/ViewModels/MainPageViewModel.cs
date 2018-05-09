using Navision.ControleDocuments.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
   public class MainPageViewModel: BaseViewModel
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
                OnPropertyChanged(Password);
            }
        }
                
        public MainPageViewModel()
        {
            new Command(async () => await loginAsync());
        }

        private async Task loginAsync()
        {
            await Task.Delay(2000);
        }

        public ICommand LoginCommand {
            get
            {
                return new Command(async () => await loginAsync());
            }
        }
    }
}
