import appconst from '../lib/appconst'
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

