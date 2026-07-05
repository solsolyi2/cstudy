namespace cStudy.Services.Delete;

public interface IDeletePostService
{
    Task<bool> DeletePostAsync(int id);
}
