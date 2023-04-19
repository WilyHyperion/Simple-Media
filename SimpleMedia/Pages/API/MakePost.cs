namespace SimpleMedia.Models.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using SimpleMedia.Models.Abstract;
public class MakePost : LoggedPage{
    public override byte[] PostLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        User u = LoginManager.GetUser(request);
        if (u == null)
        {
            response.StatusCode = 401;
            return new byte[0];
        }
        string body = Util.ReadRequestBody(request);
        Dictionary<string, dynamic> data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(body);
        string text = data["text"].ToString();
        string name = data["name"].ToString();

        Post p = new Post(text, name, u);
        Database.AddObject(p);
        Console.WriteLine("Added post: " + p.Name + "with id" + p.Id);
        var ResponseData = new Dictionary<string, dynamic>(){
            {"id", p.Id}

        };

        
        return JsonSerializer.SerializeToUtf8Bytes(ResponseData);
    }
}