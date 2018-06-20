using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Services.IServices
{
    /// <summary>
    /// Popup and Navigation
    /// </summary>
    public interface IPageService
    {
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task DisplayAlert(string title, string message, string cancel);
        Task PushAsync(INavigation nav, Page page);
        Task PopAsync(INavigation nav);
    }
}
