using cStudy.Models;

namespace cStudy.Dao;

public interface IPostDao
{
    Task<IReadOnlyList<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(int id);
    Task<Post> CreateAsync(Post post);
    Task<Post?> UpdateAsync(Post post);
    Task<bool> DeleteAsync(int id);
}
