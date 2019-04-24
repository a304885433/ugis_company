# COMMON PATHS

$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$slnFolder = Join-Path $buildFolder "../"
$outputFolder = Join-Path $buildFolder "outputs"
$webHostFolder = Join-Path $slnFolder "src/MyERP.Web.Host"


echo (Join-Path $slnFolder "../../ant/dist/*.*")

## COPY VUE To WWW
## Copy-Item (Join-Path $vueFolder "dist/*.*") (Join-Path $outputFolder "host/wwwroot")
XCOPY /y /s /e (Join-Path $slnFolder "../ant/dist/*.*") (Join-Path $outputFolder "host/wwwroot")

## CREATE DOCKER IMAGES #######################################################

# host
## Set-Location (Join-Path $outputFolder "host")

# docker rmi abp/host -f
# docker build -t abp/host .

## DOCKER COMPOSE FILES #######################################################

## Copy-Item (Join-Path $slnFolder "docker/host/*.*") $outputFolder

## FINALIZE ###################################################################

Set-Location $outputFolder

echo ok
pause