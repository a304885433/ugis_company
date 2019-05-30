<template>
    <div class="page-header-index-wide">
        <iframe :src="src" style="width: 100%;overflow: hidden" frameborder="0" :height="height"></iframe>
    </div>
</template>

<script>
    import { PageView, RouteView } from '@/layouts'

    export default {
        name: 'Html',
        components: {
            RouteView,
            PageView
        },
        data() {
            return {
                src:'',
                height: null
                //http://${location.host}/api/file/download?id=${t.uid}&name=${t.name}
            }
        },
        created() {
            let html =  this.$route.meta.html
            this.src = `/api/file/html?name=${html}`
            this.height = Math.max(400, document.documentElement.clientHeight - 240)
        },
        methods: {

        }
    }
</script>
