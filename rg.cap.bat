@echo off  
cd /d %~dp0
set cd0=%cd%
cd ./scripts/rgb
del log.*.log rg.info.json 1>nul 2>nul 
rgb
if not exist rg.info.json goto end
rmdir /s/q "%cd0%/Sample1/obj/rg/cap" 1>nul 2>nul
copy /y rg.info.json "../rg/cap/rg.info.json"
cd ../rg/cap
del log.*.log 1>nul 2>nul 
rg --interceptslocation-attr-version 1
:end
cd %cd0%