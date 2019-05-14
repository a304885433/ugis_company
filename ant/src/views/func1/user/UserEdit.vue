<template>
    <div>

        <a-form :form="form"
                class="form"
                :submit="handleSubmit">
            <a-form-item label="用户名">
                <a-input placeholder="请输入用户名"
                         v-decorator="['userName',
                                    {rules: [{ required: true, message: '请输入用户名', whitespace: true}]}
                                              ]" />
            </a-form-item>

            <a-form-item label="姓名">
                <a-input placeholder="请输入姓名"
                         v-decorator="['name',
                                        {rules: [{ required: true, message: '请输入姓名', whitespace: true}]}
                                                  ]" />
            </a-form-item>

            <a-form-item label="电话">
                <a-input placeholder="请输入电话"
                         v-decorator="['phoneNumber',
                                            {rules: [{ required: true, message: '请输入电话', whitespace: true}]}
                                                      ]" />
            </a-form-item>

            <a-form-item label="邮箱">
                <a-input placeholder="请输入邮箱"
                         v-decorator="['emailAddress',
                                                {rules: [{ required: true, type: 'email', message: '请输入邮箱', whitespace: true}]}
                                                          ]" />
            </a-form-item>

            <a-form-item label="密码"
                         v-if="this.mdl == null">
                <a-input placeholder="请输入密码"
                         type="password"
                         v-decorator="['password',
                                                    {rules: [{ required: true,  message: '请输入密码', whitespace: true}]}
                                                              ]" />
            </a-form-item>

            <a-form-item label="角色名称">
                <a-select placeholder="请选择角色"
                          v-decorator="[
                    'roleName',
                    {rules: [{ required: true, message: '请选择角色'}]}
                  ]">
                    <a-select-option v-for="item in roles"
                                     :key="item.name"
                                     :value="item.name">{{item.displayName}}</a-select-option>
                </a-select>
            </a-form-item>

        </a-form>

        <!-- fixed footer toolbar -->
        <footer-tool-bar :style="{ width: isSideMenu() && isDesktop() ? `calc(100% - ${sidebarOpened ? 256 : 80}px)` : '100%'}">
            <a-button type="primary"
                      @click="handleSubmit"
                      v-action="['create','update']"
                      style="margin-right: 10px;"
                      :loading="loading">提交</a-button>
            <a-button type="default"
                      @click="$router.go(-1)">取消</a-button>
        </footer-tool-bar>
    </div>
</template>

<script>
    import FooterToolBar from '@/components/FooterToolbar'
    import { mixin, mixinDevice } from '@/utils/mixin'
    import { User, Role } from '@/api/'

    export default {
        name: 'UserEdit',
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
                roles: [],
            }
        },
        created() {
            this.getRoles()
            this.getData()
        },
        methods: {
            getRoles() {
                Role.GetRolesAsync().then(res => {
                    this.roles = res.result.items
                })
            },
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
            handleSubmit() {
                this.form.validateFields((err, values) => {
                    if (err) {
                        return
                    }
                    this.loading = true
                    values.surname = values.userName
                    values.isActive = 1
                    values.RoleNames = [values.roleName]
                    delete values.roleName
                    if (!this.mdl) {
                        User.Create(values).then(() => {
                            this.$router.go(-1)
                        }).catch((err) => {
                            let res = err.response.data
                            this.$message.error(res.error.message)
                        }).finally(() => {
                            this.loading = false
                        })
                    } else {
                        let data = Object.assign(this.mdl, values)
                        User.Update(data).then(() => {
                            this.$router.go(-1)
                        }).catch((err) => {
                            let res = err.response.data
                            this.$message.error(res.error.message)
                        }).finally(() => {
                            this.loading = false
                        })
                    }
                })
            }
        }
    }

</script>
