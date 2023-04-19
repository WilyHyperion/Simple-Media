using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMedia.Models.Abstract;

namespace SimpleMedia.Models.Pages
{
    public class FavIcon : Page
    {
        public override bool isCurrentPage(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.Contains("/favicon.ico");
        }
        public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
        {
            return Server.RenderFile("Static/favicon.ico");
        }
    }
}