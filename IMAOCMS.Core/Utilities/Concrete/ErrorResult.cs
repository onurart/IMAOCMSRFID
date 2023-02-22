namespace IMAOCMS.Core.Utilities.Concrete;
public class ErrorResult : Result
{
    public ErrorResult() : base(false)
    {
    }
    public ErrorResult(string message) : base(false, message)
    {
    }
}