using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocuments.Controllers.Base;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.DocsModel;
using Navision.ControleDocuments.Models.UserModels;
using Navision.ControleDocuments.Services.IServices;
using Navision.ControleDocuments.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class ViewerDocumentViewModel : BaseViewModel
    {
        private readonly IStreamService _streamservice;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private DocModel _doc = new DocModel();
        public DocModel Doc
        {
            get { return _doc; }
            set
            {
                if (_doc != value)
                {
                    _doc = value;
                    Task.Run(async () => Images = await GetPdf(value));
                }
                OnPropertyChanged("Doc");
            }
        }

        private List<PdfModel> _images = new List<PdfModel>();
        public List<PdfModel> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged("Images");
            }
        }
        private string _docPath;
        public string DocPath
        {
            get { return _docPath; }
            set
            {
                _docPath = value;
                OnPropertyChanged("DocPath");
            }

        }

        public ViewerDocumentViewModel()
        {
            //DocPath = "6005.pdf";
            //DocPath = "Enterprise-Application-Patterns-using-XamarinForms.pdf";
            ////string fileName = DependencyService.Get<ILocalStorageFolder>().GetLocalFilePath("TEST.pdf");
            UserModel user = Utils.DeserializeFromJson<UserModel>(Application.Current.Properties["UserData"].ToString());
            _streamservice = new StreamService(user.Token);
            IsLoading = true;
        }
       
        private  async Task<List<PdfModel>> GetPdf(DocModel doc)
        {
            List<PdfModel> listPdfModel = await _streamservice.GetPdfFile(doc);
            IsLoading = false;
            return listPdfModel;
        }

    }
}
