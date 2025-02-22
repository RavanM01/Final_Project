using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.DTOs.Job
{
    public class GetJobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MinExpTime { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? Salary { get; set; }
        public string Address { get; set; }
        public int CategoryId { get; set; }
        public int JobTypeId { get; set; }
        public int? GenderId { get; set; }
        public int EducationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public List<int>? SkillIds { get; set; }
        public List<string>? AppUsersIds { get; set; }
    }
}
