using Infrastructure.Common.Exceptions;
namespace Student.Application.Dtos;

public class ResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public CustomServiceFault MessageDetail { get; set; }
}
