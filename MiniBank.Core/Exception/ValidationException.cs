namespace MiniBank.Core.Exception
{
    public class ValidationException : System.Exception
    {
        public ValidationException(string message):base(message){}

        public ValidationException()
        {
            
        }
    }
}