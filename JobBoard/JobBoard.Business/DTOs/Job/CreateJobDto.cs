using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.DTOs.Job
{
    public class CreateJobDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public string MinExpTime { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? Salary { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public int? CategoryId { get; set; }
        [Required]

        public int JobTypeId { get; set; }
        public int GenderId { get; set; }
        [Required]

        public int? EducationId { get; set; }
        [Required]

        public DateTime DeadlineDate { get; set; }
        public List<int>? SkillIds { get; set; }

    }
}
