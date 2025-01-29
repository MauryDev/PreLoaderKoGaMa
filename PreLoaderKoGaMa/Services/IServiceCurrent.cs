namespace PreLoaderKoGaMa.Services;

public interface IServiceCurrent
{
    object? CurrentService { get; }
    Type? CurrentServiceType { get; }

    T? GetCurrentService<T>();

}
