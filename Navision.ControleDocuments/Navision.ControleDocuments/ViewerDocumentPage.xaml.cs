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
		public ViewerDocumentPage ()
		{
			InitializeComponent ();
            Panel.TranslationY = 1000;

            ShowPanelImg.IsVisible = true;
            HidePanelImg.IsVisible = false;
        }

        async void ShowPanel(object sender, System.EventArgs e)
        {
            ShowPanelImg.IsVisible = !ShowPanelImg.IsVisible;
            HidePanelImg.IsVisible = !HidePanelImg.IsVisible;
            await Panel.TranslateTo(0, 0, 500, Easing.CubicIn);
        }

        async void HidePanel(object sender, System.EventArgs e)
        {
            await Panel.TranslateTo(0, MainContent.Height, 500, Easing.CubicOut);
            ShowPanelImg.IsVisible = !ShowPanelImg.IsVisible;
            HidePanelImg.IsVisible = !HidePanelImg.IsVisible;
        }
    }
}