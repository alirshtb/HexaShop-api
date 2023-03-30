using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly HexaShopDbContext _dbContext;

        public GenericRepository(HexaShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
            var result = _dbContext.SaveChanges();
        }

        /// <summary>
        /// add new entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>number of entities added.</returns>
        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            //var property = entity.GetType().GetProperties();
            //var id = (int)property.Where(p => p.Name == "Id").FirstOrDefault().GetValue(entity);
            //return id;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Add bulk entities.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// delete an entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of entities deleted.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// get by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entity</returns>
        public async Task<T> GetAsync(int id, List<string>? includes = null)
        {

            var expression = CommonStaticFunctions.GetLambdaExpression<T, int>("Id", id);

            var all = GetAsQueryable(includes: includes, expression: expression);

            return all.FirstOrDefault();
        }

        /// <summary>
        /// get list.
        /// </summary>
        /// <returns>entities list</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IReadOnlyList<T>> GetListAsync(List<string>? includes = null)
        {
            return await GetAsQueryable(includes: includes).ToListAsync();
        }

        /// <summary>
        /// is entity exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>tru if entity with given id is exists.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> IsExists(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id) != null;
        }

        public int Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            var result = _dbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// update an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>number of entities updated.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }


        /// <summary>
        /// Get All as Queryable.
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected IQueryable<T> GetAsQueryable(List<string>? includes = null, Expression<Func<T, bool>>? expression = null)
        {
            var entities = _dbContext.Set<T>().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    entities = entities.Include(include);
                }
            }
            
            if(expression != null)
            {
                entities = entities.Where(expression);
            }

            return entities;

        }

    }
}
