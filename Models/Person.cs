using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace SampleAPI.Models;

[Index(nameof(Email), IsUnique = true)]
public class Person : AbstractAuditableEntity
{
    [Column("first_name"), MaxLength(35), MinLength(1)]
    public string FirstName { get; set; } = null!;

    [Column("last_name"), MaxLength(35), MinLength(1)] 
    public string LastName { get; set; } = null!;

    [Column("email"), MaxLength(70), MinLength(3)]
    public string Email { get; set; } = null!;

    public bool Equals(Person person)
    {
        if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
            string.IsNullOrEmpty(Email))
            return false;

        if (string.IsNullOrEmpty(person.FirstName) || string.IsNullOrEmpty(person.LastName) ||
            string.IsNullOrEmpty(person.Email))
            return false;

        return string.Compare(FirstName, person.FirstName, StringComparison.Ordinal) == 0 &&
               string.Compare(LastName, person.LastName, StringComparison.Ordinal) == 0 &&
               string.Compare(Email, person.Email, StringComparison.Ordinal) == 0;
    }
}
