using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;

namespace maulllanam_api_be.Service;

public interface IUserService : IBaseService<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<AboutDTO> GetAbout(Guid id);
}