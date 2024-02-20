using Microsoft.AspNetCore.Mvc;
using SampleAPI.Core;
using SampleAPI.Types;

namespace SampleAPI.Controllers;

[Route("api/v2")]
[ApiController]
public class PersonControllerV2(Api api) : ControllerBase
{
    // POST: api/v2/Person/in-memory/add-person
    [HttpPost("Person/in-memory/add-person")]
    public async Task<ActionResult<AddPersonResponseV2>> AddPersonInMemory(AddPersonInput input)
    {
        var output = await api.AddPersonV2Async(input, DbType.InMemory);
        return new ActionResult<AddPersonResponseV2>(output);
    }
    
    // POST: api/v2/Person/pg-sql/add-person
    [HttpPost("Person/pg-sql/add-person")]
    public async Task<ActionResult<AddPersonResponseV2>> AddPersonPgSql(AddPersonInput input)
    {
        var output = await api.AddPersonV2Async(input, DbType.PgSql);
        return new ActionResult<AddPersonResponseV2>(output);
    }


    // POST: api/v2/Person/my-sql/add-person
    [HttpPost("Person/my-sql/add-person")]
    public async Task<ActionResult<AddPersonResponseV2>> AddPersonMySql(AddPersonInput input)
    {
        var output = await api.AddPersonV2Async(input, DbType.MySql);
        return new ActionResult<AddPersonResponseV2>(output);
    }

    // POST: api/v2/Person/my-sql/add-person
    [HttpPost("Person/redis/add-person")]
    public async Task<ActionResult<AddPersonResponseV2>> AddPersonRedis(AddPersonInput input)
    {
        var output = await api.AddPersonV2Async(input, DbType.Redis);
        return new ActionResult<AddPersonResponseV2>(output);
    }
}