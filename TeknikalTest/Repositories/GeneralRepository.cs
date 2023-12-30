﻿using TeknikalTest.Contracts;
using TeknikalTest.Data;

namespace TeknikalTest.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
        where TEntity : class
    {

        protected readonly AppDbContext _context;

        public GeneralRepository(AppDbContext context) { _context = context; }

        public TEntity? Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ICollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public bool IsExist(Guid guid)
        {
            return GetByGuid(guid) is not null;
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
   
    
}