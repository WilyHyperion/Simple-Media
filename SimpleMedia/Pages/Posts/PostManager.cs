using System.Net;
using SimpleMedia.Models.Abstract;
namespace SimpleMedia.Models.Pages{
    public class PostManager : Page{
        public override byte[] Get(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("Getting post: " + request.Url.AbsolutePath.Split("/")[2]);
            var posts = Database.GetObjects<Post>().Where(x => x.Id == int.Parse(request.Url.AbsolutePath.Split("/")[2]));
            Console.WriteLine("Found " + posts.Count() + " posts");
            if (posts.Count() == 0)
            {
                return "404 Not Found".GetBytes();
            }
            Post p = posts.First();
            return Models.Post.renderPost(p).GetBytes();
        }
        public override bool isCurrentPage(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.ToLower().StartsWith("/posts/");
        }
    }
}