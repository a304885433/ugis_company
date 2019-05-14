import { asyncRouterMap, constantRouterMap } from '@/config/router.config'
import router from '@/router/index';

/**
 * 过滤账户是否拥有某一个权限，并将菜单从加载列表移除
 *
 * @param route
 * @returns {boolean}
 */
function hasPermission (route) {
  if (route.meta && route.meta.permission) {
  for(let p of route.meta.permission){
      if(abp.auth.hasPermission(p)){
        return true
      }
    }
    return false
  }
  return true
}

function filterAsyncRouter (routerMap) {
  const accessedRouters = routerMap.filter(route => {
    // 检查该路由权限
    if (hasPermission(route)) {
      // 所有子级长度
      let all = route.children ?  route.children.length : 0
      // 如果有权限就检查其子级
      if (route.children && route.children.length) {
        route.children = filterAsyncRouter(route.children)
        // 如果有子级，但是子级一个权限都不出来，那他也不出来
        if(all > 0 && route.children.length == 0) {
          return false
        }
      }
      return true
    }
    return false
  })
  return accessedRouters
}

const permission = {
  state: {
    routers: constantRouterMap,
    addRouters: []
  },
  mutations: {
    SET_ROUTERS: (state, routers) => {
      state.addRouters = routers
      // state.routers = routers
      state.routers = constantRouterMap.concat(routers)
    }
  },
  actions: {
    GenerateRoutes ({ commit } ) {
      return new Promise(resolve => {
        const accessedRouters = filterAsyncRouter(asyncRouterMap)
        commit('SET_ROUTERS', accessedRouters)
        resolve()
      })
    }
  }
}

export default permission
