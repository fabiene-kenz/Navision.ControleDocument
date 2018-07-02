using Navision.ControleDocument.SQL.IServices;
using Navision.ControleDocument.SQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Navision.ControleDocument.SQL.Services
{
    public class ClientParam<T> : Context<Companies>, IDisposable, IClientParam<T> where T : Companies
    {
        private readonly T _repository;

        public ClientParam(string file) : base(file, new Companies())
        {
        }
        public void Dispose() => _connect.Close();

        public int AddNewCompany(T company)=> Add(company); 

        public int DelCompany(T company) => Del(company);

        public IQueryable<Companies> GetClientParams() => Get();

        public int UpdateCompany(T company) => Update(company);
        public void Commit() { _connect.Commit(); }
    }
}
