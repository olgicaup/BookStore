using Domain.Domain_Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        //string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T).IsAssignableFrom(typeof(Book)))
            {
                return entities
                    .Include(b => ((Book)(object)b).Author)
                    .Include(b => ((Book)(object)b).Publisher);
                    //.FirstOrDefaultAsync(s => s.Id == id) as T;
            }
            if (typeof(T).IsAssignableFrom(typeof(Order)))
            {
                return entities
                    .Include("Author")
                    .Include("Book")
                    .Include("Publisher")
                    .AsEnumerable();
            }
            else
            {
                return entities.AsEnumerable();
            }
        }

        public T Get(Guid? id)
        {

            if (typeof(T) == typeof(Book))
            {
                IQueryable<T> query = entities.AsQueryable();

                query = query.Include("Author").Include("Publisher");
                return query.FirstOrDefault(s => s.Id == id) as T;

            }
            if (typeof(T) == typeof(Author))
            {
                return entities.Include("AllBooks").FirstOrDefault(a => a.Id == id) as T;
            }
            if (typeof(T) == typeof(ShoppingCart))
            {
                IQueryable<T> query = entities.AsQueryable();

                return query
                    .Include(sc => ((ShoppingCart)(object)sc).BookInShoppingCarts)
                        .ThenInclude(bisc => bisc.Book)
                    .FirstOrDefault(e => ((ShoppingCart)(object)e).Id == id) as T;
            }
            if (typeof(T).IsAssignableFrom(typeof(Order)))
            {
                return entities.Include("BooksInOrder.Book").FirstOrDefault(s => s.Id == id);
            }
            else
            {
                return entities.First(s => s.Id == id);
            }

        }
        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }

        public List<T> InsertMany(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            entities.AddRange(entities);
            context.SaveChanges();
            return entities;
        }
    }
}
