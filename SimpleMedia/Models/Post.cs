namespace SimpleMedia.Models
{
    public class Post : ISaveable
    {
        public Post()
        {
            Id = Database.GetObjects<Post>().Count;
        }
        public Post(String text, String name,User sender)
        {
            Text = text;
            Sender = sender;
            Name = name;
            Time = DateTime.Now;
            Id = Database.GetObjects<Post>().Count;
        }
        public static string renderPost(Post p){
            String s = Server.ReadFileText("Frontend/Post.html");
            s = s.Replace("{{name}}", p.Name);
            s = s.Replace("{{text}}", p.Text);
            s = s.Replace("{{sender}}", p.Sender.Username);
            s = s.Replace("{{time}}", p.Time.ToString());
            s = s.Replace("{{id}}", p.Id.ToString());
            return s;
        }
        public int Id { get; set; }
        public String Name{ get; set; }
        public string Text { get; set; }
        public User Sender { get; set; }
        public DateTime Time { get; set; }

        public void Load(byte[] data)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));
            Text = reader.ReadString();
            Sender = LoginManager.GetUser(reader.ReadString());
            Time = new DateTime(reader.ReadInt64());
            Id = reader.ReadInt32();
            reader.Dispose();
        }
        public byte[] Save()
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write(Text);
            writer.Write(Sender.Username);
            writer.Write(Time.Ticks);
            writer.Write(Id);
            return ((MemoryStream)writer.BaseStream).ToArray();
        }
    }
}