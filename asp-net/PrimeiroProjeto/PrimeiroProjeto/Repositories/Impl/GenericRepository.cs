using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PrimeiroProjeto.Model.Base;
using PrimeiroProjeto.Model.Context;

namespace PrimeiroProjeto.Repositories.Impl
{
    public class GenericRepository<T> :
        IRepository<T> where T : BaseEntity
    {
        public GenericRepository(MSSQLContext context)
        {
            _context = context;
        }
        protected readonly MSSQLContext _context;
        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>().AsEnumerable();
        }
        public T FindById(long id)
        {
            return _context.Set<T>().Find(id);
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            var Value=_context.Set<T>()
                .Find(entity.Id);
            if (Value == null) { return null; }
            _context.Set<T>().Entry(Value)
                .CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return Value;

        }
        public void Delete(long id)
        {
            var Value = 
                _context.Set<T>().Find(id);
            if (Value == null)
            {
                return;
            }
            _context.Set<T>().Remove(Value);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Set<T>().Any
                (e=> e.Id==id);
            
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return _context.Set<T>()
                .FromSqlRaw(query).ToList();
        }

        public int GetCount(string query)
        {
            using var connection = _context
                .Database.GetDbConnection();
            connection.Open();
            using var command = connection.
                CreateCommand();
            command.CommandText = query;
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
}
