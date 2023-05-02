using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SimpleMedia.Models.Abstract;
namespace SimpleMedia.Models.Pages{
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
            String posts = "";
            foreach(Post p in Database.GetObjects<Post>().Where(x => x.Sender.Username == username)){
                posts += "<h2>" +  p.Name+ "</h2> <p>" + p.Text + "</p>";
            }
            if(posts == ""){
                posts = "<h2>No posts yet</h2>";
            }
            Byte[] userRendered = Server.RenderFile("Frontend/Profile.html", new Dictionary<string, string>(){
                {"USER", username},
                {"BIO", LoginManager.GetUser(username).Bio},
                {"POSTS", posts}
            });
            return userRendered;
            }
            return "404 User not found".GetBytes();
        }
    }
}