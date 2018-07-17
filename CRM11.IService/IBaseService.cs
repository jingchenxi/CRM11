using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CRM11.IService
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="model"></param>
        void Add(TEntity model);


        /// <summary>
        /// 移除实体对象
        /// </summary>
        /// <param name="model"></param>
        void Remove(TEntity model);

        /// <summary>
        /// 根据条件移除实体对象
        /// </summary>
        void RemoveByCondition(Expression<Func<TEntity, bool>> expression);


        /// <summary>
        /// 修改实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertiesName"></param>
        void Update(TEntity model, string[] propertiesName);

        /// <summary>
        /// 根据条件批量修改实体对象
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="propertiesName"></param>
        /// <param name="values"></param>
        void UpdateByCondition(Expression<Func<TEntity, bool>> expression, string[] propertiesName, string[] values);

        /// <summary>
        /// 根据条件查询数据并以IEnumerable<TEntity>形式返回数据结果
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 支持连表查询
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> WhereAndInclude(Expression<Func<TEntity, bool>> where, params string[] includeNames);

        /// <summary>
        /// 根据条件查询数据并以指定条件排序后，以IEnumerable<TEntity>形式返回数据结果
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="expression">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <param name="isAsc">是否升序，默认为true</param>
        /// <returns></returns>
        IQueryable<TEntity> WhereAndSort<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool isAsc = true);

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
        MODEL.PageData<TEntity> WherePaged<TKey>(int pageindex, int pagesize, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool isAsc = true);

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
        MODEL.PageData<TEntity> WherePaged<TKey>(int pageindex, int pagesize, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool isAsc = true, params string[] includeNames);

        TEntity Single(Expression<Func<TEntity, bool>> where);
    }
}
