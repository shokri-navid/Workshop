using System.Text.Json;
using TestWebApplication.Abstraction.Exceptions;
using TestWebApplication.Abstraction.ResponseDto;

namespace TestWebApplication.Abstraction.Middleware
{

    public class ExceptionsMiddleware : BaseExceptionMiddleware
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public ExceptionsMiddleware(ICorrelationIdProvider? correlationIdProvider,
            ILogger<BaseExceptionMiddleware> logger) : base(logger)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        protected override async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = GetErrorResponse(exception);

            var correlationId = _correlationIdProvider?.GetCurrentCorrelationId();

            if (!string.IsNullOrWhiteSpace(correlationId))
                errorResponse.response.Error?.Details.Add(
                    $"Share this correlationId with system administrator: [{correlationId}]");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse.response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            }));
        }

        private (BaseResponseDto<GeneralDto> response, int statusCode) GetErrorResponse(Exception ex) =>
            ex switch
            {
                DomainException exception => (new GeneralResponseDto(
                    exception.Code,
                    "Domain Error",
                    new List<string> { exception.Message, string.Join(", ", exception.Details) }), 400),

                ItemNotFoundException exception  => (new GeneralResponseDto(
                    exception.Code,
                    "Not found Error",
                    new List<string> { exception.Message, string.Join(", ", exception.Details) }), 404),

                PermissionDeniedException exception => (new GeneralResponseDto(
                    exception.Code,
                    "Permission Denied.",
                    new List<string> { exception.Message, string.Join(", ", exception.Details) }), 403),

                ServiceAuthenticationException exception => (new GeneralResponseDto(
                    exception.Code,
                    "Authentication Failed.",
                    new List<string> { exception.Message, string.Join(", ", exception.Details) }), 401),

                BusinessException exception => (new GeneralResponseDto(
                    exception.Code,
                    "Application Error",
                    new List<string> { exception.Message, string.Join(", ", exception.Details) }), 400),

                _ => (new GeneralResponseDto(
                    "Exception",
                    "Unhandled Error",
                    new List<string> { "There is a problem on server. please Contact administrator", "" }), 500),
            };
    }
}