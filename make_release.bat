chcp 1251
set basedir=D:\Clouds\Projects\CS\WindEnergy
set reldir=D:\Clouds\Projects\CS\WindEnergy\WindEnergy\bin\Release
echo %basedir%

rmdir /s/q %basedir%\Release\
mkdir %basedir%\Release\

xcopy %reldir% %basedir%\Release\ /H /Y /C /R /S /EXCLUDE:listnotcopy.txt
xcopy %reldir%\obfuscated\*.dll %basedir%\Release\libs\ /Y
xcopy %reldir%\obfuscated\*.exe %basedir%\Release\ /Y

del /s/q %basedir%\Release\Installer.exe
xcopy %basedir%\Installer_full.exe %basedir%\Release\ /Y
xcopy %basedir%\Installer_demo.exe %basedir%\Release\ /Y

copy  %basedir%\Release\Installer.exe.config %basedir%\Release\Installer_full.exe.config 
move  %basedir%\Release\Installer.exe.config %basedir%\Release\Installer_demo.exe.config 
