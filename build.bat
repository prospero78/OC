chcp 65001
cls
vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ modCocon.vb
vbc /t:exe /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ /r:modCocon.dll oc.vb