using Navision.ControleDocuments.CustomControles;
using Navision.ControleDocuments.UWP.CustomControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace Navision.ControleDocuments.UWP.CustomControl
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        Assembly _assembly;
        Stream _imageStream;
        StreamReader _textStreamReader;
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _assembly = typeof(CustomWebViewRenderer).Assembly;
                var retour = Application.Current.Resources.GetEnumerator();
                _imageStream = _assembly.GetManifestResourceStream("Navision.ControleDocuments.UWP.Assets.Content.Enterprise-Application-Patterns-using-XamarinForms.pdf");
                //_textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("MyNameSpace.MyTextFile.txt"));
                //StreamReader reader = new StreamReader("https://www.impots.gouv.fr/portail/files/formulaires/2042/2018/2042_2338.pdf");
                //var _htmlString = reader.ReadToEnd();
                //var retour = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var customWebView = Element as CustomWebView;
                //var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                //var dbFile = Path.Combine(@"/0c900d1a-3f8e-4b9b-9f1e-6bd3ec689cc5_nh7s0b45jarrj/", @"FilesPdf/"+ customWebView.Uri);
                //HtmlWebViewSource html = new HtmlWebViewSource();
                //html.Html = _htmlString;

                Control.Source = new Uri(string.Format("ms-appx-web:///Assets/pdfjs/web/viewer.html?file={0}", string.Format("ms-appx-web:///Assets/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
                //Control.Source = new Uri(string.Format("ms-appx-web:///Assets/pdfjs/web/viewer.html?file={0}",string.Format("ms-appx-web:///Assets/Content/{0}", customWebView.Uri)));
            }
        }
    }
}
