<!-- <template>
    <div style="max-width: 800px">
        <a-divider v-if="isMobile()" />
        <div v-if="mdl.id">
            <h3>角色：{{ mdl.name }}</h3>
        </div>
        <a-form :form="form"
                :layout="isMobile() ? 'vertical' : 'horizontal'">
            <a-form-item label="角色编码">
                <a-input v-decorator="[ 'name', {rules: [{ required: true, message: '请输入角色编码!' }]} ]"
                            placeholder="请填写角色编码" />
            </a-form-item>

            <a-form-item label="角色名称">
                <a-input v-decorator="[ 'displayName', {rules: [{ required: true, message: '请输入角色名称!' }]} ]"
                         placeholder="请填写角色名称" />
            </a-form-item>

            <a-form-item label="备注说明">
                <a-textarea :row="3"
                            v-decorator="[ 'description' ]"
                            placeholder="请填写备注" />
            </a-form-item>

            <a-form-item label="拥有权限">
                <a-row :gutter="16"
                       v-for="(permission, index) in permissions"
                       :key="index">
                    <a-col :xl="4"
                           :lg="24">
                        {{ permission.name }}：
                    </a-col>
                    <a-col :xl="20"
                           :lg="24">
                        <a-checkbox v-if="permission.actionsOptions.length > 0"
                                    :indeterminate="permission.indeterminate"
                                    :checked="permission.checkedAll"
                                    @change="onChangeCheckAll($event, permission)">
                            全选
                        </a-checkbox>
                        <a-checkbox-group :options="permission.actionsOptions"
                                          v-model="permission.selected"
                                          @change="onChangeCheck(permission)" />
                    </a-col>
                </a-row>
            </a-form-item>
            <a-button type="primary"
                      @click="save"
                      v-action="['create','update']"
                      style="margin-right: 10px;">提交</a-button>
            <a-button type="default"
                      @click="$router.go(-1)">取消</a-button>
        </a-form>
    </div>
</template>

<script>
    import { Role } from '@/api/'
    import { mixinDevice } from '@/utils/mixin'
    import { actionToObject } from '@/utils/permissions'

    export default {
        name: 'RoleEdit',
        mixins: [mixinDevice],
        components: {},
        data() {
            return {
                form: this.$form.createForm(this),
                mdl: {},

                roles: [],
                permissions: []
            }
        },
        created() {
            this.init()
        },
        methods: {
            init() {
                let id = this.$route.query.id
                id = 3
                if (id) {
                    Role.GetRoleForEdit({id}).then(res =>{
                        this.mdl = Object.assign({}, res.result)
                        this.form.setFieldsValue({
                            name: this.mdl.role.name,
                            displayName: this.mdl.role.displayName,
                            description: this.mdl.role.description,
                        })
                        this.loadPermissions(this.mdl.permissions, this.mdl.grantedPermissionNames)
                    })
                } else {
                    Role.GetAllPermissions().then((res) => {
                        this.loadPermissions(res.result.items)
                    })
                }
            },
            test() {
                Role.GetRoleForEdit({ id: 2 }).then((res) => {
                    debugger
                })
            },
            edit(record) {
                this.mdl = Object.assign({}, record)
                // 有权限表，处理勾选
                if (this.mdl.permissions && this.permissions) {
                    // 先处理要勾选的权限结构
                    const permissionsAction = {}
                    this.mdl.permissions.forEach(permission => {
                        permissionsAction[permission.permissionId] = permission.actionEntitySet.map(entity => entity.action)
                    })

                    console.log('permissionsAction', permissionsAction)
                    // 把权限表遍历一遍，设定要勾选的权限 action
                    this.permissions.forEach(permission => {
                        const selected = permissionsAction[permission.id]
                        permission.selected = selected || []
                        this.onChangeCheck(permission)
                    })

                    console.log('this.permissions', this.permissions)
                }

                this.$nextTick(() => {
                    this.form.setFieldsValue(pick(this.mdl, 'id', 'name', 'status', 'describe'))
                })
                console.log('this.mdl', this.mdl)
            },

            onChangeCheck(permission) {
                permission.indeterminate = !!permission.selected.length && (permission.selected.length < permission.actionsOptions.length)
                permission.checkedAll = permission.selected.length === permission.actionsOptions.length
            },
            onChangeCheckAll(e, permission) {
                Object.assign(permission, {
                    selected: e.target.checked ? permission.actionsOptions.map(obj => obj.value) : [],
                    indeterminate: false,
                    checkedAll: e.target.checked
                })
            },
            loadPermissions(allpermissions, rolePermissions ) {
                // 处理层级
                let data = []
                for (let p of allpermissions) {
                    let index = p.name.lastIndexOf('.')
                    // 是根节点
                    if (index == -1) {
                        let children = allpermissions.filter(t => t.name.startsWith(p.name + '.'))
                        let selected  = rolePermissions && rolePermissions.filter(t=> t.startsWith(p.name + '.')) || []
                        let checkAll = children.length == selected.length
                        let indeterminate = (selected.length > 0 && selected.length < children.length) ? true: false
                        data.push({
                            name: p.displayName,
                            value: p.name,
                            checkedAll: checkAll,
                            selected: selected,
                            indeterminate: indeterminate,
                            actionsOptions: children.map(t => {
                                return {
                                    label: t.displayName,
                                    value: t.name
                                }
                            })
                        })
                    }
                }
                this.permissions = data
            },
            save() {
                this.form.validateFields((err, values) => {
                    if (err) {
                        return
                    }
                    // 读取权限
                    let permissions = []
                    for(let p of this.permissions){
                        if(p.selected && p.selected.length) {
                            permissions.push(p.value)
                            permissions = permissions.concat(p.selected)
                        }
                    }
                    values.permissions = permissions
                    if(this.mdl.role){
                        values.id = this.mdl.role.id
                        Role.Update(values).then(()=>{
                        this.$message.success('保存成功')
                         })
                    }else{
                        Role.Create(values).then(()=>{
                        this.$message.success('保存成功')
                    })
                    }
                })
                console.log(this.permissions)
            }
        }
    }
</script>

<style scoped>


</style> -->
