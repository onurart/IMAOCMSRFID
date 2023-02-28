namespace IMAOCMS.Core.Common.Responses;
public class ApiDataResponse<T>
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}