namespace ForenserBackend.Communication.Requests
{
    public class AuthenticationRequestJson
    {
        public string UserCpf { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
}
