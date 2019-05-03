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
            <a-form-item label="排查日期">
              <a-date-picker format="YYYY-MM-DD"
                             style="width: 100%"
                             v-decorator="[
                              'startTime',
                              {rules: [{ required: true, message: '请选择开始时间'}]}
                            ]" />
            </a-form-item>
          </a-col>
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="-"
                         :colon="false">
              <a-date-picker format="YYYY-MM-DD"
                             style="width: 100%"
                             v-decorator="[
                              'endTime',
                            ]" />
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

    <a-table ref="table"
             size="default"
             rowKey="id"
             :dataSource="data"
             :columns="columns">
    </a-table>
  </a-card>
</template>

<script>
  import moment from 'moment'
  import { Report, CompanyInfo, Dic } from '@/api/'

  export default {
    name: 'Report1',
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
      }
    },
    filters: {
      statusFilter(type) {
        return statusMap[type].text
      },
      statusTypeFilter(type) {
        return statusMap[type].status
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
          param.startTime = moment(param.startTime).format('YYYY-MM-DD HH:mm:ss')
        }
        if (param.endTime) {
          param.endTime = moment(param.endTime).format('YYYY-MM-DD HH:mm:ss')
        }
        this.loading = true
        return Report.GetReport1(param)
          .then(res => {
            this.columns = res.result.colList.map(t => {
              return {
                dataIndex: t.colId,
                title: t.name
              }
            })
            this.data = res.result.data
          }).catch(err => {
            let res = err.response.data
            this.$message.error(res.error.message)
          }).finally(() => {
            this.loading = false
          })
      }
    }
  }
</script>
