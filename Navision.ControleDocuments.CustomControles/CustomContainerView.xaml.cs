using System;
using System.Collections.Generic;
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
        public static readonly BindableProperty DocumentNameProperty = BindableProperty.Create(nameof(DocumentName), typeof(string), typeof(CustomContainerView));
        public static readonly BindableProperty DocumentDateProperty = BindableProperty.Create(nameof(DocumentDate), typeof(DateTime), typeof(CustomContainerView));
        public static readonly BindableProperty DocumentSatutProperty = BindableProperty.Create(nameof(DocumentSatut), typeof(Boolean), typeof(CustomContainerView));

        public CustomContainerView()
        {
            InitializeComponent();
            this.BindingContext = this;
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
        public DateTime DocumentDate
        {
            get
            {
                return (DateTime)GetValue(DocumentDateProperty);
            }
            set
            {
                SetValue(DocumentDateProperty, value);
            }
        }
        public Boolean DocumentSatut
        {
            get
            {
                return (Boolean)GetValue(DocumentSatutProperty);
            }
            set
            {
                SetValue(DocumentSatutProperty, value);
            }
        }
    }
}