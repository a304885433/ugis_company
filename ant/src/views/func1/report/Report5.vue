<template>
  <a-card :bordered="false">
    <div class="table-page-search-wrapper">
      <a-form layout="inline"
              :form="form"
              @submit="handleSubmit">
        <a-row :gutter="48">
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="点位">
              <a-select placeholder="点位"
                        v-decorator="[ 'chkPointId',
                 {rules: [{ required: true, message: '请选择点位'}]}
               ]">
                <a-select-option v-for="dic in chkPointArr"
                                 :key="dic.id"
                                 :value="dic.id">{{dic.name}}</a-select-option>
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
              <a-button style="margin-left: 8px"
                 @click="exportXls">导出</a-button>
            </span>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table ref="table"
             size="middle"
             rowKey="id"
             :dataSource="data"
             :scroll="{ x: 1500, y: 300 }"
             :columns="columns">
    </a-table>
  </a-card>
</template>

<script>
  import moment from 'moment'
  import { Report, CompanyInfo, Dic } from '@/api/'

  export default {
    name: 'Report5',
    components: {
    },
    data() {
      return {
        form: this.$form.createForm(this),
        // 表头
        columns: [],
        data: [],
        companyArr: [],
        chkPointArr: [],
        loading: false,
        colList: null,
      }
    },
    created() {
      this.getCheckPoint()
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
          return
        }
        let request = {
          colList: this.colList, 
          data: this.data,
          name: '所有点位统计'
        }
        this.download(request)
      },
      loadData(queryParam) {
        let param = Object.assign({}, queryParam)
        if (param.startTime) {
          param.startTime = moment(param.startTime).format('YYYY-MM-DD 00:00:00')
        }
        if (param.endTime) {
          param.endTime = moment(param.endTime).format('YYYY-MM-DD 23:59:59')
        }
        this.loading = true
        return Report.GetReport5(param)
          .then(res => {
            this.colList = res.result.colList
            this.columns = res.result.colList.map((t, index) => {
              return {
                dataIndex: t.colId,
                title: t.name,
                fixed: index == 0 ? 'left' : undefined,
                width: 100,
              }
            })
            this.data = res.result.data
          }).catch(err => {
            let res = err.response.data
            this.$message.error(res.error.message)
          }).finally(() => {
            this.loading = false
          })
      },
      getCheckPoint() {
        Dic.GetAllItem({
          typeCode: 'dianweixinxi'
        }).then(res => {
          this.chkPointArr = res.result
        })
      },
    }
  }
</script>
