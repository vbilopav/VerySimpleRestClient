using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace VerySimpleRestClient
{
    public static partial class SimpleClient
    {
        public static async Task<TResult> PutAsync<TResult>(string url, object query = null, Body body = null, HttpClient client = null)
            where TResult : class =>
            (await ClientInternal.RestActionAsync<TResult>(async c =>
            {
                if (body == null) return await c.PutAsync(ClientInternal.BuildUrl(url, query), null);
                using (var content = body.GetContent())
                    return await c.PutAsync(ClientInternal.BuildUrl(url, query), content);
            }, client)).Item1;

        public static async Task<TResult> PutHttpContentAsync<TResult>(string url, object query = null, HttpContent content = null, HttpClient client = null)
            where TResult : class =>
            (await ClientInternal.RestActionAsync<TResult>(async c => await c.PutAsync(ClientInternal.BuildUrl(url, query), content), client)).Item1;

        public static async Task<JObject> PutAsync(string url, object query = null, Body body = null, HttpClient client = null)
            => await PutAsync<JObject>(url, query, body, client);

        public static async Task<JObject> PutHttpContentAsync(string url, object query = null, HttpContent content = null, HttpClient client = null)
            => await PutHttpContentAsync<JObject>(url, query, content, client);
    }

    public static partial class Client
    {

        public static async Task<(TResult, SimpleResponse)> PutAsync<TResult>(string url, object query = null, Body body = null, HttpClient client = null)
            where TResult : class =>
            await ClientInternal.RestActionAsync<TResult>(async c =>
            {
                if (body == null) return await c.PutAsync(ClientInternal.BuildUrl(url, query), null);
                using (var content = body.GetContent())
                    return await c.PutAsync(ClientInternal.BuildUrl(url, query), content);
            }, client);

        public static async Task<(TResult, SimpleResponse)> PutHttpContentAsync<TResult>(string url, object query = null, HttpContent content = null, HttpClient client = null)
            where TResult : class =>
            await ClientInternal.RestActionAsync<TResult>(async c => await c.PutAsync(ClientInternal.BuildUrl(url, query), content), client);

        public static async Task<(JObject, SimpleResponse)> PutAsync(string url, object query = null, Body body = null, HttpClient client = null)
            => await PutAsync<JObject>(url, query, body, client);
        
        public static async Task<(JObject, SimpleResponse)> PutHttpContentAsync(string url, object query = null, HttpContent content = null, HttpClient client = null)
            => await PutHttpContentAsync<JObject>(url, query, content, client);
    }
}
