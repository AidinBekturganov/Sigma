using FluentValidation.Results;

namespace Sigma.Domain.Dto;

public abstract class BaseDto<T>
{
    public List<ValidationFailure>? Errors { get; set; }
    public T Data { get; set; }
}