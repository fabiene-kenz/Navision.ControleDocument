using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Navision.ControleDocuments
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new Navision.ControleDocuments.LoadingPage());
            //MainPage = new NavigationPage(new Navision.ControleDocuments.DashboardPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

        /// <summary>
        /// On resume, the credentials from de cache are removed and
        /// user is redirected to Login page
        /// </summary>
		protected override void OnResume ()
		{
            if (Current.Properties.ContainsKey("UserData"))
                Current.Properties.Remove("UserData");
            MainPage = new NavigationPage(new Navision.ControleDocuments.MainPage());
        }
    }
}
