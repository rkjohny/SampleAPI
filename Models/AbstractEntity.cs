using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SampleAPI.Models;

public abstract class AbstractEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("row_version")]
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; } = null!;
}