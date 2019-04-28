import { axios } from '@/utils/request'

export function GetForEdit(id) {
    return axios({
        url: `/services/app/CompanyInfo/GetForEdit`,
        method: 'get',
        params: {
            id
        }
    })
}

export function SaveForEdit(data) {
    return axios({
        url: `/services/app/CompanyInfo/SaveForEdit`,
        method: 'post',
        data
    })
}