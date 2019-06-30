<template>
  <div class="logo">
    <router-link :to="{name:'dashboard'}">
        <!-- <LogoSvg alt="logo" /> -->
        <h1 style=""  v-if="showTitle">{{ title }}</h1>
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
