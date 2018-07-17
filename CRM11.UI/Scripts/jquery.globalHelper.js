(function ($) {
    //向jquery对象上 扩展 一个 globalHelper 属性
    $.extend($, {
        globalHelper: {
            showCommonWindow: function (title, url, width, height) {

                var widthEnd = (width && typeof (width) == "number") ? width : 1200;
                var heightEnd = (height && typeof (height) == "number") ? height : 550;

                window.top.$("#commonWindow")
                    .window({ "width": widthEnd, "height": heightEnd, "title": title })
                    .window("center")
                    .window("open")
                    .children("iframe")
                    .attr("src", url);

                window.top.$.messager.progress({ text: "加载中，请您稍后~~~" });
            },
            closeCommonWindow: function () {
                window.top.$("#commonWindow").window("close").children("iframe");
                window.top.$.messager.progress("close");
            },
            reloadSelectedTapDataGrid: function ()
            {
                window.top.$("#tabBox")
                    .tabs("getSelected")
                    .children("iframe")[0]
                    .contentWindow
                    .$("#tbList")
                    .datagrid("reload");
                
            },
            //公用的datagrid组件参数
            datagridPara: {
                paras: {
                    onLoadSuccess: function (jsonData) {
                        //如果返回的消息 对象 包含 Status属性，说明是返回的 json消息，那么就调用 msgProcess方法 统一处理
                        //                   不包含Status属性，说明 返回的是 节点json数据
                        if (jsonData.Status) {
                            $.msgProcess(jsonData);
                        }
                    },
                    rownumbers: true,
                    fitColumns: true,
                    pagination: true,//启用分页
                    pageList: [10, 14, 20, 30],//页容量组
                    pageSize: 14,//初始页容量，注意 值必须和 pageList里的某个值一致
                    singleSelect: true,
                    url: "",
                    toolbar: null,
                    columns: null
                },
                //init 成员 为 paras 初始化 值
                init: function (url, toolbar, columns) {
                    this.paras.url = url;
                    this.paras.toolbar = toolbar;
                    this.paras.columns = columns;
                }
            },
            //处理特殊时间格式
            changeDateFormatter: function (coldate) {
                var date = new Date(parseInt(coldate.replace("/Date(", "").replace(")/", ""), 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                return date.getFullYear() + "-" + month + "-" + currentDate;
            },
            
        }
    })
})(window.$);