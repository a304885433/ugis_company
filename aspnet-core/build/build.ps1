# COMMON PATHS

$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$slnFolder = Join-Path $buildFolder "../"
$outputFolder = $buildFolder
$webHostFolder = Join-Path $slnFolder "src/MyERP.Web.Host"

## CLEAR ######################################################################

Remove-Item $outputFolder -Force -Recurse -ErrorAction Ignore
New-Item -Path $outputFolder -ItemType Directory

## RESTORE NUGET PACKAGES #####################################################

Set-Location $slnFolder
dotnet restore

## PUBLISH WEB Host PROJECT ###################################################

Set-Location $webHostFolder
MKDIR host
dotnet publish --output (Join-Path $outputFolder "host")

## Build Vue And Copy
$vueFolder = Join-Path $slnFolder "../ant"
Set-Location $vueFolder
yarn install
yarn build
XCOPY /y /s /e (Join-Path $slnFolder "../ant/dist/*.*") (Join-Path $outputFolder "host/wwwroot")

## CREATE DOCKER IMAGES #######################################################

# host
Set-Location (Join-Path $outputFolder "host")

# docker rmi abp/host -f
# docker build -t abp/host .

## DOCKER COMPOSE FILES #######################################################

Copy-Item (Join-Path $slnFolder "docker/host/*.*") $outputFolder

## FINALIZE ###################################################################
Set-Location $outputFolder
