/*author:gj
 *调用方式：$(分页父元素id).paging(请求地址，请求数据，列表展示函数给插件调用)
 *$("#pagebox").paging("www.aijia.com",{},pageList);
 */
$.extend($.fn, {
    paging: function (action, postData, pageList, pageindex) {//（请求地址，请求参数，列表展示函数）
        var that = $(this);
        var output = {};
        var pageCount = 10; //总页数
        var pageIndex = 1;//当前页
        var pageTotal = 100;//总条数
        var pageperCount = 10;//每页多少条
        pageperCount = postData.pageSize;
        //绑定插件初始加载第一页
        pageIndex = pageindex;
        paginbyIndex(pageIndex);
        //分页获取数据的方法
        function getData(index) {
            postData = $.extend(postData, { pageIndex: pageIndex })
            $.ajax({
                type: "post", //使用get方法访问后台
                dataType: "json", //返回json格式的数据
                url: action, //要访问的后台地址
                async: false,
                data: postData, //要发送的数据
                ajaxStart: function () { /*$("#load").show(); */
                },
                complete: function () { /*$("#load").hide();*/
                }, //AJAX请求完成时隐藏loading提示
                success: function (msg) {//msg为返回的数据，在这里做数据绑定
                    jsonData = eval(msg);
                    //if (pageIndex == 1) {
                    if (typeof (jsonData) != "undefined")
                    {
                        if (!msg.IsOk)
                        {
                            alert(msg.Msg);
                            return;
                        }
                        var data = eval(msg.Data);
                        pageTotal = jsonData.Total;
                        pageCount = Math.ceil(pageTotal / pageperCount);
                        var statistics = {};
                        if (msg.Msg == "" || typeof (msg.Msg) == "undefined") {
                            statistics = {};
                        } else if (isJson(msg.Msg)) {
                            statistics = msg.Msg;
                        } else {
                            statistics = JSON.parse(msg.Msg);
                        }
                        pageList(data, pageTotal, pageCount, pageIndex, statistics);
                        creatPaginBar();
                    }
                   
                }
            });

        }
        //判断是否是json数据
        function isJson(obj) {
            var isjson = typeof (obj) == "object" && Object.prototype.toString.call(obj).toLowerCase() == "[object object]" && !obj.length;
            return isjson;
        }
        //下一页事件
        function nextPage() {
            if (pageIndex >= pageCount) {
                pageIndex = pageCount;
            } else {
                pageIndex++;
            }
            getData(pageIndex);

        }
        //上一页
        function upPage() {
            if (pageIndex == 1) {
                pageIndex = 1;
            } else {
                pageIndex--;
            }
            getData(pageIndex);

        }
        //当前页
        function paginbyIndex(index) {
            pageIndex = parseInt(index) > 0 ? index : 1;
            getData(pageIndex);

        }
        function creatPaginBar() {
            $(that).html("");
            if (pageCount <= 0) {
                $(that).html("没有找到相应的数据");
                return;
            }
            var html = "";
            //如果当前页是第一页，则上一页不能操作
            if (pageIndex == 1) {
                html += '<li class="prev disabled"><a href="#">← Prev</a></li>';
            } else {
                html += '<li class="prev page-up-action"><a href="#">← Prev</a></li>';
            }
            if (pageCount <= 8) {
                //如果总页数小于等于8条，全部显示
                for (var i = 1; i <= pageCount; i++) {
                    if (pageIndex == i) {
                        html += '<li class="active" data=' + i + '><a href="#">' + i + '</a></li>';
                    } else {
                        html += ' <li class="page-num" data=' + i + '><a href="#">' + i + '</a></li>';
                    }

                }
            } else {
                if (pageIndex < 6) {
                    //如果当前页少于6，前面显示6个
                    for (var i = 1; i <= 6; i++) {
                        if (pageIndex == i) {
                            html += '<li class="active" data=' + i + '><a href="#">' + i + '</a></li>';
                        } else {
                            html += ' <li class="page-num" data=' + i + '><a href="#">' + i + '</a></li>';
                        }
                    }
                    //第七个显示。。。
                    html += '<li class="active"><a href="#">...</a></li>';
                    //最后一个
                    html += ' <li class="page-num" data=' + pageCount + '><a href="#">' + pageCount + '</a></li>';
                } else if (pageIndex > (pageCount - 4)) {
                    //如果当前页在最后的5个里，最后5个都显示
                    html += ' <li class="page-num"  data="1" ><a href="#">' + 1 + '</a></li>';
                    html += ' <li class="page-num" data="2" ><a href="#">' + 2 + '</a></li>';
                    html += '<li class="active"><a href="#">...</a></li>';
                    for (var i = pageCount - 4; i <= pageCount; i++) {
                        if (i == pageIndex) {
                            html += '<li class="active" data=' + i + '><a href="#">' + i + '</a></li>';
                        } else {
                            html += ' <li class="page-num" data=' + i + '><a href="#">' + i + '</a></li>';
                        }
                    }
                } else {
                    html += ' <li class="page-num" data="1"><a href="#">' + 1 + '</a></li>';
                    html += ' <li class="page-num" data="2"><a href="#">' + 2 + '</a></li>';
                    html += ' <li class="active"><a href="#">...</a></li>';
                    var q = pageIndex - 1;
                    var h = pageIndex + 1;
                    html += ' <li class="page-num" data=' + q + '><a href="#">' + q + '</a></li>';
                    html += '<li class="active"  data=' + pageIndex + '><a href="#">' + pageIndex + '</a></li>';
                    html += ' <li class="page-num" data=' + h + '><a href="#">' + h + '</a></li>';
                    //第七个显示。。。
                    html += '<li class="active"><a href="#">...</a></li>';
                    //最后一个
                    html += ' <li class="page-num" data=' + pageCount + '><a href="#">' + pageCount + '</a></li>';
                }
            }
            //下一页
            if (pageIndex == pageCount) {//最后一页，不能操作
                html += '<li class="next disabled" ><a href="#">Next →</a></li>';
            } else {
                html += '<li class="next page-next-action" ><a href="#">Next →</a></li>';
            }
            //html += '<span class="page-count">共' + pageCount + '页</span>';
            $(that).html(html);
            //上一页
            $(that).find(".page-up-action").on("click", function () {
                upPage();
            });
            //下一页
            $(that).find(".page-next-action").on("click", function () {
                nextPage();
            });
            //中间
            $(that).find(".page-num").on("click", function () {
                paginbyIndex(parseInt($(this).attr("data")));
            });
        }
        return output;
    }
});
var G_AddUserNameLink = function (uname, uid, userType) {
    if (uname != "") {
        var color = "black";
        if (userType > 0)
            color = "#" + colors[userType - 1];
        return "<a href='javascript:void(0)' style='color:" + color + "' onclick=\"openWindowOwn('/Account/AccountsInfo?param=" + uid + "', '" + uid + "', 800, 800)\">" + uname + "</a>";
    }
    else {
        return "";
    }
}

function toNum(num) {
    var result = '', counter = 0;
    num = (num || 0).toString();
    for (var i = num.length - 1; i >= 0; i--) {
        counter++;
        result = num.charAt(i) + result;
        if (!(counter % 3) && i != 0) { result = ',' + result; }
    }
    return result;
}

function StrToDateTime(timestr) {
    var dt = new Date(timestr.replace("-", "/").replace("-", "/"));
    return dt;
}
$(function () {
    //给动态的子元素添加事件
    $('tbody').on('mouseover', 'tr', function () {
        $(this).addClass("over");
    });
    $('tbody').on('mouseout', 'tr', function () {
        $(this).removeClass("over");
    });
})
