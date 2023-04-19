using System.Net;
using SimpleMedia.Models.Abstract;

namespace SimpleMedia.Models.Pages;
public class Logout : Page
{
    public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (LoginManager.LoggedIn(request))
        {
            LoginManager.Logout(response);
        }
        return Server.Redirect("/Login");
    }
    public override byte[] Post(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (LoginManager.LoggedIn(request))
        {
            LoginManager.Logout(response);
        }
        return Server.Redirect("/Login");
    }
}