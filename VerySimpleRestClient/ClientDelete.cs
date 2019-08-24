using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace VerySimpleRestClient
{
    public static partial class SimpleClient
    {
        public static async Task<TResult> DeleteAsync<TResult>(string url, Query query = null, HttpClient client = null)
            where TResult : class =>
            (await ClientInternal.RestActionAsync<TResult>(async c => await c.DeleteAsync(ClientInternal.BuildUrl(url, query)), client)).Item1;

        public static async Task<JObject> DeleteAsync(string url, Query query = null, HttpClient client = null)
            => await DeleteAsync<JObject>(url, query, client);
    }

    public static partial class Client
    {
        public static async Task<(TResult, SimpleResponse)> DeleteAsync<TResult>(string url, Query query = null, HttpClient client = null)
            where TResult : class =>
            await ClientInternal.RestActionAsync<TResult>(async c => await c.DeleteAsync(ClientInternal.BuildUrl(url, query)), client);

        public static async Task<(JObject, SimpleResponse)> DeleteAsync(string url, Query query = null, HttpClient client = null)
            => await DeleteAsync<JObject>(url, query, client);
    }
}
