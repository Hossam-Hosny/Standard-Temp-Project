namespace Project.Domain.Exceptions;

public class ResourceExistException(string message) : Exception($"{message}")
{
}
