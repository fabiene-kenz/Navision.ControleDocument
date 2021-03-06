﻿using Navision.ControleDocument.SQL.Models;
using Navision.ControleDocument.SQL.Services;
using Navision.ControleDocuments.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navision.ControleDocuments.Services.Services
{
    public class VersionService : IVersionService
    {
        private readonly string _file;
        public VersionService(string file)
        {
            _file = file;
        }
        public int AddVersion(VersionModel version)
        {
            using (Version<VersionModel> v = new Version<VersionModel>(_file))
            {
                var result = v.AddVersion(version);
                //v.Commit();
                return result; 
            }
        }

        public int DelVersion(VersionModel version)
        {
            using (Version<VersionModel> v = new Version<VersionModel>(_file))
            {
                var result = v.DelVersion(version);
                //v.Commit();
                return result;
            }
        }

        public IQueryable<VersionModel> GetVersion()
        {
            Version<VersionModel> v = new Version<VersionModel>(_file);
            return v.GetVersion();
        }

        public int UpdateVersion(VersionModel version)
        {
            using (Version<VersionModel> v = new Version<VersionModel>(_file))
            {
                var result = v.UpdateVersion(version);
                //v.Commit();
                return result;
            }
        }
    }
}
