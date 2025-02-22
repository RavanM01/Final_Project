using JobBoard.Core.Entities;

namespace JobBoard.Presentation.Areas.Manage.DTOs
{
    public class IndexDto
    {
        public List<Job>? Jobs { get; set; }
        public Stats Stats { get; set; }
    }
}
