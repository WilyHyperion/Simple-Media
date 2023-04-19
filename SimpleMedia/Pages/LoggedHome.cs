using System.Net;
using SimpleMedia.Models;
using SimpleMedia.Models.Abstract;

public class LoggedHome : LoggedPage
{
    public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        return Server.RenderFile("Frontend/LoggedHome.html", new Dictionary<string, string>(){
            {"USER", LoginManager.GetUser(request).Username}
        });
    }
}