using Business.Abstract;
using Business.Entities;
using Business.Extensions;
using Core.Aspects;

namespace Business.Concrete;

public class PostManager : IPostService
{
    private const string _url = "https://jsonplaceholder.typicode.com/posts/";

    [LogAspect]
    public async Task<Post?> GetAsync(string id) => await _url.GetAsync<Post>(id);

    [ExceptionAspect]
    [PerformanceAspect(10)]
    public async Task<List<Post>?> GetAsync() => await _url.GetAsync<List<Post>>();
}



