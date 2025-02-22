using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.DTOs.User
{
    public record CompanyRegisterDto
    {
        [Required]
        public string FullName { get; set; }

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
        public IFormFile? Logo { get; set; }
        public string? LogoUrl { get; set; }
    }
}
