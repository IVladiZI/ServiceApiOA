using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <User>
    /// User class that inherits the auditable class and in turn has as a relation to the client class 
    /// </User>
    public class User : AuditableBaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int Level { get; set; }
        public Client? Client { get; set; }
    }
}