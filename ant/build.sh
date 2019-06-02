yarn run build
rm -rf ../aspnet-core/src/MyERP.Web.Host/wwwroot/admin
cp -R dist/  ../aspnet-core/src/MyERP.Web.Host/wwwroot/admin