using Navision.ControleDocuments.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using Navision.ControleDocument.DependenciesServices.IServices;
using Navision.ControleDocument.SQL.Models;
using Navision.ControleDocuments.Controllers.Helpers;
using Navision.ControleDocuments.Models.JsonModel;
using Navision.ControleDocument.SQL.Services;
using System.Linq;
using Navision.ControleDocuments.Services.Services;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using Navision.ControleDocuments.Services.IServices;

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        #region Properties
        private readonly IJsonService _jsonService;
        private readonly IVersionService _versionService;
        private readonly IGetClientParamService _getClientParamService;
        private readonly IPageService _pageService;
        private readonly INavigation _navigation;
        private Type _mainpage;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged("IsLoading"); }
        }

        private string _textLoading;

        public string TextLoading
        {
            get { return _textLoading; }
            set { _textLoading = value; OnPropertyChanged("TextLoading"); }
        }
        #endregion

        public LoadingViewModel(Type mainpage, INavigation navigation)
        {
            _mainpage = mainpage;
            string dbSql = DependencyService.Get<ISQLite>().GetLocalFilePath("db.sqlite3");
            _jsonService = new JsonService();
            
            _versionService = new VersionService(dbSql);
            _getClientParamService = new GetClientParamService(dbSql);

            IsLoading = true;
            TextLoading = "Vérification du fichier de configuration...";
            // Get config file
            var jsonObject = Utils.DeserializeFromJson<JsonModel>(_jsonService.GetJson());

            //var jsonObject = Utils.DeserializeFromJson<JsonModel>("{\"Version\": \"1.0.0.0\",\"Companies\": [{\"CompanyName\": \"e-Kenz\",\"Url\": \"http://navapi.saas.e-kenz.com\", \"Domain\": \"SAAS\"},{\"CompanyName\": \"Company1\",\"Url\": \"URL1\", \"Domain\": \"Domain1\"},{\"CompanyName\": \"Company2\",\"Url\": \"URL2\", \"Domain\": \"Domain2\"}]}");

            Task.Run(async () => await CreateTablesAsync(dbSql));
            Task.Run(async () => await PopulateDb(dbSql, jsonObject));

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                Device.BeginInvokeOnMainThread(async () => await SwitchPage());
                return false;
            });


        }

        /// <summary>
        /// Delete tables for debug purpose only
        /// </summary>
        /// <param name="dbSql">Path of the database</param>
        public async void DeleteTablesDebug(string dbSql)
        {
            try
            {
                var connection = new SQLiteAsyncConnection(dbSql);
                await connection.DropTableAsync<VersionModel>();
                await connection.DropTableAsync<Companies>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Populate dabatase if empty and update it if the version is not the latest. Leave the function if already up to date
        /// </summary>
        /// <param name="dbSql">Path of the database</param>
        /// <param name="jsonObject">Configuration file as object</param>
        public async Task PopulateDb(string dbSql, JsonModel jsonObject)
        {
            var versionService = new VersionService(dbSql);
            // One version in the Table
            var versionResult = versionService.GetVersion().FirstOrDefault();
            //var query = versionResult.FirstOrDefault();
            var clientResult = _getClientParamService.GetClient();
            // Check Version
            if (versionResult != null && versionResult.Version == jsonObject.Version)
            {
                TextLoading = "La base de données est à jour...";
                return;
            }

            if (versionResult != null && versionResult.Version != jsonObject.Version)
            {
                // Clean DB
                _versionService.DelVersion(versionResult);
                foreach (var company in clientResult)
                    _getClientParamService.DelClient(company);
            }
           
            // Add new Datas
            _versionService.AddVersion(new VersionModel { Version = jsonObject.Version });
            foreach (var company in jsonObject.Companies)
                _getClientParamService.AddClient(new Companies { CompanyName = company.CompanyName, Url = company.Url, Domain = company.Domain });
            TextLoading = "Mise à jour de la nouvelle base de données...";


            IsLoading = false;
            TextLoading = "Chargement effectué";

        }

        /// <summary>
        /// Creates VersionModel and Companies Tables if not already created
        /// </summary>
        /// <param name="dbSql">Path of the database</param>
        public async Task CreateTablesAsync(string dbSql)
        {
            try
            {
                var connection = new SQLiteAsyncConnection(dbSql);
                await connection.CreateTableAsync<VersionModel>();
                await connection.CreateTableAsync<Companies>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task SwitchPage()
        {
            var page = (Page)Activator.CreateInstance(_mainpage);
            // Main become a NavigationPage
            Application.Current.MainPage = new NavigationPage(page);
        }
    }
}
