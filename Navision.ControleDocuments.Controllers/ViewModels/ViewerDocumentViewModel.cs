using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.UserModels;
using Navision.ControleDocuments.Models.ValuesToValidateModel;
using Navision.ControleDocuments.Services.IServices;
using Navision.ControleDocuments.Services.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class ViewerDocumentViewModel : BaseViewModel
    {
        #region properties
        private readonly IStreamService _streamservice;
        private readonly IPageService _pageService;
        private readonly INavigation _navigation;
        private readonly IDocumentsService _documentsService;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private DocModel _doc = new DocModel() { };
        public DocModel Doc
        {
            get { return _doc; }
            set
            {
                if (_doc != value)
                {
                    _doc = value;
                    Task.Run(async () => Images = await GetPdf(value));
                    Task.Run(async () => ValuesModel = await GetValuesAsync());
                }
                OnPropertyChanged("Doc");
            }
        }

        private bool _showPanelImg = false;
        public bool ShowPanelImg
        {
            get { return _showPanelImg; }
            set { _showPanelImg = value; OnPropertyChanged("ShowPanelImg"); }
        }


        private bool _selectAllIsToggled;

        public bool SelectAllIsToggled
        {
            get { return _selectAllIsToggled; }
            set { _selectAllIsToggled = value; OnPropertyChanged("SelectAllIsToggled"); }
        }

        private string _propertyName;

        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; OnPropertyChanged("PropertyName"); }
        }

        private string _propertyValue;

        public string PropertyValue
        {
            get { return _propertyValue; }
            set { _propertyValue = value; OnPropertyChanged("PropertyValue"); }
        }

        private bool _isValidated;

        public bool IsValidated
        {
            get { return _isValidated; }
            set { _isValidated = value; OnPropertyChanged("IsValidated"); }
        }

        private ObservableCollection<ValuesToValidateModel> _valuesModel = new ObservableCollection<ValuesToValidateModel>();

        public ObservableCollection<ValuesToValidateModel> ValuesModel
        {
            get { return _valuesModel; }
            set { _valuesModel = value; OnPropertyChanged("ValuesModel"); }
        }

        private ValuesToValidateModel _valueModel;

        public ValuesToValidateModel ValueModel
        {
            get { return _valueModel; }
            set { _valueModel = value; OnPropertyChanged("ValueModel"); }
        }

        public ICommand DoneBtn
        {
            get
            {
                return new Command(async () => await DoneCommand());
            }
        }

        public ICommand BtnCommand
        {
            get
            {
                return new Command(async () => await SelectAllBtn());
            }
        }
        public ICommand CleanWebApi
        {
            get
            {
                return new Command(async () => await CleanFolder());
            }
        }
        private List<PdfModel> _images = new List<PdfModel>();
        public List<PdfModel> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged("Images");
            }

        }
        #endregion

        public ViewerDocumentViewModel(INavigation navigation)
        {
            UserModel user = Utils.DeserializeFromJson<UserModel>(Application.Current.Properties["UserData"].ToString());
            _pageService = new PageService();
            _navigation = navigation;
            _streamservice = new StreamService(user);
            _documentsService = new DocumentsService(user);

            

            IsLoading = true;
            SelectAllIsToggled = false;
        }
        /// <summary>
        /// Get all pdf for user
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private async Task<List<PdfModel>> GetPdf(DocModel doc)
        {
            List<PdfModel> listPdfModel = await _streamservice.GetPdfFile(doc);
            IsLoading = false;
            ShowPanelImg = true;
            return listPdfModel;
        }
        /// <summary>
        /// Get values to check
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<ValuesToValidateModel>> GetValuesAsync()
        {
            var values = await _documentsService.GetValueToCheck(Doc);
            List<ValuesToValidateModel> check = new List<ValuesToValidateModel>();
            foreach (var value in values)
            {
                check.Add(new ValuesToValidateModel { PropertyName = value.Name, PropertyValue = value.Value, IsValidated = false });
            }
            check.Add(new ValuesToValidateModel { PropertyName = "Vendor Invoice No", PropertyValue = Doc.VendorInvoiceNo, IsValidated = false });
            check.Add(new ValuesToValidateModel { PropertyName = "Vendor Shipment No", PropertyValue = Doc.VendorShipNo, IsValidated = false });
            check.Add(new ValuesToValidateModel { PropertyName = "Vendor Name", PropertyValue = Doc.VendorName, IsValidated = false });
            check.Add(new ValuesToValidateModel { PropertyName = "Document Date", PropertyValue = Doc.DocumentDate, IsValidated = false });
            ObservableCollection<ValuesToValidateModel> tcollect = new ObservableCollection<ValuesToValidateModel>(check);
            return tcollect;
        }

        /// <summary>
        /// Either the switches need to be set to true or not
        /// </summary>
        /// <returns></returns>
        private async Task SelectAllBtn()
        {
            if (SelectAllIsToggled == false)
                SelectAllIsToggled = true;
            else
                SelectAllIsToggled = false;
            ValuesModel = UpdateValuesToValidate();
        }

        /// <summary>
        /// Get back to Dashboard page if user clicks on Terminer button
        /// </summary>
        /// <returns></returns>
        private async Task DoneCommand()
        {
            var response = await _pageService.DisplayAlert("Confimer", "Enregistrer les modifications et quitter ?", "Confirmer", "Annuler");
            if (response)
            {
                await SetStatutDocument();
                await CleanFolder();
            }
        }

        /// <summary>
        /// Update the Values collection with the new IsValidated values 
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ValuesToValidateModel> UpdateValuesToValidate()
        {
            var newValuesCollection = new ObservableCollection<ValuesToValidateModel>();
            var collectionCopy = ValuesModel;

            foreach (var value in collectionCopy)
            {
                newValuesCollection.Add(new ValuesToValidateModel
                {
                    PropertyName = value.PropertyName,
                    PropertyValue = value.PropertyValue,
                    IsValidated = SelectAllIsToggled
                });
            }
            return newValuesCollection;
        }
        /// <summary>
        /// Update Statut in Navision
        /// </summary>
        private async Task SetStatutDocument()
        {
            if (ValuesModel.Any(v => !v.IsValidated))
            {
                Doc.IsApprove = false;
                Doc.DocSatut = false;

                Doc.DocDate = DateTime.Now;
            }
            else
            {
                Doc.IsApprove = true;
                Doc.DocSatut = true;
            }
            await _documentsService.ApproveOrRejectDocument(Doc);
        }
        /// <summary>
        /// Clean folder in webapi
        /// </summary>
        /// <returns></returns>
        private async Task CleanFolder()
        {
            await _pageService.PopAsync(_navigation);
            var deletedFolder = await _streamservice.CleanFolder(Doc);
            if (string.IsNullOrEmpty(deletedFolder))
                await _pageService.DisplayAlert("Erreur", "Erreur lors de la suppression du document.", "Ok");
        }

    }
}
