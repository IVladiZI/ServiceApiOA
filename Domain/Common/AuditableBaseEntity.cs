using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    /// <AuditableBaseEntity>
    /// In this abstract class is placed all the auditable parameters that must have a 
    /// column that is to say they serve us to know that date was created who was the creator 
    /// and in turn who modified for it is created in the common folder.
    /// </AuditableBaseEntity>
    public abstract class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string? CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifyUser { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
