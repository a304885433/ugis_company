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
                          {rules: [{ required: true, message: '请选择企业'}]}
                        ]"
                              @change="handleCompanyChange">
                        <a-select-option v-for="item in companyArr"
                                         :key="item.id"
                                         :chkPointId="item.chkPointIdList"
                                         @change="handleCompanyChange"
                                         :value="item.id">{{item.name}}</a-select-option>
                    </a-select>
                </a-form-item>
            </a-col>
            <a-col :md="12"
                   :sm="24">
                <a-form-item label="排查点位">
                    <a-select placeholder="排查点位"
                              v-decorator="[ 'chkPointId',
                          {rules: [{ required: true, message: '请选择排查点位'}]}
                        ]">
                        <a-select-option v-for="dic in chkPointArr"
                                         :key="dic.id"
                                         :value="dic.id">{{dic.name}}</a-select-option>
                    </a-select>
                </a-form-item>
            </a-col>
        </a-row>

        <a-row class="form-row"
               :gutter="16">
            <a-col :md="12"
                   :sm="24">
                <a-form-item label="记录日期">
                    <a-date-picker style="width: 100%"
                                   placeholder="请输入记录日期"
                                   v-decorator="[ 'chkDate', {rules: [{ required: true, message: '请输入记录日期'}]} ]" />
                </a-form-item>
            </a-col>
            <a-col :md="12"
                   :sm="24">
                <a-form-item label="记录员">
                    <a-input placeholder="记录员"
                             v-decorator="[ 'chkUserName',
                                  {rules: [{ required: true, message: '请输入记录员'}]}
                                ]">
                    </a-input>
                </a-form-item>
            </a-col>
        </a-row>
        <a-form-item label="排查批次">
                <a-input placeholder="排查批次"
                         v-decorator="[ 'chkBatch']">
                </a-input>
            </a-form-item>
    </a-form>
</template>

<script>
    import { Dic, CompanyInfo } from '@/api/'
    import Bus from '@/views/core/bus'
    import moment from 'moment'
    export default {
        name: 'TaskForm',
        props: {
            showSubmit: {
                type: Boolean,
                default: false
            },
            companyArr: {
                type: Array
            }
        },
        data() {
            return {
                form: this.$form.createForm(this),
                chkPointArr: [],
                poluTypeArr: [],
            }
        },
        created(){
            this.form.getFieldDecorator('chkUserName', {
                initialValue: this.$store.getters.nickname
            })
            this.form.getFieldDecorator('chkDate', {
                initialValue: moment()
            })
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
            handleCompanyChange(companyId, node) {
                var ids = node.data.attrs.chkPointId || '0'
                this.form.setFieldsValue({
                    chkPointId: null,
                })
                this.getChkPoint(ids)
                this.getPoluType(companyId)
            },
            getChkPoint(ids) {
                Dic.GetAllItem({
                    idList: ids
                }).then(res => {
                    this.chkPointArr = res.result.filter(t => t.typeCode == 'dianweixinxi')
                })
            },
            getPoluType(id) {
                CompanyInfo.GetPoluType({ id }).then(res => {
                    this.poluTypeArr = res.result
                    Bus.$emit('GetCompanyPoluType', this.poluTypeArr)
                })
            },
        }
    }
</script>

<style scoped>


</style>
