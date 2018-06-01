using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Navision.ControleDocuments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewerDocumentPage : ContentPage
	{
        double x, y;
        public ViewerDocumentPage ()
		{
			InitializeComponent ();
		}
        
    }
}