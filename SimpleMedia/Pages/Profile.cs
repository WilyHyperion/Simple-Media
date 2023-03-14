using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMedia.Abstract;

namespace SimpleMedia.Pages
{
    public class Profile : LoggedPage
    {
        public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            User u = LoginManager.GetUser(request);
            d.Add("username", u.Username);
            return Server.RenderFile("Frontend/Profile.html");
        }
    }
}