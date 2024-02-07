using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SampleAPI.Models;

[Index(nameof(CreatedAt)), Index(nameof(LastUpdatedAt))]
public class AbstractAuditableEntity : AbstractSyncableEntity
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("last_updated_at")]
    public DateTime LastUpdatedAt { get; set; }
}