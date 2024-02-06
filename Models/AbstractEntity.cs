using System.ComponentModel.DataAnnotations.Schema;


namespace SampleAPI.Models;

public abstract class AbstractEntity
{
    [Column("id")]
    public long Id { get; set; }
}