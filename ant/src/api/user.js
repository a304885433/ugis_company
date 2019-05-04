import { axios } from '@/utils/request'

export function ChangePassword(data) {
    return axios({
        url: `/services/app/User/ChangePassword`,
        method: 'post',
        data
    })
}

export function ResetPassword(data) {
    return axios({
        url: `/services/app/User/ResetPassword`,
        method: 'post',
        data
    })
}
