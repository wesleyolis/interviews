using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realmdigital_Interview.Services;
using System.Net;

using _WebClient = System.Net.WebClient;

namespace Realmdigital_Interview.Services
{
    class WebClient : _WebClient, IWebClient
    {
        new public WebHeaderCollection Headers
        {
            get { return base.Headers; }
            set { base.Headers = value; }
        }

        new public string UploadString(string address, string method, string data)
        {
            return base.UploadString(address, method, data);
        }
    }    
}
