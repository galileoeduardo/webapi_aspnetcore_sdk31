using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<Table> where Table : BaseEntity
    {
        Task<Table> InsertAsync(Table entity);
        Task<Table> UpdateAsync(Table entity);
        Task<bool> DeleteAsync(int id);
        Task<Table> SelectAsync(int id);
        Task<IEnumerable<Table>> SelectAsync();
        Task<bool> ExistAsync(int id);
    }
}
