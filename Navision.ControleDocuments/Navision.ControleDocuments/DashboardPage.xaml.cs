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

        /// <summary>
        /// Filter the documents by their names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterByName(object sender, TextChangedEventArgs e)
        {
            var vm = BindingContext as DashboardViewModel;

            DocumentsListView.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                DocumentsListView.ItemsSource = vm.DocsModel;
            else
                DocumentsListView.ItemsSource = vm.DocsModel.Where(i => i.DocName.Contains(e.NewTextValue.ToLower()));
            DocumentsListView.EndRefresh();
        }
    }
}