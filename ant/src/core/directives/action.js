import Vue from 'vue'
import store from '@/store'

/**
 * Action 权限指令
 * 指令用法：
 *  - 在需要控制 action 级别权限的组件上使用 v-action:[method] , 如下：
 *    <i-button v-action:add >添加用户</a-button>
 *    <a-button v-action:delete>删除用户</a-button>
 *    <a v-action:edit @click="edit(record)">修改</a>
 *
 *  - 当前用户没有权限时，组件上使用了该指令则会被隐藏
 *  - 当后台权限跟 pro 提供的模式不同时，只需要针对这里的权限过滤进行修改即可
 *
 *  @see https://github.com/sendya/ant-design-pro-vue/pull/53
 */
const action = Vue.directive('action', {
  inserted: function (el, binding, vnode) {
    // 读取路由定义的权限
    const routerPermissions = vnode.context.$route.meta.permission
    const permissions = typeof permissions === 'string' && [routerPermissions] || routerPermissions
    // 读取当前动作点的权限
    let actions = []
    if (binding.arg) {
      actions.push(binding.arg)
    }
    if (typeof binding.value === 'string') {
      actions.push(binding.value)
    }
    else if (binding.value instanceof Array) {
      actions = actions.concat(binding.value)
    }
    let hasPermission = false
    for (let p of permissions) {
      for (let actionName of actions) {
        let ap = `${p}.${actionName}`
        if (abp.auth.hasPermission(ap)) {
          hasPermission = true
          break
        }
      }
      if(hasPermission){
        break
      }
    }
    if (!hasPermission) {
      el.parentNode && el.parentNode.removeChild(el) || (el.style.display = 'none')
    }
  }
})

export default action
