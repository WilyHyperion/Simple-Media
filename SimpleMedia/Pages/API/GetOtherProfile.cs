namespace SimpleMedia.Models.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Models.Abstract;
public class GetOtherProfile : Page{
    public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
    {
        string? username = request.QueryString["username"];
        if (username == null)
        {
            return new byte[0];
        }
        User u = LoginManager.GetUser(username);
        if (u == null)
        {
            return new byte[0];
        }
        response.ContentType = "image/jpeg";
        return u.Profile;
    }
    public override byte[] Post(HttpListenerRequest request, HttpListenerResponse response)
    {
        return new byte[0];
    }
}