namespace IMAOCMS.Core.Utilities.Abstract;
public interface IResult
{
    public bool Success { get; }
    public string Message { get; }
}