#!/bin/sh

xbuild ./projects/GEDKeeper2.sln /p:Configuration=Release /p:Platform="x64" /p:MonoCS=true /p:TargetFrameworkVersion=v4.6.2 /v:quiet
cd ./deploy/
sh ./gk2_linux_deb_package.sh
