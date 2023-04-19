using System;
using System.Net;
using SimpleMedia.Models.Abstract;
namespace SimpleMedia.Models.Pages
{
    public class EditProfile : LoggedPage
    {
        public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
        {
            return Server.RenderFile("Frontend/EditProfile.html", LoginManager.GetUser(request));
        }
    }
}
