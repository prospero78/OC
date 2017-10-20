echo off
rem chcp 65001
cls
echo Сборка модКонвертер2vb.vb
vbc /t:library /debug+ /nologo /optionexplicit+ /removeintchecks- /codepage:65001 /platform:x86 /utf8output+ модКонвертер2vb.vb