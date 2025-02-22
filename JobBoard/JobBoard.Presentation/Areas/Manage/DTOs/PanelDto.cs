using JobBoard.Core.Entities;

namespace JobBoard.Presentation.Areas.Manage.DTOs
{
    public record PanelDto
    {
        public AppUser? User { get; set; }
        public List<AppUserJob> userJobs { get; set; }
      
    }
}
