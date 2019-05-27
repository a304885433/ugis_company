
#### Add-Migration
新增、修改实体，使用Add-Migration命令来得到Migration
MyERP.EntityFrameworkCore 下运行

> 命令示例
- EntityFrameworkCore\Add-Migration
- Add-Migration AddItem -Verbos
- Add-Migration AddItem -Verbos -ConnectionString "Data Source=.;Initial Catalog=TestDb;Integrated Security=False;User ID=sa;Password=***;MultipleActiveResultSets=True" ConnectionProviderName "System.Data.SqlClient"

### Update-Database 
将Migration同步到数据库
>命令示例
- EntityFrameworkCore\Update-Database 
- EntityFrameworkCore\Update-Database -Verbos
- EntityFrameworkCore\Update-Database -Script -Verbos （同步的同时返回Sql）
- EntityFrameworkCore\Update-Database -Script  20190501091050_add_ChkUserName.cs
- EntityFrameworkCore\Update-Database -Script -SourceMigration: 20190417100906_init_db.cs -TargetMigration: 20190501091050_add_ChkUserName.cs
- Update-Database -Verbos -ConnectionString "Data Source=.;Initial Catalog=TestDb;Integrated Security=False;User ID=sa;Password=***;MultipleActiveResultSets=True" ConnectionProviderName "System.Data.SqlClient"
- Update-Database -TargetMigration:"MigrationName" （更新到指定版本）
- Update-Database –TargetMigration: AddBlogUrl （EF 更新到指定版本）
- Update-Database –TargetMigration: $InitialDatabase （如果你想回滚一切至空数据库，可以使用命令 ）
- Update-Database –Migration: AddBlogUrl （EF Core 更新到指定版本）

### Enable-Migrations
启用迁移
> 命令示例
- Enable-Migrations
- Enable-Migrations -ContextTypeName SmartSnsPublisher.Web.Models.SiteDbContext

### 删除最近一次迁移
- EntityFrameworkCore\remove-migration





