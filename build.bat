@echo off
cpch 65001
cls
vbc /t:exe /debug+ /debug:pdbonly /optionexplicit+ /optioninfer- /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ oc.vb