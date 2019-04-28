<template>
  <a-form @submit="handleSubmit"
          :form="form"
          class="form">
    <a-row class="form-row"
           :gutter="16">
      <a-col :md="6"
             :sm="24">
        <a-form-item label="记录日期">
          <a-date-picker style="width: 100%"
                         placeholder="请输入记录日期"
                         v-decorator="[ 'dischargeDate', {rules: [{ required: true, message: '请输入记录日期'}]} ]" />
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="24">
        <a-form-item label="污水排放频次(每月)">
          <a-input-number placeholder="请输入污水排放频次"
                          style="width: 100%"
                          v-decorator="[ 'monthResTimes', {rules: [{ required: true, message: '请输入污水排放频次' }]} ]" />
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="24">
        <a-form-item label="单次排放量">
          <a-input placeholder="请输入单次排放量"
                   v-decorator="[ 'monthResAmount', {rules: [{ required: true, message: '请输入单次排放量'}]} ]">
            <span slot="addonAfter"
                  style="width: 10px">吨
            </span>
          </a-input>
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="24">
        <a-form-item label="月排放量">
          <a-input placeholder="请输入月排放量"
                   v-decorator="[ 'monthTotalAmount', {rules: [{ required: true, message: '请输入月排放量'}]} ]">
            <span slot="addonAfter"
                  style="width: 10px">吨
            </span>
          </a-input>
        </a-form-item>
      </a-col>
    </a-row>
    <a-row class="form-row"
           :gutter="16">
      <a-col :md="12"
             :sm="24">
        <a-form-item label="排放方式">
          <a-select placeholder="请选择排放方式"
                    v-decorator="['emissionTypeID',
                                {rules: [{ required: true, message: '请选择排放方式'}]}
                              ]">
            <a-select-option v-for="dic in paifangDicArr"
                             :key="dic.id"
                             :value="dic.id">{{dic.name}}</a-select-option>
          </a-select>
        </a-form-item>
      </a-col>
      <a-col :md="12"
             :sm="24">
        <a-form-item label="收集方式">
          <a-select placeholder="请选择收集方式"
                    v-decorator="[ 'collTypeID',
                    {rules: [{ required: true, message: '请选择责任人'}]}
                  ]">
            <a-select-option v-for="dic in shoujiDicArr"
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
      paifangDicArr: {
        type: Array
      },
      shoujiDicArr: {
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
