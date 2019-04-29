<template>
    <a-form @submit="handleSubmit"
            :form="form"
            class="form">

        <a-table :columns="columns"
                 :dataSource="data"
                 :pagination="false"
                 :loading="memberLoading">

            <template slot="poluTypeId"
                      slot-scope="text, record,index">
                <a-select style="width: 100%"
                          v-model="record.poluTypeId">
                    <a-select-option v-for="dic in yaopinDicArr"
                                     :key="dic.id"
                                     :value="dic.id">{{dic.name}}</a-select-option>
                </a-select>
            </template>
            <template slot="concentration"
                      slot-scope="text, record, index">
                <a-input-number style="width: 100%"
                                :precision="2"
                                :step="1"
                                v-model="record.concentration" />
            </template>
            <template slot="operation"
                      slot-scope="text, record">
                <a-popconfirm title="是否要删除此行？"
                              @confirm="remove(record.key)">
                    <a>删除</a>
                </a-popconfirm>
            </template>
        </a-table>
        <a-button style="width: 100%; margin-top: 16px; margin-bottom: 8px"
                  type="dashed"
                  icon="plus"
                  @click="handleAdd">新增</a-button>
        </a-card>
    </a-form>
</template>

<script>
    export default {
        name: 'CheckItem',
        props: {
            yaopinDicArr: Array
        },
        components: {
        },
        data() {
            return {
                form: null,
                memberLoading: false,
                columns: [
                    {
                        title: '因子',
                        dataIndex: 'poluTypeId',
                        width: '20%',
                        scopedSlots: { customRender: 'poluTypeId' }
                    },
                    {
                        title: '浓度',
                        dataIndex: 'concentration',
                        width: '30%',
                        scopedSlots: { customRender: 'concentration' }
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
        created() {
            this.form = this.$form.createForm(this);
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
                this.data.push({
                    medTypeId: null,
                    monthAmmount: null,
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
