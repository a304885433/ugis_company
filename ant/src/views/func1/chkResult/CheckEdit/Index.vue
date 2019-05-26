<template>
  <div>
    <a-card class="card"
            title=""
            :bordered="false">
      <check-info :companyArr="companyArr"
                  :dianweiDicArr="dianweiDicArr"
                  ref="checkInfoForm" />
    </a-card>

    <!-- table -->
    <a-card title="">
      <check-item ref="checkItemTable" />
    </a-card>

    <!-- fixed footer toolbar -->
    <footer-tool-bar :style="{ width: isSideMenu() && isDesktop() ? `calc(100% - ${sidebarOpened ? 256 : 80}px)` : '100%'}">
      <span class="popover-wrapper">
        <a-popover title="表单校验信息"
                   overlayClassName="antd-pro-pages-forms-style-errorPopover"
                   trigger="click"
                   :getPopupContainer="trigger => trigger.parentNode">
          <template slot="content">
            <li v-for="item in errors"
                :key="item.key"
                @click="scrollToField(item.key)"
                class="antd-pro-pages-forms-style-errorListItem">
              <a-icon type="cross-circle-o"
                      class="antd-pro-pages-forms-style-errorIcon" />
              <div class="">{{ item.message }}</div>
              <div class="antd-pro-pages-forms-style-errorField">{{ item.fieldLabel }}</div>
            </li>
          </template>
          <span class="antd-pro-pages-forms-style-errorIcon"
                v-if="errors.length > 0">
            <a-icon type="exclamation-circle" />{{ errors.length }}
          </span>
        </a-popover>
      </span>
      <a-button type="primary"
                @click="validate"
                style="margin-right: 10px;"
                v-action="['create','update']"
                :loading="loading">提交</a-button>
      <a-button type="default"
                @click="$router.go(-1)">取消</a-button>
    </footer-tool-bar>
  </div>
</template>

<script>
  import FooterToolBar from '@/components/FooterToolbar'
  import { mixin, mixinDevice } from '@/utils/mixin'
  import { Dic, CompanyInfo, ChkResult } from '@/api/'
  import moment from 'moment'
  import CheckItem from './CheckItem'
  import CheckInfo from './CheckInfo'

  export default {
    name: 'CompanyEditIndex',
    mixins: [mixin, mixinDevice],
    components: {
      FooterToolBar,
      CheckItem,
      CheckInfo
    },
    data() {
      return {
        description: '录入排查数据',
        loading: false,
        memberLoading: false,

        errors: [],
        dicArr: [],
        dianweiDicArr: [],
        companyArr: []
      }
    },
    created() {
      this.getCompanyInfo()
    },
    methods: {
      clearData() {
        this.$nextTick(() => {
        })
      },
      // 加载公司数据
      getCompanyInfo() {
        // 获取公司信息
        return CompanyInfo.GetAllItem().then(res => {
          this.companyArr = res.result
        })
      },
      handleSubmit(e) {
        e.preventDefault()
      },
      handleChange(value, key, column) {
        const newData = [...this.data]
        const target = newData.filter(item => key === item.key)[0]
        if (target) {
          target[column] = value
          this.data = newData
        }
      },

      // 检查表格数据是否完成
      checkTableData(rows, fieldName, message) {
        if (!rows || !rows.length) return true
        for (let row of rows) {
          if (row[fieldName] === '' || row[fieldName] === null || row[fieldName] === undefined) {
            this.$notification.warn({
              message: '提示',
              description: message
            })
            return false
          }
        }
        return true
      },

      // 读取文件
      getUploadFile(fileList) {
        if (!fileList || !fileList.length) return null
        var res = fileList.map((file) => file.response ? file.response.result : file)
        return JSON.stringify(res)
      },

      // 最终全页面提交
      validate() {
        const { $refs: { checkInfoForm, checkItemTable }, $notification } = this

        let data = checkItemTable.data

        // 验证表单通过
        checkInfoForm.form.validateFields((err, values) => {

          if (err) {
            const errors = Object.assign({}, checkInfoForm.form.getFieldsError())
            const tmp = { ...errors }
            this.errorList(tmp)
            return
          }

          if (data.length == 0) {
            this.$notification.warn({
              message: '未填写因子排查数据',
            })
            return
          }

          if (!this.checkTableData(data, 'poluTypeId', '因子数据不完整，请调整！')) {
            return
          }

          values.poluTypeList = data

          return ChkResult.SaveForEdit(values).then((res) => {
            this.$message.info(`保存成功`)
            checkItemTable.edit([])
          }).catch(err => {
            $notification.error({
              message: '保存失败，请稍后重试！',
              description: err.message
            })
          })
        })
      },
      errorList(errors) {
        if (!errors || errors.length === 0) {
          return null
        }
        this.errors = Object.keys(errors).map(key => {
          if (!errors[key]) {
            return null
          }

          return {
            key: key,
            message: errors[key][0],
          }
        })
      },
      scrollToField(fieldKey) {
        const labelNode = document.querySelector(`label[for="${fieldKey}"]`)
        if (labelNode) {
          labelNode.scrollIntoView(true)
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
