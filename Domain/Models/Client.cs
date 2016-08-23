using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Domain.Models
{
    public class Client : IEntity
    {
        [Key]
        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MaritalStatus { get; set; }

        [Required]
        public ICollection<Address> Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public List<string> Phone { get; set; }
    }
}
