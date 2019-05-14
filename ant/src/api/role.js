import { axios } from '@/utils/request'

export function GetAllPermissions(params) {
    return axios({
        url: `/services/app/Role/GetAllPermissions`,
        method: 'get',
        params
    })
}

export function GetRoleForEdit(params) {
    return axios({
        url: `/services/app/Role/GetRoleForEdit`,
        method: 'get',
        params
    })
}

export function GetRolesAsync(params) {
    return axios({
        url: `/services/app/Role/GetRolesAsync`,
        method: 'get',
        params
    })
}