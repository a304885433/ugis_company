<template>
  <a-card :bordered="false">
    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="企业名称">
              <a-input v-model="queryParam.name"
                       placeholder="" />
            </a-form-item>
          </a-col>
          <a-col :md="8"
                 :sm="24">
            <a-form-item label="企业地址">
              <a-input v-model="queryParam.address"
                       placeholder="" />
            </a-form-item>
          </a-col>
          <template v-if="advanced">
            <a-col :md="8"
                   :sm="24">
              <a-form-item label="联系人">
                <a-input-number v-model="queryParam.contact"
                                style="width: 100%" />
              </a-form-item>
            </a-col>
            <a-col :md="8"
                   :sm="24">
              <a-form-item label="电话">
                <a-input-number v-model="queryParam.tel"
                                style="width: 100%" />
              </a-form-item>
            </a-col>
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
      <router-link :to="{name: 'CompanyEdit'}">
        <a-button type="primary"
                  v-action:create
                  icon="plus">新建</a-button>
      </router-link>
    </div>

    <s-table ref="table"
             size="default"
             rowKey="id"
             :columns="columns"
             :data="loadData">
      <span slot="serial"
            slot-scope="text, record, index">
        {{ index + 1 }}
      </span>
      <span slot="status"
            slot-scope="text">
        <a-badge :status="text | statusTypeFilter"
                 :text="text | statusFilter" />
      </span>

      <span slot="action"
            slot-scope="text, record">
        <template>
          <a @click="handleEdit(record)"
             v-action:update>编辑</a>
          <a-divider type="vertical" />
          <a-popconfirm title="删除不可恢复，是否继续？"
                        @confirm="handleDelete(record.id)">
            <a v-action:delete>删除</a>
          </a-popconfirm>
        </template>
      </span>
    </s-table>
  </a-card>
</template>

<script>
  import moment from 'moment'
  import { STable } from '@/components'
  import { CompanyInfo } from '@/api/index'

  export default {
    name: 'CompanyList',
    components: {
      STable,
    },
    data() {
      return {
        mdl: {},
        // 高级搜索 展开/关闭
        advanced: false,
        // 查询参数
        queryParam: {},
        // 表头
        columns: [
          {
            title: '#',
            scopedSlots: { customRender: 'serial' }
          },
          {
            title: '企业名称',
            dataIndex: 'name'
          },
          {
            title: '企业地址',
            dataIndex: 'address'
          },
          {
            title: '企业联系人',
            dataIndex: 'contact',
            sorter: true,
            needTotal: true,
            // customRender: (text) => text + ' 次'
          },
          {
            title: '操作',
            dataIndex: 'action',
            width: '150px',
            scopedSlots: { customRender: 'action' }
          }
        ],
        // 加载数据方法 必须为 Promise 对象
        loadData: parameter => {
          return CompanyInfo.GetAll(Object.assign(parameter, this.queryParam))
            .then(res => {
              return res.result
            })
        },
      }
    },
    created() {
    },
    methods: {
      handleDelete(id) {
        CompanyInfo.Delete({ id }).then(() => {
          this.$refs.table.refresh()
        })
      },

      handleEdit(record) {
        this.$router.push({ name: 'CompanyEdit', query: { id: record.id } })
      },
      handleOk() {
        this.$refs.table.refresh()
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
