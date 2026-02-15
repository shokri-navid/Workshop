namespace TestWebApplication.Abstraction;

public class SecondSimpleService : ISimpleService
{
    public string GetName()
    {
        return nameof(SecondSimpleService);
    }
}