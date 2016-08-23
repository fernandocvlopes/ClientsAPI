using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Domain.Models
{
    public class Address
    {
        [Required]
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
    }
}
