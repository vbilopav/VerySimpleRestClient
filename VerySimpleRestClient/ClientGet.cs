using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VerySimpleRestClient
{
    public static partial class SimpleClient
    {
        public static async Task<TResult> GetAsync<TResult>(string url, object query = null, HttpClient client = null)
            where TResult : class =>
            (await ClientInternal.RestActionAsync<TResult>(async c => await c.GetAsync(ClientInternal.BuildUrl(url, query)), client)).Item1;

        public static async Task<JObject> GetAsync(string url, object query = null, HttpClient client = null)
            => await GetAsync<JObject>(url, query, client);
    }

    public static partial class Client
    {
        public static async Task<(TResult, SimpleResponse)> GetAsync<TResult>(string url, object query = null, HttpClient client = null)
            where TResult : class =>
            await ClientInternal.RestActionAsync<TResult>(async c => await c.GetAsync(ClientInternal.BuildUrl(url, query), HttpCompletionOption.ResponseHeadersRead), client);

        public static async Task<(JObject, SimpleResponse)> GetAsync(string url, object query = null, HttpClient client = null)
            => await GetAsync<JObject>(url, query, client);
    }
}
