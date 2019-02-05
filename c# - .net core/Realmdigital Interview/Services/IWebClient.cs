using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Realmdigital_Interview.Services
{
    public interface IWebClient : IDisposable
    {
        WebHeaderCollection Headers {get; set;}

        string UploadString(string address, string method, string data);
    }    
}
