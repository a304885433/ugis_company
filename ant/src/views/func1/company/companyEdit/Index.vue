<template>
  <div>
    <a-card class="card"
            title="基本信息"
            :bordered="false">
      <base-info ref="baseInfoForm"
                 :showSubmit="false"
                 :waterDicArr="waterDicArr" />
    </a-card>

    <a-card class="card"
            title="排污信息"
            :bordered="false">
      <water-info ref="waterInfoForm"
                  :showSubmit="false"
                  :paifangDicArr="paifangDicArr"
                  :shoujiDicArr="shoujiDicArr" />
    </a-card>

    <!-- table -->
    <a-card title="排查点位">
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
  import PoluType from './PoluType'
  import Test from './Test3'
  import FooterToolBar from '@/components/FooterToolbar'
  import { mixin, mixinDevice } from '@/utils/mixin'
  import { Dic, CompanyInfo } from '@/api/'
  import moment from 'moment'

  const fieldLabels = {
    name: '仓库名',
    url: '仓库域名',
    owner: '仓库管理员',
    approver: '审批人',
    dateRange: '生效日期',
    type: '仓库类型',
    name2: '任务名',
    url2: '任务描述',
    owner2: '执行人',
    approver2: '责任人',
    dateRange2: '生效日期',
    type2: '任务类型'
  }

  export default {
    name: 'CompanyEditIndex',
    mixins: [mixin, mixinDevice],
    components: {
      FooterToolBar,
      BaseInfo,
      WaterInfo,
      YaoPin,
      PoluType,
      Test
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
        mdl: {},
      }
    },
    watch: {
      '$route'(to, from) { //监听路由是否变化
        if (to.query.id != from.query.id) {
          this.getData()
        }
      }
    },
    created() {
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
          if(this.mdl.companyInfo.dischargeDate) {
            this.mdl.companyInfo.dischargeDate = moment(this.mdl.companyInfo.dischargeDate)
          }
          this.$refs.baseInfoForm.edit(this.mdl.companyInfo)
          this.$refs.waterInfoForm.edit(this.mdl.companyInfo)
          this.$refs.yaopinTable.edit(this.mdl.companyMedcineTypeList)
          this.$refs.poluTypeTable.edit(this.mdl.companyPoluTypeList)
        })
      },
      clearData(){
        this.$refs.baseInfoForm.form.resetFields()
        this.$refs.waterInfoForm.form.resetFields()
        this.$refs.yaopinTable.edit([])
        this.$refs.poluTypeTable.edit([])
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
        const { $refs: { baseInfoForm, waterInfoForm, yaopinTable, poluTypeTable }, $notification } = this

        let poluTypeData = poluTypeTable.data
        let yaopinData = yaopinTable.data

        // 验证表单通过
        baseInfoForm.form.validateFields((err, values) => {
          values.licenseFile = this.getUploadFile(values.licenseFile)
          values.craftFile = this.getUploadFile(values.craftFile)
          values.pipeFile = this.getUploadFile(values.pipeFile)
          values.issSheetFile = this.getUploadFile(values.issSheetFile)
          if (err) {
            const errors = Object.assign({}, baseInfoForm.form.getFieldsError())
            const tmp = { ...errors }
            this.errorList(tmp)
            return
          }

          waterInfoForm.form.validateFields((waterErr, waterValues) => {
            if (waterErr) {
              const errors = Object.assign({}, waterInfoForm.form.getFieldsError())
              const tmp = { ...errors }
              this.errorList(tmp)
              return
            }
            if (!this.checkTableData(poluTypeData, 'poluTypeId', '排查点位因子数据不完整，请调整！')) {
              return
            }
            if (!this.checkTableData(yaopinData, 'medTypeId', '药品数据不完整，请调整！')) {
              return
            }
            let data = {
              companyInfo: Object.assign(this.mdl.companyInfo, values, waterValues),
              companyPoluTypeList: poluTypeData,
              companyMedcineTypeList: yaopinData
            }
            return CompanyInfo.SaveForEdit(data).then((res) => {
              this.$message.info(`保存成功`)
              this.$router.go(-1)
            }).catch(err => {
              $notification.error({
                message: '保存失败，请稍后重试！',
                description: err.message
              })
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
