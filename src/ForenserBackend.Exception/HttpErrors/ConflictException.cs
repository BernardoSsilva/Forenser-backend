
using System.Net;

namespace ForenserBackend.Exception.HttpErrors
{
    public class ConflictException : ForenserException
    {
        private List<string> Errors { get; set; }
        public override int statusCode =>(int) HttpStatusCode.Conflict;

        public ConflictException(string ErrorMessage) : base(string.Empty) => Errors = [ErrorMessage];
     
        public override List<string> getErrors()
        {
            return Errors;
        }
    }
}
