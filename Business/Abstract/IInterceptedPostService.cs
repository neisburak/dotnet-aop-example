using Business.Entities;

namespace Business.Abstract;

public interface IInterceptedPostService
{
    Task<Post?> GetAsync(string id);
    Task<List<Post>?> GetAsync();
}
