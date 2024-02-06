using Microsoft.EntityFrameworkCore;


namespace SampleAPI.Repository;

public class PersonRepositoryInMemory(DbContextOptions<PersonRepositoryInMemory> options) : AbstractPersonRepository<PersonRepositoryInMemory>(options);