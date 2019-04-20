## 开发指南

### 开发环境配置
- Visual Studio 2017 版本 15.3.0 或更高版本
- .Net Core 2.1.1  
    - https://www.microsoft.com/net/download/Windows/build

### 第一个接口
功能： 根据商品名查询商品信息


#### 1. 创建商品实体
> Mysoft.RDC.Domain\Products\Product.cs

``` c#
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Mysoft.RDC.Products
{
    //可以显示的指定表名，不指定默认是实体名+s
    [Table("Product")]
    public class Product : FullAuditedEntity<long>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}

```

#### 2. 创建商品DomainService
> Mysoft.RDC.Domain\Products\ProductDomainService.cs

``` c#
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Microsoft.EntityFrameworkCore;

namespace Mysoft.RDC.Products
{
    public class ProductDomainService : DomainService
    {
        private readonly IRepository<Product, long> _productRepository;

        public ProductDomainService(IRepository<Product, long> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByName(string name)
        {
            var query = from p in _productRepository.GetAll()
                where p.Name == name
                select p;
            var product = await query.FirstOrDefaultAsync();
            if (product == null)
            {
                throw new UserFriendlyException($"商品({name})不存在");
            }
            if (product.Price < 0)
            {
                throw new UserFriendlyException($"商品({name})的价格小于0，请检查");
            }
            return product;
        }
    }
}
```

#### 3. 创建商品ApplicationService
##### 3.1 定义Dto
> Mysoft.RDC.Application\Products\Dto\ProductDto

``` c#
using Abp.AutoMapper;

namespace Mysoft.RDC.Products.Dto
{
    [AutoMapFrom(typeof(Product))]
    public class ProductDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}

```
> Mysoft.RDC.Application\Products\Dto\GetProductByNameInput

``` c#
using System.ComponentModel.DataAnnotations;

namespace Mysoft.RDC.Products.Dto
{
    public class GetProductByNameInput
    {
        [Required]
        public string Name { get; set; }
    }
}
```
> Mysoft.RDC.Application\Products\Dto\GetProductByNameOutput
```
namespace Mysoft.RDC.Products.Dto
{
    public class GetProductByNameOutput 
    {
        public ProductDto Product { get;set;}
    }
}
```

##### 3.2 定义ApplicationService接口
> Mysoft.RDC.Application\Products\IProductAppService.cs

``` c#
using System.Threading.Tasks;
using Abp.Application.Services;
using Mysoft.RDC.Products.Dto;

namespace Mysoft.RDC.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<GetProductByNameOutput> GetProductByName(GetProductByNameInput input);
    }
}
```

##### 3.3 商品ApplicationService实现
> Mysoft.RDC.Application\Products\ProductAppService.cs

``` c#
using System.Threading.Tasks;
using Mysoft.RDC.Products.Dto;

namespace Mysoft.RDC.Products
{
    public class ProductAppService: RDCAppServiceBase, IProductAppService
    {
        private readonly ProductDomainService _productDomainService;
        public ProductAppService(ProductDomainService productDomainService)
        {
            _productDomainService = productDomainService;
        }

        public async Task<GetProductByNameOutput> GetProductByName(GetProductByNameInput input)
        {
            //1.将input dto转换为domain obj

            //2.调用doamin service
            var item = await _productDomainService.GetProductByName(input.Name);
            //call other doamin serivce

            //3.将domain obj转换为output dto
            var itemDto = ObjectMapper.Map<ProductDto>(item);

            return new GetProductByNameOutput
            {
                Product = itemDto
            };
        }
    }
}
```

#### 4. 数据库
##### 4.1 数据库实体映射
> Mysoft.RDC.EntityFrameworkCore\EntityFrameworkCore\RDCDbContext.cs

``` c#
public DbSet<Product> Products { get; set; }
```

##### 4.2 生成migration文件

```
Add-Migration <name> -Verbos
```

##### 4.3 更新数据库
```
Update-Database -Verbos
```

##### 4.4 注意事项:
- EF CodeFirst模式，实体模型与数据库必须一致，所以每个开发人员在开发过程中使用自己单独的库
- 字段重命名，例如将Name重命名为Description，EF默认是删除Name，新增Description, 这个时候需要手动修改生成Migration文件的内容(修改为rename)

##### 4.5 EF性能
- 不可否认任何中间件在带来便利的同时，都会有额外的资源开销
- EF结合Linq To Sql， 在实际执行过程中，将Linq表达式转换为sql，执行Sql，所以和直接执行sql相比，EF的额外开销就是将Linq表达式转换为Sql的过程, 这是毫秒级的，不会带来性能问题
- 通常认为的EF性能问题，更多的是场景使用问题，比如在Linq表达式转换为Sql的过程中，转换出的实际sql与我们所期望的不太一致(尽管查询出的结果一样), 特别是比较复杂的Linq转换. 这种情况下，一是可以建立视图，二是EF也支持执行Sql.


#### 5. 运行服务、测试
设置Mysoft.RDC.Web.Host为启动项目，直接在Visual Studio中运行
