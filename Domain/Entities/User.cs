using Domain.Common;

namespace Domain.Entities
{
    /// <User>
    /// User class that inherits the auditable class and in turn has as a relation to the client class 
    /// </User>
    public class User : AuditableBaseEntity
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int Level { get; set; }
    }
}