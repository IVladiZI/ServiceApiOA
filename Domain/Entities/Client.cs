using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <Client>
    /// Client class that inherits from the auditable class and stores the client's personal data.
    /// </Client>
    public class Client : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime Birthday { get; set; }
        private int _age { get; set; }
        public string? Email { get; set; }
        public string? Addres { get; set; }
        /// <Age>
        /// Customer's age is calculated
        /// </Age>
        public int Age
        {
            get { 
                if (this._age <= 0) this._age = new DateTime(DateTime.Now.Subtract(this.CreateDate).Ticks).Year - 1; 
                return this._age;
            }
        }
    }
}
