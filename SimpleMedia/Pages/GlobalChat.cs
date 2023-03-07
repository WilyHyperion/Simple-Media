namespace SimpleMedia.Pages;
using System;
using System.Collections.Generic;
using System.Net;
using SimpleMedia.Abstract;
public class Chat : LoggedPage
{
    public static List<string> Messages = new List<string>(); 
    public override byte[] GetLogged(HttpListenerRequest request, HttpListenerResponse response)
    {
        Messages.Add("Hello World!");
        Messages.Add("ASDASDSADSADSA");
        return Server.RenderFile("Frontend/GlobalChat.html");
    }
    public override byte[] Post(HttpListenerRequest request, HttpListenerResponse response)
    {
        if(request.Headers["type"] == "message")
        {
            Messages.Add(Util.ReadRequestBody(request));
        }
        else if(request.Headers["type"] == "get")
        {
            int amount = 2;//int.Parse(request.Headers["amount"]);
            String r = "";
            for(int i = 0; i < amount; i++)
            {
                if(i >= Messages.Count)
                    break;
                r += Messages[i] + "\n";
            }
            return r.GetBytes();
        }
        return new byte[1];
    }
}