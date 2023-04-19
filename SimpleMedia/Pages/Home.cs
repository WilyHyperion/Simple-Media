using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMedia.Models.Abstract;

namespace SimpleMedia.Models.Pages
{
    public class Home : Page
    {
        public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
        {
            return Server.RenderFile("Frontend/Home.html");
        }
    }
}