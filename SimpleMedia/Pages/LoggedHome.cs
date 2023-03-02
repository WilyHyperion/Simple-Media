using System.Net;
using SimpleMedia;
using SimpleMedia.Abstract;

public class LoggedHome : Page
{
    public override string Get(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (!LoginManager.LoggedIn(request))
        {
            return Server.Redirect("/Login",request, response);
            
        }
        Console.WriteLine("Logged in");
        return Server.RenderFile("Frontend/LoggedHome.html");
    }
}