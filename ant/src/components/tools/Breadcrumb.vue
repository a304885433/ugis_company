<template>
  <a-breadcrumb class="breadcrumb">
    <a-breadcrumb-item v-for="(item, index) in breadList"
                       :key="item.name">
      <router-link v-if="item.name != name && index != 1"
                   :to="{ path: item.path === '' ? '/' : item.path }">{{ item.meta.title }}</router-link>
      <span v-else>{{ item.meta.title }}</span>
    </a-breadcrumb-item>
  </a-breadcrumb>
</template>

<script>
  export default {
    data() {
      return {
        name: '',
        breadList: []
      }
    },
    created() {
      this.getBreadcrumb()
    },
    methods: {
      getBreadcrumb() {
        this.breadList = []
        this.name = this.$route.name
        this.$route.matched.forEach((item, index) => {
          // item.name !== 'index' && this.breadList.push(item)
          // 嵌套路由，有路由用来做Index，不展示上面包屑的名称
          let prev =  this.$route.matched[index - 1]
          if (!prev || (prev.redirect != item.path)) {
            this.breadList.push(item)
          }
        })
      }
    },
    watch: {
      $route() {
        this.getBreadcrumb()
      }
    }
  }
</script>

<style scoped>


</style>
