<template>
    <a-form @submit="handleSubmit"
            :form="form"
            class="form">

        <a-table :columns="columns"
                 :dataSource="data"
                 :pagination="false"
                 :loading="memberLoading">

            <template slot="poluTypeId"
                      slot-scope="text, record">
                <a-select style="width: 100%"
                          v-model="record.poluTypeId">
                    <a-select-option v-for="dic in poluTypeArr"
                                     :key="dic.id"
                                     :value="dic.id">{{dic.name}}</a-select-option>
                </a-select>
            </template>
            <template slot="concentration"
                      slot-scope="text, record">
                <a-input-number style="width: 100%"
                                :precision="2"
                                :step="1"
                                v-model="record.concentration" />
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
    </a-form>
</template>

<script>
    import Bus from '@/views/core/bus'
    export default {
        name: 'CheckItem',
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
                poluTypeArr: []
            }
        },
        created() {
            this.form = this.$form.createForm(this)
            Bus.$on('GetCompanyPoluType', (data) => {
                this.poluTypeArr = data
                // 清空已有的数据
                for (let d of this.data) {
                    d.poluTypeId = null
                }
            })
        },
        methods: {
            edit(data) {
                this.data = data.map((val, index) => {
                    val.key = new Date().getTime()
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
                    key: new Date().getTime(),
                    poluTypeId: null,
                    concentration: null,
                })
            },
            remove(index) {
                this.data.splice(index, 1)
            },
        }
    }
</script>
