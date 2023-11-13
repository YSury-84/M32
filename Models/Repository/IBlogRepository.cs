using MvcStartApp.Models.Db;

namespace MvcStartApp.Models.Repository
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
    }
}
