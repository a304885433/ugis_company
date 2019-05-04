<template>
  <a-card :bordered="false">
    <div class="table-page-search-wrapper">
      <a-form layout="inline"
              :form="form"
              @submit="handleSubmit">
        <a-row :gutter="48">
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="企业名称">
              <a-select placeholder="请选择"
                        :allowClear="true"
                        @change="handleCompanyChange"
                        v-decorator="[
                          'companyId',
                          {rules: [{ required: true, message: '请选择企业'}]}
                        ]">
                <a-select-option v-for="item in companyArr"
                                 :value="item.id"
                                 :chkPointId="item.chkPointIdList"
                                 :key="item.id">{{ item.name }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="因子">
              <a-select placeholder="因子"
                        v-decorator="[ 'poluTypeId',
                 {rules: [{ required: true, message: '请选择因子'}]}
               ]">
                <a-select-option v-for="dic in poluTypeArr"
                                 :key="dic.id"
                                 :value="dic.id">{{dic.name}}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="排查日期">
              <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                <a-date-picker format="YYYY-MM-DD"
                               style="width: 100%"
                               v-decorator="[
                   'startTime',
                   {rules: [{ required: true, message: '请选择开始时间'}]}
                 ]" />
              </a-form-item>
              <span :style="{ display: 'inline-block', width: '24px', textAlign: 'center' }">
                -
              </span>
              <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }"
                           :colon="false">
                <a-date-picker format="YYYY-MM-DD"
                               style="width: 100%"
                               v-decorator="[
                              'endTime',
                            ]" />
              </a-form-item>
            </a-form-item>
          </a-col>
          <a-col :md="24"
                 :sm="24">
            <span class="table-page-search-submitButtons">
              <a-button type="primary"
                        :loading="loading"
                        @click="handleQuery">查询</a-button>
              <!-- <a-button style="margin-left: 8px"
                        @click="exportXls">导出</a-button> -->
            </span>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <div class="table-operator">
      <a-radio-group :value="view"
                     @change="handleViewChange">
        <a-radio-button value="table">表格</a-radio-button>
        <a-radio-button value="chart">图形</a-radio-button>
      </a-radio-group>
    </div>

    <div>
      <a-table v-show="view=='table'"
               ref="table"
               size="default"
               :rowKey="newid"
               :dataSource="data"
               :pagination="false"
               :columns="columns">
      </a-table>
      <v-chart ref="chart"
               v-show="view=='chart'"
               :force-fit="true"
               :width="width"
               :height="height"
               :data="dataSource"
               :scale="scale"
               :padding="['auto', 'auto', '40', '60']">
        <v-tooltip />
        <v-axis />
        <v-smooth-line position="x*y"
                       :size="2" />
        <v-smooth-area position="x*y" />
      </v-chart>
    </div>
  </a-card>
</template>

<script>
  import moment from 'moment'
  import { Report, CompanyInfo, Dic } from '@/api/'

  export default {
    name: 'Report2',
    components: {
    },
    data() {
      return {
        form: this.$form.createForm(this),
        // 表头
        columns: [],
        data: [],
        poluTypeArr: [],
        companyArr: [],
        chkPointArr: [],
        loading: false,
        view: 'table',
        width: 100,
        height: 100,
        dataSource: [],
        scale: []
      }
    },
    created() {
      // 获取公司信息
      CompanyInfo.GetAllItem().then(res => {
        this.companyArr = res.result
      })
    },
    methods: {
      handleSubmit(e) {
        e.preventDefault = true
      },
      resetSearchForm() {
        this.queryParam = {
          date: moment(new Date())
        }
      },
      handleQuery() {
        this.form.validateFields((err, values) => {
          if (err) {
            return
          }
          this.loadData(values)
        })
      },
      exportXls() {
        if (!this.data.length) {
          this.$message.warn('没有需要导出的数据！')
        }
      },
      loadData(queryParam) {
        let param = Object.assign({}, queryParam)
        if (param.startTime) {
          param.startTime = moment(param.startTime).format('YYYY-MM-DD')
        }
        if (param.endTime) {
          param.endTime = moment(param.endTime).format('YYYY-MM-DD')
        }
        this.loading = true
        return Report.GetReport2(param)
          .then(res => {
            this.columns = res.result.colList.map(t => {
              return {
                dataIndex: t.colId,
                title: t.name
              }
            })
            this.data = res.result.data
            this.reloadChart()
          }).catch(err => {
            let res = err.response.data
            this.$message.error(res.error.message)
          }).finally(() => {
            this.loading = false
          })
      },
      handleCompanyChange(companyId, node) {
        this.getPoluType(companyId)
      },
      getPoluType(id) {
        CompanyInfo.GetPoluType({ id }).then(res => {
          this.poluTypeArr = res.result
        })
      },
      handleViewChange(e) {
        this.view = e.target.value
        this.reloadChart()
      },
      reloadChart() {
        this.width = Math.max(this.width, 500, this.$refs.table.$el.clientWidth)
        this.height = Math.max( this.height ,500, this.$refs.table.$el.clientHeight)
        if(this.view != 'chart') {
          return 
        }
        this.scale = [{
          dataKey: 'x',
          alias: '时间'
        },
        {
          dataKey: 'y',
          alias: '浓度',
        }]
        this.dataSource = this.data.map(t => {
          return {
            x: t.time,
            y: t.val
          }
        })
      }
    }
  }
</script>
