using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;

namespace Navision.ControleDocuments.Services.Services
{
    public class ConnectService
    {
        //http://localhost:61798/
        //https://navapi.saas.e-kenz.com/Documents/GetDocuments
#if DEBUG
        //public string Uribase { get { return "http://localhost:61798/"; } }
        public string Uribase { get { return "http://navapi.saas.e-kenz.com"; } }
#else
        public string Uribase { get { return "http://localhost:61798/"; } }
#endif
        public HttpClient httpClient { get; private set; }
        private readonly string _userAgent = "Navision";

        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public ConnectService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", _userAgent);
        }
        public ConnectService(string token)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("User-Agent", _userAgent);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
    }
}
