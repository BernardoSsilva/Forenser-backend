namespace ForenserBackend.Communication.Errors
{
    public class ErrorMessageJson
    {
        public List<string> ErrorMessages { get; set; } = [string.Empty];

        public ErrorMessageJson(string ErrorMessage)
        {
            ErrorMessages = [ErrorMessage];
        }

        public ErrorMessageJson(List<string> messages)
        {
            ErrorMessages = messages;
        }
    }
}
