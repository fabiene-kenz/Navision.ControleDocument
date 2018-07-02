using Navision.ControleDocument.SQL.IServices;
using Navision.ControleDocument.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navision.ControleDocument.SQL.Services
{
    public class Version<T> : Context<VersionModel>, IDisposable, IVersion<T> where T : VersionModel
    {
        private readonly T _repository;

        public Version(string file) : base(file, new VersionModel())
        {
        }
        public void Dispose() => _connect.Close();

        public int AddVersion(T version) => Add(version);

        public int DelVersion(T version) => Del(version);

        public IQueryable<VersionModel> GetVersion() => Get();

        public int UpdateVersion(T version) => Update(version);
        public void Commit() { _connect.Commit(); }
    }
}
