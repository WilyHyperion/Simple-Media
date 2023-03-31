namespace SimpleMedia.Pages.Api;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class ChangeBio : LoggedPage{
    public override byte[] PostLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        User u = LoginManager.GetUser(request);
        if (u == null)
        {
            return new byte[0];
        }
        MemoryStream ms = new MemoryStream();
        request.InputStream.CopyTo(ms);
        u.Bio = Util.EscapeString(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
        return "Success".GetBytes();
    }
}