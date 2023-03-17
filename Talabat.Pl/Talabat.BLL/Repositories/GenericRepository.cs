using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specification;
using Talabat.DAL.Data;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
         => await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int Id)
        => await context.Set<T>().FindAsync(Id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        => await ApplySpec(spec).ToListAsync();

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        => await ApplySpec(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpec(ISpecification<T> spec)
          => SpecificationEvaluator<T>.BuildQuery(context.Set<T>(), spec);

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        => await ApplySpec(spec).CountAsync();


    } 
}