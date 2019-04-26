<template>
  <a-form @submit="handleSubmit"
          :form="form"
          class="form">
    <a-row class="form-row"
           :gutter="16">
      <a-col :md="6"
             :sm="24">
        <a-form-item label="记录日期">
          <a-input placeholder="请输入任务名称"
                   v-decorator="[ 'name2', {rules: [{ required: true, message: '请输入任务名称', whitespace: true}]} ]" />
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="24">
        <a-form-item label="污水排放频次(每月)">
          <a-input placeholder="请输入任务名称"
                   v-decorator="[ 'name2', {rules: [{ required: true, message: '请输入任务名称', whitespace: true}]} ]" />
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="24">
        <a-form-item label="单次排放量">
          <a-input placeholder="请输入任务描述"
                   v-decorator="[ 'url2', {rules: [{ required: true, message: '请输入任务描述', whitespace: true}]} ]">
            <span slot="addonAfter"
                  style="width: 10px">吨
            </span>
          </a-input>
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="24">
        <a-form-item label="月排放量">
          <a-input placeholder="请输入任务描述"
                   v-decorator="[ 'url2', {rules: [{ required: true, message: '请输入任务描述', whitespace: true}]} ]">
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
                    v-decorator="[
              'approver2',
              {rules: [{ required: true, message: '请选择责任人'}]}
            ]">
            <a-select-option value="王伟">王伟</a-select-option>
            <a-select-option value="李红军">李红军</a-select-option>
          </a-select>
        </a-form-item>
      </a-col>
      <a-col :md="12"
             :sm="24">
        <a-form-item label="收集方式">
          <a-select placeholder="请选择收集方式"
                    v-decorator="[
       'approver2',
       {rules: [{ required: true, message: '请选择责任人'}]}
     ]">
            <a-select-option value="王伟">王伟</a-select-option>
            <a-select-option value="李红军">李红军</a-select-option>
          </a-select>
        </a-form-item>
      </a-col>
    </a-row>
    <a-form-item v-if="showSubmit">
      <a-button htmlType="submit">Submit</a-button>
    </a-form-item>
  </a-form>
</template>

<script>
  export default {
    name: 'TaskForm',
    props: {
      showSubmit: {
        type: Boolean,
        default: false
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
        this.form.validateFields((err, values) => {
          if (!err) {
            this.$notification['error']({
              message: 'Received values of form:',
              description: values
            })
          }
        })
      }
    }
  }
</script>

<style scoped>


</style>
