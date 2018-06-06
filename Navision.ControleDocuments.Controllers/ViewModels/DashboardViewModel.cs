using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.UserModels;
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

        private readonly IDocumentsService _documentsService;
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
                if (_docModel!=value)
                {
                    _docModel = value;
                    NextPage();
                }
                OnPropertyChanged("DocModel");
            }
        }
        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;

                OnPropertyChanged("IsLoading");
            }
        }
        private readonly INavigation _navigation;
        private Type _page;
        private IPageService _pageService;
        #endregion
        #region CTR
        public DashboardViewModel(INavigation navigation, Type page)
        {
            UserModel user= Utils.DeserializeFromJson<UserModel>(Application.Current.Properties["UserData"].ToString());
            _pageService = new PageService();
            _navigation = navigation;
            _page = page;
            _documentsService = new DocumentsService(user.Token);
            Device.BeginInvokeOnMainThread(async () => DocsModel = await GetDocsAsync());
        }
        #endregion

        private async Task<ObservableCollection<DocModel>> GetDocsAsync()
        {
            //var listDocuments = await _documentsService.GetDocuments();
            List<DocModel> listDocuments = new List<DocModel>();
            listDocuments.Add(new DocModel { DocName="test", DocDate= new DateTime(2018, 06, 04), DocSatut=null});
            listDocuments.Add(new DocModel { DocName= "test2", DocDate = new DateTime(2018, 06, 04), DocSatut = null });
            ObservableCollection<DocModel> tcollect = new ObservableCollection<DocModel>(listDocuments);
            IsLoading = false;
            return tcollect;
        }
        /// <summary>
        /// Get value selected and switch page
        /// </summary>
        private async void NextPage()
        {
            try
            {
                var page = (Page)Activator.CreateInstance(_page);
                var pageDetail = (ViewerDocumentViewModel)page.BindingContext;
                await _pageService.PushAsync(_navigation,page);
                pageDetail.Doc = DocModel;
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
