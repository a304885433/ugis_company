<template>
  <a-form @submit="handleSubmit"
          :form="form"
          class="form">

    <a-form-item label="企业名称">
      <a-input placeholder="请输入企业名称"
               v-decorator="[
                                  'name',
                                  {rules: [{ required: true, message: '请输入企业名称', whitespace: true}]}
                                ]" />
    </a-form-item>

    <a-row :gutter="8">
      <a-col :md="12"
             :sm="24">
        <a-form-item label="企业地址">
          <a-input placeholder="请输入企业地址"
                   v-decorator="[
                                  'address',
                                  {rules: [{ required: true, message: '请输入企业地址', whitespace: true}]}
                                ]" />
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="12">
        <a-form-item label="经度">
          <a-input-number placeholder="请输入经度"
                          style="width: 100%;"
                          v-decorator="[
                                    'x'
                                  ]" />
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="12">
        <a-form-item label="纬度">
          <a-input-number placeholder="请输入经度"
                          style="width: 100%;"
                          v-decorator="[
                                    'y'
                                  ]" />
        </a-form-item>
      </a-col>
    </a-row>



    <a-row :gutter="8">
      <a-col :md="12"
             :sm="24">
        <a-form-item label="企业联系人">
          <a-input placeholder="请输入"
                   v-decorator="[
                    'contact',
                    {rules: [{ required: true, message: '请输入企业联系人', whitespace: true}]}
                  ]" />
        </a-form-item>
      </a-col>
      <a-col :md="12"
             :sm="24">
        <a-form-item label="联系电话">
          <a-input placeholder="请输入联系电话"
                   v-decorator="[
             'tel',
             {rules: [{ required: true, message: '请输入联系电话', whitespace: true}]}
           ]" />
        </a-form-item>
      </a-col>
    </a-row>

    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="排污许可证">
      <a-upload v-decorator="['licenseFile', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
rules: [{ validator: validateFile }]
}]"
                name="files"
                action="/api/File/Upload"
                @preview="filePreview"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>

    <a-row :gutter="8">
      <a-col :xs="24"
             :md="6">
        <a-form-item label="废水类型">
          <a-select placeholder="请选择废水类型"
                    v-decorator="[
                    'waterTypeID',
                    {rules: [{ required: true, message: '请选择废水类型'}]}
                  ]">
            <a-select-option v-for="dic in waterDicArr"
                             :key="dic.id"
                             :value="dic.id">{{dic.name}}</a-select-option>
          </a-select>
        </a-form-item>
      </a-col>
      <a-col :xs="24"
             :md="6">
        <a-form-item label="废水设施处理能力">
          <a-input placeholder="请输入废水设施处理能力"
                   v-decorator="[
                 'waterAmountBz',
                 {rules: [{ required: true, message: '请输入废水设施处理能力'}]}
               ]">
            <span slot="addonAfter"
                  style="width: 10px">m³/d
            </span>
          </a-input>
        </a-form-item>
      </a-col>
      <a-col :xs="24"
             :md="6">
        <a-form-item label="废水月处理量(批复)">
          <a-input placeholder="请输入废水月处理量批复值"
                   v-decorator="[
                        'planWaterAmount',
                        {rules: [{ required: true, message: '请输入废水月处理量批复值'}]}
                      ]">
            <span slot="addonAfter"
                  style="width: 10px">m³/d
            </span>
          </a-input>
        </a-form-item>
      </a-col>
      <a-col :xs="24"
             :md="6">
        <a-form-item label="废水月处理量(实际)">
          <a-input placeholder="请输入废水月处理量实际值"
                   v-decorator="[
                        'factWaterAmount',
                        {rules: [{ required: true, message: '请输入废水月处理量实际值'}]}
                      ]">
            <span slot="addonAfter"
                  style="width: 10px">m³/d
            </span>
          </a-input>
        </a-form-item>
      </a-col>
    </a-row>

    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="污水处理工艺流程图"
                 extra="">
      <a-upload v-decorator="['craftFile', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
rules: [{ validator: validateFile }]
}]"
                name="files"
                action="/api/File/Upload"
                @preview="filePreview"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>

    <a-form-item label="工艺流程说明（要说明污水分流情况）">
      <a-textarea placeholder=""
                  v-decorator="[
                      'craftDes'
                    ]">
      </a-textarea>
    </a-form-item>

    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="污水管网图"
                 extra="">
      <a-upload v-decorator="['pipeFile', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
rules: [{ validator: validateFile }]
}]"
                name="files"
                action="/api/File/Upload"
                @preview="filePreview"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>

    <!-- <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="环境污染险保单复印件"
                 extra="">
      <a-upload v-decorator="['issSheetFile', {
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
    </a-form-item> -->

    <!-- <a-row :gutter="8">
      <a-col :md="12"
             :xs="24">
        <a-form-item label="环评公司名称"
                     extra="">
          <a-input placeholder="请输入环评公司名称"
                   v-decorator="[
               'envCompany',
               {rules: [{ required: true, message: '请输入环评公司名称', whitespace: true}]}
             ]">
          </a-input>
        </a-form-item>
      </a-col>
      <a-col :md="12"
             :xs="24">
        <a-form-item label="企业危险评级">
          <a-select placeholder="请选择企业危险评级"
                    v-decorator="[
                     'riskBand',
                     {rules: [{ required: true, message: '请选择企业危险评级'}]}
                   ]">
            <a-select-option value="1">1</a-select-option>
            <a-select-option value="2">2</a-select-option>
            <a-select-option value="3">3</a-select-option>
            <a-select-option value="4">4</a-select-option>
            <a-select-option value="5">5</a-select-option>
            <a-select-option value="6">6</a-select-option>
          </a-select>
        </a-form-item>
      </a-col>
    </a-row> -->
    <a-row :gutter="8">
      <a-col :md="6"
             :sm="24">
        <a-form-item label="环责险购买情况（保额）">
          <a-input-number placeholder="请输入环责险购买情况（保额）"
                          style="width: 100%;"
                          v-decorator="[ 'envInsuredAmount', {rules: [{ required: true, message: '请输入环责险购买情况（保额）'}]} ]">
          </a-input-number>
        </a-form-item>
      </a-col>
      <a-col :md="6"
             :sm="24">
        <a-form-item label="环责险购买情况（期限）">
          <a-input placeholder="请输入环责险购买情况（期限）"
                   v-decorator="[ 'envDeadline', {rules: [{ required: true, message: '请输入环责险购买情况（期限）'}]} ]">
          </a-input>
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
                @preview="filePreview"
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
    name: 'BaseInfo',
    props: {
      showSubmit: {
        type: Boolean,
        default: false
      },
      waterDicArr: {
        type: Array,
      },
      dianweiDicArr: {
        type: Array,
      }
    },
    data() {
      return {
        form: this.$form.createForm(this),
        formItemLayout: {
          labelCol: { md: 2, align: 'left', xs: 24 },
          wrapperCol: { md: 14, offset: 2, xs: 24 },
        },
        dicArr: [],
        fileList: []
      }
    },
    created() {
      this.form.getFieldDecorator('licenseFileArr', { initialValue: [], preserve: true });
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
      },
      validate(rule, value, callback) {
        const regex = /^user-(.*)$/
        if (!regex.test(value)) {
          callback(new Error('需要以 user- 开头'))
        }
        callback()
      },
      normFile(e) {
        if (Array.isArray(e)) {
          return e;
        }
        return e && e.fileList;
      },
      filePreview(file) {
        let url = file.url
        let ext = file.ext
        if (!url && file.response) {
          let r = file.response.result
          if (!r || !r.uid) {
            return
          }
          ext = r.ext
          url = `http://${location.host}/api/file/download?id=${r.uid}&name=${r.name}`
        }
        ext = ext.toLowerCase()
        if (ext == '.jpg' || ext == '.png' || ext == '.bmp' || ext == '.jpeg') {
          this.$success({
            title: '图片预览',
            width: 600,
            okText: '确定',
            content: (  // JSX support
              <div>
                <img style="width:500px;" src={url} />
              </div>
            ),
          });
        } else {
          window.open(url, '_blank')
        }
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
      edit(record) {
        this.form.resetFields()
        record.licenseFile = this.parseFile(record.licenseFile)
        record.craftFile = this.parseFile(record.craftFile)
        record.pipeFile = this.parseFile(record.pipeFile)
        record.issSheetFile = this.parseFile(record.issSheetFile)
        record.purchaseFile = this.parseFile(record.purchaseFile)
        setTimeout(() => {
          this.form.setFieldsValue({ ...record })
        }, 50);
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
