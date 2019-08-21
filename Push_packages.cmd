@echo off

set nuget=nuget\nuget.exe
set feed=https://www.myget.org/F/tseemann-dk/api/v2/package
set apikey=51601496-2449-4ceb-ac2d-dd3c9d5a61bf
set nuspecs=nuget
set output=nuget
set version=2.0.1
set tools_version=0.3.2
set config=Debug

rem -- PREPARE
del %output%\*.nupkg

rem --- PACK
%nuget% pack %nuspecs%\Redux.NET.nuspec -Version %version% -Properties Configuration=%config% -OutputDirectory %output%
%nuget% pack %nuspecs%\Redux.NET.DevTools.nuspec -Version %tools_version% -Properties Configuration=%config% -OutputDirectory %output%

rem --- PUSH
%nuget% push %output%\*.nupkg -src %feed% -apikey %apikey%
