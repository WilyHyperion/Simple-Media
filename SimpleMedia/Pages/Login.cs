using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SimpleMedia.Abstract;
using System.Text.Json;

namespace SimpleMedia.Pages
{
    public class Login : Page
    {
        public override string Get(HttpListenerRequest request, HttpListenerResponse response){
            return Server.RenderFile("Frontend/Login.html");
        }
        public override string Post(HttpListenerRequest request, HttpListenerResponse response){
            string body = Util.ReadRequestBody(request);
            Dictionary<string, dynamic> data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(body);
            string username = data["username"].ToString();
            string password = data["password"].ToString();
            Console.WriteLine("Login attempt: " + username + " " + password);
            User u = LoginManager.Login(username, password, request, response);
            if (u != null)
            {
                return ""+u.Token;
            }
            return "fail";
        }
    }
}