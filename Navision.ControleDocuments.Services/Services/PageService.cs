using Navision.ControleDocuments.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Services.Services
{
    public class PageService : IPageService
    {
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PushAsync(INavigation nav, Page page)
        {
            await nav.PushAsync(page,true);
        }

        public async Task PopAsync(INavigation nav)
        {
            await nav.PopAsync(true);
        }
    }
}
