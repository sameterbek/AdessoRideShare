using AdessoRideShare.Db.Entity;
using AdessoRideShare.Db.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AdessoRideShare.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBase
    {
        void SetRecordState(IBase instance, ERecordState recordState);
        string RepositoryName { get; }


        void Add(TEntity instance);


        #region Search


        IQueryable<TEntity> Search(
        List<Expression<Func<TEntity, bool>>> filters = null,
        int? page = null,
        int? pageSize = null);

        IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> filter = null,
        int? page = null,
        int? pageSize = null);

        #endregion



        TEntity Load(decimal recordId);

        TEntity Load(decimal recordId, bool noTracking);

        TEntity Load(string[] includes, decimal recordId);


        TEntity LoadByCode(string Code);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereClause);

        TEntity Update(TEntity instance);

        IQueryable<TEntity> Queryable();



    }
}
