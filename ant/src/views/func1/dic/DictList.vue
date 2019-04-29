<template>
  <a-card :bordered="false">
    <a-row :gutter="8">
      <a-col :md="5"
             :sm="24">
        <s-tree :dataSource="dicTypeTree"
                :defaultSelectedKeys="defaultSelectedKeys"
                :search="true"
                @click="handleClick"
                @add="handleAdd"
                @titleClick="handleTitleClick"></s-tree>
      </a-col>
      <a-col :md="19"
             :sm="24">
        <s-table ref="table"
                 :auto="false"
                 size="default"
                 :columns="columns"
                 :data="loadData"
                 :alert="false"
                 :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }">
          <span slot="action"
                slot-scope="text, record">
            <template v-if="$auth('table.update')">
              <a @click="$refs.modal.edit(record)">编辑</a>
              <a-divider type="vertical" />
            </template>
            <a-dropdown>
              <a class="ant-dropdown-link">
                更多
                <a-icon type="down" />
              </a>
              <a-menu slot="overlay">
                <!-- <a-menu-item>
                  <a href="javascript:;">详情</a>
                </a-menu-item> -->
                <a-menu-item v-if="$auth('table.delete')">
                  <a @click="handleDelete(record)">删除</a>
                </a-menu-item>
              </a-menu>
            </a-dropdown>
          </span>
        </s-table>
      </a-col>
    </a-row>
    <dic-modal ref="modal"
               @ok="handleSaveOk"
               @close="handleSaveClose" />
  </a-card>
</template>

<script>
  import STree from '@/components/Tree/Tree'
  import { STable } from '@/components'
  import DicModal from './DicModal'
  import { DicType, Dic } from '@/api/index'

  export default {
    name: 'DictList',
    components: {
      STable,
      STree,
      DicModal
    },
    data() {
      return {
        defaultSelectedKeys: [],
        // 字段类型配置 
        dictType: {},

        // 查询参数
        queryParam: {},
        // 表头
        columns: [
          {
            title: '#',
            dataIndex: 'no',
            width: 15,
            customRender: (text, record, index) => index + 1
          },
          {
            title: '名称',
            dataIndex: 'name'
          },
          {
            table: '操作',
            dataIndex: 'action',
            width: '150px',
            scopedSlots: { customRender: 'action' }
          }
        ],
        // 加载数据方法 必须为 Promise 对象
        loadData: parameter => {
          return Dic.GetAll(Object.assign(parameter, this.queryParam))
            .then(res => {
              return res.result
            })
        },
        dicTypeTree: [],
        selectedRowKeys: [],
        selectedRows: []
      }
    },
    created() {
      DicType.GetAll().then(res => {
        // 默认选中第一个
        let { items } = res.result
        this.dicTypeTree = this.getTree(items, null)
        this.setDicTypeConfig(items)
        // 加载列表查询
        this.handleClick({
          key: items[0].typeCode
        })
      })
    },
    methods: {
      getTree(data, parent) {
        if (!data) return null
        let filterData = data.filter(t => t.parentTypeCode == parent)
        if (filterData.length == 0) {
          return null
        }
        return filterData.map(t => {
          return {
            key: t.typeCode,
            title: t.typeName,
            children: this.getTree(data, t.typeCode)
          }
        })
      },
      setDicTypeConfig(arrType) {
        arrType.forEach(t => {
          this.dictType[t.typeCode] = JSON.parse(t.extensionData)
        })
      },
      handleClick(e) {
        // 修改显示列信息
        if (this.dictType[e.key]) {
          let name = this.dictType[e.key].name
          this.columns[1].title = `${name}名称`
          this.$refs.modal.setName(name)
        }

        this.queryParam = {
          typeCode: e.key,
          name: e.search
        }
        this.$refs.table.refresh(true)
      },
      handleAdd(item) {
        this.$refs.modal.add(item.key)
      },
      handleTitleClick(item) {

      },
      titleClick(e) {
      },
      handleSaveOk() {
        this.$refs.table.refresh(true)
      },
      handleSaveClose() {

      },
      handleDelete(record) {
        this.$confirm({
          title: '提示',
          content: '删除数据将不可恢复，是否继续 ?',
          onOk: () => {
            Dic.Delete({ id: record.id }).then(() => {
              this.$refs.table.refresh()
            }).catch(err => {

            })
          },
          onCancel() {
          }
        })
      },
      onSelectChange(selectedRowKeys, selectedRows) {
        this.selectedRowKeys = selectedRowKeys
        this.selectedRows = selectedRows
      },
    }
  }
</script>

<style lang="less">
  .custom-tree {

    /deep/ .ant-menu-item-group-title {
      position: relative;
      &:hover {
        .btn {
          display: block;
        }
      }
    }

    /deep/ .ant-menu-item {
      &:hover {
        .btn {
          display: block;
        }
      }
    }

    /deep/ .btn {
      display: none;
      position: absolute;
      top: 0;
      right: 10px;
      width: 20px;
      height: 40px;
      line-height: 40px;
      z-index: 1050;

      &:hover {
        transform: scale(1.2);
        transition: 0.5s all;
      }
    }
  }

</style>
