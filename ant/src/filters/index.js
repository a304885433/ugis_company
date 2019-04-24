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