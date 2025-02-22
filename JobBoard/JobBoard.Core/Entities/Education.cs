using JobBoard.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Core.Entities
{
    public class Education:BaseEntity
    {
        public string Name { get; set; }
        public List<Job> Jobs { get; set; }


    }
}
