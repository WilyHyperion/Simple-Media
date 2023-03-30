using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SimpleMedia.Abstract;
namespace SimpleMedia.Pages{
    public class Profile : LoggedPage{
        public override bool isCurrentPage(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.StartsWith("/profile/");
        }
        public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
        {
            string username = request.Url.AbsolutePath.Split('/').Last();
            Console.WriteLine("Username: " + username);
            if(LoginManager.GetUser(username) != null){
                username = LoginManager.GetUser(username).Username;

            return Server.RenderFile("Frontend/Profile.html", new Dictionary<string, string>(){
                {"USER", username},
                {"BIO", LoginManager.GetUser(username).Bio}
            });
            }
            return "404".GetBytes();
        }
    }
}