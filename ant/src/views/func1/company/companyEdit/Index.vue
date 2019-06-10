<template>
  <div>
    <a-card class="card"
            title="基本信息"
            :bordered="false">
      <base-info ref="baseInfoForm"
                 :showSubmit="false"
                 :dianweiDicArr="dianweiDicArr"
                 :waterDicArr="waterDicArr" />
    </a-card>

    <a-card class="card"
            title="污染物信息"
            :bordered="false">
      <contaminants ref="contaminantsTable"
                    :showSubmit="false"
                    :contaminantsDicArr="contaminantsDicArr" />
    </a-card>

    <!-- table -->
    <a-card title="污染因子">
      <polu-type ref="poluTypeTable"
                 :yinziDicArr="yinziDicArr"></polu-type>
    </a-card>

    <!-- table -->
    <a-card title="药品及污染源购置台账">
      <!-- <test ref="yaopinTable" :yaopinDicArr="yaopinDicArr"></test> -->
      <yao-pin ref="yaopinTable"
               :yaopinDicArr="yaopinDicArr"></yao-pin>
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
                v-action="['create','update']"
                style="margin-right: 10px;"
                :loading="loading">提交</a-button>
      <a-button type="default"
                @click="$router.go(-1)">取消</a-button>
    </footer-tool-bar>
  </div>
</template>

<script>
  import BaseInfo from './BaseInfo'
  import WaterInfo from './WaterInfo'
  import YaoPin from './YaoPin'
  import Contaminants from './Contaminants'
  import PoluType from './PoluType'
  import FooterToolBar from '@/components/FooterToolbar'
  import { mixin, mixinDevice } from '@/utils/mixin'
  import { Dic, CompanyInfo } from '@/api/'
  import moment from 'moment'

  const fieldLabels = {
  }

  export default {
    name: 'CompanyEditIndex',
    mixins: [mixin, mixinDevice],
    components: {
      FooterToolBar,
      BaseInfo,
      Contaminants,
      WaterInfo,
      YaoPin,
      PoluType
    },
    data() {
      return {
        description: '录入企业相关的所有信息',
        loading: false,
        memberLoading: false,

        errors: [],
        dicArr: [],
        yaopinDicArr: [],
        yinziDicArr: [],
        waterDicArr: [],
        paifangDicArr: [],
        shoujiDicArr: [],
        dianweiDicArr: [],
        contaminantsDicArr: [],
        mdl: {},
      }
    },
    watch: {
      '$route'(to, from) { //监听路由是否变化
        if (to.query.id != from.query.id) {
          // this.getData()
          console.log(to, from)
        }
      }
    },
    created() {
      this.mdl = null
      this.getDic()
      this.getData()
    },
    methods: {
      // 加载数据
      getData() {
        let id = this.$route.query.id
        if (!id) {
          this.clearData()
          return
        }
        CompanyInfo.GetForEdit(id).then((res) => {
          this.mdl = res.result
          if (this.mdl.companyInfo.dischargeDate) {
            this.mdl.companyInfo.dischargeDate = moment(this.mdl.companyInfo.dischargeDate)
          }
          if (this.mdl.companyInfo.chkPointIdList) {
            this.mdl.companyInfo.chkPointIdList = this.mdl.companyInfo.chkPointIdList.split(',').map(t => parseInt(t))
          }
          this.$refs.baseInfoForm.edit(this.mdl.companyInfo)
          this.$refs.waterInfoForm.edit(this.mdl.companyInfo)
          this.$refs.yaopinTable.edit(this.mdl.companyMedcineTypeList)
          this.$refs.poluTypeTable.edit(this.mdl.companyPoluTypeList)
        })
      },
      clearData() {
        this.$nextTick(() => {
          this.$refs.baseInfoForm.form.resetFields()
          this.$refs.waterInfoForm.form.resetFields()
          this.$refs.yaopinTable.edit([])
          this.$refs.poluTypeTable.edit([])
        })
      },
      // 加载字典数据
      getDic() {
        Dic.GetAllItem().then(res => {
          this.dicArr = res.result
          this.yaopinDicArr = this.dicArr.filter(t => t.typeCode == 'yaopin')
          this.yinziDicArr = this.dicArr.filter(t => t.typeCode == 'yinzixinxi')
          this.waterDicArr = this.dicArr.filter(t => t.typeCode == 'feishuileixing')
          this.shoujiDicArr = this.dicArr.filter(t => t.typeCode == 'shoujifangshi')
          this.paifangDicArr = this.dicArr.filter(t => t.typeCode == 'paifangfangshi')
          this.dianweiDicArr = this.dicArr.filter(t => t.typeCode == 'dianweixinxi')
          this.contaminantsDicArr = this.dicArr.filter(t => t.typeCode == 'wuranwu')
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
        let res = fileList.map((file) => {
          if (file.status != 'done') {
            throw new Error('存在未成功上传的文件，请检查！')
          }
          return file.response ? file.response.result : file
        })
        return JSON.stringify(res)
      },

      // 最终全页面提交
      validate() {
        const { $refs: { baseInfoForm, contaminantsTable, yaopinTable, poluTypeTable }, $notification } = this
        let poluTypeData = poluTypeTable.data
        let yaopinData = yaopinTable.data
        let contaminantsData = contaminantsTable.data

        // 验证表单通过
        baseInfoForm.form.validateFields((err, values) => {
          try {
            values.licenseFile = this.getUploadFile(values.licenseFile)
            values.craftFile = this.getUploadFile(values.craftFile)
            values.pipeFile = this.getUploadFile(values.pipeFile)
            values.issSheetFile = this.getUploadFile(values.issSheetFile)
            values.purchaseFile = this.getUploadFile(values.purchaseFile)
            values.chkPointIdList = values.chkPointIdList.join(',')
          } catch (e) {
            this.$message.warn(e.message)
            return
          }

          if (err) {
            const errors = Object.assign({}, baseInfoForm.form.getFieldsError())
            const tmp = { ...errors }
            this.errorList(tmp)
            return
          }

          if (!this.checkTableData(contaminantsData, 'contaminantsId', '污染物数据不完整，请调整！')) {
            return
          }
          if (!this.checkTableData(poluTypeData, 'poluTypeId', '排查点位因子数据不完整，请调整！')) {
            return
          }
          if (!this.checkTableData(yaopinData, 'medTypeId', '药品数据不完整，请调整！')) {
            return
          }


          let mdlCompanyInfo = this.mdl ? this.mdl.companyInfo : {}
          let data = {
            companyInfo: Object.assign(mdlCompanyInfo, values),
            companyPoluTypeList: poluTypeData,
            companyMedcineTypeList: yaopinData,
            CompanyContaminantsList: contaminantsData,
          }
          this.loading = true
          return CompanyInfo.SaveForEdit(data).then((res) => {
            this.$message.info(`保存成功`)
            if (!this.$route.query.id) {
              this.$router.push({ path: '/manage/company/edit', query: { id: res.result } })
            }
          }).catch(err => {
            $notification.error({
              message: '保存失败，请稍后重试！',
              description: err.message
            })
          }).finally(() => {
            this.loading = false
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
            fieldLabel: fieldLabels[key]
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
