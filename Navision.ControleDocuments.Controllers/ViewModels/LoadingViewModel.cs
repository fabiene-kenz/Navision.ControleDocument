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

namespace Navision.ControleDocuments.Controllers.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        #region Properties
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

        public LoadingViewModel()
        {
            IsLoading = true;
            TextLoading = "Vérification du fichier de configuration...";

            string dbSql = DependencyService.Get<ISQLite>().GetLocalFilePath("db.sqlite3");
            var jsonObject = Utils.DeserializeFromJson<JsonModel>("{\"Version\": \"1.0.0.0\",\"Companies\": [{\"CompanyName\": \"Company0\",\"Url\": \"URL0\", \"Domain\": \"Domain0\"},{\"CompanyName\": \"Company1\",\"Url\": \"URL1\", \"Domain\": \"Domain1\"},{\"CompanyName\": \"Company2\",\"Url\": \"URL2\", \"Domain\": \"Domain2\"}]}");

            Task.Run(() => CreateTablesAsync(dbSql));
            Task.Run(() => PopulateDb(dbSql, jsonObject));

            //deleteTablesDebug(dbSql);

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                IsLoading = false;
                TextLoading = "Chargement effectué";
                return false;
            });
        }

        /// <summary>
        /// Delete tables for debug purpose only
        /// </summary>
        /// <param name="dbSql">Path of the database</param>
        public async void deleteTablesDebug(string dbSql)
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
        public void PopulateDb(string dbSql, JsonModel jsonObject)
        {
            VersionService versionService = new VersionService(dbSql);
            GetClientParamService client = new GetClientParamService(dbSql);

            var versionResult = versionService.GetVersion();
            var clientResult = client.GetClient();
            var query = versionResult.FirstOrDefault();
            if (query != null && query.Version == jsonObject.Version)
            {
                TextLoading = "La base de données est à jour...";
                Task.Delay(1000);
                return;
            }
            if (versionResult.Any() && query.Version != jsonObject.Version)
            {
                versionService.DelVersion(query);
                foreach (var company in clientResult.ToList())
                    client.DelClient(company);
            }
            versionService.AddVersion(new VersionModel { Version = jsonObject.Version });
            foreach (var company in jsonObject.Companies)
                client.AddClient(new Companies { CompanyName = company.CompanyName, Url = company.Url, Domain = company.Domain });
            TextLoading = "Mise à jour de la nouvelle base de données...";
        }

        /// <summary>
        /// Creates VersionModel and Companies Tables if not already created
        /// </summary>
        /// <param name="dbSql">Path of the database</param>
        public async void CreateTablesAsync(string dbSql)
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
    }
}
