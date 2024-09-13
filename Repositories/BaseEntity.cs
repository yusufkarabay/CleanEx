using System.ComponentModel.DataAnnotations;

namespace CleanEx.Repositories
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? LastModifiedAt { get; set; }
        string CreatedBy { get; set; }
        string? LastModifiedBy { get; set; }
        bool IsEnabled { get; set; }
        bool IsDeleted { get; set; }
    }
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
        public bool IsEnabled { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; } = "Yusuf";
        public string? LastModifiedBy { get; set; }
        [ConcurrencyCheck]
        public long Version { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
