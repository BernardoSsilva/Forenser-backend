namespace ForenserBackend.Communication.Requests
{
    public class UserRequestJson
    {
        public string UserName { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public  DateTime BornDate { get; set; }

        public string UserEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<string> ContactPhonesNumbers { get; set; } = [];
    }
}
