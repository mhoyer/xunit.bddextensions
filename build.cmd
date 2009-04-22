@echo off

cd tools\AssertExtensions
msbuild /p:Configuration=Debug
msbuild /p:Configuration=Release

cd ..\..

msbuild /p:Configuration=Debug
msbuild /p:Configuration=Release
