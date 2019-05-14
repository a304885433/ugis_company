import Vue from 'vue'
import { login, getInfo, logout } from '@/api/login'
import { welcome } from '@/utils/util'
import Util from '../../lib/util'
import appconst from '../../lib/appconst'

const user = {
  state: {
    token: '',
    name: '',
    welcome: '',
    avatar: '',
    roles: [],
    info: {}
  },

  mutations: {
    SET_TOKEN: (state, token) => {
      state.token = token
    },
    SET_NAME: (state, { name, welcome }) => {
      state.name = name
      state.welcome = welcome
    },
    SET_AVATAR: (state, avatar) => {
      state.avatar = avatar
    },
    SET_ROLES: (state, roles) => {
      state.roles = roles
    },
    SET_INFO: (state, info) => {
      state.info = info
    }
  },

  actions: {
    // 登录
    Login({ commit }, userInfo) {
      return new Promise((resolve, reject) => {
        login(userInfo).then(response => {
          const result = response.result

          // 设置过期时间
          var tokenExpireDate = (new Date(new Date().getTime() + 1000 * result.expireInSeconds))
          Util.abp.auth.setToken(result.accessToken, tokenExpireDate);
          Util.abp.utils.setCookieValue(appconst.authorization.encrptedAuthTokenName, result.encryptedAccessToken, tokenExpireDate, Util.abp.appPath)

          commit('SET_TOKEN', result.accessToken)
          resolve()
        }).catch(error => {
          reject(error.response.data.error)
        })
      })
    },

    // 获取用户信息
    GetInfo({ commit }) {
      return new Promise((resolve, reject) => {
        getInfo().then(response => {
      const result = response.result
          commit('SET_ROLES', {
            permissions: [],
            permissionList: []
          })
          commit('SET_INFO', result)

          commit('SET_NAME', { name: result.user.userName, welcome: welcome() })
          commit('SET_AVATAR', result.avatar)

          resolve(response)
        }).catch(error => {
          reject(error)
        })
      })
    },

    // 登出
    Logout({ commit, state }) {
      return new Promise((resolve) => {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        Util.abp.auth.clearToken()
        resolve()
      })
    }

  }
}

export default user
