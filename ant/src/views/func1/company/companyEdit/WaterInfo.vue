<template>
  <a-form @submit="handleSubmit"
          :form="form"
          class="form">
    <!-- <a-row class="form-row"
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
    </a-row> -->
    <a-row class="form-row">
      <a-col :md="12"
             :sm="24">
        <a-form-item label="污染物">
          <a-select placeholder="请选择污染物"
                    v-decorator="[ 'contaminantsId',
              {rules: [{ required: true, message: '请选择污染物'}]}
            ]">
            <a-select-option v-for="dic in contaminantsDicArr"
                             :key="dic.id"
                             :value="dic.id">{{dic.name}}</a-select-option>
          </a-select>
        </a-form-item>
      </a-col>
      <a-col :md="12"
             :sm="24">
        <a-form-item label="转移总量">
          <a-input-number placeholder="请输入转移总量"
                          style="width: 100%;"
                          v-decorator="[
                                          'transferTotal'
                                        ]">
            <span slot="addonAfter"
                  style="width: 10px">吨/月
            </span>
          </a-input-number>
        </a-form-item>
      </a-col>
    </a-row>
    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="原材料采购清单">
      <a-upload v-decorator="['purchaseFile', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
rules: [{ validator: validateFile }]
}]"
                name="files"
                action="/api/File/Upload"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>
    <a-form-item label="排查点位">
      <a-select placeholder="排查点位"
                mode="multiple"
                v-decorator="[ 'chkPointIdList',
                    {rules: [{ required: true, message: '请选择排查点位'}]}
                  ]">
        <a-select-option v-for="dic in dianweiDicArr"
                         :key="dic.id"
                         :value="dic.id">{{dic.name}}</a-select-option>
      </a-select>
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
      },
      paifangDicArr: {
        type: Array
      },
      shoujiDicArr: {
        type: Array
      },
      dianweiDicArr: {
        type: Array
      },
      contaminantsDicArr: {
        type: Array
      }
    },
    data() {
      return {
        form: this.$form.createForm(this),
        formItemLayout: {
          labelCol: { md: 2, align: 'left', xs: 24 },
          wrapperCol: { md: 14, offset: 2, xs: 24 },
        },
      }
    },
    created(){
      // this.form.getFieldDecorator('purchaseFile', { initialValue: [], preserve: true });
    },
    methods: {
      handleSubmit(e) {
        e.preventDefault()
      },
      edit(record) {
        this.form.resetFields()
        record.purchaseFile = this.parseFile(record.purchaseFile)
        setTimeout(() => {
          this.form.setFieldsValue({ ...record })
        }, 50);
      },
      normFile(e) {
        if (Array.isArray(e)) {
          return e;
        }
        return e && e.fileList;
      },
      parseFile(fileJSON) {
        if (!fileJSON) return []
        let filelist = JSON.parse(fileJSON)
        return filelist.filter(t => t != null).map(t => {
          t.status = 'done'
          t.url = `http://${location.host}/api/file/download?id=${t.uid}&name=${t.name}`
          return t;
        })
      },
      validateFile(rule, value, callback) {
        if (value) {
          for (let file of value) {
            if (file.status == 'error') {
              const lt10M = file.size / 1024 / 1024 < 10
              if (!lt10M) {
                callback(new Error(`${file.name} 不能超过 10M `))
                return
              } else {
                callback(new Error(`${file.name} 上传失败 `))
              }
            }
            if (file.percent < 100) {
              callback(new Error('上传中...'))
              return
            }
          }
        }
        // Note: 必须总是返回一个 callback，否则 validateFieldsAndScroll 无法响应
        callback()
      }
    }
  }
</script>

<style scoped>
  .form {
    text-align: left;
  }

</style>
