using IMAOCMS.Core.DTOs;
using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace IMAOCMS.API.Filters;
public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
{
    private readonly IServices<T> _service;

    public NotFoundFilter(IServices<T> service)
    {
        _service = service;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idValue = context.ActionArguments.Values.FirstOrDefault();

        if (idValue == null)
        {
            await next.Invoke();
            return;
        }

        var id = (int)idValue;
        var anyEntity = await _service.AnyAsync(x => x.Id == id);

        if (anyEntity)
        {
            await next.Invoke();
            return;
        }

        context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));
    }
}