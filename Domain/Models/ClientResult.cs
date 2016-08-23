using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Domain.Models
{
    public class ClientResult
    {
        public ClientResult()
        {
            ValidationResults = new List<ValidationResult>();
        }

        public bool Success { get; set; }

        public List<ValidationResult> ValidationResults { get; set; }
    }
}
