namespace SimpleMedia.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class GetOtherProfile : Page{
    public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
    {
        string username = request.QueryString["username"];
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