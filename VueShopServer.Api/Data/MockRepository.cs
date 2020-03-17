using System;
using System.Collections.Generic;
using System.Linq;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Data
{
    public class MockRepository<T> : IRepository<T> where T : BaseEntity
    {
        private static readonly List<T> Storage = new List<T>();

        public MockRepository()
        {
        }

        public T Delete(T entity)
        {
            Storage.Remove(entity);
            return entity;
        }
        public T Get(int id) => Storage.FirstOrDefault(e => e.Id == id);

        public T Insert(T entity)
        {
            var maxId = Storage.Any() ? Storage.Max(e => e.Id) : 0;
            entity.Id = maxId + 1;
            Storage.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            var ent = Storage.FirstOrDefault(e => e.Id == entity.Id);
            if (ent != null)
            {
                ent = entity;
            }
            else
            {
                throw new Exception($"{nameof(entity)} not exists.");
            }
            return ent;
        }

        public IQueryable<T> AsQueryable => Storage.AsQueryable();
    }
}
