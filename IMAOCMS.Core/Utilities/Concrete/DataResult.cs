using IMAOCMS.Core.Utilities.Abstract;

namespace IMAOCMS.Core.Utilities.Concrete;
public class DataResult<T> : Result, IDataResult<T>
{
    public DataResult(T data, bool success) : base(success)
    {
        Data = data;
    }
    public DataResult(T data, bool success, string messsage) : base(success, messsage)
    {
        Data = data;
    }
    public T Data { get; set; }
}