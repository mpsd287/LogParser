namespace LogParser.Web.Models;

public record SearchParameter(string Name, ParameterType Type);

public enum ParameterType
{
    String,
    Int,
    DateTime
}
