using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleMedia.Abstract;
using SimpleMedia;
using System.Net;

namespace SimpleMedia.Pages
{
    public class StaticFiles : Page
    {
        public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
        {
            string url = request.Url.AbsolutePath;
            return Server.ReadFile(url.Substring(1));
        }
        public override bool isCurrentPage(HttpListenerRequest request)
        {
            string url = request.Url.AbsolutePath;
            Console.WriteLine("URL: " + url);
            if (url.StartsWith("/static/") && Server.FileExists(url.Substring(1)))
            {
                return true;
            }
            return false;
        }
    }
}