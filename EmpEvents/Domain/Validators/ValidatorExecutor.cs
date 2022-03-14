using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Validators;

public abstract class ValidatorExecutor<T> : IValidator<T>
{
    protected abstract bool Execute(T t);

    public bool Validate(T t)
    {
        return Execute(t);
    }
}