using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.MODEL
{
    /// <summary>
    /// EasyUI分页控件所需要的数据模型
    /// </summary>
    public class PageData<TEntity>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 当前页数据集合
        /// </summary>
        public List<TEntity> rows{ get; set; }

    }
}
