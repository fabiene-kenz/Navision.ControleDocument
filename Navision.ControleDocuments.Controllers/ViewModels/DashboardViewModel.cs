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

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        #region Properties

        private readonly IDocumentsService _documentsService;
        private readonly INavigation _navigation;
        private Type _page;
        private IPageService _pageService;

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
            set { _filterNameLabel = value; OnPropertyChanged("FilterNameLabel"); }
        }
        
        private bool _approuvedSwitch = true;
        public bool ApprouvedSwitch
        {
            get { return _approuvedSwitch; }
            set { _approuvedSwitch = value; ValidateFilters(); OnPropertyChanged("ApprouvedSwitch"); }
        }

        private bool _unapprouvedSwitch = true;
        public bool UnapprouvedSwitch
        {
            get { return _unapprouvedSwitch; }
            set { _unapprouvedSwitch = value; ValidateFilters(); OnPropertyChanged("UnapprouvedSwitch"); }
        }

        private bool _toDoSwitch = true;
        public bool ToDoSwitch
        {
            get { return _toDoSwitch; }
            set { _toDoSwitch = value; ValidateFilters(); OnPropertyChanged("ToDoSwitch"); }
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

        private string _numberDocuments;
        public string NumberDocuments
        {
            get { return _numberDocuments; }
            set { _numberDocuments = value; OnPropertyChanged("NumberDocuments"); }
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
            set { _isPopUpVisible = value; OnPropertyChanged("IsPopUpVisible"); }
        }
        
        public ICommand ShowPopUpCommand
        {
            get { return new Command(async() => await ShowPopUpFilter()); }
        }

        public ICommand RefreshCommand
        {
            get { return new Command(async () => await GetDocsAsync()); }
        }

        public ICommand SearchCommand
        {
            get { return new Command(async () => await performSearch()); }
        }
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
            var listDocuments = await _documentsService.GetDocuments();
            //List<DocModel> listDocuments = new List<DocModel>();
            //listDocuments.Add(new DocModel { DocName = "JPG", DocDate = new DateTime(2018, 06, 04), DocSatut = null });
            //listDocuments.Add(new DocModel { DocName = "PDF", DocDate = new DateTime(2018, 06, 04), DocSatut = null });
            //listDocuments.Add(new DocModel { DocName = "PDF2", DocDate = new DateTime(2018, 06, 04), DocSatut = true });
            //listDocuments.Add(new DocModel { DocName = "test4", DocDate = new DateTime(2018, 06, 04), DocSatut = true });
            //listDocuments.Add(new DocModel { DocName = "test5", DocDate = new DateTime(2018, 06, 04), DocSatut = false });
            //listDocuments.Add(new DocModel { DocName = "test6", DocDate = new DateTime(2018, 06, 04), DocSatut = false });
            ObservableCollection<DocModel> tcollect = new ObservableCollection<DocModel>(listDocuments);
            _docsModelUnfiltered = tcollect;
            NumberDocuments = tcollect.Count().ToString() + " document(s) trouvé(s)";
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
        public async Task performSearch()
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

            _docsModelFiltered = saveList;
            DocsModel = _docsModelFiltered;
            NumberDocuments = DocsModel.Count().ToString() + " document(s) trouvé(s)";
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
    }
}
