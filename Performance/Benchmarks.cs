using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Performance;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Benchmarks
{
    private readonly RestClient _restClient = new();

    [Benchmark(Baseline = true)]
    public async Task Normal() => await _restClient.CallNormal();

    [Benchmark]
    public async Task Intercepted() => await _restClient.CallIntercepted();
}