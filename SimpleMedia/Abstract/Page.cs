using System.Net;
using System;
namespace SimpleMedia.Abstract;
public abstract class Page {
    public virtual string getResponse(HttpListenerRequest request){
        switch(request.HttpMethod){
            case "GET":
                return Get(request);
            case "POST":
                return Post(request);
            default:
                return InvalidedMethod(request);
        }
    }

    private string InvalidedMethod(HttpListenerRequest request)
    {
        return "Invalid Method";
    }

    public virtual string Get(HttpListenerRequest request){
        return "Invalid Method";
    }
    public virtual string Post(HttpListenerRequest request){
        return "Invalid Method";
    }
    public virtual string GetUrl(){
        return "/" + this.GetType().Name.Replace(".", "/");
    }
    public virtual bool LoginRequired(){
        return false;
    }
    public virtual bool isCurrentPage(HttpListenerRequest request){
        if(LoginRequired() && LoginManager.LoggedIn(request) == false){
            return false;
        }
        string s = request.Url.AbsolutePath.ToLower();
        string t =   this.GetUrl().ToLower();
        Console.WriteLine($"Comparing {s} to {t}");
        return s == t;
    }
}