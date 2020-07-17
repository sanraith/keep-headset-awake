@echo off
set target=bin\keep-headset-awake
rmdir /S /Q %target%
del /Q %target%.zip
dotnet build --force --no-incremental -c Release -p:CustomOutputType=Exe -o %target%\console
dotnet build --force --no-incremental -c Release -p:CustomOutputType=WinExe -o %target%\hidden
move %target%\console\*.* %target%
move %target%\hidden\keep-headset-awake.exe %target%\keep-headset-awake-NOCONSOLE.exe
rmdir /S /Q %target%\console
rmdir /S /Q %target%\hidden
del /Q %target%\*.dev.json
del /Q %target%\*.deps.json
del /Q %target%\*.pdb
powershell -Command "Compress-Archive -Force .\%target% %target%.zip"