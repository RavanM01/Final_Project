using JobBoard.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Core.Entities
{
    public class Skill :BaseEntity
    {
        public string Name { get; set; }

        public List<JobSkills> JobSkills { get; set; }
    }
}
