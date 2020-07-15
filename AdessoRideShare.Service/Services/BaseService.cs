using AdessoRideShare.Db.Entity;
using AdessoRideShare.Db.Enum;
using AdessoRideShare.Repository.Interfaces;
using AdessoRideShare.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AdessoRideShare.Service.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IBase
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IRepository<TEntity> repository,
                         IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual void Insert(TEntity entity)
        {
            entity.SetRecordState(ERecordState.Added);

            _repository.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Update(new List<TEntity> { entity });
        }


        public virtual void Update(List<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].RecordState = ERecordState.Modified;

                entities[i] = _repository.Update(entities[i]);
            }
            
        }

        public virtual void Delete(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.RecordState = ERecordState.Deleted;
                entity.Deleted = 1;
            }

            Update(entities);
        }
        public virtual void Delete(TEntity entity)
        {
            Delete(new List<TEntity> { entity });
        }

        #region Search


        public virtual IQueryable<TEntity> Search(
           List<Expression<Func<TEntity, bool>>> filters = null,
           int? page = null,
           int? pageSize = null)
        {
            return _repository.Search(filters, page, pageSize);
        }

        public virtual IQueryable<TEntity> Search(
         Expression<Func<TEntity, bool>> filter = null,
         int? page = null,
         int? pageSize = null)
        {
            var filters = new List<Expression<Func<TEntity, bool>>>();
            filters.Add(filter);

            return Search(filters, page, pageSize);
        }

        #endregion

        public virtual TEntity Get(decimal id)
        {
            return _repository.Load(id, false);
        }


        public Type IBaseType()
        {
            return this.GetType().GetInterfaces()[0].GetGenericArguments()[0];
        }
    }
}
