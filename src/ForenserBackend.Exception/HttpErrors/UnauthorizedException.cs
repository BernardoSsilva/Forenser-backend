using System.Net;

namespace ForenserBackend.Exception.HttpErrors
{
    public class UnauthorizedException : ForenserException
    {
        private List<string> Errors { get; set; }
        public override int statusCode => (int)HttpStatusCode.Unauthorized;

        public UnauthorizedException(string ErrorMessage) : base(string.Empty) => Errors = [ErrorMessage];

        public override List<string> getErrors()
        {
            return Errors;
        }
    }
}
