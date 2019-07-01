<template>
    <div>

        <a-form :form="form"
                class="form"
                :submit="handleSubmit">

            <a-form-item label="企业">
                <a-select placeholder="请选择企业"
                          v-decorator="[ 'companyId',
                              {rules: [{ required: true, message: '请选择企业'}]}
                            ]"
                          @change="handleCompanyChange">
                    <a-select-option v-for="item in companyArr"
                                     :key="item.id"
                                     :value="item.id">{{item.name}}</a-select-option>
                </a-select>
            </a-form-item>

            <a-form-item label="说明">
                <a-input placeholder="请输入说明"
                         v-decorator="['remark' ]" />
            </a-form-item>


            <a-form-item v-bind="formItemLayout"
                         style="text-align: left;"
                         label="图片"
                         extra="">
                <a-upload v-decorator="['file', {
valuePropName: 'fileList',
getValueFromEvent: normFile,
rules: [{ validator: validateFile }]
}]"
                          name="files"
                          action="/api/File/Upload"
                          @preview="handlePreview"
                          @change="handleChange"
                          list-type="picture-card">
                    <a-button>
                        <div>
                            <a-icon type="plus" />
                            <!-- <div class="ant-upload-text">上传</div> -->
                        </div>
                    </a-button>
                </a-upload>
            </a-form-item>
        </a-form>

        <a-modal :visible="previewVisible"
                 :footer="null"
                 @cancel="handleCancel">
            <img alt="example"
                 style="width: 100%"
                 :src="previewImage" />
        </a-modal>

        <!-- fixed footer toolbar -->
        <footer-tool-bar
                         :style="{ width: isSideMenu() && isDesktop() ? `calc(100% - ${sidebarOpened ? 256 : 80}px)` : '100%'}">
            <a-button type="primary"
                      @click="handleSubmit"
                      v-action="['create','update']"
                      style="margin-right: 10px;"
                      :loading="loading">提交</a-button>
            <a-button type="default"
                      @click="$router.go(-1)">取消</a-button>
        </footer-tool-bar>
    </div>
</template>

<script>
    import FooterToolBar from '@/components/FooterToolbar'
    import { mixin, mixinDevice } from '@/utils/mixin'
    import { CompanyFile } from '@/api/'

    export default {
        name: 'UserEdit',
        mixins: [mixin, mixinDevice],
        components: {
            FooterToolBar,
        },
        props: {
            companyArr: {
                type: Array
            }
        },
        data() {
            return {
                errors: [],
                form: this.$form.createForm(this),
                loading: false,
                mdl: null,
                roles: [],
                previewImage: '',
                previewVisible: false,
                formItemLayout: {
                    labelCol: { md: 2, align: 'left', xs: 24 },
                    wrapperCol: { md: 14, offset: 2, xs: 24 },
                },
            }
        },
        created() {
            this.companyArr = this.$route.params.companyArr
        },
        methods: {
            handleSubmit() {
                this.form.validateFields((err, values) => {
                    if (err) {
                        return
                    }
                    this.loading = true
                    let files = this.getUploadFile(values.file);
                    return CompanyFile.Create({
                        remark: values.remark,
                        companyId: values.companyId,
                        files
                    }).then(() => {
                        this.$router.go(-1);
                    }).finally(() => {
                        this.loading = false
                    })
                })
            },
            normFile(e) {
                if (Array.isArray(e)) {
                    return e;
                }
                return e && e.fileList;
            },
            handleCancel() {
                this.previewVisible = false
            },
            handlePreview(file) {
                this.previewImage = file.url || file.thumbUrl
                this.previewVisible = true
            },
            filePreview(file) {
                let url = file.url
                let ext = file.ext
                if (!url && file.response) {
                    let r = file.response.result
                    if (!r || !r.uid) {
                        return
                    }
                    ext = r.ext
                    url = `http://${location.host}/api/file/download?id=${r.uid}&name=${r.name}`
                }
                ext = ext.toLowerCase()
                if (ext == '.jpg' || ext == '.png' || ext == '.bmp' || ext == '.jpeg') {
                    this.$success({
                        title: null,
                        width: 600,
                        okText: '确定',
                        content: (  // JSX support
                            <div>
                                <img style="width:500px;" src={url} />
                            </div>
                        ),
                    });
                } else {
                    window.open(url, '_blank')
                }
            },
            parseFile(fileJSON) {
                if (!fileJSON) return []
                let filelist = JSON.parse(fileJSON)
                return filelist.filter(t => t != null).map(t => {
                    t.status = 'done'
                    t.url = `http://${location.host}/api/file/download?id=${t.uid}&name=${t.name}`
                    return t;
                })
            },
            validateFile(rule, value, callback) {
                if (!value) {
                    callback(new Error('至少上传一张图片...'))
                    return;
                }
                for (let file of value) {
                    if (file.status == 'error') {
                        const lt10M = file.size / 1024 / 1024 < 10
                        if (!lt10M) {
                            callback(new Error(`${file.name} 不能超过 10M `))
                            return
                        } else {
                            callback(new Error(`${file.name} 上传失败 `))
                        }
                    }
                    if (file.percent < 100) {
                        callback(new Error('上传中...'))
                        return
                    }
                }
                // Note: 必须总是返回一个 callback，否则 validateFieldsAndScroll 无法响应
                callback()
            },
            handleChange({ fileList }) {
                this.fileList = fileList
            },
            // 读取文件
            getUploadFile(fileList) {
                if (!fileList || !fileList.length) return null
                let res = fileList.map((file) => {
                    if (file.status != 'done') {
                        throw new Error('存在未成功上传的文件，请检查！')
                    }
                    return file.response ? file.response.result : file
                })
                return JSON.stringify(res)
            },
        }
    }

</script>
<style>
    /* you can make up upload button and sample style by using stylesheets */

    .ant-upload-select-picture-card i {
        font-size: 32px;
        color: #999;
    }

    .ant-upload-select-picture-card .ant-upload-text {
        margin-top: 8px;
        color: #666;
    }

</style>
