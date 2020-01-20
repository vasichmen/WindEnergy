chcp 1251
set basedir=D:\Clouds\Projects\CS\WindEnergy
set reldir=D:\Clouds\Projects\CS\WindEnergy\WindEnergy\bin\Release
echo %basedir%

rmdir /s/q %basedir%\Release\
mkdir %basedir%\Release\

xcopy %reldir% %basedir%\Release\ /H /Y /C /R /S /EXCLUDE:listnotcopy.txt
xcopy %reldir%\obfuscated\*.dll %basedir%\Release\libs\ /Y
xcopy %reldir%\obfuscated\*.exe %basedir%\Release\ /Y
