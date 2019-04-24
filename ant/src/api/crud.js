import { axios } from '@/utils/request'

export default class {

    constructor(entity) {
        this.entity = entity
    }

    Get(data) {
        return axios({
            url: `/services/app/${this.entity}/Get`,
            method: 'get',
            data
        })
    }

    GetAll(data) {
        return axios({
            url: `/services/app/${this.entity}/GetAll`,
            method: 'get',
            data
        })
    }

    Create(data) {
        return axios({
            url: `/services/app/${this.entity}/Create`,
            method: 'post',
            data
        })
    }

    Update(data) {
        return axios({
            url: `/services/app/${this.entity}/Update`,
            method: 'put',
            data
        })
    }

    Delete(data) {
        return axios({
            url: `/services/app/${this.entity}/Delete`,
            method: 'delete',
            data
        })
    }

} 