using Microsoft.EntityFrameworkCore;


namespace SampleAPI.Repository;

public class PersonRepositoryPgSql(DbContextOptions<PersonRepositoryPgSql> options) : AbstractPersonRepository<PersonRepositoryPgSql>(options);