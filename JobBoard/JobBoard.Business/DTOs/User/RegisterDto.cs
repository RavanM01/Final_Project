using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.DTOs.User
{
    public record RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
    }
}
