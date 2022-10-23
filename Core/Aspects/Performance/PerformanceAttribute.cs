using Core.Dependencies;

namespace Core.Aspects.Performance;

public class PerformanceAttribute : AttributeBase
{
    public int Interval { get; set; }
}