using cStudy.Dao;

namespace cStudy.Services.Delete;

public sealed class DeletePostServiceImpl(IPostDao postDao) : IDeletePostService
{
    public async Task<bool> DeletePostAsync(int id)
    {
        return await postDao.DeleteAsync(id);
    }
}
