using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Navision.ControleDocuments.CustomControles
{
    public class CustomWebView : WebView
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create("Uri",
                 typeof(string),
                 typeof(CustomWebView),
                 default(string),
                BindingMode.TwoWay,
                null,
                OnTextPropertyChanged);

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        public static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

            var control = bindable as CustomWebView;
            if (control == null) return;
            control.Uri = newValue.ToString();
        }
    }
}
