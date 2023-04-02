using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int ClientId { get; set; }
        public string? DocumentNumber { get; set; }
        public string? DocumentComplement { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string SecondLastName { get; set; }
        public DateTime Birthday { get; set; }
        private int _age { get; set; }
        public string? Email { get; set; }
        public string? Addres { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public Client(string name, string lastname, string secondLastName)
        {
            Name = name.ToUpper();
            Lastname = lastname.ToUpper();
            SecondLastName = secondLastName.ToUpper();
        }
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
