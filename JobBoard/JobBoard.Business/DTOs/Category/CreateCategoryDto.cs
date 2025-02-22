using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Kategori adı boş olamaz")]
        public string Name { get; set; }
    }
}
