using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace VerySimpleRestClient
{
    public abstract class Body
    {
        public abstract HttpContent GetContent();
    }

    public class Json : Body
    {
        private readonly string content; 
        public override HttpContent GetContent() => new StringContent(content, Encoding.UTF8, "application/json");

        public Json(object values)
        {
            content = JsonConvert.SerializeObject(values, Formatting.None);
        }

        public Json(IDictionary<string, object> values)
        {
            content = JsonConvert.SerializeObject(values, Formatting.None);
        }
    }

    public class TextPlain : Body
    {
        private readonly string content;
        public override HttpContent GetContent() => new StringContent(content, Encoding.UTF8, "text/plain");

        public TextPlain(string value)
        {
            content = value;
        }
    }

    public class Form : Body
    {
        private readonly MultipartFormDataContent content;
        public override HttpContent GetContent() => content;

        public Form(object values)
        {
            content = new MultipartFormDataContent();
            foreach (var info in values.GetType().GetProperties())
            {
                content.Add(new StringContent(Convert.ToString(info.GetValue(values, null))), info.Name);
            }
        }

        public Form(IDictionary<string, object> values)
        {
            content = new MultipartFormDataContent();
            foreach (var info in values)
            {
                content.Add(new StringContent(Convert.ToString(info.Value)), info.Key);
            }
        }
    }
}
