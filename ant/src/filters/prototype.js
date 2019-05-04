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