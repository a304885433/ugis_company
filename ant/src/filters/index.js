import appconst from '../lib/appconst'
import moment from 'moment'
import 'moment/locale/zh-cn'
moment.locale('zh-cn')

export function L(value, source, ...argus) {
    console.log(appconst)
    if (source) {
        return window.abp.localization.localize(value, source, argus);
    } else {
        return window.abp.localization.localize(value, appconst.localization.defaultLocalizationSourceName, argus);
    }
}
export function hasPermission(permissionName) {
    return window.abp.auth.hasPermission(permissionName);
}
export function hasAnyOfPermissions(...argus) {
    return window.abp.auth.hasAnyOfPermissions(...argus);
}
export function hasAllOfPermissions(...argus) {
    return window.abp.auth.hasAllOfPermissions(...argus);
}

// 在数组中查找对象，用于转换名称
export function showContent(value, arr, key_field, value_field) {
    if (value == null) return null
    if (!arr || !arr.length) return null
    let item = _.find(arr, item => item[key_field] == value)
    if (!item) return null
    return item[value_field]
}

export function numberFormat(value) {
    if (!value) {
        return '0'
    }
    const intPartFormat = value.toString().replace(/(\d)(?=(?:\d{3})+$)/g, '$1,') // 将整数部分逢三一断
    return intPartFormat
}

export function dateFormat(dataStr, pattern = 'YYYY-MM-DD') {
    return moment(dataStr).format(pattern)
}
