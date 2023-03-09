namespace SimpleMedia.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class ChangeProfile : LoggedPage{
    public override byte[] PostLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        User u = LoginManager.GetUser(request);
        if (u == null)
        {
            return new byte[0];
        }
        byte[] data = new byte[request.ContentLength64];
        request.InputStream.Read(data, 0, data.Length);
        u.Profile = data;
        return "Success".GetBytes();
    }
}