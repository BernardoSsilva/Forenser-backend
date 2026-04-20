namespace ForenserBackend.Communication.Response.Detailed
{
    public class DetailedUserResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public  string UserName { get; set; } = string.Empty;
        public  string CPF { get; set; } = string.Empty;
        public  DateTime BornDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public  string UserEmail { get; set; } = string.Empty;
        public List<string> ContactPhonesNumbers { get; set; } = [];

    }
}
