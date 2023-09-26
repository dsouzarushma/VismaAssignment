using System.Diagnostics.Contracts;

namespace FakeBloggingSystem.CustomExceptions
{
    public class InvalidInputException:Exception
    {
       public InvalidInputException(string message):base(message) { }
    }
}
