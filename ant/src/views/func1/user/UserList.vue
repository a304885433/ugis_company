<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <router-link :to="{name: 'UserEdit'}">
        <a-button type="primary"
                  icon="plus">新建</a-button>
      </router-link>
    </div>

    <s-table size="default"
             :columns="columns"
             ref="table"
             :data="loadData">
      <span slot="action"
            slot-scope="text, record">
        <a @click="handleEdit(record)"
           v-if="record.userName != 'admin'">编辑</a>
        <a-divider type="vertical"
                   v-if="record.userName != 'admin'" />
        <a-dropdown v-if="record.userName != 'admin'">
          <a class="ant-dropdown-link">
            更多
            <a-icon type="down" />
          </a>
          <a-menu slot="overlay">
            <a-menu-item>
              <a href="javascript:;">禁用</a>
            </a-menu-item>
            <a-menu-item>
              <a-popconfirm title="删除不可恢复，是否继续？"
                            @confirm="handleDelete(record)">
                <a>删除</a>
              </a-popconfirm>
            </a-menu-item>
          </a-menu>
        </a-dropdown>
      </span>
      <span slot="isActive"
            slot-scope="text">
        <a-badge v-if="text == 1"
                 status="success"
                 text="启用" />

        <a-badge v-else="text == 0"
                 status="default"
                 text="禁用" />
      </span>
    </s-table>

    </a-modal>

  </a-card>
</template>

<script>
  import { STable } from '@/components'
  import { User } from '@/api/'
  import moment from 'moment'

  export default {
    name: 'UserList',
    components: {
      STable
    },
    data() {
      return {
        // description: '列表使用场景：后台管理中的权限管理以及角色管理，可用于基于 RBAC 设计的角色权限控制，颗粒度细到每一个操作类型。',
        visible: false,
        labelCol: {
          xs: { span: 24 },
          sm: { span: 5 }
        },
        wrapperCol: {
          xs: { span: 24 },
          sm: { span: 16 }
        },
        form: null,
        mdl: {},

        // 高级搜索 展开/关闭
        advanced: false,
        // 查询参数
        queryParam: {},
        // 表头
        columns: [
          {
            title: '用户名',
            dataIndex: 'userName'
          },
          {
            title: '姓名',
            dataIndex: 'name'
          },
          {
            title: '状态',
            dataIndex: 'isActive',
            scopedSlots: { customRender: 'isActive' }
          },
          {
            title: '创建时间',
            dataIndex: 'creationTime',
            customRender: (val) => this.dateFormat(val)
          },
          {
            title: '操作',
            width: '150px',
            dataIndex: 'action',
            scopedSlots: { customRender: 'action' }
          }
        ],
        // 加载数据方法 必须为 Promise 对象
        loadData: parameter => {
          return User.GetAll(parameter)
            .then(res => {
              console.log('getRoleList', res)
              return res.result
            })
        },

        selectedRowKeys: [],
        selectedRows: []
      }
    },
    created() {

    },
    methods: {
      handleEdit(record) {
        this.$router.push({
          name: 'UserEdit',
          query: {
            id: record.id
          }
        })
      },

      handleDelete(record) {
        User.Delete({ id: record.id }).then(() => {
          this.$refs.table.refresh(true)
        })
      },
      onChange(selectedRowKeys, selectedRows) {
        this.selectedRowKeys = selectedRowKeys
        this.selectedRows = selectedRows
      },
      toggleAdvanced() {
        this.advanced = !this.advanced
      }
    },
  }
</script>
