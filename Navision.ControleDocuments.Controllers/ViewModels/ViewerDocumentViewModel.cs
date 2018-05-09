using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Models.DocsModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class ViewerDocumentViewModel : BaseViewModel
    {
        private DocModel _doc = new DocModel();
        public DocModel Doc
        {
            get { return _doc; }
            set { _doc = value; }
        }

        public ViewerDocumentViewModel()
        {

        }
    }
}
