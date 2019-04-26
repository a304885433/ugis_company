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

    <a-form-item label="企业地址">
      <a-input placeholder="请输入企业地址"
               v-decorator="[
                          'address',
                          {rules: [{ required: true, message: '请输入企业名称', whitespace: true}]}
                        ]" />
    </a-form-item>

    <a-row :gutter="8">
      <a-col :md="12"
             :sm="24">
        <a-form-item label="仓库联系人">
          <a-input placeholder="请输入"
                   v-decorator="[
                    'contact',
                    {rules: [{ required: true, message: '请输入企业联系人', whitespace: true}, {validator: validate}]}
                  ]" />
        </a-form-item>
      </a-col>
      <a-col :md="12"
             :sm="24">
        <a-form-item label="联系电话">
          <a-input placeholder="请输入联系电话"
                   v-decorator="[
             'tel',
             {rules: [{ required: true, message: '请输入企业联系人', whitespace: true}, {validator: validate}]}
           ]" />
        </a-form-item>
      </a-col>
    </a-row>

    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="排污许可证">
      <a-upload v-decorator="['upload', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
}]"
                name="logo"
                action="/upload.do"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>

    <a-row :gutter="8">
      <a-col :xs="24"
             :md="12">
        <a-form-item label="废水类型">
          <a-select placeholder="请选择废水类型"
                    v-decorator="[
                    'waterTypeID',
                    {rules: [{ required: true, message: '请选择执行人'}]}
                  ]">
            <a-select-option value="1">废水1</a-select-option>
            <a-select-option value="2">废水2</a-select-option>
          </a-select>
        </a-form-item>
      </a-col>
      <a-col :xs="24"
             :md="12">
        <a-form-item label=" 废水月处理量">
          <a-input placeholder="请输入企业地址"
                   v-decorator="[
                        'address',
                        {rules: [{ required: true, message: '请输入企业名称', whitespace: true}]}
                      ]">
            <span slot="addonAfter"
                  style="width: 10px">吨
            </span>
          </a-input>
        </a-form-item>
      </a-col>
    </a-row>

    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="污水处理工艺流程图"
                 extra="">
      <a-upload v-decorator="['upload', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
}]"
                name="logo"
                action="/upload.do"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>

    <a-form-item label="工艺流程说明（要说明污水分流情况）">
      <a-textarea placeholder=""
                  v-decorator="[
                      'address',
                      {rules: [{ required: true, message: '请输入企业名称', whitespace: true}]}
                    ]">
      </a-textarea>
    </a-form-item>

    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="污水管网图"
                 extra="">
      <a-upload v-decorator="['upload', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
}]"
                name="logo"
                action="/upload.do"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>

    <a-form-item v-bind="formItemLayout"
                 style="text-align: left;"
                 label="环境污染险保单复印件"
                 extra="">
      <a-upload v-decorator="['upload', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
}]"
                name="logo"
                action="/upload.do"
                list-type="picture">
        <a-button>
          <a-icon type="upload" /> 点击上传
        </a-button>
      </a-upload>
    </a-form-item>

    <a-row gutter="8">
      <a-col :md="12"
             :xs="24">
        <a-form-item label="环评公司名称"
                     extra="">
          <a-input placeholder="请输入环评公司名称"
                   v-decorator="[
               'address',
               {rules: [{ required: true, message: '请输入企业名称', whitespace: true}]}
             ]">
          </a-input>
        </a-form-item>
      </a-col>
      <a-col :md="12"
             :xs="24">
        <a-form-item label="企业危险评级">
          <a-select placeholder="请选择企业危险评级"
                    v-decorator="[
                     'waterTypeID',
                     {rules: [{ required: true, message: '请选择执行人'}]}
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
    </a-row>


    <a-form-item v-if="showSubmit">
      <a-button htmlType="submit">保存</a-button>
    </a-form-item>
  </a-form>
</template>

<script>
  export default {
    name: 'RepositoryForm',
    props: {
      showSubmit: {
        type: Boolean,
        default: false
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
        console.log('Upload event:', e);
        if (Array.isArray(e)) {
          return e;
        }
        return e && e.fileList;
      },
    }
  }
</script>

<style scoped>
  .form {
    text-align: left;
  }

</style>
