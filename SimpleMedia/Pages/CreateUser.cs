namespace SimpleMedia.Pages;
using SimpleMedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using SimpleMedia.Abstract;
using System.Text.Json;

public class CreateUser : Page
{
    public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
    {
        return File.ReadAllBytes("Frontend/CreateUser.html");
    }
    public override byte[] Post(HttpListenerRequest request, HttpListenerResponse response)
    {
        string body = Util.ReadRequestBody(request);
     Dictionary<string, dynamic>? data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(body);
        if (data == null)
        {
            return "Failed".GetBytes();
        }
        string username = data["username"].ToString();
        string password = data["password"].ToString();
        if (LoginManager.CreateUser(username, password))
        {
            LoginManager.Login(username, password, request, response);
            return Server.RenderFile("Frontend/CreateUserSuccess.html", new Dictionary<string, string>(){
                {"USERNAME", username}
            });
        }
        else{
            return "Failed".GetBytes();
        }
    }
}
