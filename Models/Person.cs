using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace SampleAPI.Models;

[Index(nameof(Email), IsUnique = true)]
public class Person : AbstractAuditableEntity
{
    [Column("first_name"), MaxLength(35), MinLength(1)]
    public string FirstName { get; init; } = null!;

    [Column("last_name"), MaxLength(35)]
    public string? LastName { get; init; }

    [Column("email"), MaxLength(70), MinLength(3)]
    public string Email { get; init; } = null!;
}
