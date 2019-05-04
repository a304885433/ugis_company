<template>
    <div>
        <a-form :form="form"
                @submit="handleSubmit">
            <a-form-item label="当前密码">
                <a-input placeholder="请输入当前密码"
                         type="password"
                         v-decorator="['currentPassword',
                                                    {rules: [{ required: true,  message: '请输入当前密码', whitespace: true}]}
                                                              ]" />
            </a-form-item>

            <a-form-item label="新密码">
                <a-input placeholder="请输入新密码"
                         type="password"
                         v-decorator="['newpassword',
                                                        {rules: [{ required: true,  message: '请输入新密码', whitespace: true}]}
                                                                  ]" />
            </a-form-item>

            <!-- <a-form-item label="再次输入">
                <a-input placeholder="请再次输入"
                         type="password"
                         v-decorator="['newpassword2',
                                                            {rules: [{ required: true,  message: '请请再次输入新密码', whitespace: true}]}
                                                                      ]" />
            </a-form-item> -->

            <!-- fixed footer toolbar -->
            <footer-tool-bar :style="{ width: isSideMenu() && isDesktop() ? `calc(100% - ${sidebarOpened ? 256 : 80}px)` : '100%'}">
                <a-button type="primary"
                          @click="handleSubmit"
                          html-type="submit"
                          style="margin-right: 10px;"
                          :loading="loading">提交</a-button>
                <a-button type="default"
                          @click="$router.go(-1)">取消</a-button>
            </footer-tool-bar>

        </a-form>

    </div>
</template>

<script>
    import FooterToolBar from '@/components/FooterToolbar'
    import { mixin, mixinDevice } from '@/utils/mixin'
    import { User } from '@/api/'

    export default {
        name: 'ChangePassword',
        mixins: [mixin, mixinDevice],
        components: {
            FooterToolBar,
        },
        data() {
            return {
                errors: [],
                form: this.$form.createForm(this),
                loading: false,
                mdl: null,
            }
        },
        created() {
            this.getData()
        },
        methods: {
            getData() {
                let id = this.$route.query.id
                if (!id) {
                    this.mdl = null
                    return
                }
                User.Get({ id }).then((res) => {
                    this.mdl = Object.assign({}, res.result)
                    this.form.setFieldsValue({ ...res.result })
                })
            },
            handleSubmit(e) {
                e.preventDefault()
                this.form.validateFields((err, values) => {
                    if (err) {
                        return
                    }
                    this.loading = true

                    User.ChangePassword(values).then(() => {
                        this.$message.success('修改成功')
                        this.$router.go(-1)
                    }).catch((err) => {
                        let res = err.response.data
                        this.$message.error(res.error.message)
                    }).finally(() => {
                        this.loading = false
                    })
                })
            }
        }
    }

</script>
