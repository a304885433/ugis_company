<template>
    <a-form @submit="handleSubmit"
            :form="form"
            class="form">
        <a-row class="form-row"
               :gutter="16">
            <a-col :md="12"
                   :sm="24">
                <a-form-item label="企业">
                    <a-select placeholder="请选择企业"
                              v-decorator="[ 'companyId',
                          {rules: [{ required: true, message: '请选择责任人'}]}
                        ]">
                        <a-select-option v-for="dic in shoujiDicArr"
                                         :key="dic.id"
                                         :value="dic.id">{{dic.name}}</a-select-option>
                    </a-select>
                </a-form-item>
            </a-col>
            <a-col :md="12"
                   :sm="24">
                <a-form-item label="排查点位">
                    <a-select placeholder="排查点位"
                              mode="multiple"
                              v-decorator="[ 'chkPointId',
                          {rules: [{ required: true, message: '请选择排查点位'}]}
                        ]">
                        <a-select-option v-for="dic in dianweiDicArr"
                                         :key="dic.id"
                                         :value="dic.id">{{dic.name}}</a-select-option>
                    </a-select>
                </a-form-item>
            </a-col>
        </a-row>
    </a-form>
</template>

<script>
    export default {
        name: 'TaskForm',
        props: {
            showSubmit: {
                type: Boolean,
                default: false
            },
            dianweiDicArr: {
                type: Array
            }
        },
        data() {
            return {
                form: this.$form.createForm(this)
            }
        },
        methods: {
            handleSubmit(e) {
                e.preventDefault()
            },
            edit(record) {
                this.form.resetFields()
                setTimeout(() => {
                    this.form.setFieldsValue({ ...record })
                }, 50);
            },
        }
    }
</script>

<style scoped>


</style>
