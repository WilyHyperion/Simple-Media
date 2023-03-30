namespace SimpleMedia
{
    public class GlobalMessage : ISaveable
    {
        public GlobalMessage()
        {
        }
        public GlobalMessage(String text, User sender)
        {
            Text = text;
            this.Validate();
            Sender = sender;
            Time = DateTime.Now;
        }
        public string Text { get; set; }
        public User Sender { get; set; }
        public DateTime Time { get; set; }
        public override string ToString()
        {
            return $"<img width = '50px' height = '50px' src = '/api/getotherprofile?username={Sender.Username}'> <a href = /profile/{Sender.Username}>{Sender.Username} </a> ({Time.ToShortTimeString()}): {Text}";
        }
        public void Load(byte[] data)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));
            Text = reader.ReadString();
            Sender = LoginManager.GetUser(reader.ReadString());
            Time = new DateTime(reader.ReadInt64());
        }

        public byte[] Save()
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write(Text);
            writer.Write(Sender.Username);
            writer.Write(Time.Ticks);
            return ((MemoryStream)writer.BaseStream).ToArray();
        }
        public void Validate()
        {
            for(int i = 0; i < Util.invalidChars.Length; i++)
            {
                    Text = Text.Replace(Util.invalidChars[i], ' ');
            }
        }
    }
}