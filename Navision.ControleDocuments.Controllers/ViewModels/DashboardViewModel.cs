using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Services.IServices;
using Navision.ControleDocuments.Services.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<DocModel> _docsModel = new ObservableCollection<DocModel>();
        public ObservableCollection<DocModel> DocsModel
        {
            get { return _docsModel; }
            set
            {
                _docsModel = value;

                OnPropertyChanged("DocsModel");
            }
        }
        private DocModel _docModel = new DocModel();
        public DocModel DocModel
        {
            get { return _docModel; }
            set
            {
                _docModel = value;
                OnPropertyChanged("DocModel");
            }
        }
        private readonly INavigation _navigation;
        private Type _page;
        #endregion
        public ICommand Click
        {
            get { return new Command(p => Test()); }
        }
        private IPageService _pageService;
        public DashboardViewModel(INavigation navigation, Type page)
        {
            _pageService = new PageService();
            _navigation = navigation;
            _page = page;
            Device.BeginInvokeOnMainThread(async () => DocsModel = await GetDocsAsync());
        }

        private async Task<ObservableCollection<DocModel>> GetDocsAsync()
        {
            List<DocModel> t = new List<DocModel>();
            t.Add(new DocModel { DocName = "test1", DocDate = DateTime.Now, DocSatut = true });
            t.Add(new DocModel { DocName = "test2", DocDate = DateTime.Now, DocSatut = false });
            t.Add(new DocModel { DocName = "test3", DocDate = DateTime.Now, DocSatut = null });
            ObservableCollection<DocModel> tcollect = new ObservableCollection<DocModel>(t);
            return tcollect;
        }
        private async void Test()
        {
            try
            {
                var page = (Page)Activator.CreateInstance(_page);
                await _pageService.PushAsync(_navigation,page);
            }
            catch (Exception ex)
            {

                throw;
            }
            //await _pageService.DisplayAlert("test", "je suis la popup", "ok", "Cancel");

            //await _pageService.PushAsync()
        }
    }
}
