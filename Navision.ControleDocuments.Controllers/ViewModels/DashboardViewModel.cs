using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.UserModels;
using Navision.ControleDocuments.Services.IServices;
using Navision.ControleDocuments.Services.Services;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Navision.ControleDocument.DependenciesServices.IServices;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        #region Properties

        private readonly IStreamService _streamservice;
        private readonly IDocumentsService _documentsService;
        private readonly INavigation _navigation;
        private Type _page;
        private IPageService _pageService;
        private IZipService _zipService;

        private DocModel _docModel = new DocModel();
        public DocModel DocModel
        {
            get { return _docModel; }
            set
            {
                _docModel = value;

                if (_docModel != null && _docModel.DocName != null && _docModel.DocDate.Year != 0001)
                    NextPage();
                OnPropertyChanged("DocModel");
            }
        }

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

        private ObservableCollection<DocModel> _docsModelFiltered;
        private ObservableCollection<DocModel> _docsModelUnfiltered = new ObservableCollection<DocModel>();

        private string _filterNameLabel;
        public string FilterNameLabel
        {
            get { return _filterNameLabel; }
            set
            {
                _filterNameLabel = value;
                OnPropertyChanged("FilterNameLabel");
            }
        }

        private bool _approuvedSwitch = true;
        public bool ApprouvedSwitch
        {
            get { return _approuvedSwitch; }
            set
            {
                _approuvedSwitch = value;
                ValidateFilters();
                OnPropertyChanged("ApprouvedSwitch");
            }
        }

        private bool _unapprouvedSwitch = true;
        public bool UnapprouvedSwitch
        {
            get { return _unapprouvedSwitch; }
            set
            {
                _unapprouvedSwitch = value;
                ValidateFilters();
                OnPropertyChanged("UnapprouvedSwitch");
            }
        }

        private bool _toDoSwitch = true;
        public bool ToDoSwitch
        {
            get { return _toDoSwitch; }
            set
            {
                _toDoSwitch = value;
                ValidateFilters();
                OnPropertyChanged("ToDoSwitch");
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        private int _numberDocuments = 0;
        public int NumberDocuments
        {
            get { return _numberDocuments; }
            set
            {
                _numberDocuments = value;
                OnPropertyChanged("NumberDocuments");
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

        private bool _isPopUpVisible = false;
        public bool IsPopUpVisible
        {
            get { return _isPopUpVisible; }
            set
            {
                _isPopUpVisible = value;
                OnPropertyChanged("IsPopUpVisible");
            }
        }

        public ICommand ShowPopUpCommand
        {
            get { return new Command(async () => await ShowPopUpFilter()); }
        }

        public ICommand RefreshCommand
        {
            get { return new Command(async () => DocsModel = await GetDocsAsync()); }
        }

        public ICommand SearchCommand
        {
            get { return new Command(async () => await PerformSearch()); }
        }

        public ICommand RefreshCollection
        {
            get
            {
                return new Command(async () => DocsModel = await UpdateDocsAsync());
            }
        }

        public ICommand LogsCommand
        {
            get { return new Command(async () => await SendLogs()); }
        }

        #endregion

        #region CTR
        public DashboardViewModel(INavigation navigation, Type page)
        {
            UserModel user = Utils.DeserializeFromJson<UserModel>(Application.Current.Properties["UserData"].ToString());
            _pageService = new PageService();
            _navigation = navigation;
            _page = page;
            _documentsService = new DocumentsService(user);
            _streamservice = new StreamService(user.Token);
            _zipService = new ZipService();
            Device.BeginInvokeOnMainThread(async () => DocsModel = await GetDocsAsync());
        }
        #endregion
        /// <summary>
        /// Get Documents
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<DocModel>> GetDocsAsync()
        {
            IsLoading = true;
            var listDocuments = await _documentsService.GetDocuments();

            ObservableCollection<DocModel> tcollect = new ObservableCollection<DocModel>(listDocuments);
            _docsModelUnfiltered = tcollect;

            NumberDocuments = tcollect.Count();
            IsLoading = false;
            return tcollect;
        }
        /// <summary>
        /// Update list of document
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<DocModel>> UpdateDocsAsync()
        {
            var collec = DocsModel.ToList();
            DocsModel = null;

            ObservableCollection<DocModel> tcollect = new ObservableCollection<DocModel>(collec);
            return tcollect;
        }
        /// <summary>
        /// Get value selected and switch page
        /// </summary>
        private async void NextPage()
        {
            try
            {
                if (DocModel.DocSatut == false || DocModel.DocSatut == true)
                    await _pageService.DisplayAlert("Erreur", "Impossible d'ouvrir le document car il a déjà été traité.", "OK");
                else
                {
                    var page = (Page)Activator.CreateInstance(_page);
                    var pageDetail = (ViewerDocumentViewModel)page.BindingContext;
                    pageDetail.Doc = DocModel;
                    await _pageService.PushAsync(_navigation, page);
                }
                DocModel = null;
            }
            catch (Exception ex)
            {
                await DependencyService.Get<ILogger>().WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Show or hide the popup for filtering the documents
        /// </summary>
        /// <returns></returns>
        private async Task ShowPopUpFilter()
        {
            IsPopUpVisible = !IsPopUpVisible;
        }

        /// <summary>
        /// Perform a search on the list of documents from a string
        /// </summary>
        /// <returns></returns>
        public async Task PerformSearch()
        {
            if (string.IsNullOrEmpty(SearchText))
                DocsModel = _docsModelUnfiltered;
            else
            {
                _docsModelFiltered = new ObservableCollection<DocModel>(_docsModelUnfiltered.Where(i => i.DocName.ToLower().Contains(SearchText.ToLower())));
                DocsModel = _docsModelFiltered;
            }
            await ValidateFilters();
        }

        /// <summary>
        /// Filter the documents by status and update the view
        /// </summary>
        /// <returns></returns>
        private async Task ValidateFilters()
        {
            ObservableCollection<DocModel> saveList = new ObservableCollection<DocModel>();

            if (!string.IsNullOrEmpty(SearchText))
                saveList = await FilterWithSearchText(saveList);
            else
                saveList = await FilterWithEmptySearchText(saveList);
            DocsModel = saveList;

            NumberDocuments = DocsModel.Count();
        }

        /// <summary>
        /// Filter the documents by status without having a string for searching documents by name
        /// </summary>
        /// <param name="saveList"></param>
        /// <returns></returns>
        private async Task<ObservableCollection<DocModel>> FilterWithEmptySearchText(ObservableCollection<DocModel> saveList)
        {
            if (ApprouvedSwitch)
                foreach (var doc in _docsModelUnfiltered.Where(x => x.DocSatut == true))
                    saveList.Add(doc);
            if (UnapprouvedSwitch)
                foreach (var doc in _docsModelUnfiltered.Where(x => x.DocSatut == false))
                    saveList.Add(doc);
            if (ToDoSwitch)
                foreach (var doc in _docsModelUnfiltered.Where(x => x.DocSatut == null))
                    saveList.Add(doc);
            return saveList;
        }

        /// <summary>
        /// Filter the documents by status with a string for searching documents by name
        /// </summary>
        /// <param name="saveList"></param>
        /// <returns></returns>
        private async Task<ObservableCollection<DocModel>> FilterWithSearchText(ObservableCollection<DocModel> saveList)
        {
            if (ApprouvedSwitch)
                foreach (var doc in _docsModelUnfiltered.Where(x => x.DocSatut == true).Where(x => x.DocName.ToLower().Contains(SearchText.ToLower())))
                    saveList.Add(doc);
            if (UnapprouvedSwitch)
                foreach (var doc in _docsModelUnfiltered.Where(x => x.DocSatut == false).Where(x => x.DocName.ToLower().Contains(SearchText.ToLower())))
                    saveList.Add(doc);
            if (ToDoSwitch)
                foreach (var doc in _docsModelUnfiltered.Where(x => x.DocSatut == null).Where(x => x.DocName.ToLower().Contains(SearchText.ToLower())))
                    saveList.Add(doc);
            return saveList;
        }

        private async Task SendLogs()
        {
            var logFolder = DependencyService.Get<ILogger>().GetLogFolder();
            string ZipFilePath = await _zipService.ZipLogs(logFolder);

            if (!string.IsNullOrEmpty(ZipFilePath) && await _streamservice.SendLogs(ZipFilePath) == true)
                await _pageService.DisplayAlert("Terminé", "Envoi des fichiers de log réussi.", "OK");
            else
                await _pageService.DisplayAlert("Erreur", "Erreur lors de l'envoi des fichiers de logs.", "OK");
        }
    }
}
