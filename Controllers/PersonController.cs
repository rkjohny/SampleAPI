using Microsoft.AspNetCore.Mvc;
using SampleAPI.Core;
using SampleAPI.Types;

namespace SampleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController(AddPersonHelper addPersonHelper) : ControllerBase
{
    // POST: api/Person/in-memory/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("in-memory/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonInMemory(AddPersonInput input)
    {
        var output = await addPersonHelper.ExecuteHelperAsync(input, DbType.InMemory); 
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }
    
    // POST: api/Person/pg-sql/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("pg-sql/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonPgSql(AddPersonInput input)
    {
        var output = await addPersonHelper.ExecuteHelperAsync(input, DbType.PgSql);
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }


    // POST: api/Person/my-sql/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("my-sql/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonMySql(AddPersonInput input)
    {
        var output = await addPersonHelper.ExecuteHelperAsync(input, DbType.MySql);
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }

    // POST: api/Person/my-sql/add-person
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("redis/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonRedis(AddPersonInput input)
    {
        var output = await addPersonHelper.ExecuteHelperAsync(input, DbType.Redis);
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }
}