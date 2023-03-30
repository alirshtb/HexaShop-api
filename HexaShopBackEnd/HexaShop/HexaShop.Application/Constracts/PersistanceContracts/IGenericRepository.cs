using HexaShop.Common.CommonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id, List<string>? includes = null);
        Task<IReadOnlyList<T>> GetListAsync(List<string>? includes = null);
        Task AddAsync(T entity);
        void Add(T entity);
        Task<int> UpdateAsync(T entity);
        int Update(T entity);
        Task<int> DeleteAsync(int id);
        Task<bool> IsExists(int id);
        Task AddRangeAsync(IEnumerable<T> entities);
        void AddRange(IEnumerable<T> entities);
    }
}
