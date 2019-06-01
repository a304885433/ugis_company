cd host

# 删除不需要的文件
del *.xml
del *.pdb

# 拷贝需要更新的文件
rd "../pack/" /s /q
mkdir  "../pack/"
xcopy /f /y .*.dll "../pack/"
xcopy /s /f "wwwroot/*.*"  "../pack/wwwroot/"

# 更新数据库升级程序
xcopy /f /y MyERP.*.dll "../updateDb/"

pause > nul