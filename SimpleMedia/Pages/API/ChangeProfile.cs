namespace SimpleMedia.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class ChangeProfile : LoggedPage{
    public override byte[] PostLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        Console.WriteLine("Change Profile");
        User u = LoginManager.GetUser(request);
        if (u == null)
        {
            Console.WriteLine("User not found");
            return new byte[0];
        }
        Console.WriteLine("User found");
        MemoryStream ms = new MemoryStream();
        request.InputStream.CopyTo(ms);
        byte[] data = ms.ToArray();
        u.Profile = data;
        Console.WriteLine("Profile Changed");
        return "Success".GetBytes();
    }
}