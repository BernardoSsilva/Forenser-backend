using System.Net;

namespace ForenserBackend.Exception
{
    public class MultipleErrorsException : ForenserException
    {
        private List<string> Errors { get; set; }
        public override int statusCode => (int) HttpStatusCode.BadRequest;

        public MultipleErrorsException(List<string> ErrorMessages) : base(string.Empty) => Errors = ErrorMessages;
        public override List<string> getErrors()
        {
            return Errors;
        }
    }
}
