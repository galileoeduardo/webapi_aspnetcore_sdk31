using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<Table> : IRepository<Table> where Table : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<Table> _dataset;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<Table>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null) return false;

                _dataset.Remove(result);
                await _context.SaveChangesAsync();


            }
            catch (Exception e)
            {

                throw e;
            }

            return true;

        }

        public async Task<Table> InsertAsync(Table entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                _dataset.Add(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }

            return entity;
        }

        public async Task<Table> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<Table>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<Table> UpdateAsync(Table entity)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(entity.Id));
                if (result == null) return null;

                entity.UpdatedAt = DateTime.UtcNow;
                entity.CreatedAt = entity.CreatedAt;

                _context.Entry(result).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw e;
            }

            return entity;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }
    }
}
