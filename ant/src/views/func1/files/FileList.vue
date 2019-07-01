<template>
  <a-card :bordered="false">

    <div class="table-page-search-wrapper">
      <a-form layout="inline"
              :form="form">
        <a-row :gutter="48">

          <a-col :md="8"
                 :sm="24">
            <a-form-item label="企业名称">
              <a-select v-decorator="[
              'companyId'
            ]"
                        placeholder="全部"
                        :allowClear="true"
                        style="width: 100%"
                        showSearch
                        :filterOption="filterSelect">
                <a-select-option v-for="item in companyArr"
                                 :value="item.id"
                                 :chkPointId="item.chkPointIdList"
                                 :key="item.id">{{ item.name }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>

          <a-col :md="8"
                 :sm="24">
            <a-form-item label="摘要">
              <a-input placeholder="摘要"
                       v-decorator="[ 'remark' ]" />
            </a-form-item>
          </a-col>

          <a-col :md="8"
                 :sm="24">
            <a-form-item label="上传日期">
              <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                <a-date-picker format="YYYY-MM-DD"
                               style="width: 100%"
                               v-decorator="[
          'startTime'
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

        </a-row>

        <a-row>
          <a-col :md="24"
                 :sm="24">
            <span class="table-page-search-submitButtons">
              <a-button type="primary"
                        :loading="loading"
                        @click="handleQuery(1)">查询</a-button>
              <a-button type="primary"
                        v-action:create
                        @click="handleAdd"
                        icon="plus">新建</a-button>
            </span>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-list :grid="{ gutter: 16, xs: 1, sm: 2, md: 4, lg: 4, xl: 6 }"
            :dataSource="list">
      <a-list-item slot="renderItem"
                   slot-scope="li, index">
        <a-card hoverable
                class="card-item"
                @click="handlePreview(li)">
          <img :alt="li.remark"
               class="img-item"
               :src="li.src"
               slot="cover" />
          <a-card-meta>
            <template slot="description">
              <a-popover :content="li.remark">
                <div class="img-desp">
                  {{ li.remark }}
                </div>
              </a-popover>
            </template>
          </a-card-meta>
          <template class="ant-card-actions"
                    v-if="hasP"
                    slot="actions">
            <a-icon v-action:update
                    type="edit"
                    @click.stop="handleEditImg(li)" />
            <a-popconfirm title="删除不可恢复，是否继续？"
                          @confirm="handleDelete(li)">
              <a-icon v-action:delete
                      type="delete"
                      @click.stop="removeImg(li)" />
            </a-popconfirm>
          </template>
        </a-card>
      </a-list-item>
    </a-list>

    <p></p>
    <a-pagination :defaultCurrent="1"
                  :total="total"
                  :pageSize="pageSize"
                  :current="pageIndex"
                  @change="handleQuery" />

    <a-modal :visible="previewVisible"
             :footer="null"
             @cancel="handleCancel">
      <img alt="example"
           style="width: 100%"
           :src="previewImage" />
      <div v-if="curr">
        {{ curr.remark }}
      </div>
    </a-modal>

    <a-modal :visible="editVisible"
             @cancel="handleCancelImg"
             @ok="handleSaveImg">
      <input type="text"
             v-model="imgDesp"
             style="width: 95%" />
    </a-modal>
  </a-card>
</template>

<script>
  import { CompanyFile, CompanyInfo } from '@/api/'
  import moment from 'moment'

  export default {
    name: 'FileList',
    components: {
    },
    data() {
      return {
        form: this.$form.createForm(this),
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
        mdl: {},
        // 高级搜索 展开/关闭
        advanced: false,
        // 加载数据方法 必须为 Promise 对象
        loadData: parameter => {
          return CompanyFile.GetAll(parameter)
            .then(res => {
              for (let t of res.result.items) {
                t.src = `http://${location.host}/api/file/download?id=${t.uId}&name=${t.name}`
              }
              return res.result
            })
        },
        list: [],
        total: 0,
        previewVisible: false,
        previewImage: null,
        pageIndex: 1,
        pageSize: 12,
        loading: false,
        imgDesp: null,
        curr: null,
        editVisible: false,
        hasP: false,
        companyId: null,
        companyArr: [],
      }
    },
    created() {
      this.handleQuery(1)
      this.hasP = window.abp.auth.hasPermission('CompanyFile.update') || window.abp.auth.hasPermission('CompanyFile.delete')
      // 获取公司信息
      CompanyInfo.GetAllItem().then(res => {
        this.companyArr = res.result
      })
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
      handleAdd(record) {
        debugger
        this.$router.push({ name: 'FileEdit', params: { companyArr: this.companyArr } })
      },
      handleQuery(pageIndex, pageSize) {
        this.pageIndex = pageIndex
        this.form.validateFields((err, values) => {
          if (err) {
            return
          }
          if (values.startTime) {
            values.startTime = moment(values.startTime).format('YYYY-MM-DD 00:00:00')
          }
          if (values.endTime) {
            values.endTime = moment(values.endTime).format('YYYY-MM-DD 23:59:59')
          }
          values.skipCount = (this.pageIndex - 1) * this.pageSize;
          this.loading = true
          this.loadData(values).then((res) => {
            this.list = res.items
            this.total = res.totalCount
          }).finally(() => {
            this.loading = false
          })
        })
      },
      handlePageChange(pageIndex) {
        this.pageIndex = pageIndex
        this.handleQuery(pageIndex)
      },
      handleDelete(record) {
        CompanyFile.Delete({ id: record.id }).then(() => {
          this.$message.success('删除成功')
          this.handleQuery(1)
        })
      },
      onChange(selectedRowKeys, selectedRows) {
        this.selectedRowKeys = selectedRowKeys
        this.selectedRows = selectedRows
      },
      toggleAdvanced() {
        this.advanced = !this.advanced
      },
      handleCancel() {
        this.previewVisible = false
      },
      handlePreview(file) {
        this.curr = file
        this.previewImage = file.src
        this.previewVisible = true
      },
      removeImg() {

      },
      handleEditImg(img) {
        this.curr = img
        this.imgDesp = img.remark
        this.editVisible = true
      },
      handleCancelImg(img) {
        this.editVisible = false
      },
      handleSaveImg() {
        CompanyFile.Update({
          id: this.curr.id,
          remark: this.imgDesp
        }).then(() => {
          this.$message.success('修改成功')
          this.handleQuery(this.pageIndex)
          this.editVisible = false
        }).catch(err => {
          let res = err.response.data
          this.$message.error(`修改失败 ${res.error.message}`)
        })
      },
    },
  }
</script>
<style scoped>
  .img-item {
    width: 100%;
    height: 160px;
  }

  .img-desp {
    /* width: 100%; */
    /* line-height: 20px; */
    height: 40px;
    /* overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis; */
  }

  .card-item {
    /* height: 225px; */
  }

  .card-item :global(.ant-card-body) {
    padding: 10px !important;
    color: red !important;
  }

  .ant-card-meta {
    width: 100%;
    height: 100%;
  }

</style>
