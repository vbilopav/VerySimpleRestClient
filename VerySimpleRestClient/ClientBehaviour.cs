using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace VerySimpleRestClient
{
    public static class ClientBehaviour
    {
        private static bool _autoDispose = false;
        private static HttpClient _client = null;
        private static Func<HttpClient> _clientFunc = null;

        public static bool IsAutoDispose => _autoDispose;
        public static HttpClient DefaultClient => _client;
        public static Func<HttpClient> DefaultClientFunc => _clientFunc;

        public static void SetAutoDisposable()
        {
            _autoDispose = true;
        }

        public static void DoNotDispose()
        {
            _autoDispose = false;
        }

        public static void UseThisClient(HttpClient client)
        {
            ClientBehaviour._client = client;
            ClientBehaviour._clientFunc = null;
        }

        public static void UseThisClient(Func<HttpClient> clientFunc)
        {
            ClientBehaviour._client = null;
            ClientBehaviour._clientFunc = clientFunc;
        }
    }
}
