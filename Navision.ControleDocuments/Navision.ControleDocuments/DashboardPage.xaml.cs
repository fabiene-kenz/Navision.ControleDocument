using Navision.ControleDocuments.Controllers.ViewModels;
using Navision.ControleDocuments.Services.Services;
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
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();
            BindingContext = new DashboardViewModel(Navigation,typeof(ViewerDocumentPage));
		}
	}
}