using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Core.Entities
{
    public class AppUserJob
    {
        public int Id { get; set; } 

        public string? UserId { get; set; }
        public AppUser? User { get; set; }

        public int JobId { get; set; } 
        public Job Job { get; set; }

        public string CreatedByUserId { get; set; } 
        public AppUser CreatedByUser { get; set; }
    }
}
