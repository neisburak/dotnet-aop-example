using Business.Entities;

namespace Business.Abstract;

public interface IPostService
{
    Task<Post?> GetAsync(string id);
    Task<List<Post>?> GetAsync();
}