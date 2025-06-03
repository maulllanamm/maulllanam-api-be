using maulllanam_api_be.Entity;

namespace maulllanam_api_be.Service;

public interface ISkillService: IBaseService<Skill>
{
   Task<List<Skill>>  GetBySkillUserId(Guid userId);
}