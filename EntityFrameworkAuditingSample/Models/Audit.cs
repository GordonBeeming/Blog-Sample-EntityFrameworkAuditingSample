using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkAuditingSample.Models
{
    public class Audit
    {
        [Key]
        public Guid Id { get; set; }
        public string ChangeType { get; set; }
        public string ObjectType { get; set; }
        public string FromJson { get; set; }
        public string ToJson { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("AuditUser")]
        public string AuditUserId { get; set; }
        public string TableName { get; set; }
        public string IdentityJson { get; set; }

        public ApplicationUser AuditUser { get; set; }
    }
}