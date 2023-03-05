using System.Net;
using SimpleMedia.Abstract;

namespace SimpleMedia.Pages;
public class Logout : Page
{
    public override string Get(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (LoginManager.LoggedIn(request))
        {
            LoginManager.Logout(response);
        }
        return Server.Redirect("/Login");
    }
    public override string Post(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (LoginManager.LoggedIn(request))
        {
            LoginManager.Logout(response);
        }
        return Server.Redirect("/Login");
    }
}