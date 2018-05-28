﻿using Navision.ControleDocuments.Controllers.ViewModels;
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
	public partial class LoadingPage : ContentPage
	{
		public LoadingPage ()
		{
			InitializeComponent();
            BindingContext = new LoadingViewModel(typeof(MainPage),Navigation);
        }
	}
}