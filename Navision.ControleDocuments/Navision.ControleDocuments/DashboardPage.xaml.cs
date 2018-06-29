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
        private static DashboardViewModel _context;
        public DashboardPage ()
		{
			InitializeComponent ();
            _context= new DashboardViewModel(Navigation, typeof(ViewerDocumentPage));
            BindingContext = _context;
        }
        protected override void OnAppearing()
        {
            _context.RefreshCollection.Execute(null);
            //return
        }
    }
}