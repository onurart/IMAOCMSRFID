namespace IMAOCMS.Core.Common.Responses;
public class ApiDataListResponse<T>
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public List<T> Data { get; set; }
}