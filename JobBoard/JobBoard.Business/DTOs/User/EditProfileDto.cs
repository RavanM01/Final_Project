using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.DTOs.User
{
    public record EditProfileDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public IFormFile LogoUrl { get; set; }
        public IFormFile CvUrl { get; set; }
        public string? Address  { get; set; }
        public string? Description { get; set; }
    }
}
