using JobBoard.Core.Entities;
using JobBoard.DAL.Context;
using JobBoard.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.DAL.Repositories.Implementations
{
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        public CategoryRepo(AppDbContext context) : base(context)
        {
        }
    }
}
