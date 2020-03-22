<template>
  <a-locale-provider :locale="locale">
    <div id="app">
      <router-view :key="key" />
    </div>
  </a-locale-provider>
</template>

<script>
  import zhCN from 'ant-design-vue/lib/locale-provider/zh_CN'
  import { AppDeviceEnquire } from '@/utils/mixin'

  export default {
    mixins: [AppDeviceEnquire],
    data() {
      return {
        locale: zhCN
      }
    },
    computed: {
      key() {
        let d = new Date()
        let time = d.getMonth() + '_' + d.getDay() + '_' + d.getHours() + '_' + d.getSeconds()
        return (this.$route.name || this.$route.path) + time
      }
    },
    mounted() {

    }
  }
</script>
<style>
  #app {
    height: 100%;
  }

</style>
