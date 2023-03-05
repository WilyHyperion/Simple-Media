using System.Net;
using SimpleMedia;
using SimpleMedia.Abstract;

public class LoggedHome : LoggedPage
{
    public override string GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        return Server.RenderFile("Frontend/LoggedHome.html", new Dictionary<string, string>(){
            {"USER", LoginManager.RequestUser(request).Username}
        });
    }
}