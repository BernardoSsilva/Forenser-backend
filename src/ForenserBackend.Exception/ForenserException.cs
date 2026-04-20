namespace ForenserBackend.Exception
{
    public abstract class ForenserException:SystemException
    {
        protected ForenserException(string message):base(message)
        {
            
        }

        public abstract int statusCode { get; }
        public abstract List<string> getErrors();
    }
}
