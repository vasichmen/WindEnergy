chcp 1251

Set /P ver=  ������� ������ ��� ����������� ^>
echo %ver%
Set n=windenergy_
Set ext=.zip
Set name=%n%%ver%%ext%
echo %name%
"C:\Program Files\7-Zip\7z.exe" a -tzip -ssw -mx7 -r0 -x@exclusions_arhive_program.txt "D:\Clouds\Projects\CS\WindEnergy\%name%" "D:\Clouds\Projects\CS\WindEnergy\UI\bin\Release\"
