@echo off
nuget pack MeliMelo.nuspec
..\packages\squirrel.windows.1.2.3\tools\Squirrel.exe --releasify MeliMelo.1.2.0.nupkg
