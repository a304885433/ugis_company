<template>
    <a-form :form="form"
            @submit="handleSubmit">
        <a-form-item v-for="(k, index) in formKeys"
                     :key="k"
                     :required="false">
            <a-input v-decorator="[
                `names${k}`,
                {
                  validateTrigger: ['change', 'blur'],
                  preserve: true,
                  rules: [{
                    required: true,
                    whitespace: true,
                    message: 'Please input passenger\'s name or delete this field.',
                  }],
                }
              ]"
                     placeholder="passenger name"
                     style="width: 60%; margin-right: 8px" />
            <a-icon v-if="formKeys.length > 1"
                    class="dynamic-delete-button"
                    type="minus-circle-o"
                    :disabled="formKeys.length === 1"
                    @click="() => remove(index)" />
        </a-form-item>
        <a-form-item v-bind="formItemLayout">
            <a-button type="dashed"
                      style="width: 60%"
                      @click="add">
                <a-icon type="plus" /> Add field
            </a-button>
        </a-form-item>
        <a-form-item v-bind="formItemLayout">
            <a-button type="primary"
                      html-type="submit">
                Submit
            </a-button>
        </a-form-item>
    </a-form>
</template>

<script>
    let id = 0;
    export default {
        data() {
            return {
                formKeys: [],
                formItemLayout: {
                    labelCol: {
                        xs: { span: 24 },
                        sm: { span: 4 },
                    },
                    wrapperCol: {
                        xs: { span: 24 },
                        sm: { span: 20 },
                    },
                },
            };
        },
        beforeCreate() {
            this.form = this.$form.createForm(this);
            // this.form.getFieldDecorator('keys', { initialValue: [], preserve: true });
        },
        methods: {
            remove(k) {
                this.formKeys.splice(k, 1)
            },

            add() {
                this.formKeys.push(this.formKeys.length)
            },

            handleSubmit(e) {
                e.preventDefault();
                this.form.validateFields((err, values) => {
                    debugger
                    if (!err) {
                        console.log('Received values of form: ', values);
                    }
                });
            },
        },
    };
</script>
<style>
    .dynamic-delete-button {
        cursor: pointer;
        position: relative;
        top: 4px;
        font-size: 24px;
        color: #999;
        transition: all .3s;
    }

    .dynamic-delete-button:hover {
        color: #777;
    }

    .dynamic-delete-button[disabled] {
        cursor: not-allowed;
        opacity: 0.5;
    }

</style>
