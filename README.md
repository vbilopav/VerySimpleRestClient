# VerySimpleRestClient

.NET Standard Very Simple REST client ~ My `HttpClient` abstraction

## What is it?

Library for sending REST requests for .NET Standard based on `HttpClient`

## But why? / Motivation

Because almost all .NET projects I've seen have some kind of `HttpClient` abstraction. 

Because you probably just want to to send some REST/HTTP requests and get the response - not juggle multiple 
disposlable objects and multiple serializations and deserializatons - you just one want one line of code that 
sends the request and read the response.

`HttpClient` is pretty big and complex interface, there with many pitfalls and more often then not - mistakes and bugshappen.

So, this is mine solution that I'm using in various projects. I really don't wan't to write `HttpClient` helpers all over again, so hence this library. 

## Quickstart

... is only way to start.

Just install `VBSoftware.VerySimpleRestClient` NuGet package and add using directive `using VerySimpleRestClient;` or reference static classes directly. There are only two of them:

- `SimpleClient` to get the response (from get or post or put or delete).
- `Client` to get the response as well some ordinary http data like status code and stuff ...

No injection, no configuration, no nothing...

## How can I use it?

Examples:

Send simple GET request:
```csharp
var result = await SimpleClient.GetAsync("http://...");
// result is dynamic JObject
Assert.Equal("value1", result["key1"]);
```

Send simple GET request and serialize JSON to a class:
```csharp
var result = await SimpleClient.GetAsync<MyResponse>("http://...");
// result is MyResponse object
Assert.Equal("value1", result.Key1);
```

Send simple GET request and fetch usual additional data, such as status code, content type, etc...
```csharp
var (result, response) = await Client.GetAsync("http://...");
Assert.Equal(HttpStatusCode.OK, response.StatusCode);
Assert.Equal("text/plain; charset=utf-8", response.ContentType);
// ... etc, see source code for SimpleResponse: https://github.com/vbilopav/VerySimpleRestClient/blob/master/VerySimpleRestClient/SimpleResponse.cs
```

Inlcude query string deserialized from object instance, anonymous object or dictionary:
```csharp
var result = await SimpleClient.GetAsync("http://...", new Query(new { key1 = "value1" }));
```

Send a POST request with JSON body serialized from object instance, anonymous object or dictionary: 
```csharp
var result = await SimpleClient.PostAsync("http://...", body: new Json(new
{
    key1 = "value1", /* ... */
}));
```

Send a POST request with multipart form body serialized from object instance, anonymous object or dictionary: 
```csharp
var result = await SimpleClient.PostAsync("http://...", body: new Form(new
{
    key1 = "value1", /* ... */
}));
```

Send a POST request with body in plain text format:
```csharp
var result = await SimpleClient.PostAsync("http://...", body: new TextPlain("The quick brown fox..."));
```

Send a POST request with custom `HTTPContent` content in body:
```csharp
using (var content = new HttpContent()) //replace HttpContent with non-abstract version
{
	var result = await SimpleClient.PostHttpContentAsync("http://...", body: content);
}
```

Reuse same `HttpClient` for multiple requests:
```csharp
using (var client = new HttpClient()) //replace HttpContent with non-abstract version
{
	//
	// configure client additionally if neccessary, add Auth headers, etc ...
	//
	var result1 = await SimpleClient.GetAsync("http://...", client: client);
	var result2 = await SimpleClient.PostAsync("http://...", client: client);
	var result3 = await SimpleClient.PutAsync("http://...", client: client);
	var result4 = await SimpleClient.DeleteAsync("http://...", client: client);
}
```


## Unit tests

There are none.

However, this library is already used in multiple projects and in multiple unit tests already so it is very well tested 
and covered with other projects unit tests. Such as this for example: https://github.com/vbilopav/postgrest.net/tree/master/UnitTests

There might be transfer of unit tests from other projects if need to change this library riese, which I highly doubt it will.

## Licence

Copyright (c) Vedran Bilopavlović.
This source code is licensed under the [MIT license](https://github.com/vbilopav/VerySimpleRestClient/blob/master/LICENSE).
