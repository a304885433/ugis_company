import { axios } from '@/utils/request'

export default class {

    constructor(entity) {
        this.entity = entity
    }

    Get(params) {
        return axios({
            url: `/services/app/${this.entity}/Get`,
            method: 'get',
            params
        })
    }

    GetAll(params) {
        return axios({
            url: `/services/app/${this.entity}/GetAll`,
            method: 'get',
            params
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

    Save(data, key='id') {
        if(data.id){
            return this.Update(data)
        }else{
            return this.Create(data)
        }
    }

    Delete(params) {
        return axios({
            url: `/services/app/${this.entity}/Delete`,
            method: 'delete',
            params
        })
    }

} 