public class InternalServerException : Exception
{
    public InternalServerException(string message = "UnexpectedFailurePLeaseTryAgainLater", Exception ex = null) : base(message, ex)
    {

    }
}