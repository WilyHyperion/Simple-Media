using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SimpleMedia.Abstract;
namespace SimpleMedia.Pages{
    public class Profile : LoggedPage{
        public override bool isCurrentPage(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.StartsWith("/profile");
        }
    }
}