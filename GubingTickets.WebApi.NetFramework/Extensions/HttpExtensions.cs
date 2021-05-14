using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace GubingTickets.WebApi.NetFramework.Extensions
{
    public static class HttpExtensions
    {
        public static HttpResponseMessage GetHttpResponseMessage<TResponse>(this TResponse response, HttpRequestMessage httpRequestMessage, HttpStatusCode httpResponseCode)
        {
            return httpRequestMessage.CreateResponse(httpResponseCode, response);
        }
    }
}