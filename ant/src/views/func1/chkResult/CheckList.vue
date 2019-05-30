<template>
  <a-card :bordered="false">
    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="企业名称">
              <a-select v-model="queryParam.companyId"
                        placeholder="全部"
                        :allowClear="true"
                        showSearch
                        :filterOption="filterSelect"
                        @change="handleCompanyChange">
                <a-select-option v-for="item in companyArr"
                                 :value="item.id"
                                 :chkPointId="item.chkPointIdList"
                                 :key="item.id">{{ item.name }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="排查点位">
              <a-select v-model="queryParam.chkPointId"
                        placeholder="全部"
                        :allowClear="true"
                        showSearch
                        :filterOption="filterSelect"
                        default-value="0">
                <a-select-option v-for="item in chkPointArr"
                                 :value="item.id"
                                 :key="item.id">{{ item.name }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <template v-if="advanced">
            <a-col :md="8"
                   :sm="24">
              <a-form-item label="排查因子">
                <a-select v-model="queryParam.poluTypeId"
                          placeholder="全部"
                          :allowClear="true"
                          showSearch
                          :filterOption="filterSelect"
                          default-value="0">
                  <a-select-option v-for="item in poluTypeArr"
                                   :value="item.id"
                                   :key="item.id">{{ item.name }}</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
            <a-col :md="8"
                   :sm="24">
              <a-form-item label="排查日期">
                <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                  <a-date-picker v-model="queryParam.startChkDate"
                                 format="YYYY-MM-DD HH:mm:ss"
                                 style="width: 100%" />
                </a-form-item>
                <span :style="{ display: 'inline-block', width: '24px', textAlign: 'center' }">
                  -
                </span>
                <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                  <a-date-picker v-model="queryParam.endChkDate"
                                 format="YYYY-MM-DD HH:mm:ss"
                                 style="width: 100%" />
                </a-form-item>
              </a-form-item>
            </a-col>
            <a-col :md="8"
                   :sm="24">
              <a-form-item label="浓度范围">
                <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                  <a-input-number v-model="queryParam.startConcentration"
                                  style="width: 100%" />
                </a-form-item>
                <span :style="{ display: 'inline-block', width: '24px', textAlign: 'center' }">
                  -
                </span>
                <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                  <a-input-number v-model="queryParam.endConcentration"
                                  style="width: 100%" />
                </a-form-item>
              </a-form-item>
            </a-col>
            <!-- <a-col :md="8"
                   :sm="24">
              <a-form-item label="排查批次">
                <a-input v-model="queryParam.chkBatch" />
              </a-form-item>
            </a-col> -->
          </template>
          <a-col :md="!advanced && 8 || 24"
                 :sm="24">
            <span class="table-page-search-submitButtons"
                  :style="advanced && { float: 'right', overflow: 'hidden' } || {} ">
              <a-button type="primary"
                        @click="$refs.table.refresh(true)">查询</a-button>
              <a-button style="margin-left: 8px"
                        @click="() => queryParam = {}">重置</a-button>
              <a @click="toggleAdvanced"
                 style="margin-left: 8px">
                {{ advanced ? '收起' : '展开' }}
                <a-icon :type="advanced ? 'up' : 'down'" />
              </a>
            </span>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <div class="table-operator">
      <a-button type="primary"
                icon="plus"
                v-action:create
                @click="handleAdd">新建</a-button>
    </div>

    <s-table ref="table"
             size="default"
             :rowKey="newid"
             :columns="columns"
             :data="loadData">
      <span slot="serial"
            slot-scope="text, record, index">
        {{ index + 1 }}
      </span>
    </s-table>
  </a-card>
</template>

<script>
  import moment from 'moment'
  import { STable } from '@/components'
  import { ChkResult, CompanyInfo, Dic } from '@/api/'

  export default {
    name: 'CheckList',
    components: {
      STable,
    },
    data() {
      return {
        mdl: {},
        // 高级搜索 展开/关闭
        advanced: true,
        // 查询参数
        queryParam: {
          companyId: undefined,
          chkPointId: undefined,
          poluTypeId: undefined,
          startChkDate: undefined,
          endChkDate: undefined,
          startConcentration: undefined,
          endConcentration: undefined,
          chkBatch: undefined,
        },
        // 表头
        columns: [
          {
            title: '#',
            scopedSlots: { customRender: 'serial' }
          },
          {
            title: '企业名称',
            dataIndex: 'companyName'
          },
          {
            title: '点位',
            dataIndex: 'chkPointName'
          },
          {
            title: '排查因子',
            dataIndex: 'poluTypeName',
          },
          {
            title: '浓度',
            dataIndex: 'concentration',
          },
          // {
          //   title: '排查批次',
          //   dataIndex: 'chkBatch',
          // },
          {
            title: '排查时间',
            dataIndex: 'chkDate',
            customRender: (date) => moment(date).format('YYYY-MM-DD HH:mm:ss')
          },
          // {
          //   title: '操作',
          //   dataIndex: 'action',
          //   width: '150px',
          //   scopedSlots: { customRender: 'action' }
          // }
        ],
        // 加载数据方法 必须为 Promise 对象
        loadData: parameter => {
          let param = Object.assign(parameter, this.queryParam)
          if (param.startChkDate) {
            param.startChkDate = moment(param.startChkDate).format('YYYY-MM-DD HH:mm:ss')
          }
          if (param.endChkDate) {
            param.endChkDate = moment(param.endChkDate).format('YYYY-MM-DD HH:mm:ss')
          }
          return ChkResult.GetCustomAll(param)
            .then(res => {
              return res.result
            })
        },
        selectedRowKeys: [],
        selectedRows: [],

        options: {
          alert: false,
          rowSelection: null,
        },
        poluTypeArr: [],
        companyArr: [],
        chkPointArr: [],
      }
    },
    created() {
      // 获取公司信息
      CompanyInfo.GetAllItem().then(res => {
        this.companyArr = res.result
      })
    },
    methods: {
      handleCompanyChange(companyId, node) {
        var ids = node.data.attrs.chkPointId || '0'
        this.queryParam.poluTypeId = null
        this.queryParam.chkPointId = null
        this.getChkPoint(ids)
        this.getPoluType(companyId)
      },
      handleAdd(record) {
        this.$router.push({ name: 'CheckEdit', params: { companyArr: this.companyArr, chkPointArr: this.chkPointArr, poluTypeArr: this.chkPointArr } })
      },
      handleEdit(record) {
        this.$refs.modal.edit(record)
      },
      handleOk() {
        this.$refs.table.refresh()
      },
      getChkPoint(ids) {
        Dic.GetAllItem({
          idList: ids
        }).then(res => {
          this.chkPointArr = res.result.filter(t => t.typeCode == 'dianweixinxi')
        })
      },
      getPoluType(id) {
        CompanyInfo.GetPoluType({ id }).then(res => {
          this.poluTypeArr = res.result
        })
      },
      toggleAdvanced() {
        this.advanced = !this.advanced
      },
      resetSearchForm() {
        this.queryParam = {
          date: moment(new Date())
        }
      }
    }
  }
</script>
