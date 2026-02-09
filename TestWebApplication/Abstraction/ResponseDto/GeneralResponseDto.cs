using System.Text.Json.Serialization;

namespace TestWebApplication.Abstraction.ResponseDto;

public class GeneralResponseDto : BaseResponseDto<GeneralDto>
{
    [JsonConstructor]
    public GeneralResponseDto(GeneralDto data) : base(data)
    {
    }

    public GeneralResponseDto(string code, string message, List<string> details = null)
        : base(code, message, details)
    {
    }
}