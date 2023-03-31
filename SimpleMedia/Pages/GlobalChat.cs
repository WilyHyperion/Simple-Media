namespace SimpleMedia.Pages;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class Chat : LoggedPage
{
    List<HttpListenerResponse> listeners = new List<HttpListenerResponse>();
    public Chat(){
        Messages = Database.GetObjects<GlobalMessage>();
    }
    public static List<GlobalMessage> Messages;
    public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        String m = "";
        for(int i =( Messages.Count() - 11); i < Messages.Count() ; i++)
        {
            if(i < 0)
            {
                continue; 
            }

            m += "<li>" + Messages[i].ToString() + "</li>";
        }
        return Server.RenderFile("Frontend/GlobalChat.html", new Dictionary<string, string>{
            {"MESSAGES", m}
        });
    }
    public override byte[] Post(HttpListenerRequest request, HttpListenerResponse response)
    {
        if(request.Headers["type"] == "send")
        {
            GlobalMessage m = new GlobalMessage(Util.ReadRequestBody(request), LoginManager.GetUser(request));
            m.Validate();
            Messages.Add(m);
            Database.AddObject(m);  
            for(int i = 0; i < listeners.Count(); i++)
            {
                HttpListenerResponse r = listeners[i];
                if(r == null || !r.OutputStream.CanWrite)
                {
                    listeners.RemoveAt(i);
                    i--;
                    continue;
                }
                try{
                r.OutputStream.Write(m.ToString().GetBytes(), 0, m.ToString().Length);
                r.OutputStream.Close();
                listeners.RemoveAt(i);
                   i--;
            
                }catch(Exception e)
                {
                    listeners.RemoveAt(i);
                    i--;
                }
             }
            
        }
        else if (request.Headers["type"] == "register")
        {
            listeners.Add(response);
            return null;
        }
        else if(request.Headers["type"] == "get")
        {
            int start = int.Parse(request.Headers["start"]);
            int amount = int.Parse(request.Headers["amount"]);
            String r = "";
            for(int i = amount; i  > 0; i--)
            {

                if(start  - i >= Messages.Count())
                {
                    continue;
                }
                if(Messages.Count()  - start - i < 0)
                {
                    continue;
                }
                r  += Messages[Messages.Count()  - start - i].ToString() + "\n";

            }
            return r.GetBytes();
        }
        return null;
    }
}