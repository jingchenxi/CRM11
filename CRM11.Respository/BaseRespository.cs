using CRM11.IRespository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CRM11.MODEL;

namespace CRM11.Respository
{
    
    public class BaseRespository<TEntity> : IBaseRespository<TEntity>
        where TEntity :class
    {
        DbContext context = EFFactory.GetEntityContext(); //获得一个线程只拥有一个的DbContext对象

        DbSet<TEntity> db = null;

        /// <summary>
        /// 供外部访问的 DbSet<TEntity>对象
        /// </summary>
        public DbSet<TEntity> DbSet { get => db; }

        /// <summary>
        /// 构造BaseRespository时从Entity容器中获取具体的DateSet表
        /// </summary>
        public BaseRespository()
        {
            db = context.Set<TEntity>();

        }
    
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="model"></param>
        public void Add(TEntity model)
        {
            db.Add(model);
        }

        /// <summary>
        /// 移除实体对象
        /// </summary>
        /// <param name="model"></param>
        public void Remove(TEntity model)
        {
            db.Attach(model);
            db.Remove(model);           
        }

        /// <summary>
        /// 根据条件移除实体对象
        /// </summary>
        public void RemoveByCondition(Expression<Func<TEntity,bool>> expression)
        {
            var list = db.Where(expression);

            foreach (var item in list)
            {
                db.Attach(item);
                db.Remove(item);
            }
        }

        /// <summary>
        /// 修改实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertiesName"></param>
        public void Update(TEntity model,string[] propertiesName)
        {
            //关闭EF容器的验证检查，方便修改操作
            context.Configuration.ValidateOnSaveEnabled = false;

            DbEntityEntry entry = context.Entry<TEntity>(model);

            entry.State = EntityState.Unchanged;

            foreach (var item in propertiesName)
            {
                entry.Property(item).IsModified = true;
            }
        }

        /// <summary>
        /// 根据条件批量修改实体对象(利用反射)
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="propertiesName"></param>
        /// <param name="values"></param>
        public void UpdateByCondition(Expression<Func<TEntity,bool>> expression, string[] propertiesName,string[] values)
        {
            var list = db.Where(expression);

            Type type = typeof(TEntity);

            foreach (var item in list)
            {
                for (int i = 0; i < propertiesName.Length; i++)
                {
                    PropertyInfo propertyInfo = type.GetProperty(propertiesName[i]);

                    propertyInfo.SetValue(item, values[i]);
                }
            }

        }

        /// <summary>
        /// 根据条件查询数据并以IEnumerable<TEntity>形式返回数据结果
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return db.Where(expression); 
            //为什么要ToList，因为where返回IQueryable(接口)类型，要返回IEnumerable类型，所以可以用ToList()转换，明确调用
        }

        /// <summary>
        /// 支持连表查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> WhereAndInclude(Expression<Func<TEntity, bool>> where, params string[] includeNames)
        {
            if (includeNames != null)
            {
                DbQuery<TEntity> dbQuery = db;
                foreach (var item in includeNames)
                {
                    dbQuery = db.Include(item);
                }

                return dbQuery.Where(where);
            }
            return db.Where(where);
        }

        /// <summary>
        /// 根据条件查询数据并以指定条件排序后，以IEnumerable<TEntity>形式返回数据结果
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="expression">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <param name="isAsc">是否升序，默认为true</param>
        /// <returns></returns>
        public IQueryable<TEntity> WhereAndSort<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool isAsc = true)
        {
            if (isAsc)
            {
                return db.Where(expression).OrderBy(orderby);
            }else
            {
                return db.Where(expression).OrderByDescending(orderby);
            }
            
        }

        /// <summary>
        /// 根据条件查询数据并以指定条件排序，实现分页，以EasyUI需要的数据模型形式返回数据结果
        /// </summary>
        /// <typeparam name="TKey">排序lambda表达式返回的排序类型，有编译器自动推断出来</typeparam>
        /// <param name="pageindex">当前请求页码</param>
        /// <param name="pagesize">每页的数据条数</param>
        /// <param name="expression">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public PageData<TEntity> WherePaged<TKey>(int pageindex, int pagesize, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool isAsc = true)
        {
            int skipCount = (pageindex - 1) * pagesize; //要跳过的数据条数

            var dbSetWhere = db.Where(expression);
            IOrderedQueryable<TEntity> iorderQueryData = null;
            if (isAsc)
            {
                iorderQueryData = dbSetWhere.OrderBy(orderby);
            }else
            {
                iorderQueryData = dbSetWhere.OrderByDescending(orderby);
            }
            MODEL.PageData<TEntity> pageData = new PageData<TEntity>();
            pageData.rows = iorderQueryData.Skip(skipCount).Take(pagesize).ToList();
            pageData.total = dbSetWhere.Count();

            return pageData;
        }

        /// <summary>
        /// 支持多表查询，根据条件查询数据并以指定条件排序，实现分页，以EasyUI需要的数据模型形式返回数据结果
        /// </summary>
        /// <typeparam name="TKey">排序lambda表达式返回的排序类型，有编译器自动推断出来</typeparam>
        /// <param name="pageindex">当前请求页码</param>
        /// <param name="pagesize">每页的数据条数</param>
        /// <param name="expression">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="includeNames">要连接的表名列表</param>
        /// <returns></returns>
        public PageData<TEntity> WherePaged<TKey>(int pageindex, int pagesize, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool isAsc = true,params string[] includeNames)
        {
            int skipCount = (pageindex - 1) * pagesize;

            DbQuery<TEntity> dbQuery = db;

            foreach (var item in includeNames)
            {
                dbQuery = db.Include(item);
            }
            var dbWhere = dbQuery.Where(expression);
            IOrderedQueryable<TEntity> iorderQueryData = null;
            if (isAsc)
            {
                iorderQueryData = dbWhere.OrderBy(orderby);
            }
            else
            {
                iorderQueryData = dbWhere.OrderByDescending(orderby);
            }
            MODEL.PageData<TEntity> pageData = new PageData<TEntity>();
            pageData.rows = iorderQueryData.Skip(skipCount).Take(pagesize).ToList();
            pageData.total = dbWhere.Count();

            return pageData;
        }

       

        /// <summary>
        /// 查询唯一的单个数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> where)
        {
            return db.SingleOrDefault(where);
        }
    }
}
