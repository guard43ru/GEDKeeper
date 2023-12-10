@echo off

set APP_VER=3.4.1

call ..\clean.cmd

dotnet build ../projects/GKv3/GEDKeeper3.sln /p:Configuration=Release /p:Platform=MSWin86

set BUILD_STATUS=%ERRORLEVEL%
if %BUILD_STATUS%==0 goto installer
if not %BUILD_STATUS%==0 goto fail

:fail
pause 
exit /b %BUILD_STATUS% 

:installer
rem cd .\deploy
call gk_win_portable.cmd %APP_VER%
rem cd ..
pause
exit /b 0
