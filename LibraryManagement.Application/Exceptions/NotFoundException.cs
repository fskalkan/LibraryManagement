namespace LibraryManagement.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"{name} with id '{key}' was not found.")
    {
    }
}