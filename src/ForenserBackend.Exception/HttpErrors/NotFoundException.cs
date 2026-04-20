using System.Net;

namespace ForenserBackend.Exception.HttpErrors
{
    public class NotFoundException : ForenserException
    {
        private List<string> Errors { get; set; }
        public override int statusCode => (int)HttpStatusCode.NotFound;

        public NotFoundException(string ErrorMessage) : base(string.Empty) => Errors = [ErrorMessage];

        public override List<string> getErrors()
        {
            return Errors;
        }
    }
}
