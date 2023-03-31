using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SimpleMedia.Abstract;
namespace SimpleMedia.Pages{
    public class Profile : Page{
        public override bool isCurrentPage(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.StartsWith("/profile/");
        }
        public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
        {
            if(LoginManager.LoggedIn(request)){
                if(LoginManager.GetUser(request).Username == request.Url.AbsolutePath.Split('/').Last()){
                    return Server.Redirect("/editprofile");
                }
            }
            string username = request.Url.AbsolutePath.Split('/').Last();
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