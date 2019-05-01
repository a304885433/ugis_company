// ie polyfill
import '@babel/polyfill'

import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/'
import { VueAxios, axios } from './utils/request'

import Util from './lib/util';

import bootstrap from './core/bootstrap'
import './core/use'
import './permission' // permission control
import './utils/filter' // global filter

Vue.config.productionTip = false

// mount axios Vue.$http and this.$http
Vue.use(VueAxios)

// 默认语言设置
if (!Util.abp.utils.getCookieValue('Abp.Localization.CultureName')) {
  let language = navigator.language;
  abp.utils.setCookieValue('Abp.Localization.CultureName', language, new Date(new Date().getTime() + 5 * 365 * 86400000), abp.appPath);
}

// 注册过滤器
import * as filters from './filters' // global filters
Object.keys(filters).forEach(key => {
  Vue.filter(key, filters[key])
  Vue.prototype[key] = filters[key]
})

// 读取默认设置
axios.get('/AbpUserConfiguration/GetAll').then(response => {
  Util.abp = Util.extend(true, Util.abp, response.result);
  new Vue({
    router,
    store,
    created() {
      bootstrap()
    },
    render: h => h(App)
  }).$mount('#app')

})

