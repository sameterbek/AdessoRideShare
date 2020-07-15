using AdessoRideShare.Db.Entity;
using AdessoRideShare.Db.Enum;
using AdessoRideShare.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AdessoRideShare.Repository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBase
    {
        public DbContext _context;


        public Repository(IDbFactory Context)
        {
            _context = Context.Init();
        }

        public string RepositoryName
        {
            get
            {
                string fullName = this.GetType().ToString();
                return fullName.Substring(fullName.LastIndexOf(".") + 1);
            }
        }




        public virtual void Add(TEntity instance)
        {
            SetRecordId(instance);

            SetSessionValues(instance);

            _context.Set<TEntity>().Add(instance);
        }

        public void SetSessionValues(IBase instance)
        {
            if (instance.RecordState == ERecordState.Added)
            {
                instance.CreatedDateTime = DateTime.Now;
                instance.Deleted = 0;
            }

            foreach (var property in instance.GetType().GetProperties())
            {
                if (
                    (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) && property.GetCustomAttribute<NotMappedAttribute>() == null) ||
                     (property.PropertyType.BaseType != null && (
                     property.PropertyType.BaseType.IsGenericType && property.PropertyType.BaseType.GetGenericTypeDefinition() == typeof(List<>) && property.GetCustomAttribute<NotMappedAttribute>() == null))
                     )
                {
                    try
                    {
                        if (property.GetValue(instance, null) != null)
                            foreach (var baseItem in (IEnumerable)property.GetValue(instance, null))
                            {
                                if (baseItem is Base)
                                {
                                    Base baseItemInstance = (Base)baseItem;
                                    SetSessionValues(baseItemInstance);
                                }
                            }

                    }
                    catch { }
                }
            }
        }
        public void SetRecordState(IBase instance, ERecordState recordState)
        {
            instance.RecordState = recordState;

        }

        public void SetRecordId(IBase instance)
        {
            if (instance.RecordState == ERecordState.Added)
                instance.SetRecordId();

            foreach (var property in instance.GetType().GetProperties())
            {
                if (
                    (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) && property.GetCustomAttribute<NotMappedAttribute>() == null) ||
                     (property.PropertyType.BaseType != null && (
                     property.PropertyType.BaseType.IsGenericType && property.PropertyType.BaseType.GetGenericTypeDefinition() == typeof(List<>) && property.GetCustomAttribute<NotMappedAttribute>() == null))
                     )
                {
                    try
                    {
                        if (property.GetValue(instance, null) != null)
                            foreach (var baseItem in (IEnumerable)property.GetValue(instance, null))
                            {
                                if (baseItem is Base)
                                {
                                    Base baseItemInstance = (Base)baseItem;

                                    SetRecordId(baseItemInstance);
                                }
                            }

                    }
                    catch { }
                }
            }

        }

        private TEntity Find(decimal recordId)
        {
            return _context.Set<TEntity>().Find(recordId);
        }

        public virtual TEntity Load(decimal recordId)
        {
            return Queryable().FirstOrDefault(p => p.RecordId == recordId);
        }

        public virtual TEntity Load(decimal recordId, bool noTracking)
        {
            if (noTracking)
                return Queryable().FirstOrDefault(p => p.RecordId == recordId);
            else
                return Load(recordId);
        }

        public virtual TEntity Load(string[] includes, decimal recordId)
        {
            var query = Queryable();

            foreach (string include in includes) query = query.Include(include);

            return query.Single(p => p.RecordId == recordId);
        }


        public virtual TEntity LoadByCode(string Code)
        {
            return Queryable().SingleOrDefault(p => p.Code == Code);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereClause, string[] includes)
        {
            var query = Queryable();

            foreach (string include in includes) query = query.Include(include);

            return query.Where(whereClause).FirstOrDefault();
        }


        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereClause)
        {
            return SingleOrDefault(whereClause, new string[] { });
        }

        public static PropertyInfo[] GetPropertiesByAttribute(Type type, Type attribute)
        {

            var props = from p in type.GetProperties()
                        let attrs = p.GetCustomAttributes(attribute, true)
                        where attrs.Length != 0
                        select p;

            return props.ToArray();
        }

        public static PropertyInfo[] GetMappedProperties(Type type)
        {
            return type.GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() != null).ToArray();
        }

        public static PropertyInfo[] GetNotMappedProperties(Type type)
        {
            return type.GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
        }

        public void SetEntityValues(IBase dynamicProxy, IBase poco, IBase parentPoco = null)
        {
            var readonlyFields = new List<string> { "RecordId", "CreatedDateTime" };

            #region  alt enityleri güncellerken upper record idler güncellenemez.

            if (parentPoco != null)
            {
                var parentProperty = GetPropertiesByAttribute(poco.GetType(), typeof(ForeignKeyAttribute)).Where(p => p.Name == parentPoco.GetType().Name).FirstOrDefault();

                if (parentProperty != null)
                {
                    readonlyFields.Add(parentProperty.GetCustomAttribute<ForeignKeyAttribute>().Name);
                }
            }

            #endregion

            Type pocoType = poco.GetType();
            
            var headerCurrentValues = _context.Entry(dynamicProxy).CurrentValues;
            var headerProps = pocoType.GetProperties();

            var modifiedSpecialFields = new string[] { "ModifiedDateTime", "ModifiedUser" };

            foreach (var propertyName in headerCurrentValues.Properties.Where(x => !readonlyFields.Contains(x.Name)))
            {
                headerCurrentValues[propertyName] = headerProps.Where(x => x.Name == propertyName.Name).Single().GetValue(poco);
            }

            foreach (var notMappedProperty in GetMappedProperties(dynamicProxy.GetType()))
            {
                if (notMappedProperty.CanWrite)
                    notMappedProperty.SetValue(dynamicProxy, headerProps.Where(x => x.Name == notMappedProperty.Name).Single().GetValue(poco));

            }

            dynamicProxy.RecordState = poco.RecordState;

            Func<PropertyInfo, bool> listWhere = (PropertyInfo p) => (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                 ||
                  (p.PropertyType.BaseType != null && (
                  p.PropertyType.BaseType.IsGenericType && p.PropertyType.BaseType.GetGenericTypeDefinition() == typeof(List<>))));


            var listProperties = GetNotMappedProperties(pocoType).Where(listWhere).ToList();

            var notMapProps = GetMappedProperties(pocoType).Where(
                p => (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() != typeof(List<>)
                ||
                 (p.PropertyType.BaseType != null && (
                 p.PropertyType.BaseType.IsGenericType && p.PropertyType.BaseType.GetGenericTypeDefinition() != typeof(List<>))))
                ).ToList();

            var dynamicProxyListProperties = GetNotMappedProperties(dynamicProxy.GetType()).Where(listWhere).ToList();

            var listPropertyNames = listProperties.Select(x => x.Name);
            var notMapPropNames = notMapProps.Select(x => x.Name);


            foreach (var listProperty in listProperties)
            {
                #region Details:


                var listItems = (IList)listProperty.GetValue(poco);

                if (listItems == null)
                    continue;

                foreach (IBase listItem in listItems)
                {
                    if (listItem.RecordState == ERecordState.Unchanged)
                        continue;

                    else if (listItem.RecordState == ERecordState.Added)
                    {
                        var propertyOnDynamic = dynamicProxyListProperties.Where(x => x.Name == listProperty.Name).FirstOrDefault();

                        if (propertyOnDynamic == null)
                            throw new Exception(String.Format("{0} List<> property not found on class:{1}", listProperty.Name, dynamicProxy.GetType()));

                        var listValueOnDynamic = (IList)propertyOnDynamic.GetValue(dynamicProxy);

                        if (listValueOnDynamic == null)
                        {
                            var listType = typeof(List<>);
                            var constructedListType = listType.MakeGenericType(listItem.GetType());
                            var instance = (IList)Activator.CreateInstance(constructedListType);
                            instance.Add(listItem);
                            propertyOnDynamic.SetValue(dynamicProxy, instance);
                        }
                        else if (!listValueOnDynamic.Contains(listItem))
                            listValueOnDynamic.Add(listItem);
                    }
                    else
                    {
                        var dbListItem = _context.Set<TEntity>().Find(listItem.RecordId);

                        if (dbListItem != null)
                        {
                            if (listItem.RecordState == ERecordState.Deleted)
                            {
                                _context.Entry(dbListItem).Property("Deleted").CurrentValue = 1;
                                _context.Entry(dbListItem).Property("Deleted").IsModified = true;
                            }
                            else
                            {
                                SetEntityValues((IBase)dbListItem, listItem, poco);
                            }
                        }
                    }
                }
                #endregion
            }
        }
        public virtual TEntity Update(TEntity instance)
        {
            var existingParent = _context.Set<TEntity>().SingleOrDefault(p => p.RecordId == instance.RecordId);

            if (existingParent == null)
                throw new Exception("Main  object not found for id: " + instance.RecordId);
            else
            {
                SetRecordId(instance);

                SetSessionValues(instance);

                SetEntityValues(existingParent, instance);


                return existingParent;
            }
        }

        public IQueryable<TEntity> Queryable()
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>().Where(x => x.Deleted == 0);

            return queryable;
        }



        public IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> filter = null,
        int? page = null,
        int? pageSize = null)
        {
            var filters = new List<Expression<Func<TEntity, bool>>>();
            filters.Add(filter);
            return Search(filters, page, pageSize);
        }

        public IQueryable<TEntity> Search(
        List<Expression<Func<TEntity, bool>>> filters = null,
        int? page = null,
        int? pageSize = null)
        {
            IQueryable<TEntity> query = Queryable();

            if (filters != null)
                foreach (var filter in filters)
                    query = query.Where(filter);

            if (page != null && pageSize != null)
            {

                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }

    }
}
