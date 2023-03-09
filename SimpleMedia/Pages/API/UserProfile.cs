namespace SimpleMedia.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class UserProfile : LoggedPage{
    public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        //return File.ReadAllBytes("/workspaces/Simple-Media/SimpleMedia/Image/selfie.jpg");
        User u = LoginManager.GetUser(request);
        Console.WriteLine("User Profile: " + u.Profile.Length);
        return u.Profile;
    }
    public override byte[] PostLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        User u = LoginManager.GetUser(request);
        return u.Profile;
    }
}