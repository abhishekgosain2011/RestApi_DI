﻿using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private ICustomerContext Context;

        private IDbSet<TEntity> Entities
        {
            get { return this.Context.Set<TEntity>(); }
        }

        public Repository(ICustomerContext context)
        {
            this.Context = context;
        }

        public void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities.AsQueryable();
        }

        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        public TEntity Insert(TEntity entity)
        {
            Entities.Add(entity);
            return entity;
        }
        public void Save()
        {
            Context.SaveChanges();
        }

        public bool Update(TEntity entity, object id)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Context.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
    }
}
