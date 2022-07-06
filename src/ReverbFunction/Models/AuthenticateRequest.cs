using System.ComponentModel.DataAnnotations;

namespace ReverbFunction.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}