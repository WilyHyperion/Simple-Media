namespace SimpleMedia.Abstract {
    using System.Net;
    using System;
    public abstract class LoggedPage : Page {
        public override string Get(HttpListenerRequest request, HttpListenerResponse response) {
            if (LoginManager.LoggedIn(request)) {
                return GetLogged(request, response);
            } else {
                return GetNotLogged(request, response);
            }
        }
        public override string Post(HttpListenerRequest request, HttpListenerResponse response) {
            if (LoginManager.LoggedIn(request)) {
                return PostLogged(request, response);
            } else {
                return PostNotLogged(request, response);
            }
        }
        public virtual string GetLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return base.Get(request, response);
        }
        public virtual string GetNotLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return Server.Redirect("/Login");
        }
        public virtual string PostLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return "Invalid Method";
        }
        public virtual string PostNotLogged(HttpListenerRequest request, HttpListenerResponse response) {
            return "Invalid Method";
        }
    }
}