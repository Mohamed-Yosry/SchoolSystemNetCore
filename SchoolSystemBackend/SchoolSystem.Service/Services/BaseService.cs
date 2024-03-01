using Microsoft.EntityFrameworkCore;
using SchoolSystem.PresistenceDB.DbContext;
using SchoolSystem.Service.Contract.Services;
using System.Linq.Expressions;

namespace SchoolSystem.Service.Services
{
    public class BaseService<Entity> : IBaseSerivce<Entity> where Entity : class
    {
        private readonly APIDbContext _context;
        public BaseService(
            APIDbContext context)
        {
            _context = context;
        }

        public async Task<Entity?> CreateAsync(Entity entity)
        {
            var result = await _context.Set<Entity>().AddAsync(entity);
            if (result != null)
                return entity;
            return null;
        }

        public bool Delete(Entity entity)
        {
            var result = _context.Set<Entity>().Remove(entity);
            if (result != null)
                return true;
            return false;
        }

        public async Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> match, string[] Includes = null)
        {
            var query = _context.Set<Entity>();
            if(Includes != null)
                foreach (var include in Includes)
                    query.Include(include);
            return await query.Where(match).ToListAsync();
        }

        public async Task<IEnumerable<Entity>> FindAllAsync(Expression<Func<Entity, bool>> match, int? skip, int? take, 
            Expression<Func<Entity, object>> orderBy = null, string orderByDirection = "ASC", string[] Includes = null)
        {
            IQueryable<Entity> query = _context.Set<Entity>();//.Where(match);

            if (Includes != null)
                foreach (var include in Includes)
                    query.Include(include);

            query = query.Where(match);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (orderBy != null)
            {
                if (orderByDirection == "ASC")
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>().ToListAsync();
        }

        public async Task<Entity> GetByIdAsync(object Id)
        {
            return await _context.Set<Entity>().FindAsync(Id);
        }

        public Entity? Update(Entity entity)
        {
            var result =  _context.Set<Entity>().Update(entity);
            if (result != null)
                return entity;
            return null;
        }
    }
}
