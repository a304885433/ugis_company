import { axios } from '@/utils/request'

export function Xls(data) {
    return axios({
        url: `/File/Xls`,
        method: 'post',
        data
    })
}