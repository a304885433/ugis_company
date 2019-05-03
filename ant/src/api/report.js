import { axios } from '@/utils/request'

export function GetReport1(params) {
    return axios({
        url: `/services/app/Report/GetReport1`,
        method: 'get',
        params
    })
}

export function GetReport2(params) {
    return axios({
        url: `/services/app/Report/GetReport2`,
        method: 'get',
        params
    })
}

export function GetReport3(params) {
    return axios({
        url: `/services/app/Report/GetReport3`,
        method: 'get',
        params
    })
}

export function GetReport4(params) {
    return axios({
        url: `/services/app/Report/GetReport4`,
        method: 'get',
        params
    })
}

export function GetReport5(params) {
    return axios({
        url: `/services/app/Report/GetReport5`,
        method: 'get',
        params
    })
}