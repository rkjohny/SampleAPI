using Microsoft.AspNetCore.Mvc;
using SampleAPI.Core;
using SampleAPI.Types;

namespace SampleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController(Api api) : ControllerBase
{
    // POST: api/Person/in-memory/add-person
    [HttpPost("in-memory/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonInMemory(AddPersonInput input)
    {
        var output = await api.AddPersonAsync(input, DbType.InMemory); 
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }
    
    // POST: api/Person/pg-sql/add-person
    [HttpPost("pg-sql/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonPgSql(AddPersonInput input)
    {
        var output = await api.AddPersonAsync(input, DbType.PgSql);
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }


    // POST: api/Person/my-sql/add-person
    [HttpPost("my-sql/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonMySql(AddPersonInput input)
    {
        var output = await api.AddPersonAsync(input, DbType.MySql);
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }

    // POST: api/Person/my-sql/add-person
    [HttpPost("redis/add-person")]
    public async Task<ActionResult<AddPersonOutput>> AddPersonRedis(AddPersonInput input)
    {
        var output = await api.AddPersonAsync(input, DbType.Redis);
        return new ActionResult<AddPersonOutput>((AddPersonOutput)output);
    }
}