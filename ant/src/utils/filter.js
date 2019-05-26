// 主要是封装vue组件最常用的过滤器，该过滤器同样会被挂载到vue实例方法上

import appconst from '../lib/appconst'
import moment from 'moment'
import 'moment/locale/zh-cn'
moment.locale('zh-cn')

export function L(value, source, ...argus) {
    if (source) {
        return window.abp.localization.localize(value, source, argus);
    } else {
        return window.abp.localization.localize(value, appconst.localization.defaultLocalizationSourceName, argus);
    }
}

/**
 * 根据value在数据源中查找值，返回对应的文本
 * @param {*} value 值
 * @param {*} arr 数据源数组
 * @param {*} value_field 值字段
 * @param {*} text_field 文本字段
 */
export function showContent(value, arr, value_field = 'id' , text_field = 'name') {
    if (value == null) return null
    if (!arr || !arr.length) return null
    let item = _.find(arr, item => item[value_field] == value)
    if (!item) return null
    return item[text_field]
}

/**
 * 格式化金额
 * @param {*} value 
 */
export function numberFormat(value) {
    if (!value) {
        return '0'
    }
    const intPartFormat = value.toString().replace(/(\d)(?=(?:\d{3})+$)/g, '$1,') // 将整数部分逢三一断
    return intPartFormat
}

/**
 * 格式化日期
 * @param {*} dataStr 
 * @param {*} pattern 
 */
export function dateFormat(dataStr, pattern = 'YYYY-MM-DD') {
    return moment(dataStr).format(pattern)
}
