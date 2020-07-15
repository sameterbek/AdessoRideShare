using AdessoRideShare.Db.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AdessoRideShare.Service.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : IBase
    {
        TEntity Get(decimal id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Update(List<TEntity> entities);


        #region Search


        IQueryable<TEntity> Search(
        List<Expression<Func<TEntity, bool>>> filters = null,
        int? page = null,
        int? pageSize = null);

        IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> filter,
        int? page = null,
        int? pageSize = null);

        #endregion


    }
}
