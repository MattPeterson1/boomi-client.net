using System;
using System.Net;

namespace Dell.Boomi.Client
{
    public class BoomiException : Exception
    {
        public BoomiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }
    }
}
