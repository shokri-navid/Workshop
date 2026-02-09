using TestWebApplication.Abstraction.Application;

namespace TestWebApplication.Abstraction.RequestDto;

public class PersonEnquieryCommand : EnquiryCommandBase
{
    public PersonEnquieryCommand() : base("name")
    {
    }

    public override Dictionary<string, string> FilterPropertyMapping() => new Dictionary<string, string>
    {
        {"family", "familly"}
    };
}