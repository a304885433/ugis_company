import Util from '../lib/util'
// 主要是封装vue组件最常用的方法，直接挂载在实例上

/**
 * 生成随机id，在不关注key的场景，用于指定列表等组件的唯一key
 */
export function newid() {
    var random = function () { // 生成10-12位不等的字符串
        return Number(Math.random().toString().substr(2)).toString(36); // 转换成十六进制
    }
    var arr = [];
    function createId() {
        var num = random();
        var _bool = false;
        arr.forEach(v => {
            if (v === num) _bool = true;
        });
        if (_bool) {
            createId();
        } else {
            return num
        }
    }
    return createId();
}

/**
 * 适用于下拉框控件，对文本做过滤搜索
 */
export function filterSelect(input, option) {
    return option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0
}

/**
 * 下载文件
 */
export function download(requestData, actionUrl = '/api/File/Xls') {
    var form = document.createElement("form")
    form.style.display = "none"
    form.action = actionUrl
    form.method = "post"
    form.target = '_blank'
    document.body.appendChild(form)

    let parms = {
        json: JSON.stringify(requestData)
    }
    const token = Util.abp.auth.getToken()
    if (!!token) {
        parms["Authorization"] = "Bearer " + token;
    }
    parms[".AspNetCore.Culture"] = Util.abp.utils.getCookieValue("Abp.Localization.CultureName");
    parms["Abp.TenantId"] = Util.abp.multiTenancy.getTenantIdCookie();

    for (var key in parms) {
        var input = document.createElement("input")
        input.type = "hidden"
        input.name = key
        input.value = parms[key]
        //input.value = encodeURIComponent(parms[key]);
        form.appendChild(input)
    }
    form.submit()
}
