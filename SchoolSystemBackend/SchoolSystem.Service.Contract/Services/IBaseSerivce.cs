using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Services
{
    public interface IBaseSerivce<Entity> where Entity : class
    {
        Task<Entity> GetByIdAsync(object Id);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity?> CreateAsync(Entity entity);
        Entity? Update(Entity entity);
        bool Delete(Entity entity);
        Task<IEnumerable<Entity>> FindAllAsync(Expression<Func<Entity, bool>> match, int? skip, int? take,
            Expression<Func<Entity, object>> orderBy = null, string orderByDirection = "ASC", string[] Includes = null);
        Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> match, string[] Includes = null);
    }
}
