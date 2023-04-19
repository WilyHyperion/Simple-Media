using System.Net;
using SimpleMedia.Models;
using SimpleMedia.Models.Abstract;
public class CreatePost : LoggedPage{
    public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        return Server.RenderFile("Frontend/CreatePost.html", LoginManager.GetUser(request));
    }
}