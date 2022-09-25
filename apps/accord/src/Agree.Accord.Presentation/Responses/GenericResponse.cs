namespace Agree.Accord.Presentation.Responses;

public class GenericResponse
{
    public GenericResponse(object data) => Data = data;

    public object Data { get; private set; }
}

public class GenericResponse<T>
{
    public GenericResponse(T data) => Data = data;

    public T Data { get; private set; }
}