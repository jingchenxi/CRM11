using CRM11.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CRM11.IRespository;
using CRM11.Utility;
using CRM11.MODEL;

namespace CRM11.Service
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected IBaseRespository<TEntity> dal = null; //操作数据实体对象

        public BaseService()
        {
            InitDal();        
        }

        public abstract void InitDal();


        public IRespository.IDbSession DBSession
        {
            get
            {
                object dbSession = System.Runtime.Remoting.Messaging.CallContext.GetData(typeof(IRespository.IDbSession).FullName);
                if (dbSession == null)
                {
                    dbSession = DI.GetObject<IRespository.IDbSession>("dalSession");
                    System.Runtime.Remoting.Messaging.CallContext.SetData(typeof(IRespository.IDbSession).FullName, dbSession);
                }
                return dbSession as IRespository.IDbSession;
            }
        }

        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="model"></param>
        public void Add(TEntity model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 移除实体对象
        /// </summary>
        /// <param name="model"></param>
        public void Remove(TEntity model)
        {
            dal.Remove(model);
        }

        /// <summary>
        /// 根据条件移除实体对象
        /// </summary>
        public void RemoveByCondition(Expression<Func<TEntity, bool>> expression)
        {
            dal.RemoveByCondition(expression);
        }

        /// <summary>
        /// 修改实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertiesName"></param>
        public void Update(TEntity model, string[] propertiesName)
        {
            dal.Update(model, propertiesName);
        }

        /// <summary>
        /// 根据条件批量修改实体对象
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="propertiesName"></param>
        /// <param name="values"></param>
        public void UpdateByCondition(Expression<Func<TEntity, bool>> expression, string[] propertiesName, string[] values)
        {
            dal.UpdateByCondition(expression, propertiesName, values);
        }

        /// <summary>
        /// 根据条件查询数据并以IEnumerable<TEntity>形式返回数据结果
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return dal.Where(expression);
        }

        /// <summary>
        /// 支持连表查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> WhereAndInclude(Expression<Func<TEntity, bool>> where, params string[] includeNames)
        {
            return dal.WhereAndInclude(where, includeNames);
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
            return dal.WhereAndSort(expression, orderby, isAsc);
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
            return dal.WherePaged(pageindex, pagesize, expression, orderby, isAsc);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> where)
        {
            return dal.Single(where);
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
        public MODEL.PageData<TEntity> WherePaged<TKey>(int pageindex, int pagesize, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool isAsc = true, params string[] includeNames)
        {
            return dal.WherePaged(pageindex, pagesize, expression, orderby, isAsc, includeNames);
        }
    }
}
