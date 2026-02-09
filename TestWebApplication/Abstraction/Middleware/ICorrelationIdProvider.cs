namespace TestWebApplication.Abstraction.Middleware;

public interface ICorrelationIdProvider
{
    void SetCurrentCorrelationId(string correlationId);
    string GetCurrentCorrelationId();
}