using Navision.ControleDocuments.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private DateTime _dateDoc = DateTime.Now;
        public DateTime DateDoc
        {
            get { return _dateDoc; }
            set
            {
                _dateDoc = value;
                OnPropertyChanged("DateDoc");
            }
        }

        public DashboardViewModel()
        { }
    }
}
