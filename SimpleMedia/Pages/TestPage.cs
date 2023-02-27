using SimpleMedia.Abstract;
using System.Net;
namespace SimpleMedia;
public class PageOne : Page {
    public override string getResponse(HttpListenerRequest request){
        return "Ok";
    }
}