/*
 *以下参数代表的意思：  
 *linkAlertWidth 弹窗的宽度
 *linkAlertTitleHeight 弹窗里面的弹窗头部的高度
 *linkAlertTitleBackgroundColor 弹窗头部的背景色
 *linkAlertContentHeight 弹窗主体内容的高度
 */
$.extend($.fn, {
	alertShow:function(obj) {
			// 调用遮罩层

		    alertHtml();
		    $("#alertWindowMask").show();
		    $("#alertWindowMask").css("height", $(document.body).height() + "px");
		    $("#linkAlert").css({
		    	"width": obj.linkAlertWidth
		    }).children("#linkAlertTitle").css({
		    	"height": obj.linkAlertTitleHeight,
		    	"backgroundColor": obj.linkAlertTitleBackgroundColor
		    }).siblings("#linkAlertContent").css({
		    	"height": obj.linkAlertContentHeight
		    });
		function alertHtml() {
				$("body").append("<div id=\"alertWindowMask\" ></div><div id=\"linkAlert\"><div class=\"clearfix\" id=\"linkAlertTitle\"><h3>头部信息</h3><span id=\"linkAlertOffBtn\" onclick='javascript:$(this).parents(\"#linkAlert\").remove();$(\"#alertWindowMask\").remove();'></span></div><div id=\"linkAlertContent\" ></div><div id=\"linkAlertBtn\" class=\"clearfix\"><button type=\"button\" class=\"btn btn-default btn-primary save-btn\">确认付款</button><button type=\"button\" class=\"btn btn-default btn-info\" onclick='javascript:$(this).parents(\"#linkAlert\").remove();$(\"#alertWindowMask\").remove();'>关闭</button></div></div>");
		}
	}
})

/*
 *以下参数代表的意思：  
 *linkAlertWidth 弹窗的宽度
 *linkAlertTitleHeight 弹窗里面的弹窗头部的高度
 *linkAlertTitleBackgroundColor 弹窗头部的背景色
 *linkAlertContentHeight 弹窗主体内容的高度
 */
function alertShowFn() {
	$(".alert-btn").alertShow({
		linkAlertWidth:"1000px",
		linkAlertTitleHeight: "34px",
		linkAlertTitleBackgroundColor:"#fff",
		linkAlertContentHeight:"500px",
	});
}    
