using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMedia.Abstract;

namespace SimpleMedia.Pages
{
    public class Home : Page
    {
        public override string Get(HttpListenerRequest request, HttpListenerResponse response)
        {
            return Server.RenderFile("Frontend/Home.html");
        }
    }
}