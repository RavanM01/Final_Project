
using JobBoard.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Core.Entities
{
    public class Job : BaseEntity
    {
        //age, gender
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
        public int ViewCount { get; set; }


        public List<JobSkills> JobSkills { get; set; }
        public Gender? Gender { get; set; }
        public Education Education { get; set; } 
        public Category Category { get; set; }
        public JobType JobType { get; set; }

        public List<AppUserJob> AppUserJobs { get; set; }
     
        


    }
}
