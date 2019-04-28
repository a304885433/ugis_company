<template>
    <a-form @submit="handleSubmit"
            :form="form"
            class="form">

        <a-form-item v-for="(k, index) in form.getFieldValue('keys')"
                     :key="k"
                     :required="false">
            <a-input v-decorator="[
       `names[${k}]`,
       {
         validateTrigger: ['change', 'blur'],
         preserve: true,
         rules: [{
           required: true,
           whitespace: true,
           message: 'Please input passenger\'s name or delete this field.',
         }],
       }
     ]"
                     placeholder="passenger name"
                     style="width: 60%; margin-right: 8px" />
            <a-icon v-if="form.getFieldValue('keys').length > 1"
                    class="dynamic-delete-button"
                    type="minus-circle-o"
                    :disabled="form.getFieldValue('keys').length === 1"
                    @click="() => remove(k)" />
        </a-form-item>

        <!-- <a-table :columns="columns"
                 :dataSource="data"
                 :pagination="false"
                 :loading="memberLoading">
            <template slot="medTypeId"
                      slot-scope="text, record, index">
                <a-form-item>
                    <a-input v-decorator="[
                      `names[${index}]`,
                      {
                        validateTrigger: ['change', 'blur'],
                        preserve: true,
                        rules: [{
                          required: true,
                          whitespace: true,
                          message: 'Please input passenger\'s name or delete this field.',
                        }],
                      }
                    ]"
                             style="width: 100%; margin-right: 8px" />
                </a-form-item>
            </template>
            <template slot="operation"
                      slot-scope="text, record">
                <a-popconfirm title="是否要删除此行？"
                              @confirm="remove(record.key)">
                    <a>删除</a>
                </a-popconfirm>
            </template>
        </a-table> -->
        <a-button style="width: 100%; margin-top: 16px; margin-bottom: 8px"
                  type="dashed"
                  icon="plus"
                  @click="handleAdd">新增</a-button>
        </a-card>
        <a-form-item>
            <a-button type="primary"
                      html-type="submit">
                Submit
            </a-button>
        </a-form-item>
    </a-form>
</template>

<script>
    export default {
        name: 'YaoPin',
        props: {
            yaopinDicArr: Array
        },
        components: {
        },
        data() {
            return {
                form: null,
                memberLoading: false,
                columns: [
                    {
                        title: '药品',
                        dataIndex: 'medTypeId',
                        width: '20%',
                        scopedSlots: { customRender: 'medTypeId' }
                    },
                    {
                        title: '月购置量',
                        dataIndex: 'monthAmmount',
                        width: '30%',
                        scopedSlots: { customRender: 'monthAmmount' }
                    },
                    {
                        title: '操作',
                        key: 'action',
                        scopedSlots: { customRender: 'operation' }
                    }
                ],
                // yaopinDicArr: [],
                data: [],
                errors: []
            }
        },
        created() {
            this.form = this.$form.createForm(this);
            this.form.getFieldDecorator('keys', { initialValue: [], preserve: true });
            this.form.getFieldDecorator('names', { initialValue: [], preserve: true });
        },
        methods: {
            handleSubmit(e) {
                e.preventDefault()
                this.form.validateFields((err, values) => {
                    debugger
                    if (!err) {
                        this.$notification['error']({
                            message: 'Received values of form:',
                            description: values
                        })
                    }
                })
            },
            handleAdd() {
                const { form } = this;
                // can use data-binding to get
                const keys = form.getFieldValue('keys');
                const nextKeys = keys.concat(keys.length);
                // can use data-binding to set
                // important! notify form to detect changes
                form.setFieldsValue({
                    keys: nextKeys,
                });

                const length = this.data.length
                const key = length === 0 ? '1' : (parseInt(this.data[length - 1].key) + 1).toString()
                this.data.push({
                    key,
                    medTypeId: null,
                    monthAmmount: null,
                })

                // this.form.getFieldDecorator(`waterTypeId$`, {
                //     initialValue: null, preserve: true,
                //     rules: [{ required: true, message: '请输入企业名称', whitespace: true }]
                // });
                // this.form.getFieldDecorator(`monthAmmount${key}`, {
                //     initialValue: null, preserve: true,
                //     rules: [{ required: true, message: '请输入企业名称', whitespace: true }]
                // });

                // this.$nextTick(() => {
                //     this.form.validateFields((err, values) => {
                //         var d = this.data
                //         debugger
                //         if (err) {
                //             reject(err)
                //             return
                //         }
                //         resolve(values)
                //     })
                // })
            },
            remove(key) {
                const { form } = this;
                // can use data-binding to get
                const keys = form.getFieldValue('keys');
                // We need at least one passenger
                if (keys.length === 1) {
                    return;
                }

                // can use data-binding to set
                form.setFieldsValue({
                    keys: keys.filter(key => key !== k),
                });
            },
            saveRow(record) {
                this.memberLoading = true
                const { key, name, workId, department } = record
                if (!name || !workId || !department) {
                    this.memberLoading = false
                    this.$message.error('请填写完整成员信息。')
                    return
                }
                // 模拟网络请求、卡顿 800ms
                new Promise((resolve) => {
                    setTimeout(() => {
                        resolve({ loop: false })
                    }, 800)
                }).then(() => {
                    const target = this.data.filter(item => item.key === key)[0]
                    target.editable = false
                    target.isNew = false
                    this.memberLoading = false
                })
            },
            toggle(key) {
                const target = this.data.filter(item => item.key === key)[0]
                target.editable = !target.editable
            },
            getRowByKey(key, newData) {
                const data = this.data
                return (newData || data).filter(item => item.key === key)[0]
            },
            cancel(key) {
                const target = this.data.filter(item => item.key === key)[0]
                target.editable = false
            },
            handleChange(value, key, column) {
                const newData = [...this.data]
                const target = newData.filter(item => key === item.key)[0]
                if (target) {
                    target[column] = value
                    this.data = newData
                }
            }

        }
    }
</script>

<style lang="less"
       scoped>
    .card {
        margin-bottom: 24px;
    }

    .popover-wrapper {
        /deep/ .antd-pro-pages-forms-style-errorPopover .ant-popover-inner-content {
            min-width: 256px;
            max-height: 290px;
            padding: 0;
            overflow: auto;
        }
    }

    .antd-pro-pages-forms-style-errorIcon {
        user-select: none;
        margin-right: 24px;
        color: #f5222d;
        cursor: pointer;
        i {
            margin-right: 4px;
        }
    }

    .antd-pro-pages-forms-style-errorListItem {
        padding: 8px 16px;
        list-style: none;
        border-bottom: 1px solid #e8e8e8;
        cursor: pointer;
        transition: all .3s;

        &:hover {
            background: #e6f7ff;
        }
        .antd-pro-pages-forms-style-errorIcon {
            float: left;
            margin-top: 4px;
            margin-right: 12px;
            padding-bottom: 22px;
            color: #f5222d;
        }
        .antd-pro-pages-forms-style-errorField {
            margin-top: 2px;
            color: rgba(0, 0, 0, .45);
            font-size: 12px;
        }
    }

</style>
