using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.MODEL
{
    public class TreeNode
    {
        /// <summary>
        /// 节点的 id，它对于加载远程数据很重要
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 要显示的节点文本
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 节点状态，'open' 或 'closed'，默认是 'open'。当设置为 'closed' 时，该节点有子节点，并且将从远程站点加载它们
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 指示节点是否被选中
        /// </summary>
        public bool @checked { get; set; }

        /// <summary>
        /// 给一个节点添加的自定义属性
        /// </summary>
        public object attributes { get; set; }

        /// <summary>
        ///定义了一些子节点的节点数组 
        /// </summary>
        public List<TreeNode> children { get; set; }
    }
}
