### 如何对请求做验证

参考
[ValidationAttribute](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.validationattribute(v=vs.110).aspx)

示例
[CreateProduct API](https://git.mingyuanyun.com/platform/rdc/AbpExample/blob/master/src/AbpExample.Application/Products/ProductAppService.cs)

- Data Annotations 验证

``` C#
    public class ProductDto
    {
        [Required] //不为空
        [StringLength(Product.MaxNameLength)] //最大长度
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
```

- 自定义验证, 实现ICustomValidate接口

``` C#
 public void AddValidationErrors(CustomValidationContext context)
{
    if (Price < 10 || Price > 1000)
    {
        context.Results.Add(new ValidationResult($"价格不在允许的范围(10-1000)"));
    }
}
```

- 请求补偿，场景：设置默认值，对请求的数据做修正。实现IShouldNormalize 接口

``` C#
public void Normalize()
{
    if (Name.Equals("computer", StringComparison.OrdinalIgnoreCase))
    {
        Price = 8888.88M;
    }
}
```

### 如何做分页查询

示例
[GetProducts API](https://git.mingyuanyun.com/platform/rdc/AbpExample/blob/master/src/AbpExample.Application/Products/ProductAppService.cs)

### 如何添加配置信息

示例
[GetAppSetting API](https://git.mingyuanyun.com/platform/rdc/AbpExample/blob/master/src/AbpExample.Application/Configuration/ConfigurationAppService.cs)