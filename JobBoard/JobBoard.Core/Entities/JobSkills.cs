using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Core.Entities
{
    public class JobSkills
    {
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
