using System.Net;
using System;
namespace SimpleMedia.Abstract;
public abstract class Page {

    public virtual string getResponse(HttpListenerRequest request, HttpListenerResponse response){
        switch(request.HttpMethod){
            case "GET":
                return Get(request, response);
            case "POST":
                return Post(request, response);
            default:
                return InvalidedMethod(request, response);
        }
    }

    private string InvalidedMethod(HttpListenerRequest request, HttpListenerResponse response)
    {
        return "Invalid Method";
    }

    public virtual string Get(HttpListenerRequest request, HttpListenerResponse response){
        return "Invalid Method";
    }
    public virtual string Post(HttpListenerRequest request, HttpListenerResponse response){
        return "Invalid Method";
    }
    public virtual string GetUrl(){
        return "/" + this.GetType().Name.Replace(".", "/");
    }
    public virtual bool LoginRequired(){
        return false;
    }
    public virtual bool isCurrentPage(HttpListenerRequest request){
    //  if(LoginRequired() && LoginManager.LoggedIn(request) == false){
     //       return false;
        //}
        string s = request.Url.AbsolutePath.ToLower();
        string t =   this.GetUrl().ToLower();
        return s == t;
    }
}