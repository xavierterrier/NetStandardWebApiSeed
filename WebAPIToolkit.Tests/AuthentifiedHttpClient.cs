using System;
using System.Net.Http;
using Microsoft.Owin.Testing;

namespace WebAPIToolkit.Tests
{
    public class AuthentifiedHttpClient : HttpClient
    {
        private readonly TestServer _server;
        bool _disposed = false;

        public AuthentifiedHttpClient() : this(TestServer.Create<Startup>())
        {
            
        }

        public AuthentifiedHttpClient(TestServer server) : base(server.Handler)
        {
            _server = server;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _server.Dispose();
            }

            _disposed = true;
            base.Dispose(disposing);
        }
    }
}
