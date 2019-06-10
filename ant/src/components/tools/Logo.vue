<template>
  <div class="logo">
    <router-link :to="{name:'dashboard'}">
      <div style="width: 200px;margin-right: -35px;" v-if="showTitle">{{ title }}</div>
    </router-link>
  </div>
</template>

<script>
import LogoSvg from '@/assets/logo.svg?inline'

export default {
  name: 'Logo',
  components: {
    LogoSvg
  },
  props: {
    title: {
      type: String,
      default: '水污染源快速监测排查平台系统',
      required: false
    },
    showTitle: {
      type: Boolean,
      default: true,
      required: false
    }
  }
}
</script>
