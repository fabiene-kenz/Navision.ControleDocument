using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.ValuesToValidateModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class ViewerDocumentViewModel : BaseViewModel
    {
        #region Properties
        private DocModel _doc = new DocModel();
        public DocModel Doc
        {
            get { return _doc; }
            set { _doc = value; }
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
            SelectAllIsToggled = false;
            ValuesModel = GetValuesAsync();
        }

        private ObservableCollection<ValuesToValidateModel> GetValuesAsync()
        {
            List<ValuesToValidateModel> t = new List<ValuesToValidateModel>();
            t.Add(new ValuesToValidateModel { PropertyName = "Name 0", PropertyValue = "Value 0", IsValidated = false });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 1", PropertyValue = "Value 1", IsValidated = true });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 2", PropertyValue = "Value 2", IsValidated = false });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 3", PropertyValue = "Value 3", IsValidated = true });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 4", PropertyValue = "Value 4", IsValidated = false });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 5", PropertyValue = "Value 5", IsValidated = true });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 6", PropertyValue = "Value 6", IsValidated = false });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 7", PropertyValue = "Value 7", IsValidated = true });
            t.Add(new ValuesToValidateModel { PropertyName = "Name 8", PropertyValue = "Value 8", IsValidated = false });
            ObservableCollection<ValuesToValidateModel> tcollect = new ObservableCollection<ValuesToValidateModel>(t);
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

    }
}
