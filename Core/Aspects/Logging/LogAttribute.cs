using Core.Dependencies;
using Microsoft.Extensions.Logging;

namespace Core.Aspects.Logging;

public class LogAttribute : AttributeBase
{
    public LogLevel Level { get; set; }
}