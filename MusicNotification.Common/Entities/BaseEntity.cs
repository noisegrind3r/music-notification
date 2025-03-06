using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicNotification.Common.Entities;
public class BaseEntity
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public T Clone<T>() where T : BaseEntity
    {
        var clone = (T)MemberwiseClone();
        clone.Id = 0;
        return clone;
    }
}