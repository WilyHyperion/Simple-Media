namespace SimpleMedia.Models.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Models.Abstract;
public class UserProfile : LoggedPage{
    public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        //return File.ReadAllBytes("/workspaces/Simple-Media/SimpleMedia/Image/selfie.jpg");
        User u = LoginManager.GetUser(request);
        return u.Profile;
    }
    public override byte[] PostLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        User u = LoginManager.GetUser(request);
        return u.Profile;
    }
}