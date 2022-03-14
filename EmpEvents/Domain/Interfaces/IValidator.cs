namespace EmpEvents.Domain.Interfaces;

internal interface IValidator<in T>
{
    public bool Validate(T t);
    
}