# 发布操作

工作目录 build

- 子目录 host 存放运行站点的全部文件
- 子目录 updateDb 存放数据库升级程序
- 子目录 pack 存放本次变更的增量程序集

- 运行build.ps1进行后台和前端编译，将编译的文件，写入到当前host目录
- 运行pack.bat，拷贝增量文件到pack目录，并更新数据库升级程序


# 数据库升级

- updateDb/appsettings.json  配置需要更新的数据库信息
- 运行updateDb/updatedb.bat，进行数据库升级