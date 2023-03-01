using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMedia.Abstract;

namespace SimpleMedia.Pages
{
    public class Login : Page
    {
        public override string Get(HttpListenerRequest request){
            return Server.RenderFile("Frontend/Login.html");
        }
        public override string Post(HttpListenerRequest request){
            string username = request.QueryString["username"];
            string password = request.QueryString["password"];
            Console.WriteLine($"Username: {username} Password: {password}");
            return "Invalid login";
     //       if(username == "admin" && password == "admin"){
  //              LoginManager.Login(request);
   //             return "Logged in";
  //          }
    //        return "Invalid login";
        }
    }
}