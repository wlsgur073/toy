using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaWorks.Models
{
    [Table("TB_USER_SESSIONS")]
    public class UserSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("USER_ID")]
        public string UserId { get; set; }

        [Required]
        [Column("SESSION_TOKEN")]
        public Guid SessionToken { get; set; }

        [Required]
        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Required]
        [Column("LOGIN_TIME")]
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("LAST_ACTIVE")]
        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        [Column("IP_ADDRESS")]
        public string IpAddress { get; set; }

        [StringLength(200)]
        [Column("MACHINE_ID")]
        public string MachineId { get; set; }
    }
}