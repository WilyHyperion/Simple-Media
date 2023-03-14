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
        MemoryStream ms = new MemoryStream();
        request.InputStream.CopyTo(ms);
        byte[] data = ms.ToArray();
        //make sure its an image file
        if( !Util.isVaildImage(data)){
            return "Invalid image".GetBytes();
        }
        u.Profile = data;
        return "Success".GetBytes();
    }
}