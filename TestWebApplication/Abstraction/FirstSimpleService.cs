namespace TestWebApplication.Abstraction;

public class FirstSimpleService : ISimpleService
{
    public string GetName()
    {
        return nameof(FirstSimpleService);
    }
}