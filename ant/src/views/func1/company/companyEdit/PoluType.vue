<template>
    <div>
        <a-table :columns="columns"
                 :dataSource="data"
                 :pagination="false"
                 :loading="memberLoading">
            <template v-for="(col, i) in ['name', 'workId', 'department']"
                      :slot="col"
                      slot-scope="text, record">
                <a-input :key="col"
                         v-if="record.editable"
                         style="margin: -5px 0"
                         :value="text"
                         :placeholder="columns[i].title"
                         @change="e => handleChange(e.target.value, record.key, col)" />
                <template v-else>{{ text }}</template>
            </template>
            <template slot="poluTypeId"
                      slot-scope="text, record">
                <a-select placeholder="请选择因子"
                          style="width: 100%"
                          v-model="record.poluTypeId"
                          v-decorator="[
                            'poluTypeId',
                            {rules: [{ required: true, message: '请选择废水类型'}]}
                          ]">
                    <a-select-option v-for="dic in yinziDicArr"
                                     :key="dic.id"
                                     :value="dic.id">{{dic.name}}</a-select-option>
                </a-select>
            </template>
            <template slot="unit"
                      slot-scope="text, record, index">
                <a-input v-model="record.unit"
                         style="width: 100%" />
            </template>
            <template slot="emissionStdType"
                      slot-scope="text, record">
                <a-input v-model="record.emissionStdType"
                         style="width: 100%" />
            </template>
            <template slot="operation"
                      slot-scope="text, record, index">
                <a-popconfirm title="是否要删除此行？"
                              @confirm="remove(index)">
                    <a>删除</a>
                </a-popconfirm>
            </template>
        </a-table>
        <a-button style="width: 100%; margin-top: 16px; margin-bottom: 8px"
                  type="dashed"
                  icon="plus"
                  @click="handleAdd">新增</a-button>
        </a-card>
    </div>
</template>

<script>
    export default {
        name: 'PpluType',
        props: {
            yinziDicArr: Array
        },
        components: {
        },
        data() {
            return {
                loading: false,
                memberLoading: false,
                columns: [
                    {
                        title: '污染因子',
                        dataIndex: 'poluTypeId',
                        width: '20%',
                        scopedSlots: { customRender: 'poluTypeId' }
                    },
                    {
                        title: '排放限值(单位）',
                        dataIndex: 'unit',
                        width: '20%',
                        scopedSlots: { customRender: 'unit' }
                    },
                    {
                        title: '排放标准名称(单位）',
                        dataIndex: 'emissionStdType',
                        scopedSlots: { customRender: 'emissionStdType' }
                    },
                    {
                        title: '操作',
                        key: 'action',
                        scopedSlots: { customRender: 'operation' }
                    }
                ],
                data: [],
                errors: []
            }
        },
        methods: {
            edit(data) {
                this.data = data.map((val, index) => {
                    val.key = index
                    return val
                })
            },
            handleSubmit(e) {
                e.preventDefault()
                this.form.validateFields((err, values) => {
                    if (!err) {
                    }
                })
            },
            handleAdd() {
                const length = this.data.length
                this.data.push({
                    key: length === 0 ? '1' : (parseInt(this.data[length - 1].key) + 1).toString(),
                    poluTypeId: null,
                    unit: null,
                    emissionStdType: null,
                })
            },
            remove(index) {
                this.data.splice(index, 1)
            },
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
