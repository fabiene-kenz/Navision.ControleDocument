using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Navision.ControleDocuments.CustomControles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomContainerView : ContentView
    {
        public static readonly BindableProperty DocumentNameProperty = BindableProperty.Create(nameof(DocumentName), typeof(string), typeof(CustomContainerView), null, BindingMode.TwoWay, null, OnTextPropertyChanged);
        public static readonly BindableProperty DocumentDateProperty = BindableProperty.Create(nameof(DocumentDate), typeof(DateTime?), typeof(CustomContainerView), null, propertyChanged: OnDatetimePropertyChanged);
        public static readonly BindableProperty DocumentSatutProperty = BindableProperty.Create(nameof(DocumentSatut), typeof(Boolean?), typeof(CustomContainerView), null, BindingMode.TwoWay, null, OnStatutPropertyChanged);

        public CustomContainerView()
        {
            InitializeComponent();
            DocumentDateLabel.PropertyChanged += EntryPropertyChanged;
        }

        public static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

            var control = bindable as CustomContainerView;
            if (control == null) return;
            if (newValue != null)
                control.DocumentName = newValue.ToString();
        }
        public static void OnStatutPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomContainerView;
            if (control == null) return;
            if (newValue == null) control.DocumentSatut = null;
            else
            {
                control.DocumentSatut = Convert.ToBoolean(newValue.ToString());
            }
        }
        public static void OnDatetimePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomContainerView;
            if (control == null) return;
            if (newValue == null) control.DocumentDate = null;
            else
            {
                control.DocumentDate = Convert.ToDateTime(newValue.ToString());
            }

        }
        void EntryPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Entry.TextProperty.PropertyName)
            {

            }
        }

        public string DocumentName
        {
            get
            {
                return (string)GetValue(DocumentNameProperty);
            }
            set
            {
                SetValue(DocumentNameProperty, value);
            }
        }
        public DateTime? DocumentDate
        {
            get
            {
                return (DateTime?)GetValue(DocumentDateProperty);
            }
            set
            {
                SetValue(DocumentDateProperty, value);
            }
        }
        public Boolean? DocumentSatut
        {
            get
            {
                return (Boolean?)GetValue(DocumentSatutProperty);
            }
            set
            {
                SetValue(DocumentSatutProperty, value);
            }
        }
    }
}