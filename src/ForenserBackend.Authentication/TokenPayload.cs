namespace ForenserBackend.Authentication
{
    public class TokenPayload
    {
        public string UserEmail { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

    }
}
