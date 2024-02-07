using Microsoft.AspNetCore.Mvc;
using SampleAPI.Services;
using SampleAPI.Types;

namespace SampleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController(PersonService service) : ControllerBase
{
    // POST: api/Person/in-memory/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("in-memory/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonInMemory(AddPersonInput input)
    {
        return new ActionResult<AddPersonOutput>(await service.AddPersonInMemoryAsync(input));
    }
    
    // POST: api/Person/pg-sql/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("pg-sql/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonPgSql(AddPersonInput input)
    {
        return new ActionResult<AddPersonOutput>(await service.AddPersonPgSqlAsync(input));
    }


    // POST: api/Person/my-sql/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("my-sql/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonMySql(AddPersonInput input)
    {
        return new ActionResult<AddPersonOutput>(await service.AddPersonMySqlAsync(input));
    }

    // POST: api/Person/my-sql/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("redis/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonRedis(AddPersonInput input)
    {
        return new ActionResult<AddPersonOutput>(await service.AddPersonRedisAsync(input));
    }
}