<template>
  <a-modal title="操作"
           :width="600"
           :visible="visible"
           :confirmLoading="confirmLoading"
           @ok="handleOk"
           @cancel="handleCancel">
    <a-spin :spinning="confirmLoading">
      <a-form :form="form">
        <a-form-item :label="name">
          <a-input v-decorator="['name', {}]" />
        </a-form-item>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
  import { Dic } from '@/api/'
  export default {
    name: 'DicModal',
    data() {
      return {
        name: '',
        labelCol: {
          xs: { span: 24 },
          sm: { span: 5 }
        },
        wrapperCol: {
          xs: { span: 24 },
          sm: { span: 16 }
        },
        visible: false,
        confirmLoading: false,
        mdl: {}
      }
    },
    beforeCreate() {
      this.form = this.$form.createForm(this, {
        props: ['name', 'typeCode']
      })
    },
    created() {

    },
    methods: {
      setName(name) {
        this.name = name + '名称'
      },
      add(typeCode) {
        this.form.resetFields()
        this.edit({ typeCode })
      },
      edit(record) {
        this.mdl = Object.assign({}, record)
        this.visible = true
        this.$nextTick(() => {
          this.form.setFieldsValue({ ...record })
        })
      },
      close() {
        this.$emit('close')
        this.visible = false
      },
      handleOk() {
        // 触发表单验证
        this.form.validateFields((err, values) => {
          // 验证表单没错误
          if (err) {
            return
          }
          this.confirmLoading = true
          // 模拟后端请求 2000 毫秒延迟
          // new Promise((resolve) => {
          //   // setTimeout(() => resolve(), 2000)
          // })
          Dic.Save(Object.assign(this.mdl, values)).then((res) => {
            if (res.success) {
              this.$message.success('保存成功')
              this.$emit('ok')
              this.close()
            } else {
              this.$message.error(res.error.message)
            }
          }).catch(() => {
            // Do something
          }).finally(() => {
            this.confirmLoading = false
          })
        })
      },
      handleCancel() {
        this.close()
      }

    }
  }
</script>
