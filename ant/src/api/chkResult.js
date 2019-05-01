import { axios } from '@/utils/request'

export function SaveForEdit(data) {
    return axios({
        url: `/services/app/ChkResult/SaveForEdit`,
        method: 'post',
        data
    })
}

export function GetCustomAll(params) {
    return axios({
        url: `/services/app/ChkResult/GetCustomAll`,
        method: 'get',
        params
    })
}
