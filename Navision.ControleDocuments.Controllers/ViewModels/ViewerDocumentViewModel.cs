using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.UserModels;
using Navision.ControleDocuments.Services.IServices;
using Navision.ControleDocuments.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class ViewerDocumentViewModel : BaseViewModel
    {
        private readonly IStreamService _streamservice;
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

        private DocModel _doc = new DocModel();
        public DocModel Doc
        {
            get { return _doc; }
            set
            {
                if (_doc != value)
                {
                    _doc = value;
                    Task.Run(async () => Images = await GetPdf(value));
                }
                OnPropertyChanged("Doc");
            }
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
            set { _valuesModel = value; OnPropertyChanged("Values"); }
        }

        private ValuesToValidateModel _valueModel;

        public ValuesToValidateModel ValueModel
        {
            get { return _valueModel; }
            set { _valueModel = value; OnPropertyChanged("Value"); }
        }

        public ICommand BtnCommand
        {
            get
            {
                return new Command(async () => await SelectAllBtn());
            }
        }

        #endregion

        public ViewerDocumentViewModel()
        {
            //DocPath = "6005.pdf";
            //DocPath = "Enterprise-Application-Patterns-using-XamarinForms.pdf";
            ////string fileName = DependencyService.Get<ILocalStorageFolder>().GetLocalFilePath("TEST.pdf");
            UserModel user = Utils.DeserializeFromJson<UserModel>(Application.Current.Properties["UserData"].ToString());
            _streamservice = new StreamService(user.Token);
            IsLoading = true;
        }
       
        private  async Task<List<PdfModel>> GetPdf(DocModel doc)
        {
            List<PdfModel> listPdfModel = await _streamservice.GetPdfFile(doc);
            IsLoading = false;
            return listPdfModel;
        }

        private ObservableCollection<ValuesToValidateModel> GetValuesAsync()
        {
            List<ValuesToValidateModel> t = new List<ValuesToValidateModel>();
            t.Add(new ValuesToValidateModel { PropertyName = "Name 0", PropertyValue = "Value 0", IsValidated = false });
            //t.Add(new ValuesToValidate { PropertyName = "Name 1", PropertyValue = "Value 1", IsValidated = true });
            //t.Add(new ValuesToValidate { PropertyName = "Name 2", PropertyValue = "Value 2", IsValidated = false });
            //t.Add(new ValuesToValidate { PropertyName = "Name 3", PropertyValue = "Value 3", IsValidated = true });
            //t.Add(new ValuesToValidate { PropertyName = "Name 4", PropertyValue = "Value 4", IsValidated = false });
            //t.Add(new ValuesToValidate { PropertyName = "Name 5", PropertyValue = "Value 5", IsValidated = true });
            //t.Add(new ValuesToValidate { PropertyName = "Name 6", PropertyValue = "Value 6", IsValidated = false });
            //t.Add(new ValuesToValidate { PropertyName = "Name 7", PropertyValue = "Value 7", IsValidated = true });
            //t.Add(new ValuesToValidate { PropertyName = "Name 8", PropertyValue = "Value 8", IsValidated = false });
            ObservableCollection<ValuesToValidateModel> tcollect = new ObservableCollection<ValuesToValidateModel>(t);
            return tcollect;
        }

        private async Task SelectAllBtn()
        {
            var newValuesCollection = new ObservableCollection<ValuesToValidateModel>();

            var collectionCopy = ValuesModel;
            foreach (var value in collectionCopy)
            {
                newValuesCollection.Add(new ValuesToValidateModel
                { PropertyName = value.PropertyName,
                    PropertyValue = value.PropertyValue,
                    IsValidated = !value.IsValidated });
            }
            ValuesModel = null;
            ValuesModel = newValuesCollection;
        }

    }
}
