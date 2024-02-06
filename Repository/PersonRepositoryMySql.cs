using Microsoft.EntityFrameworkCore;


namespace SampleAPI.Repository;

public class PersonRepositoryMySql(DbContextOptions<PersonRepositoryMySql> options) : AbstractPersonRepository<PersonRepositoryMySql>(options);